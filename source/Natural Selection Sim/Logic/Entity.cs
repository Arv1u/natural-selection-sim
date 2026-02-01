using System;
using System.Collections.Generic;

namespace Natural_Selection_Sim
{
    public abstract class Entity // Abstrackte Klasse für Entitys
    {
        protected static Random rng = new Random();

        public double MutationRate;
        public double BirthRate;
        public double DeathRate;
        public double Speed;
        public double Size;

        public bool HasEaten { get; protected set; }
        public bool IsAlive { get; set; } = true;

        protected Entity(double b, double d, double m, double s, double z)//Construcktor für Entitys welche kein Eltern haben 
        {
            MutationRate = m;
            BirthRate = Mutate(b);
            DeathRate = Mutate(d);  
            Speed = Mutate(s);
            Size = Mutate(z);
        }

        protected Entity(Entity parent)//Construcktor für Entitys welche geboren werden
        {
            MutationRate = parent.MutationRate;
            BirthRate = Mutate(parent.BirthRate);
            DeathRate = Mutate(parent.DeathRate);
            Speed = Mutate(parent.Speed);
            Size = Mutate(parent.Size);
        }

        protected double Mutate(double value)//Mutiert die Werte
        {
            double factor = 1.0 + ((rng.NextDouble() * 2.0 - 1.0) * MutationRate);

            //TODO: refactor. Quick and dirty int overflow fix.
            if (value * factor >= 2000000000)
                return value;

            return Math.Max(0.01, value * factor);
        }

        public abstract void Act(List<Entity> entities, ref int plants); //Abstrackte Methode für das Verhalten der Entitys
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
