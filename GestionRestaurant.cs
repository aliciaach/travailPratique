using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travaile_Pratique_1_aliciaach
{
    internal class GestionRestaurant
    {
        private SalleAManger salleAManger = new SalleAManger();
        private Telephone telephone = new Telephone();
        private Cuisine cuisine = new Cuisine();
        Restaurant restaurant = new Restaurant();
        Inventaire inventaire = new Inventaire();


        public void ouvrirRestaurant()
        {
            restaurant.EstOuvert = true;

            Thread tempsOuverture = new Thread(new ThreadStart(this.restaurant.LancerMinuteurOuvertureRestaurant));
            tempsOuverture.Start();

            Thread prendreAppel = new Thread(() => this.telephone.PrendreAppel(this.restaurant));
            prendreAppel.Start();

            Thread ouvrirSalleManger = new Thread(() => this.salleAManger.OpenSalleAManger(this.restaurant));
            ouvrirSalleManger.Start();

            Thread ouverturCuisine = new Thread(() => this.cuisine.OuvertureCuisine(this.restaurant));
            ouverturCuisine.Start();

            Thread gererInventaire = new Thread(() => this.inventaire.GererInventaire(this.restaurant));
            gererInventaire.Start();

            ouvrirSalleManger.Join();
            tempsOuverture.Join();
            prendreAppel.Join();
            ouvrirSalleManger.Join();
        }
    }
}
