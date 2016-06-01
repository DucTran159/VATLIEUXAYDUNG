using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace QL_VatLieuXayDung
{
    public partial class frmNhanVien : Form
    {
        OleDbConnection conn;
        DataTable dt;
        OleDbDataAdapter adapter;
        OleDbCommand cmd;
        public frmNhanVien()
        {
            InitializeComponent();
            conn = Connect.getConnect();
        }
        
        public void loadTable()
        {
            conn.Open();
            adapter = new OleDbDataAdapter("select * from T_NHAN_VIEN", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dgvNhanVien.DataSource = dt;
            conn.Close();
        }

        
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            loadTable();
            for (int i = dgvNhanVien.Rows.Count - 1; i < dgvNhanVien.Rows.Count; ++i)
            {
                //dataGridView1.Rows[i].Selected = true;
                if (dt.Rows.Count < 10)
                    txtMaNV.Text = "NV000" + (dt.Rows.Count + 1);
                else if (dt.Rows.Count < 100)
                    txtMaNV.Text = "NV00" + (dt.Rows.Count + 1);
                else txtMaNV.Text = "NV0" + (dt.Rows.Count + 1);

            }
        }

        int ktma()
        {
            string s = "select count(*) from T_NHAN_VIEN where MANV=(N'" + txtMaNV.Text + "')";
            cmd = new OleDbCommand(s, conn);
            object o = cmd.ExecuteScalar();
            return (int)(o);
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "" || txtTenNV.Text == "" || txtMail.Text == "" | txtMatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }
            else if (ktma() >= 1)
            {
                MessageBox.Show("Mã nhan vien bị trùng");
                return;
            }
            else
            {
                cmd = new OleDbCommand("insert into T_NHAN_VIEN(MANV,TENNV,PHAI,EMAILNV,DIENTHOAINV,MANHOM,MATKHAU) values ('" + txtMaNV.Text + "','" + txtTenNV.Text + "')", conn);
                cmd.ExecuteNonQuery();

            }
        }

       
    }
}
