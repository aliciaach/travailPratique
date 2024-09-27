using Travaile_Pratique_1_aliciaach;

namespace Travail_Pratique_1_aliciaach
{
    internal class Program
    {


        static void Main(string[] args)
        {
            GestionRestaurant gestionRestaurant = new GestionRestaurant();

            gestionRestaurant.ouvrirRestaurant();
            Console.WriteLine("fINI");
        }
    }
}
