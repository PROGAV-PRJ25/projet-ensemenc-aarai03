using System;
using System.Collections.Generic;
using System.Linq;

public class Simulateur
{
    private Jardin Jardin { get; set; }
    private List<Plante> Catalogue { get; set; }
    private Magasin Magasin { get; set; }

    public Simulateur(Jardin jardin, List<Plante> catalogue)
    {
        Jardin = jardin;
        Catalogue = catalogue;
        Magasin = new Magasin();
    }

    public void AfficherMenuPrincipal()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ENSemenC - Simulateur de Potager ===");
            Console.WriteLine($"Date: {Jardin.DateCourante:dd/MM/yyyy}");
            Console.WriteLine($"Localisation: {Jardin.Localisation.Nom}");
            Console.WriteLine($"Argent: {Jardin.Argent}€");
            Console.WriteLine("\n1. Gérer le jardin");
            Console.WriteLine("2. Passer une semaine");
            Console.WriteLine("3. Accéder au magasin");
            Console.WriteLine("4. Quitter");

            Console.Write("\nChoix: ");
            var choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    GererJardin();
                    break;
                case "2":
                    PasserSemaine();
                    break;
                case "3":
                    Magasin.AfficherMenu(Jardin);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }
        }
    }

    private void GererJardin()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== GESTION DU JARDIN ===");
            Jardin.AfficherEtat();
            
            if (Jardin.Animaux.Any())
            {
                Console.WriteLine("\nAnimaux présents:");
                foreach (var animal in Jardin.Animaux)
                {
                    Console.WriteLine($"- {animal.Nom} {animal.GetSymbole()}");
                }
            }

            Console.WriteLine("\nActions disponibles:");
            Console.WriteLine("1. Semer des plantes");
            Console.WriteLine("2. Récolter les plantes mûres");
            Console.WriteLine("3. Traiter les plantes malades");
            Console.WriteLine("4. EffrayerAnimaux");
            Console.WriteLine("5. Retour");

            Console.Write("\nChoix: ");
            var choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    SemerPlantes();
                    break;
                case "2":
                    RecolterPlantes();
                    break;
                case "3":
                    TraiterPlantesMalades();
                    break;
                case "4":
                    EffrayerAnimaux();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }

            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
    }

    private void EffrayerAnimaux()
    {
        Jardin.EffrayerAnimaux();
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    private void SemerPlantes()
    {
        Console.WriteLine("\n=== SEMER DES PLANTES ===");
        
        // Afficher les semis disponibles
        Console.WriteLine("\nSemis disponibles:");
        foreach (var semis in Jardin.InventaireSemis)
        {
            if (semis.Value > 0)
                Console.WriteLine($"- {semis.Key}: {semis.Value} graines");
        }

        if (Jardin.InventaireSemis.All(s => s.Value <= 0))
        {
            Console.WriteLine("\nVous n'avez plus de semis disponibles!");
            Console.WriteLine("Visitez le magasin pour en acheter.");
            return;
        }

        Console.Write("\nQuelle plante voulez-vous semer? (nom ou 'annuler'): ");
        string choix = Console.ReadLine();
        
        if (choix.ToLower() == "annuler") return;
        
        if (!Jardin.InventaireSemis.ContainsKey(choix) || Jardin.InventaireSemis[choix] <= 0)
        {
            Console.WriteLine("Semis non disponible.");
            return;
        }
        
        // Choisir le terrain
        Console.WriteLine("\nTerrains disponibles:");
        foreach (var terrain1 in Jardin.Terrains)
        {
            double espaceOccupe = terrain1.Plantes.Sum(p => p.PlaceNecessaire);
            double espaceDispo = terrain1.Surface * 10000 - espaceOccupe; // Conversion m² en cm²
            Console.WriteLine($"- Terrain {terrain1.Id}: {terrain1.Surface}m² ({terrain1.TypeTerrain}), " + 
                             $"Espace libre: {espaceDispo/10000:F2}m²");
        }
        
        Console.Write("\nSur quel terrain? (numéro): ");
        if (!int.TryParse(Console.ReadLine(), out int terrainId))
        {
            Console.WriteLine("Numéro invalide.");
            return;
        }
        
        var terrain = Jardin.Terrains.FirstOrDefault(t => t.Id == terrainId);
        if (terrain == null)
        {
            Console.WriteLine("Terrain introuvable.");
            return;
        }
        
        // Créer la plante
        Plante nouvellePlante = CreerNouvellePlante(choix);
        if (nouvellePlante == null)
        {
            Console.WriteLine("Type de plante inconnu.");
            return;
        }

        if (terrain.CanPlant(nouvellePlante))
        {
            terrain.AddPlant(nouvellePlante);
            Jardin.InventaireSemis[choix]--;
            Console.WriteLine($"{choix} semée avec succès sur le terrain {terrainId}!");
        }
        else
        {
            Console.WriteLine("Pas assez d'espace pour cette plante.");
        }
    }

    private Plante CreerNouvellePlante(string typePlante)
    {
        return typePlante switch
        {
            "Carotte" => new Carotte(),
            "Tomate" => new Tomate(),
            "Rose" => new Rose(),
            "Salade" => new Salade(),
            "PommeDeTerre" => new PommeDeTerre(),
            "Tournesol" => new Tournesol(),
            "Cactus" => new Cactus(),
            "Bambou" => new Bambou(),
            "PlanteMagique" => new PlanteMagique(),
            "Mandragore" => new Mandragore(),
            
        };
    }

    private void PasserSemaine()
    {
        Jardin.PasserSemaine();
        
        // Vérifier si des plantes sont prêtes à être récoltées
        bool plantesRecoltables = Jardin.Terrains
            .Any(t => t.Plantes.Any(p => p.Etat == EtatPlante.Recoltable));
        
        if (plantesRecoltables)
        {
            Console.WriteLine("\nATTENTION: Certaines plantes sont prêtes à être récoltées!");
        }
    }

    private void RecolterPlantes()
    {
        int totalRecolte = 0;
        
        foreach (var terrain in Jardin.Terrains)
        {
            foreach (var plante in terrain.Plantes.ToList())
            {
                if (plante.Etat == EtatPlante.Recoltable)
                {
                    int quantite = plante.Recolter();
                    totalRecolte += quantite;
                    
                    if (Jardin.InventaireRecoltes.ContainsKey(plante.Nom))
                        Jardin.InventaireRecoltes[plante.Nom] += quantite;
                    else
                        Jardin.InventaireRecoltes[plante.Nom] = quantite;
                    
                    if (plante.Etat == EtatPlante.Mort)
                        terrain.RemovePlant(plante);
                    
                    Console.WriteLine($"- {plante.Nom}: {quantite} unités récoltées");
                }
            }
        }
        
        if (totalRecolte > 0)
        {
            Console.WriteLine($"\nTotal récolté: {totalRecolte} unités");
        }
        else
        {
            Console.WriteLine("\nAucune plante n'est prête à être récoltée.");
        }
    }

    private void TraiterPlantesMalades()
    {
        int plantesTraitees = 0;
        
        foreach (var terrain in Jardin.Terrains)
        {
            foreach (var plante in terrain.Plantes)
            {
                if (plante.Etat == EtatPlante.Malade)
                {
                    plante.Traiter();
                    plantesTraitees++;
                    Console.WriteLine($"- {plante.Nom} a été traitée");
                }
            }
        }
        
        if (plantesTraitees == 0)
        {
            Console.WriteLine("\nAucune plante malade à traiter.");
        }
        else
        {
            Console.WriteLine($"\nTotal plantes traitées: {plantesTraitees}");
        }
    }
}