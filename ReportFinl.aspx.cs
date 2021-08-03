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


public partial class ReportFinl : System.Web.UI.Page 
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
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Myid"])))
            {

                if (Convert.ToInt32(Request.QueryString["Myid"].ToString()) > 0)
                {
                    aa = Convert.ToInt32(Request.QueryString["Myid"].ToString());
                    str = aa.ToString();
                    hddstr1.Text = Convert.ToInt32(str.Replace("'", String.Empty)).ToString();
                    // DisplayRecord();

                    //if (hddstr1.Text == "1")
                    //{
                    //    lbltname.Text = "Family Details";
                    //    DisplayRecord21();
                    //}
                    if (hddstr1.Text == "2")
                    {
                        lbltname.Text = "Provident Fund";
                        DisplayRecord22();
                    }
                    if (hddstr1.Text == "3")
                    {
                        lbltname.Text = "New Pension Trust";
                        DisplayRecord23();
                    }
                    if (hddstr1.Text == "4")
                    {
                        lbltname.Text = "Gratuity";
                        DisplayRecord24();
                    }
                    if (hddstr1.Text == "5")
                    {
                        lbltname.Text = "Benevolent Fund";
                        DisplayRecord25();
                    }
                    if (hddstr1.Text == "6")
                    {
                        lbltname.Text = "Encashment";
                        DisplayRecord26();
                    }
                }
            }
        }
    }
    //private void DisplayRecord21()
    //{
    //    SqlParameter[] pr21 = {
    //              new SqlParameter("@aa","1"),   
    //              new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),                                 
    //    };
    //    DataTable dt = dbClass.getData("Proc_GetAlldata", pr21);
    //    if (dt.Rows.Count > 0)
    //    {
    //        CreateDynamicBox13(dt);
    //    }
    //    else
    //    {

    //    }
    //}

    private void DisplayRecord22() 
    {
        SqlParameter[] pr22 = {
                  new SqlParameter("@aa","2"),   
                  new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),                              
        };
        DataTable dt = dbClass.getData("Proc_GetAlldata", pr22);
        if (dt.Rows.Count > 0)
        {
            CreateDynamicBox13(dt);
        }
        else
        {

        }
    }
    private void DisplayRecord23()
    {
        SqlParameter[] pr21 = {
                  new SqlParameter("@aa","3"),             
                    new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),                    
        };
        DataTable dt = dbClass.getData("Proc_GetAlldata", pr21);
        if (dt.Rows.Count > 0)
        {
            CreateDynamicBox14(dt);
        }
        else
        {

        }
    }
    private void DisplayRecord24()
    {
        SqlParameter[] pr21 = {
                  new SqlParameter("@aa","4"),       
                    new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),                          
        };
        DataTable dt = dbClass.getData("Proc_GetAlldata", pr21);
        if (dt.Rows.Count > 0)
        {
            CreateDynamicBox15(dt);
        }
        else
        {

        }
    }
    private void DisplayRecord25()
    {
        SqlParameter[] pr21 = {
                  new SqlParameter("@aa","5"),
                    new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),                                 
        };
        DataTable dt = dbClass.getData("Proc_GetAlldata", pr21);
        if (dt.Rows.Count > 0)
        {
            CreateDynamicBox16(dt);
        }
        else
        {

        }
    }
    private void DisplayRecord26() 
    {
        SqlParameter[] pr21 = {
                  new SqlParameter("@aa","6"), 
                  new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),                              
        };
        DataTable dt = dbClass.getData("Proc_GetAlldata", pr21);
        if (dt.Rows.Count > 0)
        {
            CreateDynamicBox17(dt);
        }
        else
        {

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
            sb.Append("<td><asp:Label ID='lblstate" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["ModiDate"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblcity" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["ProvidentShare"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblpin" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["Status"].ToString() + "</td>");
         


            sb.Append("</td></tr> </div>");
            idRow1.InnerHtml = sb.ToString();
            // idRow1.InnerHtml = sb.ToString();         
        }
    }
    protected void CreateDynamicBox14(DataTable dt)
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
            sb.Append("<td><asp:Label ID='lblstate" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["ModiDate"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblcity" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["PensionShare"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblpin" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["Status"].ToString() + "</td>");


            sb.Append("</td></tr> </div>");
            idRow1.InnerHtml = sb.ToString();

        }
    }
    protected void CreateDynamicBox15(DataTable dt)
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
            sb.Append("<td><asp:Label ID='lblstate" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["ModiDate"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblcity" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["GratuityShare"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblpin" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["Status"].ToString() + "</td>");


            sb.Append("</td></tr> </div>");
            idRow1.InnerHtml = sb.ToString();

        }
    }
    protected void CreateDynamicBox16(DataTable dt)
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
            sb.Append("<td><asp:Label ID='lblstate" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["ModiDate"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblcity" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["BenevolentShare"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblpin" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["Status"].ToString() + "</td>");


            sb.Append("</td></tr> </div>");
            idRow1.InnerHtml = sb.ToString();

        }
    }
    protected void CreateDynamicBox17(DataTable dt)
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
            sb.Append("<td><asp:Label ID='lblstate" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["ModiDate"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblcity" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["EncashShare"].ToString() + "</td>");
            sb.Append("<td><asp:Label ID='lblpin" + i + "' runat='server' Text='Label'></asp:Label>" + dt.Rows[idx]["Status"].ToString() + "</td>");


            sb.Append("</td></tr> </div>");
            idRow1.InnerHtml = sb.ToString();

        }
    }





    protected void btn_Submit_ServerClick(object sender, EventArgs e)
    {

       
    }
}