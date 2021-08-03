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
using System.Globalization;
public partial class AddRelation : System.Web.UI.Page
{
    DatabaseFunctions dbClass = new DatabaseFunctions();
    string aa;
    string aa1, str,str1;
    
    protected void Page_Load(object sender, EventArgs e)
    
    {
        
        
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])) || Session["UserName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            Lbluname.Text = Session["UserName"].ToString();
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Myid"])))
            {
                aa = Request.QueryString["Myid"].ToString();
                aa1 = aa.Substring(0, 3);
                if (aa1 == "hos")
                {
                    aa = Request.QueryString["Myid"].ToString();
                    aa1 = aa.Substring(0, 3);
                    str = aa.Substring(3);
                    str1 = str.Replace("'", String.Empty);
                    lblno1.Text = str1;
                    Deleterecord();

                }
            }            
          //  DisplayRecord();


           SqlParameter[] pr1 = {
           new SqlParameter("@Username",Session["UserName"].ToString()),             
           };
            DataTable dt = dbClass.getData("proc_Getusername", pr1);
            if (dt.Rows.Count > 0)
            {
                uderval.Text = dt.Rows[0]["IsUser"].ToString();
                if (uderval.Text == "0")
                {
                    DisplayRecord();
                }
                if (uderval.Text == "1")
                {
                    DisplayRecord1();
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
   
  protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtRelation.Text == "")
        {
            //lbl_Error.Text = "Please Enter Frist Name"; Session["UserName"]
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Relation.');", true);
            txtRelation.Focus();
        }
        else
        {

           // string ddate1 = System.DateTime.Now.ToString();
            //DateTime ddate1 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);  
            SqlParameter[] pr4 ={              
                             new SqlParameter("@Relation",txtRelation.Text),
                             new SqlParameter("@Username",Lbluname.Text),
                             //new SqlParameter("@Type","1"),
                                };
            int dd4 = dbClass.InsertData("Proc_InsertRelation", pr4);
            if (dd4 > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('New Item Successfully Create.');", true);
                txtRelation.Text = "";
                if (uderval.Text == "0")
                    {
                        DisplayRecord();
                    }
                if (uderval.Text == "1")
                    {
                        DisplayRecord1();
                    }
                
              //  DisplayRecord();
            }

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

    private void DisplayRecord()
    {

        SqlParameter[] pr1 = {
             //new SqlParameter("@UserId",Convert.ToInt32(Session["UserId"].ToString())),
                 //  new SqlParameter("@MemberId",Session["UserName"].ToString()),                
                
                   
        };

        DataTable dt = dbClass.getData("Proc_GetdataRelation", pr1);
        if (dt.Rows.Count > 0)
        {
            GvSaleRecordDetails.DataSource = dt;
            GvSaleRecordDetails.DataBind();

        }
        else
        {
            GvSaleRecordDetails.DataSource = null;
            GvSaleRecordDetails.DataBind();
        }

    }

    private void DisplayRecord1()
    {

        SqlParameter[] pr1 = {
             new SqlParameter("@Username",Lbluname.Text),           
        };

        DataTable dt = dbClass.getData("Proc_GetdataRelationUser", pr1);
        if (dt.Rows.Count > 0)
        {
            GvSaleRecordDetails1.DataSource = dt;
            GvSaleRecordDetails1.DataBind();

        }
        else
        {
            GvSaleRecordDetails1.DataSource = null;
            GvSaleRecordDetails1.DataBind();
        }

    }
    public void Deleterecord()
    {
        //

        SqlParameter[] pr1 = {
             new SqlParameter("@id",lblno1.Text),
                   
        };

        DataTable dt = dbClass.getData("Proc_GetRelationidname", pr1);
        if (dt.Rows.Count > 0)
        {
            Lbluname1.Text = dt.Rows[0]["TYPE_NAME"].ToString();
        }


        SqlParameter[] pr2 = {
             new SqlParameter("@id",Lbluname1.Text),
                   
        };

        DataTable dt2 = dbClass.getData("Proc_GetRelationidname1", pr2);
        if (dt2.Rows.Count > 0)       
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('All Ready Used Not Delete.');", true);
        }
        else
        {
            //  Lbluname1.Text = dt2.Rows[0]["TYPE_NAME"].ToString();
            string ddate1 = System.DateTime.Now.ToString();
            SqlParameter[] pr4 ={              
                             new SqlParameter("@Relationid",lblno1.Text),
                             new SqlParameter("@Username",Lbluname.Text),
                            // new SqlParameter("@Type","0"),
                                };
            int dd4 = dbClass.InsertData("Proc_DeleteRelation", pr4);
            if (dd4 > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Delete Successfully.');", true);
                txtRelation.Text = "";
                DisplayRecord();
            }
        }
       
    }


    protected void txtEMPCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandText = "GetEmployeefromEmployeeTableByEmpID";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = txtEMPCode.Text;
        //    con.Open();
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(ds);
        //    con.Close();
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        txtEMPCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMP_CODE"]);
        //        txtName.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
        //        txtdepartment.Text = Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT"]);
        //        txtdesignation.Text = Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION"]);
        //        txtfinancialyear.Text = Convert.ToString(ds.Tables[0].Rows[0]["FN_YR"]);
        //        txtDocDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["DOB"]);
        //        txtpaddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["P_ADDRESS"]);
        //        txtcaddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["C_ADDRESS"]);
        //        txtreligion.Text = Convert.ToString(ds.Tables[0].Rows[0]["REGION"]);
        //        ddlmaritalstatus.ClearSelection();
        //        ddlgender.ClearSelection();
        //        ddlEmploymentstatus.ClearSelection();
        //        ddlgender.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[0]["SEX"])).Selected = true;
        //        ddlmaritalstatus.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[0]["MARITAL_STATUS"])).Selected = true;
        //        ddlEmploymentstatus.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_STATUS"])).Selected = true;
        //        ddlreasonforendofemployment.Text = Convert.ToString(ds.Tables[0].Rows[0]["REAGON_FOR_END_EMP"]);
        //        txtcomments.Text = Convert.ToString(ds.Tables[0].Rows[0]["comments"]);
        //        txtemaild.Text = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
        //    }
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
        //txtName.Text = string.Empty;
        //txtEMPCode.Text = string.Empty;
        //txtpaddress.Text = string.Empty;
        //txtreligion.Text = string.Empty;
        //txtfinancialyear.Text = string.Empty;
        //txtemaild.Text = string.Empty;
        //txtpaddress.Text = string.Empty;
    }
}
