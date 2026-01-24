namespace Natural_Selection_Sim
{
    public class Omnivore : Entity
    {
        public Omnivore(float b, float d, float m, float s, float z)
            : base(b, d, m, s, z) { }

        public Omnivore(Entity parent)
            : base(parent) { }

        public override void Act(List<Entity> entities, ref int plants)
        {
            if (plants > 0)
            {
                plants--;
                HasEaten = true;
                return;
            }

            var possibleTargets = entities.FindAll(e =>
                e != this &&
                e.IsAlive &&
                e.Speed < Speed &&
                e.Size < Size * 1.2f
            );

            if (possibleTargets.Count == 0) return;

            var target = possibleTargets[rng.Next(possibleTargets.Count)];
            target.IsAlive = false;
            HasEaten = true;
        }

        protected override Entity CreateChild()
        {
            return new Omnivore(this);
        }
    }
}
