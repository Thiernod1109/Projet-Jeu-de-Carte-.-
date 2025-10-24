namespace Travail1_bon;

using System;
using System.Collections.Generic;

public class GameBoard
{
    public DrawStack PilePioche { get; set; }
    public DepositStack PileDépôt { get; set; }
    public List<Player> Joueurs { get; set; }
    public int JoueurCourant { get; set; }
    public bool DirectionHoraire { get; set; }
    public int CartesParJoueur { get; set; }

    public GameBoard(List<Player> joueurs, int cartesParJoueur)
    {
        Joueurs = joueurs;
        CartesParJoueur = cartesParJoueur;
        PilePioche = new DrawStack(new List<Card>());
        PileDépôt = new DepositStack();
        JoueurCourant = 0;
        DirectionHoraire = true;
    }
    
    /*Cette methode permet de creer un paquet de 52 cartes, le melange,
     *distribue les cartes une à une à chaque joueur (X fois selon CartesParJoueur),
     * et les cartes restantes deviennent la pile de pioche (DrawStack)
    */
    public void DistribuerCartes()
    {
        CardPair paire = new CardPair();
        paire.Shuffle();

        for (int i = 0; i < CartesParJoueur; i++)
        {
            foreach (Player joueur in Joueurs)
            {
                Card carte = paire.Draw();
                joueur.AddCard(carte);
            }
        }

        PilePioche = new DrawStack(paire.Cards);
    }

    
    // Cette methode permet de choisir un joueur aléatoire pour commencer la partie
    public void ChoisirPremierJoueur()
    {
        Random random = new Random();
        JoueurCourant = random.Next(Joueurs.Count);
    }
    
    // Cette methode retourne le joueur qui doit jouer en ce moment

    public Player GetCurrentPlayer()
    {
        return Joueurs[JoueurCourant];
    }
    
    
    // Cette methode permet de passer au joueur suivant
    public void NextPlayer()
    {
        if (DirectionHoraire)
        {
            JoueurCourant = (JoueurCourant + 1) % Joueurs.Count;
        }
        else
        {
            JoueurCourant = (JoueurCourant - 1 + Joueurs.Count) % Joueurs.Count;
        }
    }
    
    
    // Cette methode permet pour inverser la direction 
    public void ReverseDirection()
    {
        DirectionHoraire = !DirectionHoraire;
    }
    
    // Methode pour sauter le tour du joueur suivant
    public void SkipPlayer()
    {
        NextPlayer();
    }
}