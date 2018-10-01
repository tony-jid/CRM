using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public static class FileHelper
    {
        public static string RenameFile(string fileName, string newName)
        {
            return newName + Path.GetExtension(fileName);
        }

        public static FileStream OpenFile(string path)
        {
            try
            {
                if (File.Exists(path))
                    return File.Open(path, FileMode.Open);
                else
                    return null;
            }
            catch (Exception e)
            {
                // Directory path might not be found!
                return null;
            }
        }

        /// <summary>
        /// Copying file from a filestream to another
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveFile(FileStream fileStream, string path)
        {
            try
            {
                using (var _fileStream = File.Create(path))
                {
                    fileStream.CopyTo(_fileStream);
                }

                return true;
            }
            catch (Exception e)
            {
                // Directory path might not be found!
                return false;
            }
        }

        public static bool SaveFile(IFormFile formFile, string path)
        {
            try
            {
                using (var fileStream = File.Create(path))
                {
                    formFile.CopyTo(fileStream);
                }

                return true;
            }
            catch (Exception e)
            {
                // Directory path might not be found!
                return false;
            }
        }

        public static bool DeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                    File.Delete(path);

                return true;
            }
            catch (Exception e)
            {
                // Directory path might not be found!
                return false;
            }
        }
    }
}
