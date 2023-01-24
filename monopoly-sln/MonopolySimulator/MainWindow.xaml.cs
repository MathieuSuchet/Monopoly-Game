using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using MonopolyLib;
using MonopolyLib.Logique.Events;
using MonopolyLib.Logique.Joueurs;
using MonopolyLib.Logique.PartieComponents.Parties;
using Newtonsoft.Json;

namespace MonopolySimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Tour> All { get; set; }
        private Tour _current;

        private readonly ListJoueurs serialized = new ListJoueurs();

        private Partie _partie;
        public List<Joueur> Joueurs { get; set; }

        private int _cpt = 1;

        private bool _started;

        private PartieResultat results = new PartieResultat();

        public Joueur? FocusedPlayer { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ExteriorEvents.PlayerResume += OnPlayerResume;
            ExteriorEvents.PartieStarted += OnPartieStarted;
            ExteriorEvents.PartieResumé += OnPartieResumé;
            ExteriorEvents.PartieEnded += OnPartieEnded;
            ExteriorEvents.PartieNextTurn += OnPartieNextTurn;

            DetailsResume.Content = new CroissanceWindow();
            
            Joueurs = new List<Joueur>
            {
                new JoueurIa("Player 1", true),
                new JoueurIa("Player 2", true),
                new JoueurIa("Player 3", true),
                new JoueurIa("Player 4", true),
            };

            DataContext = this;
        }

        private void Add(Tour t)
        {
            Tours.ItemsSource ??= All;
            All.Add(t);
            OnPropertyChanged(nameof(All));
        }

        private void OnPartieNextTurn(object? sender, PartieResumeArgs e)
        {
            _cpt++;

            /*for (int index = 0; index < Joueurs.Count; index++)
            {
                _current.Resume[index] += _current.Changes.Count == 0 ? "No changes\n" : "Changes : \n";
                if (_current.Changes.Count == 0) continue;

                Dictionary<Joueur, List<Change>> list = _current.Changes.Where((x,i) => x.Value.Any(x => x.Source == _current.Joueurs[index])).ToDictionary(x => x.Key, y => y.Value);

                 if (list.Count != 0)
                 {
                     _current.Resume[index] += "Loss : \n";
                     foreach (var j in list.Select(jList => jList.Key))
                     {
                         for (int i = 0; i<list[j].Count; i++)
                         {
                             Change change = list[j][i];
                             change.Dest ??= new JoueurIa("Banque", true);
                             change.Source ??= new JoueurIa("Banque", true);
                             _current.Resume[index] += $"{change.Source.Nom} => {change.Dest.Nom} ({-change.ChangeAmount})\n";
                         }
                         _current.Resume[index] += "\n";
                     }
                 }

                 list = _current.Changes.Where((x,i) => x.Value.Any(x => x.Dest == _current.Joueurs[index])).ToDictionary(x => x.Key, y => y.Value);
                if (list.Count == 0) continue;
                _current.Resume[index] += "Win : \n";
                foreach (var j in list.Select(jList => jList.Key))
                {
                    for (int i = 0; i<list[j].Count; i++)
                    {
                        Change change = list[j][i];
                        change.Dest ??= new JoueurIa("Banque", true);
                        change.Source ??= new JoueurIa("Banque", true);
                        _current.Resume[index] += $"{change.Dest.Nom} <= {change.Source.Nom} ({change.ChangeAmount})\n";
                    }
                    _current.Resume[index] += "\n";
                }
            
            }*/
            Add(_current);
            _current = new Tour(_cpt);
        }

        private void OnPartieEnded(object? sender, PartieResumeArgs e)
        {
            _cpt = 1;
            ClearJson();
            Joueurs.Clear();
            ToJson();
        }

        private void OnPartieResumé(object? sender, PartieResumeArgs e)
        {
        }

        private void OnPartieStarted(object? sender, PartieStartArgs e)
        {
            ClearJson();
            All.Clear();
            OnPropertyChanged(nameof(All));
            Joueurs = e.Partie.Joueurs;
            OnPropertyChanged(nameof(Joueurs));
            
            foreach (var player in Joueurs)
            {
                results.StatsMap.Add(player.Nom, new Stats());
            }

            _partie = e.Partie;
            serialized.Clear();

            foreach (Joueur joueur in Joueurs)
            {
                joueur.OnPlayerTurnStarted += OnPlayerTurnStarted;
                joueur.OnPlayerTurnEnded += OnPlayerTurnEnded;
                
                joueur.OnPlayerMovement += OnPlayerMovement;
                
                joueur.OnPlayerMoneyChanged += OnPlayerMoneyChanged;
                
                joueur.OnPlayerBuyCase += OnPlayerBuyCase;
                joueur.OnPlayerSellCase += OnPlayerSellCase;
                
                joueur.OnPlayerBuyHouse += OnPlayerBuyHouse;
                joueur.OnPlayerSellHouse += OnPlayerSellHouse;
                
                joueur.OnPlayerTransaction += OnPlayerTransaction;
                
                joueur.OnPlayerCaseDepart += OnPlayerCaseDepart;
            }
        }

        private void OnPlayerCaseDepart(object? sender, EventArgs e)
        {
            results.StatsMap[(sender as Joueur)!.Nom].PassagesCaseDepart++;
        }

        private void OnPlayerSellHouse(object? sender, PlayerHouseArgs e)
        {
            OnPlayerTransaction((sender as Joueur)!, new PlayerTransactionArgs(e.NbMaisons * e.Target.PrixUnitMaison, null, sender as Joueur, $"Vente de {e.NbMaisons}"));
            results.StatsMap[(sender as Joueur)!.Nom].NbMaisonsVendues += e.NbMaisons;
        }

        private void OnPlayerBuyHouse(object? sender, PlayerHouseArgs e)
        {            
            OnPlayerTransaction((sender as Joueur)!, new PlayerTransactionArgs(e.NbMaisons * e.Target.PrixUnitMaison, (sender as Joueur)!, null, $"Achat de {e.NbMaisons} maisons"));
            results.StatsMap[(sender as Joueur)!.Nom].NbMaisonsAchetees += e.NbMaisons;
        }

        private void OnPlayerSellCase(object? sender, PlayerCaseArgs e)
        {
            OnPlayerTransaction(sender, new PlayerTransactionArgs(e.Argent, null, (sender as Joueur)!, $"Vente de {e.CaseAchetee.Nom}"));
            results.StatsMap[(sender as Joueur)!.Nom].NbCasesVendues++;
        }

        private void OnPlayerBuyCase(object? sender, PlayerCaseArgs e)
        {
            //OnPlayerTransaction(sender, new PlayerTransactionArgs(e.Argent, (sender as Joueur)!, null));
            results.StatsMap[(sender as Joueur)!.Nom].NbCasesAchetees++;
        }

        private void OnPlayerMoneyLose(object? sender, PlayerMoneyMovementArgs e)
        {
            results.StatsMap[(sender as Joueur)!.Nom].ArgentPerdu += e.Argent;
        }

        private void OnPlayerMoneyChanged(object? sender, PlayerMoneyMovementArgs e)
        {
            if (e.Lost)
            {
                //OnPlayerTransaction(sender, new PlayerTransactionArgs(e.Argent, (sender as Joueur)!, null));
                OnPlayerMoneyLose(sender, e);
            }
            else
            {
                //OnPlayerTransaction(sender, new PlayerTransactionArgs(e.Argent,null , (sender as Joueur)!));
                OnPlayerMoneyWin(sender, e);
            }
        }

        private void OnPlayerMoneyWin(object? sender, PlayerMoneyMovementArgs e)
        {
            results.StatsMap[(sender as Joueur)!.Nom].ArgentGagne += e.Argent;
        }

        private void OnPlayerTurnStarted(object? sender, PlayerStartTurnArgs e)
        {
            
        }

        private void OnPlayerMovement(object? sender, PlayerMoveArgs e)
        {
            Joueur j = (sender as Joueur)!;
            if (!_current.Avancements.ContainsKey(j))
            {
                _current.Avancements.Add(j, new List<string>());
                return;
            }

            //_current.Avancements[j].Add($"{e.OldPosition} => {e.NewPosition}");
            results.StatsMap[j.Nom].NbCasesParcourues++;
        }

        private void OnPlayerTransaction(object? sender, PlayerTransactionArgs e)
        {
            Joueur banque = new JoueurIa("Banque", true);

            Change c = new Change(e.Change, e.Source ?? banque, e.Dest ?? banque, e.Reason);
            
            _current.Changes.Add(c);
            Console.WriteLine($"{e.Source} ({e.Change}) => {e.Dest}");
        }

        private void OnPlayerTurnEnded(object? sender, PlayerTurnArgs e)
        {
            Joueur j = (sender as Joueur)!;
            if (!_current.Croissances.ContainsKey(j))
            {
                _current.Croissances.Add(j, $"Croissance => {e.NewArg - e.OldArg}\n");
            }
            else
            {
                _current.Croissances[j] = $"Croissance => {e.NewArg - e.OldArg}\n";
            }

            if (!_current.Avancements.ContainsKey(j))
            {
                _current.Avancements.Add(j, new List<string>());
            }
            else
            {
                _current.Avancements[j].Add($"{e.OldPos} => {e.NewPos}");
            }
        }


        private void OnPlayerResume(object? sender, PlayerResumeArgs e)
        {
            if (_current.Joueurs.All(x => x != e.Joueur))
            {
                _current.Joueurs.Add(e.Joueur);
            }

            int index = _current.Joueurs.IndexOf(e.Joueur);
            if (_current.Resume.Count <= index)
            {
                _current.Resume.Add(string.Empty);
            }

            _current.Resume[index] += e.Resume;
            serialized.Add(e.Joueur);
        }

        private void SinglePartie_OnClick(object sender, RoutedEventArgs e)
        {
            if (_started) return;
            _started = true;

            _current = new Tour(_cpt);
            _cpt++;

            All = new ObservableCollection<Tour>();
            _current = new Tour(_cpt);
            new Thread(AnimateText).Start();
            results = new PartieResultat();
            
            Joueurs = new List<Joueur>
            {
                new JoueurIa("Player 1", true, 10000),
                new JoueurIa("Player 2", true, 10000),
                new JoueurIa("Player 3", true, 10000),
                new JoueurIa("Player 4", true, 10000),
            };

            Partie p = new PartieChaos(Joueurs, false, true);
            p.Gestionnaire();
            _started = false;
            Stats.Visibility = Visibility.Visible;
        }

        private void MultipleParties_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Tours_OnSelected(object sender, RoutedEventArgs e)
        {
            if ((sender as ListBox)!.SelectedItems.Count == 0) return;
            (DetailsResume?.Content as ControlMonopoly)!.ClearContent();
            (DetailsResume.Content as ControlMonopoly)!.Tour = All[Tours.SelectedIndex];
            Players.ItemsSource = All[Tours.SelectedIndex].Joueurs;
            OnPropertyChanged(nameof(Players.ItemsSource));
        }

        private void AnimateText()
        {
            int cpt = 0;
            while (_started)
            {
                State.Dispatcher.Invoke(() => State.Text = "Ongoing");
                while (cpt < 3)
                {
                    if (!_started)
                    {
                        break;
                    }

                    State.Dispatcher.Invoke(() => State.Text += ".");
                    cpt++;
                    Thread.Sleep(300);
                }

                cpt = 0;
            }

            State.Dispatcher.Invoke(() => State.Text = "Ended");
        }

        private void ClearJson()
        {
            File.Delete("../../../../MonopolyStats/Croissances.json");
            File.Delete("../../../../MonopolyStats/Stats.json");
        }

        private void ToJson()
        {
            using StreamWriter stream = new StreamWriter(File.OpenWrite("../../../../MonopolyStats/Croissances.json"));
            stream.WriteLine(JsonConvert.SerializeObject(serialized));

            using StreamWriter streamWriter = new StreamWriter(File.OpenWrite("../../../../MonopolyStats/Stats.json"));
            streamWriter.WriteLine(JsonConvert.SerializeObject(results));
        }

        private void Categories_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Joueur? j = null;
            Tour? t = null;
            if (All is null || All.Count == 0) return;
            if ((sender as ListBox)!.SelectedIndex == -1) return;
            if (Tours.SelectedIndex != -1)
            {
                t = All[Tours.SelectedIndex];
            }

            if (Players.SelectedIndex != -1)
            {
                j = t?.Joueurs[Players.SelectedIndex];
            }

            (DetailsResume?.Content as ControlMonopoly)!.ClearContent();
            DetailsResume.Content = (sender as ListBox)!.SelectedIndex switch
            {
                0 => new CroissanceWindow(t, j),
                1 => new ChangesWindow(t, j),
                2 => new AvancementWindow(t, j),
                _ => DetailsResume.Content
            };
        }

        private void Players_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FocusedPlayer = (sender as ListBox)!.SelectedItem as Joueur;
            (DetailsResume?.Content as ControlMonopoly)!.ClearContent();
            (DetailsResume?.Content as ControlMonopoly)!.Player = FocusedPlayer;
        }
    }
}