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
    public partial class frmParty : Form
    {

        DataAccess.Party oParty;
        Model.Party oMParty;
   
        frmMessageBox oFrmMsgBox;

        eVariable.TransactionType TransactionType;
        public frmParty()
        {
            InitializeComponent();
            eVariable.DisableTextPanelEnterKey(panel3);
        }

        public void LoadParty()
        {
            try
            {
                oParty = new DataAccess.Party();
                dgParty.Rows.Clear();
                eVariable.DisableGridColumnSort(dgParty);
                foreach (DataRow row in oParty.getParty("", "").Rows)
                {
                    dgParty.Rows.Add(row[0], row[1],row[2]);
                }
            }
            catch (Exception ex)
            {
              
            }

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            oParty = new DataAccess.Party();
            if (txtParty.Text.Trim() != String.Empty)
            {

                oMParty = new Model.Party();
                oMParty.ID = eVariable.sID;
                oMParty.PARTY = txtParty.Text;
                oMParty.STATUS = chkStatus.Checked == true ? "ACTIVE" : "INACTIVE";

                if (TransactionType == eVariable.TransactionType.EDIT)
                {
                    if (!oParty.UpdateParty(oMParty))
                    {
                        oFrmMsgBox = new frmMessageBox("THIS RECORD CANNOT BE UPDATED");
                        oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                        oFrmMsgBox.ShowDialog();
                        TransactionType = eVariable.TransactionType.ADD;
                        LoadParty();
                        eVariable.ClearText(panel3);
                        return;                    
                    }

                }
                else
                {
                    if (oParty.IsPartyExists(txtParty.Text))
                    {
                        oFrmMsgBox = new frmMessageBox("RECORD ALREADY EXISTS");
                        oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                        oFrmMsgBox.ShowDialog();
                        return;
                    }

                    oParty = new DataAccess.Party();
                    oParty.InsertParty(oMParty);
                }                                
                oFrmMsgBox = new frmMessageBox("RECORD SUCCESSFULLY SAVED");
                oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();
                TransactionType = eVariable.TransactionType.ADD;
                LoadParty();
                eVariable.ClearText(panel3);
            }

        }
        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            eVariable.ClearText(panel3);
            TransactionType = eVariable.TransactionType.ADD;
        }

        private void dgParty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DatagridSelect(sender, e);
        }

        private void dgParty_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DatagridSelect(sender, e);
        }

        void DatagridSelect(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgParty.Rows.Count > 0 && e.RowIndex >= 0)
                {
                    oMParty = new Model.Party();
                    oMParty.ID = dgParty.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    oMParty.PARTY = dgParty.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                    oMParty.STATUS = dgParty.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
           
            }
        
        }
      
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (oMParty.ID.Trim() != String.Empty)
            {
                eVariable.sID = oMParty.ID;
                txtParty.Text = oMParty.PARTY;
                chkStatus.Checked = oMParty.STATUS == "ACTIVE" ? true : false;
                TransactionType = eVariable.TransactionType.EDIT;
            }
        }

        private void frmParty_Load(object sender, EventArgs e)
        {
            LoadParty();
            TransactionType = eVariable.TransactionType.ADD;
        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {

        }
    }
}
