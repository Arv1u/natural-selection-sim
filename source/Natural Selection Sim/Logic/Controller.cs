using System;
using System.Collections.Generic;
using System.Linq;

namespace Natural_Selection_Sim
{
    public class SimulationController
    {
        private List<Entity> entities = new List<Entity>();
        private int plantsPerStep;

        public SimulationController(SimulationConfig config)
        {
            plantsPerStep = config.PlantsPerStep;

            CreateSpecies<Carnivore>(config.Carnivore);
            CreateSpecies<Herbivore>(config.Herbivore);
            CreateSpecies<Omnivore>(config.Omnivore);
        }

        private void CreateSpecies<T>(SpeciesConfig cfg) where T : Entity
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

        public void Step()
        {
            int plants = plantsPerStep;

            entities.Sort((a, b) => b.Speed.CompareTo(a.Speed));

            foreach (var e in entities)
                if (e.IsAlive)
                    e.Act(entities, ref plants);

            List<Entity> newborns = new List<Entity>();

            foreach (var e in entities)
            {
                if (!e.IsAlive) continue;

                var child = e.TryDuplicate();
                if (child != null)
                    newborns.Add(child);
            }

            foreach (var e in entities)
                if (!e.HasEaten)
                    e.IsAlive = false;

            entities.RemoveAll(e => !e.IsAlive);
            entities.AddRange(newborns);

            foreach (var e in entities)
                e.Reset();
            
        }

        public SimulationStats GetStats()
        {
            SimulationStats stats = new SimulationStats();

            FillStats<Carnivore>(stats.Carnivores);
            FillStats<Herbivore>(stats.Herbivores);
            FillStats<Omnivore>(stats.Omnivores);

            return stats;
        }

        private void FillStats<T>(SpeciesStats s) where T : Entity
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

    public class SpeciesConfig
    {
        public int StartCount;
        public double BirthRate;
        public double DeathRate;
        public double MutationRate;
        public double Speed;
        public double Size;
    }

    public class SimulationConfig
    {
        public int PlantsPerStep;
        public SpeciesConfig Carnivore = new SpeciesConfig();
        public SpeciesConfig Herbivore = new SpeciesConfig();
        public SpeciesConfig Omnivore = new SpeciesConfig();
    }

    public class SimulationStats
    {
        public SpeciesStats Carnivores = new SpeciesStats();
        public SpeciesStats Herbivores = new SpeciesStats();
        public SpeciesStats Omnivores = new SpeciesStats();
    }

    public class SpeciesStats
    {
        public int Count;
        public double AvgSpeed;
        public double AvgSize;
        public double AvgBirthRate;
        public double AvgDeathRate;
        public double AvgMutationRate;
    }
}
