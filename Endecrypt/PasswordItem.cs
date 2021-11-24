using Endecrypt.Core;
using Endecrypt.Misc;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Endecrypt
{
    public partial class PasswordItem : UserControl
    {
        public PasswordItem(string title, string accountname, string password, string image)
        {
            InitializeComponent();
            Title.Text = title;
            AccountName.Text = accountname;
            this.Password = password;
            imageSrc = image;

            if (File.Exists(image))
            {
                icon.Image = Image.FromFile(image);
            }
            else
            {
                if (!image.Equals("none"))
                {
                    MessageBox.Show("The Image File: \"" + image + "\" was not found", "Image Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public delegate void DeleteItem(int index);
        public event DeleteItem DeleteItemEvent;

        private int Index = 0;
        private string Password;
        private int count = 20;
        private string imageSrc;
        private bool isTick = false;

        private void btnCopy_Click(object sender, EventArgs e)
        {
           if (isTick)
            {
                timer1.Stop();
                count = 20;
            }
            Clipboard.SetText(Password);
            timer1.Start();
            isTick = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("are you sure?", "Delete: " + AccountName.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (DeleteItemEvent != null) DeleteItemEvent(this.Index);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddPasswordDialog passwordDialog = new AddPasswordDialog(GetTitle(), GetName(), Password, imageSrc);
            if (passwordDialog.ShowDialog() == DialogResult.OK)
            {
                CurrentData.currentData[this.Index].Title = passwordDialog.getTitle();
                CurrentData.currentData[this.Index].AccountName = passwordDialog.getName();
                CurrentData.currentData[this.Index].Password = passwordDialog.getPassword();
                CurrentData.currentData[this.Index].ImageSrc = passwordDialog.getImage();
                Title.Text = CurrentData.currentData[this.Index].Title;
                AccountName.Text = CurrentData.currentData[this.Index].AccountName;
                Password = CurrentData.currentData[this.Index].Password;
                if (CurrentData.currentData[this.Index].ImageSrc != imageSrc)
                {
                    imageSrc = CurrentData.currentData[this.Index].ImageSrc;
                    icon.Image = Image.FromFile(imageSrc);
                }

                IStorage storage = new Storage();
                storage.RefreshData();
            }
        }

        public void SetIndex(int index)
        {
            this.Index = index;
        }

        public string GetTitle()
        {
            return Title.Text;
        }
        public string GetName()
        {
            return AccountName.Text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count -= 1;
            label1.Text = "in clippboard";
            label1.Visible = true;
            if (count == 0)
            {
                timer1.Stop();
                count = 20;
                isTick = false;
                label1.Visible = false;
                bool flag = Clipboard.GetText().Equals(Password);
                if (flag) Clipboard.Clear();
            }
        }
    }
}
