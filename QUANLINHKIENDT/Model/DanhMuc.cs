using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QUANLINHKIENDT.Model
{
    class DanhMuc
    {
        XMLFile XmlFile = new XMLFile();
        public XmlNodeList getListDM()
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("DanhMuc.xml");
            XmlNodeList nodeList = XDoc.SelectNodes("/DanhMucs/DanhMuc");
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode a = nodeList[i];
            }
            return nodeList;

        }

        public void AddDanhMuc(String tenDanhMuc, String moTa)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("DanhMuc.xml");
            XmlNode sanPhamsNode = XDoc.SelectSingleNode("/DanhMucs");
            XmlNodeList sanPhamNodes = sanPhamsNode.SelectNodes("DanhMuc");
            XmlNode lastDanhMuc = sanPhamNodes[sanPhamNodes.Count - 1]; ;

            XmlElement newDanhMuc = XDoc.CreateElement("DanhMuc");

            //id
            int iddanhmuc = int.Parse(lastDanhMuc.SelectSingleNode("IDDanhMuc").InnerText);
            XmlElement newIDDM = XDoc.CreateElement("IDDanhMuc");
            newIDDM.InnerText = (iddanhmuc + 1).ToString();

            //ten
            XmlElement newTenDanhMuc = XDoc.CreateElement("tenDanhMuc");
            newTenDanhMuc.InnerText = tenDanhMuc;
            //mota
            XmlElement newMoTa = XDoc.CreateElement("moTa");
            newMoTa.InnerText = moTa;

            // Thêm các phần tử con vào phần tử Product
            newDanhMuc.AppendChild(newIDDM);
            newDanhMuc.AppendChild(newTenDanhMuc);
            newDanhMuc.AppendChild(newMoTa);

            // Thêm phần tử SanPham mới vào tệp XML
            sanPhamsNode.AppendChild(newDanhMuc);

            // Lưu tệp XML sau khi thêm dữ liệu mới
            XDoc.Save("DanhMuc.xml");
        }

        public void UpdateDanhMuc(int iddanhmuc, String tendanhmuc, String mota)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("DanhMuc.xml");

            // Tạo biểu thức XPath dựa trên biến idSanPham
            string xPathExpression = $"/DanhMucs/DanhMuc[IDDanhMuc = '{iddanhmuc}']";
            XmlNodeList nodeList = XDoc.SelectNodes(xPathExpression);

            if (nodeList.Count > 0)
            {
                // Lấy phần tử cần cập nhật (ví dụ: <tenSanPham>)
                XmlNode newIDDanhMuc = nodeList[0].SelectSingleNode("IDDanhMuc");
                XmlNode newTenDanhMuc = nodeList[0].SelectSingleNode("tenDanhMuc");
                XmlNode newMoTa = nodeList[0].SelectSingleNode("moTa");

                // Thay đổi giá trị của phần tử cần cập nhật
                if (tendanhmuc != "")
                    newTenDanhMuc.InnerText = tendanhmuc;
                    newMoTa.InnerText = mota;

            }

            // Lưu lại tệp XML sau khi cập nhật
            XDoc.Save("DanhMuc.xml");
        }

        public void DeleteDanhMuc(String iddanhmuc)
        {
            int madanhmuc = int.Parse(iddanhmuc);
            XmlDocument XDoc = XmlFile.getXmlDocument("DanhMuc.xml");
            XmlNode nodeCanXoa = XDoc.SelectSingleNode($"/DanhMucs/DanhMuc[IDDanhMuc = '{madanhmuc}']");

            if (nodeCanXoa != null)
            {
                // Xóa node nếu nó được tìm thấy
                XmlNode parent = nodeCanXoa.ParentNode;
                parent.RemoveChild(nodeCanXoa);

                // Lưu lại tệp XML sau khi xóa
                XDoc.Save("DanhMuc.xml");

                MessageBox.Show("Đã xóa phần tử có IDSanPham = " + iddanhmuc, "Thành công");
            }
            else
            {
                MessageBox.Show("Lỗi", "Không tìm thấy phần tử cần xóa trong tệp XML.");
            }

        }
    }
}
