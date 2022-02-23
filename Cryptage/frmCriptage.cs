using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace Cryptage
{
    public partial class frmCriptage : Form
    {
        public frmCriptage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           textBox2.Text = Convert.ToBase64String(myLib.EncryptSym(textBox1.Text, myLib.cle, myLib.iv));
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
              textBox2.Text = myLib.DecryptSym(System.Convert.FromBase64String(textBox1.Text), myLib.cle, myLib.iv);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CspParameters cspParams = new CspParameters();

            // Les clés publique et privée
           
            RSAParameters publicKeys;

            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                // charger la clé publique
                rsa.FromXmlString(myLib.pu);
             
                publicKeys = rsa.ExportParameters(false);
                rsa.Clear();
            }


            // La clé publique est utilisée pour chiffrer les données
            textBox2.Text = Convert.ToBase64String(myLib.EncryptAssym (textBox1.Text, publicKeys));
          
      






        }

        private void button5_Click(object sender, EventArgs e)
        {
            myLib.Generate_cle_iv();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            myLib.generate_public_private_key();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CspParameters cspParams = new CspParameters();

            // La clé privée
            RSAParameters privateKeys;
        
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                // charger la clé privée
                rsa.FromXmlString(myLib.pr);
                privateKeys = rsa.ExportParameters(true);
               
                rsa.Clear();
            }


            // La clé privée est utilisée pour déchiffrer les données
            textBox2.Text = myLib.DecryptAssym (System.Convert.FromBase64String(textBox1.Text), privateKeys);



        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox2.Text = myLib.hash512(textBox1.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox2.Text = myLib.hash256(textBox1.Text);
        }
    }
}
