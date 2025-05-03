public abstract class Terrain{
    public string TypeSol { get; protected set; }
    public double Size { get; protected set; } // en m²
    public double SunExposure { get; protected set; } // de 0 à 1
    public double Drainage { get; protected set; } // de 0 à 1
    public List<Plante> Plantes { get; protected set; }

    protected Terrain()
    {
        Plantes = new List<Plante>();
    }

    public virtual bool CanPlant(Plante plante)
    {
        double espaceOccupe = Plantes.Sum(p => p.EspaceRequis);
        return (Size - espaceOccupe) >= plante.EspaceRequis;
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


        

