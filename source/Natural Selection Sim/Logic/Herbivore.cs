namespace Natural_Selection_Sim
{
    public class Herbivore : Entity
    {
        public Herbivore(double b, double d, double m, double s, double z)
            : base(b, d, m, s, z) { }

        public Herbivore(Entity parent)
            : base(parent) { }

        public override void Act(List<Entity> entities, ref int plants)//Fressen von Pflanzen
        {
            if (plants > 0)
            {
                plants--;
                HasEaten = true;
            }
        }

        protected override Entity CreateChild()//Erstellt ein neues Herbivore objekt
        {
            return new Herbivore(this);
        }
    }
}
