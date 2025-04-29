public class Meteo
    {
        public double Temperature { get; set; }
        public double EauDispo { get; set; } // de 0 à 1
        public double LumiereDispo { get; set; } // de 0 à 1
        public MeteoEvent EvenementActuel { get; set; }

        public Meteo(double temp, double water, double light)
        {
            Temperature = temp;
            EauDispo = water;
            LumiereDispo = light;
            EvenementActuel = MeteoEvent.None;
        }
    }