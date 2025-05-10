// Plante.cs

public enum TypePlante
{
    Annuelle,
    Vivace,
    MauvaiseHerbe,
    Ornementale,
    Comestible,
    Imaginaire
}

public enum Saison
{
    Printemps,
    Ete,
    Automne,
    Hiver
}

public enum EtatPlante
{
    Graine,
    Pousse,
    Mature,
    Malade,
    Mort,
    Recoltable
}

public abstract class Plante
{
    public string Nom { get; protected set; }
    public TypePlante Type { get; protected set; }
    public List<Saison> SaisonsSemis { get; protected set; }
    public string TerrainPrefere { get; protected set; }
    public double Espacement { get; protected set; } // en cm
    public double PlaceNecessaire { get; protected set; } // en cm²
    public double VitesseCroissance { get; protected set; } // cm par semaine
    public double BesoinEau { get; protected set; } // de 0 à 1
    public double BesoinLuminosite { get; protected set; } // de 0 à 1
    public (double Min, double Max) TemperatureIdeale { get; protected set; } // en °C
    public List<string> MaladiesPossibles { get; protected set; }
    public int EsperanceVie { get; protected set; } // en semaines
    public int Production { get; protected set; } // nombre de fruits/légumes
    public EtatPlante Etat { get; protected set; }
    public double Sante { get; protected set; } // de 0 à 1
    public double Taille { get; protected set; } // en cm
    public int Age { get; protected set; } // en semaines

    protected Plante(string nom, TypePlante type)
    {
        Nom = nom;
        Type = type;
        SaisonsSemis = new List<Saison>();
        MaladiesPossibles = new List<string>();
        Etat = EtatPlante.Graine;
        Sante = 1.0;
        Taille = 0.1; // 1mm pour commencer
        Age = 0;
    }

    public virtual void Pousser(double qualiteTerrain, double eauDisponible, double luminosite, double temperature)
    {
        if (Etat == EtatPlante.Mort) return;

        Age++;
        
        // Calcul de la satisfaction des besoins
        double satisfactionEau = 1 - Math.Abs(BesoinEau - eauDisponible);
        double satisfactionLumiere = 1 - Math.Abs(BesoinLuminosite - luminosite);
        double satisfactionTemp = temperature >= TemperatureIdeale.Min && temperature <= TemperatureIdeale.Max ? 1 : 
                                (temperature < TemperatureIdeale.Min ? temperature / TemperatureIdeale.Min : 
                                TemperatureIdeale.Max / temperature);
        
        double satisfactionMoyenne = (satisfactionEau + satisfactionLumiere + satisfactionTemp + qualiteTerrain) / 4;

        if (satisfactionMoyenne < 0.5)
        {
            Sante -= 0.2;
            if (Sante <= 0)
            {
                Mourir();
                return;
            }
        }
        else
        {
            Sante = Math.Min(1.0, Sante + 0.05);
        }

        // Croissance en fonction de la satisfaction
        Taille += VitesseCroissance * satisfactionMoyenne;

        // Changement d'état
        if (Etat == EtatPlante.Graine && Taille > 1.0)
            Etat = EtatPlante.Pousse;
        else if (Etat == EtatPlante.Pousse && Taille > TailleMature() * 0.5)
            Etat = EtatPlante.Mature;
        else if (Etat == EtatPlante.Mature && Age > EsperanceVie * 0.7)
            Etat = EtatPlante.Recoltable;

        // Vérifier les maladies
        VerifierMaladie();
    }

    protected virtual double TailleMature()
    {
        return 30.0; // taille moyenne par défaut en cm
    }

    protected virtual void VerifierMaladie()
    {
        if (Etat == EtatPlante.Mort) return;

        Random rand = new Random();
        foreach (var maladie in MaladiesPossibles)
        {
            if (rand.NextDouble() < 0.05) // 5% de chance par maladie
            {
                Etat = EtatPlante.Malade;
                Sante -= 0.3;
                if (Sante <= 0) Mourir();
                return;
            }
        }
    }

    public virtual void Mourir()
    {
        Etat = EtatPlante.Mort;
        Sante = 0;
    }

    public virtual void Traiter()
    {
        if (Etat == EtatPlante.Malade)
        {
            Etat = EtatPlante.Mature;
            Sante = Math.Min(1.0, Sante + 0.3);
        }
    }

    public virtual int Recolter()
    {
        if (Etat != EtatPlante.Recoltable) return 0;
        
        int recolte = (int)(Production * Sante);
        Mourir();
        return recolte;
    }

    
}
