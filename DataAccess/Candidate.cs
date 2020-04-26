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
    public class Candidate
    {
        public string sConnectionString = eVariable.sGlobalConnectionString;
        public OleDbConnectionStringBuilder osb = new OleDbConnectionStringBuilder();
        DatabaseQuery.DBQuery ddq = new DatabaseQuery.DBQuery();
        DataSet ds = new DataSet();

        public DataTable getCandidates(String sType, string sValue)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                string sQuery;

                switch (sType)
                {
                    case "ID":
                        sQuery = "SELECT B.CANDIDATE_ID,A.FIRST_NAME,A.MIDDLE_NAME,A.LAST_NAME,A.DOB,A.AGE,A.COURSE,A.SECTION,A.CONTACT_NO,A.ADDRESS,C.ID AS POSITION_ID,C.POSITION,D.ID AS PARTY_ID,D.PARTY,B.PROFILE_PIC,B.ELECTION_CODE FROM TBL_STUDENTS A INNER JOIN TBL_CANDIDATES B ON A.STUDENT_ID = B.CANDIDATE_ID INNER JOIN TBL_POSITION C ON C.ID = B.POSITION_ID INNER JOIN TBL_PARTY D ON D.ID = B.PARTY_ID WHERE B.STATUS = 'ACTIVE' AND A.STUDENT_ID = '" + sValue + "'";
                        break;
                    case "STUDENT ID":
                        sQuery = "SELECT B.CANDIDATE_ID,A.FIRST_NAME,A.MIDDLE_NAME,A.LAST_NAME,A.DOB,A.AGE,A.COURSE,A.SECTION,A.CONTACT_NO,A.ADDRESS,C.ID AS POSITION_ID,C.POSITION,D.ID AS PARTY_ID,D.PARTY,B.PROFILE_PIC,B.ELECTION_CODE FROM TBL_STUDENTS A INNER JOIN TBL_CANDIDATES B ON A.STUDENT_ID = B.CANDIDATE_ID INNER JOIN TBL_POSITION C ON C.ID = B.POSITION_ID INNER JOIN TBL_PARTY D ON D.ID = B.PARTY_ID WHERE B.STATUS = 'ACTIVE' AND A.STUDENT_ID LIKE '%" + sValue + "%'";
                        break;
                    case "CANDIDATE ID":
                        sQuery = "SELECT B.CANDIDATE_ID,A.FIRST_NAME,A.MIDDLE_NAME,A.LAST_NAME,A.DOB,A.AGE,A.COURSE,A.SECTION,A.CONTACT_NO,A.ADDRESS,C.ID AS POSITION_ID,C.POSITION,D.ID AS PARTY_ID,D.PARTY,B.PROFILE_PIC,B.ELECTION_CODE FROM TBL_STUDENTS A INNER JOIN TBL_CANDIDATES B ON A.STUDENT_ID = B.CANDIDATE_ID INNER JOIN TBL_POSITION C ON C.ID = B.POSITION_ID INNER JOIN TBL_PARTY D ON D.ID = B.PARTY_ID WHERE B.STATUS = 'ACTIVE' AND A.STUDENT_ID LIKE '%" + sValue + "%'";
                        break;
                    case "FIRST NAME":
                        sQuery = "SELECT B.CANDIDATE_ID,A.FIRST_NAME,A.MIDDLE_NAME,A.LAST_NAME,A.DOB,A.AGE,A.COURSE,A.SECTION,A.CONTACT_NO,A.ADDRESS,C.ID AS POSITION_ID,C.POSITION,D.ID AS PARTY_ID,D.PARTY,B.PROFILE_PIC,B.ELECTION_CODE FROM TBL_STUDENTS A INNER JOIN TBL_CANDIDATES B ON A.STUDENT_ID = B.CANDIDATE_ID INNER JOIN TBL_POSITION C ON C.ID = B.POSITION_ID INNER JOIN TBL_PARTY D ON D.ID = B.PARTY_ID WHERE B.STATUS = 'ACTIVE' AND A.FIRST_NAME LIKE '%" + sValue + "%'";
                        break;
                    case "LAST NAME":
                        sQuery = "SELECT B.CANDIDATE_ID,A.FIRST_NAME,A.MIDDLE_NAME,A.LAST_NAME,A.DOB,A.AGE,A.COURSE,A.SECTION,A.CONTACT_NO,A.ADDRESS,C.ID AS POSITION_ID,C.POSITION,D.ID AS PARTY_ID,D.PARTY,B.PROFILE_PIC,B.ELECTION_CODE FROM TBL_STUDENTS A INNER JOIN TBL_CANDIDATES B ON A.STUDENT_ID = B.CANDIDATE_ID INNER JOIN TBL_POSITION C ON C.ID = B.POSITION_ID INNER JOIN TBL_PARTY D ON D.ID = B.PARTY_ID WHERE B.STATUS = 'ACTIVE' AND A.LAST_NAME LIKE '%" + sValue + "%'";
                        break;
                    case "INACTIVE":
                        sQuery = "SELECT B.CANDIDATE_ID,A.FIRST_NAME,A.MIDDLE_NAME,A.LAST_NAME,A.DOB,A.AGE,A.COURSE,A.SECTION,A.CONTACT_NO,A.ADDRESS,C.ID AS POSITION_ID,C.POSITION,D.ID AS PARTY_ID,D.PARTY,B.PROFILE_PIC,B.ELECTION_CODE FROM TBL_STUDENTS A INNER JOIN TBL_CANDIDATES B ON A.STUDENT_ID = B.CANDIDATE_ID INNER JOIN TBL_POSITION C ON C.ID = B.POSITION_ID INNER JOIN TBL_PARTY D ON D.ID = B.PARTY_ID WHERE B.STATUS = 'INACTIVE' AND A.STUDENT_ID LIKE '%" + sValue + "%'";
                        break;
                    default:
                        sQuery = "SELECT B.CANDIDATE_ID,A.FIRST_NAME,A.MIDDLE_NAME,A.LAST_NAME,A.DOB,A.AGE,A.COURSE,A.SECTION,A.CONTACT_NO,A.ADDRESS,C.ID AS POSITION_ID,C.POSITION,D.ID AS PARTY_ID,D.PARTY,B.PROFILE_PIC,B.ELECTION_CODE FROM TBL_STUDENTS A INNER JOIN TBL_CANDIDATES B ON A.STUDENT_ID = B.CANDIDATE_ID INNER JOIN TBL_POSITION C ON C.ID = B.POSITION_ID INNER JOIN TBL_PARTY D ON D.ID = B.PARTY_ID WHERE B.STATUS = 'ACTIVE' AND A.FIRST_NAME LIKE '%" + sValue + "%'";
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

        public DataTable getCandidatesByPosition(string sPosition)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Select b.CANDIDATE_ID,a.First_Name,a.Middle_Name,a.Last_Name,a.DOB,a.Age,a.Course,a.Section,a.Contact_No,a.Address,c.ID as Position_ID,c.Position,d.ID as Party_ID,d.Party,b.ELECTION_CODE from tbl_students a inner join tbl_candidates b on a.STUDENT_ID = b.CANDIDATE_ID inner join tbl_Position c on c.ID = b.POSITION_ID inner join tbl_Party d on d.ID = b.PARTY_ID where a.Status = 'ACTIVE' and b.Position_ID = '" + sPosition + "'"; 

                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public Boolean IsCandidatIeExists(Model.Candidate oData)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "SELECT * FROM TBL_CANDIDATES WHERE CANDIDATE_ID = '" + oData.UNIQUE_ID + "'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public Boolean IsSamePositionAndParty(Model.Candidate oData)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "SELECT * FROM TBL_CANDIDATES WHERE POSITION_ID = '" + oData._Position.ID + "' AND PARTY_ID = '" + oData._Party.ID + "' AND STATUS = 'ACTIVE' AND ELECTION_CODE = '" + oData.ELECTION_CODE + "'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public Boolean IsCandidateNotEmpty()
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "SELECT * FROM TBL_CANDIDATES WHERE STATUS = 'ACTIVE'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }



        public void ARCHIVE_CANDIDATE(Model.Student oData)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "UDPATE TBL_CANDIDATES SET  [STATUS] = 'INACTIVE' WHERE CANDIDATE_ID = '" + oData.UNIQUE_ID + "' AND ELECTION_CODE = '" + oData.ELECTION_CODE + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCandidateStatus(string sElectionCode)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Update tbl_Candidates set Status = 'INACTIVE' where ELECTION_CODE = '" + sElectionCode + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void InsertCandidate(Model.Candidate oData)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Insert Into tbl_candidates (CANDIDATE_ID,PROFILE_PIC,POSITION_ID,PARTY_ID,ELECTION_CODE,ADDED_DATE) Values ('" + oData.UNIQUE_ID + "','" + oData.PROFILE_PIC + "','" + oData._Position.ID + "','" + oData._Party.ID + "','" + oData.ELECTION_CODE + "','" + oData.DATE_ADDED + "')";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCandidate(Model.Candidate oData)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Update tbl_Candidates set POSITION_ID = '" + oData._Position.ID + "',PARTY_ID = '" + oData._Party.ID + "',PROFILE_PIC = '" + oData.PROFILE_PIC + "',ELECTION_CODE = '" + oData.ELECTION_CODE + "' where CANDIDATE_ID =  '" + oData.UNIQUE_ID + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }




    }
}
