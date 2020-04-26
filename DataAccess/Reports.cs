using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.IO;
using ePublicVariable;

namespace DataAccess
{
    public class Reports
    {

        public string sConnectionString = eVariable.sGlobalConnectionString;
        public OleDbConnectionStringBuilder osb = new OleDbConnectionStringBuilder();
        DatabaseQuery.DBQuery ddq = new DatabaseQuery.DBQuery();
        DataSet ds = new DataSet();

        public DataTable GetStudents(DateTime dDateFrom, DateTime dDateTo)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "SELECT STUDENT_ID,(FIRST_NAME + ' ' + MIDDLE_NAME + ' ' + LAST_NAME) AS FULLNAME, DOB, AGE, COURSE,SECTION,CONTACT_NO,ADDRESS FROM TBL_STUDENTS WHERE DATE_ADDED BETWEEN '" + dDateFrom.ToString("yyyy-MM-dd") + "' AND '" + dDateTo.ToString("yyyy-MM-dd") + "'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public DataTable getCandidates(DateTime dDateFrom, DateTime dDateTo , string sElectionCode)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Select b.CANDIDATE_ID, (a.FIRST_NAME + ' ' + a.MIDDLE_NAME + ' ' + a.LAST_NAME)  AS FULLNAME,a.DOB,a.AGE,a.COURSE,a.SECTION,a.CONTACT_NO,a.ADDRESS,c.ID as POSITION_ID,c.POSITION,d.ID as PARTY_ID,d.PARTY,b.PROFILE_PIC from tbl_students a inner join tbl_candidates b on a.STUDENT_ID = b.CANDIDATE_ID inner join tbl_Position c on c.ID = b.POSITION_ID inner join tbl_Party d on d.ID = b.PARTY_ID where a.Status = 'ACTIVE' and b.Added_Date between '" + dDateFrom.ToString("yyyy-MM-dd") + "' AND '" + dDateTo.ToString("yyyy-MM-dd") + "' AND ELECTION_CODE ='" + sElectionCode + "'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public DataTable getVoteResult(DateTime dStart, DateTime dEnd, string sElectionCode)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;



                ddq.CommandText = " SELECT CANDIDATE_ID, (FIRST_NAME + ' ' + MIDDLE_NAME + ' ' + LAST_NAME) AS FULLNAME, POSITION, PARTY, COUNT(*) VOTE_COUNT,COURSE, ELECTION_CODE " +
                                  " FROM TBL_RESULT WHERE DATE_ADDED BETWEEN '" + dStart.ToString("yyyy-MM-dd") + "' AND '" + dEnd.ToString("yyyy-MM-dd") + "' AND ELECTION_CODE = '" + sElectionCode + "' " +
                                  " GROUP BY CANDIDATE_ID, FIRST_NAME,MIDDLE_NAME, LAST_NAME, POSITION, PARTY,COURSE, ELECTION_CODE ";


                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public DataTable getResultWinner(DateTime dStart, DateTime dEnd, string sElectionCode)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.AddParamer("@DATE_FROM", SqlDbType.NVarChar, dStart.ToString("yyyy-MM-dd"));
                ddq.AddParamer("@DATE_TO", SqlDbType.NVarChar, dEnd.ToString("yyyy-MM-dd"));
                ddq.AddParamer("@ELECTION_CODE", SqlDbType.NVarChar, sElectionCode);

                ddq.CommandText = "sp_getresult";

                ds = ddq.GetDataset(CommandType.StoredProcedure);

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


    }
}
