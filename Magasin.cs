public class Magasin
{
    public static void AfficherMenu(Jardin jardin)
    {
        bool continuer = true;
        while (continuer)
        {
            Console.Clear();
            Console.WriteLine("=== MAGASIN ===");
            Console.WriteLine($"Argent: {jardin.Argent}€");
            Console.WriteLine("\n1. Acheter des semis");
            Console.WriteLine("2. Vendre des récoltes");
            Console.WriteLine("3. Acheter un terrain");
            Console.WriteLine("4. Retour");
            
            Console.Write("\nChoix: ");
            var choix = Console.ReadLine();
            
            switch (choix)
            {
                case "1":
                    AcheterSemis(jardin);
                    break;
                case "2":
                    VendreRecoltes(jardin);
                    break;
                case "3":
                    AcheterTerrain(jardin);
                    break;
                case "4":
                    continuer = false;
                    break;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }
            
            if (continuer)
            {
                Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }
    }

    private static void AcheterSemis(Jardin jardin)
    {
        var prixSemis = new Dictionary<string, int>
        {
            {"Carotte", 1},
            {"Tomate", 2},
            {"Salade", 2},
            {"Rose", 3},
            {"PommeDeTerre", 2},
            {"Tournesol", 2},
            {"Cactus", 6},
            {"Bambou", 4},
            {"PlanteMagique", 10},
            {"Mandragore", 8}
        };
        
        Console.WriteLine("\n=== ACHAT DE SEMIS ===");
        foreach (var semis in prixSemis)
        {
            Console.WriteLine($"{semis.Key}: {semis.Value}€/unité");
        }
        
        Console.Write("\nQuel semis? (nom ou 'annuler'): ");
        string choix = Console.ReadLine();
        
        if (choix.ToLower() == "annuler") return;
        
        if (!prixSemis.ContainsKey(choix))
        {
            Console.WriteLine("Semis non disponible.");
            return;
        }
        
        Console.Write("Quantité? ");
        if (!int.TryParse(Console.ReadLine(), out int quantite) || quantite <= 0)
        {
            Console.WriteLine("Quantité invalide.");
            return;
        }
        
        int cout = prixSemis[choix] * quantite;
        if (cout > jardin.Argent)
        {
            Console.WriteLine("Fonds insuffisants.");
            return;
        }
        
        if (jardin.InventaireSemis.ContainsKey(choix))
            jardin.InventaireSemis[choix] += quantite;
        else
            jardin.InventaireSemis[choix] = quantite;
        
        jardin.Argent -= cout;
        Console.WriteLine($"Achat réussi! Vous avez maintenant {jardin.InventaireSemis[choix]} graines de {choix}.");
    }

    private static void VendreRecoltes(Jardin jardin)
    {
        var prixVente = new Dictionary<string, int>
        {
            {"Carotte", 2},
            {"Tomate", 3},
            {"Salade", 2},
            {"Rose", 5},
            {"PommeDeTerre", 2},
            {"Tournesol", 2},
            {"Cactus", 6},
            {"Bambou", 4},
            {"PlanteMagique", 10},
            {"Mandragore", 8}
        };
        
        if (jardin.InventaireRecoltes.All(r => r.Value == 0))
        {
            Console.WriteLine("\nVous n'avez aucune récolte à vendre!");
            return;
        }
        
        Console.WriteLine("\n=== VENTE DE RÉCOLTES ===");
        foreach (var recolte in jardin.InventaireRecoltes)
        {
            if (recolte.Value > 0)
            {
                int prix = prixVente.GetValueOrDefault(recolte.Key, 1);
                Console.WriteLine($"{recolte.Key}: {recolte.Value} unités ({prix}€/unité)");
            }
        }
        
        Console.Write("\nQuelle récolte? (nom ou 'annuler'): ");
        string choix = Console.ReadLine();
        
        if (choix.ToLower() == "annuler") return;
        
        if (!jardin.InventaireRecoltes.ContainsKey(choix) || jardin.InventaireRecoltes[choix] <= 0)
        {
            Console.WriteLine("Récolte non disponible.");
            return;
        }
        
        Console.Write("Quantité? ");
        if (!int.TryParse(Console.ReadLine(), out int quantite) || quantite <= 0)
        {
            Console.WriteLine("Quantité invalide.");
            return;
        }
        
        if (quantite > jardin.InventaireRecoltes[choix])
        {
            Console.WriteLine("Vous n'avez pas assez de cette récolte.");
            return;
        }
        
        int prixUnitaire = prixVente.GetValueOrDefault(choix, 1);
        int gain = prixUnitaire * quantite;
        
        jardin.InventaireRecoltes[choix] -= quantite;
        jardin.Argent += gain;
        
        Console.WriteLine($"Vente réussie! Gain: {gain}€");
    }

    private static void AcheterTerrain(Jardin jardin)
    {
        var prixTerrains = new Dictionary<string, int>
        {
            {"Sable", 50},   // par m²
            {"Terre", 70},
            {"Argile", 60},
            {"Mixte", 65}
        };
        
        Console.WriteLine("\n=== ACHAT DE TERRAIN ===");
        foreach (var terrain in prixTerrains)
        {
            Console.WriteLine($"{terrain.Key}: {terrain.Value}€/m²");
        }
        
        Console.Write("\nType de terrain? (nom ou 'annuler'): ");
        string type = Console.ReadLine();
        
        if (type.ToLower() == "annuler") return;
        
        if (!prixTerrains.ContainsKey(type))
        {
            Console.WriteLine("Type non disponible.");
            return;
        }
        
        Console.Write("Surface (m²)? ");
        if (!double.TryParse(Console.ReadLine(), out double surface) || surface <= 0)
        {
            Console.WriteLine("Surface invalide.");
            return;
        }
        
        int cout = (int)(prixTerrains[type] * surface);
        if (cout > jardin.Argent)
        {
            Console.WriteLine("Fonds insuffisants.");
            return;
        }
        
        int nouveauId = jardin.Terrains.Max(t => t.Id) + 1;
        Terrain nouveauTerrain = type switch
        {
            "Sable" => new TerrainSable(nouveauId, surface),
            "Terre" => new TerrainTerre(nouveauId, surface),
            "Argile" => new TerrainArgile(nouveauId, surface),
            "Mixte" => new TerrainMixte(nouveauId, surface),
            
        };
        
        if (nouveauTerrain != null)
        {
            jardin.Terrains.Add(nouveauTerrain);
            jardin.Argent -= cout;
            Console.WriteLine($"Terrain {type} de {surface}m² acheté pour {cout}€!");
        }
    }
}