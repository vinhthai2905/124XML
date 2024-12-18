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
    public partial class Menu : Form
    {
        static public QLSanPham formQLSP;
        public Menu()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formQLSP = new QLSanPham();
            formQLSP.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThanhToan formThanhToan = new ThanhToan();
            formThanhToan.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            QuanLy formQuanLy = new QuanLy();
            formQuanLy.Show();
            //this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LichSuThanhToan formLichSuThanhToan = new LichSuThanhToan();
            formLichSuThanhToan.Show();
            //this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaoLuuDuLieu formSaoLuu = new SaoLuuDuLieu();
            formSaoLuu.Show();
        }
    }
}
