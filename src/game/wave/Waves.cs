namespace BulletHell.Game.Waves
{
    public sealed class Waves : ObjectManager<Wave>
    {
        private static ObjectManager<Wave> s_instance;

        public static int Amount => s_instance.ObjectAmount;

        // TODO make waves more challenging
        protected sealed override Wave[] ObjectArray => new Wave[] {
            new(25f, 0.75f, 2f, 0),
            new(25f, 1f, 3f, 1),
            new(50f, 1.5f, 5f, 2),
        };

        public Waves() : base(ref s_instance) {}

        public static Wave FromID(int id) => s_instance.ObjectFromID(id);
    }
}
