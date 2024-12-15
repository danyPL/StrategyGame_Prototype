using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ts.Types
{
    public abstract class Spell
    {
        public int Id { get; set; }
       public string Name { get; set; }
        public string Description { get; set; }
        public TypeDamage TypeDmg { get; set; }
        public int Damage {  get; set; }
        public string Effect {  get; set; } // Efekt w postaci wypisanego tekstu np coś wybucha itp
        protected Spell() {
            Damage = 0;
            TypeDmg = TypeDamage.Magic;
            Name = "";
            Description = "";
            Effect = "";
        }
    }
}
