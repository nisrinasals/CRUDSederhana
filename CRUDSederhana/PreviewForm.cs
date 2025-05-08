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

namespace CRUDSederhana
{
    public partial class PreviewForm : Form
    {
        private string connectionString = "Server=SAS\\SQLEXPRESS;Database=OrganisasiMahasiswa;Integrated Security=True";
        public PreviewForm(DataTable data)

        {
            InitializeComponent();
            //menetapkan data source DataGridView ke DataTable yg diterima
            dgvPreview.DataSource = data;

        }
        //event ketika form dimuat
        private void PreviewForm_Load(object sender, EventArgs e)
        {
            dgvPreview.AutoResizeColumns();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda ingin mengimpor data ini ke database?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //mengimpor data dari DataGriView ke database
                ImportDataToDatabase();
            }
        }
        private bool ValidateRow(DataRow row)
        {
            string nim = row["NIM"].ToString();

            if (nim.Length != 11)
            {
                MessageBox.Show("NIM harus terdiri dari 11 karakter.", "Kesalahan Validasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void ImportDataToDatabase()
        {
            try
            {
                DataTable dt = (DataTable)dgvPreview.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    if (!ValidateRow(row))
                    {
                        continue;
                    }

                    string query = "INSERT INTO Mahasiswa (NIM, Nama, Email, Telepon, Alamat) VALUES (@NIM, @Nama, @Email, @Telepon, @Alamat)";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@NIM", row["NIM"]);
                            cmd.Parameters.AddWithValue("@Nama", row["Nama"]);
                            cmd.Parameters.AddWithValue("@Email", row["Email"]);
                            cmd.Parameters.AddWithValue("@Telepon", row["Telepon"]);
                            cmd.Parameters.AddWithValue("@Alamat", row["Alamat"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Data berhasil diimpor ke database.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat mengimpor data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.dgvPreview = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPreview
            // 
            this.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreview.Location = new System.Drawing.Point(30, 27);
            this.dgvPreview.Name = "dgvPreview";
            this.dgvPreview.RowHeadersWidth = 51;
            this.dgvPreview.RowTemplate.Height = 24;
            this.dgvPreview.Size = new System.Drawing.Size(701, 303);
            this.dgvPreview.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(625, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // PreviewForm
            // 
            this.ClientSize = new System.Drawing.Size(770, 394);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvPreview);
            this.Name = "PreviewForm";
            this.Text = "Preview Form";
            this.Load += new System.EventHandler(this.PreviewForm_Load_2);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).EndInit();
            this.ResumeLayout(false);

        }

        private void PreviewForm_Load_2(object sender, EventArgs e)
        {

        }
    }
}