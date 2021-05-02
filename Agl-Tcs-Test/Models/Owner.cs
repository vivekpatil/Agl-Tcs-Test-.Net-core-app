using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agl_Tcs_Test.Models
{
    public class Owner
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public List<Pet> Pets= new List<Pet>();
    }
}
