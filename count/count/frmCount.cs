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
        FileStream stream;
        public frmCount()
        {
            InitializeComponent();
        }
        public frmCount(string key,string url)
        {
            InitializeComponent();
            this.key = key;
            this.url = url;
            this.Text = "Count: key=" + key + ";    url=" + file;
            txtKeys.Text = key;
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (stream != null)
                stream.Close();
            openFileDialog.FileName = "";
            openFileDialog.ShowDialog();
            url = openFileDialog.FileName;
            if (url == "")
                return;
            Wait(url);
            lbStatus.Text = "Opened";
            richTextBox.Text = readFile(url);
            Application.DoEvents();
            this.Text = "Count: key=" + key + ";    file=" + fileName(url);
            lbResult.Text = "0";
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            lbStatus.Text = "Counting...";
            lbResult.Text = "0";
            this.Text = "Count: key=" + key + ";    file=" + fileName(url);
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
            int strt = 0; //vị trí xuất hiện chuỗi
            int idx = -1; //vị trí trước đó có xuất hiện chuỗi
            int len = key.Length;
            temp = temp.ToUpper();
            string tmpKey = key.ToUpper();
            while (strt != -1)
            {
                strt = temp.IndexOf(tmpKey, idx + 1);
                if(strt!=-1)
                {
                    count += 1;
                    idx = strt;
                    //hightlight chuỗi tìm thấy
                    richTextBox.Select(strt, len);
                    richTextBox.SelectionBackColor = Color.Yellow;
                }
                lbResult.Text = count.ToString();
            }
            txtKeys.Text = key;
            lbStatus.Text = "Done!";
            this.Text= "Count: key=" + key + ";    file=" + fileName(url)+";   Count=" + count.ToString();
            txtKeys.Select(0, len);
        }
        private void txtKeys_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // khi xảy ra sự kiện enter trong text box thì gọi tới method btnSearch_Click
            {
                btnSearch_Click(sender, e);
            }
        }
        private void frmCount_Shown(object sender, EventArgs e)
        {
            if (!File.Exists(url))
            {
                MessageBox.Show("File "+fileName(url)+" không tồn tại");
                this.Dispose();
                return;
            }
            Wait(url);
            lbStatus.Text = "Opened";
            Application.DoEvents();
            this.Text = "Count: key=" + key + ";    file=" + fileName(url);
            lbResult.Text = "0";
            richTextBox.Text = readFile(url);
            lbStatus.Text = "Counting...";
            Application.DoEvents();
            if (fileText == "")
                this.Close();
            btnSearch_Click(sender, e);
        }
        public void Wait(string url)
        {
            if (IsFileOpened(url))
            {
                MessageBox.Show("File " + fileName(url) + " đang được mở."); // đưa ra thông báo chờ
                lbStatus.Text = "Waiting...";
                this.Text = "Count: key=" + key + ";    file=" + fileName(url);
                lbResult.Text = "0";
                richTextBox.Text = "";
                Application.DoEvents();
                while (IsFileOpened(url))
                {
                    // không làm gì
                    // chờ tài nguyên được giải phóng
                }
            }
        }
        public string fileName(string url) // trả về tên của file
        {
            string[] arr = url.Split('\\');
            string filename = arr[arr.Length - 1];
            return filename;
        }
        public string readFile(string url) // đọc dữ liệu từ file
        {
            string text = "";
            try
            {
                stream = new FileStream(url, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                text = reader.ReadToEnd();
            }
            catch
            {
                MessageBox.Show("File " + fileName(url) + " không tồn tại!");
            }
            return text;
        }
        protected virtual bool IsFileOpened(string url) // kiểm tra file có đang được sử dụng hay không
        {
            FileInfo file = new FileInfo(url);
            FileStream fstream = null;
            try
            {
                fstream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (fstream != null)
                    fstream.Close();
            }
            return false;
        }

        private void frmCount_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (stream != null)
                stream.Close();
        }
    }
}
