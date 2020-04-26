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
    public partial class frmLock : Form
    {
        frmMessageBox oFrmMsgBox;
        string sKey = string.Empty;
        public frmLock()
        {
            InitializeComponent();
            eVariable.DisableTextPanelEnterKey(pnlBody);
        }

        

        public frmLock(string sPass)
        {
            InitializeComponent();
            eVariable.DisableTextPanelEnterKey(pnlBody);
            sKey = sPass;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == sKey.Trim())
            {
                Close();
            }
            else
            {
                oFrmMsgBox = new frmMessageBox("THE PASSWORD YOU ENTERED IS INCORRECT!");
                oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;                
                oFrmMsgBox.ShowDialog();
                txtPassword.Focus();
            }
        }
    }
}
