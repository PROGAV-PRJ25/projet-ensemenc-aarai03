public class Country
    {
        public string Nom { get; private set; }
        public List<Saison> SaisonsDeCroissance { get; private set; }
        public List<Type> PlantesDispo { get; private set; }
        public Meteo MeteoTypique { get; private set; }

        public Country(string nom, List<Saison> saisons, List<Type> plantes, Meteo meteo)
        {
            Nom = nom;
            SaisonsDeCroissance = saisons;
            PlantesDispo = plantes;
            MeteoTypique = meteo;
        }
    }

    