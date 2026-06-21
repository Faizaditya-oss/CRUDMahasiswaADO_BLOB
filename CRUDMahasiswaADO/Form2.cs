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

namespace CRUDMahasiswaADO
{
    public partial class Form2 : Form
    {
        static string connectionString = "Data Source=LAPTOP-4UOCIEQ0\\FAIZADITYA;Initial Catalog=DBAkademikADO;Integrated Security=True";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlDataAdapter da;
        DataTable dtMahasiswa;
        DataTable dtProdi;
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

        try
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            
            SqlCommand cmd = new SqlCommand("select namaprodi from programstudi", conn);
            cmd.CommandType = CommandType.Text;
            dtProdi = new DataTable();

            da = new SqlDataAdapter(cmd);
            da.Fill(dtProdi);
            cmbProdi.DataSource = dtProdi;
            cmbProdi.DisplayMember = "namaprodi";
            cmbProdi.ValueMember = "namaprodi";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Gagal load data: " + ex.Message);
        }
        }

        public class class1
        {
            public string Nama { get; set; }
            public string JenisKelamin { get; set; }
            public string Alamat { get; set; }
            public string NamaProdi { get; set; }
            public DateTime TanggalDaftar { get; set; }
        }




        private void btnLoad_Click(object sender, EventArgs e)

        {
            try {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_Report", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@inProdi", SqlDbType.VarChar, 50).Value = cmbProdi.SelectedValue;
                cmd.Parameters.Add("@inTglMsuk", SqlDbType.VarChar, 4).Value = dtpTanggalMasuk.Value.Year.ToString();

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
                    MessageBox.Show("Data tidak ditemukan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data: " + ex.Message);
            }
        }
       
        



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbProdi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(cmbProdi.SelectedValue.ToString(), dtpTanggalMasuk.Value);
            form3.Show();
            this.Hide();
        }
    }
}
