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
    public partial class frmDangNhap : Form
    {
        int kq;
        OleDbConnection conn;
        public frmDangNhap()
        {
            conn = Connect.getConnect();
            InitializeComponent();
            
        }

        
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            Boolean q = false;
            kq = Connect.Check_Config();

            if (kq==1 || kq==2)
            {
                MessageBox.Show("cHUOI CAU HINH KHONG TON tAI HOAC KHONG DUNG");

                this.Hide();
                frmCauHinh f = new frmCauHinh();
                f.Show();
            }
            else
            {
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter("select * from T_NHAN_VIEN", conn);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)// dem tung dong table
                {
                    //MessageBox.Show("" + dt.Rows[i].Field<string>(0) + "_" + dt.Rows[i].Field<string>(6));
                    if (dt.Rows[i].Field<string>(0) == txtmanv.Text && dt.Rows[i].Field<string>(6) == txtmatkhau.Text)// (0) la cot tai khoan
                    {
                        q = true;
                       
                    }    
                }
                if (q == true)
                {
                    this.Hide();
                    FormMain f = new FormMain();
                    f.Show();
                }
                else MessageBox.Show("that bai");
                conn.Close();
            }

           
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
           
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
        }
    }
}
