public class TerrainSable : Terrain
{
    public TerrainSable(int id, double surface) : base(id, surface, TypeSol.Sable)
    {
        RetentionEau = 0.3;
        Drainage = 0.9;
    }
}

public class TerrainArgile : Terrain
{
    public TerrainArgile(int id, double surface) : base(id, surface, TypeSol.Argile)
    {
        RetentionEau = 0.9;
        Drainage = 0.3;
    }
}

public class TerrainTerre : Terrain
{
    public TerrainTerre(int id, double surface) : base(id, surface, TypeSol.Terre)
    {
        RetentionEau = 0.7;
        Drainage = 0.6;
    }
}

public class TerrainMixte : Terrain
{
    public TerrainMixte(int id, double surface) : base(id, surface, TypeSol.Mixte)
    {
        RetentionEau = 0.8;
        Drainage = 0.5;
    }
}