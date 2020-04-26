using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Forms;
using ePublicVariable;


namespace DataAccess
{
    public class SystemFunction
    {

        public string sConnectionString = eVariable.sGlobalConnectionString;
        public string sMasterDBConnection = eVariable.sGlobalMasterConnectionString;
        public OleDbConnectionStringBuilder osb = new OleDbConnectionStringBuilder();
        DatabaseQuery.DBQuery ddq = new DatabaseQuery.DBQuery();
        DataSet ds = new DataSet();

        #region Database Function

        public void BackupDatabase(String sPath)
        {
            try
            {
                osb.ConnectionString = sMasterDBConnection;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "BACKUP DATABASE [iVotingSystem] to disk ='" + sPath + @"\\" + "iVotingSystem_" + DateTime.Now.ToString("yyyy-MM-dd") + ".BAK" + "' WITH INIT";

                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RestoreDatabase(String sPath)
        {
            try
            {
                osb.ConnectionString = sMasterDBConnection;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "USE MASTER RESTORE Database iVotingSystem FROM DISK ='" + sPath + "' WITH REPLACE;";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean IsDatabaseExits()
        {
            try
            {
                osb.ConnectionString = sMasterDBConnection;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "SELECT name FROM master.sys.databases WHERE name = 'iVotingSystem'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void DropDatabase()
        {
            try
            {
                osb.ConnectionString = sMasterDBConnection;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "DROP DATABASE iVotingSystem";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Election Code

        public DataTable getElectionCode(string sType, string sValue)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                string sSQL = string.Empty;
                switch (sType)
                {
                    case "STUDENT ID":
                        sSQL = "Select * from tbl_ElectionCode where status = 'ACTIVE' AND STUDENT_ID LIKE '%" + sValue + "%' order by date_added desc";
                        break;
                    case "INACTIVE":
                        sSQL = "Select * from tbl_ElectionCode where status = 'INACTIVE' order by date_added desc";
                        break;
                    default:
                        sSQL = "Select * from tbl_ElectionCode where status = 'ACTIVE' order by date_added desc";
                        break;
                }

                ddq.CommandText = sSQL;
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public Boolean IsElectionCodeActive()
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Select * from tbl_ElectionCode where Status = 'ACTIVE' order by status asc";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public string CreateElectionCode(string sKey)
        {

            return "";
        }

        public void UpdateElectionCode()
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Update tbl_ElectionCode set STATUS = 'INACTIVE' where STATUS = 'ACTIVE'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertElectionCode(Model.ElectionCode oData)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Insert Into tbl_ElectionCode (CODE,DATE_ADDED,ADDED_BY) Values ('" + oData.CODE + "','" + oData.ADDED_DATE + "','" + oData.ADDED_BY + "')";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region VotersKey
        Model.GenerateKey oMGeneratedKey;
        public DataTable getGeneratedKey(String sType, string sValue)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                string sQuery;

                switch (sType)
                {
                    case "STUDENT ID":
                        sQuery = "Select * from tbl_VotersKey where voters_id LIKE '%" + sValue + "%' and status = 'ACTIVE'";
                        break;
                    case "INACTIVE":
                        sQuery = "Select * from tbl_VotersKey where voters_id = '" + sValue + "' and status = 'ACTIVE'";
                        break;
                    default:
                        sQuery = "Select * from tbl_VotersKey where Status = 'Active'";
                        break;

                }

                ddq.CommandText = sQuery;
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void InsertKey(Model.GenerateKey oData)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;
                ddq.CommandText = "Insert Into tbl_VotersKey (VOTERS_ID,GENERATED_KEY,DATE_ADDED,ELECTION_CODE) Values ('" + oData.VOTERS_ID + "','" + oData.GENERATED_KEY + "','" + oData.DATED_ADDED + "','" + oData.ELECTION_CODE + "')";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateGeneratedKey(string sElectionCode)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;
                ddq.CommandText = "Update tbl_VotersKey set Status = 'INACTIVE' Where ELECTION_CODE = '" + sElectionCode + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean IsKeyExists(String sVotersID)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Select * from tbl_VotersKey Where Voters_ID = '" + sVotersID + "' and Status = 'Active'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Schedule

        public bool IsScheduleOpen()
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Select * from tbl_VotingSchedule where status = 'OPEN'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void InsertSchedule(Model.VotingSchedule oData)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Insert Into tbl_VotingSchedule (DATE_START,DATE_END,TIME_START,TIME_END) Values ('" + oData.DATE_START + "','" + oData.DATE_END + "','" + oData.TIME_START + "','" + oData.TIME_END + "')";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateScheduleStatus()
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Update tbl_VotingSchedule set Status = 'CLOSED'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void UpdateSchedule(Model.VotingSchedule oData)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Update tbl_VotingSchedule set DATE_START = '" + oData.DATE_START + "',DATE_END = '" + oData.DATE_END + "', TIME_START  = '" + oData.TIME_START + "',TIME_END = '" + oData.TIME_END + "',STATUS =  '" + oData.STATUS + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public Boolean IsScheduleExists()
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Select * from tbl_VotingSchedule";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public DataTable GetVotingSchedule()
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Select * from tbl_VotingSchedule";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        #endregion

      

    }
}
