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
            string url = Environment.CurrentDirectory; // lấy đường dẫn của thư mục chứa file thực thi
            url=String.Concat(url,"\\");
            string key;
            string cmd = txtCmd.Text;
            string[] arr = cmd.Trim().Split(' ');//tách các thành phần của câu lệnh
            string method = arr[0].ToUpper();
            if(method.Equals("COUNT") == false) // kiểm tra cú pháp
            {
                MessageBox.Show("Nhập sai cú pháp");
                return;
            }
            if(arr.Length<3)
            {
                MessageBox.Show("Nhập sai cú pháp");
                return;
            }
            key = arr[1];
            for (int i = 2; i < arr.Length; i++)// với mỗi file có 1 tiến trình sử lý được hiển thị trên 1 form
            {
                string tmp = url;
                tmp = String.Concat(tmp, arr[i]);
                myThread proc = new myThread(key, tmp);
                Thread thr = new Thread(proc.myProcess);
                thr.SetApartmentState(ApartmentState.STA); //
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
