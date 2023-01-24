using System.Collections.Generic;
using MonopolyLib.Logique.Cases;

namespace MonopolyLib.Logique.Joueurs.Estimators
{
    public class EstimatorResult
    {
        public List<CaseAchetable> Cases { get; set; }
        public float Argent { get; set; }
    }
}
