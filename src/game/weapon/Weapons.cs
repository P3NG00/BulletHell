namespace BulletHell.Game.Weapon
{
    public sealed class Weapons : ObjectManager<Weapon>
    {
        private static ObjectManager<Weapon> s_instance;

        public static int Amount => s_instance.ObjectAmount;

        // TODO make weapons less overpowered
        public static readonly Weapon Pistol = new Weapon(
            projectileInfo: new(
                damage: 2f,
                speed: 9.5f,
                radius: 10f,
                lifeSeconds: 1.5f
            ),
            name: "Pistol",
            clipSize: 16,
            fireRateSeconds: 0.25f,
            reloadSeconds: 1.1f,
            switchSeconds: 0.4f,
            id: 0
        );
        public static readonly Weapon MachineGun = new Weapon(
            projectileInfo: new(
                damage: 1.5f,
                speed: 8f,
                radius: 8f,
                lifeSeconds: 1f
            ),
            name: "Machine Gun",
            clipSize: 48,
            fireRateSeconds: 0.17f,
            reloadSeconds: 1.25f,
            switchSeconds: 0.6f,
            id: 1
        );
        public static readonly Weapon MiniGun = new Weapon(
            projectileInfo: new(
                damage: 0.75f,
                speed: 4f,
                radius: 6f,
                lifeSeconds: 0.75f
            ),
            name: "Mini Gun",
            clipSize: 256,
            fireRateSeconds: 0.05f,
            reloadSeconds: 3.5f,
            switchSeconds: 0.8f,
            id: 2
        );
        // TODO create shotgun with spread

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
