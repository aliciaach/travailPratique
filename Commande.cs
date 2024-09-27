using System;

public class Commande
{
	private string idCommande {  get; set; }
	private TypeCommande typeCommande { get; set; }
	private TypeRepas typeRepas { get; set; }
	private int nbrPersonnes {  get; set; }
	private TimeSpan tempsPreparation { get; set; }
	private TimeSpan delaiMaximum { get; set; }
	private EtatCommande etatCommande { get; set; }

    public String IdCommande
    {
        get { return idCommande; }
        set { idCommande = value; }
    }
	public TimeSpan DelaiMaximum
	{
		get { return delaiMaximum; }
		set { delaiMaximum = value; }
	}

	public EtatCommande EtatCommande
	{
		get { return etatCommande; }
		set { etatCommande = value; }
	}

	public TypeRepas TypeRepas
	{
		get { return typeRepas; }
		set { typeRepas = value; }
	}
    public TypeCommande TypeCommande
    {
        get { return typeCommande; }
        set { typeCommande = value; }
    }
    public int NbrPersonnes
	{
		get { return nbrPersonnes; }
		set { nbrPersonnes = value; }
	}


	public Commande(string idCommande, int nbrPersonnes, TypeCommande typeCommande, TypeRepas typeRepas, TimeSpan tempsPreparation, TimeSpan delaiMaximum, EtatCommande etatCommande)
	{
		this.idCommande = idCommande;
		this.nbrPersonnes = nbrPersonnes;
		this.typeCommande = typeCommande;
		this.typeRepas = typeRepas;
		this.tempsPreparation = tempsPreparation;
		this.delaiMaximum = delaiMaximum;
		this.etatCommande = etatCommande;
	}
    public override string ToString()
    {
        return $"[{this.idCommande.ToString()}, {this.nbrPersonnes} personnes, {this.typeCommande}, {this.typeRepas}, Temps préparation: {this.tempsPreparation}, " +
			$"Delai Restant: {this.delaiMaximum}, Commende {this.etatCommande}]";
    }
}
