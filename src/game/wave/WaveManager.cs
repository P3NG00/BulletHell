using System;
using BulletHell.Game.Entities.Enemies;
using BulletHell.Input;
using BulletHell.Scenes;
using BulletHell.Utils;

namespace BulletHell.Game.Waves
{
    public static class WaveManager
    {
        public static int CurrentWaveTicks { get; private set; }
        public static int NextSpawnTicks { get; private set; }

        public static float SpawnDistance => Display.WindowSize.ToVector2().Length() * 0.625f;
        public static int CurrentWave => s_wave.ID;

        private static Wave s_wave;

        public static void Update()
        {
            if (Keybinds.WaveIncrease.PressedThisFrame)
                NextWave();
        }

        public static void Tick()
        {
            if (--CurrentWaveTicks <= 0)
                NextWave();
            if (--NextSpawnTicks <= 0)
            {
                NextSpawnTicks = s_wave.SpawnRateTicks; // TODO keep track of different enemy spawn ticks separately
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
            if (CurrentWave >= Waves.Amount - 1)
            {
                SceneManager.Scene = new GameEndScene();
                return;
            }
            s_wave = Waves.FromID(CurrentWave + 1);
            CurrentWaveTicks = s_wave.WaveLengthTicks;
        }

        private static void SpawnEnemy()
        {
            var direction = Util.Random.NextUnitVector();
            var spawnOffset = SpawnDistance * direction;
            var position = GameScene.Player.Position + spawnOffset;
            var (EnemyType, EnemyHealth) = s_wave.EnemyTypes.GetRandom(); // TODO keep track of each enemy spawn ticks separately instead of choosing a random entity to spawn
            var enemy = (AbstractEnemy)Activator.CreateInstance(EnemyType, position, EnemyHealth);
            GameScene.AddEnemy(enemy);
        }
    }
}
