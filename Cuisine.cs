using System;
using Travaile_Pratique_1_aliciaach;

public class Cuisine
{	
	private Commande commandeEnTraitement { get; set; }	
	private Inventaire inventaire { get; set; }

    public Cuisine()
	{
		inventaire = new Inventaire();
	}

	public void OuvertureCuisine(Restaurant restaurant)
	{
        Console.WriteLine("OUVERTURE CUISINE");

        while (restaurant.EstOuvert || (restaurant.UrgentOrder != null))
		{
			Thread.Sleep(1000);
			commandeEnTraitement = restaurant.UrgentOrder;

			if (commandeEnTraitement != null)
			{
				TraiterUneCommande(commandeEnTraitement);
				lock (restaurant.ListDesCommandes)
				{
					restaurant.ListDesCommandes.Remove(commandeEnTraitement);
				}
			}
		}
	}
	public void TraiterUneCommande(Commande commandeEnTraitement)
	{
			Console.WriteLine($"Commande #{commandeEnTraitement.IdCommande} envoyée à la cuisine. Délai max : {commandeEnTraitement.DelaiMaximum}.");
			commandeEnTraitement.EtatCommande = EtatCommande.EnCours;
            inventaire.passerCommandeCuisine(commandeEnTraitement);
	}
}
