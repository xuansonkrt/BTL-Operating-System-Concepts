using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace count
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string url = "E:\\IT_MTA\\Nam 3\\Lý thuyết hệ điều hành\\BTL\\BTL-Operating-System-Concepts\\count\\count\\bin\\Debug\\";
            string key;
            string cmd = txtCmd.Text;
            string[] arr = cmd.Trim().Split(' ');
            if(arr[0].Equals("count")==false)
            {
                MessageBox.Show("Nhập sai cú pháp");
                return;
            }
            if(arr.Length<3)
            {
                MessageBox.Show("Nhập sai cú pháp");
                return;

            }
            
            //url = String.Concat(url, arr[2]);
            key = arr[1];
            //frmCount frm = new frmCount(arr[1],url);
            //frm.ShowDialog();
            // MessageBox.Show(arr.Length.ToString());
            for (int i = 2; i < arr.Length; i++)
            {
                string tmp = url;
                tmp = String.Concat(tmp, arr[i]);
                myThread proc = new myThread(key, tmp);
                Thread thr = new Thread(proc.myProcess);
                thr.Start();
            }
            
        }
        
        private void txtCmd_KeyDown(object sender, KeyEventArgs e) // thực thi khi nhấn enter trong text box
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnSubmit_Click(sender, e);
            }
        }
    }
}
