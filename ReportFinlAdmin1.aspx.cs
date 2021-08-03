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
using System.Drawing;
//using DataBaseLayer;
using System.Data.SqlClient;
using System.Text;
//using DataBaseLayer;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;


public partial class ReportFinlAdmin1 : System.Web.UI.Page  
{
    // public commonFunctions objClass = new commonFunctions();
    DatabaseFunctions dbClass = new DatabaseFunctions();
    StringBuilder _strVal = new StringBuilder();
    StringBuilder sb = new StringBuilder();
    int aa=0;
    int aa1=0;
    string str;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])) || Session["UserName"] == null)
        {
            Response.Redirect("Default.aspx");
        }     
        if (!IsPostBack)
        {
            ensbledata1.Disabled = true;
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Myid"])))
            {
                if (Session["sub1"] == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter User ID.');", true);
                    txtusername.Focus();
                    return;
                }

                if (Convert.ToInt32(Request.QueryString["Myid"].ToString()) > 0)
                {
                    aa = Convert.ToInt32(Request.QueryString["Myid"].ToString());
                    str = aa.ToString();
                    hddstr1.Text = Convert.ToInt32(str.Replace("'", String.Empty)).ToString();
                    // DisplayRecord();
                    if (hddstr1.Text == "1")
                    {
                        if (Session["sub1"] != null)
                        {
                            txtusername.Text = Session["sub1"].ToString();
                            lbltname.Text = "Family Details";
                            DisplayRecord21();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter User ID.');", true);
                            txtusername.Focus();
                            return;
                        }
                    }
                }
            }
        }
    }

    private void DisplayRecord21()
    {
        if (Session["sub1"].ToString() == "")
        {
            //lbl_Error.Text = "Please Enter Frist Name";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter User ID.');", true);
            txtusername.Focus();
        }
        else
        {
            SqlParameter[] pr21 = {
                  new SqlParameter("@aa","1"),   
                  new SqlParameter("@EMP_CODE",Session["sub1"].ToString()),                                 
        };
            DataTable dt = dbClass.getData("Proc_GetAlldata", pr21);
            if (dt.Rows.Count > 0)
            {
                CreateDynamicBox13(dt);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Wrong User ID. Please Enter Correct');", true);
            }
        }
    }
    protected void CreateDynamicBox13(DataTable dt)
    {
        int j = 1;
        for (int idx = 0; idx <= dt.Rows.Count - 1; idx++)
        {
            int i = idx + 1;

            sb.Append("<div class='medium-12 small-12 columns' id='idColumn" + i + "'><tr>");
            sb.Append("<td><asp:Label ID='lblHname" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["FML_MEMBER_NAME"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblAdd" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["REL_TYPE_ID"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='LblConName" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["DOB"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblConNo" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["EMP_CODE"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblstate" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["MTHLY_INCOME"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblcity" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["OCCUPATION"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblpin" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["ModiDate"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblday" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["Status"].ToString() + "</td>");
            //<%#Eval("Status").ToString()=="1"?"ACTIVE":"INACTIVE"%>


            //sb.Append("<td><a href=");
            //sb.Append("'SupAdminAgrementImg.aspx?Myid=");
            //sb.Append(dt.Rows[idx]["id"].ToString());
            //sb.Append("'<i class='fa fa-file-text-o'></i><asp:Label ID='lblprice' runat='server' Text='Label'></asp:Label></a></span></td>");


            //sb.Append("<td><a href=");
            //sb.Append("'SupCorpratePendingStatus.aspx?Myid=Cop");
            //sb.Append(dt.Rows[idx]["id"].ToString());
            //sb.Append("'<i class='fa fa-file-text-o'></i><asp:Label ID='lblprice' runat='server' Text='Label'></asp:Label></a></span></td>");


            sb.Append("</td></tr> </div>");
            idRow1.InnerHtml = sb.ToString();
            // idRow1.InnerHtml = sb.ToString();         
        }
    }
    protected void btnsunbitt_Click(object sender, EventArgs e)
    {
        if (txtusername.Text == "")
        {          
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter User ID.');", true);
            txtusername.Focus();
        }
        else
        {
            Session["sub1"] = txtusername.Text;
            txtusername.Text = Session["sub1"].ToString();
            ensbledata1.Disabled = true;  
        }
    }
}