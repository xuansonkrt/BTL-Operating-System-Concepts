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
            string url = "E:\\IT_MTA\\Nam 3\\Lý thuyết hệ điều hành\\BTL\\count\\count\\count\\bin\\Debug\\";
            url = String.Concat(url, arr[2]);
             frmCount frm = new frmCount(arr[1],url);
            frm.ShowDialog();
            // MessageBox.Show(arr.Length.ToString());
          
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
