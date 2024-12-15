

using ts.Types;

namespace ts
{
    class Program
    {
        static void Pojedynek()
        {

        }
        static void Main(string[] args)
        {
            Jednostka test = new Jednostka
            {
                Name = "danyPL",
                Type_dmg = Types.TypeDamage.Melle ,
                Damage_range = "7-12"
            };
            Jednostka test2 = new Jednostka
            {
                Name = "Capibara",
                Type_dmg = Types.TypeDamage.Magic,
                Damage_range = "5-10"
            };

            var spells = test.Avalible_Spells.ToArray();
            var spell = spells.FirstOrDefault(e => e.Id.Equals(1));
            Console.WriteLine(test.Damage_max);
            test.Pojedynek(test2);
        }
    }
}