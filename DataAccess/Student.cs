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
    public class Student
    {
        public string sConnectionString = eVariable.sGlobalConnectionString;
        public OleDbConnectionStringBuilder osb = new OleDbConnectionStringBuilder();


        DatabaseQuery.DBQuery ddq = new DatabaseQuery.DBQuery();
        DataSet ds = new DataSet();
        Model.Student mStudent;

        public DataTable getStudent(String sType, string sValue)
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
                        sQuery = "SELECT * FROM TBL_STUDENTS WHERE STATUS = 'ACTIVE' AND STUDENT_ID LIKE '%" + sValue + "%'";
                        break;
                    case "FIRST NAME":
                        sQuery = "SELECT * FROM TBL_STUDENTS WHERE STATUS = 'ACTIVE' AND FIRST_NAME LIKE '%" + sValue + "%'";
                        break;
                    case "LAST NAME":
                        sQuery = "SELECT * FROM TBL_STUDENTS WHERE STATUS = 'ACTIVE' AND LAST_NAME LIKE '%" + sValue + "%'";
                        break;
                    case "INACTIVE":
                        sQuery = "SELECT * FROM TBL_STUDENTS WHERE STATUS = 'INACTIVE'";
                        break;
                    default:
                        sQuery = "SELECT * FROM TBL_STUDENTS WHERE STATUS = 'ACTIVE'";
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


        public DataTable getCandidate(String sType, string sValue)
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
                        sQuery = "SELECT * FROM TBL_STUDENTS WHERE STUDENT_ID Like '%" + sValue + "%' and status = 'ACTIVE'";
                        break;
                    case "FIRST NAME":
                        sQuery = "SELECT * FROM TBL_STUDENTS WHERE FIRST_NAME Like '%" + sValue + "%' and status = 'ACTIVE'";
                        break;
                    case "MIDDLE NAME":
                        sQuery = "SELECT * FROM TBL_STUDENTS WHERE MIDDLE_NAME Like '%" + sValue + "%' and status = 'ACTIVE'";
                        break;
                    case "LAST NAME":
                        sQuery = "SELECT * FROM TBL_STUDENTS WHERE LAST_NAME Like '%" + sValue + "%' and status = 'ACTIVE'";
                        break;
                    case "INACTIVE":
                        sQuery = "Select * from tbl_Students where status = 'INACTIVE'";
                        break;
                    default:
                        sQuery = "Select * from tbl_Students where status = 'ACTIVE' and Student_ID not in (Select Candidate_ID from tbl_Candidates where status = 'ACTIVE')";
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

        public void DeleteStudents(string sID)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Update tbl_Students set Status = 'INACTIVE' where Student_ID = '" + sID + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void InsertStudent(Model.Student oData)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Insert Into tbl_Students (Student_ID,First_Name,Middle_Name,Last_Name,DOB,Age,Course,Section,Address,Contact_No,Date_Added) Values ('" + oData.UNIQUE_ID + "','" + oData.FIRST_NAME + "','" + oData.MIDDLE_NAME + "','" + oData.LAST_NAME + "','" + oData.DOB + "','" + oData.AGE + "','" + oData.COURSE + "','" + oData.SECTION + "','" + oData.ADDRESS + "','" + oData.CONTACT_NO + "','" + DateTime.Now.ToShortDateString() + "')";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Boolean IsRecordExists(Model.Student oData)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Select Student_ID from tbl_Students where Student_ID = '" + oData.UNIQUE_ID + "'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void UpdateStudent(Model.Student oData)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Update tbl_Students set First_Name = '" + oData.FIRST_NAME + "',Middle_Name = '" + oData.MIDDLE_NAME + "',Last_Name = '" + oData.LAST_NAME + "',DOB = '" + oData.DOB + "',AGE  = '" + oData.AGE + "',COURSE = '" + oData.COURSE + "',SECTION = '" + oData.SECTION + "',ADDRESS = '" + oData.ADDRESS + "',Contact_No = '" + oData.CONTACT_NO + "',Modified_Date = '" + DateTime.Now.ToShortDateString() + "' where STUDENT_ID =  '" + oData.UNIQUE_ID + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public List<Model.Student> RetrieveCSVData(string sPath)
        {

            try
            {

                List<string> queries = new List<string>(File.ReadAllLines(sPath));
                List<Model.Student> lstStudent = new List<Model.Student>();

                foreach (string query in queries.Where(fw => !fw.ToString().Contains("_")))
                {
                    string[] queryArr = query.Split(',');
                    mStudent = new Model.Student();
                    mStudent.UNIQUE_ID = queryArr[0];
                    mStudent.FIRST_NAME = queryArr[1];
                    mStudent.MIDDLE_NAME = queryArr[2];
                    mStudent.LAST_NAME = queryArr[3];
                    mStudent.DOB = (queryArr[4]);
                    mStudent.AGE = queryArr[5];
                    mStudent.COURSE = queryArr[6];
                    mStudent.SECTION = queryArr[7];
                    mStudent.ADDRESS = queryArr[8];
                    mStudent.CONTACT_NO = queryArr[9];
                    mStudent.DATE_ADDED = queryArr[10];
                    mStudent.MODIFIED_DATE = queryArr[11];

                    lstStudent.Add(mStudent);

                }

                return lstStudent;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
