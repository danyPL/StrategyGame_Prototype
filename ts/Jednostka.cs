using System;
using System.Collections.Generic;
using ts.Types;

namespace ts
{
    public class Jednostka : ClassStats
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }

        public void Pojedynek(Jednostka target)
        {
            Console.WriteLine($"Rozpoczyna się pojedynek pomiędzy {Name} a {target.Name}!");
            Console.WriteLine($"{Name} ma {Health} HP, {target.Name} ma {target.Health} HP.\n");

            int tura = 1;

            while (Health > 0 && target.Health > 0)
            {
                Console.WriteLine($"Tura {tura}:");

                // Sprawdzenie szybkości i ustawienie kolejności ataków
                if (Speed >= target.Speed)
                {
                    Atack(target); // Obecna jednostka atakuje jako pierwsza
                    if (target.Health > 0) target.Atack(this); // Jeśli cel przeżył, atakuje w odpowiedzi
                }
                else
                {
                    target.Atack(this); // Cel atakuje jako pierwszy
                    if (Health > 0) Atack(target); // Jeśli obecna jednostka przeżyła, atakuje w odpowiedzi
                }

                Console.WriteLine(); // Oddzielenie tury wizualnie
                tura++;
            }

            // Wyświetlenie wyniku walki
            if (Health > 0)
            {
                Console.WriteLine($"{Name} wygrywa pojedynek z {target.Name}. Pozostało mu {Health} HP.");
            }
            else
            {
                Console.WriteLine($"{target.Name} wygrywa pojedynek z {Name}. Pozostało mu {target.Health} HP.");
            }
        }

        public void Atack(Jednostka target)
        {
            Random rnd = new Random();

            // Obrażenia atakującego
            int jednostkaDmg = rnd.Next(Damage_min, Damage_max + 1);
            int damageDealt = Math.Max(1, jednostkaDmg - target.Defense);

            // Obrażenia obrońcy
            int targetDmg = rnd.Next(target.Damage_min, target.Damage_max + 1);
            int damageTaken = Math.Max(1, targetDmg - Defense);

            // Losowa szansa na użycie zaklęcia
            int spellChance = rnd.Next(0, 2); // 0 = brak zaklęcia, 1 = zaklęcie użyte Każdy może ale nie musi użyć losowego zaklęcia dostępnego dla jego klasy :>
            if (spellChance == 1 && Avalible_Spells.Count > 0)
            {
                Spell jednostkaSpell = Avalible_Spells[rnd.Next(Avalible_Spells.Count)];
                damageDealt += jednostkaSpell.Damage;
                Console.WriteLine($"{Name} używa zaklęcia {jednostkaSpell.Name}, zadając dodatkowe {jednostkaSpell.Damage} obrażeń.");
            }

            if (spellChance == 1 && target.Avalible_Spells.Count > 0)
            {
                Spell targetSpell = target.Avalible_Spells[rnd.Next(target.Avalible_Spells.Count)];
                damageTaken += targetSpell.Damage;
                Console.WriteLine($"{target.Name} używa zaklęcia {targetSpell.Name}, zadając dodatkowe {targetSpell.Damage} obrażeń.");
            }

            // Aktualizacja punktów zdrowia
            target.Health -= damageDealt;
            Health -= damageTaken;

            // Wyświetlamy informacje o ataku :>
            Console.WriteLine($"{Name} zadaje {target.Name} obrażenia: {damageDealt}.");
            Console.WriteLine($"{target.Name} zadaje {Name} obrażenia: {damageTaken}.");
            Console.WriteLine($"{Name} ma teraz {Math.Max(0, Health)} zdrowia.");
            Console.WriteLine($"{target.Name} ma teraz {Math.Max(0, target.Health)} zdrowia.");
        }

        public Jednostka()
        {
            TypeDmg = TypeDamage.Ranged;
        }
    }
}
