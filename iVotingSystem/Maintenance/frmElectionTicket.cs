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
    public partial class frmElectionTicket : Form
    {
        CommonFunction.CommonFunction oStringUtility;
        DataAccess.SystemFunction oElection;
        DataAccess.Candidate oCandidate;
        DataAccess.SystemFunction oGeneratedKey;

        Model.ElectionCode oMElection;


        Forms.frmMessageBox oFrmMsgBox;
        

        string sElectionCode;
        string sCurrentCode;
        string sStatus = string.Empty;

        public frmElectionTicket()
        {
            InitializeComponent();
        }

        void LoadKeys()
        {
            oElection = new DataAccess.SystemFunction();
            dgDetails.Rows.Clear();

            foreach(DataRow row in oElection.getElectionCode("","").Rows)
            {
                dgDetails.Rows.Add(row["CODE"],Convert.ToDateTime(row["DATE_ADDED"]).ToString("yyyy-MM-dd h:mm:ss tt"), row["STATUS"]);
                sCurrentCode = row["CODE"].ToString();
            }       
            
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmElectionTicket_Load(object sender, EventArgs e)
        {
            LoadKeys();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                oCandidate = new DataAccess.Candidate();
                oGeneratedKey = new DataAccess.SystemFunction();
                oStringUtility = new CommonFunction.CommonFunction();

                sElectionCode = DateTime.Now.ToString("yyyy") + "-" + oStringUtility.GetRandomString(5);             

                if (oCandidate.IsCandidateNotEmpty())
                {
                    oFrmMsgBox = new Forms.frmMessageBox("WARNING! YOU HAVE EXISTING ACTIVE CANDIDATE WITH CURRENT ELECTION TICKET NUMBER [ " + sCurrentCode + " ] USED. PROCEDING THIS ACTIVITY WILL MAKE THEM ALL INACTIVE. WOULD YOU LIKE TO PROCEED?");
                    oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.QUERY;
                    oFrmMsgBox.ShowDialog();
                    if (oFrmMsgBox.sAnswer.ToUpper() != "YES")
                    {
                        return;
                    }

                }
             
                oElection = new DataAccess.SystemFunction();
                oMElection = new Model.ElectionCode();

                oMElection.CODE = sElectionCode;
                oMElection.ADDED_DATE = DateTime.Now;
                oMElection.ADDED_BY = "Rolly";


                if (rdCloseElection.Checked)
                {
                    oElection.UpdateElectionCode();

                    oCandidate = new DataAccess.Candidate();
                    oCandidate.UpdateCandidateStatus(sCurrentCode);

                    oGeneratedKey = new DataAccess.SystemFunction();
                    oGeneratedKey.UpdateGeneratedKey(sCurrentCode);
                }
                else
                {
                    oElection.UpdateElectionCode();
                    oElection.InsertElectionCode(oMElection);

                    oCandidate = new DataAccess.Candidate();
                    oCandidate.UpdateCandidateStatus(sCurrentCode);

                    oGeneratedKey = new DataAccess.SystemFunction();
                    oGeneratedKey.UpdateGeneratedKey(sCurrentCode);
           
                }

                LoadKeys();
                oFrmMsgBox = new Forms.frmMessageBox("RECORD SUCESSFULLY SAVED.");
                oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();    

                
            }
            catch (Exception ex)
            {

            }
        }

   
    }
}
