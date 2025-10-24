namespace Travail1_bon;

public class DepositStack
{
        public List<Card> Cards { get; set; }

        public DepositStack()
        {
            Cards = new List<Card>();
        }

        // Methode pour quand un joueur pose une carte
        public void Deposit(Card card)
        {
            Cards.Add(card);
        }

        //Methode pour voir quelle carte est sur la table et vérifier si on peut jouer dessus
        public Card GetTopCard()
        {
            if (Cards.Count > 0)
            {
                return Cards[Cards.Count - 1];
            }
            return default(Card);
        }

        // Methode Quand la pile de pioche est vide, on récupère toutes les cartes de
        // la pile de dépôt (sauf la dernière) pour refaire une pile de pioche.
        public List<Card> TakeAllButTop()
        {
            List<Card> result = new List<Card>();
            
            for (int i = 0; i < Cards.Count - 1; i++)
            {
                result.Add(Cards[i]);
            }

            // Garder juste la dernière carte
            Card derniere = Cards[Cards.Count - 1];
            Cards = new List<Card>();
            Cards.Add(derniere);
            
            return result;
        }
}