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
    public partial class frmBackupRestoreDB : Form
    {
        DataAccess.SystemFunction oBackup;
        DataAccess.SystemFunction oRestore;

        Forms.frmMessageBox oFrmMsgBox;
        public frmBackupRestoreDB()
        {
            InitializeComponent();
        }

       
        private void btnBackup_Click(object sender, EventArgs e)
        {
            
        }

        void ClearFields()
        {
            txtDBBackup.Text = string.Empty;
            txtDBRestore.Text = string.Empty;
        }


        private void btnBrowseB_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog oFolderDialog = new FolderBrowserDialog();
          
            if (oFolderDialog.ShowDialog() == DialogResult.OK)
            {                
                txtDBBackup.Text = oFolderDialog.SelectedPath;
            }
        }

    

        private void btnCloseB_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCloseR_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnButton_Click(object sender, EventArgs e)
        {
            if (btnButton.Text.Trim() == "BACKUP")
            {
                #region Backup
                oBackup = new DataAccess.SystemFunction();

                if (txtDBBackup.Text.Trim() != string.Empty)
                {
                    if (oBackup.IsDatabaseExits())
                    {
                        oBackup.BackupDatabase(txtDBBackup.Text);

                        oFrmMsgBox = new Forms.frmMessageBox("DATABASE SUCCESSFULL BACKUP IN PATH :" + " " + txtDBBackup.Text);
                        oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                        oFrmMsgBox.ShowDialog();
                    }
                    else
                    {
                        oFrmMsgBox = new Forms.frmMessageBox("DATABASE DOES NOT EXISTS");
                        oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                        oFrmMsgBox.ShowDialog();
                    }

                }
                else
                {
                    oFrmMsgBox = new Forms.frmMessageBox("PLEASE SELECT DISTINATION FOLDER TO SAVE THE FILE.");
                    oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                }
                txtDBBackup.Text = string.Empty;
                return;
                #endregion
            }
            else
            {
                #region Restore

                if (txtDBRestore.Text.Trim() != String.Empty)
                {
                    oRestore = new DataAccess.SystemFunction();
                    //if (oRestore.IsDatabaseExits())
                    //{
                    //    oRestore.DropDatabase();                    
                    //}                    
                    oRestore.RestoreDatabase(txtDBRestore.Text);

                    oFrmMsgBox = new Forms.frmMessageBox("DATABASE SUCCESSFULLY RESTORED");
                    oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                }
                txtDBRestore.Text = string.Empty;
                return;
                #endregion
            }
        }

        private void tbControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbControl.SelectedIndex == 0)
            {
                btnButton.Text = "BACKUP";
            }
            else
            {
                btnButton.Text = "RESTORE";
            }
        }

        private void btnBrowseR_Click(object sender, EventArgs e)
        {

            OpenFileDialog oDiaglog = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse .BAK Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "bak",
                Filter = "BAK files (*.BAK)|*.BAK",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (oDiaglog.ShowDialog() == DialogResult.OK)
            {
                txtDBRestore.Text = oDiaglog.FileName;
            }
        }

      
         
    }
}
