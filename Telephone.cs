using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travaile_Pratique_1_aliciaach
{
    internal class Telephone
    {
        private Random random = new Random();
        
        public void PrendreAppel(Restaurant restaurant)
        {
            while (restaurant.EstOuvert)
            {

                if (!restaurant.EstOuvert)
                {
                    break; // Exit the loop immediately if the restaurant is closed
                }

                string idCommande = Restaurant.GenererIdCommande();
                int nbrPersonnes = random.Next(1, 11); //Entre 1 & 10 personnes 
                TypeCommande typeCommande = (TypeCommande)random.Next(2, 4); //Seulement livraison et emporter, donc juste valeur 2 & 3
                TypeRepas typeRepas = (TypeRepas)random.Next(1, 4);
                TimeSpan tempsPreparation = nbrPersonnes * TimeSpan.FromMilliseconds(500);
                TimeSpan delaiMaximum = Restaurant.genererDelaiMaxium(typeCommande);
                EtatCommande etatCommande = EtatCommande.NonCommencee;

                Commande commande = new Commande(idCommande, nbrPersonnes, typeCommande, typeRepas, tempsPreparation, delaiMaximum, etatCommande);
                restaurant.AjouterDesCommandes(commande);


                int randomIntervalle = random.Next(1500, 2501);
                TimeSpan intervalle = TimeSpan.FromMilliseconds(randomIntervalle);
                Console.WriteLine($"Nouvelle commande par téléphone ({commande.TypeCommande}) : #{commande.IdCommande} - {commande.NbrPersonnes} plat(s) ({commande.TypeRepas}).");

                Thread.Sleep(randomIntervalle);

                if (!restaurant.EstOuvert)
                {
                    break; // Double check again if the restaurant is still open before doing the loop again
                }

            }
#if DEBUG
            Console.WriteLine("-------------------------------------------- FERMETURE COMMANDE TÉLÉPHONE ---------------------------------------");
#endif
        }
    }

    
}
