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
    public partial class frmPosition : Form
    {

        DataAccess.Position oPosition;
        Model.Position oMPosition;   

        frmMessageBox oFrmMsgBox;
        eVariable.TransactionType TransactionType;

        public frmPosition()
        {
            InitializeComponent();
            eVariable.DisableTextPanelEnterKey(panel3);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            oPosition = new DataAccess.Position();
            if (tbxPosition.Text.Trim() != String.Empty)
            {
                
                    oMPosition = new Model.Position();
                    oMPosition.ID = eVariable.sID;
                    oMPosition.POSITION = tbxPosition.Text;
                    oMPosition.STATUS = chkStatus.Checked == true ? "ACTIVE" : "INACTIVE";

                    if (TransactionType == eVariable.TransactionType.EDIT)
                    {
                        oPosition.UpdatePosition(oMPosition);
                    }
                    else
                    {
                        if (oPosition.IsPositionExists(tbxPosition.Text))
                        {
                            oFrmMsgBox = new frmMessageBox("RECORD ALREADY EXISTS");
                            oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                            oFrmMsgBox.ShowDialog();
                            return;
                        }
                        oPosition = new DataAccess.Position();
                        oPosition.InsertPosition(oMPosition);
                    }
                    oFrmMsgBox = new frmMessageBox("RECORD SUCCESSFULLY SAVED");
                    oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                    LoadPosition();
                    eVariable.ClearText(panel3);
                    TransactionType = eVariable.TransactionType.ADD;
                    
              
            }
        }

        public void LoadPosition()
        {
            try
            {
                oPosition = new DataAccess.Position();
                dgPosition.Rows.Clear();
                eVariable.DisableGridColumnSort(dgPosition);
                foreach (DataRow row in oPosition.getPosition("","").Rows)
                {
                    dgPosition.Rows.Add(row[0], row[1],row[2]);
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (oMPosition.ID.Trim() != String.Empty)
            {
                eVariable.sID = oMPosition.ID;
                tbxPosition.Text = oMPosition.POSITION;
                chkStatus.Checked = oMPosition.STATUS == "ACTIVE" ? true : false;
                TransactionType = eVariable.TransactionType.EDIT;
            }
        }

        private void dgPosition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DatagridSelect(sender, e);
        }

        void DatagridSelect(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgPosition.Rows.Count > 0 && e.RowIndex >= 0)
                {
                    oMPosition = new Model.Position();
                    oMPosition.ID = dgPosition.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    oMPosition.POSITION = dgPosition.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                    oMPosition.STATUS = dgPosition.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        private void dgPosition_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DatagridSelect(sender, e);
        }

        private void frmPosition_Load(object sender, EventArgs e)
        {
            LoadPosition();
        }

   
        private void btnCancel_Click(object sender, EventArgs e)
        {
            eVariable.ClearText(panel3);
            TransactionType = eVariable.TransactionType.ADD;
        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}
