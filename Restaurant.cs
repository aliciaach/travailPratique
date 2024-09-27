using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Travaile_Pratique_1_aliciaach;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Restaurant
{
    private TimeSpan tempsOuverture { get; set; }
    protected List<Commande> listDesCommandes; // Liste des commandes
    protected Stack<Commande> commandesTerminees; // Commandes terminées
    private Commande urgentOrder;
    private static int compteurId = 0;
    private static readonly object verrou = new object();
    private bool estOuvert;

    public Commande UrgentOrder
    {  get { return urgentOrder; } 
       set { urgentOrder = value; }
    }

    public bool EstOuvert
    {
        get { return estOuvert; } 
        set { estOuvert = value; }
    }
    public List<Commande> ListDesCommandes
    {
        get { return listDesCommandes; }
        set { listDesCommandes = value; }
    }

    public Stack<Commande> CommandesTerminees
    {
        get { return commandesTerminees; }
        set { commandesTerminees = value; }
    }
    public Restaurant()
	{
        this.listDesCommandes = new List<Commande>(); // Liste des commandes
        this.commandesTerminees = new Stack<Commande>(); // Commandes terminées
        this.estOuvert = false;
        this.tempsOuverture = TimeSpan.FromMilliseconds(18000);
	}

    public void LancerMinuteurOuvertureRestaurant()
    {
        this.estOuvert = true;
        

        TimeSpan intervalle = TimeSpan.FromMilliseconds(3000);
#if DEBUG
        Console.WriteLine("************************** OUVERTURE DU RESTAURANT **************************");
#endif

        while (this.tempsOuverture > TimeSpan.Zero)
        {
            Thread.Sleep(intervalle);
            this.tempsOuverture -= intervalle;
            Console.WriteLine($"Temps restant : {this.tempsOuverture}");

            lock (this.listDesCommandes)
            {
                foreach (var commande in this.listDesCommandes)
                {
                    commande.DelaiMaximum -= intervalle;

                    if (commande.DelaiMaximum.TotalMilliseconds == 0)
                    {
                        commande.EtatCommande = EtatCommande.Expiree;
                        Console.WriteLine($"Commande {commande.IdCommande} expirée.");
                    }
                }
            }

         lock (this.listDesCommandes)
            {
                this.listDesCommandes.RemoveAll(commande => commande.EtatCommande == EtatCommande.Expiree);
            }
            
        }
        this.estOuvert = false;
        Console.WriteLine("************************************ CLOSING RESTAURANT *************************************");
    }

    internal static TimeSpan genererDelaiMaxium(TypeCommande typeCommande)
    {
        TimeSpan delaiMaximum = TimeSpan.Zero;
        switch (typeCommande)
        {
            case TypeCommande.DineIn:
                delaiMaximum = TimeSpan.FromMilliseconds(30000);
                break;
            case TypeCommande.Livraison:
                delaiMaximum = TimeSpan.FromMilliseconds(20000);
                break;
            case TypeCommande.Emporter:
                delaiMaximum = TimeSpan.FromMilliseconds(40000);
                break;
            default:
                //ERREUR
                break;
        }
        return delaiMaximum;
    }

    public static string GenererIdCommande()
    {
        
        lock (verrou)
        {
            compteurId++;
        }
        string idCommande = DateTime.Now.ToString("mmssfff") + "-" + compteurId;
        Console.WriteLine(idCommande);
        return idCommande;

    }

    public void AjouterDesCommandes(Commande commande)
    {
        lock (verrou)
        {
            this.listDesCommandes.Add(commande);
            OrderList();
            this.urgentOrder = this.listDesCommandes.ElementAt(0);
        }
    }

    private void OrderList()
    {
        this.listDesCommandes.Sort((x, y) => x.DelaiMaximum.CompareTo(y.DelaiMaximum));
    }
}
