public enum TypeSol {Sable, Argile, terre, Mixte}

public abstract class Terrain{
    public int Id { get; }
    public double Surface { get; } // en m²
    public TypeSol TypeTerrain {get; protected set;}
    public double Qualite { get; protected set; } // de 0 à 1
    public double RetentionEau { get; protected set; } // de 0 à 1
    public double Drainage { get; protected set; } // de 0 à 1
    public List<Plante> Plantes { get; protected set; }

    protected Terrain()
    {
        Plantes = new List<Plante>();
        
    }

    protected Terrain(int id, double surface, TypeSol type)
    {
        Id = id;
        Surface = surface;
        TypeTerrain = type;
        Qualite = 0.7; // valeur par défaut
    }

    public virtual bool CanPlant(Plante plante)
    {
        double espaceOccupe = Plantes.Sum(p => p.PlaceNecessaire);
        return (Surface - espaceOccupe) >= plante.PlaceNecessaire;
    }

    public virtual void AddPlant(Plante plante)
    {
        if (CanPlant(plante))
        {
            Plantes.Add(plante);
        }
    }
    public virtual void RemovePlant(Plante plante)
    {
        Plantes.Remove(plante);
    }
}


        

