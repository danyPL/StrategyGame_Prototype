using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ts.Spells;

namespace ts.Types
{
    public abstract class ClassStats:Spell
    {
        string damage_range;
        int damage_max;
        int health;
        int defense;
        int damage_min;
        TypeDamage td;
        public List<Spell> avaible_spells;
        public string Damage_range
        {
            get { return damage_range; }
            set
            {
                damage_range = value;
                string[] splited = damage_range.Split("-");
                int dmg_min = Convert.ToInt32(splited[0]);
                int dmg_max = Convert.ToInt32(splited[1]);
                if (dmg_max <= 0) damage_max = 1;
                if(dmg_min <= 0) damage_min = 1;
                damage_max = dmg_max;
                damage_min = dmg_min;
            }
        }
        protected ClassStats()
        {
            avaible_spells = new List<Spell>();
            Health = 100;

        }
        public TypeDamage Type_dmg
        {
            get { return td; }
            set
            {
                td = value;
                Avalible_Spells.Clear();
                switch (td)
                {
                    case TypeDamage.Ranged:
                        Damage_min = 5;
                        Damage_max = 8;
                        Defense = 3;
                        Avalible_Spells.Add(new RangedSpell
                        {
                            Id=0,
                            Name = "Piercing Arrow",
                            Description = "A powerful arrow that pierces through enemies.",
                            Damage = 7,
                            Effect = "Pierces through multiple enemies"
                        });
                        Avalible_Spells.Add(new RangedSpell
                        {
                            Id=1,
                            Name = "Explosive Shot",
                            Description = "An arrow that explodes on impact.",
                            Damage = 10,
                            Effect = "Deals area damage"
                        });
                        break;

                    case TypeDamage.Magic:
                        Damage_min = 6;
                        Damage_max = 10;
                        Defense = 2;
                        Avalible_Spells.Add(new MagicSpell
                        {
                            Id=0,
                            Name = "Fireball",
                            Description = "A ball of fire that burns enemies.",
                            Damage = 12,
                            Effect = "Deals burning damage over time"
                        });
                        Avalible_Spells.Add(new MagicSpell
                        {
                            Id=1,
                            Name = "Ice Shard",
                            Description = "A shard of ice that slows enemies.",
                            Damage = 8,
                            Effect = "Reduces enemy movement speed"
                        });
                        break;

                    case TypeDamage.Melle:
                        Damage_min = 5;
                        Damage_max = 7;
                        Defense = 4;
                        Avalible_Spells.Add(new WarriorSpell
                        {
                            Id=0,
                            Name = "Power Strike",
                            Description = "A strong attack that deals massive damage.",
                            Damage = 10,
                            Effect = "Ignores part of enemy defense"
                        });
                        Avalible_Spells.Add(new WarriorSpell
                        {
                            Id=1,
                            Name = "Shield Bash",
                            Description = "An attack that stuns the enemy.",
                            Damage = 6,
                            Effect = "Stuns the target for 2 seconds"
                        });
                        break;

                    case TypeDamage.TrueMelle:
                        Damage_min = 4;
                        Damage_max = 6;
                        Defense = 5;
                        Avalible_Spells.Add(new WarriorSpell
                        {
                            Id=0,
                            Name = "Whirlwind",
                            Description = "A spinning attack that hits all nearby enemies.",
                            Damage = 9,
                            Effect = "Deals area damage"
                        });
                        Avalible_Spells.Add(new WarriorSpell
                        {
                            Id=1,
                            Name = "Berserk",
                            Description = "Increases attack speed for a short duration.",
                            Damage = 5,
                            Effect = "Boosts attack speed"
                        });
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
    
        }
        public int Damage_max { get { return damage_max; } set { damage_max = value; } }
        public int Damage_min { get { return damage_min; } set { damage_min = value; } }

        public int Defense { get { return defense; } set {  defense = value; } }
        public int Health { get { return health; } set { health = value; } }
        public List<Spell> Avalible_Spells { get { return avaible_spells; } set { avaible_spells = value; } }
    }   
}
