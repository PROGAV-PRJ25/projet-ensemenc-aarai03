// Enumérations pour les caractéristiques communes
    public enum TypePlante { Annual, Perennial, Weed, Ornamental, Commercial }
    public enum TypeTerrain { Sand, Loam, Clay }
    public enum Saison { Spring, Summer, Autumn, Winter }
    public enum EtatSante { Healthy, Stressed, Diseased, Dying, Dead }
    public enum MeteoEvent { None, HeavyRain, Hailstorm, Drought, HeatWave, Frost }
    
    public enum TypeIntru { None, Rabbit, Rodent, Bird, Snail, Insect }

public abstract class Plante
{
    public string Nom {get; protected set;}
    public TypePlante Type { get; protected set; }
    public List<Saison> SaisonsPlantation { get; protected set; }
    public bool Comestible {get; protected set;}
    public bool EstVivace {get; protected set;}
    public string TerrainPrefere {get; protected set;}
    public List<string> Maladie {get; protected set;}
    public List<string> Saisons {get; protected set;}
    public float EspaceRequis {get; protected set;} // en cm²
    public float VitesseDeCroissance {get; protected set;} // en cm par semaine
    public float BesoinEau {get; protected set;} // en %
    public float BesoinLumiere {get; protected set;} // en %
    public int EsperanceVie {get; protected set;}
    public int NBPousse {get; protected set;}
     public EtatSante Sante { get; protected set; }
    public (int min, int max) TemperaturePrefere {get; protected set;} // en °C
    public int Age {get; protected set;} // en semaine
    public double HauteurActuelle { get; protected set; } // en cm


    protected Plante()
        {
            SaisonsPlantation = new List<Saison>();
            Sante = EtatSante.Healthy;
            Age = 0;
            HauteurActuelle = 0.1; // début comme semis
        }

    protected virtual double CalculerConditionScore(Meteo meteo, Terrain typeSole)
        {
            double score = 0;
            int facteurs= 0;

            // Vérification du sol
            if (typeSole.TypeSol == TerrainPrefere) score += 1.0;
            else score += 0.5;
            facteurs++;

            // Vérification de l'eau
            double waterDiff = Math.Abs(meteo.EauDispo - BesoinEau);
            score += 1.0 - waterDiff;
            facteurs++;

            // Vérification de la lumière
            double lightDiff = Math.Abs(meteo.LumiereDispo - BesoinLumiere);
            score += 1.0 - lightDiff;
            facteurs++;

            // Vérification de la température
            if (meteo.Temperature >= TemperaturePrefere.min && meteo.Temperature <= TemperaturePrefere.max)
                score += 1.0;
            else if (meteo.Temperature < TemperaturePrefere.min - 5 || meteo.Temperature > TemperaturePrefere.max + 5)
                score += 0.2; // conditions extrêmes
            else
                score += 0.5; // conditions non idéales
            facteurs++;

            return score / facteurs;
        }

        protected virtual void UpdateHealth(double conditionScore)
        {
            if (conditionScore >= 0.8)
                Sante = EtatSante.Healthy;
            else if (conditionScore >= 0.6)
                Sante = EtatSante.Stressed;
            else if (conditionScore >= 0.5)
                Sante = EtatSante.Diseased;
            else
                Sante = EtatSante.Dying;
        }


    public virtual void Grandir(Meteo meteo, Terrain terrain, double FacteurSoin)
        {
            Age++;
            
            // Vérifier les conditions de croissance
            double conditionScore = CalculerConditionScore(meteo, terrain);
            
            if (conditionScore < 0.5)
            {
                Sante = EtatSante.Dying;
                return;
            }

            // Croissance en fonction des conditions
            double FacteurCroissance = conditionScore * FacteurSoin;
            HauteurActuelle += VitesseDeCroissance * FacteurCroissance;
            
            // Mise à jour de la santé
            UpdateHealth(conditionScore);
            
            // Vérifier la fin de vie
            if (Age >= EsperanceVie)
            {
                Sante = EtatSante.Dead;
            }
        }
















//(, pailler, arroser, traiter, semer telle ou telle graine, récolter un légume mûr, installer serre, une barrière, un pare-soleil…) 
    public abstract void Désherber();
    public abstract void Pailler();
    public abstract void Arroser();
    public abstract void Traiter();
    public abstract void Semer();
    public abstract void Recolter();
    public abstract void Installer();

}