using System.IO;

namespace BulletHell.Utils
{
    public static class Data
    {
        private const string SAVE_FILE = "save";

        public static bool SaveExists => File.Exists(SAVE_FILE);

        // TODO implement 'high score' saving for displaying when the game is over or from the homescreen

        public static void Save()
        {
            using (var stream = new BinaryWriter(File.Open(SAVE_FILE, FileMode.Create)))
            {
                // TODO
            }
        }

        public static void Load()
        {
            using (var stream = new BinaryReader(File.Open(SAVE_FILE, FileMode.Open)))
            {
                // TODO
            }
        }
    }
}
