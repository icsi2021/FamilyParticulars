using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Net.Mail;

public partial class Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        string IsEdit = string.Empty;
        string EmpCode = string.Empty;
        int k = 0;
        try
        {
            if (ddlemptype.SelectedItem.Text == "Single" && txtempcode.Text != "")
            {
                if (ddledit.SelectedItem.Text == "Yes")
                {
                    IsEdit = "1";
                }
                else
                {
                    IsEdit = "0";
                }
                EmpCode = txtempcode.Text;               
                cmd.Connection = con;
                cmd.CommandText = "AllEditable";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IsEdit", SqlDbType.NVarChar).Value = IsEdit;
                cmd.Parameters.Add("@EmpCode", SqlDbType.NVarChar).Value = EmpCode;
                con.Open();
                k = cmd.ExecuteNonQuery();
                con.Close();
            }
            else if (ddlemptype.SelectedItem.Text == "All")
            {
                if (ddledit.SelectedItem.Text == "Yes")
                {
                    IsEdit = "1";
                }
                else
                {
                    IsEdit = "0";
                }
                EmpCode = "All";               
                cmd.Connection = con;
                cmd.CommandText = "AllEditable";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IsEdit", SqlDbType.NVarChar).Value = IsEdit;
                cmd.Parameters.Add("@EmpCode", SqlDbType.NVarChar).Value = EmpCode;
                con.Open();
                 k = cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert", "ErrorAlert();", true);
                return;
            }
           
            if (k > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "Alertmessage();", true);
            }           
        }
        catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "Notification();", true); }
    }
}
