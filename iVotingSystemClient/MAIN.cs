using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iVotingSystemClient
{
    public partial class MAIN : Form
    {

        DataAccess.Student oStudent;

        DataAccess.VotersLogin oVotersLogin;

        Form xFrm;
        public string sVoters_ID = string.Empty;
        public string sPassword = string.Empty;

        Forms.frmMessageBox oFrmMsgBox;
        public MAIN()
        {
            InitializeComponent();
        }

        public MAIN(string sUserID,string sPass)
        {
            InitializeComponent();

            sVoters_ID = sUserID;
            sPassword = sPass;
        }
       

        private void btnVote_Click(object sender, EventArgs e)
        {
            oVotersLogin = new DataAccess.VotersLogin();
            string sStatus = oVotersLogin.GetVotersStatus(sVoters_ID, sPassword);

            if (sStatus.Trim() != "ACTIVE")
            {
                oFrmMsgBox = new Forms.frmMessageBox("YOU HAVE ALREADY VOTED. PLEASE COME AGAIN THANK YOU.");
                oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();
                return;
            }

            xFrm = new Forms.frmCandidate(sVoters_ID);
            Form_Load(xFrm);
        }

        void Form_Load(Form xForm)
        {
            xForm.TopLevel = false;
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(xForm);
            xForm.Show();
        }

        private void MAIN_Load(object sender, EventArgs e)
        {
            DataAccess.SystemFunction oVotingSchedule = new DataAccess.SystemFunction();

            foreach (DataRow row in oVotingSchedule.GetVotingSchedule().Rows)
            {
                lblDateTime.Text = row["DATE_END"].ToString().Trim() + " / " + row["TIME_END"].ToString().Trim();
            }

            GetUser();
        }

        void GetUser()
        {
            oStudent = new DataAccess.Student();

            foreach (DataRow row in oStudent.getStudent("ID", sVoters_ID).Rows)
            {
                lblUser.Text = row["FIRST_NAME"].ToString() + " " + row["LAST_NAME"].ToString();  
            }
        
        }         
    }
}
