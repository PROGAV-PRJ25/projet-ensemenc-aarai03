public class Lapin : Animal
{
    public Lapin() : base("Lapin", TypeAnimal.Lagomorphe)
    {
        ProbabiliteApparition = 0.15;
        DegatsPlantes = 0.7;
        ProbabiliteFuite = 0.4;
    }

    public override string GetSymbole() => "🐇";
}

public class Souris : Animal
{
    public Souris() : base("Souris", TypeAnimal.Rongeur)
    {
        ProbabiliteApparition = 0.2;
        DegatsPlantes = 0.5;
        ProbabiliteFuite = 0.6;
    }

    public override string GetSymbole() => "🐁";
}

public class Corbeau : Animal
{
    public Corbeau() : base("Corbeau", TypeAnimal.Oiseau)
    {
        ProbabiliteApparition = 0.1;
        DegatsPlantes = 0.3;
        ProbabiliteFuite = 0.8;
    }

    public override string GetSymbole() => "🐦";
}

public class Limace : Animal
{
    public Limace() : base("Limace", TypeAnimal.Insecte)
    {
        ProbabiliteApparition = 0.25;
        DegatsPlantes = 0.4;
        ProbabiliteFuite = 0.3;
    }

    public override string GetSymbole() => "🐌";
}