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
    public partial class ManageAccount : Form
    {
        QuanLyXeKhachEntities db = new QuanLyXeKhachEntities();

        public ManageAccount()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            serviceManage data = new serviceManage();
            var list = data.getAllAcc();
            dataGridView1.DataSource = list;
        }
        private void ManageAccount_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0 || index >= dataGridView1.RowCount)
            {
                return;
            }
            DataGridViewRow row = dataGridView1.Rows[index];
            string tenNv = Convert.ToString(row.Cells[2].Value);
            string chucVu =Convert.ToString( row.Cells[3].Value);
            string tenTK = Convert.ToString(row.Cells[4].Value);
            string matKhau=  Convert.ToString(row.Cells[5].Value);

            txtNameEmp.Text = tenNv;
            txtPosition.Text = chucVu;
            txtUserName.Text = tenTK;
            txtPass.Text = matKhau;


        }
        /*
         * find phải tìm dwauj trên khóa chính
         */

        private void btnChangeDataAcc_Click(object sender, EventArgs e)
        {
            String tenTK = dataGridView1.SelectedCells[0].OwningRow.Cells["TenTK"].Value.ToString();
            TaiKhoan tk = db.TaiKhoans.Find(tenTK);
            //nv.stt = Convert.ToInt32(txtMaNV.Text);
            tk.TenNV = txtNameEmp.Text;
            tk.ChucVu = txtPosition.Text;
            tk.TenTK = txtUserName.Text;
            tk.MatKhau = txtPass.Text;
            db.SaveChanges();
            LoadData();
        }
        //dua vao khoa chinh de xóa
        private void btnDeleteAcc_Click(object sender, EventArgs e)
        {
            string tenTK = txtUserName.Text;
            TaiKhoan tk = db.TaiKhoans.Where(p => p.TenTK == tenTK).SingleOrDefault();
            db.TaiKhoans.Remove(tk);
            db.SaveChanges();
            LoadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("bạn có thật sự muốn thoát ?", "thoát ứng dụng", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
