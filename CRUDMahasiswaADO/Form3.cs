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
    public partial class Form3 : Form
    {
        static string connectionString = "Data Source=LAPTOP-4UOCIEQ0\\FAIZADITYA;Initial Catalog=DBAkademikADO;Integrated Security=True";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlDataAdapter da;
        DataTable dtMahasiswa;

        CrystalReportsGallery listmahasiswa = new CrystalReportsGallery();

        string prodi {  get; set; }
        DateTime tglmasuk { get; set; }
        public Form3(string Prodi,DateTime TglMsuk)
        {
            InitializeComponent();

            prodi = Prodi;
            tglmasuk = TglMsuk;

            try
            {
                DataTable dtMahasiswa = dbLogic.getDataRekap(prodi, tglmasuk);

                listMahasiswa.SetDataSource(dtMahasiswa);
                crystalReportViewer1.ReportSource = listMahasiswa;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                //simpanLog(ex.Message);
                MessageBox.Show("Gagal load data: " + ex.Message);
            }
        }

        private void crystalReportViewer2_Load(object sender, EventArgs e)
        {

        }
    }
}
