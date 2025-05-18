public enum TypeAnimal
{
    Rongeur,
    Oiseau,
    Insecte,
    Reptile,
    Lagomorphe
}

public abstract class Animal
{
    public string Nom { get; protected set; }
    public TypeAnimal Type { get; protected set; }
    public double ProbabiliteApparition { get; protected set; } // 0 à 1
    public double DegatsPlantes { get; protected set; } // 0 à 1
    public double ProbabiliteFuite { get; protected set; } // 0 à 1

    protected Animal(string nom, TypeAnimal type)
    {
        Nom = nom;
        Type = type;
    }

    public virtual void EndommagerPlante(Plante plante)
    {
        if (new Random().NextDouble() < DegatsPlantes)
        {
            double degats = 0.1 + (new Random().NextDouble() * 0.4); // Entre 10% et 50% de dégâts
            plante.Sante -= degats;
            Console.WriteLine($"{Nom} a endommagé {plante.Nom}! Santé -{(degats*100):F0}%");
            
            if (plante.Sante <= 0)
            {
                plante.Mourir();
                Console.WriteLine($"{plante.Nom} a été détruite par {Nom}!");
            }
        }
        else
        {
            Console.WriteLine($"{Nom} a tenté d'attaquer {plante.Nom} mais a échoué!");
        }
    }

    public abstract string GetSymbole();
}