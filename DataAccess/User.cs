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
    public class User
    {

        public string sConnectionString = eVariable.sGlobalConnectionString;
        public OleDbConnectionStringBuilder osb = new OleDbConnectionStringBuilder();
        DatabaseQuery.DBQuery ddq = new DatabaseQuery.DBQuery();
        DataSet ds = new DataSet();

        public DataTable GetUser(String sType, string sValue)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                string sQuery;

                switch (sType)
                {
                    case "FIRST NAME":
                        sQuery = "Select * from tbl_User";
                        break;
                    case "Username":
                        sQuery = "Select * from tbl_User where username = '" + sValue + "'";
                        break;
                    case "INACTIVE":
                        sQuery = "Select * from tbl_User";
                        break;
                    default:
                        sQuery = "Select * from tbl_User";
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

        public Boolean IsRecordExists(String sUsername)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Select * from tbl_User where username = '" + sUsername + "'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }



        public void ArchiveUser(string sUsername)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Update tbl_User set Status = 'INACTIVE' where Username = '" + sUsername + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void InsertUser(Model.User oData)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Insert Into tbl_user (Username,Fullname,Password,Role,Contact_No,Address,Added_By,Date_Added) Values ('" + oData.USERNAME + "','" + oData.FULLNAME + "','" + oData.PASSWORD + "','" + oData.ROLE + "','" + oData.CONTACT_NO + "','" + oData.ADDRESS + "','" + "Rolly" + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUser(Model.User oData)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Update tbl_user set Fullname = '" + oData.FULLNAME + "',Password = '" + oData.PASSWORD + "',Role = '" + oData.ROLE + "',Contact_No = '" + oData.CONTACT_NO + "',Address = '" + oData.ADDRESS + "',Added_By = '" + "Rolly" + "' where Username = '" + oData.USERNAME + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean IsLogin(String sUsername, string sPassword)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Select * from tbl_user where Username = '" + sUsername + "' and Password = '" + sPassword + "'";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean IsAccountEmpty()
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;


                ddq.CommandText = "Select * from tbl_user";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count <= 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

    }
}
