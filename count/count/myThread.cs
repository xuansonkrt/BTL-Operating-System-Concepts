using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace count
{
    class myThread
    {
        string url;
        string key;
        public myThread(string key, string url)
        {
            this.url = url;
            this.key = key;
        }
        public void myProcess()
        {
            frmCount frm = new frmCount(this.key, this.url);
            frm.ShowDialog();
        }
    }

}
