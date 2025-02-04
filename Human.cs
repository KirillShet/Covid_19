using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid_19
{
    public class Human
    {
        public DateTime Date { get; set; }
        public int Susceptible { get; set; }
        public int Infected { get; set; }
        public int Removed { get; set; }
    }
}
