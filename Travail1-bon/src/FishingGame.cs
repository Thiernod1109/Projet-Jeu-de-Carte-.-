using System;
using System.Collections.Generic;
using Travail1_bon;

namespace JeuDePeche
{
    public class FishingGame
    {
        private GameBoard Board;
        private List<IGameObserver> Observers;
        private bool GameEnded;
        private Player Winner;
        private int cartesAaPiocher = 0;

        public FishingGame(List<Player> players, int cardsPerPlayer)
        {
            Board = new GameBoard(players, cardsPerPlayer);
            Observers = new List<IGameObserver>();
            GameEnded = false;
            Winner = null;
            cartesAaPiocher = 0;
        }

        public void AjouterObservateur(IGameObserver observer)
        {
            Observers.Add(observer);
        }

        public void DémarrerPartie()
        {
            Board.DistribuerCartes();
            Board.ChoisirPremierJoueur();

            Console.WriteLine("La partie commence avec " + Board.Joueurs.Count + 
                            " joueurs\n");
            Console.WriteLine("C'est a " + Board.GetCurrentPlayer().PersonneInfo.GetFullName() + 
                            " de commencer\n");

            // Poser la première carte
            Card premiereCarte = Board.PilePioche.PiocherCarte();
            Board.PileDépôt.Deposit(premiereCarte);

            while (!GameEnded)
            {
                TourJoueur();
            }

            NotifyObservers(new GameLoggerAction(obs => obs.OnGameEnded(Winner)));
            AfficherBilan();
        }

        private void TourJoueur()
        {
            Player joueur = Board.GetCurrentPlayer();
            Card topCard = Board.PileDépôt.GetTopCard();

            // Si le joueur doit piocher des cartes (accumulation du 2)
            if (cartesAaPiocher > 0)
            {
                // Vérifier si le joueur a un 2
                Card deux = TrouverDeux(joueur.Main);

                if (deux.Equals(default(Card)))
                {
                    // Pas de 2, il pioche les cartes
                    for (int i = 0; i < cartesAaPiocher; i++)
                    {
                        if (Board.PilePioche.IsEmpty())
                        {
                            NotifyObservers(new GameLoggerAction(obs => obs.OnDrawPileEmpty()));
                            Board.PilePioche.Remettre(Board.PileDépôt);
                        }

                        if (!Board.PilePioche.IsEmpty())
                        {
                            Card cartePiochee = Board.PilePioche.PiocherCarte();
                            joueur.AddCard(cartePiochee);
                        }
                    }
                    Console.WriteLine(">>> " + joueur.PersonneInfo.GetFullName() + 
                                    " pioche " + cartesAaPiocher + " cartes\n");
                    cartesAaPiocher = 0;
                    Board.NextPlayer();
                    System.Threading.Thread.Sleep(500);
                    return;
                }
                else
                {
                    // Il a un 2, il le pose pour contrer
                    joueur.PlayCard(deux, topCard);
                    Board.PileDépôt.Deposit(deux);
                    NotifyObservers(new GameLoggerAction(obs => obs.OnPlayerPlayed(joueur, deux)));
                    cartesAaPiocher += 2;  // Accumule 2 cartes supplémentaires
                    Console.WriteLine(">>> " + joueur.PersonneInfo.GetFullName() + 
                                    " contre l'attaque! Maintenant " + cartesAaPiocher + 
                                    " cartes a piocher\n");
                    Board.NextPlayer();
                    System.Threading.Thread.Sleep(500);
                    return;
                }
            }

            // Jeu normal
            Card carteChoisie = joueur.ChoisirCarte(topCard);

            if (carteChoisie.Equals(default(Card)))
            {
                // Pas de carte possible, il faut piocher
                if (Board.PilePioche.IsEmpty())
                {
                    NotifyObservers(new GameLoggerAction(obs => obs.OnDrawPileEmpty()));
                    Board.PilePioche.Remettre(Board.PileDépôt);
                }

                if (!Board.PilePioche.IsEmpty())
                {
                    Card cartePiochee = Board.PilePioche.PiocherCarte();
                    joueur.AddCard(cartePiochee);
                    NotifyObservers(new GameLoggerAction(obs => obs.OnPlayerDrew(joueur)));
                }
            }
            else
            {
                // Il peut jouer une carte
                joueur.PlayCard(carteChoisie, topCard);
                Board.PileDépôt.Deposit(carteChoisie);
                NotifyObservers(new GameLoggerAction(obs => obs.OnPlayerPlayed(joueur, carteChoisie)));

                ProcessSpecialCard(carteChoisie);

                // Vérifier si le joueur a terminé
                if (!joueur.HasCard())
                {
                    GameEnded = true;
                    Winner = joueur;
                    return;
                }

                // Vérifier si le joueur n'a plus qu'une carte
                if (joueur.Main.Count == 1)
                {
                    NotifyObservers(new GameLoggerAction(obs => obs.OnPlayerOneCard(joueur)));
                }
            }

            Board.NextPlayer();
            System.Threading.Thread.Sleep(500);
        }

        private Card TrouverDeux(List<Card> main)
        {
            foreach (Card c in main)
            {
                if (c.Value == CardValue.Two)
                {
                    return c;
                }
            }
            return default(Card);
        }

        private void ProcessSpecialCard(Card card)
        {
            if (card.Value == CardValue.Two)
            {
                cartesAaPiocher += 2;
                Console.WriteLine("[ATTAQUE] Le joueur suivant doit piocher 2 cartes!\n");
            }
            else if (card.Value == CardValue.Ace)
            {
                Player prochainJoueur = Board.GetCurrentPlayer();
                NotifyObservers(new GameLoggerAction(obs => obs.OnPlayerSkipped(prochainJoueur)));
                Board.SkipPlayer();
            }
            else if (card.Value == CardValue.Ten)
            {
                Board.ReverseDirection();
                Console.WriteLine("[DIRECTION INVERSEE]\n");
            }
        }

        private void AfficherBilan()
        {
            Console.WriteLine("\n========== BILAN FINAL ==========\n");
            foreach (Player joueur in Board.Joueurs)
            {
                int points = joueur.CalculerPoints();
                Console.WriteLine(joueur.PersonneInfo.GetFullName() + 
                                ": " + points + " points");
            }
        }

        private void NotifyObservers(GameLoggerAction action)
        {
            foreach (IGameObserver observer in Observers)
            {
                action.Execute(observer);
            }
        }
    }

    // Classe helper pour les délégués
    public class GameLoggerAction
    {
        public delegate void ObserverAction(IGameObserver observer);
        private ObserverAction action;

        public GameLoggerAction(ObserverAction act)
        {
            action = act;
        }

        public void Execute(IGameObserver observer)
        {
            action(observer);
        }
    }
}