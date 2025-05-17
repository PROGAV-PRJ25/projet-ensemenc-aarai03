using System;
using System.Collections.Generic;
using System.Linq;

public class Simulateur
{
    private Terrain Terrain;
    private List<Plante> Catalogue;
    private Pays Pays;

    public Simulateur(Terrain terrain, List<Plante> catalogue, Pays pays)
    {
        Terrain = terrain;
        Catalogue = catalogue;
        Pays = pays;
    }
/*
    public void AfficherCatalogueEtSemer()
    {
        Console.WriteLine("Catalogue de plantes disponibles :");
        for (int i = 0; i < Catalogue.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Catalogue[i].Nom} (Type: {Catalogue[i].Type})");
        }

        Console.WriteLine("Combien de plantes voulez-vous semer ?");
        if (int.TryParse(Console.ReadLine(), out int nombrePlantes) && nombrePlantes > 0)
        {
            for (int i = 0; i < nombrePlantes; i++)
            {
                Console.WriteLine($"Choisissez une plante à semer (numéro {i + 1}) :");
                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Catalogue.Count)
                {
                    Plante nouvellePlante = CreerNouvellePlante(Catalogue[index - 1]);
                    if (Terrain.CanPlant(plante))
                    {
                        Terrain.AddPlant(plante);
                        Console.WriteLine($"{plante.Nom} a été semée avec succès !");
                    }
                    else
                    {
                        Console.WriteLine("Pas assez d'espace pour cette plante.");
                    }
                }
                else
                {
                    Console.WriteLine("Numéro invalide.");
                }
            }
        }
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }
*/
    public void AfficherCatalogueEtSemer()
    {
        Console.WriteLine("Catalogue de plantes disponibles :");
        for (int i = 0; i < Catalogue.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Catalogue[i].Nom} (Type: {Catalogue[i].Type})");
        }

        Console.WriteLine("Combien de plantes voulez-vous semer ?");
        if (int.TryParse(Console.ReadLine(), out int nombrePlantes) && nombrePlantes > 0)
        {
            for (int i = 0; i < nombrePlantes; i++)
            {
                Console.WriteLine($"Choisissez une plante à semer (numéro {i + 1}) :");
                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Catalogue.Count)
                {
                    // Création d'une nouvelle instance pour éviter les références partagées
                    Plante nouvellePlante = CreerNouvellePlante(Catalogue[index - 1]);
                    Terrain.AddPlant(nouvellePlante);
                }
                else
                {
                    Console.WriteLine("Numéro invalide.");
                    i--; // Réessayer pour cette plante
                }
            }
        }
        Console.ReadKey();
    }

    private Plante CreerNouvellePlante(Plante modele)
    {
        // Implémentez cette méthode pour cloner correctement chaque type de plante
        switch (modele)
        {
            case Carotte c: return new Carotte();
            case Tomate t: return new Tomate();
            // ... autres types de plantes
            default: return modele; // fallback
        }
    }
    public void PasserTour(int semaine)
    {
        if (!Terrain.Plantes.Any())
        {
            Console.WriteLine("Aucune plante semée !");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"\n-------- Semaine N° {semaine} --------");

        // Générer la météo
        Meteo meteo = Pays.GenererMeteo(DateTime.Now.AddDays(semaine * 7));
        Console.WriteLine($"Météo : Température = {meteo.Temperature}°C, Précipitation = {meteo.Precipitation * 100}%, Lumière = {meteo.LumiereDispo * 100}%");
        Console.WriteLine($"Événement météo : {meteo.EvenementActuel}");

        // Générer un événement aléatoire
        Evenement evenement = Pays.GenererEvenement();
        Console.WriteLine($"Événement : {evenement.Description} (Impact : {evenement.Impact})");

        // Appliquer l'impact de l'événement sur les plantes
        foreach (var plante in Terrain.Plantes.ToList())
        {
            plante.Sante += evenement.Impact * 0.01; // Convertir l'impact en pourcentage
            if (plante.Sante <= 0)
            {
                plante.Mourir();
            }
        }

        // Faire pousser les plantes en fonction de la météo
        foreach (var plante in Terrain.Plantes.ToList())
        {
            plante.Pousser(Terrain.Qualite, meteo.Precipitation, meteo.LumiereDispo, meteo.Temperature);
            if (plante.Etat == EtatPlante.Mort)
            {
                Terrain.RemovePlant(plante);
                Console.WriteLine($"{plante.Nom} est morte et a été retirée du terrain.");
            }
            else if (plante.Etat == EtatPlante.Recoltable)
            {
                Console.WriteLine($"{plante.Nom} est prête à être récoltée !");
            }
        }

        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    public void RecolterPlantes()
    {
        foreach (var plante in Terrain.Plantes.ToList())
        {
            if (plante.Etat == EtatPlante.Recoltable)
            {
                int recolte = plante.Recolter();
                Console.WriteLine($"Vous avez récolté {recolte} unités de {plante.Nom}.");
                if (plante.Etat == EtatPlante.Mort)
                {
                    Terrain.RemovePlant(plante);
                }
            }
        }
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    public void AfficherEtatPlantes()
    {
        Console.WriteLine("État des plantes :");
        foreach (var plante in Terrain.Plantes)
        {
            Console.WriteLine($"{plante.Nom} : État = {plante.Etat}, Santé = {plante.Sante * 100}%, Taille = {plante.Taille}cm, Âge = {plante.Age} semaines");
        }
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    public void TraiterPlantesMalades()
    {
        foreach (var plante in Terrain.Plantes)
        {
            if (plante.Etat == EtatPlante.Malade)
            {
                plante.Traiter();
                Console.WriteLine($"{plante.Nom} a été traitée et est maintenant en meilleure santé.");
            }
        }
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    public void AfficherMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Menu Principal ===");
            Console.WriteLine("1. Afficher le catalogue et semer des plantes");
            Console.WriteLine("2. Passer une semaine");
            Console.WriteLine("3. Récolter les plantes prêtes");
            Console.WriteLine("4. Afficher l'état des plantes");
            Console.WriteLine("5. Traiter les plantes malades");
            Console.WriteLine("6. Quitter");
            Console.Write("Choisissez une option : ");

            string choix = Console.ReadLine();
            switch (choix)
            {
                case "1":
                    AfficherCatalogueEtSemer();
                    break;
                case "2":
                    PasserTour(1); // Passer une semaine
                    break;
                case "3":
                    RecolterPlantes();
                    break;
                case "4":
                    AfficherEtatPlantes();
                    break;
                case "5":
                    TraiterPlantesMalades();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Option invalide.");
                    break;
            }
        }
    }
}


