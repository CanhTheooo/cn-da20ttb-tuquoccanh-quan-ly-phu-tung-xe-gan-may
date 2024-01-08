using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VehiclePartStore.Views
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmCustomer frm = new frmCustomer();
            frm.ShowDialog();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory();
            frm.ShowDialog();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct();
            frm.ShowDialog();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmOrderHeader frm = new frmOrderHeader();
            frm.ShowDialog();
        }

        private void btnStatis_Click(object sender, EventArgs e)
        {
            frmStatistics frm = new frmStatistics();
            frm.ShowDialog();
        }
    }
}
