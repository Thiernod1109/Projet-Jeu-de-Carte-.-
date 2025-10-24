using JeuDePeche;
using Travail1_bon;

class Program
    {
        static void Main()
        {
            Console.WriteLine("=== JEU DE PECHE ===\n");
            
            // Demander le nombre de joueurs
            int nbJoueurs = 0;
            bool valide = false;
            
            while (!valide)
            {
                Console.Write("Combien de joueurs ? (2-4) :  \n");
                string input = Console.ReadLine();
                
                if (int.TryParse(input, out nbJoueurs) && nbJoueurs >= 2 && nbJoueurs <= 4)
                {
                    valide = true;
                }
                else
                {
                    Console.WriteLine("Veuillez entrer un nombre entre 2 et 4");
                }
            }
            
            // Demander les noms des joueurs
            List<Player> joueurs = new List<Player>();
            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.Write($"\nNom du joueur {i + 1} : ");
                string nom = Console.ReadLine();
                
                Console.Write("Prénom : ");
                string prenom = Console.ReadLine();
                
                Person personne = new Person(nom, prenom, i + 1);
                Player joueur = new Player(personne);
                joueurs.Add(joueur);
            }
            
            // Demander le nombre de cartes à distribuer
            int nbCartes = 0;
            valide = false;
            
            while (!valide)
            {
                Console.Write("\nCombien de cartes par joueur ? (5-8) : ");
                string input = Console.ReadLine();
                
                if (int.TryParse(input, out nbCartes) && nbCartes >= 5 && nbCartes <= 8)
                {
                    valide = true;
                }
                else
                {
                    Console.WriteLine("Veuillez entrer un nombre entre 5 et 8");
                }
            }
            
            // Lancer la partie
            Console.WriteLine("\n========== DEBUT DE LA PARTIE ==========\n");
            FishingGame partie = new FishingGame(joueurs, nbCartes);
            partie.AjouterObservateur(new GameLogger());
            partie.DémarrerPartie();
            
            Console.WriteLine("\n\nAppuyez sur Entrer pour quitter");
            Console.ReadLine();
        }
    }
