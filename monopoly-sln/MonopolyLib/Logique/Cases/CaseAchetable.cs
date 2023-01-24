using System.ComponentModel;
using System.Runtime.CompilerServices;
using MonopolyLib.Logique.Joueurs;

namespace MonopolyLib.Logique.Cases
{
    public abstract class CaseAchetable : Case, INotifyPropertyChanged
    {
        public float PrixAchat { get; }

        public float Prix { get; }

        public Joueur? Proprio { get; set; }

        public bool Achetée { get; set; }

        public float Profit { get; set; }

        public bool Hypotheque { get; set; }
        public override string ResumeCarte()
        {
            return base.ResumeCarte() +
                "Prix d'achat : " + PrixAchat + "\n";
        }
        protected float PrixFinalAttr;
        public float PrixFinal
        {
            get => Hypotheque ? 0 : GetPrixFinal();
            set
            {
                SetPrixFinal(value);
                OnPropertyChanged(nameof(PrixFinal));
            }
        }

        protected abstract float GetPrixFinal();

        protected abstract void SetPrixFinal(float value);

        public CaseAchetable(string nom, float prix, float prixAchat) : base(nom)
        {
            Prix = prix;
            PrixAchat = prixAchat;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
