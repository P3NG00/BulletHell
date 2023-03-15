namespace BulletHell.Game.Weapon
{
    public sealed class Weapons : ObjectManager<Weapon>
    {
        private static ObjectManager<Weapon> s_instance;

        public static int Amount => s_instance.ObjectAmount;

        // TODO balance weapon stats
        public static readonly Weapon Pistol = new(
            weaponInfo: new(
                name: "Pistol",
                clipSize: 16,
                fireRateSeconds: 0.25f,
                reloadSeconds: 1.1f,
                switchSeconds: 0.4f
            ),
            projectileInfo: new(
                damage: 2f,
                speed: 9.5f,
                radius: 10f,
                lifeSeconds: 1.5f
            ),
            id: 0
        );
        public static readonly Weapon MachineGun = new(
            weaponInfo: new(
                name: "Machine Gun",
                clipSize: 48,
                fireRateSeconds: 0.17f,
                reloadSeconds: 1.25f,
                switchSeconds: 0.6f
            ),
            projectileInfo: new(
                damage: 1.5f,
                speed: 8f,
                radius: 8f,
                lifeSeconds: 1f
            ),
            id: 1
        );
        public static readonly Weapon Minigun = new(
            weaponInfo: new(
                name: "Minigun",
                clipSize: 256,
                fireRateSeconds: 0.05f,
                reloadSeconds: 3.5f,
                switchSeconds: 1f,
                projectileSpread: 0.15f
            ),
            projectileInfo: new(
                damage: 0.75f,
                speed: 4f,
                radius: 6f,
                lifeSeconds: 0.75f
            ),
            id: 2
        );
        public static readonly Weapon Shotgun = new(
            weaponInfo: new(
                name: "Shotgun",
                clipSize: 8,
                fireRateSeconds: 0.75f,
                reloadSeconds: 2f,
                switchSeconds: 0.8f,
                projectilesPerShot: 8,
                projectileSpread: 0.3f
            ),
            projectileInfo: new(
                damage: 0.8f,
                speed: 6f,
                radius: 4f,
                lifeSeconds: 0.5f
            ),
            id: 3
        );

        protected sealed override Weapon[] ObjectArray => new[]
        {
            Pistol,
            MachineGun,
            Minigun,
            Shotgun,
        };

        public Weapons() : base(ref s_instance) {}

        public static Weapon FromID(int id) => s_instance.ObjectFromID(id);
    }
}
