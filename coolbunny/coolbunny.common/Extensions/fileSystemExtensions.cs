using System.IO;

namespace coolbunny.common.Extensions
{
    public static class fileSystemExtensions
    {
        public static void Empty(this DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles()) file.Delete();
            foreach (var subDirectory in directory.GetDirectories()) subDirectory.Empty();
        }
    }
}