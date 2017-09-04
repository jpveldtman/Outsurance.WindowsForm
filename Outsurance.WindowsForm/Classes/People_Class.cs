using Outsurance.WindowsForm.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Outsurance.WindowsForm.Classes
{
    public static class People_Class
    {
        private static List<Person_Class> _peopleList = new List<Person_Class>();

        public static List<Person_Class> ReadAllPeople(string file, out string error)
        {
            error = null;
            try
            {
                _peopleList = FileAccess.ReadAllLines(file)
                    .Skip(1)
                    .Select(x => x.Split(','))
                    .Select(x => AddPersontoList(x[0], x[1], x[2])).ToList();
            }
            catch (IndexOutOfRangeException ex)
            {
                error = ex.Message;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return _peopleList;
        }
        private static Person_Class AddPersontoList(string name, string surname, string address)
        {
            return new Person_Class()
            {
                Name = name,
                Surname = surname,
                Address = address
            };
        }
        public static List<string> GetFullNameList()
        {
            return _peopleList.Select(x => x.Name + " " + x.Surname).ToList();
        }
        public static List<string> GetAddressList()
        {
            return _peopleList.Select(x => x.Address).Distinct().ToList();
        }


    }
}
