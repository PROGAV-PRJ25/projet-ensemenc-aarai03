public enum TypeSol {Sable, Argile, Terre, Mixte}

public abstract class Terrain{
    public int Id { get; }
    public double Surface { get; } // en m²
    public TypeSol TypeTerrain {get; protected set;}
    public double Qualite { get;  set; } // de 0 à 1
    public double RetentionEau { get;  set; } // de 0 à 1
    public double Drainage { get;  set; } // de 0 à 1
    public List<Plante> Plantes { get; protected set; }

    protected Terrain()
    {
        Plantes = new List<Plante>(); // Initialisation de la liste
        Qualite = 0.7; // valeur par défaut
    }

    protected Terrain(int id, double surface, TypeSol type) : this() // Appel au constructeur par défaut
    {
        Id = id;
        Surface = surface;
        TypeTerrain = type;
    }

    public virtual bool CanPlant(Plante plante)
    {
        if (plante == null ) return false;
        double espaceOccupe = Plantes?.Sum(p => p?.PlaceNecessaire) ?? 0; // Gestion du cas null
        return (Surface * 10000 - espaceOccupe) >= plante.PlaceNecessaire; // Conversion m² en cm²
    }

    public virtual void AddPlant(Plante plante)
    {
        if (plante == null)
        {
            Console.WriteLine(" Erreur : plante null");
        }
        else if (CanPlant(plante))
        {
            Plantes.Add(plante);
        }
        else
        {
            Console.WriteLine("Pas assez d'espace pour cette plante");       
        }
    }
    public virtual void RemovePlant(Plante plante)
    {
        Plantes?.Remove(plante);
    }
}


        

