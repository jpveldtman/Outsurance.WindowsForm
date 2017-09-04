using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsurance.WindowsForm.Helpers
{
    public class Sorter
    {
        public static Dictionary<string, int> GetWordCountDescendingOrder(List<string> listOfPeople)
        {
            return listOfPeople.GroupBy(x => x)
                .OrderByDescending(x => x.Count())
                .ThenBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Count());
        }

        public static List<string> GetAsendingList(List<string> listOfPeople)
        {
            return listOfPeople.OrderBy(x => x).ToList();
        }

        public static List<string> GetAsendingListIgnoringNumbers(List<string> listOfPeople)
        {
            return listOfPeople.OrderBy(x => RemoveNumbers(x))
                .ToList<string>();
        }

        private static string RemoveNumbers(String text)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Char c in text)
                if (Char.IsLetter(c))
                    sb.Append(c);

            return sb.ToString();
        }
    }
}
