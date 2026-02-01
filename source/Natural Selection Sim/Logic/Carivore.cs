namespace Natural_Selection_Sim
{
    public class Carnivore : Entity
    {
        public Carnivore(double b, double d, double m, double s, double z)
            : base(b, d, m, s, z) { }

        public Carnivore(Entity parent)
            : base(parent) { }

        public override void Act(List<Entity> entities, ref int plants)
        {
            var possibleTargets = entities.FindAll(e => // Erstellt eine liste mit allen Entity die Gefressen werden können
                e != this &&
                e.IsAlive &&
                e.Speed < Speed &&
                e.Size < Size * 1.2f
            );

            if (possibleTargets.Count == 0) return;

            var target = possibleTargets[rng.Next(possibleTargets.Count)];//sucht sich zufällig ein target aus der liste aus
            target.IsAlive = false;
            HasEaten = true;
        }

        protected override Entity CreateChild()//Erstellt ein neues Carnivore objekt
        {
            return new Carnivore(this);
        }
    }
}
