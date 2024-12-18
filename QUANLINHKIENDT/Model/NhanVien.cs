using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QUANLINHKIENDT.Model
{
    class NhanVien
    {
        XMLFile XmlFile = new XMLFile();
        private string connectionString = "Data Source=LAPTOP-GR95O5RQ\\HUYENTRANG;Initial Catalog=dbQuanLyLinhKienPC;Integrated Security=True";

        public XmlNodeList getListNhanVien()
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("NhanVien.xml");
            XmlNodeList nodeList = XDoc.SelectNodes("/NhanViens/NhanVien");
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode a = nodeList[i];
            }
            return nodeList;

        }

        public void AddNhanVien(int idchucvu, String tennhanvien, String quequan)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("NhanVien.xml");
            XmlNode nhanVienNodes = XDoc.SelectSingleNode("/NhanViens");
            XmlNodeList nhanVienNode = nhanVienNodes.SelectNodes("NhanVien");
            XmlNodeList idNodes = XDoc.SelectNodes("//NhanViens/NhanVien/IDNhanVien");

            XmlElement newNhanVien = XDoc.CreateElement("NhanVien");
            //id
            int idnhanvien = int.Parse(idNodes[idNodes.Count - 1].InnerText);
            XmlElement newIDSP = XDoc.CreateElement("IDNhanVien");
            newIDSP.InnerText = (idnhanvien + 1).ToString();
            //idchucvu
            XmlElement newIDCV = XDoc.CreateElement("IDChucVu");
            newIDCV.InnerText = idchucvu.ToString();
            //ten
            XmlElement newTenNhanVien = XDoc.CreateElement("tenNhanVien");
            newTenNhanVien.InnerText = tennhanvien;
            //mota
            XmlElement newQueQuan = XDoc.CreateElement("queQuan");
            newQueQuan.InnerText = quequan;

            // Thêm các phần tử con vào phần tử Product
            newNhanVien.AppendChild(newIDSP);
            newNhanVien.AppendChild(newIDCV);
            newNhanVien.AppendChild(newTenNhanVien);
            newNhanVien.AppendChild(newQueQuan);

            // Thêm phần tử SanPham mới vào tệp XML
            nhanVienNodes.AppendChild(newNhanVien);

            // Lưu tệp XML sau khi thêm dữ liệu mới
            XDoc.Save("NhanVien.xml");
        }

        public void UpdateNhanVien(int idnhanvien, int idchucvu, String ten, String quenquan)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("NhanVien.xml");

            // Tạo biểu thức XPath dựa trên biến idSanPham
            string xPathExpression = $"/NhanViens/NhanVien[IDNhanVien = '{idnhanvien}']";
            XmlNodeList nodeList = XDoc.SelectNodes(xPathExpression);

            if (nodeList.Count > 0)
            {
                // Lấy phần tử cần cập nhật (ví dụ: <tenSanPham>)
                XmlNode newIDChucVu = nodeList[0].SelectSingleNode("IDChucVu");
                XmlNode newTenNhanVien = nodeList[0].SelectSingleNode("tenNhanVien");
                XmlNode newQueQuan = nodeList[0].SelectSingleNode("queQuan");

                // Thay đổi giá trị của phần tử cần cập nhật
                if (idnhanvien != null)
                    newTenNhanVien.InnerText = ten;
                newIDChucVu.InnerText = idchucvu.ToString();
                newQueQuan.InnerText = quenquan;

            }

            // Lưu lại tệp XML sau khi cập nhật
            XDoc.Save("NhanVien.xml");
        }

        public void DeleteNhanVien(int idnhanvien)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("NhanVien.xml");
            XmlNode nodeCanXoa = XDoc.SelectSingleNode($"/NhanViens/NhanVien[IDNhanVien = '{idnhanvien}']");

            if (nodeCanXoa != null)
            {
                // Xóa node nếu nó được tìm thấy
                XmlNode parent = nodeCanXoa.ParentNode;
                parent.RemoveChild(nodeCanXoa);

                // Lưu lại tệp XML sau khi xóa
                XDoc.Save("NhanVien.xml");

                MessageBox.Show("Đã xóa phần tử có IDSanPham = " + idnhanvien, "Thành công");
            }
            else
            {
                MessageBox.Show("Không tìm thấy phần tử cần xóa trong tệp XML.", "Lỗi");
            }
        }

        public void InsertXmlDataToDatabase()
        {
            try
            {
                // Kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String queryDelete = "delete NHANVIEN";
                    SqlCommand command = new SqlCommand(queryDelete, connection);
                    command.ExecuteNonQuery();
                    // Đọc dữ liệu từ tệp XML
                    XmlDocument XDoc = XmlFile.getXmlDocument("NhanVien.xml");
                    // Lấy danh sách các phần tử HoaDon từ XML
                    XmlNodeList nodeList = XDoc.SelectNodes("/NhanViens/NhanVien");
                    // Lặp qua từng phần tử và chèn vào cơ sở dữ liệu
                    foreach (XmlNode nhanvienNode in nodeList)
                    {
                        int idnhanvien = int.Parse(nhanvienNode.SelectSingleNode("IDNhanVien").InnerText);
                        int idchucvu = int.Parse(nhanvienNode.SelectSingleNode("IDChucVu").InnerText);
                        string ten = nhanvienNode.SelectSingleNode("tenNhanVien").InnerText;
                        string quequan = nhanvienNode.SelectSingleNode("queQuan").InnerText;
                        // Thực hiện câu lệnh SQL Insert
                        string insertQuery = $"insert into NHANVIEN(idChucVu,tenNhanVien,queQuan)" +
                            $" VALUES ({idchucvu}, N'{ten}', N'{quequan}')";

                        using (command = new SqlCommand(insertQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                //MessageBox.Show("Dữ liệu đã được chèn thành công vào cơ sở dữ liệu.", "Thành công");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lỗi");
            }
        }
    }
}
