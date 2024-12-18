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
    public partial class QuanLy : Form
    {
        Model.XMLFile XmlFile = new Model.XMLFile();
        XmlNodeList nodeListNhanVien, nodeListChuVu, nodeListDanhMuc;
        private Dictionary<string, string> DataChucVu = new Dictionary<string, string>();
        public QuanLy()
        {
            InitializeComponent();
        }

      

        private void QuanLy_Load(object sender, EventArgs e)
        {
            // Đặt tiêu đề mới cho tab thứ nhất (ví dụ: "Tab 1")
            tabControl1.TabPages[0].Text = "Quản lý nhân viên";

            tabControl1.TabPages[1].Text = "Quản lý hãng";
            

            //Chucvu
            Model.ChucVu chucvu = new Model.ChucVu();
            nodeListChuVu = chucvu.getListChucVu();
            String idCV = "";
            foreach (XmlNode x in nodeListChuVu)
            {
                idCV = x.ChildNodes[0].InnerText;
                comboBoxCV.Items.Add(x.ChildNodes[1].InnerText);
                DataChucVu.Add(x.ChildNodes[0].InnerText, x.ChildNodes[1].InnerText);
            }
            
            LoadNhanVien();
            LoadDanhMuc();
        }

        public void LoadNhanVien()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã nhân viên");
            dt.Columns.Add("Chức vụ");
            dt.Columns.Add("Tên nhân viên");
            dt.Columns.Add("Quê quán");
            String iddm = "";
            //NhanVien
            Model.NhanVien nhanvien = new Model.NhanVien();
            nodeListNhanVien = nhanvien.getListNhanVien();
            foreach (XmlNode x in nodeListNhanVien)
            {
                DataRow dr = dt.NewRow();
                dr["Mã nhân viên"] = x.ChildNodes[0].InnerText;
                iddm = x.ChildNodes[1].InnerText;
                if (DataChucVu.ContainsKey(iddm))
                {
                    string tenchucvu = DataChucVu[iddm];
                    dr["Chức vụ"] = tenchucvu;
                }
                dr["Tên nhân viên"] = x.ChildNodes[2].InnerText;
                dr["Quê quán"] = x.ChildNodes[3].InnerText;
                dt.Rows.Add(dr);
            }
            grvNhanVien.DataSource = dt;
        }


        public void LoadDanhMuc()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Tên danh mục");
            dt.Columns.Add("Mô tả");
            Model.DanhMuc danhmuc = new Model.DanhMuc();
            nodeListDanhMuc = danhmuc.getListDM();
            foreach (XmlNode x in nodeListDanhMuc)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = x.ChildNodes[0].InnerText;
                dr["Tên danh mục"] = x.ChildNodes[1].InnerText;
                dr["Mô tả"] = x.ChildNodes[2].InnerText;
                dt.Rows.Add(dr);
            }
            grvDanhMuc.DataSource = dt;
        }
        private void btnAddNV_Click(object sender, EventArgs e)
        {
            String ten = txtTenNV.Text;
            int idcv = comboBoxCV.SelectedIndex + 1;
            String quequan = txtDiaChi.Text;

            if (ten == "" || idcv == null || quequan == "")
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin");
            }
            else
            {
                //them san pham vao file xml
                Model.NhanVien nhanvien = new Model.NhanVien();
                nhanvien.AddNhanVien(idcv, ten, quequan);

                MessageBox.Show("Đã thêm thành công");
                ResetData();

                // Cập nhật lại DataGridView
                LoadNhanVien();
            }
        }

        private void grvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
           {
            if (e.RowIndex >= 0) // Kiểm tra xem người dùng đã chọn một hàng chưa
            {
                DataGridViewRow selectedRow = grvNhanVien.Rows[e.RowIndex];

                // Lấy dữ liệu từ các ô trong hàng được chọn và hiển thị lên các TextBox
                txtMNV.Text = selectedRow.Cells[0].Value.ToString();
                txtTenNV.Text = selectedRow.Cells[2].Value.ToString();
                txtDiaChi.Text = selectedRow.Cells[3].Value.ToString();
                string chucvu = selectedRow.Cells[1].Value.ToString();
                comboBoxCV.SelectedItem = chucvu;

            }
        }

        private void btnUpdateNV_Click(object sender, EventArgs e)
        {
            int idnhanvien = int.Parse(txtMNV.Text);
            String ten = txtTenNV.Text;
            int idcv = comboBoxCV.SelectedIndex + 1;
            String quequan = txtDiaChi.Text;

            //update san pham vao file xml
            Model.NhanVien nhanvien = new Model.NhanVien();
            nhanvien.UpdateNhanVien(idnhanvien, idcv, ten, quequan);

            MessageBox.Show("Đã update thành công");
            ResetData();

            // Cập nhật lại DataGridView
            LoadNhanVien();
        }

        private void btnDeleteNV_Click(object sender, EventArgs e)
        {
            int idnhanvien = int.Parse(txtMNV.Text);
            if(idnhanvien == null)
            {
                MessageBox.Show("Bạn chưa chọn nhân viên");
            }
            else
            {
                 //update san pham vao file xml
                Model.NhanVien nhanvien = new Model.NhanVien();
                nhanvien.DeleteNhanVien(idnhanvien);

                MessageBox.Show("Đã xoa thành công");
                ResetData();

                // Cập nhật lại DataGridView
                LoadNhanVien();
            }
            

        }

  

        private void tbnThemDM_Click(object sender, EventArgs e)
        {
            String tendanhmuc = txtTenDM.Text;
            String mota = txtMoTa.Text;

            if ( tendanhmuc == "" || mota == "")
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin");
            }
            else
            {
                //them san pham vao file xml
                Model.DanhMuc danhmuc = new Model.DanhMuc();
                danhmuc.AddDanhMuc(tendanhmuc, mota);

                MessageBox.Show("Đã thêm thành công");
                ResetData();

                // Cập nhật lại DataGridView
                LoadDanhMuc();
            }
        }

        private void grvDanhMuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0) // Kiểm tra xem người dùng đã chọn một hàng chưa
            {
                DataGridViewRow selectedRow = grvDanhMuc.Rows[e.RowIndex];

                // Lấy dữ liệu từ các ô trong hàng được chọn và hiển thị lên các TextBox
                txtIDDM.Text = selectedRow.Cells[0].Value.ToString();
                txtTenDM.Text = selectedRow.Cells[1].Value.ToString();
                txtMoTa.Text = selectedRow.Cells[2].Value.ToString();
            }
        }

        private void btnSuaDM_Click(object sender, EventArgs e)
        {
            int iddanhmuc = int.Parse(txtIDDM.Text);
            if (iddanhmuc == null) {
                MessageBox.Show("Bạn chưa chọn danh mục");
           }
            
            String tendanhmuc = txtTenDM.Text;
            String mota = txtMoTa.Text;

            //update san pham vao file xml
            Model.DanhMuc danhmuc = new Model.DanhMuc();
            danhmuc.UpdateDanhMuc(iddanhmuc, tendanhmuc, mota);

            MessageBox.Show("Đã update thành công");
            ResetData();

            // Cập nhật lại DataGridView
            LoadDanhMuc();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnXoaDM_Click(object sender, EventArgs e)
        {
            String iddanhmuc = txtIDDM.Text;

            //delete san pham vao file xml
            Model.DanhMuc danhmuc = new Model.DanhMuc();
            danhmuc.DeleteDanhMuc(iddanhmuc);

            MessageBox.Show("Đã xoa thành công");
            ResetData();

            // Cập nhật lại DataGridView
            LoadDanhMuc();
        }


        public void ResetData()
        {
            txtMNV.Text = "";
            txtTenNV.Text = "";
            txtDiaChi.Text = "";
            comboBoxCV.Text = null;
            txtIDDM.Text = "";
            txtTenDM.Text = "";
            txtMoTa.Text = "";
        }

        
    }
}
