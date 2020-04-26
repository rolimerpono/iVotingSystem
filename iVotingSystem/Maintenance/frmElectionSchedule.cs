using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iVotingSystem.Maintenance
{
    public partial class frmElectionSchedule : Form
    {
        DataAccess.SystemFunction oVotingSchedule;
        Model.VotingSchedule oMVotingSchedule;

        string sTitle = "Voting Management System";

        Forms.frmMessageBox oFrmMsgBox;
        string sScheduleStatus = string.Empty;

        public frmElectionSchedule()
        {
            InitializeComponent();
        }        

        void LoadRecordDetails()
        {
            try
            {
                oVotingSchedule = new DataAccess.SystemFunction();
                oMVotingSchedule = new Model.VotingSchedule();

                dgDetails.Rows.Clear();

                if (!oVotingSchedule.IsScheduleExists())
                {
                    oMVotingSchedule.DATE_END = DateTime.Now.ToString("yyyy-MM-dd");
                    oMVotingSchedule.TIME_END = DateTime.Now.ToString("h:mm:ss tt");
                    oMVotingSchedule.DATE_START = DateTime.Now.ToString("yyyy-MM-dd");
                    oMVotingSchedule.TIME_END = DateTime.Now.ToString("h:mm:ss tt");
                    oMVotingSchedule.STATUS = "CLOSED";
                    oVotingSchedule.InsertSchedule(oMVotingSchedule);
                }

                foreach (DataRow row in oVotingSchedule.GetVotingSchedule().Rows)
                {
                    dgDetails.Rows.Add(row["DATE_START"].ToString().Trim(), row["TIME_START"].ToString().Trim(), row["DATE_END"].ToString().Trim(), row["TIME_END"].ToString().Trim(), row["STATUS"].ToString().Trim());
                    sScheduleStatus = row["STATUS"].ToString().Trim();
                }


                if (CheckDateTime())
                {
                    oMVotingSchedule = new Model.VotingSchedule();
                    oMVotingSchedule.STATUS = "CLOSED";
                    oVotingSchedule.UpdateSchedule(oMVotingSchedule);
                }

                if (sScheduleStatus != "CLOSED")
                {
                    lblStatus.ForeColor = Color.Lime;
                    lblStatus.Text = "OPEN";
                }
                else
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "CLOSED";
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                oMVotingSchedule = new Model.VotingSchedule();

                if (DateTimeValidation())
                {
                    oFrmMsgBox = new Forms.frmMessageBox("THE DATE RANGE YOU HAVE SELECTED ARE INVALID OR END DATE ARE LESS THAN THE CURRENT DATE.");
                    oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                    LoadRecordDetails();
                    return;
                }

                oVotingSchedule = new DataAccess.SystemFunction();                          
                if (oVotingSchedule.IsScheduleExists())
                {

                    oMVotingSchedule.DATE_START = dtDateFrom.Value.ToString("yyyy-MM-dd");
                    oMVotingSchedule.TIME_START = dtTimeFrom.Value.ToString("h:mm:ss tt");
                    oMVotingSchedule.DATE_END = dtDateTo.Value.ToString("yyyy-MM-dd");
                    oMVotingSchedule.TIME_END = dtTimeTo.Value.ToString("h:mm:ss tt");
                    oMVotingSchedule.STATUS = cboStatus.Text;

                    oVotingSchedule.UpdateSchedule(oMVotingSchedule);
                }             

                LoadRecordDetails();
                MessageBox.Show("Record has been succesfully saved", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
  
            }
        }

        private bool DateTimeValidation()
        {
            if (Convert.ToDateTime(dtDateTo.Value.ToString("yyyy-MM-dd")) < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))
                       || Convert.ToDateTime(dtDateFrom.Value.ToString("yyyy-MM-dd")) > Convert.ToDateTime(dtDateTo.Value.ToString("yyyy-MM-dd"))
                &&
                Convert.ToDateTime(dtTimeTo.Value.ToString("h:mm:ss tt")) < Convert.ToDateTime(DateTime.Now.ToString("h:mm:ss tt"))
                       || Convert.ToDateTime(dtTimeFrom.Value.ToString("h:mm:ss tt")) > Convert.ToDateTime(dtTimeTo.Value.ToString("h:mm:ss tt")
                ))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckDateTime()
        { 
            if (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) > Convert.ToDateTime(oMVotingSchedule.DATE_END) && Convert.ToDateTime(DateTime.Now.ToString("h:mm:ss tt")) > Convert.ToDateTime(oMVotingSchedule.TIME_END))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    

        private void cboStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DatagridSelect(sender, e);
        }

        void DatagridSelect(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                oMVotingSchedule = new Model.VotingSchedule();

                if (dgDetails.Rows.Count > 0)
                {
                    oMVotingSchedule.DATE_START = dgDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
                    oMVotingSchedule.TIME_START = dgDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                    oMVotingSchedule.DATE_END = dgDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                    oMVotingSchedule.TIME_END = dgDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
                    oMVotingSchedule.STATUS = dgDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
          
                }
            }
            catch (Exception ex)
            {
        
            }
        }

        private void dgDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DatagridSelect(sender, e);
        }

        private void frmElectionSchedule_Load(object sender, EventArgs e)
        {
            dtDateFrom.Value = DateTime.Now;
            dtDateTo.Value = DateTime.Now;
            dtTimeFrom.Value = DateTime.Now;
            dtTimeTo.Value = DateTime.Now;
            LoadRecordDetails();
        }


       
    }
}
