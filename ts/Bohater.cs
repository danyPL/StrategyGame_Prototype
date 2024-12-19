using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts
{
    public class Bohater
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public Bohater(string name, int attack, int defense)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
        }
    }
}
