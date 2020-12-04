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
    public partial class ManageInfoTrip : Form
    {
        public ManageInfoTrip()
        {
            InitializeComponent();
        }
        QuanLyXeKhachEntities db = new QuanLyXeKhachEntities();
        private void label7_Click(object sender, EventArgs e)
        {

        }
        public void LoadData()
        {
            serviceManage data = new serviceManage();
            var list = data.getAllThongTin();
            dataGridView1.DataSource = list;
        }
        private void btnShowData_Click(object sender, EventArgs e)
        {
            var list2 = db.NhanViens.Where(p => p.ChucVu == "TX").ToList();
            cbNameDriver.DataSource = list2;
            cbNameDriver.DisplayMember = "TenNV";
            cbIdDriver.DataSource = list2;
            cbIdDriver.DisplayMember = "MaNV";
       

            var list4 = db.NhanViens.Where(p => p.ChucVu == "HDV").ToList();
            cbNameTG.DataSource = list4;
            cbNameTG.DisplayMember = "TenNV";
            cbIdTG.DataSource = list4;
            cbIdTG.DisplayMember = "MaNV";

            serviceManage dataXK = new serviceManage();
            var list3 = dataXK.getAllvehicle();
            cbIdVeh.DataSource = list3;
            cbIdVeh.DisplayMember = "MaXe";

            serviceManage dataLocation = new serviceManage();
            var list5 = dataLocation.getAllLocations();
            cbNameLocation.DataSource = list5;
            cbNameLocation.DisplayMember = "TebDD";
        }
        public void ClearData()
        {
            txtStt.Text = "";
            cbIdDriver.Text = "";
            cbNameDriver.Text = "";
            cbIdTG.Text = "";
            cbNameTG.Text = "";
            cbIdVeh.Text = "";
            cbNameDriver.Text = "";
            cbNameLocation.Text = "";
            txtFind.Text = "";
            dtstar.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
            btnUpdate.Enabled = true;
            btnChange.Enabled = true;
            
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ArrayList temp = new ArrayList();
            foreach (var infor in db.ThongTinChuyenDis)
            {

                temp.Add(Convert.ToInt32(infor.stt));

            }

            if (cbNameDriver.Text == "" || cbNameTG.Text == "" || cbIdVeh.Text == "")
            {
                MessageBox.Show("Bị thiếu thông tin vui lòng nhập lại");
            }
            else
            {
                ThongTinChuyenDi tt = new ThongTinChuyenDi()
                {
                    MaChuyen = "CD" + Convert.ToString(temp.Count + 1),
                    MaTX = cbIdDriver.Text,
                    TenTX = cbNameDriver.Text,
                    MaHDV= cbIdTG.Text,
                    TenHDV = cbNameDriver.Text,
                    MaCX = cbIdVeh.Text,
                    TenDD = cbNameLocation.Text,
                    NgayBatDau = dtstar.Value,
                    NgayKetThuc = dtEnd.Value
                };
                db.ThongTinChuyenDis.Add(tt);
                db.SaveChanges();
                LoadData();
                MessageBox.Show("thêm thành công ");

                // after add successfullly, delete data in textbox

                ClearData();
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
            String maChuyen = Convert.ToString(row.Cells[1].Value);
            String maTx = Convert.ToString(row.Cells[2].Value);
            String tenTX = Convert.ToString(row.Cells[3].Value);
            String maHDV = Convert.ToString(row.Cells[4].Value);
            String tenHDV = Convert.ToString(row.Cells[5].Value);
            String maChuyenXe = Convert.ToString(row.Cells[6].Value);
            String maDD = Convert.ToString(row.Cells[7].Value);
            DateTime star = Convert.ToDateTime(row.Cells[8].Value);
            DateTime end = Convert.ToDateTime(row.Cells[9].Value);

            //upload UI
            txtStt.Text = stt;
            cbIdDriver.Text = maTx;
            cbNameDriver.Text = tenTX;
            cbIdTG.Text = maHDV;
            cbNameTG.Text = tenHDV;
            cbIdVeh.Text = maChuyenXe;
            cbNameLocation.Text = maChuyenXe;
            dtstar.Value = star;
            dtEnd.Value = end;

            if (txtStt.Text == "")
            {
                btnUpdate.Enabled = true;
                btnChange.Enabled = false;
             
            }
            else
            {
                btnUpdate.Enabled = false;
                btnChange.Enabled = true;
              
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("bạn có thật sự muốn thoát ?", "thoát ứng dụng", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            String id = dataGridView1.SelectedCells[0].OwningRow.Cells["MaChuyen"].Value.ToString();
            ThongTinChuyenDi tt = db.ThongTinChuyenDis.Find(id);
            //kh.stt = Convert.ToInt32(txtStt.Text);
            tt.MaTX = cbIdDriver.Text;
            tt.TenTX = cbNameDriver.Text;
            tt.MaHDV = cbIdTG.Text;
            tt.TenHDV = cbNameDriver.Text;
            tt.MaCX = cbIdVeh.Text;
            tt.TenDD = cbNameLocation.Text;
            tt.NgayBatDau = dtstar.Value;
            tt.NgayKetThuc = dtEnd.Value;
            db.SaveChanges();
            LoadData();
        }

        private void ManageInfoTrip_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Boolean flag = false;
            string nameFind = txtFind.Text;
            //int stt = Convert.ToInt32(txtFind.Text);
            foreach (var tt in db.ThongTinChuyenDis)
            {
                if (tt.TenTX.Equals(nameFind) || tt.TenHDV.Equals(nameFind) || tt.MaTX.Equals(nameFind) || tt.MaHDV.Equals(nameFind))
                {
                    flag = true;
                }
            }
            if (flag)
            {

                var list2 = db.ThongTinChuyenDis.Where(p => p.TenTX == nameFind || p.TenHDV == nameFind || p.MaTX == nameFind || p.MaHDV == nameFind).ToList();
                dataGridView1.DataSource = list2;

                /*var list2 = (from a in db.nhanViens where a.Ma_nv == stt select a).ToList();
                dataGridView1.DataSource = list2;*/
            }
            else MessageBox.Show("Không tìm thấy dữ liệu");
        }

        private void cbNameDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtStt.Text == "")
            {
                btnUpdate.Enabled = true;
                btnChange.Enabled = false;
            }
           
        }

        private void cbNameTG_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cbIdDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
      
        }

        private void cbNameLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }

}
