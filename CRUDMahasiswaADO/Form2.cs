using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRUDMahasiswaADO
{
    public partial class Form2 : Form
    {
        private readonly string connectionString =
            "Data Source=LAPTOP-4UOCIEQ0\\FAIZADITYA;Initial Catalog=DBAkademikADO;Integrated Security=True";

        private SqlDataAdapter da;
        private DataTable dtMahasiswa;
        private DataTable dtProdi;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dtpTanggalMasuk.Format = DateTimePickerFormat.Custom;
            dtpTanggalMasuk.CustomFormat = "yyyy";
            dtpTanggalMasuk.ShowUpDown = true;
            dtpTanggalMasuk.MinDate = new DateTime(2000, 1, 1);
            dtpTanggalMasuk.MaxDate = DateTime.Now;

            cmbProdi.DropDownStyle = ComboBoxStyle.DropDownList;

            btnCetak.Enabled = false;

            LoadProdi();
        }

        private void LoadProdi()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "SELECT NamaProdi FROM ProgramStudi",
                        conn);

                    dtProdi = new DataTable();

                    da = new SqlDataAdapter(cmd);
                    da.Fill(dtProdi);

                    cmbProdi.DataSource = dtProdi;
                    cmbProdi.DisplayMember = "NamaProdi";
                    cmbProdi.ValueMember = "NamaProdi";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gagal load data prodi : " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "sp_Report",
                        conn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(
                        "@inProdi",
                        SqlDbType.VarChar,
                        50).Value = cmbProdi.SelectedValue;

                    cmd.Parameters.Add(
                        "@inTglMsuk",
                        SqlDbType.VarChar,
                        4).Value = dtpTanggalMasuk.Value.Year.ToString();

                    da = new SqlDataAdapter(cmd);

                    dtMahasiswa = new DataTable();

                    da.Fill(dtMahasiswa);

                    dataGridView1.DataSource = dtMahasiswa;

                    if (dtMahasiswa.Rows.Count > 0)
                    {
                        btnCetak.Enabled = true;
                    }
                    else
                    {
                        btnCetak.Enabled = false;

                        MessageBox.Show(
                            "Data tidak ditemukan",
                            "Informasi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gagal load data : " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            Form3 frmReport = new Form3(
                cmbProdi.SelectedValue.ToString(),
                dtpTanggalMasuk.Value);

            frmReport.Show();
        }

        private void cmbProdi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}