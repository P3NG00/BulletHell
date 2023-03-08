namespace BulletHell.Game.Weapon
{
    public sealed class Weapons : ObjectManager<Weapon>
    {
        private static ObjectManager<Weapon> s_instance;

        public static int Amount => s_instance.ObjectAmount;

        // TODO make weapons less overpowered
        // TODO make more verbose and readable
        public static Weapon Pistol = new Weapon(new(2f, 9.5f, 8f, 1.5f), "Pistol", 16, 0.25f, 1.1f, 0.4f, 0);
        public static Weapon MachineGun = new Weapon(new(1.5f, 8f, 12f, 1f), "Machine Gun", 48, 0.1f, 1.25f, 0.6f, 1);
        public static Weapon MiniGun = new Weapon(new(0.75f, 4f, 6f, 0.75f), "Mini Gun", 256, 0.05f, 3.5f, 0.8f, 2);

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
