using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.Types
{
    abstract class ClassStats:BasicModifers
    {
        string damage_range;
        int damage_max;
        int damage_min;
        TypeDamage td;
        public string Damage_range
        {
            get { return damage_range; }
            set
            {
                damage_range = value;
                string[] splited = damage_range.Split("-");
                int dmg_min = Convert.ToInt32(splited[0]);
                int dmg_max = Convert.ToInt32(splited[1]);
                
                damage_max = dmg_max;
                damage_min = dmg_min;
            }
        }
        public TypeDamage Type_dmg { get { return td; } 
            set {
                switch (td)
                {
                    case TypeDamage.Ranged:
                        damage_min = 0;
                        damage_max = 0;
                        break;
                    case TypeDamage.Magic:
                        damage_min = 1;
                        damage_max = 1;
                        break;
                    case TypeDamage.Melle:
                    damage_min = 2;
                    damage_max = 2;
                        break;
                    case TypeDamage.TrueMelle:
                        damage_min = 3;
                        damage_max = 3;
                        break;
                    
                            
                }
            }
        }
        public int Damage_max { get { return damage_max; } set { damage_max = value; } }
        public int Damage_min { get { return damage_min; } set { damage_min = value; } }

        
    }   
}
