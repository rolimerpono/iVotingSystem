using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iVotingSystem.Forms
{
    public partial class frmStudentList : Form
    {
        DataAccess.Student oStudent;
        Model.Student oMStudent;
        public int iStudentID;

        public string sTitle = "Voting System Management";

        frmMessageBox oFrmMsgBox;
        frmStudentEntry oFrm;
        public frmStudentList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            oFrm = new frmStudentEntry();
            oFrm.TransactionType = ePublicVariable.eVariable.TransactionType.ADD;
            oFrm.ShowDialog();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboSearchBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void LoadStudents()
        {
            try
            {
                oStudent = new DataAccess.Student();
                dgStudents.Rows.Clear();

                foreach (DataRow row in oStudent.getStudent(cboSearchBy.Text,tbxSearch.Text).Rows)
                {
                    dgStudents.Rows.Add(row["STUDENT_ID"], row["FIRST_NAME"], row["MIDDLE_NAME"], row["LAST_NAME"], row["DOB"], row["AGE"], row["COURSE"], row["SECTION"], row["ADDRESS"], row["CONTACT_NO"]);
                }

                lblTotalRecords.Text = dgStudents.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
               
            }

        }

        private void frmStudentList_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

      

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgStudents.Rows.Count > 0)
            {
                if (dgStudents.SelectedCells[0].Selected == true)
                {
                    oFrm = new frmStudentEntry(this,oMStudent);
                    oFrm.TransactionType = ePublicVariable.eVariable.TransactionType.EDIT;
                    oFrm.ShowDialog();
                }
            }
        }

        void DatagridSelect(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                oMStudent = new Model.Student();

                if (dgStudents.Rows.Count > 0 && e.RowIndex >= 0)
                {
                    oMStudent.UNIQUE_ID = dgStudents.Rows[e.RowIndex].Cells[0].Value.ToString();
                    oMStudent.FIRST_NAME = dgStudents.Rows[e.RowIndex].Cells[1].Value.ToString();
                    oMStudent.MIDDLE_NAME = dgStudents.Rows[e.RowIndex].Cells[2].Value.ToString();
                    oMStudent.LAST_NAME = dgStudents.Rows[e.RowIndex].Cells[3].Value.ToString();
                    oMStudent.DOB = dgStudents.Rows[e.RowIndex].Cells[4].Value.ToString();
                    oMStudent.AGE = dgStudents.Rows[e.RowIndex].Cells[5].Value.ToString().ToString();
                    oMStudent.COURSE = (dgStudents.Rows[e.RowIndex].Cells[6].Value.ToString());
                    oMStudent.SECTION = (dgStudents.Rows[e.RowIndex].Cells[7].Value.ToString());
                    oMStudent.ADDRESS = dgStudents.Rows[e.RowIndex].Cells[8].Value.ToString();
                    oMStudent.CONTACT_NO = dgStudents.Rows[e.RowIndex].Cells[9].Value.ToString();

                }
            }
            catch (Exception ex)
            {
      
            }
        }

        private void dgStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DatagridSelect(sender, e);
        }

        private void dgStudents_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DatagridSelect(sender, e);
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            try
            {
                oStudent = new DataAccess.Student();

                if (MessageBox.Show("You want to archive this record?", sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    oStudent.DeleteStudents(oMStudent.UNIQUE_ID);
                    LoadStudents();
                    MessageBox.Show("Record has been succesfully archived", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
   
            }
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }


    
       
    }
}
