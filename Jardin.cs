public class Jardin
{
    public List<Terrain> Terrains { get; private set; }
    public Pays Localisation { get; private set; }
    public DateTime DateCourante { get; private set; }
    public Dictionary<string, int> InventaireSemis { get; private set; }
    public Dictionary<string, int> InventaireRecoltes { get; private set; }
    public int Argent { get; set; }
    public List<Animal> Animaux { get; private set; }
    private List<Animal> AnimauxDisponibles { get; set; }

    public Jardin(Pays pays)
    {
        Localisation = pays;
        Terrains = new List<Terrain>();
        InventaireSemis = new Dictionary<string, int>();
        InventaireRecoltes = new Dictionary<string, int>();
        DateCourante = new DateTime(2025, 3, 1); // Début printemps
        Argent = 100; // Argent de départ
        
        // Initialiser avec quelques terrains de base
        Terrains.Add(new TerrainTerre(1, 10)); // 10m²
        Terrains.Add(new TerrainSable(2, 5));  // 5m²
        
        // Initialiser avec quelques semis de base
        InventaireSemis.Add("Carotte", 10);
        InventaireSemis.Add("Tomate", 5);
        InventaireSemis.Add("Salade", 8);
        Animaux = new List<Animal>();
        InitialiserAnimaux();
    }

    private void InitialiserAnimaux()
    {
        AnimauxDisponibles = new List<Animal>
        {
            new Lapin(),
            new Souris(),
            new Corbeau(),
            new Limace()
        };
    }

    public void PasserSemaine()
    {
        DateCourante = DateCourante.AddDays(7);
        var meteo = Localisation.GenererMeteo(DateCourante);
        
        Console.WriteLine($"\n=== Semaine du {DateCourante:dd/MM/yyyy} ===");
        Console.WriteLine($"Météo: {meteo.Description}");
        
        foreach (var terrain in Terrains)
        {
            foreach (var plante in terrain.Plantes.ToList())
            {
                plante.Pousser(terrain.Qualite, meteo.Precipitation, meteo.LumiereDispo, meteo.Temperature);
                
                // Appliquer l'impact des événements météo
                if (meteo.EvenementActuel != MeteoEvent.None)
                {
                    double impact = Localisation.GetImpactMeteo(meteo.EvenementActuel);
                    plante.Sante += impact;
                    
                    if (plante.Sante <= 0)
                    {
                        plante.Mourir();
                        terrain.RemovePlant(plante);
                        Console.WriteLine($"{plante.Nom} a été détruite par {meteo.EvenementActuel}!");
                    }
                }
            }
        }
    }

    private void GererAnimaux()
    {
        // Faire fuir certains animaux existants
        foreach (var animal in Animaux.ToList())
        {
            if (new Random().NextDouble() < animal.ProbabiliteFuite)
            {
                Animaux.Remove(animal);
                Console.WriteLine($"{animal.Nom} a quitté le jardin.");
            }
        }

        // Ajouter de nouveaux animaux aléatoirement
        foreach (var animalType in AnimauxDisponibles)
        {
            if (new Random().NextDouble() < animalType.ProbabiliteApparition)
            {
                Animaux.Add(Activator.CreateInstance(animalType.GetType()) as Animal);
                Console.WriteLine($"Un {animalType.Nom} est apparu dans le jardin! {animalType.GetSymbole()}");
            }
        }

        // Les animaux endommagent les plantes
        foreach (var animal in Animaux)
        {
            foreach (var terrain in Terrains)
            {
                if (terrain.Plantes.Any() && new Random().NextDouble() > 0.5) // 50% de chance d'attaquer ce terrain
                {
                    var planteCible = terrain.Plantes[new Random().Next(terrain.Plantes.Count)];
                    animal.EndommagerPlante(planteCible);
                }
            }
        }
    }

    public void EffrayerAnimaux()
    {
        if (!Animaux.Any())
        {
            Console.WriteLine("Il n'y a aucun animal à effrayer.");
            return;
        }

        int animauxEffrayes = 0;
        foreach (var animal in Animaux.ToList())
        {
            if (new Random().NextDouble() < 0.7) // 70% de chance de fuite
            {
                Animaux.Remove(animal);
                animauxEffrayes++;
            }
        }

        Console.WriteLine($"Vous avez effrayé {animauxEffrayes} animal(s)!");
    }

    public void AfficherEtat()
    {
        Console.WriteLine("\n=== ÉTAT DU JARDIN ===");
        Console.WriteLine($"Date: {DateCourante:dd/MM/yyyy}");
        Console.WriteLine($"Localisation: {Localisation.Nom}");
        Console.WriteLine($"Argent: {Argent}€");
        
        foreach (var terrain in Terrains)
        {
            Console.WriteLine($"\nTerrain {terrain.Id} ({terrain.TypeTerrain}, {terrain.Surface}m²):");
            foreach (var plante in terrain.Plantes)
            {
                Console.WriteLine($"- {plante.Nom}: {plante.Etat}, Santé: {plante.Sante:P0}, Taille: {plante.Taille:F1}cm");
            }
        }
    }
}