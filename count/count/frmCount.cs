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
        string file;
        string fileText; // nội dung file
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
            string[] arr = url.Split('\\');
            file = arr[arr.Length - 1];
            this.Text = "Count: key=" + key + ";    url=" + file;
            txtKeys.Text = key;
            try
            {
                fileText = File.ReadAllText(url);
            } catch
            {
                MessageBox.Show("File "+ file + " không tồn tại!");
                fileText = "";
                //this.Close();
            }
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
                    fileText = File.ReadAllText(url);
                    richTextBox.Text = fileText;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lbStatus.Text = "Counting...";
            Application.DoEvents();
            Count();
        }
        private void Count()
        {
            int count = 0;
            key = txtKeys.Text;
            String temp = richTextBox.Text;
            richTextBox.Text = ""; // cập nhật lại text khi tìm từ khác
            richTextBox.Text = temp;
            //count
            int strt = 0; //vị trí trước đó có xuất hiện chuỗi
            int idx = -1; // 
            int len = key.Length;
            temp = temp.ToUpper();
            string tmpKey = key.ToUpper();
            RichTextBox rtb = new RichTextBox();
            while (strt != -1)
            {
                strt = temp.IndexOf(tmpKey, idx + 1);
                if(strt!=-1)
                {
                    count += 1;
                    idx = strt;
                    //hight light
                    richTextBox.Select(strt, len);
                    richTextBox.SelectionBackColor = Color.Yellow;
                }
                lbResult.Text = count.ToString();
            }
            //hight light
      /*      while (index < richTextBox.Text.LastIndexOf(key))
            {
                richTextBox.Find(key, index, richTextBox.TextLength, RichTextBoxFinds.None);
                richTextBox.SelectionBackColor = Color.Yellow;
                //  count++;
                index = richTextBox.Text.IndexOf(key, index) + 1;
            }*/
            txtKeys.Text = key;
            lbStatus.Text = "Done!";

            this.Text= "Count: key=" + key + ";    url=" + file+";   Count=" + count.ToString();
            txtKeys.Select(0, len);
        }


        private void txtKeys_KeyDown(object sender, KeyEventArgs e)
        {
            //key = txtKeys.Text;
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void frmCount_Load(object sender, EventArgs e)
        {
          //  
        }

        private void frmCount_Shown(object sender, EventArgs e)
        {
            lbStatus.Text = "Counting...";
            Application.DoEvents();
            //Count();
            if (fileText == "")
                this.Close();
            btnSearch_Click(sender, e);
        }
    }
}
