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
    public class Position
    {

        public string sConnectionString = eVariable.sGlobalConnectionString;
        public OleDbConnectionStringBuilder osb = new OleDbConnectionStringBuilder();


        DatabaseQuery.DBQuery ddq = new DatabaseQuery.DBQuery();
        DataSet ds = new DataSet();

        Model.Position oMPosition;
        public DataTable getPosition(string sType, string sValue)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                string sQuery = String.Empty;

                switch (sType)
                {
                    case "ID":
                        sQuery = "Select * from tbl_Position where ID LIKE '%" + sValue + "%' and status = 'ACTIVE'";
                        break;
                    case "POSITION":
                        sQuery = "Select * from tbl_Position where Position LIKE '%" + sValue + "5' and status = 'ACTIVE'";
                        break;
                    case "INACTIVE":
                        sQuery = "Select * from tbl_Position where Position Like '%" + sValue + "%' and status = 'INACTIVE'";
                        break;
                    default:
                        sQuery = "Select * from tbl_Position where Position Like '%" + sValue + "%' and status = 'ACTIVE'";
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

        public DataTable getPositionCount(string sType, string sValue)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;
                ddq.CommandText = "Select distinct Count(Position) from tbl_Position";
                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void InsertPosition(Model.Position oData)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Insert Into tbl_Position (Position,Status) Values ('" + oData.POSITION + "', '" + oData.STATUS + "')";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean IsPositionExists(string sValue)
        {
            try
            {
                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Select * from tbl_Position where Position = '" + sValue + "'";

                ds = ddq.GetDataset(CommandType.Text);

                return ds.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void UpdatePosition(Model.Position oData)
        {
            try
            {

                osb.ConnectionString = sConnectionString;
                ddq = new DatabaseQuery.DBQuery();
                ddq.ConnectionString = osb.ConnectionString;

                ddq.CommandText = "Update tbl_Position set Status = '" + oData.STATUS + "', Position = '" + oData.POSITION + "' Where ID  = '" + oData.ID + "'";
                ddq.ExecuteNonQuery(CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

    }
}
