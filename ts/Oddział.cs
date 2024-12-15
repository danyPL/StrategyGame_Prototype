using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
        /*
         * Queue to struktura danych, 
         * która działa na zasadzie FIFO (First In, First Out),
         * co oznacza, że pierwszy element, który został dodany do kolejki, 
         * jest również pierwszym, który z niej zostanie usunięty.
         */
        public Oddział(string name, Color factionColor, Jednostka unitType, int count)
        {
            Name = name;
            FactionColor = factionColor;
            UnitType = unitType;
            Count = count;
            UnitHealthQueue = new Queue<int>(Enumerable.Repeat(UnitType.Health, Count)); // dopytać krzysia bo do końca nwm co chodzi
        }

        public int MinDamage => UnitType.Damage_min * Count;
        public int MaxDamage => UnitType.Damage_max * Count;
        public int TotalHealth => UnitHealthQueue.Sum(); 
        public bool IsDefeated => UnitHealthQueue.Count == 0; 

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
                    damage -= currentHealth; // Jednostka ginie
                }
                else
                {
                    UnitHealthQueue.Enqueue(currentHealth - damage); 
                    damage = 0; 
                }
            }
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
        public static void Walka(Oddział oddziałA, Oddział oddziałB)
        {
            Console.WriteLine($"Rozpoczyna się bitwa między oddziałami {oddziałA.Name} i {oddziałB.Name}!\n");

            int tura = 1;
            while (!oddziałA.IsDefeated && !oddziałB.IsDefeated)
            {
                Console.WriteLine($"--- Tura {tura} ---");

                // Oddział A atakuje oddział B
                int damageA = oddziałA.DealDamage();
                Console.WriteLine($"{oddziałA.Name} zadaje {damageA} obrażeń.");
                oddziałB.TakeDamage(damageA);
                Console.WriteLine($"{oddziałB.Name}: pozostałe jednostki: {oddziałB.Count}, HP: {oddziałB.TotalHealth}");

                if (oddziałB.IsDefeated)
                {
                    Console.WriteLine($"\n{oddziałB.Name} został pokonany! {oddziałA.Name} wygrywa bitwę!");
                    break;
                }

                // Oddział B atakuje oddział A
                int damageB = oddziałB.DealDamage();
                Console.WriteLine($"{oddziałB.Name} zadaje {damageB} obrażeń.");
                oddziałA.TakeDamage(damageB);
                Console.WriteLine($"{oddziałA.Name}: pozostałe jednostki: {oddziałA.Count}, HP: {oddziałA.TotalHealth}");

                if (oddziałA.IsDefeated)
                {
                    Console.WriteLine($"\n{oddziałA.Name} został pokonany! {oddziałB.Name} wygrywa bitwę!");
                    break;
                }

                tura++;
            }
        }
    }

   
}
