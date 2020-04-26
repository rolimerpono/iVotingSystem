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
    public partial class frmUser : Form
    {
        
        DataAccess.User oUser;
        Model.User oMUser;
        Boolean bEdit = false;

        frmMessageBox oFrmMsgBox;
        public frmUser()
        {
            InitializeComponent();
            eVariable.DisableTextPanelEnterKey(pnlInfo);
        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtFname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtMname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtLname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContactNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        void LoadRecords()
        {
            oUser = new DataAccess.User();
            dgDetails.Rows.Clear();

            foreach (DataRow row in oUser.GetUser("", "").Rows)
            {
                dgDetails.Rows.Add(row["USERNAME"], row["FULLNAME"], row["PASSWORD"], row["ROLE"], row["CONTACT_NO"], row["ADDRESS"]);
            }
        
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            oUser = new DataAccess.User();
            oMUser = new Model.User();

            foreach (var o in pnlInfo.Controls.OfType<TextBox>().ToList())
            {
                if (o.Text.Trim() == String.Empty)
                {
                    oFrmMsgBox = new frmMessageBox("ALL FIELDS ARE REQUIRED!");
                    oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                    o.Focus();
                    return;
                }
            }

            if (txtPassword.Text.Trim() != txtRePassword.Text.Trim())
            {
                oFrmMsgBox = new frmMessageBox("PASSWORD YOU ENTER DID NOT MATCH.");
                oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();
                txtPassword.Focus();
                return;
            }

            oMUser.FULLNAME = txtFullname.Text;
            oMUser.USERNAME = txtUsername.Text;
            oMUser.PASSWORD = txtPassword.Text;
            oMUser.ROLE = cboRole.Text;
            oMUser.CONTACT_NO = txtContactNo.Text;
            oMUser.ADDRESS = txtAddress.Text;

            if (bEdit)
            {
                oUser.UpdateUser(oMUser);
                ClearFields();
                LoadRecords();
                oFrmMsgBox = new frmMessageBox("RECORD SUCCESSFULL SAVED.");
                oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;   
                oFrmMsgBox.ShowDialog();
                bEdit = false;
            }
            else
            {
                if (oUser.IsRecordExists(txtUsername.Text.Trim()))
                {
                    oFrmMsgBox = new frmMessageBox("USERNAME YOU CREATED ALREADY EXISTS.");
                    oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                    return;
                }
                oUser.InsertUser(oMUser);
                ClearFields();
                LoadRecords();
                oFrmMsgBox = new frmMessageBox("RECORD SUCCESSFULL SAVED.");
                oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();

            }
        }

        void ClearFields()
        {
            foreach (var o in pnlInfo.Controls.OfType<TextBox>().ToList())
            {
                o.Text = string.Empty;
            }
            txtUsername.Enabled = true;
            cboRole.Text = string.Empty;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bEdit = true;
            if (oMUser.USERNAME.Trim() != String.Empty)
            {
                txtUsername.Text = oMUser.USERNAME;
                txtFullname.Text = oMUser.FULLNAME;
                txtPassword.Text = oMUser.PASSWORD;
                txtRePassword.Text = oMUser.PASSWORD;
                cboRole.Text = oMUser.ROLE;
                txtContactNo.Text = oMUser.CONTACT_NO;
                txtAddress.Text = oMUser.ADDRESS;

            }
            txtUsername.Enabled = false;
        }

        private void dgDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DatagridSelect(sender, e);
        }

        private void dgDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DatagridSelect(sender, e);
        }

        void DatagridSelect(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgDetails.Rows.Count > 0)
                {
                    oMUser = new Model.User();
                    oMUser.USERNAME = dgDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
                    oMUser.FULLNAME = dgDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                    oMUser.PASSWORD = dgDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                    oMUser.ROLE = dgDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
                    oMUser.CONTACT_NO = dgDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
                    oMUser.ADDRESS = dgDetails.Rows[e.RowIndex].Cells[5].Value.ToString();
                }
            }
            catch (Exception ex)
            {
              
            }
        }

        private void cboRole_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

     
        
    }
}
