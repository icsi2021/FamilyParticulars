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
using System.Data.OracleClient;
using System.Data.SqlClient;
public partial class Dashboard : System.Web.UI.Page
{
    DatabaseFunctions dbClass = new DatabaseFunctions();
    string EMPCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])) || Convert.ToString(Session["UserName"]) == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }
            EMPCode = Convert.ToString(Session["UserName"]);
            txtEMPCode.Text = EMPCode;
            BindUserDetails(txtEMPCode.Text);
            Showdetail();
            DisplayRecordFamilydetail();
            DisplayRecordProvident();
            DisplayRecordPension();
            DisplayRecordGratuity();
            DisplayRecordBenevolent();
            DisplayRecordEncashment();
            //  btnaddrelation.Visible = false;

            // lblwelcomemessage.Text = "Welcome: " + Convert.ToString(Session["Name"]);
        }
    }
    /// <summary>
    /// Refresh Data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    /// <summary>
    /// Oracle Connection
    /// </summary>

    /// <summary>
    /// Redirect to Report Page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param> 

    public void Showdetail()
    {
        SqlParameter[] pr = {  
                                  new SqlParameter("@aa","1"),
                                  new SqlParameter("@Uname", Session["UserName"])
                                  };
        DataTable dt = new DataTable();
        dt = dbClass.getData("Proc_Getallrecords", pr);
        if (dt.Rows.Count >= 1)
        {
            string App = dt.Rows[0]["IsApproved"].ToString();
            string Rej = dt.Rows[0]["IsReject"].ToString();
            string Hardcopy = dt.Rows[0]["Ishardcopy"].ToString();

            if (App == "1")
            {
                lblBen1.Text = "Approved";
                lblBen1.ToolTip = Convert.ToString(dt.Rows[0]["Comments"]);
            }
            //else
            //{
            //    lblBen1.Text = "Pending";
            //}
            if (App == "2")
            {
                lblBen1.Text = "Rejected";
                lblBen1.ToolTip = Convert.ToString(dt.Rows[0]["Comments"]);
            }
            //else
            //{
            //    lblBen2.Text = "Pending";
            //}
            if (Hardcopy == "3")
            {
                lblBen2.Text = "Received";
            }
            //else
            //{
            //    lblBen3.Text = "NA";
            //}
        }
        SqlParameter[] pr1 = {                    
                                  new SqlParameter("@aa","2"),
                                  new SqlParameter("@Uname", Session["UserName"])
                                  };
        DataTable dt1 = new DataTable();
        dt1 = dbClass.getData("Proc_Getallrecords", pr1);
        if (dt1.Rows.Count >= 1)
        {

            string App = dt1.Rows[0]["IsApproved"].ToString();
            string Rej = dt1.Rows[0]["IsReject"].ToString();
            string Hardcopy = dt1.Rows[0]["Ishardcopy"].ToString();

            if (App == "1")
            {
                lblBen4.Text = "Approved";
                lblBen4.ToolTip = Convert.ToString(dt1.Rows[0]["Comments"]);
            }
            //else
            //{
            //  lblBen4.Text = "Pending";
            //}
            if (App == "2")
            {
                lblBen4.Text = "Rejected";
                lblBen4.ToolTip = Convert.ToString(dt1.Rows[0]["Comments"]);
            }
            //else
            //{
            //    lblBen4.Text = "Pending";
            //}
            if (Hardcopy == "3")
            {
                lblBen5.Text = "Received";
            }
            // else
            // {
            //   lblBen6.Text = "NA";
            // }
        }

        SqlParameter[] pr2 = {                    
                                  new SqlParameter("@aa","3"),
                                  new SqlParameter("@Uname", Session["UserName"])
                                  };
        DataTable dt2 = new DataTable();
        dt2 = dbClass.getData("Proc_Getallrecords", pr2);
        if (dt2.Rows.Count >= 1)
        {
            string App = dt2.Rows[0]["IsApproved"].ToString();
            string Rej = dt2.Rows[0]["IsReject"].ToString();
            string Hardcopy = dt2.Rows[0]["Ishardcopy"].ToString();
            if (App == "1")
            {
                lblBen7.Text = "Approved";
                lblBen7.ToolTip = Convert.ToString(dt2.Rows[0]["Comments"]);
            }
            //else
            //{
            //    lblBen7.Text = "Pending";
            //}
            if (App == "2")
            {
                lblBen7.Text = "Rejected";
                lblBen7.ToolTip = Convert.ToString(dt2.Rows[0]["Comments"]);
            }
            //else
            //{
            //    lblBen8.Text = "Pending";
            //}
            if (Hardcopy == "3")
            {
                lblBen8.Text = "Recieved";
            }
            //else
            //{
            //    lblBen9.Text = "NA";
            //}
        }
        SqlParameter[] pr3 = {                    
                                  new SqlParameter("@aa","4"),
                                  new SqlParameter("@Uname", Session["UserName"])
                                  };
        DataTable dt3 = new DataTable();
        dt3 = dbClass.getData("Proc_Getallrecords", pr3);
        if (dt3.Rows.Count >= 1)
        {
            string App = dt3.Rows[0]["IsApproved"].ToString();
            string Rej = dt3.Rows[0]["IsReject"].ToString();
            string Hardcopy = dt3.Rows[0]["Ishardcopy"].ToString();

            if (App == "1")
            {
                lblBen10.Text = "Approved";
                lblBen10.ToolTip = Convert.ToString(dt3.Rows[0]["Comments"]);
            }
            //else
            //{
            //    lblBen10.Text = "Pending";
            //}
            if (App == "2")
            {
                lblBen10.Text = "Rejected";
                lblBen10.ToolTip = Convert.ToString(dt3.Rows[0]["Comments"]);
            }
            //else
            //{
            //    lblBen11.Text = "Pending";
            //}
            if (Hardcopy == "3")
            {
                lblBen11.Text = "Received";
            }
            //else
            //{
            //    lblBen12.Text = "NA";
            //}
        }
        SqlParameter[] pr4 = {                    
                                  new SqlParameter("@aa","5"),
                                  new SqlParameter("@Uname", Session["UserName"])
                                  };
        DataTable dt4 = new DataTable();
        dt4 = dbClass.getData("Proc_Getallrecords", pr4);
        if (dt4.Rows.Count >= 1)
        {
            string App = dt4.Rows[0]["IsApproved"].ToString();
            string Rej = dt4.Rows[0]["IsReject"].ToString();
            string Hardcopy = dt4.Rows[0]["Ishardcopy"].ToString();

            if (App == "1")
            {
                lblBen13.Text = "Approved";
                lblBen13.ToolTip = Convert.ToString(dt4.Rows[0]["Comments"]);
            }
            //else
            //{
            //    lblBen13.Text = "Pending";
            //}
            if (App == "2")
            {
                lblBen13.Text = "Rejected";
                lblBen13.ToolTip = Convert.ToString(dt4.Rows[0]["Comments"]);
            }
            //else
            //{
            //    lblBen14.Text = "Pending";
            //}
            if (Hardcopy == "3")
            {
                lblBen14.Text = "Recieved";

            }
            //else
            //{
            //    lblBen15.Text = "NA";
            //}
        }

        SqlParameter[] pr5 = {                    
                                  new SqlParameter("@aa","6"),
                                  new SqlParameter("@Uname", Session["UserName"])
                                  };
        DataTable dt5 = new DataTable();
        dt5 = dbClass.getData("Proc_Getallrecords", pr5);
        if (dt5.Rows.Count >= 1)
        {
            string App = dt5.Rows[0]["IsApproved"].ToString();
            string Rej = dt5.Rows[0]["IsReject"].ToString();
            string Hardcopy = dt5.Rows[0]["Ishardcopy"].ToString();

            if (App == "1")
            {
                lblBen16.Text = "Approved";
                lblBen16.ToolTip = Convert.ToString(dt5.Rows[0]["Comments"]);
            }
            //else
            //{
            //    lblBen16.Text = "Pending";
            //}
            if (App == "2")
            {
                lblBen16.Text = "Rejected";
                lblBen16.ToolTip = Convert.ToString(dt5.Rows[0]["Comments"]);
            }
            //else
            //{
            //    lblBen17.Text = "Pending";
            //}
            if (Hardcopy == "3")
            {
                lblBen17.Text = "Recieved";
            }
            //else
            //{
            //    lblBen18.Text = "NA";
            //}
        }
    }
    private void DisplayRecordFamilydetail()
    {
        SqlParameter[] pr1 = {            
           new SqlParameter("@aa","1"),   
           new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),
        };

        DataTable dt = dbClass.getData("Proc_GetAlldata_hardcopyapprove", pr1);
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
    private void DisplayRecordProvident()
    {
        SqlParameter[] pr1 = {            
                  new SqlParameter("@aa","2"),   
                  new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),
        };

        DataTable dt = dbClass.getData("Proc_GetAlldata_hardcopyapprove", pr1);
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
    private void DisplayRecordPension()
    {

        SqlParameter[] pr1 = {            
                  new SqlParameter("@aa","3"),   
                  new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),
        };

        DataTable dt = dbClass.getData("Proc_GetAlldata_hardcopyapprove", pr1);
        if (dt.Rows.Count > 0)
        {
            GvSaleRecordDetails2.DataSource = dt;
            GvSaleRecordDetails2.DataBind();
        }
        else
        {
            GvSaleRecordDetails2.DataSource = null;
            GvSaleRecordDetails2.DataBind();
        }
    }
    private void DisplayRecordGratuity()
    {
        SqlParameter[] pr1 = {            
                  new SqlParameter("@aa","4"),   
                  new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),
        };

        DataTable dt = dbClass.getData("Proc_GetAlldata_hardcopyapprove", pr1);
        if (dt.Rows.Count > 0)
        {
            GvSaleRecordDetails3.DataSource = dt;
            GvSaleRecordDetails3.DataBind();
        }
        else
        {
            GvSaleRecordDetails3.DataSource = null;
            GvSaleRecordDetails3.DataBind();
        }
    }
    private void DisplayRecordBenevolent()
    {
        SqlParameter[] pr1 = {            
                  new SqlParameter("@aa","5"),   
                  new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),
        };

        DataTable dt = dbClass.getData("Proc_GetAlldata_hardcopyapprove", pr1);
        if (dt.Rows.Count > 0)
        {
            GvSaleRecordDetails4.DataSource = dt;
            GvSaleRecordDetails4.DataBind();
        }
        else
        {
            GvSaleRecordDetails4.DataSource = null;
            GvSaleRecordDetails4.DataBind();
        }
    }
    private void DisplayRecordEncashment()
    {
        SqlParameter[] pr1 = {            
                  new SqlParameter("@aa","6"),   
                  new SqlParameter("@EMP_CODE",Session["UserName"].ToString()),
        };

        DataTable dt = dbClass.getData("Proc_GetAlldata_hardcopyapprove", pr1);
        if (dt.Rows.Count > 0)
        {
            GvSaleRecordDetails5.DataSource = dt;
            GvSaleRecordDetails5.DataBind();
        }
        else
        {
            GvSaleRecordDetails5.DataSource = null;
            GvSaleRecordDetails5.DataBind();
        }
    }
    public void BindUserDetails(string Code)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetAllDetailsByEMPCODE";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = Code;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IsApproved"].ToString() == "0")
                {
                    // btnreport.Enabled = false;
                }
                txtEMPCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMP_CODE"]);
                txtName.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                txtdepartment.Text = Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT"]);
                txtdesignation.Text = Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION"]);
                if (Convert.ToString(ds.Tables[0].Rows[0]["FN_YR"]).Contains("-"))
                { txtfinancialyear.Text = Convert.ToString(ds.Tables[0].Rows[0]["FN_YR"]).Split('-')[1] + " " + Convert.ToString(ds.Tables[0].Rows[0]["FN_YR"]).Split('-')[2]; }
                else { txtfinancialyear.Text = Convert.ToString(ds.Tables[0].Rows[0]["FN_YR"]); }
                txtDocDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["DOB"]);
                txtpaddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["P_ADDRESS"]);
                txtcaddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["C_ADDRESS"]);
                txtreligion.Text = Convert.ToString(ds.Tables[0].Rows[0]["REGION"]);
                ddlgender.Text = Convert.ToString(ds.Tables[0].Rows[0]["SEX"]);
                ddlmaritalstatus.Text = Convert.ToString(ds.Tables[0].Rows[0]["MARITAL_STATUS"]);
                txtBlood.Text = Convert.ToString(ds.Tables[0].Rows[0]["BLOODGROUP"]);
                txtMobile.Text = Convert.ToString(ds.Tables[0].Rows[0]["MOBILE"]);
                ddlEmploymentstatus.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_STATUS"]);
                ddlreasonforendofemployment.Text = Convert.ToString(ds.Tables[0].Rows[0]["REAGON_FOR_END_EMP"]);
                txtcomments.Text = Convert.ToString(ds.Tables[0].Rows[0]["Comments"]);
                txtaccountno.Text = Convert.ToString(ds.Tables[0].Rows[0]["BankAccount"]);
                txtifsc.Text = Convert.ToString(ds.Tables[0].Rows[0]["IFSCCode"]);
                txtbankname.Text = Convert.ToString(ds.Tables[0].Rows[0]["BankName"]);
                ViewState["Email"] = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                Session["Email"] = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}

