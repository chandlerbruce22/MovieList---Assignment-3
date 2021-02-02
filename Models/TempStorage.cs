using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Models
{
    public class TempStorage
    {
        private static List<AddMovie> applications = new List<AddMovie>();

        public static IEnumerable<AddMovie> Applications => applications;

        public static void AddApplication(AddMovie application)
        {
            applications.Add(application);
        }
    }
}
