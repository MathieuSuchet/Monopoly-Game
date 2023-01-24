namespace MonopolyLib.Logique.Joueurs.RetireursArgent
{
    public abstract class RetireurArgent : PlayerComponentsMother
    {
        public RetireurArgent(Joueur? j) : base(j) { }

        public virtual bool RetirerArgent(float value)
        {
            Player.OnMoneyChanged(value, true);
            return true;
        }
    }
}
