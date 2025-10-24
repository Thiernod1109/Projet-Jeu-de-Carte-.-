namespace Travail1_bon;
using System;
public interface IGameObserver
{
   
        void OnPlayerPlayed(Player player, Card card);
        void OnPlayerDrew(Player player);
        void OnPlayerSkipped(Player player);
        void OnGameEnded(Player winner);
        void OnDrawPileEmpty();
        void OnPlayerOneCard(Player player);
   

    
}