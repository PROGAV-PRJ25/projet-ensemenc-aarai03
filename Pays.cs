
public enum TypeEvenement
{
    Intempérie,
    Intrus,
    Bonus
}

public class Evenement
{
    public string Description { get; }
    public TypeEvenement Type { get; }
    public int Impact { get; } // positif ou négatif

    public Evenement(string description, TypeEvenement type, int impact)
    {
        Description = description;
        Type = type;
        Impact = impact;
    }
}



public abstract class Pays
{
    public string Nom { get; }
    public List<string> PlantesDisponibles { get; }
    public List<Evenement> EvenementsPossibles { get; }

    protected Pays(string nom)
    {
        Nom = nom;
        PlantesDisponibles = new List<string>();
        EvenementsPossibles = new List<Evenement>();
    }

    public abstract Meteo GenererMeteo(DateTime date);
    public abstract Evenement GenererEvenement();
}

public class France : Pays
{
    public France() : base("France")
    {
        PlantesDisponibles.AddRange(new[] { "Carotte", "Tomate", "Rose", "Salade", "Pomme de terre" });
        
        EvenementsPossibles.AddRange(new[]
        {
            new Evenement("Orage violent avec grêle!", TypeEvenement.Intempérie, -20),
            new Evenement("Canicule exceptionnelle", TypeEvenement.Intempérie, -15),
            new Evenement("Gel tardif", TypeEvenement.Intempérie, -30),
            new Evenement("Lièvre dans le potager", TypeEvenement.Intrus, -10),
            new Evenement("Campagnol affamé", TypeEvenement.Intrus, -15),
            new Evenement("Voisin offre des plants", TypeEvenement.Bonus, +25),
            new Evenement("Pluie bienfaisante", TypeEvenement.Bonus, +10)
        });
    }

    public override Meteo GenererMeteo(DateTime date)
    {
        Random rand = new Random();
        
        // Variation saisonnière
        double baseTemp;
        double basePrecip;
        
        if (date.Month >= 3 && date.Month <= 5) // Printemps
        {
            baseTemp = 10 + rand.NextDouble() * 15; // 10-25°C
            basePrecip = 0.4 + rand.NextDouble() * 0.4; // 40-80%
        }
        else if (date.Month >= 6 && date.Month <= 8) // Été
        {
            baseTemp = 18 + rand.NextDouble() * 20; // 18-38°C
            basePrecip = 0.1 + rand.NextDouble() * 0.3; // 10-40%
        }
        else if (date.Month >= 9 && date.Month <= 11) // Automne
        {
            baseTemp = 5 + rand.NextDouble() * 15; // 5-20°C
            basePrecip = 0.3 + rand.NextDouble() * 0.5; // 30-80%
        }
        else // Hiver
        {
            baseTemp = -5 + rand.NextDouble() * 10; // -5 à +5°C
            basePrecip = 0.2 + rand.NextDouble() * 0.3; // 20-50%
        }
        
        // Ajouter une variation aléatoire
        double temp = baseTemp + (rand.NextDouble() * 6 - 3); // ±3°C
        double precip = Math.Max(0, Math.Min(1, basePrecip + (rand.NextDouble() * 0.4 - 0.2))); // ±20%
        double lumi = 0.7 + rand.NextDouble() * 0.3; // 70-100%
        
        string desc = temp switch
        {
            > 30 => "Canicule",
            > 25 => "Chaud et ensoleillé",
            > 20 => "Agréable",
            > 15 => "Doux",
            > 10 => "Fraîche",
            > 5 => "Froid",
            > 0 => "Très froid",
            _ => "Gelée"
        };
        
        desc += precip switch
        {
            > 0.8 => " avec pluies torrentielles",
            > 0.6 => " avec pluie",
            > 0.4 => " avec averses",
            > 0.2 => " légèrement pluvieux",
            _ => " sec"
        };
        
        return new Meteo(temp, precip, lumi);
    }

    public override Evenement GenererEvenement()
    {
        Random rand = new Random();
        return EvenementsPossibles[rand.Next(EvenementsPossibles.Count)];
    }
}


