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
    
    public partial class frmImportExport : Form
    {
        
        DataAccess.Student oStudent;
        DataAccess.Candidate oCandidate;        


        List<Model.Student> lstStudent;
        List<Model.Candidate> lstCandidate;

        String sTitle = "Voting Management System";

        Forms.frmMessageBox oFrmMsgBox;
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
            }
            else
            {
                btnButton.Text = "EXPORT";
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
                    MessageBox.Show("Please select file destination.");
                    return;
                }

                #region Export

                #endregion

            }
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

                     
       
    }
}
