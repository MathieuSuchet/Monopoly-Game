using System.Windows.Controls;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolySimulator;

public partial class PropertiesStateWindow : ControlMonopoly
{
    public PropertiesStateWindow()
    {
        InitializeComponent();
    }
    
    public PropertiesStateWindow(Tour? tour, Joueur? j)
    {
        InitializeComponent();
        Tour = tour;
        Player = j;
    }
    
    protected override void CheckTour()
    {
        TourStatus.Text = Tour is null ? "Veuillez sélectionner un tour" : "Tour " + Tour.NumTour;
        if (Player is null || Tour is null || !Tour.Croissances.ContainsKey(Player)) return;

        foreach (var caseAchetable in Tour.Properties[Player])
        {
            if (caseAchetable is CaseMaison cm)
            {
                SetText($"{cm.Nom} :\n" +
                        $"\tNb maisons : {cm.NbMaisons}\n" +
                        $"\tPrix final : {cm.PrixFinal}\n" +
                        $"\tRecettes   : {cm.Profit}");
            }
            
        }
        
        
    }

    protected override void CheckJoueur()
    {
        PlayerStatus.Text = Player is null ? "Veuillez sélectionner un joueur" : "Propriétés pour " + Player.Nom;
        if (Player is null || Tour is null || !Tour.Croissances.ContainsKey(Player)) return;
        //SetText(Tour.Properties[Player]);
        
    }

    private void SetText(string properties)
    {
        //Properties.Text += croissance + "\n";
    }
}