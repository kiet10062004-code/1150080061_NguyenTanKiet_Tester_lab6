using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _1150080061_NguyenTanKiet_lab6_bai4_5_6
{
    public partial class Form1 : Form
    {
        string connStr = @"Data Source=.\SQLEXPRESS;
                           Initial Catalog=Organization_unit;
                           Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string unitId = textBox1.Text.Trim();
            string name = textBox2.Text.Trim();
            string description = richTextBox1.Text.Trim();

            if (unitId == "")
            {
                MessageBox.Show("Unit Id không được để trống");
                textBox1.Focus();
                return;
            }

            if (name == "")
            {
                MessageBox.Show("Name là bắt buộc");
                textBox2.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string check = "SELECT COUNT(*) FROM OrganizationUnit WHERE UnitId = @id";
                    SqlCommand cmdCheck = new SqlCommand(check, conn);
                    cmdCheck.Parameters.AddWithValue("@id", unitId);

                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("UnitId đã tồn tại!");
                        return;
                    }

                    string sql = @"INSERT INTO OrganizationUnit(UnitId, Name, Description)
                                   VALUES(@id, @name, @des)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", unitId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@des", description);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Lưu thành công!");

                    textBox1.Clear();
                    textBox2.Clear();
                    richTextBox1.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "Bạn có chắc muốn thoát không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
