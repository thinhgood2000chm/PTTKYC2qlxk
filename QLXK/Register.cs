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
    public partial class Register : Form
    {
        QuanLyXeKhachEntities db = new QuanLyXeKhachEntities();
        public Register()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ArrayList temp = new ArrayList();
            ArrayList ArrayUserName = new ArrayList();
            foreach (var user in db.TaiKhoans)
            {
                temp.Add(Convert.ToInt32(user.stt));
                //lưu giá trị vào mảng vì nếu dùng lại user in tailjoan sẽ bị lỗi đa luồng
                ArrayUserName.Add(user.TenTK);
                
            }
            bool flag = true;
            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                for (int i = 0; i < ArrayUserName.Count; i++)
                {
                   // MessageBox.Show(Convert.ToString(ArrayUserName[i])+ txtUsername.Text);
                    if (txtUsername.Text.Equals(Convert.ToString(ArrayUserName[i])))
                    {
                        MessageBox.Show("Tên tài khoản đã tồn tại, vui lòng nhập tên khác");
                        // neu ko break thi nó xét tieeos găp ở dươi ko trùng nên tạo tk nhưng tạo xong lại bị trùng tk đã có
                        flag = false;
                        break;
                    }
                
                }
                if (flag)
                {

                    TaiKhoan createAcc = new TaiKhoan()
                    {
                        MaNV = "NV" + Convert.ToString(temp.Count + 1),
                        TenNV = txtNameEmp.Text,
                        ChucVu = txtPosition.Text,
                        TenTK = txtUsername.Text,
                        MatKhau = txtPassword.Text

                    };
                    db.TaiKhoans.Add(createAcc);
                    db.SaveChanges();
                    MessageBox.Show("Đăng kí tài khoan thành công");
                    txtNameEmp.Text = "";
                    txtPosition.Text = "";
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtConfirmPassword.Text = "";

                }

            }
            else MessageBox.Show("mật khẩu không khớp");
            
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
