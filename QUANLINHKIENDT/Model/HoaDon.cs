using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SqlClient;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;

namespace QUANLINHKIENDT.Model
{
    class HoaDon
    {
        XMLFile XmlFile = new XMLFile();
        private string connectionString = "Data Source=LAPTOP-GR95O5RQ\\HUYENTRANG;Initial Catalog=dbQuanLyLinhKienPC;Integrated Security=True";
        public XmlNodeList getListHoaDon()
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("HoaDon.xml");
            XmlNodeList nodeList = XDoc.SelectNodes("/HoaDons/HoaDon");
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode a = nodeList[i];
            }
            return nodeList;

        }

        public void AddHoaDon(int idSaPham, String tenKhachHang, int soLuong, int donGia, int tongTien, string ngayThanhToan)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("HoaDon.xml");
            XmlNode hoaDonNodes = XDoc.SelectSingleNode("/HoaDons");
            XmlNodeList hoaDonNode = hoaDonNodes.SelectNodes("HoaDon");
            XmlNode lastHoaDon = hoaDonNode[hoaDonNode.Count - 1];

            //tao element san pham moi
            XmlElement newHoaDon = XDoc.CreateElement("HoaDon");
            //id
            int idhoadon = int.Parse(lastHoaDon.SelectSingleNode("IDHoaDon").InnerText);
            XmlElement newIDHD = XDoc.CreateElement("IDHoaDon");
            newIDHD.InnerText = (idhoadon + 1).ToString();
            //idsanpham
            XmlElement newIDSP = XDoc.CreateElement("IDSanPham");
            newIDSP.InnerText = idSaPham.ToString();
            //ten khach hang
            XmlElement newTenKhachHang = XDoc.CreateElement("tenKhachHang");
            newTenKhachHang.InnerText = tenKhachHang;

            //soluong
            XmlElement newSoLuong = XDoc.CreateElement("soLuong");
            newSoLuong.InnerText = soLuong.ToString();

            //dongia
            XmlElement newDonGia = XDoc.CreateElement("donGia");
            newDonGia.InnerText = donGia.ToString();

            //hinhanh
            XmlElement newTongTien = XDoc.CreateElement("tongTien");
            newTongTien.InnerText = tongTien.ToString();
            //danhmuc
            XmlElement newNgayTT = XDoc.CreateElement("ngayThanhToan");
            newNgayTT.InnerText = ngayThanhToan;


            // Thêm các phần tử con vào phần tử Product
            newHoaDon.AppendChild(newIDHD);
            newHoaDon.AppendChild(newIDSP);
            newHoaDon.AppendChild(newTenKhachHang);
            newHoaDon.AppendChild(newSoLuong);
            newHoaDon.AppendChild(newDonGia);
            newHoaDon.AppendChild(newTongTien);
            newHoaDon.AppendChild(newNgayTT);

            // Thêm phần tử SanPham mới vào tệp XML
            hoaDonNodes.AppendChild(newHoaDon);

            // Lưu tệp XML sau khi thêm dữ liệu mới
            XDoc.Save("HoaDon.xml");
        }

        public string LoadDataAndGenerateHtml()
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("HoaDon.xml");

            // Bắt đầu tạo chuỗi HTML
            string htmlContent = "<html><body><h2>Lịch Sử Thanh Toán<table border='1'><thead><tr><th>ID Hóa Đơn</th><th>ID Sản Phẩm</th><th>Tên Khách Hàng</th><th>Số Lượng</th><th>Đơn Giá</th><th>Tổng Tiền</th><th>Ngày Thanh Toán</th></tr></thead><tbody>";

            // Lấy danh sách các phần tử HoaDon từ XML
            XmlNodeList hoaDonList = XDoc.SelectNodes("/HoaDons/HoaDon");

            // Lặp qua từng phần tử và thêm vào chuỗi HTML
            foreach (XmlNode hoaDonNode in hoaDonList)
            {
                string idHoaDon = hoaDonNode.SelectSingleNode("IDHoaDon").InnerText;
                string idSanPham = hoaDonNode.SelectSingleNode("IDSanPham").InnerText;
                string tenKhachHang = hoaDonNode.SelectSingleNode("tenKhachHang").InnerText;
                string soLuong = hoaDonNode.SelectSingleNode("soLuong").InnerText;
                string donGia = hoaDonNode.SelectSingleNode("donGia").InnerText;
                string tongTien = hoaDonNode.SelectSingleNode("tongTien").InnerText;
                string ngayThanhToan = hoaDonNode.SelectSingleNode("ngayThanhToan").InnerText;

                // Thêm dữ liệu vào chuỗi HTML
                htmlContent += $"<tr><td>{idHoaDon}</td><td>{idSanPham}</td><td>{tenKhachHang}</td><td>{soLuong}</td><td>{donGia}</td><td>{tongTien}</td><td>{ngayThanhToan}</td></tr>";
            }

            // Kết thúc chuỗi HTML
            htmlContent += "</tbody></table></h2></body></html>";

            return htmlContent;
        }
        public void DeleteByIdSanPham(int idSanPham)
        {
            XmlDocument xDoc = XmlFile.getXmlDocument("HoaDon.xml");

            // Lấy danh sách các phần tử HoaDon từ XML
            XmlNodeList nodeList = xDoc.SelectNodes($"/HoaDons/HoaDon[IDSanPham = '{idSanPham}']");
            if (nodeList.Count > 0)
            {
                foreach (XmlNode node in nodeList)
                {
                    // Xóa node nếu nó được tìm thấy
                    XmlNode parent = node.ParentNode;
                    parent.RemoveChild(node);

                    // Lưu lại tệp XML sau khi xóa
                    xDoc.Save("HoaDon.xml");

                }

            }
            else
            {
                MessageBox.Show("Lỗi", "Không tìm thấy phần tử cần xóa trong tệp XML.");
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
                    String deleteData = "delete HOADON";
                    using (SqlCommand command = new SqlCommand(deleteData, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Đọc dữ liệu từ tệp XML
                    XmlDocument XDoc = XmlFile.getXmlDocument("HoaDon.xml");

                    // Lấy danh sách các phần tử HoaDon từ XML
                    XmlNodeList nodeList = XDoc.SelectNodes("/HoaDons/HoaDon");

                    // Lặp qua từng phần tử và chèn vào cơ sở dữ liệu
                    foreach (XmlNode hoadonNode in nodeList)
                    {
                        int idHoaDon = int.Parse(hoadonNode.SelectSingleNode("IDHoaDon").InnerText);
                        int idSanPham = int.Parse(hoadonNode.SelectSingleNode("IDSanPham").InnerText);
                        string tenKhachHang = hoadonNode.SelectSingleNode("tenKhachHang").InnerText;
                        int soluong = int.Parse(hoadonNode.SelectSingleNode("soLuong").InnerText);
                        double donGia = double.Parse(hoadonNode.SelectSingleNode("donGia").InnerText);
                        double tongTien = double.Parse(hoadonNode.SelectSingleNode("tongTien").InnerText);
                        string ngayThanhToan = hoadonNode.SelectSingleNode("ngayThanhToan").InnerText;
                        // Thực hiện câu lệnh SQL Insert
                        string insertQuery = $"INSERT INTO HOADON (idHoaDon,idSanPham,tenKhachHang,soLuong,donGia,tongTien,ngayThanhToan)" +
                            $" VALUES ({idHoaDon},{idSanPham},N'{tenKhachHang}',{soluong},{donGia} , {tongTien},'{ngayThanhToan}')";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        public void ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                // Tạo một worksheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("HoaDon_Export");
                // Tạo header
                worksheet.Cells[1, 1].Value = "ID Hóa Đơn";
                worksheet.Cells[1, 2].Value = "ID Sản Phẩm";
                worksheet.Cells[1, 3].Value = "Tên Khách Hàng";
                worksheet.Cells[1, 4].Value = "Số Lượng";
                worksheet.Cells[1, 5].Value = "Đơn Giá";
                worksheet.Cells[1, 6].Value = "Tổng Tiền";
                worksheet.Cells[1, 7].Value = "Ngày Thanh Toán";
                // Đọc dữ liệu từ tệp XML
                XmlDocument XDoc = XmlFile.getXmlDocument("HoaDon.xml");
                XmlNodeList nodeList = XDoc.SelectNodes("/HoaDons/HoaDon");
                // Ghi dữ liệu vào file Excel
                int row = 2;
                foreach (XmlNode hoadonNode in nodeList)
                {
                    worksheet.Cells[row, 1].Value = int.Parse(hoadonNode.SelectSingleNode("IDHoaDon").InnerText);
                    worksheet.Cells[row, 2].Value = int.Parse(hoadonNode.SelectSingleNode("IDSanPham").InnerText);
                    worksheet.Cells[row, 3].Value = hoadonNode.SelectSingleNode("tenKhachHang").InnerText;
                    worksheet.Cells[row, 4].Value = int.Parse(hoadonNode.SelectSingleNode("soLuong").InnerText);
                    worksheet.Cells[row, 5].Value = int.Parse(hoadonNode.SelectSingleNode("donGia").InnerText);
                    worksheet.Cells[row, 6].Value = int.Parse(hoadonNode.SelectSingleNode("tongTien").InnerText);
                    worksheet.Cells[row, 7].Value = hoadonNode.SelectSingleNode("ngayThanhToan").InnerText;
                    row++;
                }
                // Thêm ô tổng cộng số lượng ở cuối cùng và tổng tiền
                worksheet.Cells[row + 2, 3].Value = "Tổng cộng số lượng";
                worksheet.Cells[row + 2, 4].Formula = $"SUM(D2:D{row - 1})";

                worksheet.Cells[row + 3, 3].Value = "Tổng cộng tiền";
                worksheet.Cells[row + 3, 4].Formula = $"SUM(F2:F{row - 1})";

                // Lưu file Excel
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                    excelPackage.SaveAs(excelFile);
                    MessageBox.Show("Xuất file Excel thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
