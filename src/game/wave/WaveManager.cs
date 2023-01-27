using BulletHell.Game.Entities;
using BulletHell.Input;
using BulletHell.Scenes;
using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public static class WaveManager
    {
        public static int CurrentWaveTicks { get; private set; }
        public static int NextSpawnTicks { get; private set; }

        public static float SpawnDistance => Display.WindowSize.ToVector2().Length();
        public static float EnemyHealth => s_wave.EnemyHealth;
        public static int CurrentWave => s_wave.ID;

        private static bool CanSpawn => NextSpawnTicks <= 0;

        private static Wave s_wave;

        public static void Update()
        {
            if (Keybinds.WaveIncrease.PressedThisFrame)
                NextWave();
        }

        public static void Tick()
        {
            CurrentWaveTicks--;
            if (CurrentWaveTicks <= 0)
                NextWave();
            NextSpawnTicks--;
            if (CanSpawn)
            {
                NextSpawnTicks = s_wave.SpawnRateTicks;
                SpawnEnemy();
            }
        }

        public static void Reset()
        {
            s_wave = Waves.FromID(0);
            CurrentWaveTicks = s_wave.WaveLengthTicks;
            NextSpawnTicks = 0;
        }

        private static void NextWave()
        {
            if (CurrentWave < Waves.Amount - 1)
            {
                s_wave = Waves.FromID(CurrentWave + 1);
                CurrentWaveTicks = s_wave.WaveLengthTicks;
                return;
            }
            SceneManager.Scene = new GameEndScene();
            GameScene.NullifySingleton();
        }

        private static void SpawnEnemy()
        {
            var direction = Util.Random.NextUnitVector();
            var spawnOffset = SpawnDistance * direction;
            var position = GameScene.Player.Position + spawnOffset;
            new Enemy(position, EnemyHealth);
        }
    }
}
