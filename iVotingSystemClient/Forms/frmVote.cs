using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ePublicVariable;

namespace iVotingSystemClient.Forms
{
    public partial class frmVote : Form
    {
        List<Model.VotedCandidate> oMVotedCandidatelstRes = new List<Model.VotedCandidate>();
        Model.VotedCandidate oMVotedCandidate =  new Model.VotedCandidate();        

        DataAccess.VotersLogin oVotersLogin = new DataAccess.VotersLogin();

        CommonFunction.CommonFunction oImageConvert;
        CommonFunction.CommonFunction oStringUtility;

        frmMessageBox oFrmMsgBox;

        public frmVote()
        {
            InitializeComponent();

            eVariable.DisablePanelTextKeyPress(pnlMain);
        }

       
        
        public frmVote(Forms.frmCandidate oFrm,List<Model.VotedCandidate> oMVotedCandidatelst)
        {
            InitializeComponent();

            oMVotedCandidatelstRes = oMVotedCandidatelst;
            eVariable.DisablePanelTextKeyPress(pnlMain);
            
        }

        void LoadRecords()
        {
            dgDetails.Rows.Clear();

            foreach (var iTem in oMVotedCandidatelstRes)
            {
                dgDetails.Rows.Add(iTem.CANDIDATE_ID, iTem.FIRST_NAME, iTem.MIDDLE_NAME, iTem.LAST_NAME, iTem.DOB, iTem.AGE, iTem.COURSE, iTem.SECTION, iTem.CONTACT_NO, iTem.ADDRESS, iTem.POSITION_ID, iTem.POSITION, iTem.PARTY_ID, iTem.PARTY,iTem.PROFILE_PIC,iTem.ELECTION_CODE);
            }        
        }

        private void frmVote_Load(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void dgDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                

                if (dgDetails.Rows.Count > 0)
                {
                    ClearFields();

                    txtStudentID.Text = dgDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtFname.Text = dgDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtMname.Text = dgDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtLname.Text = dgDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtDOB.Text = dgDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtAge.Text = dgDetails.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtCourse.Text = dgDetails.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtSection.Text = dgDetails.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtPosition.Text = dgDetails.Rows[e.RowIndex].Cells[11].Value.ToString();
                    txtParty.Text = dgDetails.Rows[e.RowIndex].Cells[13].Value.ToString();

                    oStringUtility = new CommonFunction.CommonFunction();
                    if (dgDetails.Rows[e.RowIndex].Cells[14].Value.ToString() != string.Empty)
                    { 
                        pImage.Image = oImageConvert.BaseStringToImage(oStringUtility.DecompressString(dgDetails.Rows[e.RowIndex].Cells[14].Value.ToString()));
                    }

                }
            }
            catch (Exception ex)
            {
       
            }
        }

        private void dgDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dgDetails.Rows.Count > 0)
                {
                    txtStudentID.Text = dgDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtFname.Text = dgDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtMname.Text = dgDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtLname.Text = dgDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtDOB.Text = dgDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtAge.Text = dgDetails.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtCourse.Text = dgDetails.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtSection.Text = dgDetails.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtPosition.Text = dgDetails.Rows[e.RowIndex].Cells[11].Value.ToString();
                    txtParty.Text = dgDetails.Rows[e.RowIndex].Cells[13].Value.ToString();

                    oStringUtility = new CommonFunction.CommonFunction();
                    oImageConvert = new CommonFunction.CommonFunction();
                    if (dgDetails.Rows[e.RowIndex].Cells[14].Value.ToString() != string.Empty)
                    {
                        pImage.Image = oImageConvert.BaseStringToImage(oStringUtility.DecompressString(dgDetails.Rows[e.RowIndex].Cells[14].Value.ToString()));
                    }

                }
            }
            catch (Exception ex)
            {
               
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (oMVotedCandidatelstRes.Count > 0)
            {
                oFrmMsgBox = new frmMessageBox("ARE YOU SURE YOU WANT TO PROCEED TO THE TRANSACTION");
                oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.QUERY;
                oFrmMsgBox.ShowDialog();

                if (oFrmMsgBox.sAnswer == "YES")
                {
                    foreach (var iTem in oMVotedCandidatelstRes)
                    {
                        oVotersLogin = new DataAccess.VotersLogin();
                        oMVotedCandidate = new Model.VotedCandidate();

                        oMVotedCandidate.CANDIDATE_ID = iTem.CANDIDATE_ID;
                        oMVotedCandidate.FIRST_NAME = iTem.FIRST_NAME;
                        oMVotedCandidate.MIDDLE_NAME = iTem.MIDDLE_NAME;
                        oMVotedCandidate.LAST_NAME = iTem.LAST_NAME;
                        oMVotedCandidate.DOB = iTem.DOB;
                        oMVotedCandidate.COURSE = iTem.COURSE;
                        oMVotedCandidate.SECTION = iTem.SECTION;
                        oMVotedCandidate.POSITION = iTem.POSITION;
                        oMVotedCandidate.PARTY = iTem.PARTY;
                        oMVotedCandidate.DATE_ADDED = iTem.DATE_ADDED;
                        oMVotedCandidate.VOTERS_ID = iTem.VOTERS_ID.ToUpper();
                        oMVotedCandidate.ELECTION_CODE = iTem.ELECTION_CODE;

                        oVotersLogin.VoteCandidate(oMVotedCandidate);
                    }

                    oFrmMsgBox = new frmMessageBox("YOU HAVE SUCCESSFULLY VOTED. THANK YOU.");
                    oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();

                    oVotersLogin = new DataAccess.VotersLogin();
                    oVotersLogin.UpdateVotersKey(oMVotedCandidate);

                    frmCandidate oFrmCandidate = new frmCandidate();
                    oFrmCandidate.CloseApplication();
                    Close();

                    frmLogin oFrm = new frmLogin();
                    oFrm.ShowDialog();
                }
            }
            
        }

        void ClearFields()
        {
            foreach (var o in pnlMain.Controls.OfType<TextBox>().ToList())
            {
                o.Text = string.Empty;
            }
            pImage.Image = null;
        }

       

    }
}
