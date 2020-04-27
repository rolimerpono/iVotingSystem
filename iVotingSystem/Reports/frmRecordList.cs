using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using ePublicVariable;
namespace iVotingSystem.Reports
{
    public partial class frmRecordList : Form
    {     

        string sReportPath = Application.StartupPath.Replace("bin", "").Replace("Debug", "").Replace("\\\\", "") + @"\Reports\";
        string sReportName = string.Empty;
        DataAccess.Reports oReports;                        
        public frmRecordList()
        {
            InitializeComponent();
            eVariable.DisableKeyPress(txtElectionCode);
        }
       
        private void frmReports_Load(object sender, EventArgs e)
        {
            dtDateFrom.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            dtDateTo.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

         
        }

    
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtElectionCode.Text.Trim() == string.Empty && cboSearch.Text != "STUDENT LIST")
            {
                Forms.frmMessageBox oFrm = new Forms.frmMessageBox("ELECTION CODE NUMBER IS REQUIRED IN THIS REPORT");
                oFrm.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                oFrm.ShowDialog();
                txtElectionCode.Focus();
                return;
            }

            switch (cboSearch.Text)
            { 
                case "STUDENT LIST":
                    sReportName = "rptRecordList.rdlc";
                    LoadStudents();
                    break;
                case "CANDIDATE LIST":
                    sReportName = "rptCandidateList.rdlc";                    
                    LoadCandidates();
                    break;    
                case "VOTE RESULT":
                    sReportName = "rptResult.rdlc";
                    VoteResult();
                    break;
                case "ELECTION WINNER":
                    sReportName = "rptResult.rdlc";
                    ElectionWinner();
                    break;  
            }

           
        }
 
        void LoadStudents()
        {
            oReports = new DataAccess.Reports();            

            ReportViewer.LocalReport.ReportPath = sReportPath + sReportName;
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", oReports.GetStudents(dtDateFrom.Value,dtDateTo.Value)));
            ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            ReportViewer.ZoomMode = ZoomMode.Percent;
            ReportViewer.ZoomPercent = 100;
            ReportViewer.RefreshReport();

        }

        void LoadCandidates()
        {
            oReports = new DataAccess.Reports();

            ReportViewer.LocalReport.ReportPath = sReportPath + sReportName;
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", oReports.getCandidates(dtDateFrom.Value, dtDateTo.Value,txtElectionCode.Text)));
            ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            ReportViewer.ZoomMode = ZoomMode.Percent;
            ReportViewer.ZoomPercent = 100;
            ReportViewer.RefreshReport();

        }

        void VoteResult()
        {
            oReports = new DataAccess.Reports();

            ReportViewer.LocalReport.ReportPath = sReportPath + sReportName;
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", oReports.getVoteResult(dtDateFrom.Value,dtDateTo.Value,txtElectionCode.Text)));
            ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            ReportViewer.ZoomMode = ZoomMode.Percent;
            ReportViewer.ZoomPercent = 100;
            ReportViewer.RefreshReport();

        }

        void ElectionWinner()
        {
            oReports = new DataAccess.Reports();

            ReportViewer.LocalReport.ReportPath = sReportPath + sReportName;
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", oReports.getResultWinner(dtDateFrom.Value, dtDateTo.Value, txtElectionCode.Text)));
            ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            ReportViewer.ZoomMode = ZoomMode.Percent;
            ReportViewer.ZoomPercent = 100;
            ReportViewer.RefreshReport();

        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

    }
}
