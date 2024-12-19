using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ts.Types;

namespace ts
{
    public class Oddział
    {
        public string Name { get; set; }
        public Color FactionColor { get; set; }
        public Jednostka UnitType { get; set; }
        public int Count { get; set; }
        private Queue<int> UnitHealthQueue; // Przechowuje obecne zdrowie każdej jednostki w oddziale
        public List<Jednostka>? UnitList { get; set; }

        /*
         * Queue to struktura danych, 
         * która działa na zasadzie FIFO (First In, First Out),
         * co oznacza, że pierwszy element, który został dodany do kolejki, 
         * jest również pierwszym, który z niej zostanie usunięty.
         */

        public Oddział(string name, Color factionColor, Jednostka unitType, int count, List<Jednostka> unitList = null)
        {
            Name = name;
            FactionColor = factionColor;
            UnitType = unitType;

            if (unitList != null && unitList.Count > 0)
            {
                Count = unitList.Count;
                UnitHealthQueue = new Queue<int>(unitList.Select(u => u.Health)); 
                UnitList = unitList; 
            }
            else
            {
                Count = count;
                UnitHealthQueue = new Queue<int>(Enumerable.Repeat(UnitType.Health, Count)); // Działa jak dotychczas, gdy lista jednostek nie jest przekazana
            }
        }

        public int MinDamage => UnitType.Damage_min * Count;
        public int MaxDamage => UnitType.Damage_max * Count;
        public int TotalHealth => UnitHealthQueue.Sum();
        public bool IsDefeated => UnitHealthQueue.Count == 0;

        // Zwiększenie ataku i obrony jednostek, zależnie od dowódcy
        public void ApplyCommanderBonus(Bohater commander)
        {
            if (commander != null)
            {
                UnitType.Damage_min += commander.Attack;
                UnitType.Damage_max += commander.Attack;
                UnitType.Defense += commander.Defense;
            }
        }

        /// <summary>
        /// Oddział otrzymuje obrażenia, zdrowie traci jednostka na przedzie kolejki.
        /// Jeśli obrażenia są większe niż zdrowie, przechodzą na kolejną jednostkę.
        /// </summary>
        public void TakeDamage(int damage)
        {
            while (damage > 0 && UnitHealthQueue.Count > 0)
            {
                int currentHealth = UnitHealthQueue.Dequeue();
                if (damage >= currentHealth)
                {
                    damage -= currentHealth;
                }
                else
                {
                    UnitHealthQueue.Enqueue(currentHealth - damage);
                    damage = 0;
                }
            }
            Count = UnitHealthQueue.Count; 
        }

        public int DealDamage()
        {
            Random rnd = new Random();
            return rnd.Next(MinDamage, MaxDamage + 1);
        }

        public override string ToString()
        {
            return $"{Name}: {Count} jednostek, {TotalHealth} HP, obrażenia: {MinDamage}-{MaxDamage}";
        }

        public void Walka_Ostateczna(Oddział oddziałB)
        {
            Console.WriteLine($"Rozpoczyna się bitwa między oddziałami {Name} i {oddziałB.Name}!\n");

            int tura = 1;
            Random rnd = new Random();

            while (!IsDefeated && !oddziałB.IsDefeated)
            {
                if (UnitList.Count > 0 && oddziałB.UnitList.Count > 0)
                {
                    Console.WriteLine($"--- Tura {tura} ---");
                }

                for (int i = 0; i < Math.Min(UnitList.Count, oddziałB.UnitList.Count); i++)
                {
                    int randomIndexA = rnd.Next(UnitList.Count);
                    int randomIndexB = rnd.Next(oddziałB.UnitList.Count);

                    Jednostka jednostkaA = UnitList[randomIndexA];
                    Jednostka jednostkaB = oddziałB.UnitList[randomIndexB];

                    if (!(jednostkaA.Health <= 0 || jednostkaB.Health <= 0))
                    {
                        jednostkaA.Pojedynek(jednostkaB, true, tura);
                    }
                }

                oddziałB.TakeDamage(DealDamage());
                Console.WriteLine($"{oddziałB.Name}: pozostałe jednostki: {oddziałB.Count}, HP: {oddziałB.TotalHealth}");

                if (oddziałB.IsDefeated)
                {
                    Console.WriteLine($"{oddziałB.Name} został pokonany! {Name} wygrywa bitwę!");
                    break;
                }

                TakeDamage(oddziałB.DealDamage());
                Console.WriteLine($"{Name}: pozostałe jednostki: {Count}, HP: {TotalHealth}");

                if (IsDefeated)
                {
                    Console.WriteLine($"{Name} został pokonany! {oddziałB.Name} wygrywa bitwę!");
                    break;
                }

                tura++;
            }
        }



        public void Walka(Oddział oddziałB)
        {
            Console.WriteLine($"Rozpoczyna się bitwa między oddziałami {Name} i {oddziałB.Name}!\n");

            int tura = 1;
            while (!IsDefeated && !oddziałB.IsDefeated)
            {
                Console.WriteLine($"--- Tura {tura} ---");

                int damageA = DealDamage();
                Console.WriteLine($"{Name} zadaje {damageA} obrażeń.");
                oddziałB.TakeDamage(damageA);
                Console.WriteLine($"{oddziałB.Name}: pozostałe jednostki: {oddziałB.Count}, HP: {oddziałB.TotalHealth}");

                if (oddziałB.IsDefeated)
                {
                    Console.WriteLine($"\n{oddziałB.Name} został pokonany! {Name} wygrywa bitwę!");
                    break;
                }

                int damageB = oddziałB.DealDamage();
                Console.WriteLine($"{oddziałB.Name} zadaje {damageB} obrażeń.");
                TakeDamage(damageB);
                Console.WriteLine($"{Name}: pozostałe jednostki: {Count}, HP: {TotalHealth}");

                if (IsDefeated)
                {
                    Console.WriteLine($"\n{Name} został pokonany! {oddziałB.Name} wygrywa bitwę!");
                    break;
                }

                tura++;
            }
        }
    }
}
