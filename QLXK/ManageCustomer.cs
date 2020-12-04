using System;
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
    public partial class ManageCustomer : Form
    {
        QuanLyXeKhachEntities db = new QuanLyXeKhachEntities();
        public string dataAdd;
        public ManageCustomer()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            serviceManage data = new serviceManage();
            var list = data.getAllCustomer();
            dataGridView1.DataSource = list;
        }

        private void thôngTinNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var userInfor in db.TaiKhoans)
            {
                if (userInfor.TenTK == dataAdd)
                {
                    MessageBox.Show("Tên người dùng: " + userInfor.TenNV + "\nChức vụ: " + userInfor.ChucVu + "\nTên tài khoản: " + userInfor.TenTK);
                 
                }
            }
        }

      
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0 || index >= dataGridView1.RowCount)
            {
                return;
            }
            // get data in cells of gridView
            DataGridViewRow row = dataGridView1.Rows[index];
            String stt = Convert.ToString(row.Cells[0].Value);
            String id = Convert.ToString(row.Cells[1].Value);
            String name = Convert.ToString(row.Cells[2].Value);
            String phone = Convert.ToString(row.Cells[3].Value);
            bool isMale = Convert.ToBoolean(row.Cells[4].Value);

            //upload UI
            txtStt.Text = stt;
            txtMaKh.Text = id;
            txtCustomerName.Text = name;
            txtCustomerPhoneNum.Text = phone;
            radMaleCus.Checked = isMale;
            radFemaleCus.Checked = !isMale;

        

        }

        private void ManageCustomer_Load(object sender, EventArgs e)
        {
            LoadData();
            foreach (var userInfor in db.TaiKhoans)
            {
                if (userInfor.TenTK == dataAdd)
                {
                    txtTenTK.Text = userInfor.TenTK;
                }
            }
            int index = 0;
            foreach (var customer in db.KhachHangs)
            {
          
                if (customer.Sdt==null)
                {
                    DataGridViewRow row = dataGridView1.Rows[index];
                    //String stt = Convert.ToString(row.Cells[0].Value);
                    String id = Convert.ToString(row.Cells[1].Value);
                    String name = Convert.ToString(row.Cells[2].Value);
                    String phone = Convert.ToString(row.Cells[3].Value);
                    bool isMale = Convert.ToBoolean(row.Cells[4].Value);

                    //upload UI
                    //txtStt.Text = stt;
                    txtMaKh.Text = id;
                    txtCustomerName.Text = name;
                    txtCustomerPhoneNum.Text = phone;
                    radMaleCus.Checked = isMale;
                    radFemaleCus.Checked = !isMale;
                    break;
                }
                else
                    index = index + 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            String Makh=txtMaKh.Text;
            KhachHang kh = db.KhachHangs.Where(p => p.MaKH == Makh).SingleOrDefault();
            PhieuVe pv = db.PhieuVes.Where(p => p.MaKH == Makh).SingleOrDefault();
            db.PhieuVes.Remove(pv);
            db.KhachHangs.Remove(kh);

            db.SaveChanges();
            LoadData();
        }
        public void Clear()
        {
            txtMaKh.Text = "";
            txtCustomerName.Text = "";
            txtStt.Text = "";
            radFemaleCus.Checked = false;
            radMaleCus.Checked = false;
            LoadData();
        }
        private void btnChange_Click(object sender, EventArgs e)
        {
            if(!txtCustomerName.Text.Equals("")|| !txtMaKh.Text.Equals(""))
            {
                String Makh = txtMaKh.Text;
                KhachHang kh = db.KhachHangs.Where(p => p.MaKH == Makh).SingleOrDefault();
                kh.TenKH = txtCustomerName.Text;
                kh.Sdt = txtCustomerPhoneNum.Text;
                kh.GioiTinh = radMaleCus.Checked ? true : false;
                db.SaveChanges();
                LoadData();
            }
            else
            {
                String id = dataGridView1.SelectedCells[0].OwningRow.Cells["MaKH"].Value.ToString();
                KhachHang kh = db.KhachHangs.Find(id);
                //kh.stt = Convert.ToInt32(txtStt.Text);
                kh.TenKH = txtCustomerName.Text;
                kh.Sdt = txtCustomerPhoneNum.Text;
                kh.GioiTinh = radMaleCus.Checked ? true : false;
                db.SaveChanges();
                LoadData();
            }
                
         
        }

        private void btnDeleteTextBox_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Boolean flag = false;
            string nameFind = txtFind.Text;
            //int stt = Convert.ToInt32(txtFind.Text);
            foreach (var kh in db.KhachHangs)
            {
                if (kh.TenKH.Equals(nameFind) || kh.Sdt.Equals(nameFind) || kh.MaKH.Equals(nameFind))
                {
                    flag = true;
                }
            }
            if (flag)
            {

                var list2 = db.KhachHangs.Where(p => p.TenKH == nameFind ||p.Sdt == nameFind || p.MaKH == nameFind).ToList();
                dataGridView1.DataSource = list2;

                /*var list2 = (from a in db.nhanViens where a.Ma_nv == stt select a).ToList();
                dataGridView1.DataSource = list2;*/
            }
            else MessageBox.Show("Không tìm thấy dữ liệu");
        }

        private void QLTTCDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtTenTK.Text.Contains("employee"))
            {
                ManageInfoTrip manageInfoTrip = new ManageInfoTrip();
                this.Hide();
                manageInfoTrip.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("người dùng hiện tại không có quyền truy cập chức năng này");
                QLTTCDToolStripMenuItem.Enabled = false;
            }
            


        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
