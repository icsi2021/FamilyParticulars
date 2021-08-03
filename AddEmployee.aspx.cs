using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
public partial class AddEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])) || Session["UserName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
    /// <summary>
    /// Save Employee  Details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtEMPCode.Text != "" && txtemaild.Text != "" && txtdepartment.Text != "" && txtdesignation.Text != "" && txtDocDate.Text != "")
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "InsertUpdateIntoEmployeeUserTable";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = txtEMPCode.Text;
                cmd.Parameters.Add("@EMPLOYEE_NAME", SqlDbType.NVarChar).Value = txtName.Text;
                cmd.Parameters.Add("@DEPARTMENT", SqlDbType.NVarChar).Value = txtdepartment.Text;
                cmd.Parameters.Add("@DESIGNATION", SqlDbType.NVarChar).Value = txtdesignation.Text;
                if (txtDocDate.Text != "")
                {
                    cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = txtDocDate.Text;
                }
                else if (txtDocDate.Text != "")
                {
                    cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = txtDocDate.Text;
                }
                else
                {
                    cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = System.DateTime.Now.ToShortDateString();
                }
                cmd.Parameters.Add("@FN_YR", SqlDbType.NVarChar).Value = txtfinancialyear.Text;
                cmd.Parameters.Add("@EMPLOYEE_STATUS", SqlDbType.NVarChar).Value = ddlEmploymentstatus.SelectedItem.Text;

                cmd.Parameters.Add("@BLOODGROUP", SqlDbType.NVarChar).Value = txtBlood.Text;
                cmd.Parameters.Add("@MOBILE", SqlDbType.NVarChar).Value = TxtMoble.Text;

                cmd.Parameters.Add("@REAGON_FOR_END_EMP", SqlDbType.NVarChar).Value = ddlreasonforendofemployment.Text;
                cmd.Parameters.Add("@MARITAL_STATUS", SqlDbType.NVarChar).Value = ddlmaritalstatus.SelectedItem.Text;
                cmd.Parameters.Add("@SEX", SqlDbType.NVarChar).Value = ddlgender.SelectedItem.Text;
                cmd.Parameters.Add("@P_ADDRESS", SqlDbType.NVarChar).Value = txtpaddress.Text;
                cmd.Parameters.Add("@C_ADDRESS", SqlDbType.NVarChar).Value = txtcaddress.Text;
                cmd.Parameters.Add("@REGION", SqlDbType.NVarChar).Value = txtreligion.Text;
                cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@emailid", SqlDbType.NVarChar).Value = txtemaild.Text;
                cmd.Parameters.Add("@Comments", SqlDbType.NVarChar).Value = txtcomments.Text;
                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage1", "alertMessage1();", true);
                    Refresh();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //lblError.Text = ex.Message.ToString();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage1", "Message();", true);
        }
    }
    /// <summary>
    /// Close the Add Employee Page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtEMPCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetEmployeefromEmployeeTableByEmpID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = txtEMPCode.Text;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtEMPCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMP_CODE"]);
                txtName.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                txtdepartment.Text = Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT"]);
                txtdesignation.Text = Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION"]);
                txtfinancialyear.Text = Convert.ToString(ds.Tables[0].Rows[0]["FN_YR"]);
                txtDocDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["DOB"]);
                txtpaddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["P_ADDRESS"]);
                txtcaddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["C_ADDRESS"]);
                txtreligion.Text = Convert.ToString(ds.Tables[0].Rows[0]["REGION"]);
                ddlmaritalstatus.ClearSelection();
                ddlgender.ClearSelection();
                ddlEmploymentstatus.ClearSelection();
                ddlgender.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[0]["SEX"])).Selected = true;
                ddlmaritalstatus.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[0]["MARITAL_STATUS"])).Selected = true;
                ddlEmploymentstatus.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_STATUS"])).Selected = true;
                ddlreasonforendofemployment.Text = Convert.ToString(ds.Tables[0].Rows[0]["REAGON_FOR_END_EMP"]);
                txtcomments.Text = Convert.ToString(ds.Tables[0].Rows[0]["comments"]);
                txtemaild.Text = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "alertmessage();", true);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void Refresh()
    {
        txtName.Text = string.Empty;
        txtEMPCode.Text = string.Empty;
        txtpaddress.Text = string.Empty;
        txtreligion.Text = string.Empty;
        txtfinancialyear.Text = string.Empty;
        txtemaild.Text = string.Empty;
        txtpaddress.Text = string.Empty;
        txtBlood.Text = string.Empty;
        TxtMoble.Text = string.Empty;
    }
}
