using System;
using System.Collections.Generic;
using System.Linq;

namespace Natural_Selection_Sim
{
    public class SimulationController
    {
        private List<Entity> entities = new List<Entity>();//Liste für alle Entitys
        private int plantsPerStep;

        public SimulationController(SimulationConfig config)//Construcktor für die Simulation
        {
            plantsPerStep = config.PlantsPerStep;

            CreateSpecies<Carnivore>(config.Carnivore);
            CreateSpecies<Herbivore>(config.Herbivore);
            CreateSpecies<Omnivore>(config.Omnivore);
        }

        private void CreateSpecies<T>(SpeciesConfig cfg) where T : Entity//Erstellt die Startpopulation der jeweiligen Species
        {
            for (int i = 0; i < cfg.StartCount; i++)
            {
                entities.Add(
                    (T)Activator.CreateInstance(
                        typeof(T),
                        cfg.BirthRate,
                        cfg.DeathRate,
                        cfg.MutationRate,
                        cfg.Speed,
                        cfg.Size
                    )
                );
            }
        }

        public void Step()//Simuliert einen Zeitschritt
        {
            int plants = plantsPerStep;

            entities.Sort((a, b) => b.Speed.CompareTo(a.Speed));//Sortierung nach Speed damit schnellere Entitys zuerst handeln

            foreach (var e in entities)//Entitys dürfen handeln
                if (e.IsAlive)
                    e.Act(entities, ref plants);

            List<Entity> newborns = new List<Entity>();//Neue Liste für Kinder

            foreach (var e in entities) //Entitys dürfen sich vermehren
            {
                if (!e.IsAlive) continue;

                var child = e.TryDuplicate();
                if (child != null)
                    newborns.Add(child);
            }

            foreach (var e in entities)//Entitys die nicht gegessen haben sterben
                if (!e.HasEaten)
                    e.IsAlive = false;

            entities.RemoveAll(e => !e.IsAlive);//Entfernt alle toten Entitys aus der Liste
            entities.AddRange(newborns);//Fügt die Kinder zur Entity liste hinzu

            foreach (var e in entities)//Flags für das nächste Runde zurücksetzen
                e.Reset();
            
        }

        public SimulationStats GetStats()//Gibt Statistiken über die aktuelle Simulation zurück
        {
            SimulationStats stats = new SimulationStats();

            FillStats<Carnivore>(stats.Carnivores);
            FillStats<Herbivore>(stats.Herbivores);
            FillStats<Omnivore>(stats.Omnivores);

            return stats;
        }

        private void FillStats<T>(SpeciesStats s) where T : Entity//Füllt die Statistiken für die jeweilige Species
        {
            var list = entities.OfType<T>().ToList();
            s.Count = list.Count;

            if (s.Count == 0) return;

            s.AvgSpeed = list.Average(e => e.Speed);
            s.AvgSize = list.Average(e => e.Size);
            s.AvgBirthRate = list.Average(e => e.BirthRate);
            s.AvgDeathRate = list.Average(e => e.DeathRate);
            s.AvgMutationRate = list.Average(e => e.MutationRate);
        }
    }

    public class SpeciesConfig//Konfigurationsklasse für die Species
    {
        public int StartCount;
        public double BirthRate;
        public double DeathRate;
        public double MutationRate;
        public double Speed;
        public double Size;
    }

    public class SimulationConfig//Konfigurationsklasse für die Simulation
    {
        public int PlantsPerStep;
        public SpeciesConfig Carnivore = new SpeciesConfig();
        public SpeciesConfig Herbivore = new SpeciesConfig();
        public SpeciesConfig Omnivore = new SpeciesConfig();
    }

    public class SimulationStats//Statistiken über die Simulation
    {
        public SpeciesStats Carnivores = new SpeciesStats();
        public SpeciesStats Herbivores = new SpeciesStats();
        public SpeciesStats Omnivores = new SpeciesStats();
    }

    public class SpeciesStats//Statistiken über eine Species
    {
        public int Count;
        public double AvgSpeed;
        public double AvgSize;
        public double AvgBirthRate;
        public double AvgDeathRate;
        public double AvgMutationRate;
    }
}
