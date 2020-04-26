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
    public partial class frmSearch : Form
    {

        DataAccess.Student oStudent;
        DataAccess.Party oParty;
        DataAccess.Position oPosition;

        public Model.Student oMStudent { get; set; }
        public Model.Party oMParty { get; set; }
        public Model.Position oMPosition { get; set; }
        private int iRowIndex = 0;

        public frmSearch()
        {
            InitializeComponent();
            oMStudent = new Model.Student();
            oMParty = new Model.Party();
            oMPosition = new Model.Position();
            eVariable.DisableTextPanelEnterKey(panel3);
            eVariable.DisableKeyPress(cboSearch);
        }

        public enum RECORD_TYPE : int
        { 
        
            STUDENT = 0,
            PARTY = 1,
            POSITION= 2
        }

        public RECORD_TYPE RecordType;


        private void LoadRecords()
        {
            try
            {
                if (RecordType == RECORD_TYPE.STUDENT)
                {
                    StudentGrid();
                    oStudent = new DataAccess.Student();
                    dgDetails.Rows.Clear();
                    foreach (DataRow row in oStudent.getCandidate(cboSearch.Text, txtSearch.Text).Rows)
                    {
                        dgDetails.Rows.Add(row["STUDENT_ID"], row["FIRST_NAME"], row["MIDDLE_NAME"], row["LAST_NAME"], row["DOB"], row["AGE"], row["COURSE"], row["SECTION"], row["ADDRESS"], row["CONTACT_NO"]);
                    }
                }
                else if (RecordType == RECORD_TYPE.POSITION)
                {
                    PositionGrid();
                    oPosition = new DataAccess.Position();
                    dgDetails.Rows.Clear();

                    foreach (DataRow row in oPosition.getPosition(cboSearch.Text, txtSearch.Text).Rows)
                    {
                        dgDetails.Rows.Add(row[0], row[1]);
                    }
                }
                else if (RecordType == RECORD_TYPE.PARTY)
                {
                    PartyGrid();
                    oParty = new DataAccess.Party();
                    dgDetails.Rows.Clear();

                    foreach (DataRow row in oParty.getParty(cboSearch.Text, txtSearch.Text).Rows)
                    {
                        dgDetails.Rows.Add(row[0], row[1]);
                    }
                }
            }
            catch (Exception ex)
            { }
        
        }      

        private void StudentGrid()
        {
            dgDetails.Columns.Clear();
            dgDetails.Columns.Add("", "STUDENT ID");
            dgDetails.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDetails.Columns.Add("", "FIRST NAME");
            dgDetails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDetails.Columns.Add("", "MIDDLE NAME");
            dgDetails.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDetails.Columns.Add("", "LAST NAME");
            dgDetails.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDetails.Columns.Add("", "DOB");
            dgDetails.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDetails.Columns.Add("", "AGE");
            dgDetails.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDetails.Columns.Add("", "COURSE");
            dgDetails.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDetails.Columns.Add("", "SECTION");
            dgDetails.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDetails.Columns.Add("", "ADDRESS");
            dgDetails.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDetails.Columns.Add("", "CONTACT NO");
            dgDetails.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void PartyGrid()
        {
            dgDetails.Columns.Clear();
            dgDetails.Columns.Add("", "ID");
            dgDetails.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDetails.Columns.Add("", "PARTY");
            dgDetails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           
        }
        private void PositionGrid()
        {
            dgDetails.Columns.Clear();
            dgDetails.Columns.Add("", "ID");
            dgDetails.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgDetails.Columns.Add("", "POSITION");
            dgDetails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        void SearchFilter()
        {
            if (RecordType == RECORD_TYPE.STUDENT)
            {
                cboSearch.Items.Clear();
                cboSearch.Text = "STUDENT ID";
                cboSearch.Items.Add("STUDENT ID");
                cboSearch.Items.Add("FIRST NAME");
                cboSearch.Items.Add("MIDDLE NAME");
                cboSearch.Items.Add("LAST NAME");
            }
            else if (RecordType == RECORD_TYPE.PARTY)
            {
                cboSearch.Items.Clear();
                cboSearch.Text = "ID";
                cboSearch.Items.Add("ID");
                cboSearch.Items.Add("PARTY");           
            }
            else if (RecordType == RECORD_TYPE.POSITION)
            {
                cboSearch.Items.Clear();
                cboSearch.Text = "ID";
                cboSearch.Items.Add("ID");
                cboSearch.Items.Add("POSITION");   
            }
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            SearchFilter();
            LoadRecords();
        }

        private void dgDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgDetails.Rows.Count > 0 && e.RowIndex >= 0)
            {
                iRowIndex = e.RowIndex;
                SelectRecord();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgDetails.Rows.Count > 0 && iRowIndex >= 0)
            {                
                SelectRecord();
            }
        }

        private void SelectRecord()
        {
            if (dgDetails.Rows.Count >= 0 && iRowIndex >= 0)
            {
                if (RecordType == RECORD_TYPE.STUDENT)
                {

                    oMStudent = new Model.Student();

                    oMStudent.UNIQUE_ID = dgDetails.Rows[iRowIndex].Cells[0].Value.ToString();
                    oMStudent.FIRST_NAME = dgDetails.Rows[iRowIndex].Cells[1].Value.ToString();
                    oMStudent.MIDDLE_NAME = dgDetails.Rows[iRowIndex].Cells[2].Value.ToString();
                    oMStudent.LAST_NAME = dgDetails.Rows[iRowIndex].Cells[3].Value.ToString();
                    oMStudent.DOB = dgDetails.Rows[iRowIndex].Cells[4].Value.ToString();
                    oMStudent.AGE = dgDetails.Rows[iRowIndex].Cells[5].Value.ToString();
                    oMStudent.COURSE = dgDetails.Rows[iRowIndex].Cells[6].Value.ToString();
                    oMStudent.SECTION = dgDetails.Rows[iRowIndex].Cells[7].Value.ToString();
                    oMStudent.CONTACT_NO = dgDetails.Rows[iRowIndex].Cells[8].Value.ToString();
                    oMStudent.ADDRESS = dgDetails.Rows[iRowIndex].Cells[9].Value.ToString();


                }
                else if (RecordType == RECORD_TYPE.PARTY)
                {

                    oMParty = new Model.Party();

                    oMParty.ID = dgDetails.Rows[iRowIndex].Cells[0].Value.ToString();
                    oMParty.PARTY = dgDetails.Rows[iRowIndex].Cells[1].Value.ToString();

                }
                else if (RecordType == RECORD_TYPE.POSITION)
                {

                    oMPosition = new Model.Position();

                    oMPosition.ID = dgDetails.Rows[iRowIndex].Cells[0].Value.ToString();
                    oMPosition.POSITION = dgDetails.Rows[iRowIndex].Cells[1].Value.ToString();

                }
            }

            Close();
        
        }

        private void dgDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgDetails.Rows.Count > 0 && e.RowIndex >= 0)
            {
                iRowIndex = e.RowIndex;                
            }
        }

        private void dgDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgDetails.Rows.Count > 0 && e.RowIndex >= 0)
            {
                iRowIndex = e.RowIndex;                
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }        
        
    }
}
