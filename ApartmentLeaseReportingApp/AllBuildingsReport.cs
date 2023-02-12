using Microsoft.Reporting.WinForms;
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

namespace ApartmentLeaseReportingApp
{
    public partial class AllBuildingsReport : Form
    {
        public AllBuildingsReport()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            reportViewer1.Reset();
            DataTable dt = GetData();
            ReportDataSource ds = new ReportDataSource("DataSet2", dt);

            reportViewer1.LocalReport.DataSources.Add(ds);
            reportViewer1.LocalReport.ReportPath = @"D:\Repos\AD\Coursework1\AD_Coursework1\ApartmentLeaseReportingApp\BuildingsReport.rdlc";
            //reportViewer1.LocalReport.ReportEmbeddedResource = System.IO.Path.GetFullPath(@"");
            reportViewer1.RefreshReport();
        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            string connectionString = @"Data Source=INIVOS-LAP19\SQLEXPRESS;Initial Catalog=apartmentsmgt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("[GetAllBuildings]", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
