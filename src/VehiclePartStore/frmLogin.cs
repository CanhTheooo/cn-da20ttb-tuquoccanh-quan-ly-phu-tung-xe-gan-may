using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehiclePartStore.Dto;
using VehiclePartStore.Services;
using VehiclePartStore.Views;

namespace VehiclePartStore
{
    public partial class frmLogin : Form
    {
        UserService service;
        public frmLogin()
        {
            InitializeComponent();
            service = new UserService();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (service.Authorize(new UserDto { Username = txtUserName.Text.Trim(), Password = txtPassword.Text.Trim() }))
            {
                Ultils.Ultils.ShowInfoOKMess("Đăng nhập thành công!");
                this.Hide();
                frmMain frm = new frmMain();
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                Ultils.Ultils.ShowErrorOKMess("Lỗi đăng nhập!");
            }
           
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
           
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.SelectAll();
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
