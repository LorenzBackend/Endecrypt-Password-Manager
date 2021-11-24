using Endecrypt.Core;
using Endecrypt.Misc;
using Newtonsoft.Json;
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
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        List<PasswordItem> ItemList = new List<PasswordItem>();

        private void search_OnValueChanged(object sender, EventArgs e)
        {
            string searchFor = search.Text;
            if (searchFor.Length < 1)
            {
                foreach (PasswordItem item in flowLayoutPanel1.Controls)
                {
                    item.Show();
                }
                return;
            }
            foreach (PasswordItem item in flowLayoutPanel1.Controls)
            {
                if (item.GetName().Contains(searchFor) || item.GetTitle().Contains(searchFor))
                {
                    item.Show();
                }
                else
                {
                    item.Hide();
                }
            }
        }

        private void RefreshList()
        {
            flowLayoutPanel1.Controls.Clear();
            IStorage storage = new Storage();
            List<PasswordObject> ObjList = storage.LoadPasswords();
            if (ObjList == null) return;

            foreach (var dataObj in ObjList)
            {
                PasswordItem item = 
                    new PasswordItem(dataObj.Title, dataObj.AccountName, dataObj.Password, dataObj.ImageSrc);
                item.DeleteItemEvent += DeleteItem;
                flowLayoutPanel1.Controls.Add(item);
                item.SetIndex(flowLayoutPanel1.Controls.GetChildIndex(item));
                ItemList.Add(item);
            }       
        }

        private void DeleteItem(int index)
        {
            ItemList.RemoveAt(index);
            CurrentData.currentData.RemoveAt(index);
            IStorage storge = new Storage();
            flowLayoutPanel1.Controls.RemoveAt(index);

            foreach (PasswordItem item in flowLayoutPanel1.Controls)
            {
                item.SetIndex(flowLayoutPanel1.Controls.GetChildIndex(item));
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {     
            AddPasswordDialog addPw = new AddPasswordDialog();      
            if (addPw.ShowDialog() == DialogResult.OK)
            {
                IStorage storge = new Storage();
                storge.AddPassword(addPw.getTitle(), addPw.getName(), addPw.getPassword(), addPw.getImage());
                RefreshList();
            }
        }

        private void main_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
