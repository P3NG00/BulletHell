using BulletHell.Scenes;
using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public static class WaveManager
    {
        private const float SPAWN_DISTANCE = 1000f; // TODO adjust to ensure spawning offscreen

        public static int CurrentWaveTicks { get; private set; }
        public static int NextSpawnTicks { get; private set; }

        public static int CurrentWave => s_wave.ID;

        private static bool CanSpawn => NextSpawnTicks <= 0;

        private static Wave s_wave;

        public static void Tick()
        {
            NextSpawnTicks--;
            if (CanSpawn)
            {
                NextSpawnTicks = s_wave.SpawnRateTicks;
                SpawnEnemy();
            }
            CurrentWaveTicks--;
            if (CurrentWaveTicks <= 0)
            {
                s_wave = Waves.FromID(s_wave.ID + 1);
                CurrentWaveTicks = s_wave.WaveLengthTicks;
            }
        }

        public static void Reset()
        {
            s_wave = Waves.FromID(0);
            CurrentWaveTicks = s_wave.WaveLengthTicks;
            NextSpawnTicks = s_wave.SpawnRateTicks;
        }

        private static void SpawnEnemy()
        {
            var direction = SPAWN_DISTANCE * Util.Random.NextUnitVector();
            var position = GameScene.Player.Position + direction;
            GameScene.AddEnemy(new(position));
        }
    }
}
