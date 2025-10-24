namespace Travail1_bon;

public struct Card
{
    // Une Carte est constituee d'une valeur et de sa couleur 
    public CardValue Value {get; set;}
    public CardColor Color {get; set;}
    
    
    public Card (CardValue value, CardColor color)
    {
        Value = value;
        Color = color;
    }
    
    // Une carte à une valeur et une couleur qui vont déterminer son nom
    public string NomCarte()
    {
        string valueName = "";
        switch (Value)
        {
            case CardValue.Ace:
                valueName = "As";
                break;
            case CardValue.Jack:
                valueName = "Valet";
                break;
            case CardValue.Queen:
                valueName = "Dame";
                break;
            case CardValue.King:
                valueName = "Roi";
                break;
            default:
                return valueName = Value.ToString();
                break;
        }
        
        return valueName + " de " + Color.Name;
        
    }
    
    /*Sachant que l’as compte pour 11 points, valet, dame et roi comptent pour 2 points et que les
     *autres cartes comptent pour leur valeurs faciales (7->7 points, 3->3points…etc.)
     */
    
    public int PointsCarte()
    {
        switch (Value)
        {
            case CardValue.Ace:
                return 11;
            case CardValue.Jack:
                return 2;
            case CardValue.Queen:
                return 2;
            case CardValue.King:
                return 2;
            default:
                return (int)Value;
        }
    }
    
    /* Methode pour verifier si une carte peut etre jouée sur la carte
     * du dessus de la pile de depot
     */

    public bool CanPlayOn(Card topCard)
    {
        /* Un Valet (jack) peut être déposé sur n’importe quelle valeur ou
        *couleur qui est sur la pile de dépôt sauf sur un 2
        */
        
        if (Value == CardValue.Jack)
        {
            return topCard.Value != CardValue.Two;
        }
        
        /* Une autre carte peut etre jouer sur n'importe quel autre carte
        a moins que ça soit meme valeur ou meme couleur */
        return Value == topCard.Value || Color.Equals(topCard.Color);
    }
}
