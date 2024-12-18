using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QUANLINHKIENDT
{
    public partial class ThanhToan : Form
    {
        Model.XMLFile XmlFile = new Model.XMLFile();
        XmlNodeList  nodeListSanPham;
        int sTT = 0;
        public ThanhToan()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                int soLuongMua = int.Parse(textBoxSoLuong.Text);
                if (soLuongMua > 0 && int.Parse(textBoxSoLuong.Text) <= int.Parse(dataGridView1.CurrentRow.Cells[3].FormattedValue.ToString()))
                {
                    dataGridView2.Rows.Add(
                        dataGridView1.CurrentRow.Cells[0].FormattedValue.ToString(),
                        dataGridView1.CurrentRow.Cells[1].FormattedValue.ToString(),
                        soLuongMua,
                        dataGridView1.CurrentRow.Cells[2].FormattedValue.ToString(),
                        (soLuongMua * int.Parse(dataGridView1.CurrentRow.Cells[2].FormattedValue.ToString()))
                        );
                }
                else
                    MessageBox.Show("Số lượng hàng không đủ mong bạn thông cảm!!", "Thông Báo");
                textBoxSoLuong.Text = "";
            }
            catch { }
            capNhatTongTien();
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(950, 620);
            // Đặt ngày tháng năm hiện tại
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Enabled = false;
            dateTimePicker1.ShowUpDown = false;
            LoadSanPham();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            int idsanpham;
            String tenKhachHang = txtTenKH.Text;
            int soLuong;
            int donGia;
            int tongtien;
            DateTime selectedDate = dateTimePicker1.Value;
            String ngayThanhToan = selectedDate.ToString();
            Model.HoaDon hoaDon = new Model.HoaDon();
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                idsanpham = int.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString());
                soLuong = int.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString());
                // Cập nhật số lượng sản phẩm
                Model.SanPham sanpham = new Model.SanPham();
                sanpham.UpdateSoLuong(idsanpham, soLuong);
                donGia = int.Parse(dataGridView2.Rows[i].Cells[3].Value.ToString());
                tongtien = int.Parse(dataGridView2.Rows[i].Cells[4].Value.ToString());
                hoaDon.AddHoaDon(idsanpham, tenKhachHang, soLuong, donGia, tongtien, ngayThanhToan);
            }

            MessageBox.Show("In Hóa Đơn Thành Công");
            dataGridView2.Rows.Clear();
            txtTenKH.Text = "";

            // Làm mới lại số lượng sản phẩm
            LoadSanPham();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try { dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index); }
            catch { }
            capNhatTongTien();
        }

        public void LoadSanPham()
        {
            // clear data
            dataGridView1.Rows.Clear();
            Model.SanPham sanpham = new Model.SanPham();
            nodeListSanPham = sanpham.getSanPham();
            foreach (XmlNode x in nodeListSanPham)
            {
                dataGridView1.Rows.Add(x.ChildNodes[0].InnerText, x.ChildNodes[1].InnerText, x.ChildNodes[3].InnerText, x.ChildNodes[7].InnerText, x.ChildNodes[6].InnerText, x.ChildNodes[2].InnerText);
            }
        }

        void capNhatTongTien()
        {
            int tongTien = 0;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                tongTien += int.Parse(dataGridView2.Rows[i].Cells[4].Value.ToString());
            }
            labelTongTien.Text = tongTien.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelTongTien_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSoLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
