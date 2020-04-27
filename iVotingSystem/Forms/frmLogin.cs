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

namespace iVotingSystem.Forms
{
    public partial class frmLogin : Form
    {
        XMLSerializer.Serializerset oXMLSerializerSet;
        CommonFunction.CommonFunction oUtility;
        DefaultLogin.User oDefUser;

        frmMessageBox oFrmMsgBox;

        MAIN oMainForm;

        DataAccess.SystemFunction oDatabase;
        DataAccess.User oUser;
        

        public frmLogin()
        {
            InitializeComponent();
            eVariable.DisableTextPanelEnterKey(pnlMain);
        }

        public void DefaultLogin()
        {
            oXMLSerializerSet = new XMLSerializer.Serializerset(Application.StartupPath + @"\Settings.xml");
            oDefUser = oXMLSerializerSet.ReadXmlSerialize(Application.StartupPath + @"\Settings.xml");
        } 

       

        private void btnLogin_Click(object sender, EventArgs e)
        {            
            oUser = new DataAccess.User();
            oUtility = new CommonFunction.CommonFunction();
            
            if (oUtility.Decrypt(oDefUser.USERNAME.Trim()) == txtUsername.Text && oUtility.Decrypt(oDefUser.PASSWORD.Trim()) == txtPassword.Text) //Default Login
            {
                this.Hide();
                this.ShowInTaskbar = false;
                ePublicVariable.eVariable.sUsername = txtUsername.Text;
                ePublicVariable.eVariable.sPassword = txtPassword.Text;
                ePublicVariable.eVariable.sFullname = oUtility.Decrypt(oDefUser.FULLNAME);
                ePublicVariable.eVariable.sRole = oUtility.Decrypt(oDefUser.ROLE);
                oMainForm = new MAIN();
                oMainForm.ShowDialog();                
            }

            else
            {
                #region CheckDatabase

                oDatabase = new DataAccess.SystemFunction();

                if (!oDatabase.IsDatabaseExits())
                {
                    oFrmMsgBox = new frmMessageBox("DATABASE DOES NOT EXISTS. PLEASE RESTORE FIRST THE DATABASE. THANK YOU");
                    oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();

                    Maintenance.frmBackupRestoreDB oFrm = new Maintenance.frmBackupRestoreDB();
                    oFrm.ShowDialog();
                }

                #endregion


                #region DBLogin
                if (oUser.IsLogin(txtUsername.Text.Trim(), txtPassword.Text.Trim()))
                {
                    
                    this.ShowInTaskbar = false;
                    this.Hide();
                    ePublicVariable.eVariable.sUsername = txtUsername.Text;
                    ePublicVariable.eVariable.sPassword = txtPassword.Text;
                    oMainForm = new MAIN();
                    oMainForm.ShowDialog();

                }
                else
                {
                    oFrmMsgBox = new Forms.frmMessageBox("PLEASE ENTER CORRECT USERNAME AND PASSWORD.");
                    oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                    txtUsername.Focus();
                }
                #endregion
            }            

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

        private void frmLogin_Load(object sender, EventArgs e)
        {
            DefaultLogin();
            Thread T = new Thread(new ThreadStart(GetCurrentDate));
            T.Start();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }      

    }
}
