using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace iVotingSystemClient.Forms
{
    public partial class frmCandidate : Form
    {

        frmMessageBox oFrmMsgBox;


        DataAccess.Position oPostion;
        DataAccess.Candidate oCandidate;

        CommonFunction.CommonFunction oImageConvert;
        CommonFunction.CommonFunction oStringUtility;

        Model.VotedCandidate oMVotedCandidate = new Model.VotedCandidate();
        List<Model.VotedCandidate> oMVotedCandidatelst = new List<Model.VotedCandidate>();
        public string sVoters_ID = string.Empty;
        public string sElectionCode = string.Empty;
        public frmCandidate(String sUserID)
        {
            InitializeComponent();

            foreach (var o in pnlInfo.Controls.OfType<TextBox>().ToList())
            {
                o.KeyPress += TextKeyPress;
            }

            sVoters_ID = sUserID;
        }


        public frmCandidate()
        {
            InitializeComponent();

            foreach (var o in pnlInfo.Controls.OfType<TextBox>().ToList())
            {
                o.KeyPress += TextKeyPress;
            }
            
        }

        void LoadPosition()
        {
            oPostion = new DataAccess.Position();
            cboPosition.Items.Clear();

            cboPosition.DataSource = oPostion.getPosition("","");

            cboPosition.DisplayMember = "POSITION";
            cboPosition.ValueMember = "ID";            
        
        }

        private void frmVote_Load(object sender, EventArgs e)
        {            
            LoadPosition();
            GetCandidatesByPosition(cboPosition.SelectedValue.ToString());
        }

        void TextKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;   
        }

       

        void GetCandidatesByPosition(string sData)
        {
            try
            {
                oCandidate = new DataAccess.Candidate();
                dgDetails.Rows.Clear();


                foreach (DataRow row in oCandidate.getCandidatesByPosition(sData).Rows)
                {
                    dgDetails.Rows.Add(row["CANDIDATE_ID"], row["LAST_NAME"] + ", " + row["FIRST_NAME"] + " " + row["MIDDLE_NAME"]);
                }

                ClearFields();
            }
            catch (Exception ex)
            {
     
            }
        
        }

        private void cboPosition_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCandidatesByPosition(cboPosition.SelectedValue.ToString());            
        }

        void ClearFields()
        {
            foreach (var o in pnlInfo.Controls.OfType<TextBox>().ToList())
            {
                o.Text = string.Empty;
            }
            pImage.Image = null;
        }

        private void dgDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dgDetails.Rows.Count > 0)
                {
                    ClearFields();
                    oImageConvert = new CommonFunction.CommonFunction();

                    foreach (DataRow row in oCandidate.getCandidates("ID", dgDetails.Rows[e.RowIndex].Cells[0].Value.ToString()).Rows)
                    {
                        sElectionCode = row["ELECTION_CODE"].ToString();
                        txtStudentID.Text = row["CANDIDATE_ID"].ToString();
                        txtFname.Text = row["FIRST_NAME"].ToString();
                        txtMname.Text = row["MIDDLE_NAME"].ToString();
                        txtLname.Text = row["LAST_NAME"].ToString();
                        txtPosition.Text = row["POSITION"].ToString();
                        txtParty.Text = row["PARTY"].ToString();
                        txtDOB.Text = row["DOB"].ToString();
                        txtAge.Text = row["AGE"].ToString();
                        txtCourse.Text = row["COURSE"].ToString();
                        txtSection.Text = row["SECTION"].ToString();

                        oStringUtility = new CommonFunction.CommonFunction();
                        if (row["PROFILE_PIC"].ToString().Trim() != String.Empty)
                        {
                            pImage.Image = oImageConvert.BaseStringToImage(oStringUtility.DecompressString(row["PROFILE_PIC"].ToString()));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
             
            }
        }      

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtStudentID.Text.Trim() != string.Empty)
            {
                AddCandidate();
            }
            else
            {
                oFrmMsgBox = new frmMessageBox("PLEASE SELECT CANDIDIATE.");
                oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();
            }
        }

        void clearField()
        {
            foreach (Control o in pnlInfo.Controls.Cast<TextBox>().ToList())
            {
                o.Text = string.Empty;
            }
            sElectionCode = string.Empty;
        }

        void AddCandidate()
        {
            try
            {
                Boolean bFound = false;

                if (dgAddedList.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgAddedList.Rows)
                    {
                        if (row.Cells[2].Value.ToString() == txtPosition.Text)
                        {
                            bFound = true;
                            break;
                        }
                    }
                    if (bFound)
                    {
                        oFrmMsgBox = new frmMessageBox("ONLY ONE CANDIDATE PER POSITION AT A TIME.");
                        oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                        oFrmMsgBox.ShowDialog();
                    }
                    else
                    {

                        oMVotedCandidate = new Model.VotedCandidate();

                        dgAddedList.Rows.Add(txtStudentID.Text, (txtLname.Text + ", " + txtFname.Text + " " + txtMname.Text).ToString(), txtPosition.Text, txtParty.Text);

                        oMVotedCandidate.ELECTION_CODE = sElectionCode;
                        oMVotedCandidate.CANDIDATE_ID = txtStudentID.Text;
                        oMVotedCandidate.FIRST_NAME = txtFname.Text;
                        oMVotedCandidate.MIDDLE_NAME = txtMname.Text;
                        oMVotedCandidate.LAST_NAME = txtLname.Text;
                        oMVotedCandidate.DOB = Convert.ToDateTime(txtDOB.Text);
                        oMVotedCandidate.AGE = txtAge.Text;
                        oMVotedCandidate.COURSE = txtCourse.Text;
                        oMVotedCandidate.SECTION = txtSection.Text;
                        oMVotedCandidate.POSITION = txtPosition.Text;
                        oMVotedCandidate.PARTY = txtParty.Text;
                        oMVotedCandidate.DATE_ADDED = DateTime.Now;

                        oMVotedCandidate.VOTERS_ID = sVoters_ID;

                        oStringUtility = new CommonFunction.CommonFunction();
                        if (pImage.Image != null)
                        {
                            oMVotedCandidate.PROFILE_PIC = oStringUtility.CompressString(oImageConvert.ImageToBaseString(pImage.Image, ImageFormat.Png));
                        }
                        oMVotedCandidatelst.Add(oMVotedCandidate);

                    }
                }
                else
                {
                    oMVotedCandidate = new Model.VotedCandidate();

                    dgAddedList.Rows.Add(txtStudentID.Text, (txtLname.Text + ", " + txtFname.Text + " " + txtMname.Text).ToString(), txtPosition.Text, txtParty.Text);

                    oMVotedCandidate.ELECTION_CODE = sElectionCode;
                    oMVotedCandidate.CANDIDATE_ID = txtStudentID.Text;
                    oMVotedCandidate.FIRST_NAME = txtFname.Text;
                    oMVotedCandidate.MIDDLE_NAME = txtMname.Text;
                    oMVotedCandidate.LAST_NAME = txtLname.Text;
                    oMVotedCandidate.DOB = Convert.ToDateTime(txtDOB.Text);
                    oMVotedCandidate.AGE = txtAge.Text;
                    oMVotedCandidate.COURSE = txtCourse.Text;
                    oMVotedCandidate.SECTION = txtSection.Text;
                    oMVotedCandidate.POSITION = txtPosition.Text;
                    oMVotedCandidate.PARTY = txtParty.Text;
                    oMVotedCandidate.DATE_ADDED = DateTime.Now;

                    oMVotedCandidate.VOTERS_ID = sVoters_ID;

                    if (pImage.Image != null)
                    {
                        oMVotedCandidate.PROFILE_PIC = oStringUtility.CompressString(oImageConvert.ImageToBaseString(pImage.Image, ImageFormat.Png));
                    }

                    oMVotedCandidatelst.Add(oMVotedCandidate);

                }
            }
            catch (Exception ex)
            {
        
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgAddedList.Rows.Count > 0)
            {
                if (dgAddedList.SelectedRows.Count > 0)
                {
                    dgAddedList.Rows.RemoveAt(dgAddedList.SelectedRows[0].Index);
                }
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {            
            Forms.frmVote oFrm = new frmVote(this,oMVotedCandidatelst);
            oFrm.ShowDialog();
            
        }

      
        private void dgAddedList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String sCandidateID = String.Empty;
                if (dgAddedList.Rows.Count > 0)
                {
                    sCandidateID = dgAddedList.Rows[e.RowIndex].Cells[0].Value.ToString();

                    foreach (var iTem in oMVotedCandidatelst.Where(fw => fw.CANDIDATE_ID == sCandidateID).ToList())
                    {
                        sElectionCode = iTem.ELECTION_CODE;
                        txtStudentID.Text = iTem.CANDIDATE_ID;
                        txtFname.Text = iTem.FIRST_NAME;
                        txtMname.Text = iTem.MIDDLE_NAME;
                        txtLname.Text = iTem.LAST_NAME;
                        txtDOB.Text = iTem.DOB.ToString("yyyy-MM-dd");
                        txtAge.Text = iTem.AGE;
                        txtPosition.Text = iTem.POSITION;
                        txtParty.Text = iTem.PARTY;
                        txtCourse.Text = iTem.COURSE;
                        txtSection.Text = iTem.SECTION;

                        oStringUtility = new CommonFunction.CommonFunction();
                        if (iTem.PROFILE_PIC.Trim() != String.Empty)
                        {
                            pImage.Image = oImageConvert.BaseStringToImage(oStringUtility.DecompressString(iTem.PROFILE_PIC));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
        
            }
        }

        public void CloseApplication()
        {
            Close();
        }
    }
}
