public enum MeteoEvent 
{
    Pluie, 
    Gel, 
    Averse, 
    VagueChaleur, 
    Secheresse, 
    Orage,
    Grele,
    VentViolent,
    Brouillard,
    None
}

public class Meteo
{
    public double Temperature { get; set; }
    public double Precipitation { get; set; } // de 0 à 1
    public double LumiereDispo { get; set; } // de 0 à 1
    public MeteoEvent EvenementActuel { get; set; }
    public string Description { get; set; }

    public Meteo(double temp, double precip, double lumiere, MeteoEvent evenement, string desc)
    {
        Temperature = temp;
        Precipitation = precip;
        LumiereDispo = lumiere;
        EvenementActuel = evenement;
        Description = desc;
    }
}