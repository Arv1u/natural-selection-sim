using System;
using System.Collections.Generic;

namespace Natural_Selection_Sim
{
    public abstract class Entity
    {
        protected static Random rng = new Random();

        public float BirthRate;
        public float DeathRate;
        public float MutationRate;
        public float Speed;
        public float Size;

        public bool HasEaten { get; protected set; }
        public bool IsAlive { get; set; } = true;

        protected Entity(float b, float d, float m, float s, float z)
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

        protected float Mutate(float value)
        {
            float factor = 1f + (float)((rng.NextDouble() * 2 - 1) * MutationRate);
            return Math.Max(0.01f, value * factor);
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
