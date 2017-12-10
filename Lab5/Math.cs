using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class Math
    {
        //
        // Returns x, y, gcd
        // Where FirstNumber * x + SecondNumber * y = gcd
        //
        public static Tuple<Int64, Int64, Int64> ExtendedEuclideanAlgorithm(Int64 FirstNumber, Int64 SecondNumber)
        {
            if (FirstNumber == 0)
            {
                return new Tuple<Int64, Int64, Int64>(0, 1, SecondNumber);
            }

            var result = ExtendedEuclideanAlgorithm(SecondNumber % FirstNumber, FirstNumber);
            var x1 = result.Item2 - (SecondNumber / FirstNumber) * result.Item1;
            var y1 = result.Item1;

            return new Tuple<Int64, Int64, Int64>(x1, y1, result.Item3);
        }

        public static Int64 EuclidGcd(Int64 FirstNumber, Int64 SecondNumber)
        {
            return ExtendedEuclideanAlgorithm(FirstNumber, SecondNumber).Item3;
        }

        public static Int64 ModularInverse(Int64 Number, Int64 Mod)
        {
            return ExtendedEuclideanAlgorithm(Number, Mod).Item1;
        }

        public static Int64 Pow(Int64 Base, Int64 Power)
        {
            if (Power == 0)
            {
                return 1;
            }

            if (Power == 1)
            {
                return Base;
            }

            var result = Pow(Base * Base, Power / 2);
            if (Power % 2 == 1)
            {
                result *= Base;
            }

            return result;
        }

        public static Int64 Pow(Int64 Base, Int64 Power, Int64 Mod)
        {
            if (Power == 0)
            {
                return 1;
            }

            if (Power == 1)
            {
                return Base % Mod;
            }

            var result = Pow(((Base % Mod) * (Base % Mod)) % Mod, Power / 2, Mod);
            if (Power % 2 == 1)
            {
                result = ((result % Mod) * (Base % Mod)) % Mod;
            }

            return result % Mod;
        }

        public static Int64 Phi(Int64 FirstPrime, Int64 SecondPrime)
        {
            return (FirstPrime - 1) * (SecondPrime - 1);
        }
    }
}
