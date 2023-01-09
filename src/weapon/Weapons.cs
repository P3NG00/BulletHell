using System.Collections.Immutable;

namespace BulletHell.Weapon
{
    public static class Weapons
    {
        public static int Amount => s_weapons.Length;

        public static readonly Weapon Pistol = new Weapon("Pistol", 12, 0.5f, 1.2f);
        public static readonly Weapon MachineGun = new Weapon("Machine Gun", 32, 0.1f, 1.5f);

        private static readonly ImmutableArray<Weapon> s_weapons = ImmutableArray.Create(
            Pistol,
            MachineGun);

        public static Weapon FromID(int id) => s_weapons[id];
    }
}
