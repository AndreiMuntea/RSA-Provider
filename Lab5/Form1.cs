using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var provider = new RSACryptoProvider(
                        Int64.Parse(textBox5.Text),
                        Int64.Parse(textBox4.Text),
                        Int64.Parse(textBox3.Text),
                        Int32.Parse(textBox7.Text),
                        Int32.Parse(textBox6.Text)
                    );
                textBox2.Text = provider.Encrypt(textBox1.Text);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
}

        private void label7_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var provider = new RSACryptoProvider(Int64.Parse(textBox5.Text), Int64.Parse(textBox4.Text), Int64.Parse(textBox3.Text), Int32.Parse(textBox7.Text), Int32.Parse(textBox6.Text));
                textBox1.Text = provider.Decrypt(textBox2.Text);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
}

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var res = GetRandomValues();
                textBox5.Text = res.Item1.ToString();
                textBox4.Text = res.Item2.ToString();
                textBox3.Text = res.Item3.ToString();
            } catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private Tuple<Int64, Int64, Int32> GetRandomValues()
        {

            var lower = Math.Pow(27, Int32.Parse(textBox7.Text));
            var upper = Math.Pow(27, Int32.Parse(textBox6.Text));

            var res = Math.GetRandomPrime(lower, upper);
            var phi = Math.Phi(res.Item1, res.Item2);
            Int32 encryptionExponent = 0;

            for (encryptionExponent = 2; encryptionExponent <= 6666; ++encryptionExponent)
            {
                if (Math.EuclidGcd(phi, encryptionExponent) == 1)
                {
                    break;
                }
            }

            if (encryptionExponent == 0)
            {
                throw new Exception("Couldn't find a proper value for encryption exponent. Sorry!");
            }

            return new Tuple<Int64, Int64, Int32>(res.Item1, res.Item2, encryptionExponent);
        }
    }
}
