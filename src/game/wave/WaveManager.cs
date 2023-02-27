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
        public static int[] NextSpawnTicks { get; private set; }

        public static float SpawnDistance => Display.WindowSize.ToVector2().Length() * 0.625f;
        public static int CurrentWave => s_wave.ID;

        private static Wave s_wave;

        public static void HandleInput()
        {
            if (Keybinds.WaveIncrease.PressedThisFrame)
                NextWave();
        }

        public static void Tick()
        {
            // check next wave
            if (--CurrentWaveTicks <= 0)
                NextWave();
            // decrement spawn ticks
            for (int i = 0; i < NextSpawnTicks.Length; i++)
            {
                if (--NextSpawnTicks[i] <= 0)
                {
                    NextSpawnTicks[i] = s_wave.WaveInfoArray[i].SpawnRateTicks;
                    SpawnEnemy(s_wave.WaveInfoArray[i]);
                }
            }
        }

        public static void Reset()
        {
            s_wave = Waves.FromID(0);
            CurrentWaveTicks = s_wave.WaveLengthTicks;
            ResetNextSpawnTicks();
        }

        private static void ResetNextSpawnTicks() => NextSpawnTicks = Util.PopulateArray(s_wave.WaveInfoArray.Length, i => s_wave.WaveInfoArray[i].SpawnRateTicks);

        private static void NextWave()
        {
            if (CurrentWave >= Waves.Amount - 1)
            {
                SceneManager.Scene = new GameEndScene();
                return;
            }
            s_wave = Waves.FromID(CurrentWave + 1);
            CurrentWaveTicks = s_wave.WaveLengthTicks;
            ResetNextSpawnTicks();
        }

        private static void SpawnEnemy(WaveInfo waveInfo)
        {
            var direction = Util.Random.NextUnitVector();
            var spawnOffset = SpawnDistance * direction;
            var position = GameScene.Player.Position + spawnOffset;
            var enemy = (AbstractEnemy)Activator.CreateInstance(waveInfo.EnemyType, position, waveInfo.EnemyHealth);
            GameScene.AddEnemy(enemy);
        }
    }
}
