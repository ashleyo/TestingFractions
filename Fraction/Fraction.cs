using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionLib
{
    public struct Fraction : IEquatable<Fraction>
    {
        //ADDED specific Exception types
        public class FractionException : NotFiniteNumberException
        {
            public FractionException() : base("denominator of a fraction may not be zero") { }
        }

        //ADDED static values for unity and zero - mostly to ensure that these values are unique
        public static readonly Fraction Zero = new Fraction(0, 1);
        public static readonly Fraction Unity = new Fraction(1, 1);

        public int numerator;
        public int denominator;

        /*ADDED factory method to create fractions - prompted by the desire to have a unique zero,
         * this allows e.g. Fraction.Create(0,99) to return Fraction.Zero which would be impossible to do (other 
         * than by value) using a constructor
         */
        public static Fraction Create(int numerator, int denominator = 1)
        {
            if (denominator == 0) throw new FractionException();
            if (denominator < 0) {numerator = -numerator; denominator = -denominator;  } //ensure denominator is +ve
            if (numerator == 0) return Zero; 
            return new Fraction(numerator, denominator);
        }
        
        /*ADDED as a consequence make the constructor private - then go through the code and replace 'new Fraction(...'
         * with 'Fraction.Create(...'
         */
        private Fraction(int numerator, int denominator=1)
        {   
            this.numerator = numerator;
            this.denominator = denominator;
        }

        //ADDED allow assigning an int to a fraction via an implicit cast operator
        public static implicit operator Fraction(int theInt)
        {
            return Fraction.Create(theInt, 1);
        }

        //copy constructor
        public Fraction(Fraction theFraction)
        {
            numerator = theFraction.numerator;
            denominator = theFraction.denominator;
        }

        public override string ToString() => $"{numerator}/{denominator}";

        public static Fraction Multiply(Fraction F, Fraction G)
            => Fraction.Create(F.numerator * G.numerator, F.denominator * G.denominator);
        public static Fraction operator *(Fraction F, Fraction G) => Multiply(F, G);
            
        public static Fraction Reciprocal(Fraction F) => Fraction.Create(F.denominator, F.numerator);
        public static Fraction Negate(Fraction F) => Fraction.Create(-F.numerator, F.denominator);
        public static Fraction operator -(Fraction F) => Negate(F);

        public static Fraction Divide(Fraction F, Fraction G) => Multiply(F, Reciprocal(G));
        public static Fraction operator /(Fraction F, Fraction G) => Divide(F, G);


        public static Fraction Add(Fraction F, Fraction G)
            => Fraction.Create(F.numerator * G.denominator + F.denominator * G.numerator,
                            F.denominator * G.denominator);
        public static Fraction operator +(Fraction F, Fraction G) => Add(F, G);

        public static Fraction Subtract(Fraction F, Fraction G) => Add(F, -G);
        public static Fraction operator -(Fraction F, Fraction G) => Subtract(F, G);


        public static Fraction Reduce(Fraction F)
        {
            int numerator = F.numerator;
            int denominator = F.denominator;
            int gcd = Utility.GCD(numerator, denominator);
            numerator /= gcd;
            denominator /= gcd;

            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }
            return Fraction.Create(numerator, denominator);
        }
        /*
        public static Fraction Reduce_old(Fraction F)
        {
            List<int> numexp = Utility.GetPrimeFactorExpansion(Math.Abs(F.numerator));
            List<int> denexp = Utility.GetPrimeFactorExpansion(F.denominator);


            // Don't use either of these to loop over because you can't modify 
            // things you are looping over - it causes a runtime error. Use a copy instead.
            foreach (var v in Utility.GetPrimeFactorExpansion(F.denominator))
            {
                if (denexp.Contains(v))
                {
                    denexp.Remove(v);
                    numexp.Remove(v);
                }
            }

            int newnum = 1, newden = 1;

            //multiply out lists of factors to get back single int, eg [2,2,3] => 12
            foreach (var v in numexp) newnum *= v; //newnum = newnum * v;
            foreach (var v in denexp) newden *= v;

            return Fraction.Create(Math.Sign(F.numerator)*newnum, newden);
        }
        */

        public static bool operator ==(Fraction F, Fraction G) => F.CompareTo(G) == 0;
        public static bool operator !=(Fraction F, Fraction G) => !(F == G);

        //ADDED overrides for comparison operators that delegate to CompareTo
        public static bool operator >(Fraction F, Fraction G) => F.CompareTo(G) > 0;
        public static bool operator <(Fraction F, Fraction G) => F.CompareTo(G) < 0;
        public static bool operator >=(Fraction F, Fraction G) => F.CompareTo(G) >= 0;
        public static bool operator <=(Fraction F, Fraction G) => F.CompareTo(G) <= 0;

        public int CompareTo(Fraction rhs)
            => (this.numerator * rhs.denominator).CompareTo(this.denominator * rhs.numerator);

        //ADDED Equals and GetHashcode using compiler inserts
        public override bool Equals(object? obj) => obj is Fraction && Equals((Fraction)obj);
        public bool Equals(Fraction other) => this.CompareTo(other) == 0;       

        public override int GetHashCode()
        {
            Fraction temp = Reduce(this);
            return HashCode.Combine(temp.numerator, temp.denominator);
        }
    }

    public static class Utility
    {
        public static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
        return Math.Abs(a);
        }

        public static List<int> GetPrimeFactorExpansion(int n)
        {
            if (n <= 0) throw new ArgumentException();

            List<int> theList = new List<int>();

            foreach (var v in Primes.primes)
            {
                while (n % v == 0)
                {
                    theList.Add(v);
                    n = n / v;
                }
                if (n == 1) break;
            }

            return theList;
        }      
    }
}
