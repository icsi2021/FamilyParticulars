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

public partial class ReportDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Convert.ToString(Session["UserName"]).ToLower() != "finance") && Convert.ToString(Session["UserName"]).ToLower() != "admin")
            {
                txtnon.Text = Convert.ToString(Session["UserName"]);
                txtnon.Enabled = false;
                ddlreport.Items.Add("EMPCODE");               
                ddlreport.Items.Add("FORM NAME");
                ddlreport.Items.Add("ALL FORM REPORT"); 
            }
            else
            {
                ddlreport.Items.Add("EMPCODE");
                ddlreport.Items.Add("ALL EMPLOYEE REPORT");
                ddlreport.Items.Add("ALL FORM REPORT");                
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if ((ddlreport.SelectedItem.Text == "EMPCODE" || ddlreport.SelectedItem.Text == "FORM NAME") && txtnon.Text != "")
        {

        }
        else if ((ddlreport.SelectedItem.Text != "EMPCODE" || ddlreport.SelectedItem.Text != "FORM NAME"))
        {

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage1", "alertMessage1();", true);
            return;
        }
    }
}
