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
    public partial class ListPhieuVe : Form
    {
        public ListPhieuVe()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ListPhieuVe_Load(object sender, EventArgs e)
        {
            serviceManage data = new serviceManage();
            var list = data.getAllTicket();
            dataGridView1.DataSource = list;
        }
    }
}
