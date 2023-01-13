using System.Collections.Immutable;

namespace BulletHell.Game.Waves
{
    public static class Waves // TODO implement GameObject and ObjectManager<T:GameObject>
    {
        private static ImmutableArray<Wave> s_waves = ImmutableArray.Create(new Wave[] {
            new(25f, 0.5f, 0),
            new(25f, 0.75f, 1),
            new(50f, 1.25f, 2),
        });

        // TODO implement id checking against array index

        public static Wave FromID(int id) => s_waves[id];
    }
}
