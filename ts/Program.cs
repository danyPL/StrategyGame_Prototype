

using System.Drawing;
using ts.Types;

namespace ts
{
    class Program
    {
      
        static void Main(string[] args)
        {
            /*
             * Witam, ogólnie trochę rozwinąłem pana wizje co do tej gry turowej, i zostały dodane dodatkowe elementy:
             * - Osobiste Spelle dla kazdej klasy jednostki majace rozne efekty oczywiscie losowe :>>
             * - Unikalne statystki dla roznych klas typu attackspeed, defense itp
             * - Oddziały zawierające nie tylko dane jednostki, statystki i system walki(rowniez ten do wojny_ostatecznej) ale takze bonus 
             *   zalezny od tego czy znajduje sie w nim dany Bohater. 
             *   Ogólnie sam system kolejek który został zaimplementowany w klasie Oddział był zaproponowany mi przez 
             *   ChatGPT(Chciałem zrobić bardziej skomplikowaną metodą z Listą ale mnie od tego odwiódł) i dzięki temu poznałem klase Queue
             *   oczywiście zaponzałem się później z dokumentacja i tutorialami i zaimplementowałem własną wersje kodu :>
             * - Armia która posiada osobisty system walk, obrażeń, wojny jak i system losowych manewrów wykonywanych przez oddziały danej armii.
             * - Również została zaimplementowana Wojna Ostateczna
             */
            Jednostka test = new Jednostka
            {
                Name = "danyPL",
                Type_dmg = ts.Types.TypeDamage.Melle,
                Damage_min = 7,
                Damage_max = 12,
                Health = 50
            };
            Jednostka test2 = new Jednostka
            {
                Name = "Capibara",
                Type_dmg = ts.Types.TypeDamage.TrueMelle,
                Damage_min = 7,
                Damage_max = 12,
                Health = 50
            };
            Jednostka test3 = new Jednostka
            {
                Name = "Nieznajomy",
                Type_dmg = ts.Types.TypeDamage.Melle,
                Damage_min = 7,
                Damage_max = 12,
                Health = 50
            };
            Jednostka test4 = new Jednostka
            {
                Name = "Strzelec Wyborowy",
                Type_dmg = ts.Types.TypeDamage.Ranged,
                Damage_min = 7,
                Damage_max = 12,
                Health = 50
            };


            List<Jednostka> jednostki = new List<Jednostka>();
            List<Jednostka> jednostki2 = new List<Jednostka>();

            jednostki.Add(test);
            jednostki.Add(test2);

            jednostki2.Add(test3);
            jednostki2.Add(test4);
            Oddział oddział1 = new Oddział("Oddzial Gamma 2", Color.Green, test, jednostki.Count,jednostki);
            Oddział oddział2 = new Oddział("Oddzial Gamma 3", Color.Blue, test2, jednostki2.Count,jednostki2);

            List<Oddział> oddzialy1 = new List<Oddział> { oddział1 };
            List<Oddział> oddzialy2 = new List<Oddział> { oddział2 };

            // Tutaj tworzymy armie :)
            Armia armia1 = new Armia("Alfa", oddzialy1);
            Armia armia2 = new Armia("Beta", oddzialy2);
            Bohater dowodca_Alfa = new Bohater("Dowódca Alfa", 10, 8);
            Bohater dowodca_Beta = new Bohater("Dowódca Beta", 8, 10);
            armia1.Dowodca = dowodca_Alfa;
            armia2.Dowodca = dowodca_Beta;
            Console.WriteLine("Stan armii przed wojną:");
            Console.WriteLine(armia1);
            Console.WriteLine(armia2);
            Console.WriteLine();

            // Symulacja wojny
            // armia1.Wojna(armia2); zwykła wojna
            armia1.Wojna_Ostateczna(armia2); 

        }
    }
}