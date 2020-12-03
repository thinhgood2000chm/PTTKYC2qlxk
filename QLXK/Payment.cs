using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services;
namespace QLXK
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }
        public string dataAdd;
        QuanLyXeKhachEntities db = new QuanLyXeKhachEntities();
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void thôngTinNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var userInfor in db.TaiKhoans)
            {
                if (userInfor.TenTK == dataAdd)
                {
                    MessageBox.Show("Tên người dùng: " + userInfor.TenNV + "\nChức vụ: " + userInfor.ChucVu + "\nTên tài khoản: " + userInfor.TenTK); ;
                }
            }
        }

        private void btnShowDataBox_Click(object sender, EventArgs e)
        {
            serviceManage data = new serviceManage();
            var list = data.getAllvehicle();
            cbIDveh.DataSource = list;
            cbIDveh.DisplayMember = "MaXe";

            var listLocation = data.getAllLocations();
            cbLocation.DataSource = listLocation;
            cbLocation.DisplayMember = "TebDD";
            cbCost.DataSource = listLocation;
            cbCost.DisplayMember = "GiaTien";

            foreach (var userInfor in db.TaiKhoans)
            {
                if (userInfor.TenTK == dataAdd)
                {
                    txtNameEmp.Text = userInfor.TenNV;
                    txtIdEmp.Text = userInfor.MaNV;

                }
            }
        }
      
        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtCusPay.Text) < Convert.ToInt32(cbCost.Text))
            {
                MessageBox.Show("Số tiền khách trả phải lớn hơn số tiền cần trả");
            }
            else
            {
                ArrayList temp = new ArrayList();
                ArrayList temp2 = new ArrayList();
                foreach (var user in db.KhachHangs)
                {

                    temp.Add(Convert.ToInt32(user.stt));
                   

                }
                KhachHang kh = new KhachHang()
                {
                    MaKH = "KH" + Convert.ToString(temp.Count + 1),
                    TenKH = txtNameCus.Text
                };
                db.KhachHangs.Add(kh);
                db.SaveChanges();



                foreach (var user in db.PhieuVes)
                {

                    temp2.Add(Convert.ToInt32(user.stt));


                }
                PhieuVe pv = new PhieuVe()
                {
                    MaPhieu = "PV" + Convert.ToString(temp2.Count + 1),
                    MaKH = "KH" + Convert.ToString(temp.Count+1),
                    TenKH = txtNameCus.Text,
                    TenNVBV = txtNameEmp.Text,
                    MaNVBV = txtIdEmp.Text,
                    SoTien = Convert.ToInt32(cbCost.Text),
                    ChoNgoi = txtSit.Text,
                    NgayKhoiHanh = Convert.ToDateTime(dtTimeStart.Text),
                    MaChuyenxe = cbIDveh.Text,


                    TenDD = cbLocation.Text
                };
                db.PhieuVes.Add(pv);
                db.SaveChanges();
                /*
                 * after add infor of hoaDon, move on QLKH to continue update data of customer
                 */
                ManageCustomer manageCustomer = new ManageCustomer();
                this.Hide();
                manageCustomer.ShowDialog();
                this.Show();
            }
        }

        private void txtNameEmp_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
