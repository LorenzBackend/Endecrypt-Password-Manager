using Endecrypt.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Endecrypt
{

    public partial class AddPasswordDialog : Form
    {
        public AddPasswordDialog()
        {
            InitializeComponent();
        }
        private string imageKey = "none";

        public AddPasswordDialog(string title, string name, string password, string image)
        {
            InitializeComponent();
            txtTitle.Text = title;
            txtName.Text = name;
            txtPw.isPassword = true;
            txtPw.Text = password;
            if (!image.Equals("none") && File.Exists(image))
            {
                 imageKey = image;
                pictureBox1.Image = Image.FromFile(image);
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (!CheckInputs()) return;
            this.DialogResult = DialogResult.OK;
        }

      private bool CheckInputs()
        {
            if (txtTitle.Text.Length < 1)
            {
                MessageBox.Show("No Title", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtName.Text.Length < 1)
            {
                MessageBox.Show("No Account Name", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtPw.Text.Length < 8)
            {
                MessageBox.Show("Password is to short", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image|*.jpg;*.jpeg;*.png;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                imageKey = ofd.FileName;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string rule = "!#$%()*+,-./0123456789:;<=>@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
            string result = "";
            Random r = new Random();
            for (int x = 0; x < rule.Length; x++)
            {
                result += rule[r.Next(0, rule.Length)];
            }
            txtPw.Text = result;
        }

        private void txtPw_OnValueChanged(object sender, EventArgs e)
        {
            txtPw.isPassword = true;
        }
        
        public string getTitle()
        {
            return txtTitle.Text;
        }

        public string getName()
        {
            return txtName.Text;
        }

        public string getPassword()
        {
            return txtPw.Text;
        }

        public string getImage()
        {
            return imageKey;
        }
    }
}
