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
    public partial class SaoLuuDuLieu : Form
    {
        public SaoLuuDuLieu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model.SanPham sanpham = new Model.SanPham();
            sanpham.InsertXmlDataToDatabase();
            MessageBox.Show("Đã sao lưu thành công");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Model.HoaDon hoadon = new Model.HoaDon();
            hoadon.InsertXmlDataToDatabase();
            MessageBox.Show("Đã sao lưu thành công");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Model.DangNhap dangnhap = new Model.DangNhap();
            dangnhap.InsertXmlDataToDatabase();
            MessageBox.Show("Đã sao lưu thành công");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Model.NhanVien nhanvien = new Model.NhanVien();
            nhanvien.InsertXmlDataToDatabase();
            MessageBox.Show("Đã sao lưu thành công");
        }

    }
}
