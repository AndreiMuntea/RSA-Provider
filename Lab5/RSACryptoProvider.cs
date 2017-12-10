using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class RSACryptoProvider
    {
        private Int64 EncryptionExponent;
        private Int64 DecryptionExponent;
        private Int64 FirstPrime;
        private Int64 SecondPrime;
        private Int64 Modulus;
        private Int64 PhiN;
        private Int32 CypherTextBlockSize; // l 
        private Int32 PlainTextBlockSize; // k

        public RSACryptoProvider(Int64 FirstPrime, Int64 SecondPrime, Int64 EncryptionExponent, Int32 PlainTextBlockSize, Int32 CypherTextBlockSize)
        {
            this.FirstPrime = FirstPrime;
            this.SecondPrime = SecondPrime;
            this.EncryptionExponent = EncryptionExponent;
            this.PlainTextBlockSize = PlainTextBlockSize;
            this.CypherTextBlockSize = CypherTextBlockSize;
            this.Modulus = FirstPrime * SecondPrime;
            this.PhiN = Math.Phi(FirstPrime, SecondPrime);

            var p1 = Math.Pow(27, this.PlainTextBlockSize);
            var p2 = Math.Pow(27, this.CypherTextBlockSize);
            if (p1 >= this.Modulus || p2 <= this.Modulus)
            {
                throw new Exception("Modulus should be between 27^" + PlainTextBlockSize.ToString() + " and 27^" + CypherTextBlockSize.ToString());
            }

            if (Math.EuclidGcd(this.PhiN, this.EncryptionExponent) != 1)
            {
                throw new Exception("Gcd of encryption exponent and Phi(n) should be 1!");
            }

            this.DecryptionExponent = Math.ModularInverse(EncryptionExponent, PhiN);
        }

        public String Encrypt(String Plaintext)
        {
            var numericalEquivalents = RSACryptoProvider.GetNumericalEquivalents(Plaintext.ToUpper(), this.PlainTextBlockSize);
            String result = "";
            foreach (var n in numericalEquivalents)
            {
                var encrypted = Math.Pow(n, EncryptionExponent, Modulus);
                result += GetStringEquivalent(encrypted, CypherTextBlockSize);
            }

            return result;
        }

        private static String GetStringEquivalent(Int64 Number, Int32 Blocksize)
        {
            var size = Letters.Count();
            String result = "";

            for (int i = Blocksize - 1; i >= 0; --i)
            {
                var power = Math.Pow(size, i);
                var res = Number / power;
                result += ReversedLetters[res];
                Number -= (res * power);
            }

            return result;
        }

        private static List<Int64> GetNumericalEquivalents(String Text, Int32 Blocksize)
        {
            var result = new List<Int64>();

            if (Text.Count() % Blocksize != 0)
            {
                Text += new String('_', Text.Count() % Blocksize);
            }

            for(int i = 0; i < Text.Count(); i += Blocksize)
            {
                result.Add(GetNumericalEquivalent(Text.Substring(i, Blocksize)));
            }

            return result;
        }

        private static Int64 GetNumericalEquivalent(String Block)
        {
            var size = Letters.Count();
            Int64 result = 0;

            for(int i = Block.Count() - 1; i >= 0; --i)
            {
                var character = Block[Block.Count() - i - 1];
                result += Math.Pow(size, i) * Letters[character];
            }

            return result;
        }

        private static Dictionary<Char, Int64> Letters = new Dictionary<Char, Int64>
        {
            { '_' , 0 },
            { 'A' , 1 },
            { 'B' , 2 },
            { 'C' , 3 },
            { 'D' , 4 },
            { 'E' , 5 },
            { 'F' , 6 },
            { 'G' , 7 },
            { 'H' , 8 },
            { 'I' , 9 },
            { 'J' , 10 },
            { 'K' , 11 },
            { 'L' , 12 },
            { 'M' , 13 },
            { 'N' , 14 },
            { 'O' , 15 },
            { 'P' , 16 },
            { 'Q' , 17 },
            { 'R' , 18 },
            { 'S' , 19 },
            { 'T' , 20 },
            { 'U' , 21 },
            { 'V' , 22 },
            { 'W' , 23 },
            { 'X' , 24 },
            { 'Y' , 25 },
            { 'Z' , 26 },
        };

        private static Dictionary<Int64, Char> ReversedLetters = Letters.ToDictionary(x => x.Value, x => x.Key);
    }
}
