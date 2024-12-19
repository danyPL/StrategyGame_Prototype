using System;
using System.Collections.Generic;
using System.Linq;

namespace ts
{
    public class Armia
    {
        public string Name { get; set; }
        public List<Oddział> Oddzialy { get; set; }
        public Bohater? Dowodca { get; set; }

        public Armia(string name, List<Oddział> oddzialy, Bohater? dowodca = null)
        {
            Name = name;
            Oddzialy = oddzialy;
            Dowodca = dowodca;

            foreach (var oddzial in Oddzialy)
            {
                oddzial.ApplyCommanderBonus(Dowodca);
            }
        }

        public int MinDamage => Oddzialy.Sum(oddzial => oddzial.MinDamage);

        public int MaxDamage => Oddzialy.Sum(oddzial => oddzial.MaxDamage);

        public int TotalHealth => Oddzialy.Sum(oddzial => oddzial.TotalHealth);

        public bool IsDefeated => Oddzialy.All(oddzial => oddzial.IsDefeated);

        public int MakeManouver(int damageTaken, string army_name, string? army2_name)
        {
            Random rnd = new Random();
            int manewr = rnd.Next(0, 3);  

            switch (manewr)
            {
                case 0:
                    int damageBlocked = (int)Math.Floor(damageTaken * 0.15);
                    damageTaken -= damageBlocked;
                    Console.WriteLine($"Armia {army_name} wykonuje manewr defensywny, blokując {damageBlocked} obrażeń od armii {army2_name}");
                    return damageTaken;

                case 1:
                    int bonusDamage = (int)Math.Floor(damageTaken * 0.2);
                    damageTaken += bonusDamage;
                    Console.WriteLine($"Armia {army_name} wykonuje manewr ofensywny, zwiększając obrażenia o {bonusDamage}.");
                    return damageTaken;

                default:
                    Console.WriteLine($"Armia {army_name} nie wykonuje specjalnego manewru.");
                    return damageTaken;
            }
        }

        public void TakeDamage(int damage)
        {
            if (Oddzialy.Count == 0) return;

            int damagePerOddzial = damage / Oddzialy.Count;
            foreach (var oddzial in Oddzialy)
            {
                oddzial.TakeDamage(damagePerOddzial);
            }

            Oddzialy.RemoveAll(oddzial => oddzial.IsDefeated);
        }

        public override string ToString()
        {
            var oddzialyInfo = string.Join("\n", Oddzialy.Select(oddzial => oddzial.ToString()));
            return $"{Name}:\n{oddzialyInfo}\nCałkowite zdrowie: {TotalHealth}, obrażenia: {MinDamage}-{MaxDamage}";
        }

        public int DealDamage()
        {
            Random rnd = new Random();
            return rnd.Next(MinDamage, MaxDamage + 1);
        }
        /*
      * Ostatni podpunkt zadania polegał na stworzeniu tzw. Wojny Ostatecznej, czyli symulacji walki dwóch armii.
      * Myślę, że udało mi się wiernie odwzorować tę koncepcję. Zamiast ograniczać się do ogólnikowego przebiegu walk,
      * postanowiłem pójść na całość i stworzyć rzeczywistą symulację starć na każdym poziomie:
      * między poszczególnymi jednostkami, oddziałami, aż po rozstrzygnięcia na poziomie całych armii.
      * PS. Manewery Armii są dostępne jedynie w przypadku zwykłej wojny a nie Wojny Ostatecznej!
      */

        public void Wojna_Ostateczna(Armia armia_target)
        {
            Console.WriteLine("Rozpoczyna się wojna OSTATECZNA między armiami!");
            Console.WriteLine($"Armia {Name} vs Armia {armia_target.Name}");
            Console.WriteLine();

            int tura = 1;

            while (!IsDefeated && !armia_target.IsDefeated)
            {
                if (Oddzialy.Count > 0 && armia_target.Oddzialy.Count > 0)
                {
                    Console.WriteLine($"--- Tura {tura} ---");
                }

                for (int i = 0; i < Oddzialy.Count && i < armia_target.Oddzialy.Count; i++)
                {
                    Oddział oddziałA = Oddzialy[i];
                    Oddział oddziałB = armia_target.Oddzialy[i];

                    Console.WriteLine($"Bitwa między oddziałem {oddziałA.Name} i oddziałem {oddziałB.Name}");

                    oddziałA.Walka_Ostateczna(oddziałB);

                    if (oddziałB.IsDefeated)
                    {
                        armia_target.Oddzialy.RemoveAt(i);
                        break;
                    }
                    if (oddziałA.IsDefeated)
                    {
                        Oddzialy.RemoveAt(i);
                        break;
                    }
                }

                if (armia_target.Oddzialy.Count == 0)
                {
                    Console.WriteLine($"Wszystkie oddziały Armii {armia_target.Name} zostały pokonane!");
                    Console.WriteLine($"{Name} wygrywa wojnę ostateczna!");
                    Console.WriteLine($"Pozostałe oddziały {Oddzialy.Count}");
                    break;
                }

                if (Oddzialy.Count == 0)
                {
                    Console.WriteLine($"Wszystkie oddziały Armii {Name} zostały pokonane!");
                    Console.WriteLine($"{armia_target.Name} wygrywa wojnę ostateczna!");
                    Console.WriteLine($"Pozostałe oddziały {armia_target.Oddzialy.Count}");
                    break;
                }

                tura++;
            }
        }



        public void Wojna(Armia armia_target)
        {
            Console.WriteLine("Rozpoczyna się wojna między armiami!");
            Console.WriteLine(Name);
            Console.WriteLine(armia_target.Name);
            Console.WriteLine();

            int tura = 1;
            Random rnd = new Random();

            while (!IsDefeated && !armia_target.IsDefeated)
            {
                Console.WriteLine($"--- Tura {tura} ---");

                int damageArmia1 = DealDamage();
                int specialManouver_army1 = rnd.Next(0, 3);  

                if (specialManouver_army1 == 1)  // Troche wiecej losowości tf tf :> 
                    damageArmia1 = armia_target.MakeManouver(damageArmia1, armia_target.Name, Name);  
                Console.WriteLine($"{Name} zadaje {damageArmia1} obrażeń {armia_target.Name}.");
                armia_target.TakeDamage(damageArmia1);
                Console.WriteLine($"Pozostałe życie {Name}: {TotalHealth}HP");
                Console.WriteLine($"Pozostałe życie {armia_target.Name}: {armia_target.TotalHealth}HP");

                if (armia_target.IsDefeated)
                {
                    Console.WriteLine($"\n{armia_target.Name} została pokonana! {Name} wygrywa wojnę!");
                    Console.WriteLine($"Pozostałe oddziały {Oddzialy.Count}");

                    break;
                }

                int damageArmia2 = armia_target.DealDamage();
                int specialManouver_army2 = rnd.Next(0, 3);  

                if (specialManouver_army2 == 1)  
                {
                    damageArmia2 = MakeManouver(damageArmia2, Name, armia_target.Name);  
                }
                Console.WriteLine($"{armia_target.Name} zadaje {damageArmia2} obrażeń {Name}.");
                TakeDamage(damageArmia2);
                Console.WriteLine($"Pozostałe życie {Name}: {TotalHealth}HP");
                Console.WriteLine($"Pozostałe życie {armia_target.Name}: {armia_target.TotalHealth}HP");

                if (IsDefeated)
                {
                    Console.WriteLine($"\n{Name} została pokonana! {armia_target.Name} wygrywa wojnę!");
                    Console.WriteLine($"Pozostałe oddziały {armia_target.Oddzialy.Count}");
                    break;
                }

                tura++;
            }
        }
    }
}
