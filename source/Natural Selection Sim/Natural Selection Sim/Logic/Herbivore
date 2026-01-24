namespace Natural_Selection_Sim
{
    public class Herbivore : Entity
    {
        public Herbivore(float b, float d, float m, float s, float z)
            : base(b, d, m, s, z) { }

        public Herbivore(Entity parent)
            : base(parent) { }

        public override void Act(List<Entity> entities, ref int plants)
        {
            if (plants > 0)
            {
                plants--;
                HasEaten = true;
            }
        }

        protected override Entity CreateChild()
        {
            return new Herbivore(this);
        }
    }
}
