namespace MonopolyLib.Logique.Joueurs.RetireursArgent
{
    public class RetireurArgentBasique : RetireurArgent
    {
        public RetireurArgentBasique(Joueur? j) : base(j) { }

        public override bool RetirerArgent(float value)
        {
            if (Player.Argent < value)
            {
                return false;
            }
            base.RetirerArgent(value);
            Player.Argent -= value;
            return true;
        }
    }
}
