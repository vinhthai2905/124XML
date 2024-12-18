using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLINHKIENDT
{
    public partial class Login : Form
    {
        static public Menu fromMenu;
        public Login()
        {
            InitializeComponent();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Model.DangNhap DN = new Model.DangNhap();
            String maNV = DN.Login(txtUserName.Text, txtPassword.Text);
            if (!maNV.Equals(""))
            {
                fromMenu = new Menu();
                fromMenu.Show();

            }
            else
            {
                thongbao.Text = "Sai tài khoản hoặc mật khẩu";
            }
            Console.WriteLine(DN.Login(txtUserName.Text, txtPassword.Text));
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(1311,680);
            txtPassword.UseSystemPasswordChar = true;
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false; // Hiển thị mật khẩu
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true; // Ẩn mật khẩu
            }
        }
    }
}
