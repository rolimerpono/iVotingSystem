using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ePublicVariable;

namespace iVotingSystemClient
{
    public partial class frmLogin : Form
    {

        DataAccess.VotersLogin oVotersLogin;
        DataAccess.SystemFunction oVotingSched;

        Forms.frmMessageBox oFrmMsgBox;
        MAIN oMainForm;

        string dDateStart;
        string dDateEnd;
        string tTimeStart;
        string tTimeEnd;
        string sStatus = string.Empty;

        
        

        public frmLogin()
        {
            InitializeComponent();

            eVariable.DisableTextPanelEnterKey(pnlMain);
            
        }      

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            oVotersLogin = new DataAccess.VotersLogin();
            if (lblStatus.Text == "OPEN")
            {

                if (oVotersLogin.IsLogin(txtUsername.Text.Trim(), txtPassword.Text.Trim()))
                {
                    string sStatus = oVotersLogin.GetVotersStatus(txtUsername.Text, txtPassword.Text);

                    if (sStatus.Trim() != "ACTIVE")
                    {
                        oFrmMsgBox = new Forms.frmMessageBox("YOU HAVE ALREADY VOTED. PLEASE COME AGAIN THANK YOU.");
                        oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                        oFrmMsgBox.ShowDialog();
                        return;
                    }

                    this.Hide();
                    oMainForm = new MAIN(txtUsername.Text,txtPassword.Text);
                    oMainForm.ShowDialog();

                }
                else
                {
                    oFrmMsgBox = new Forms.frmMessageBox("PLEASE ENTER CORRECT USERNAME AND PASSWORD.");
                    oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                    txtUsername.Focus();
                }
            }
            else
            {
                oFrmMsgBox = new Forms.frmMessageBox("VOTING SCHEDULE WAS CLOSE.");
                oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Thread T = new Thread(new ThreadStart(GetCurrentDate));
            T.Start();
        }

        void GetCurrentDate()
        {
            for (int i = 0; i <= 2000; i++)
            {
                Thread.Sleep(1000);
                this.Invoke((MethodInvoker)delegate
                {
                    lblCurrentDate.Text = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");
                });
                if (i == 1001)
                {
                    i = 0;
                }
            }
        }

        void GetVotingSchedule()
        {

            oVotingSched = new DataAccess.SystemFunction();

            foreach (DataRow row in oVotingSched.GetVotingSchedule().Rows)
            {
                dDateEnd = row["DATE_END"].ToString();
                dDateStart = row["DATE_START"].ToString();
                tTimeStart = row["TIME_START"].ToString();
                tTimeEnd = row["TIME_END"].ToString();
                sStatus = row["STATUS"].ToString().Trim();
            }
            if (sStatus != "CLOSED")
            {
                if (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) > Convert.ToDateTime(dDateEnd))
                {                
                    oVotingSched.UpdateScheduleStatus();                    
                }
            }

            lblCurrentDate.Text = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");

            GetScheduleStatus();
        }

        void GetScheduleStatus()
        {

            oVotingSched = new DataAccess.SystemFunction();
            if (oVotingSched.IsScheduleOpen())
            {
                lblStatus.Text = "OPEN";
                lblStatus.ForeColor = Color.Lime;
            }
            else
            {
                lblStatus.Text = "CLOSE";
                lblStatus.ForeColor = Color.Red;
            }

            
        }

    }
}
