namespace Travail1_bon;

/*Au début du jeu, après avoir distribué les cartes aux joueurs, les cartes restantes deviennent la pile de pioche*/
public class DrawStack
{
    public List<Card> Cards { get; set; }

    public DrawStack(List<Card> cards)
    {
        Cards = new List<Card>();
        foreach (Card c in cards)
        {
            Cards.Add(c);
        }
    }

     /* Methode pour quand un joueur ne peut pas jouer, il pioche une carte, ainsi la carte piochée, est enlevée
     * de la pile de carte
     */
    public Card PiocherCarte()
    {
        if (Cards.Count > 0)
        {
            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
        return default(Card);
    }

    // Methode juste pour vérifier s'il faut refaire la pile de pioche avec les cartes de la pile de dépôt.
    public bool IsEmpty()
    {
        return Cards.Count == 0;
    }
    
    //Methode pour refaire la pile quand elle est vide en utilisant les cartes de la pile de dépôt sauf la derniere posée.
    public void Remettre(DepositStack depositStack)
    {
        List<Card> allCards = depositStack.TakeAllButTop();
        foreach (Card c in allCards)
        {
            Cards.Add(c);
        }
    }
        
}