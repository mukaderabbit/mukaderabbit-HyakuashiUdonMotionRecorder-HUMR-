using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Humr.Editor
{
    public static class PathUtils
    {
        public static void CreateDirectoryIfNotExist(string path)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }

        public static string SanitizeFileName(string input)
        {
            var sanitized = input;
            foreach (var c in Path.GetInvalidFileNameChars()) sanitized = sanitized.Replace(c, '_');
            return sanitized;
        }

        public static string GetBaseAnimationName(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var match = Regex.Match(fileName, @"\d{4}-\d{2}-\d{2}_\d{2}-\d{2}-\d{2}");

            return match.Success ? match.Value : fileName;
        }
    }
}