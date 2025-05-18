
var france = new France();
var jardin = new Jardin(france);





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

var simulateur = new Simulateur(jardin, catalogue);
simulateur.AfficherMenuPrincipal();

// Création du simulateur





