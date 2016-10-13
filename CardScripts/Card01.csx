#r "..\\CardGame\\bin\\Debug\\CardGame.dll"
using CardGame.Model.Cards;

public class Card01 : Minion
{
    public Card01()
    {
        BaseAttack = 2;
        BaseHealth = 3;
        BaseCost = 2;
    }
	
	public void ToString() {
		Console.WriteLine($"FROM INSIDE SCRIPT - Minion: BaseAttack={BaseAttack}, BaseHealth={BaseHealth}, BaseCost={BaseCost}");
	}
}

var card = new Card01();
card.ToString();