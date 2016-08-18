namespace CardGameConsoleTestApp.Model.Cards
{
    public class Spell : Card
    {
        public Spell() : this("", 0)
        {
        }

        public Spell(string name, int cost) : base(name, cost)
        {
        }
    }
}
