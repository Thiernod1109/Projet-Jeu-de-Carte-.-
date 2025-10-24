namespace Travail1_bon;

public class Player
{
    public Person PersonneInfo { get; set; }
    public List<Card> Main { get; set; }

    public Player(Person personne)
    {
        PersonneInfo = personne;
        Main = new List<Card>();
    }
 
    /* Cette methode parcourt toutes les cartes du joueur pour chercher celle
     * qui peuvent etre jouées sur la carte du dessus
     * si elle en trouve, elle va mettre la premiere
     * si elle n'en trouve pas le joueur ne va rien poser il va directement piocher sur la pile 
     */
    public Card ChoisirCarte(Card topCard)
    {
        // Chercher toutes les cartes que l'on peut jouer
        List<Card> cartesPossibles = new List<Card>();
            
        foreach (Card c in Main)
        {
            //verifier si l'on peut jouer cette carte si oui on l'ajoute directement à la liste c'est a dire on sur le pile de depot
            if (c.CanPlayOn(topCard)) 
            {
                cartesPossibles.Add(c);
            }
        }

        // Si aucune carte possible, retourner une carte vide
        if (cartesPossibles.Count == 0)
        {
            return default(Card);
        }

        // Retourner la première carte possible
        return cartesPossibles[0];
    }

    
    // Methode pour ajouter des cartes si le joueur ne pas de carte a jouer 
    public void AddCard(Card card)
    {
        {
            Main.Add(card);
        }
    }
    
    // Methode pour enleve une carte en main le supprimer 
    public void RemoveCard(Card card)
    {
        Main.Remove(card);
    }
    
    // Methode pour verifier si la carte peut etre jouee si oui on l'enleve si non on le laisse 
    public bool PlayCard(Card card, Card topCard)
    {
        if (card.CanPlayOn(topCard))
        {
            Main.Remove(card);
            return true;
        }
        return false;
    }

    // Methode pour verifier si le joueur a toujours des cartes en main
    public bool HasCard()
    {
        return Main.Count > 0;
    }

    // Methode pour calculer le point de chque joueur apres la partie finie 
    public int CalculerPoints()
    {
        int total = 0;
        foreach (Card c in Main)
        {
            total += c.PointsCarte();
        }
        return total;
    }
}