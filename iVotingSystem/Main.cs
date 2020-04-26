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

namespace iVotingSystem
{
    public partial class MAIN : Form
    {
        Form xFrm;

        XMLSerializer.Serializerset oXMLSerializerSet;
        DefaultLogin.User oDefUser;

        Forms.frmMessageBox oFrmMsgBox;
        DataAccess.SystemFunction oElection;
        DataAccess.SystemFunction oDatabase;

        DataAccess.User oUser;
        public MAIN()
        {
            InitializeComponent();                 
        }

        void GetUser()
        {
            oUser = new DataAccess.User();

            foreach (DataRow row in oUser.GetUser("Username", eVariable.sUsername).Rows)
            {
                eVariable.sRole = row["ROLE"].ToString();
                eVariable.sFullname = row["FULLNAME"].ToString();
            }

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            GetUser();
            LoadDashBoard();
            Thread T = new Thread(new ThreadStart(GetCurrentDate));
            T.Start();
        }

        void Form_Load(Form xForm)
        {
            xForm.TopLevel = false;

            foreach (Control item in pnlMain.Controls)
            {
                if (!item.Name.Contains("SubMenu"))
                {
                    pnlMain.Controls.Remove(item);
                    break; 
                }
            }

            pnlMain.Controls.Add(xForm);
            xForm.Show();                
        }

   

        

        private void btnReports_Click(object sender, EventArgs e)
        {
            IsDBExists();
            MovePanel(btnReports, pnlRSubMenu);
            pnlUSubMenu.Visible = false;
            
            if (pnlRSubMenu.Visible == true)
            {
                pnlRSubMenu.Visible = false;
                btnReports.Text = btnReports.Text.Replace("-", "+");                
            }
            else
            {
                ClearControls();
                pnlRSubMenu.Visible = true;
                btnReports.Text = btnReports.Text.Replace("+", "-");
            }
            btnUtility.Text = btnUtility.Text.Replace("-", "+");
         
        }        

        private void btnPosition_Click(object sender, EventArgs e)
        {
            IsDBExists();
            ClearControls();
            Forms.frmPosition oFrm = new Forms.frmPosition();
            oFrm.ShowDialog();
        }

        private void btnParty_Click(object sender, EventArgs e)
        {
            IsDBExists();
            ClearControls();
            Forms.frmParty oFrm = new Forms.frmParty();
            oFrm.ShowDialog();
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            ClearControls();
            Maintenance.frmImportExport oFrm = new Maintenance.frmImportExport();
            oFrm.ShowDialog();
        }

        void ClearControls()
        {
            foreach (Control item in pnlMain.Controls)
            {
                if (!item.Name.Contains("SubMenu"))
                {
                    if (item.Name.Contains("dashboard"))
                    {
                        return;
                    }
                    pnlMain.Controls.Remove(item);
                }
                else
                {
                    pnlRSubMenu.Visible = false;
                    pnlUSubMenu.Visible = false;
                    break;
                }

            }

            btnUtility.Text = btnUtility.Text.Replace("-", "+");
            btnReports.Text = btnReports.Text.Replace("-", "+");
        }

     
        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        void MovePanel(Control btn, Control oPanel)
        {          
            oPanel.Top = btn.Top + 118;
            oPanel.Left = btn.Left;
        }  

 
        private void btnUtility_Click(object sender, EventArgs e)
        {
            IsDBExists();            
            MovePanel(btnUtility,pnlUSubMenu);
            pnlRSubMenu.Visible = false;
            
            if (pnlUSubMenu.Visible == true)
            {
                pnlUSubMenu.Visible = false;
                btnUtility.Text = btnUtility.Text.Replace("-", "+");
                
            }
            else
            {
                ClearControls();
                pnlUSubMenu.Visible = true;
                btnUtility.Text = btnUtility.Text.Replace("+", "-");
            }

            btnReports.Text =  btnReports.Text.Replace("-", "+");

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

        private void btnUserAccount_Click(object sender, EventArgs e)
        {
            ClearControls();            
            Forms.frmUser oFrm = new Forms.frmUser();
            oFrm.ShowDialog();
        }

        public void DefaultLogin()
        { 
            oXMLSerializerSet = new XMLSerializer.Serializerset(@"D:\XML_Data.xml");
            oDefUser = oXMLSerializerSet.ReadXmlSerialize(@"D:\XML_Data.xml");            
        }

        private void btnCandidateList_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlRSubMenu.Visible = false;
            xFrm = new Reports.frmRecordList();
            Form_Load(xFrm);
        }

        private void btnBackUpRestore_Click(object sender, EventArgs e)
        {
            ClearControls();
            Maintenance.frmBackupRestoreDB oFrm = new Maintenance.frmBackupRestoreDB();
            oFrm.ShowDialog();
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            IsDBExists();
            ClearControls();
            xFrm = new Forms.frmStudentList();
            Form_Load(xFrm);
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            oDatabase = new DataAccess.SystemFunction();
            if (!oDatabase.IsDatabaseExits())
            {
                oFrmMsgBox = new Forms.frmMessageBox("DATABASE DOES NOT EXISTS. PLEASE RESTORE FIRST THE DATABASE. THANK YOU");
                oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();

                Maintenance.frmBackupRestoreDB oFrmDB = new Maintenance.frmBackupRestoreDB();
                oFrmDB.ShowDialog();

                return;
            }

            ClearControls();           
            Forms.frmLock oFrm = new Forms.frmLock(eVariable.sPassword);
            oFrm.ShowDialog();

        }


        private void btnCandidate_Click(object sender, EventArgs e)
        {
            IsDBExists();
            ClearControls();
            oElection = new DataAccess.SystemFunction();
            if (!oElection.IsElectionCodeActive())
            {
                oFrmMsgBox = new Forms.frmMessageBox("THERE IS NO ACTIVE ELECTION TICKET NUMBER. PLEASE OPEN FIRST TO START. THANK YOU. CLICK : UTILITY ELECTION TICKET NO");
                oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();
                return;
            }

            xFrm = new Forms.frmCandidateList();
            Form_Load(xFrm);
        }

        void LoadDashBoard()
        {
           
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LoadDashBoard();
        }

        void IsDBExists()
        {
            oDatabase = new DataAccess.SystemFunction();
            if (!oDatabase.IsDatabaseExits())
            {
                oFrmMsgBox = new Forms.frmMessageBox("DATABASE DOES NOT EXISTS. PLEASE RESTORE FIRST THE DATABASE. THANK YOU");
                oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();

                Maintenance.frmBackupRestoreDB oFrmDB = new Maintenance.frmBackupRestoreDB();
                oFrmDB.ShowDialog();

                return;
            }
        
        }

        private void btnElectionTicket_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlUSubMenu.Visible = false;
            Maintenance.frmElectionTicket oFrm = new Maintenance.frmElectionTicket();
            oFrm.ShowDialog();
        }

        private void btnElectionSched_Click(object sender, EventArgs e)
        {
            ClearControls();
            Maintenance.frmElectionSchedule oFrm = new Maintenance.frmElectionSchedule();
            oFrm.ShowDialog();
        }

        private void btnUVotersPass_Click(object sender, EventArgs e)
        {
            ClearControls();
            Maintenance.frmGenerateKey oFrm = new Maintenance.frmGenerateKey();
            oFrm.ShowDialog();
        }

        private void btnVotersList_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlRSubMenu.Visible = false;
            xFrm = new Reports.frmRecordList();
            Form_Load(xFrm);
        }

        private void btnVoteResult_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlRSubMenu.Visible = false;
            xFrm = new Reports.frmRecordList();
            Form_Load(xFrm);
        }

        private void btnRecordList_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlRSubMenu.Visible = false;
            xFrm = new Reports.frmRecordList();
            Form_Load(xFrm);
        }

        private void btnVotersPass_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlRSubMenu.Visible = false;
            Forms.frmLock oFrm = new Forms.frmLock(eVariable.sPassword);
            oFrm.ShowDialog();

            Maintenance.frmGenerateKey oFx = new Maintenance.frmGenerateKey();
            oFx.ShowDialog();
        }   
    }
}
