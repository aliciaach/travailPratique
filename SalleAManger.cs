using System;

public class SalleAManger
{
	private int nbrTablesDisponibles { get; set; }
    private static readonly object verrou = new object();

    Random random = new Random();   
    public void OpenSalleAManger(Restaurant restaurant)
    {
        nbrTablesDisponibles = 8;
        Console.WriteLine("SALLE A MANGEEEEEEEEEEEEEER OUVERTTTTTTTTEEEEEEEEEEE");
        while (restaurant.EstOuvert)
        {
            if (this.nbrTablesDisponibles != 0)
            {
                string idCommande = Restaurant.GenererIdCommande();
                int nbrPersonnes = random.Next(1, 5); //Entre 1 & 4 personnes 
                TypeCommande typeCommande = TypeCommande.DineIn; //Seulement livraison et emporter, donc juste valeur 2 & 3
                TypeRepas typeRepas = (TypeRepas)random.Next(1, 4);
                TimeSpan tempsPreparation = nbrPersonnes * TimeSpan.FromMilliseconds(500);
                TimeSpan delaiMaximum = Restaurant.genererDelaiMaxium(typeCommande);
                EtatCommande etatCommande = EtatCommande.NonCommencee;

                Commande commande = new Commande(idCommande, nbrPersonnes, typeCommande, typeRepas, tempsPreparation, delaiMaximum, etatCommande);

                restaurant.AjouterDesCommandes(commande);

                Console.WriteLine($"Nouvelle Commande Salle à Manger : {commande.IdCommande}");

                lock (verrou)
                {
                    this.nbrTablesDisponibles--;
                }
            }
            if (!restaurant.EstOuvert)
            {
                break;  // Stop if the restaurant is closed
            }
        }
#if DEBUG
        Console.WriteLine("STEP FINAL : FERMETURE SALLE A MANGER");
#endif
    }

    public void LibererUneTable()
    {
        TimeSpan tempsLibererTable = TimeSpan.FromMicroseconds(random.Next(15000, 20001));
        Thread.Sleep(tempsLibererTable);

        lock (verrou)
        {
            this.nbrTablesDisponibles++;
        }
    }
}

