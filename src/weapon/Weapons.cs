namespace BulletHell.Weapon
{
    public static class Weapons
    {
        public static int Amount => s_weapons.Length;

        public static readonly Weapon Pistol = new Weapon("Pistol", 0.5f);
        public static readonly Weapon MachineGun = new Weapon("Machine Gun", 0.1f);

        private static readonly Weapon[] s_weapons = new[]
        {
            Pistol,
            MachineGun,
        };

        public static Weapon FromID(int id) => s_weapons[id];
    }
}
