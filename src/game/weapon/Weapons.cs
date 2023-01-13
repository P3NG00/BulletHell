namespace BulletHell.Game.Weapon
{
    public sealed class Weapons : ObjectManager<Weapon>
    {
        private static ObjectManager<Weapon> s_instance;

        public static int Amount => s_instance.ObjectAmount;

        public static Weapon Pistol = new Weapon("Pistol", 12, 0.5f, 1.2f, 0.5f, 0);
        public static Weapon MachineGun = new Weapon("Machine Gun", 32, 0.1f, 1.5f, 0.75f, 1);
        public static Weapon MiniGun = new Weapon("Mini Gun", 100, 0.05f, 7f, 2f, 2);

        protected sealed override Weapon[] ObjectArray => new[]
        {
            Pistol,
            MachineGun,
            MiniGun,
        };

        public Weapons() : base(ref s_instance) {}

        public static Weapon FromID(int id) => s_instance.ObjectFromID(id);
    }
}
