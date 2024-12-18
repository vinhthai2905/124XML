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
    public partial class LichSuThanhToan : Form
    {
        Model.XMLFile XmlFile = new Model.XMLFile();
        XmlNodeList nodeListHoaDon, nodeListSanPham;
        private Dictionary<string, string> tenSanPham = new Dictionary<string, string>();
        public LichSuThanhToan()
        {
            //Danh muc
            Model.SanPham sanpham = new Model.SanPham();
            nodeListSanPham = sanpham.getSanPham();
            foreach (XmlNode x in nodeListSanPham)
            {
                tenSanPham.Add(x.ChildNodes[0].InnerText, x.ChildNodes[1].InnerText);
            }
            InitializeComponent();
        }


        public void LoadHoaDon()
        {
            Model.HoaDon hoadon = new Model.HoaDon();
            nodeListHoaDon = hoadon.getListHoaDon();
            string ten = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("TenSanPham");
            dt.Columns.Add("TenKhachHang");
            dt.Columns.Add("Số lượng");
            dt.Columns.Add("Đơn giá");
            dt.Columns.Add("Tổng tiền");
            dt.Columns.Add("Ngày thanh toán");
            foreach (XmlNode x in nodeListHoaDon)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = x.ChildNodes[0].InnerText;
                String idsanpham = x.ChildNodes[1].InnerText;
                if (tenSanPham.ContainsKey(idsanpham))
                {
                     ten = tenSanPham[idsanpham];
                    dr["TenSanPham"] = ten;
                }
                dr["TenKhachHang"] = x.ChildNodes[2].InnerText;
                dr["Số lượng"] = x.ChildNodes[3].InnerText;
                dr["Đơn giá"] = x.ChildNodes[4].InnerText;
                dr["Tổng tiền"] = x.ChildNodes[5].InnerText;
                dr["Ngày thanh toán"] = x.ChildNodes[6].InnerText;
                dt.Rows.Add(dr);
            }
            grvHoaDon.DataSource = dt;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            
            String textTimKiem = txtTimKiem.Text.ToLower();
            LoadHoaDon();

            if (grvHoaDon.DataSource is DataTable dataTable)
            {
               
                DataView dataView = dataTable.DefaultView;

                string escapedText = textTimKiem.Replace("'", "''");

                dataView.RowFilter = $"TenKhachHang LIKE '%{escapedText}%' OR TenSanPham LIKE '%{escapedText}%'";

                grvHoaDon.DataSource = dataView;
            }
            else
            {
                // Xử lý trường hợp DataSource không phải là DataTable
                MessageBox.Show("Dữ liệu không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadHoaDon();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu formMenu = new Menu();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Model.HoaDon hoadon = new Model.HoaDon();
            hoadon.ExportToExcel();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void LichSuThanhToan_Load(object sender, EventArgs e)
        {
          
            this.Size = new System.Drawing.Size(820, 600);
            LoadHoaDon();
        }
    }
}
