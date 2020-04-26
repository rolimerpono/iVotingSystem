using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using ePublicVariable;

namespace iVotingSystem.Maintenance
{
    public partial class frmGenerateKey : Form
    {

        Forms.frmMessageBox oFrmMsgBox;

        DataAccess.Student oStudent;
        DataAccess.SystemFunction oGenerateKey;
        DataAccess.SystemFunction oElection;

        CommonFunction.CommonFunction oKeyGenerator;
        Model.GenerateKey oMGenerateKey;

        Model.Student oMStudent;

        string sElectionCode = String.Empty;

        public frmGenerateKey()
        {
            InitializeComponent();
            eVariable.DisableTextPanelEnterKey(panel1);
        }

        private void frmGenerateKey_Load(object sender, EventArgs e)
        {

            LoadRecords();

        }

        void LoadRecords()
        {
            try
            {
                oGenerateKey = new DataAccess.SystemFunction();
                dgDetails.Rows.Clear();

                foreach (DataRow row in oGenerateKey.getGeneratedKey("STUDENT ID", tbxSearch.Text).Rows)
                {
                    dgDetails.Rows.Add(row["ID"], row["VOTERS_ID"], row["GENERATED_KEY"], row["DATE_ADDED"], "ROLLY", row["ELECTION_CODE"], row["STATUS"]);
                }

                lblTotalRecords.Text = dgDetails.Rows.Count.ToString();
            }

            catch (Exception ex)
            {
               
            }
                
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            oStudent = new DataAccess.Student();

            oElection = new DataAccess.SystemFunction();
            
            if (!oElection.IsElectionCodeActive())
            {
                oFrmMsgBox = new Forms.frmMessageBox("THERE IS NO ACTIVE ELECTION TICKET NUMBER. PLEASE OPEN FIRST TO START. THANK YOU. CLICK : UTILITY ELECTION TICKET NO");
                oFrmMsgBox.MessageType = Forms.frmMessageBox.MESSAGE_TYPE.INFO;
                oFrmMsgBox.ShowDialog();
                return;
            }

            foreach (DataRow row in oElection.getElectionCode("","").Rows)
            {
                sElectionCode = row["CODE"].ToString();
            }

            foreach (DataRow row in oStudent.getStudent("", "").Rows)
            {
                oKeyGenerator = new CommonFunction.CommonFunction();
                oGenerateKey = new DataAccess.SystemFunction();
                oMGenerateKey = new Model.GenerateKey();

                oMGenerateKey.GENERATED_KEY = oKeyGenerator.GetAssortedString(5);
                oMGenerateKey.VOTERS_ID = row["STUDENT_ID"].ToString();
                oMGenerateKey.DATED_ADDED = DateTime.Now.ToString("yyyy-MM-dd");
                oMGenerateKey.ELECTION_CODE = sElectionCode;
                oMGenerateKey.STATUS = "ACTIVE";

                if (!oGenerateKey.IsKeyExists(oMGenerateKey.VOTERS_ID))
                {
                    oGenerateKey.InsertKey(oMGenerateKey);
                }
            }

            LoadRecords();

        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {

        }
    }
}
