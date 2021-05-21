using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CreaDev.Framework.Core.Helpers
{
    public static class FileHelper
    {

        public static string GetRandomFileName(bool removeExtension = true)
        {
            string result = Path.GetRandomFileName();
            if (removeExtension)
            {
                int indexLastDot = result.LastIndexOf('.');
                if (indexLastDot > -1)
                {
                    result = result.Remove(indexLastDot, result.Length - (indexLastDot));

                }
            }
            return result;
        }
        public static string SaveFromDataUri(string dataUri, string path, bool createPathIfNotFound = true, string fileName = null)
        {
            var base64Data = Regex.Match(dataUri, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            string type = Regex.Match(dataUri, @"data:image/(?<type>.+?),(?<data>.+)").Groups["type"].Value.Replace(";base64", "");
            var binData = Convert.FromBase64String(base64Data);

            if (string.IsNullOrWhiteSpace(fileName))
                fileName = GetRandomFileName();

            if (!path.EndsWith("\\"))
            {
                path = path + "\\";
            }

            bool pathExists = System.IO.Directory.Exists(path);
            if (!pathExists)
            {
                if (!createPathIfNotFound)
                    throw new DirectoryNotFoundException($"path :{path} does not exist");

                System.IO.Directory.CreateDirectory(path);
            }

            CreateFile(path, fileName, type, binData);
            return $"{fileName}.{type}";

        }

        private static void CreateFile(string path, string fileName, string type, byte[] binData)
        {
            using (FileStream stream = new FileStream(($"{path}{fileName}.{type}"), FileMode.Create))
            {
                stream.Write(binData, 0, binData.Length);
                stream.Flush();
            }
        }

        public static bool TryDelete(string path, string name)
        {
            bool isSuccess = true;
            if (!path.EndsWith("\\"))
            {
                path = path + "\\";
            }
            try
            {
                System.IO.File.Delete(path + name);
            }
            catch (Exception ex)
            {
                isSuccess = false;


            }
            return isSuccess;
        }
        public static string GetFileType(string filename)
        {
            string fileType = null;

            if (!String.IsNullOrEmpty(filename))
            {
                int indexLastDot = filename.LastIndexOf('.');

                if (indexLastDot < 0)
                    return fileType;

                fileType = filename.Substring(indexLastDot + 1, filename.Length - (indexLastDot + 1));
            }
            return fileType;
        }
    }
}