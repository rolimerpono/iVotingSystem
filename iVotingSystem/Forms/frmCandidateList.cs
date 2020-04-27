using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ePublicVariable;

namespace iVotingSystem.Forms
{
    public partial class frmCandidateList : Form
    {

        DataAccess.Candidate oCandidate;
        Model.Candidate oMCandidate;        
        

        public string sTitle = "Voting System Management";

        frmCandidateEntry oFrm;
        frmMessageBox oFrmMsgBox;
        

        
        public frmCandidateList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadCandidates()
        {
            try
            {
                eVariable.DisableGridColumnSort(dgCandidates);
               
                oCandidate = new DataAccess.Candidate();
                dgCandidates.Rows.Clear();

                foreach (DataRow row in oCandidate.getCandidates(cboSearchBy.Text, tbxSearch.Text).Rows)
                {
                    dgCandidates.Rows.Add(row["CANDIDATE_ID"], row["FIRST_NAME"], row["MIDDLE_NAME"], row["LAST_NAME"], row["DOB"], row["AGE"], row["COURSE"], row["SECTION"], row["CONTACT_NO"], row["ADDRESS"], row["POSITION_ID"], row["POSITION"], row["PARTY_ID"], row["PARTY"],row["PROFILE_PIC"],row["ELECTION_CODE"]);
                }

                lblTotalRecords.Text = dgCandidates.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
          
            }
            
        }

        private void frmCandidates_Load(object sender, EventArgs e)
        {                 
            LoadCandidates();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Forms.frmCandidateEntry oFrm = new frmCandidateEntry(this);
            oFrm.TransactionType = eVariable.TransactionType.ADD;
            oFrm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgCandidates.Rows.Count > 0)
            {
               oFrm = new frmCandidateEntry(this, oMCandidate);
               oFrm.TransactionType = eVariable.TransactionType.EDIT;
               oFrm.ShowDialog();    
            }
        }

        void DatagridSelect(object sender, DataGridViewCellEventArgs e)
        {
            try
            {                
                oMCandidate = new Model.Candidate();

                if (dgCandidates.Rows.Count > 0 && e.RowIndex >=0 )
                {
                    oMCandidate.UNIQUE_ID = dgCandidates.Rows[e.RowIndex].Cells[0].Value.ToString();
                    oMCandidate.FIRST_NAME = dgCandidates.Rows[e.RowIndex].Cells[1].Value.ToString();
                    oMCandidate.MIDDLE_NAME = dgCandidates.Rows[e.RowIndex].Cells[2].Value.ToString();
                    oMCandidate.LAST_NAME = dgCandidates.Rows[e.RowIndex].Cells[3].Value.ToString();
                    oMCandidate.DOB = dgCandidates.Rows[e.RowIndex].Cells[4].Value.ToString();
                    oMCandidate.AGE = dgCandidates.Rows[e.RowIndex].Cells[5].Value.ToString();
                    oMCandidate.COURSE = dgCandidates.Rows[e.RowIndex].Cells[6].Value.ToString();

                    oMCandidate.SECTION = dgCandidates.Rows[e.RowIndex].Cells[7].Value.ToString();
                    oMCandidate.CONTACT_NO = dgCandidates.Rows[e.RowIndex].Cells[8].Value.ToString();
                    oMCandidate.ADDRESS = dgCandidates.Rows[e.RowIndex].Cells[9].Value.ToString();
                    oMCandidate._Position.ID = dgCandidates.Rows[e.RowIndex].Cells[10].Value.ToString();
                    oMCandidate._Position.POSITION = dgCandidates.Rows[e.RowIndex].Cells[11].Value.ToString();
                    oMCandidate._Party.ID = dgCandidates.Rows[e.RowIndex].Cells[12].Value.ToString();
                    oMCandidate._Party.PARTY = dgCandidates.Rows[e.RowIndex].Cells[13].Value.ToString();
                    oMCandidate.PROFILE_PIC = dgCandidates.Rows[e.RowIndex].Cells[14].Value.ToString();
                    oMCandidate.ELECTION_CODE = dgCandidates.Rows[e.RowIndex].Cells[15].Value.ToString();
                    

                }
            }
            catch (Exception ex)
            {
                
            }
        }      

        private void dgCandidates_CellClick(object sender, DataGridViewCellEventArgs e)
        {           
            DatagridSelect(sender, e);            
        }

        private void dgCandidates_CellEnter(object sender, DataGridViewCellEventArgs e)
        {          
            DatagridSelect(sender, e);            
        }        

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboSearchBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCandidates();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadCandidates();
        }

        private void dgCandidates_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgCandidates.Rows.Count > 0)
            {
                oFrm = new frmCandidateEntry(this, oMCandidate);
                oFrm.TransactionType = eVariable.TransactionType.EDIT;
                oFrm.ShowDialog();
            }
        }

     
       
    }
}
