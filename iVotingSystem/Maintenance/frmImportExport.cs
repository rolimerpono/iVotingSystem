using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace iVotingSystem.Maintenance
{
    
    public partial class frmImportExport : Form
    {
        
        DataAccess.Student oStudent;
        DataAccess.Candidate oCandidate;        


        List<Model.Student> lstStudent;
        List<Model.Candidate> lstCandidate;

        String sTitle = "Voting Management System";

        Forms.frmMessageBox oFrmMsgBox;
        SaveFileDialog oSaveFileDialog;
        private string[] oOutputCSV;
        public frmImportExport()
        {
            InitializeComponent();
        }

     
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboImport_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboExport_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;   
        }

        private void tbControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbControl.SelectedIndex == 0)
            {
                btnButton.Text = "IMPORT";

                btnPopulate.Visible = false;
            }
            else
            {
                btnButton.Text = "EXPORT";
                btnPopulate.Visible = true;
            }
        }

        private void btnButton_Click(object sender, EventArgs e)
        {

            if (btnButton.Text.Trim() == "IMPORT")
            {

                if (txtImportPath.Text.Trim() == String.Empty)
                {
                    MessageBox.Show("Please select file to import");
                    return;
                }

                #region Import
                try
                {
                    if (cboImport.SelectedText == "STUDENT" || cboImport.Text == "STUDENT")
                    {
                        if (dgImport.Rows.Count > 0)
                        {
                            foreach (var iData in lstStudent)
                            {
                                oStudent = new DataAccess.Student();
                                if (!oStudent.IsRecordExists(iData))
                                {
                                    oStudent.InsertStudent(iData);
                                }
                            }

                            MessageBox.Show("Record has been succesfully saved", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    if (cboImport.SelectedText == "POSITION" || cboImport.Text == "POSITION")
                    {
                        if (dgImport.Rows.Count > 0)
                        {
                            foreach (var iData in lstCandidate)
                            {
                                oCandidate = new DataAccess.Candidate();
                                oCandidate.InsertCandidate(iData);
                            }
                            MessageBox.Show("Record has been succesfully saved", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                }
                catch (Exception ex)
                {
            
                }
                #endregion
            }
            else
            {
                if (txtExportPath.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("PLEASE SELECT DISTINATION FILE PATH");
                    return;
                }

                if (dgExport.Rows.Count == 0)
                {
                    MessageBox.Show("POPULATE RECORD FIRST");
                    return;
                }

                #region Export
                ExtractData();
                #endregion

            }
        }
        private void ExtractData()
        {

            try
            {
                int iColumnCount = dgExport.Columns.Count;
                string sColumnNames = "";

                oOutputCSV = new string[dgExport.Rows.Count + 1];
                for (int i = 0; i < iColumnCount; i++)
                {
                    sColumnNames += dgExport.Columns[i].HeaderText.ToString() + ",";
                }
                oOutputCSV[0] += sColumnNames;
                for (int i = 1; (i - 1) < dgExport.Rows.Count; i++)
                {
                    for (int j = 0; j < iColumnCount; j++)
                    {
                        oOutputCSV[i] += dgExport.Rows[i - 1].Cells[j].Value.ToString() + ",";
                    }
                }

                File.WriteAllLines(oSaveFileDialog.FileName, oOutputCSV, Encoding.UTF8);
                oFrmMsgBox = new Forms.frmMessageBox("RECORD SUCESSFULLY EXRACTED TO : " + " :" + oSaveFileDialog.FileName);
                oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();
            }
            catch (Exception ex)
            { }
        }

            

        private void btnBrowseI_Click(object sender, EventArgs e)
        {

            try
            {

                if (cboImport.SelectedText == "STUDENT" || cboImport.Text == "STUDENT")
                {
                    OpenFileDialog oDiaglog = new OpenFileDialog
                    {
                        InitialDirectory = @"D:\",
                        Title = "Browse CSV Files",

                        CheckFileExists = true,
                        CheckPathExists = true,

                        DefaultExt = "csv",
                        Filter = "CSV files (*.CSV)|*.CSV",
                        FilterIndex = 2,
                        RestoreDirectory = true,

                        ReadOnlyChecked = true,
                        ShowReadOnly = true
                    };

                    if (oDiaglog.ShowDialog() == DialogResult.OK)
                    {
                        txtImportPath.Text = oDiaglog.FileName;

                        oStudent = new DataAccess.Student();
                        lstStudent = oStudent.RetrieveCSVData(txtImportPath.Text);
                        dgImport.DataSource = lstStudent;
                    }
                }

                if (cboImport.SelectedText == "CANDIDATE" || cboImport.Text == "CANDIDATE")
                {
                    OpenFileDialog oDiaglog = new OpenFileDialog
                    {
                        InitialDirectory = @"D:\",
                        Title = "Browse CSV Files",

                        CheckFileExists = true,
                        CheckPathExists = true,

                        DefaultExt = "csv",
                        Filter = "CSV files (*.CSV)|*.CSV",
                        FilterIndex = 2,
                        RestoreDirectory = true,

                        ReadOnlyChecked = true,
                        ShowReadOnly = true
                    };

                }

                if (txtImportPath.Text.Trim() != String.Empty)
                {
                    foreach (DataGridViewColumn col in dgImport.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                    for (int i = 0; i < dgImport.RowCount; i++)
                    {
                        for (int j = 0; j < dgImport.ColumnCount; j++)
                        {
                            if (dgImport.Rows[i].Cells[j].Value.ToString() == string.Empty)
                            {
                                dgImport.Columns[j].Visible = false;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
   
            }
        }

        private void btnBrowseE_Click(object sender, EventArgs e)
        {
            try
            {
                oSaveFileDialog = new SaveFileDialog();
                oSaveFileDialog.Filter = "CSV (*.csv) | *.csv";
                oSaveFileDialog.FileName = "DataResult.csv";

                if (oSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtExportPath.Text = oSaveFileDialog.FileName;
                }
            }
            catch (Exception ex)
            { }
        }

        private void btnPopulate_Click(object sender, EventArgs e)
        {
            if (cboExport.Text == "STUDENT")
            {
                LoadStudent();
            }
        }

        void StudentStructure()
        {
            dgExport.Columns.Clear();
            dgExport.Columns.Add("", "STUDENT ID");
            dgExport.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgExport.Columns.Add("", "FIRST NAME");
            dgExport.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgExport.Columns.Add("", "MIDDLE NAME");
            dgExport.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgExport.Columns.Add("", "LAST NAME");
            dgExport.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgExport.Columns.Add("", "DOB");
            dgExport.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgExport.Columns.Add("", "AGE");
            dgExport.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgExport.Columns.Add("", "COURSE NO");
            dgExport.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgExport.Columns.Add("", "SECTION");
            dgExport.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgExport.Columns.Add("", "ADDRESS");
            dgExport.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgExport.Columns.Add("", "CONTACT NO");
            dgExport.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
          
        }

        public void LoadStudent()
        {
            try
            {
                
                dgExport.Rows.Clear();
                StudentStructure();
                oStudent = new DataAccess.Student();                
                foreach (DataRow row in oStudent.getStudent("", "").Rows)
                {
                    dgExport.Rows.Add(row["STUDENT_ID"], row["FIRST_NAME"], row["MIDDLE_NAME"], row["LAST_NAME"], row["DOB"], row["AGE"], row["COURSE"], row["SECTION"], row["ADDRESS"], row["CONTACT_NO"]);
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void frmImportExport_Load(object sender, EventArgs e)
        {
            btnPopulate.Visible = false;
        }

                     
       
    }
}
