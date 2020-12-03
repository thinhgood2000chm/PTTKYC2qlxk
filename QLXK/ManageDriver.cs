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
    public partial class ManageDriver : Form
    {
        public ManageDriver()
        {
            InitializeComponent();
        }
        QuanLyXeKhachEntities DB = new QuanLyXeKhachEntities();
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            var list2 = db.NhanViens.Where(p => p.ChucVu == "TX").ToList();
            cbNameDriver.DataSource = list2;
            cbNameDriver.DisplayMember = "TenNV";

            serviceManage dataXK = new serviceManage();
            var list3 = dataXK.getAllvehicle();
            cbIdVeh.DataSource = list3;
            cbIdVeh.DisplayMember = "MaXe";
        }
    }
}
