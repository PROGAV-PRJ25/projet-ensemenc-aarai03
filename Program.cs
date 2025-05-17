
var terrain = new TerrainTerre(1, 10) // 10 m²
{
    Qualite = 0.8,
    RetentionEau = 0.7,
    Drainage = 0.6
};





// Initialisation des données
var catalogue = new List<Plante>
{
    new Carotte(),
    new Tomate(),
    new Rose(),
    new Salade(),
    new PommeDeTerre(),
    new Tournesol(),
    new Cactus(),
    new Bambou(),
    new PlanteMagique(),
    new Mandragore()
};

var simulateur = new Simulateur(terrain, catalogue, new France());
simulateur.AfficherMenu();

// Création du simulateur





