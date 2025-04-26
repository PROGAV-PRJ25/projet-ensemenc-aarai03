public abstract class Plante
{
    public string Nom {get; set;}
    public bool Comestible {get; set;}
    public bool EstVivace {get; set;}
    public string TerrainPrefere {get; set;}
    public List<string> Maladie {get; set;}
    public List<string> Saisons {get; set;}
    public float EspaceRequis {get; set;}
    public float VitesseDeCroissance {get; set;}
    public float BesoinEau {get; set;}
    public float BesoinLumiere {get; set;}
    public int EsperanceVie {get; set;}
    public int NBPousse {get; set;}
    public (int min, int max) TemperaturePrefere {get; set;}

//(, pailler, arroser, traiter, semer telle ou telle graine, récolter un légume mûr, installer serre, une barrière, un pare-soleil…) 
    public abstract void Désherber();
    public abstract void Pailler();
    public abstract void Arroser();
    public abstract void Traiter();
    public abstract void Semer();
    public abstract void Recolter();
    public abstract void Installer();

}