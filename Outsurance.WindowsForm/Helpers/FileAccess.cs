using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Outsurance.WindowsForm.Helpers
{
    public static class FileAccess
    {
        /// <summary>
        /// Read all the file names into a string array
        /// </summary>
        /// <param name="file">Contains the full path of the file</param>
        /// <returns></returns>
        public static string[] ReadAllLines(string file)
        {
            return File.ReadAllLines(file);
        }

        /// <summary>
        /// Writes the result to a file
        /// </summary>
        /// <param name="file">Contains the full path of the file</param>
        /// <param name="content"></param>
        public static void WriteAllLines(string file, IEnumerable<string> content, out string error)
        {
            error = null;
            try
            {
                File.WriteAllLines(file, content);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

        }
        /// <summary>
        /// Confirms the file headings are Name,Surname,Address
        /// </summary>
        /// <param name="file">Contains the full path of the file</param>
        /// <returns></returns>
        public static bool ValidateFormat(string file)
        {
            string headers = File.ReadLines(file).First();
            return headers == "Name,Surname,Address";
        }

    }
}
