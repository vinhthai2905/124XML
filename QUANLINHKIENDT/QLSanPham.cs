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
    public partial class QLSanPham : Form
    {

        Model.XMLFile XmlFile = new Model.XMLFile();
        XmlNodeList  nodeListDM, nodeListNSX, nodeListSanPham;
        private Dictionary<string, string> categoryDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> categoryDictionary1 = new Dictionary<string, string>();
        public QLSanPham()
        {
            InitializeComponent();
        }

        private void QLSanPham_Load(object sender, EventArgs e)
        {
            //id
            txtID.ReadOnly = true;
            txtDonGia.Text = "0";
            txtSoLuong.Text = "0";
            //Danh muc
            Model.DanhMuc danhmuc = new Model.DanhMuc();
            nodeListDM = danhmuc.getListDM();
            foreach (XmlNode x in nodeListDM)
            {
                comboboxLoai.Items.Add(x.ChildNodes[1].InnerText);
                categoryDictionary.Add(x.ChildNodes[0].InnerText, x.ChildNodes[1].InnerText);
            }

            //Nha san xuat
            Model.NhaSanXuat nhasx = new Model.NhaSanXuat();
            nodeListNSX = nhasx.getListNSX();
            foreach (XmlNode x in nodeListNSX)
            {
                comboboxnsx.Items.Add(x.ChildNodes[1].InnerText);
                categoryDictionary1.Add(x.ChildNodes[0].InnerText, x.ChildNodes[1].InnerText);
            }

            //LoadXML
            LoadXML();
          
        }

        private void ResetData()
        {
            //xoadulieu
            txtID.Text = "";
            txtTen.Text = "";
            txtDonGia.Text = "";
            txtSoLuong.Text = "";
            txtBaoHanh.Text = "";
            txtTinhTrang.Text = "";
            txtMoTa.Text = "";
            comboboxLoai.SelectedItem = null;
            comboboxnsx.SelectedItem = null;
        }

        private void LoadXML()
        {
            //SanPham
            Model.SanPham sanpham = new Model.SanPham();
            DataTable dt = new DataTable();
            String idDM = "", idNSX = "";
            dt.Columns.Add("ID");
            dt.Columns.Add("Tên sản phẩm");
            dt.Columns.Add("Đơn giá");
            dt.Columns.Add("Loại");
            dt.Columns.Add("Nhà sản xuất");
            dt.Columns.Add("Số lượng");
            dt.Columns.Add("Tình trạng");
            dt.Columns.Add("Bảo hành");
            dt.Columns.Add("Mô tả");
            nodeListSanPham = sanpham.getSanPham();
            foreach (XmlNode x in nodeListSanPham)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = x.ChildNodes[0].InnerText;
                dr["Tên sản phẩm"] = x.ChildNodes[1].InnerText;
                dr["Đơn giá"] = x.ChildNodes[3].InnerText;
                idDM = x.ChildNodes[9].InnerText;
                if (categoryDictionary.ContainsKey(idDM))
                {
                    string tendanhmuc = categoryDictionary[idDM];
                    dr["Loại"] = tendanhmuc;
                }
                idNSX = x.ChildNodes[4].InnerText;
                if (categoryDictionary1.ContainsKey(idNSX))
                {
                    string tennhasanxuat = categoryDictionary1[idNSX];
                    dr["Nhà sản xuất"] = tennhasanxuat;
                }
                dr["Số lượng"] = x.ChildNodes[7].InnerText;
                dr["Tình trạng"] = x.ChildNodes[6].InnerText;
                dr["Bảo hành"] = x.ChildNodes[5].InnerText;
                dr["Mô tả"] = x.ChildNodes[2].InnerText;
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            String ten = txtTen.Text;
            int dongia = int.Parse(txtDonGia.Text);
            int soluong = int.Parse(txtSoLuong.Text);
            String hinhanh = "20-236-988-01_7c79ecf51d3f4cee80a8cb4a081e6287_grande.jpg";
            String baohanh = txtBaoHanh.Text;
            String tinhtrang = txtTinhTrang.Text;
            int idloai = comboboxLoai.SelectedIndex + 1;
            int idnhasanxuat = comboboxnsx.SelectedIndex + 1;
            String mota = txtMoTa.Text;

            if (ten == "" || dongia == null || soluong == null || baohanh=="" || tinhtrang=="" )
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin");
            }
            else
            {
                //them san pham vao file xml
                Model.SanPham sanpham = new Model.SanPham();
                sanpham.AddSanPham(ten, mota, dongia, idnhasanxuat, baohanh, tinhtrang, soluong, hinhanh, idloai);

                MessageBox.Show("Đã thêm thành công");
                ResetData();

                // Cập nhật lại DataGridView
                LoadXML();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String ten = txtTen.Text;
            int idsanpham = int.Parse(txtID.Text);
            int dongia = int.Parse(txtDonGia.Text);
            int soluong = int.Parse(txtSoLuong.Text);
            String hinhanh = "20-236-988-01_7c79ecf51d3f4cee80a8cb4a081e6287_grande.jpg";
            String baohanh = txtBaoHanh.Text;
            String tinhtrang = txtTinhTrang.Text;
            int idloai = comboboxLoai.SelectedIndex + 1;
            int idnhasanxuat = comboboxnsx.SelectedIndex + 1;
            String mota = txtMoTa.Text;
            //them san pham vao file xml
            Model.SanPham sanpham = new Model.SanPham();
            sanpham.UpdateSanPham(idsanpham, ten, mota, dongia, idnhasanxuat, baohanh, tinhtrang, soluong, hinhanh, idloai);

            MessageBox.Show("Đã cập nhật thành công");

            ResetData();
            // Cập nhật lại DataGridView
            LoadXML();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem người dùng đã chọn một hàng chưa
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Lấy dữ liệu từ các ô trong hàng được chọn và hiển thị lên các TextBox
                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtTen.Text = selectedRow.Cells[1].Value.ToString();
                txtDonGia.Text = selectedRow.Cells[2].Value.ToString();
                txtSoLuong.Text = selectedRow.Cells[5].Value.ToString();
                txtBaoHanh.Text = selectedRow.Cells[7].Value.ToString();
                txtTinhTrang.Text = selectedRow.Cells[6].Value.ToString();
                txtMoTa.Text = selectedRow.Cells[8].Value.ToString();
                string loai = selectedRow.Cells[3].Value.ToString();
                comboboxLoai.SelectedItem = loai;
                string nhasx = selectedRow.Cells[4].Value.ToString();
                comboboxnsx.SelectedItem = nhasx;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menu formMenu = new Menu();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idsanpham = int.Parse(txtID.Text);
            Model.SanPham sanpham = new Model.SanPham();
            sanpham.DeleteSanPham(idsanpham);


            MessageBox.Show("Đã xóa thành công");

            ResetData();
            // Cập nhật lại DataGridView
            LoadXML();
        }

       

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


      




    }
}
