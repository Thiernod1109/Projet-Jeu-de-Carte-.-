namespace Travail1_bon;
using System;

/*GameLogger est une classe qui implémente l'interface IGameObserver
 * Elle reçoit les événements du jeu et les affiche dans la console
 */

public class GameLogger : IGameObserver
{
    public void OnPlayerPlayed(Player player, Card card)
    {
        Console.WriteLine(">>> " + player.PersonneInfo.GetFullName() + 
                          " a joue: " + card.NomCarte());
    }

    public void OnPlayerDrew(Player player)
    {
        Console.WriteLine("<<< " + player.PersonneInfo.GetFullName() + 
                          " pioche une carte");
    }

    public void OnPlayerSkipped(Player player)
    {
        Console.WriteLine("!!! " + player.PersonneInfo.GetFullName() + 
                          " saute son tour");
    }

    public void OnGameEnded(Player winner)
    {
        Console.WriteLine("\n*** " + winner.PersonneInfo.GetFullName() + 
                          " a gagne la partie! ***\n");
    }

    public void OnDrawPileEmpty()
    {
        Console.WriteLine("[*] Pile de pioche vide! Remise en place en cours...\n");
    }

    public void OnPlayerOneCard(Player player)
    {
        Console.WriteLine("!!! " + player.PersonneInfo.GetFullName() + 
                          " n'a plus qu'une carte!!!\n");
    }
}