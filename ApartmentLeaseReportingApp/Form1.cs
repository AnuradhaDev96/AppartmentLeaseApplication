using Microsoft.Reporting.WinForms;
using System.Data;
using System.Data.SqlClient;

namespace ApartmentLeaseReportingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnShowReport_Click(object sender, EventArgs e)
        {
            var apartmentReportForm = new AllApartmentsReport();
            apartmentReportForm.Show();
            //reportViewer1.Reset();
            //DataTable dt = GetData();
            //ReportDataSource ds = new ReportDataSource("DataSet1", dt);

            //reportViewer1.LocalReport.DataSources.Add(ds);
            //reportViewer1.LocalReport.ReportPath = @"D:\Repos\AD\Coursework1\AD_Coursework1\ApartmentLeaseReportingApp\Report1.rdlc";
            ////reportViewer1.LocalReport.ReportEmbeddedResource = System.IO.Path.GetFullPath(@"");
            //reportViewer1.RefreshReport();
        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            string connectionString = @"Data Source=INIVOS-LAP19\SQLEXPRESS;Initial Catalog=apartmentsmgt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("[GetAllApartments]", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        private void BtnShowBuildingsReport_Click(object sender, EventArgs e)
        {
            var reportForm = new AllBuildingsReport();
            reportForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var reportForm = new AllApartmentClassesView();
            reportForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var reportForm = new AllParkingSpacesReportView();
            reportForm.Show();
        }

        private void btnManageBuildings_Click(object sender, EventArgs e)
        {
            var form = new CreateBuildingWindow();
            form.Show();
        }

        private void btnApproveExtensions_Click(object sender, EventArgs e)
        {
            var form = new ApproveExtensionRequests();
            form.Show();
        }
    }
}