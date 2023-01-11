using System.Collections.Immutable;

namespace BulletHell.Weapon
{
    public static class Weapons
    {
        public static int Amount => s_weapons.Length;

        public static readonly Weapon Pistol = new Weapon("Pistol", 12, 0.5f, 1.2f, 0.5f, 0);
        public static readonly Weapon MachineGun = new Weapon("Machine Gun", 32, 0.1f, 1.5f, 0.75f, 1);
        // TODO minigun. long reload, long clip size, high fire rate, long switch to time

        private static readonly ImmutableArray<Weapon> s_weapons = ImmutableArray.Create(
            Pistol,
            MachineGun);

        static Weapons()
        {
            // check id's
            for (int i = 0; i < s_weapons.Length; i++)
                if (s_weapons[i].ID != i)
                    throw new System.Exception($"Weapon '{s_weapons[i].Name}' has wrong ID");
        }

        public static Weapon FromID(int id) => s_weapons[id];
    }
}
