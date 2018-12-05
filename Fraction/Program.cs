using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FractionLib.Fraction;

namespace FractionLib
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(Reduce(Fraction.Create(512, 64)));
            Console.WriteLine($"Should be 1/1 {Reduce(Fraction.Create(4, 4))}");
            //Console.WriteLine($"3/4 is less than 5/7  is {F.CompareTo(G) == -1}");
            //Console.WriteLine($"3/4 is more than 5/7  is {G.CompareTo(F) == -1}");
            //Console.WriteLine($"3/4 is same as 6/8  is {F.CompareTo(Fraction.Create(6,8)) == 0}");
            //Console.WriteLine($"3/4 is same as 6/8  is {F == Fraction.Create(6, 8)}");
            Console.WriteLine($"{Multiply(Fraction.Zero, Fraction.Create(1,2))}");
            Console.ReadKey();
        }
    }   
}
