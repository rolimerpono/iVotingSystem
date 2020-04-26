using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ePublicVariable
{
    public static class eVariable
    {

        public static void DisableGridColumnSort(DataGridView oGrid)
        {
            foreach (DataGridViewColumn col in oGrid.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.Frozen = false;
            }
        }

        public static void ClearText(Control oControl)
        {
            foreach (Control o in oControl.Controls.OfType<TextBox>().ToList())
            {
                o.Text = string.Empty;
            }

            foreach (CheckBox o in oControl.Controls.OfType<CheckBox>().ToList())
            {
                o.Checked = false;
            }
        }

        public static void DisablePanelTextKeyPress(Control oControl)
        {
            foreach (Control o in oControl.Controls.OfType<TextBox>().ToList())
            {
                o.KeyPress += TextKeyPress;
            }
        }

        private static void TextKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        public static void DisableTextEnterKey(Control oControl)
        {
            oControl.KeyDown += TextKeyDown;
        }


        public static void DisableTextPanelEnterKey(Control oControl)
        {
            foreach (Control o in oControl.Controls.OfType<TextBox>().ToList())
            {
                if (!o.Name.ToLower().Contains("address"))
                {
                    o.KeyDown += TextKeyDown;
                }
            }
        }



        private static void TextKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        public static bool IsFieldEmpty(Control oControl)
        {
            foreach (var oText in oControl.Controls.OfType<TextBox>().ToList())
            {
                if (oText.Text.Trim() == String.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        public static void DisableKeyPress(Control oControl)
        {
            oControl.KeyPress += NoKeyPress;
        }

        private static void NoKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public static void ValidAmount(Control oControl)
        {
            oControl.KeyPress += ValidAmountKeyPress;
        }

        public static void ValidNumber(Control oControl)
        {
            oControl.KeyPress += ValidNumberKeyPress;
        }

        public static void ValidAmountPanel(Control oControl)
        {
            foreach (Control o in oControl.Controls.OfType<TextBox>().ToList())
            {
                o.KeyPress += ValidAmountKeyPress;
            }

        }

        public static void ValidNumberPanel(Control oControl)
        {
            foreach (Control o in oControl.Controls.OfType<TextBox>().ToList())
            {
                o.KeyPress += ValidNumberKeyPress;
            }

        }

        private static void ValidAmountKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf('.') != -1)
                {
                    e.Handled = true;
                    return;
                }

                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 46)
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private static void ValidNumberKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (((Control)sender).Text == "" && e.KeyChar == '0')
                {
                    e.Handled = true;
                    return;
                }
                if (e.KeyChar < '0' || e.KeyChar > '9')
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        public static int GetAge(DateTime dStart, DateTime dEnd)
        {
            return (dEnd.Year - dStart.Year - 1) + (((dEnd.Month > dStart.Month) || ((dEnd.Month == dStart.Month) && (dEnd.Day >= dStart.Day))) ? 1 : 0);
        }

        public enum MESSAGE : int
        {         
            ALL_FIELDS_ARE_ALL_REQUIRED = 0,
            RECORD_HAS_BEEN_SUCCESSFULLY_SAVE  = 1,
            RECORD_ALREADY_EXISTS = 2,
            YOU_CAN_ONLY_VOTE_ONE_POSITION_PER_CANDIDATE = 3            
        }

        public enum MESSAGE_BOX_TYPE
        { 
            INFO = 0,
            QUERY = 1
        }
        public static MESSAGE_BOX_TYPE m_MessageBoxType;

        public enum TransactionType : int
        { 
            NONE = 0,
            ADD = 1,
            EDIT = 2,
            DELETE = 3
        }

        public static string sID = string.Empty;
        public static string sUsername = string.Empty;
        public static string sPassword = string.Empty;
        public static string sFullname = string.Empty;
        public static string sRole = string.Empty;

        public static string sPartyID = string.Empty;
        public static string sPositionID = string.Empty;
        public static string sGlobalConnectionString = @"Data Source=.\SQLSERVERR2;Initial Catalog=iVotingSystem;Integrated Security=True";
        public static string sGlobalMasterConnectionString = @"Data Source=.\SQLSERVERR2;Initial Catalog=master;Integrated Security=True";

      
    }
}
