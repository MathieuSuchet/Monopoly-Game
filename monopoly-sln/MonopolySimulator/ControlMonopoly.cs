using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using MonopolyLib.Logique.Joueurs;

namespace MonopolySimulator;

public class ControlMonopoly : UserControl, INotifyPropertyChanged
{
    private Tour? _tour;

    public Tour? Tour
    {
        get => _tour;
        set
        {
            _tour = value;
            OnPropertyChanged(nameof(Tour));
            CheckTour();
        }
    }

    protected virtual void CheckTour(){}

    private Joueur? _joueur;

    public Joueur? Player
    {
        get => _joueur;
        set
        {
            _joueur = value;
            OnPropertyChanged(nameof(Player));
            CheckJoueur();
        }
    }

    protected virtual void CheckJoueur()
    {
        
    }

    protected ControlMonopoly()
    { }

    protected ControlMonopoly(Tour tour)
    {
        Tour = tour;
    }

    protected ControlMonopoly(Tour? tour, Joueur? joueur)
    {
        
        Tour = tour;
        Player = joueur;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void ClearContent()
    {
        
    }
}