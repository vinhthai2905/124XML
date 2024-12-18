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
    class SanPham
    {
        XMLFile XmlFile = new XMLFile();
        private string connectionString = "Data Source=LAPTOP-GR95O5RQ\\HUYENTRANG;Initial Catalog=dbQuanLyLinhKienPC;Integrated Security=True";
        public XmlNodeList getSanPham()
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("SanPham.xml");
            XmlNodeList nodeList = XDoc.SelectNodes("/SanPhams/SanPham");
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode a = nodeList[i];
            }
            return nodeList;
        }

        public void AddSanPham(String tenSP, String moTa, int donGia, int idNSX, String baoHanh, String tinhTrang, int soLuong, String hinhAnh, int idDanhMuc)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("SanPham.xml");
            XmlNode sanPhamsNode = XDoc.SelectSingleNode("/SanPhams");
            XmlNodeList sanPhamNodes = sanPhamsNode.SelectNodes("SanPham");
            XmlNode lastSanPham = sanPhamNodes[sanPhamNodes.Count - 1];

            //tao element san pham moi
            XmlElement newSanPham = XDoc.CreateElement("SanPham");
            //id
            int idsanpham = int.Parse(lastSanPham.SelectSingleNode("IDSanPham").InnerText);
            XmlElement newIDSP = XDoc.CreateElement("IDSanPham");
            int idSanPhamMoi = idsanpham++;
            while (checkExistID(idSanPhamMoi))
            {
                idSanPhamMoi++;
            }
            newIDSP.InnerText = idSanPhamMoi.ToString();
            //ten
            XmlElement newTenSanPham = XDoc.CreateElement("tenSanPham");
            newTenSanPham.InnerText = tenSP;
            //mota
            XmlElement newMota = XDoc.CreateElement("moTa");
            newMota.InnerText = moTa;
            //dongia
            XmlElement newDonGia = XDoc.CreateElement("donGia");
            newDonGia.InnerText = donGia.ToString();
            //idnhasanxuat
            XmlElement newIDNSX = XDoc.CreateElement("IDNhaSanXuat");
            newIDNSX.InnerText = idNSX.ToString();
            //baohanh
            XmlElement newBaoHanh = XDoc.CreateElement("baoHanh");
            newBaoHanh.InnerText = baoHanh;
            //tinhtrang
            XmlElement newTinhTrang = XDoc.CreateElement("tinhTrangSanPham");
            newTinhTrang.InnerText = tinhTrang;
            //soluong
            XmlElement newSoLuong = XDoc.CreateElement("soLuong");
            newSoLuong.InnerText = soLuong.ToString();
            //hinhanh
            XmlElement newHinhAnh = XDoc.CreateElement("hinHanh");
            newHinhAnh.InnerText = hinhAnh;
            //danhmuc
            XmlElement newIDDM = XDoc.CreateElement("IDDanhMuc");
            newIDDM.InnerText = idDanhMuc.ToString();


            // Thêm các phần tử con vào phần tử Product
            newSanPham.AppendChild(newIDSP);
            newSanPham.AppendChild(newTenSanPham);
            newSanPham.AppendChild(newMota);
            newSanPham.AppendChild(newDonGia);
            newSanPham.AppendChild(newIDNSX);
            newSanPham.AppendChild(newBaoHanh);
            newSanPham.AppendChild(newTinhTrang);
            newSanPham.AppendChild(newSoLuong);
            newSanPham.AppendChild(newHinhAnh);
            newSanPham.AppendChild(newIDDM);

            // Thêm phần tử SanPham mới vào tệp XML
            sanPhamsNode.AppendChild(newSanPham);

            // Lưu tệp XML sau khi thêm dữ liệu mới
            XDoc.Save("SanPham.xml");
        }
        public Boolean checkExistID(int id)
        {
            XmlDocument xDoc = XmlFile.getXmlDocument("SanPham.xml");
            String pathNode = $"/SanPhams/SanPham[IDSanPham='{id}']";
            XmlNodeList nodeList = xDoc.SelectNodes(pathNode);
            if (nodeList.Count > 0)
                return true;
            return false;
        }


        public void UpdateSanPham(int idsanpham, String tenSP, String moTa, int donGia, int idNSX, String baoHanh, String tinhTrang, int soLuong, String hinhAnh, int idDanhMuc)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("SanPham.xml");

            // Tạo biểu thức XPath dựa trên biến idSanPham
            string xPathExpression = $"/SanPhams/SanPham[IDSanPham = '{idsanpham}']";
            XmlNodeList nodeList = XDoc.SelectNodes(xPathExpression);

            if (nodeList.Count > 0)
            {
                // Lấy phần tử cần cập nhật (ví dụ: <tenSanPham>)
                XmlNode newTenSanPham = nodeList[0].SelectSingleNode("tenSanPham");
                XmlNode newMoTa = nodeList[0].SelectSingleNode("moTa");
                XmlNode newDonGia = nodeList[0].SelectSingleNode("donGia");
                XmlNode newIDNSX = nodeList[0].SelectSingleNode("IDNhaSanXuat");
                XmlNode newBaoHanh = nodeList[0].SelectSingleNode("baoHanh");
                XmlNode newTinhTrang = nodeList[0].SelectSingleNode("tinhTrangSanPham");
                XmlNode newSoLuong = nodeList[0].SelectSingleNode("soLuong");
                XmlNode newHinhAnh = nodeList[0].SelectSingleNode("hinHanh");
                XmlNode newIDDM = nodeList[0].SelectSingleNode("IDDanhMuc");

                // Thay đổi giá trị của phần tử cần cập nhật
                if (newTenSanPham != null || newMoTa != null || newDonGia != null || newIDNSX != null || newBaoHanh != null || newTinhTrang != null || newSoLuong != null || newHinhAnh != null || newIDDM != null)
                {
                    newTenSanPham.InnerText = tenSP;
                    newMoTa.InnerText = moTa;
                    newDonGia.InnerText = donGia.ToString();
                    newIDNSX.InnerText = idNSX.ToString();
                    newBaoHanh.InnerText = baoHanh;
                    newTinhTrang.InnerText = tinhTrang;
                    newSoLuong.InnerText = soLuong.ToString();
                    newHinhAnh.InnerText = hinhAnh;
                    newIDDM.InnerText = idDanhMuc.ToString();
                }

                // Lưu lại tệp XML sau khi cập nhật
                XDoc.Save("SanPham.xml");

            }
            else
            {
                MessageBox.Show("Không tìm thấy phần tử cần cập nhật trong tệp XML.", "Thành công");
            }
        }

        public void UpdateSoLuong(int idsanpham, int newSoLuong)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("SanPham.xml");
            // Tạo biểu thức XPath dựa trên biến idSanPham
            string xPathExpression = $"/SanPhams/SanPham[IDSanPham = '{idsanpham}']";
            XmlNodeList nodeList = XDoc.SelectNodes(xPathExpression);
            if (nodeList.Count > 0)
            {
                // Lấy phần tử cần cập nhật (ví dụ: <tenSanPham>)
                XmlNode soLuong = nodeList[0].SelectSingleNode("soLuong");
                // Thay đổi giá trị của phần tử cần cập nhật
                if (soLuong != null)
                {
                    soLuong.InnerText = (int.Parse(soLuong.InnerText) - newSoLuong).ToString();
                }
                // Lưu lại tệp XML sau khi cập nhật
                XDoc.Save("SanPham.xml");
            }
            else
            {
                MessageBox.Show("Error", "Không tìm thấy phần tử cần cập nhật trong tệp XML.");
            }
        }

        public void DeleteSanPham(int idsanpham)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("SanPham.xml");
            XmlNode nodeCanXoa = XDoc.SelectSingleNode($"/SanPhams/SanPham[IDSanPham = '{idsanpham}']");
            HoaDon hd = new HoaDon();
            if (nodeCanXoa != null)
            {
                // Xóa node nếu nó được tìm thấy
                XmlNode parent = nodeCanXoa.ParentNode;
                parent.RemoveChild(nodeCanXoa);

                // Lưu lại tệp XML sau khi xóa
                XDoc.Save("SanPham.xml");
                hd.DeleteByIdSanPham(idsanpham);
                MessageBox.Show("Đã xóa phần tử có IDSanPham = " + idsanpham, "Thành công");
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
                    String deleteData = "delete SANPHAM";
                    using (SqlCommand command = new SqlCommand(deleteData, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Đọc dữ liệu từ tệp XML
                    XmlDocument XDoc = XmlFile.getXmlDocument("SanPham.xml");

                    // Lấy danh sách các phần tử HoaDon từ XML
                    XmlNodeList nodeList = XDoc.SelectNodes("/SanPhams/SanPham");

                    // Lặp qua từng phần tử và chèn vào cơ sở dữ liệu
                    foreach (XmlNode sanphamNode in nodeList)
                    {
                        int idSanPham = int.Parse(sanphamNode.SelectSingleNode("IDSanPham").InnerText);
                        string tenSanPham = sanphamNode.SelectSingleNode("tenSanPham").InnerText;
                        string moTa = sanphamNode.SelectSingleNode("moTa").InnerText;
                        int donGia = int.Parse(sanphamNode.SelectSingleNode("donGia").InnerText);
                        int idNhaSanXuat = int.Parse(sanphamNode.SelectSingleNode("IDNhaSanXuat").InnerText);
                        string baoHanh = sanphamNode.SelectSingleNode("baoHanh").InnerText;
                        string tinhTrangSP = sanphamNode.SelectSingleNode("tinhTrangSanPham").InnerText;
                        int soluong = int.Parse(sanphamNode.SelectSingleNode("soLuong").InnerText);
                        string hinhanh = "1b_d55b96e7b5314601a6c9c877e1c606ec_large_405b5da5871c4d2aad42aa59665cfe6e_1024x1024.png";
                        int iddanhmuc = int.Parse(sanphamNode.SelectSingleNode("IDDanhMuc").InnerText);
                        // Thực hiện câu lệnh SQL Insert
                        string insertQuery = $"INSERT INTO SANPHAM (IDSanPham,tenSanPham,moTa,donGia,IDNhaSanXuat,baoHanh,tinhTrangSanPham,soLuong,hinHanh,IDDanhMuc)" +
                            $" VALUES ({idSanPham},N'{tenSanPham}', N'{moTa}', {donGia},{idNhaSanXuat},N'{baoHanh}', N'{tinhTrangSP}', {soluong}, '{hinhanh}', {iddanhmuc})";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }


                    }
                    HoaDon hoadon = new HoaDon();
                    hoadon.InsertXmlDataToDatabase();
                }

                //MessageBox.Show("Dữ liệu đã được chèn thành công vào cơ sở dữ liệu.", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }
    }
}
