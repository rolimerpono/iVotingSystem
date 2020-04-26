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
    public class Party
    {

        public string sConnectionString = eVariable.sGlobalConnectionString;
        public OleDbConnectionStringBuilder osb = new OleDbConnectionStringBuilder();


        DatabaseQuery.DBQuery ddq = new DatabaseQuery.DBQuery();
        DataSet ds = new DataSet();


        public DataTable getParty(string sType, string sValue)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                string sQuery = string.Empty;

                switch (sType)
                {
                    case "ID":
                        sQuery = "Select * from tbl_Party where ID Like '%" + sValue + "%' and status = 'ACTIVE'";
                        break;
                    case "PARTY":
                        sQuery = "Select * from tbl_Party where Party Like '%" + sValue + "%' and status = 'ACTIVE'";
                        break;
                    case "INACTIVE":
                        sQuery = "Select * from tbl_Party where Party Like '%" + sValue + "%' and status = 'INACTIVE'";
                        break;
                    default:
                        sQuery = "Select * from tbl_Party where Party Like '%" + sValue + "%' and status = 'ACTIVE'";
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

        public Boolean IsPartyExists(string sValue)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Select * from tbl_Party where Party = '" + sValue + "'";

                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void InsertParty(Model.Party oData)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Insert Into tbl_Party (Party,Status) Values ('" + oData.PARTY + "','" + oData.STATUS + "')";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateParty(Model.Party oData)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Update tbl_Party set Status = '" + oData.STATUS + "', Party = '" + oData.PARTY + "' Where ID  = '" + oData.ID + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
