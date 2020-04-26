using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using ePublicVariable;

namespace DataAccess
{
    public class VotersLogin
    {

        public string sConnectionString = eVariable.sGlobalConnectionString;
        public OleDbConnectionStringBuilder osb = new OleDbConnectionStringBuilder();
        DatabaseQuery.DBQuery ddq = new DatabaseQuery.DBQuery();
        DataSet ds = new DataSet();
        

        public Boolean IsLogin(String sVotersID, string sPassword)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Select * from tbl_VotersKey where voters_Id = '" + sVotersID + "' and Generated_Key = '" + sPassword + "'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public string GetVotersStatus(String sVotersID, string sPassword)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Select STATUS from tbl_VotersKey where voters_Id = '" + sVotersID + "' and Generated_Key = '" + sPassword + "'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0][0].ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public DataTable GetCandidate(String sCandidateID, string sPassword)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Select * from tbl_Candidates where Candidate_ID = '" + sCandidateID + "'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void VoteCandidate(Model.VotedCandidate oData)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Insert Into tbl_result (CANDIDATE_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,COURSE,POSITION,PARTY,VOTERS_ID,DATE_ADDED,ELECTION_CODE) values ('" + oData.CANDIDATE_ID + "','" + oData.FIRST_NAME + "','" + oData.MIDDLE_NAME + "','" + oData.LAST_NAME + "','" + oData.COURSE + "','" + oData.POSITION + "','" + oData.PARTY + "','" + oData.VOTERS_ID + "','" + oData.DATE_ADDED + "','" + oData.ELECTION_CODE + "')";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        public void UpdateVotersKey(Model.VotedCandidate oData)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Update tbl_VotersKey set status = 'INACTIVE' where Voters_ID =  '" + oData.VOTERS_ID + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }



    }
}
