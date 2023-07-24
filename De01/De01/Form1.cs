using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De01
{
    public partial class frmSinhvien : Form
    {
        string str = "Data Source=DESKTOP-41RE0AP\\SQLEXPRESS; Initial Catalog=QuanlySV;Integrated Security=True";
        BindingSource bs = null;

        Model1 context = new Model1();
        List<Sinhvienn> listsinhvien;

        public frmSinhvien()
        {
            InitializeComponent();
        }

        private void frmSinhvien_Load(object sender, EventArgs e)
        {
            /*SqlConnection cnn = new SqlConnection(str);
            if (cnn == null)
            {
                cnn = new SqlConnection(str);
            }

            SqlDataAdapter adt = new SqlDataAdapter("Select * from Sinhvienn", cnn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adt);
            DataSet ds = new DataSet();
            adt.Fill(ds, "Sinhvienn");
            bs = new BindingSource(ds, "Sinhvienn");
            txtID.DataBindings.Add("text", bs, "MaSV");
            txtName.DataBindings.Add("text", bs, "HotenSV");
            dateTimePicker1.DataBindings.Add("text", bs, "NgaySinh");
            comboBox1.DataBindings.Add("text", bs, "MaLop");

            listsinhvien = context.Sinhvienns.ToList();
            List<Lop> listclass = context.Lops.ToList();
            dssv(listsinhvien);
            listlop(listclass);*/
            List<Sinhvienn> listsinhvien = context.Sinhvienns.ToList();
            List<Lop> listLop = context.Lops.ToList();
            listlop(listLop);
            dssv(listsinhvien);
        }
        public void listlop(List<Lop> listLop)
        {
            comboBox1.DataSource = listLop;
            comboBox1.DisplayMember = "TenLop";
            comboBox1.ValueMember = "MaLop";
        }
        public void dssv(List<Sinhvienn> listsinhvien)
        {
            listView1.Items.Clear();
            foreach(Sinhvienn sv in listsinhvien)
            {
                ListViewItem lv = new ListViewItem();
                lv.SubItems.Add(sv.MaSV);
                lv.SubItems.Add(sv.HotenSV);
                lv.SubItems.Add(sv.NgaySinh.ToString());
                lv.SubItems.Add(sv.MaLop);
                listView1.Items.Add(lv);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                Sinhvienn sv = new Sinhvienn()
                {
                    MaSV = txtID.Text,
                    HotenSV = txtName.Text,
                    NgaySinh = dateTimePicker1.Value,
                    MaLop = comboBox1.Text,
                };
                context.Sinhvienns.Add(sv);
                context.SaveChanges();
                dssv(context.Sinhvienns.ToList());
            }
            catch
            {
                MessageBox.Show("Dữ liệu vừa nhập bị trùng");
            }

            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin đầy đủ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtID.Text = " ";
                txtName.Text = " ";
            }

            
        }
        private void AddStudent(string id, string name, string lop, string ngay)
        {
            SqlConnection sqlConnection = new SqlConnection(str);
            string query = "insert into Sinhvien(MaSV, HotenSV, NgaySinh, MaLop) value (id, name, ngay, lop)";
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add("@MaSV", id);
            sqlCommand.Parameters.Add("@HotenSV", name);
            sqlCommand.Parameters.Add("@NgaySinh",ngay);
            sqlCommand.Parameters.Add("@MaLop", lop);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Thêm sinh viên thành công.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                Sinhvienn delete = context.Sinhvienns.FirstOrDefault(d => d.MaSV == txtID.Text);
                if(delete != null)
                {
                    context.Sinhvienns.Remove(delete);
                    context.SaveChanges();
                    dssv(context.Sinhvienns.ToList());
                    MessageBox.Show("Xóa sinh viên thành công");
                }    
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                Sinhvienn ud = context.Sinhvienns.FirstOrDefault(d => d.MaSV == txtID.Text);
                if (ud != null)
                {
                    ud.HotenSV = txtName.Text;
                    ud.NgaySinh = dateTimePicker1.Value;
                    ud.MaLop = (comboBox1.SelectedItem as Lop).MaLop;
                    context.SaveChanges();
                    dssv(context.Sinhvienns.ToList());
                    MessageBox.Show("Sửa sinh viên thành công");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin sinh viên");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
