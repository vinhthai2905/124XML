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
    class DangNhap
    {
        XMLFile XmlFile = new XMLFile();
        // j7DZsDebY0iNI3E
        private string connectionString = "Data Source=LAPTOP-GR95O5RQ\\HUYENTRANG;Initial Catalog=dbQuanLyLinhKienPC;Integrated Security=True";
        public String Login(String userName, String password)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("TaiKhoan.xml");
            XmlNodeList nodeList = XDoc.SelectNodes("/TaiKhoans/TaiKhoan[username = '" + userName + "']");

            if (nodeList.Count != 0)
                if (nodeList[0].ChildNodes[2].InnerText.Equals(password))
                {
                    return nodeList[0].ChildNodes[0].InnerText;
                }

                else
                {
                    return "";
                }
            return "";
        }

        public void InsertXmlDataToDatabase()
        {
            try
            {
                // Kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String queryDelete = "delete TAIKHOAN";
                    SqlCommand command = new SqlCommand(queryDelete, connection);
                    command.ExecuteNonQuery();

                    // Đọc dữ liệu từ tệp XML
                    XmlDocument XDoc = XmlFile.getXmlDocument("TaiKhoan.xml");

                    // Lấy danh sách các phần tử HoaDon từ XML
                    XmlNodeList nodeList = XDoc.SelectNodes("/TaiKhoans/TaiKhoan");

                    // Lặp qua từng phần tử và chèn vào cơ sở dữ liệu
                    foreach (XmlNode taikhoanNode in nodeList)
                    {
                        int idTK = int.Parse(taikhoanNode.SelectSingleNode("IDTaiKhoan").InnerText);
                        string username = taikhoanNode.SelectSingleNode("username").InnerText;
                        string password = taikhoanNode.SelectSingleNode("password").InnerText;
                        string hoTen = taikhoanNode.SelectSingleNode("hoTen").InnerText;
                        string SDT = taikhoanNode.SelectSingleNode("SDT").InnerText;
                        string diaChi = taikhoanNode.SelectSingleNode("diaChi").InnerText;
                        int trangThai = int.Parse(taikhoanNode.SelectSingleNode("trangThai").InnerText);
                        int IDChucVu = int.Parse(taikhoanNode.SelectSingleNode("IDChucVu").InnerText);
                        // Thực hiện câu lệnh SQL Insert
                        string insertQuery = $"insert into TAIKHOAN(IDTaiKhoan,username,[password],hoTen,SDT,diaChi,trangThai,IDChucVu)" +
                            $" VALUES ('{idTK}','{username}', '{password}', N'{hoTen}', '{SDT}', N'{diaChi}', {trangThai}, {IDChucVu})";
                        using (command = new SqlCommand(insertQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Lỗi");
            }
        }
    }
}
