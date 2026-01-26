using System;
using System.Collections.Generic;

namespace Natural_Selection_Sim
{
    public abstract class Entity
    {
        protected static Random rng = new Random();

        public double BirthRate;
        public double DeathRate;
        public double MutationRate;
        public double Speed;
        public double Size;

        public bool HasEaten { get; protected set; }
        public bool IsAlive { get; set; } = true;

        protected Entity(double b, double d, double m, double s, double z)
        {
            BirthRate = b;
            DeathRate = d;
            MutationRate = m;
            Speed = s;
            Size = z;
        }

        protected Entity(Entity parent)
        {
            BirthRate = Mutate(parent.BirthRate);
            DeathRate = Mutate(parent.DeathRate);
            MutationRate = parent.MutationRate;
            Speed = Mutate(parent.Speed);
            Size = Mutate(parent.Size);
        }

        protected double Mutate(double value)
        {
            double factor = 1 + ((rng.NextDouble() * 2 - 1) * MutationRate);
            return Math.Max(0.01, value * factor);
        }

        public abstract void Act(List<Entity> entities, ref int plants);
        protected abstract Entity CreateChild();

        public Entity TryDuplicate()
        {
            if (HasEaten && rng.NextDouble() < BirthRate)
                return CreateChild();

            return null;
        }

        public void Reset()
        {
            HasEaten = false;
        }
    }
}
