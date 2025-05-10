public class Carotte : Plante
{
    public Carotte() : base("Carotte", TypePlante.Comestible)
    {
        SaisonsSemis = new List<Saison> { Saison.Printemps, Saison.Ete };
        TerrainPrefere = "Terre";
        Espacement = 5;
        PlaceNecessaire = 25;
        VitesseCroissance = 2.5;
        BesoinEau = 0.7;
        BesoinLuminosite = 0.8;
        TemperatureIdeale = (15, 25);
        MaladiesPossibles = new List<string> { "Mildiou", "Rouille" };
        EsperanceVie = 12;
        Production = 10;
    }

    protected override double TailleMature()
    {
        return 20.0;
    }
}

public class Tomate : Plante
{
    public Tomate() : base("Tomate", TypePlante.Comestible)
    {
        SaisonsSemis = new List<Saison> { Saison.Printemps };
        TerrainPrefere = "Terre";
        Espacement = 40;
        PlaceNecessaire = 1600;
        VitesseCroissance = 3.0;
        BesoinEau = 0.8;
        BesoinLuminosite = 0.9;
        TemperatureIdeale = (18, 30);
        MaladiesPossibles = new List<string> { "Mildiou", "Oïdium" };
        EsperanceVie = 20;
        Production = 30;
    }

    protected override double TailleMature()
    {
        return 100.0; // plantes de tomates plus grandes
    }
}

public class Rose : Plante
{
    public Rose() : base("Rose", TypePlante.Ornementale)
    {
        SaisonsSemis = new List<Saison> { Saison.Printemps, Saison.Automne };
        TerrainPrefere = "Argile";
        Espacement = 50;
        PlaceNecessaire = 2500;
        VitesseCroissance = 1.5;
        BesoinEau = 0.6;
        BesoinLuminosite = 0.7;
        TemperatureIdeale = (10, 28);
        MaladiesPossibles = new List<string> { "Oïdium", "Taches noires" };
        EsperanceVie = 260; // 5 ans
        Production = 15;
    }
}

public class Salade : Plante
{
    public Salade() : base("Salade", TypePlante.Comestible)
    {
        SaisonsSemis = new List<Saison> { Saison.Printemps, Saison.Automne };
        TerrainPrefere = "Terre";
        Espacement = 30;
        PlaceNecessaire = 900;
        VitesseCroissance = 2.0;
        BesoinEau = 0.8;
        BesoinLuminosite = 0.7;
        TemperatureIdeale = (10, 20);
        MaladiesPossibles = new List<string> { "Mildiou", "Pucerons" };
        EsperanceVie = 8;
        Production = 1; // Une salade par plant
    }
}

public class PommeDeTerre : Plante
{
    public PommeDeTerre() : base("PommeDeTerre", TypePlante.Comestible)
    {
        SaisonsSemis = new List<Saison> { Saison.Printemps };
        TerrainPrefere = "Terre";
        Espacement = 40;
        PlaceNecessaire = 1600;
        VitesseCroissance = 1.5;
        BesoinEau = 0.6;
        BesoinLuminosite = 0.8;
        TemperatureIdeale = (15, 25);
        MaladiesPossibles = new List<string> { "Doryphore", "Mildiou" };
        EsperanceVie = 20;
        Production = 10; // Nombre de pommes de terre par plant
    }

    protected override double TailleMature()
    {
        return 60.0;
    }
}

public class Tournesol : Plante
{
    public Tournesol() : base("Tournesol", TypePlante.Ornementale)
    {
        SaisonsSemis = new List<Saison> { Saison.Printemps };
        TerrainPrefere = "Terre";
        Espacement = 50;
        PlaceNecessaire = 2500;
        VitesseCroissance = 3.0;
        BesoinEau = 0.5;
        BesoinLuminosite = 1.0; // Beaucoup de lumière
        TemperatureIdeale = (20, 30);
        MaladiesPossibles = new List<string> { "Oïdium", "Pucerons" };
        EsperanceVie = 16;
        Production = 1; // Une fleur
    }

    protected override double TailleMature()
    {
        return 200.0; // Très grand
    }

}

public class Cactus : Plante
{
    public Cactus() : base("Cactus", TypePlante.Ornementale)
    {
        SaisonsSemis = new List<Saison> { Saison.Printemps, Saison.Ete };
        TerrainPrefere = "Sable";
        Espacement = 20;
        PlaceNecessaire = 400;
        VitesseCroissance = 0.5; // Croissance lente
        BesoinEau = 0.2; // Besoin en eau faible
        BesoinLuminosite = 0.9;
        TemperatureIdeale = (25, 40);
        MaladiesPossibles = new List<string> { "Pourriture" };
        EsperanceVie = 520; // 10 ans
        Production = 0; // Non récoltable
    }

    

    public override int Recolter()
    {
        Console.WriteLine("Les cactus ne sont pas récoltables!");
        return 0;
    }
}

public class Bambou : Plante
{
    public Bambou() : base("Bambou", TypePlante.Comestible) // Ou un nouveau type Commerciale
    {
        SaisonsSemis = new List<Saison> { Saison.Printemps };
        TerrainPrefere = "Terre";
        Espacement = 100;
        PlaceNecessaire = 10000;
        VitesseCroissance = 5.0; // Croissance très rapide
        BesoinEau = 0.8;
        BesoinLuminosite = 0.7;
        TemperatureIdeale = (15, 30);
        MaladiesPossibles = new List<string>();
        EsperanceVie = 260; // 5 ans
        Production = 5; // Tiges de bambou
    }

    protected override double TailleMature()
    {
        return 500.0; // Très grand
    }

    

    public override void Pousser(double qualiteTerrain, double eauDisponible, double luminosite, double temperature)
    {
        base.Pousser(qualiteTerrain, eauDisponible, luminosite, temperature);
        
        // Le bambou peut envahir d'autres terrains
        if (Etat == EtatPlante.Mature && new Random().NextDouble() < 0.05)
        {
            Console.WriteLine("Le bambou s'étend à un terrain voisin!");
            // Logique pour s'étendre...
        }
    }
}

public class PlanteMagique : Plante
{
    public PlanteMagique() : base("PlanteMagique", TypePlante.Imaginaire)
    {
        SaisonsSemis = new List<Saison> { Saison.Printemps, Saison.Automne };
        TerrainPrefere = "Argile";
        Espacement = 40;
        PlaceNecessaire = 1600;
        VitesseCroissance = 1.8;
        BesoinEau = 0.7;
        BesoinLuminosite = 0.5; // Peu de lumière
        TemperatureIdeale = (10, 25);
        MaladiesPossibles = new List<string> { "Malédiction", "Sort raté" };
        EsperanceVie = 52; // 1 an
        Production = 3; // Potions magiques
    }

    

    
}

public class Mandragore : Plante
{
    public Mandragore() : base("Mandragore", TypePlante.Imaginaire)
    {
        SaisonsSemis = new List<Saison> { Saison.Automne };
        TerrainPrefere = "Argile";
        Espacement = 60;
        PlaceNecessaire = 3600;
        VitesseCroissance = 0.3; // Croissance très lente
        BesoinEau = 0.6;
        BesoinLuminosite = 0.3; // Ombre
        TemperatureIdeale = (5, 15);
        MaladiesPossibles = new List<string> { "Malédiction" };
        EsperanceVie = 104; // 2 ans
        Production = 1; // Très précieuse
    }

    protected override double TailleMature()
    {
        return 30.0;
    }

    

    public override int Recolter()
    {
        if (Etat != EtatPlante.Recoltable) return 0;
        
        // La mandragore crie quand on la récolte
        Console.WriteLine("AAAAAAAAAAH! (La mandragore pousse un cri perçant)");
        return base.Recolter();
    }
}
    