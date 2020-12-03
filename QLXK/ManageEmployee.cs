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
    public partial class ManageEmployee : Form
    {
        QuanLyXeKhachEntities db = new QuanLyXeKhachEntities();
        public ManageEmployee()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            serviceManage data = new serviceManage();
            var list = data.getAllEmp();
            dataGridView1.DataSource = list;
        }
        private void ManageEmployee_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void ClearData()
        {

            txtNameE.Text = "";
            txtPhoneEmp.Text = "";
            txtPosition.Text = "";
            dtBirth.Value = DateTime.Now;
            dtDayWork.Value = DateTime.Now;
            radFemale.Checked = false;
            radMale.Checked = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ArrayList temp = new ArrayList();
            foreach (var user in db.NhanViens)
            {

                temp.Add(Convert.ToInt32(user.stt));

            }

            if (txtPhoneEmp.Text == "" || txtNameE.Text == "" || txtPosition.Text == "")
            {
                MessageBox.Show("Bị thiếu thông tin vui lòng nhập lại");
            }
            else
            {
                NhanVien nv = new NhanVien()
                {
                    MaNV = "NV" + Convert.ToString(temp.Count + 1),
                    TenNV = txtNameE.Text,
                    SDT = txtPhoneEmp.Text,
                    GioiTinh = radFemale.Checked ? true : false,
                    ChucVu = txtPosition.Text,
                    NgaySinh = dtBirth.Value,
                    NgayVaoLam = dtDayWork.Value
                };
                db.NhanViens.Add(nv);
                db.SaveChanges();
                LoadData();
                MessageBox.Show("thêm nhân viên thành công ");

                // after add successfullly, delete data in textbox

                ClearData();
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            String id = dataGridView1.SelectedCells[0].OwningRow.Cells["MaNV"].Value.ToString();
            NhanVien nv = db.NhanViens.Find(id);
            nv.stt = Convert.ToInt32(txtStt.Text);
            nv.TenNV = txtNameE.Text;
            nv.SDT = txtPhoneEmp.Text;
            nv.ChucVu = txtPosition.Text;
            nv.GioiTinh = radMale.Checked ? true : false;
            nv.NgaySinh = dtBirth.Value;
            nv.NgayVaoLam = dtDayWork.Value;
            db.SaveChanges();
            LoadData();
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
            String position = Convert.ToString(row.Cells[3].Value);
            bool isMale = Convert.ToBoolean(row.Cells[4].Value);
            String phone = Convert.ToString(row.Cells[5].Value);
            DateTime birth = Convert.ToDateTime(row.Cells[6].Value);
            DateTime work = Convert.ToDateTime(row.Cells[7].Value);

            //upload UI
            txtStt.Text = stt;
            txtMaSo.Text = id;
            txtNameE.Text = name;
            txtPhoneEmp.Text = phone;
            txtPosition.Text = position;
            radMale.Checked = isMale;
            radFemale.Checked = !isMale;
            dtBirth.Value = birth;
            dtDayWork.Value = work;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            String id = txtMaSo.Text;
            NhanVien nv = db.NhanViens.Where(p => p.MaNV == id).SingleOrDefault();
            db.NhanViens.Remove(nv);
            db.SaveChanges();
            LoadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Boolean flag = false;
            string nameFind = txtFind.Text;
            //int stt = Convert.ToInt32(txtFind.Text);
            foreach (var nv in db.NhanViens)
            {
                if (nv.TenNV.Equals(nameFind) || nv.ChucVu.Equals(nameFind) || nv.SDT.Equals(nameFind) || nv.MaNV.Equals(nameFind))
                {
                    flag = true;
                }
            }
            if (flag)
            {

                var list2 = db.NhanViens.Where(p => p.TenNV == nameFind || p.ChucVu == nameFind || p.SDT == nameFind || p.MaNV == nameFind).ToList();
                dataGridView1.DataSource = list2;

                /*var list2 = (from a in db.nhanViens where a.Ma_nv == stt select a).ToList();
                dataGridView1.DataSource = list2;*/
            }
            else MessageBox.Show("Không tìm thấy dữ liệu");
        }

        private void bbtnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void đăngKíToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.ShowDialog();
            this.Show();
        }

        private void hiênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageAccount manage = new ManageAccount();
            this.Hide();
            manage.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("cách ghi dữ liệu đối với các trường:\n Hướng dẫn viên: HDV \n Tài xế: TX \n bán vé và nhân viên văn phòng ghi bình thường ");
        }
    }
}
