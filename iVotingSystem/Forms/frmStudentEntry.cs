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
    public partial class frmStudentEntry : Form
    {

        public DataAccess.Student oStudent;
        public Model.Student oMStudent = new Model.Student();              

        public string sTitle = "Voting System Management";

        #region Forms
        frmStudentList oFrmStudents;
        frmMessageBox oFrmMsgBox;
        #endregion

        public eVariable.TransactionType TransactionType;
        public frmStudentEntry()
        {
            InitializeComponent();
            eVariable.DisableTextPanelEnterKey(pnlMain);
            eVariable.ValidNumber(txtContactNo);
        }

        public frmStudentEntry(frmStudentList oFrmStudent, Model.Student oData)
        {
            InitializeComponent();
            
            oMStudent = oData;
            this.oFrmStudents = oFrmStudent;
            eVariable.DisableTextPanelEnterKey(pnlMain);
            eVariable.ValidNumber(txtContactNo);

        }

        void TextKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }      

        void LoadStudents()
        {
            
            if (TransactionType == eVariable.TransactionType.EDIT)
            {                
                txtStudentID.Text = oMStudent.UNIQUE_ID;
                txtFname.Text = oMStudent.FIRST_NAME;
                txtMname.Text = oMStudent.MIDDLE_NAME;
                txtLname.Text = oMStudent.LAST_NAME;
                txtAge.Text = oMStudent.AGE;
                txtCourse.Text = oMStudent.COURSE;
                txtSection.Text = oMStudent.SECTION;
                txtContactNo.Text = oMStudent.CONTACT_NO;
                txtAddress.Text = oMStudent.ADDRESS;
                dtDOB.Value = Convert.ToDateTime(oMStudent.DOB);

                txtStudentID.Enabled = false;
            }
            else
            {
                txtStudentID.Enabled = true;
            }

            txtStudentID.Focus();
        }

   
        private void frmStudentEntry_Load(object sender, EventArgs e)
        {
            oStudent = new DataAccess.Student();
            LoadStudents();
            txtStudentID.Focus();
            if (TransactionType == eVariable.TransactionType.ADD)
            {
                txtStudentID.Enabled = false;
                txtStudentID.Text = oStudent.GetStudentAutoNo();
            }
            else
            {
                txtStudentID.Enabled = false;
                chkAutoNumber.Enabled = false;
            }
        }       

        private void btnSave_Click(object sender, EventArgs e)
        {
            oStudent = new DataAccess.Student();
            oMStudent = new Model.Student();

            foreach (var oText in pnlMain.Controls.OfType<TextBox>().ToList())
            {
                if (oText.Text.Trim() == String.Empty)
                {
                    oFrmMsgBox = new Forms.frmMessageBox(eVariable.MESSAGE.ALL_FIELDS_ARE_ALL_REQUIRED.ToString().Replace("_"," "));
                    oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                    return;
                }            
            }            

            oMStudent.UNIQUE_ID = txtStudentID.Text;
            oMStudent.FIRST_NAME = txtFname.Text;
            oMStudent.MIDDLE_NAME = txtMname.Text;
            oMStudent.LAST_NAME = txtLname.Text;            
            oMStudent.DOB = dtDOB.Value.ToString("yyyy-MM-dd");
            oMStudent.AGE = txtAge.Text;
            oMStudent.COURSE = txtCourse.Text;
            oMStudent.SECTION = txtSection.Text;
            oMStudent.CONTACT_NO = txtContactNo.Text;
            oMStudent.ADDRESS = txtAddress.Text;                        

            if (TransactionType == eVariable.TransactionType.EDIT)
            {
                oStudent.UpdateStudent(oMStudent);
            }
            else
            {
                if (!oStudent.IsRecordExists(oMStudent))
                {
                    oStudent.InsertStudent(oMStudent);
                }
                else
                {
                    oFrmMsgBox = new frmMessageBox(eVariable.MESSAGE.RECORD_ALREADY_EXISTS.ToString().Replace("_"," "));
                    oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
                    oFrmMsgBox.ShowDialog();
                    return;
                }
            }
            oFrmStudents.LoadStudents();                       
            oFrmMsgBox = new frmMessageBox(eVariable.MESSAGE.RECORD_HAS_BEEN_SUCCESSFULLY_SAVE.ToString().Replace("_"," "));
            oFrmMsgBox.MessageType = frmMessageBox.MESSAGE_TYPE.INFO;
            oFrmMsgBox.ShowDialog();
            TransactionType = eVariable.TransactionType.ADD;
            Close();
            
        }

        private void pnlBottom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;   
        }

        private void dtDOB_ValueChanged(object sender, EventArgs e)
        {
            txtAge.Text = eVariable.GetAge(dtDOB.Value.Date,DateTime.Now.Date).ToString();
        }

        private void chkAutoNumber_Click(object sender, EventArgs e)
        {
            oStudent = new DataAccess.Student();

            if (chkAutoNumber.Checked)
            {
                txtStudentID.Enabled = false;
                txtStudentID.Text = oStudent.GetStudentAutoNo();
            }
            else
            {
                txtStudentID.Enabled = true;
                txtStudentID.Text = "";
            }
        }
       
    }
}
