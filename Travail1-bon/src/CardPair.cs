namespace Travail1_bon;

public class CardPair
{
    /*
     * Un jeu complet appelé Paire de Cartes (Toutes les combinaisons simples possibles) contient 52
     */
    public List<Card> Cards { get; set; }

    public CardPair()
    {
        Cards = new List<Card>();
        Initialize();
    }

    
    public void Initialize()
    {
        // Créer les 4 couleurs (Trefle, Carreau, Cœur, Pique)
        CardColor[] colors = new CardColor[4];
        colors[0] = new CardColor("Trefle");
        colors[1] = new CardColor("Carreau");
        colors[2] = new CardColor("Coeur");
        colors[3] = new CardColor("Pique");
        
        // Créer toutes les combinaisons
        for (var c = 0; c < 4; c++) // Pour chaque couleur
        {
            for (var v = 1; v <= 13; v++) // Pour Chaque valeur (1 à 13)
            {
                var carte = new Card((CardValue)v, colors[c]);
                Cards.Add(carte);
            }
        }
    }

    //Methode pour melanger les 52 Cartes de manieres aléatoire
    public void Shuffle()
    {
        var random = new Random();
            
        for (var i = Cards.Count - 1; i > 0; i--)  // Nombre aléatoire entre 0 et i
        {
            var randomIndex = random.Next(i + 1);
                
            (Cards[i], Cards[randomIndex]) = (Cards[randomIndex], Cards[i]);
        }
    }

    // Methode pour le paquet de carte, elle prend la première carte du paquet, l'enlève du paquet
    public Card Draw()
    {
        if (Cards.Count > 0) // si le paquet n'est pas vide 
        {
            var card = Cards[0]; 
            Cards.RemoveAt(0);
            return card;
        }
        return default(Card); 
    }

    // Methode pour verifier s'il y a encore des cartes à piocher 
    public bool IsEmpty()
    {
        return Cards.Count == 0;
    }
}