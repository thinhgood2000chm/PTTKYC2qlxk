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
    public partial class Login : Form
    {
        QuanLyXeKhachEntities db = new QuanLyXeKhachEntities();
        public Login()
        {
            InitializeComponent();
        }
 
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Boolean flag = false;
            foreach (var user in db.TaiKhoans)
            { 
                // if (user.TenTK.Equals(Convert.ToString(txtUsername.Text)) && user.MatKhau.Equals(Convert.ToString(txtPassword.Text)))
                if (user.TenTK == txtUsername.Text && user.MatKhau == txtPassword.Text)
                {
                    if (txtUsername.Text.Contains("admin"))
                    {

                        ManageEmployee manageEmployee = new ManageEmployee();
                        this.Hide();
                        manageEmployee.ShowDialog();
                        this.Show();
                    }
                    else if (txtUsername.Text.Contains("employee"))
                    {
                        ManageCustomer manageCustomer = new ManageCustomer();
                        manageCustomer.dataAdd = txtUsername.Text;
                        this.Hide();
                        manageCustomer.ShowDialog();
                        this.Show();
                    }
                    else if (txtUsername.Text.Contains("seller"))
                    {
                        Payment payment = new Payment();
                        payment.dataAdd = txtUsername.Text;
                        this.Hide();
                        payment.ShowDialog();
                        this.Show();
                    }
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                txtErrorMess.Text = "Sai tên tài khoản hoặc mật khẩu";
            }
            else
            {
                txtErrorMess.Text = "";
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
    }
}
