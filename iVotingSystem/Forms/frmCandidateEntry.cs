using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using ePublicVariable;

namespace iVotingSystem.Forms
{
    public partial class frmCandidateEntry : Form
    {

        DataAccess.Candidate oCandidate = new DataAccess.Candidate();
        DataAccess.Party oParty = new DataAccess.Party();
        Model.Party oMParty = new Model.Party();
        DataAccess.SystemFunction oElection = new DataAccess.SystemFunction();
        
        public Model.Candidate oMCandidate = new Model.Candidate();

        CommonFunction.CommonFunction oImageConvert;
        CommonFunction.CommonFunction oStringUtility;        
        OpenFileDialog oDiagLog;            
 
        frmCandidateList oFrmCandidateList;

        frmMessageBox oFrmMsgBox;
     

        public eVariable.TransactionType TransactionType;
        public frmCandidateEntry()
        {
            InitializeComponent();
            eVariable.DisablePanelTextKeyPress(pnlMain);  
        }

        public frmCandidateEntry(frmCandidateList oFrmCandidate)
        {
            InitializeComponent();
            oFrmCandidateList = oFrmCandidate;
            eVariable.DisablePanelTextKeyPress(pnlMain);  
        }
     

        public frmCandidateEntry(frmCandidateList oFrmCandidate, Model.Candidate oData)
        {
            InitializeComponent();            
            oMCandidate = oData;
            oFrmCandidateList = oFrmCandidate;
            eVariable.DisablePanelTextKeyPress(pnlMain);  
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void DisPlayCandidates()
        {

            oImageConvert = new CommonFunction.CommonFunction();

            if (TransactionType == eVariable.TransactionType.EDIT)
            {

                eVariable.sID = oMCandidate.UNIQUE_ID;
                txtCandidateID.Text = oMCandidate.UNIQUE_ID;
                txtFullname.Text = oMCandidate.FIRST_NAME + " " + oMCandidate.MIDDLE_NAME + " " + oMCandidate.LAST_NAME;
                txtAge.Text = oMCandidate.AGE.ToString();
                txtDOB.Text = oMCandidate.DOB;
                txtContactNo.Text = oMCandidate.CONTACT_NO;
                txtAddress.Text = oMCandidate.ADDRESS;
                txtSection.Text = oMCandidate.SECTION;
                txtCourse.Text = oMCandidate.COURSE;
                txtPosition.Text = oMCandidate._Position.POSITION ;
                txtParty.Text = oMCandidate._Party.PARTY;
                

                oStringUtility = new CommonFunction.CommonFunction();
                if (oMCandidate.PROFILE_PIC != string.Empty)
                {
                    pImage.Image = oImageConvert.BaseStringToImage(oStringUtility.DecompressString(oMCandidate.PROFILE_PIC));
                }

                EDFields(true);
                
            }
            else
            {
                EDFields(false);
            }
        }

        void TextKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void frmCandidateEntry_Load(object sender, EventArgs e)
        {
            DisPlayCandidates();
            GetElectionCode();
            txtCandidateID.Focus();
        }

        void EDFields(Boolean bFlag)        
        {            
            btnBrowse.Enabled = !bFlag;                     
        }
      

        private void btnSave_Click(object sender, EventArgs e)
        {
            oCandidate = new DataAccess.Candidate();
            oImageConvert = new CommonFunction.CommonFunction();

            foreach (var oText in pnlMain.Controls.OfType<TextBox>().ToList())
            {
                if (oText.Text.Trim() == String.Empty)
                {
                    
                    oFrmMsgBox = new frmMessageBox("All fields are required.");
                    oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                    return;
                }
            }

            oMCandidate = new Model.Candidate();
            oMCandidate.UNIQUE_ID = eVariable.sID;
            oMCandidate._Position.ID = eVariable.sPositionID;
            oMCandidate._Party.ID = eVariable.sPartyID;
            oMCandidate.ELECTION_CODE = lblElectionNo.Text;
            oMCandidate.DATE_ADDED = DateTime.Now.ToString("yyyy-MM-dd");
            oMCandidate.ADDED_BY = eVariable.sUsername;
            
            if (txtParty.Text != "INDEPENDENT")
            {
                if (oCandidate.IsSamePositionAndParty(oMCandidate))
                {
                    oFrmMsgBox = new frmMessageBox("THERE SHOULD BE ONLY ONE POSITION PER PARTY");
                    oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                    return;
                }
            }                              

            oStringUtility = new CommonFunction.CommonFunction();
            if (pImage.Image != null)
            {
                oMCandidate.PROFILE_PIC = oStringUtility.CompressString(oImageConvert.ImageToBaseString(pImage.Image, ImageFormat.Png));
            }                            

            
            if (TransactionType  == eVariable.TransactionType.EDIT)
            {
                oMCandidate.MODIFIED_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                oMCandidate.MODIFIED_BY = eVariable.sUsername;
                oCandidate.UpdateCandidate(oMCandidate);
            }
            else
            {                
                oCandidate.InsertCandidate(oMCandidate);
            }

            oFrmCandidateList.LoadCandidates();            
            oFrmMsgBox = new frmMessageBox("Record has been successfully saved.");
            oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
            oFrmMsgBox.ShowDialog();
            Close();
            
           
        }

     
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            frmSearch oFrm = new frmSearch();
            oFrm.RecordType = frmSearch.RECORD_TYPE.STUDENT;
            oFrm.ShowDialog();            
            if (oFrm.oMStudent.UNIQUE_ID != null)
            {
                eVariable.sID = oFrm.oMStudent.UNIQUE_ID;
                txtCandidateID.Text = oFrm.oMStudent.UNIQUE_ID;
                txtFullname.Text = oFrm.oMStudent.FIRST_NAME + " " + oFrm.oMStudent.MIDDLE_NAME + " " + oFrm.oMStudent.LAST_NAME;
                txtDOB.Text = oFrm.oMStudent.DOB;
                txtAge.Text = oFrm.oMStudent.AGE;
                txtCourse.Text = oFrm.oMStudent.COURSE;
                txtSection.Text = oFrm.oMStudent.SECTION;
                txtContactNo.Text = oFrm.oMStudent.CONTACT_NO;
                txtAddress.Text = oFrm.oMStudent.ADDRESS;
            }
            
        }

        private void btnBrowsePos_Click(object sender, EventArgs e)
        {
            frmSearch oFrm = new frmSearch();
            oFrm.RecordType = frmSearch.RECORD_TYPE.POSITION;

            oFrm.ShowDialog();
            if (oFrm.oMPosition.ID != null)
            {
                eVariable.sPositionID = oFrm.oMPosition.ID;
                txtPosition.Text = oFrm.oMPosition.POSITION;
            }      
        }

    

        private void btnBrowseParty_Click(object sender, EventArgs e)
        {
            frmSearch oFrm = new frmSearch();
            oFrm.RecordType = frmSearch.RECORD_TYPE.PARTY;
            oFrm.ShowDialog();
            if (oFrm.oMParty.ID != null)
            {
                eVariable.sPartyID = oFrm.oMParty.ID;
                txtParty.Text = oFrm.oMParty.PARTY;
            }
        }

        

        private void lblUpload_Click(object sender, EventArgs e)
        {
            oDiagLog = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Image Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = ".jpg",
                Filter = "Image Files (*.png)|*.png",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (oDiagLog.ShowDialog() == DialogResult.OK)
            {
                pImage.Image = Image.FromFile(oDiagLog.FileName);
            } 
        }



        void GetElectionCode()
        {
            oElection = new DataAccess.SystemFunction();

            foreach (DataRow row in oElection.getElectionCode("","").Rows)
            {
                lblElectionNo.Text = row["CODE"].ToString();
            }
        
        }

      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            eVariable.ClearText(pnlMain);
            pImage.Image = null;
            TransactionType = eVariable.TransactionType.ADD;
        }

      

       
    }
}
