using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.Types;

namespace ts.Spells
{
    class RangedSpell : Spell
    {
        public RangedSpell()
        {
            TypeDmg = TypeDamage.Ranged;
        }
    }
}
