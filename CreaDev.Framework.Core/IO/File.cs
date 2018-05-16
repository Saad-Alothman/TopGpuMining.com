using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CreaDev.Framework.Core.IO
{
    public static class File
    {
        public static bool IsPathMatchesFilePath(string path, string fileNameWithFullPath)
        {
            string requestedFileDirectoryName = System.IO.Path.GetDirectoryName(fileNameWithFullPath);
            string mediaDirectoryName = System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(path));
            return requestedFileDirectoryName == mediaDirectoryName;
        }
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

        private static void DeleteFile(string fullFileName)
        {
            System.IO.File.Delete(fullFileName);
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
        public static List<string> GetFilesType(List<string> filenames)
        {


            return filenames.Select(GetFileType).ToList();
        }
        public static List<string> GetFilesExtensions(HttpFileCollection files)
        {
            List<string> result = new List<string>();
            foreach (string file in files)
            {
                HttpPostedFile hpf = files[file];

                result.Add(GetFileType(hpf.FileName));
            }

            return result;
        }
        public static List<string> GetFilesExtensions(HttpFileCollectionBase files)
        {
            List<string> result = new List<string>();
            foreach (string file in files)
            {
                HttpPostedFileBase hpf = files[file];

                result.Add(GetFileExtension(hpf));
            }

            return result;
        }
        public static string GetFileExtension(HttpPostedFileBase file)
        {
            string result = string.Empty;
            
            result =GetFileType(file.FileName);

            return result;
        }

        public static List<string> GetNonListedFiles(string path, IEnumerable<string> fileNamesList)
        {
            string[] directoryFiles = Directory.GetFiles(path);
            List<string> nonListed = directoryFiles.Where(f => !fileNamesList.Contains(f)).ToList();
            return nonListed;
        }

        public static Dictionary<string, bool> TryDelete(List<string> nonListedFiles)
        {
            Dictionary<string, bool> filesResult = new Dictionary<string, bool>();

            foreach (var nonListedFile in nonListedFiles)
            {
                var isSuccess = TryDelete(nonListedFile);
                filesResult.Add(nonListedFile, isSuccess);
            }
            return filesResult;
        }

        public static bool TryDelete(string fileName)
        {
            bool isSuccess = true;
            try
            {
                System.IO.File.Delete(fileName);
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
        public static bool TryDelete(string path, string fileName)
        {
            bool isSuccess = true;
            try
            {
                if (!path.EndsWith("\\"))
                {
                    path += "\\";
                }
                System.IO.File.Delete(path + fileName);
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public static string SaveFile(byte[] fileBytes, string path, string extension,string fileName=null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = CreaDev.Framework.Core.IO.File.GetRandomFileName();
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }
            if (extension[0]!='.')
            {
                extension = "." + extension;
            }
            fileName = fileName + extension;
            System.IO.File.WriteAllBytes(path+fileName, fileBytes);

            return fileName;
        }
        public static string SaveFile(HttpPostedFileBase postedFile, string path, string extension, string fileName = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = GetRandomFileName();
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }
            if (extension[0] != '.')
            {
                extension = "." + extension;
            }
            fileName = fileName + extension;
            postedFile.SaveAs(path + fileName);
            return fileName;
        }

        public static string SaveFromDataUri(string dataUri, string path, bool createPathIfNotFound = true, string fileName = null)
        {
            var base64Data = Regex.Match(dataUri, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            string type = Regex.Match(dataUri, @"data:image/(?<type>.+?),(?<data>.+)").Groups["type"].Value.Replace(";base64", "");
            var binData = Convert.FromBase64String(base64Data);

            if (string.IsNullOrWhiteSpace(fileName))
                fileName = CreaDev.Framework.Core.IO.File.GetRandomFileName();

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

            using (FileStream stream = new FileStream(($"{path}{fileName}.{type}"), FileMode.Create))
            {
                stream.Write(binData, 0, binData.Length);
                stream.Flush();
            }
            return $"{fileName}.{type}";

        }


    }
}
