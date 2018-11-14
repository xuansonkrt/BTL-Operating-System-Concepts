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

namespace count
{
    public partial class frmCount : Form
    {
        string key;
        string url;
        bool isFile = true;
        public frmCount()
        {
            InitializeComponent();
            //url = @"E:\IT_MTA\Nam 3\Lý thuyết hệ điều hành\BTL\count\count\count\bin\Debug\file1.txt";
            //string fileText = File.ReadAllText(url);
            //richTextBox.Text = fileText;
        }
        public frmCount(string key,string url)
        {
            InitializeComponent();
            this.key = key;
            this.url = url;
            isFile = false;
            txtKeys.Text = key;
            string fileText = File.ReadAllText(url);
            richTextBox.Text = fileText;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog openFile = new OpenFileDialog();
            if(openFile.ShowDialog()==DialogResult.OK)
            {
                if ((myStream = openFile.OpenFile()) != null)
                {
                    url = openFile.FileName;
                    string fileText = File.ReadAllText(url);
                    richTextBox.Text = fileText;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Count();
        }
        private void Count()
        {
            lbStatus.Text = "Counting...";
            int index = 0;
            int count = 0;
            string tmp = txtKeys.Text;
            key = txtKeys.Text;
            String temp = richTextBox.Text;
            richTextBox.Text = "";
            richTextBox.Text = temp;
            //count
            int strt = 0; //vị trí trước đó có xuất hiện chuỗi
            int idx = -1; // 
            while (strt != -1)
            {
                strt = temp.IndexOf(key, idx + 1);
                count += 1;
                idx = strt;
                lbResult.Text = count.ToString();
            }
            //hight light
            while (index < richTextBox.Text.LastIndexOf(key))
            {
                //searches the text in a richTextBox control for a string within a rangr of text withing the content 
                richTextBox.Find(key, index, richTextBox.TextLength, RichTextBoxFinds.None);
                //select color. this is added auto when a match is found
                richTextBox.SelectionBackColor = Color.Yellow;
                //  count++;
                // after a match is found the index is increased so the search won't stop at the same
                index = richTextBox.Text.IndexOf(key, index) + 1;

            }
            txtKeys.Text = tmp;
            lbStatus.Text = "Done!";

            // lbResult.Text = count.ToString();
        }


        private void txtKeys_KeyDown(object sender, KeyEventArgs e)
        {
            string tmp = txtKeys.Text;

            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
            txtKeys.Text = tmp;


        }

        private void frmCount_Load(object sender, EventArgs e)
        {
          //  Search();   
        }
    }
}
