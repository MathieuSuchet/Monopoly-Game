using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MonopolyLib.Logique.Cases;
using MonopolyLib.Logique.Joueurs;

namespace MonopolySimulator;

public class Tour
{
    public int NumTour { get; set; }

    public ObservableCollection<Joueur> Joueurs { get; set; }
    
    public ObservableCollection<string> Resume { get; set; }
    
    public List<Change> Changes { get; set; }
    
    public Dictionary<Joueur, List<CaseAchetable>> Properties { get; }

    public Dictionary<Joueur,List<string>> Avancements { get; set; }
    public Dictionary<Joueur,string> Croissances { get; set; }

    public Tour(int numTour)
    {
        NumTour = numTour;
        Joueurs = new ObservableCollection<Joueur>();
        Resume = new ObservableCollection<string>();
        Changes = new List<Change>();
        Avancements = new Dictionary<Joueur, List<string>>();
        Croissances = new Dictionary<Joueur, string>();
        Properties = new Dictionary<Joueur, List<CaseAchetable>>();
    }
}