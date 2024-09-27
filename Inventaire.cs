using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travaile_Pratique_1_aliciaach
{
    class Inventaire
    {
        private int nbrPizza = 15;
        private int nbrSpaghettis = 15;
        private int nbrSandwich = 15;
        private static readonly object verrou = new object();
        const string PLAT_PIZZA = "pizza";
        const string PLAT_SPAGHETTIS = "spaguettis";
        const string PLAT_SANDWICH = "sandwich";

        //REVENIR 
        public void GererInventaire(Restaurant restaurant)
        {
            Console.WriteLine($"Inventaire : Pizza = {this.nbrPizza}, Sandwich = {this.nbrSandwich}, Spaghettis = {this.nbrSpaghettis}");
            if (this.nbrPizza < 10)
            {
                lock(verrou)
                {
                    PasserUneCommandeStock(this.nbrSandwich);
                }
            }
            if (this.nbrSpaghettis < 10)
            {
                lock (verrou)
                {
                    PasserUneCommandeStock(this.nbrSandwich);
                }
            }
            if (this.nbrPizza < 10)
            {
                lock (verrou)
                {
                    PasserUneCommandeStock(this.nbrSandwich);
                }
            }
        }

        public void passerCommandeCuisine(Commande commande)
        {
            switch (commande.TypeRepas)
            {
                case TypeRepas.Spaghettis:

                    if (commande.NbrPersonnes <= nbrSpaghettis)
                    {
                        lock (verrou)
                        {
                            this.nbrSpaghettis -= commande.NbrPersonnes;
                        }
                        commande.EtatCommande = EtatCommande.Terminee;
                    }
                    else
                    {
                        Console.WriteLine($"ALERTE : Stock de {PLAT_SPAGHETTIS} critique. Réapprovisionnement en cours...");
                        lock (verrou)
                        {
                            commande.EtatCommande = EtatCommande.EnAttente;
                        }
                        Console.WriteLine($"Commande #{commande.IdCommande} en attente");
                        PasserUneCommandeStock(this.nbrSpaghettis);
                    }
                    break;

                case TypeRepas.Sandwich:

                    if (commande.NbrPersonnes <= nbrSandwich)
                    {
                        lock (verrou)
                        {
                            this.nbrSandwich -= commande.NbrPersonnes;
                        }
                        commande.EtatCommande = EtatCommande.Terminee;

                    }
                    else
                    {
                        Console.WriteLine($"ALERTE : Stock de {PLAT_SANDWICH} critique. Réapprovisionnement en cours...");
                        lock (verrou)
                        {
                            commande.EtatCommande = EtatCommande.EnAttente;
                        }
                        Console.WriteLine($"Commande #{commande.IdCommande} en attente");
                        PasserUneCommandeStock(this.NbrSandwich);
                    }
                    break;

                case TypeRepas.Pizza:

                    if (commande.NbrPersonnes <= nbrPizza)
                    {   
                        lock (verrou)
                        {
                            this.nbrPizza -= commande.NbrPersonnes;
                        }
                        commande.EtatCommande = EtatCommande.Terminee;
                    }
                    else
                    {
                        Console.WriteLine($"ALERTE : Stock de {PLAT_PIZZA} critique. Réapprovisionnement en cours...");
                        lock (verrou)
                        {
                            commande.EtatCommande = EtatCommande.EnAttente;
                        }
                        Console.WriteLine($"Commande #{commande.IdCommande} en attente");
                        PasserUneCommandeStock(this.nbrPizza);

                    }
                    break;
            }
            if (commande.EtatCommande.Equals(EtatCommande.Terminee))
            {
                Console.WriteLine($"Commande #{commande.IdCommande} terminée !");
            }
            Console.WriteLine($"Inventaire : Pizza = {this.nbrPizza}, Sandwich = {this.nbrSandwich}, Spaghettis = {this.nbrSpaghettis}");
        }
        private void PasserUneCommandeStock(int stockCommandee) 
        {
            Random random = new Random();
            int tempsRevitallement = random.Next(5000, 10001);

            Console.WriteLine($"Réapprovisionnement en Pizza commandé. Livraison prévue dans {tempsRevitallement} ms.");

            Thread.Sleep(tempsRevitallement);

            lock (verrou)
            {
                stockCommandee += 15;
            }
        }

        public int NbrPizza
        {
            get { return nbrPizza; }
            set { nbrPizza = value; }
        }

        public int NbrSpaghettis
        {
            get { return nbrSpaghettis; }
            set { nbrSpaghettis = value; }
        }

        public int NbrSandwich
        {
            get { return nbrSandwich; }
            set { nbrSandwich = value; }
        }
    }
}
