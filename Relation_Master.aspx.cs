using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Net.Mail;
public partial class Relation_Master : System.Web.UI.Page
{
    DatabaseFunctions dbClass = new DatabaseFunctions();
    /// <summary>
    /// 
    /// </summary>
    string EMPCode = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>,
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!Page.IsPostBack)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])) || Session["UserName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                EMPCode = Convert.ToString(Session["UserName"]);
                txtEMPCode.Text = EMPCode;
                DataTable dtCode = GetEmployeeByID(txtEMPCode.Text);
                if (dtCode != null && dtCode.Rows.Count > 0)
                {
                    BindUserDetails(txtEMPCode.Text);

                    btnsubmit.Enabled = true;
                }
                else if (GD_FamilyDetails.Rows.Count <= 0 && dtCode.Rows.Count <= 0)
                {
                    SetInitialRow();
                    btnsubmit.Enabled = false;
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Code"></param>
    public void BindUserDetails(string Code)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetAllDetailsByEMPCODE1";
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
                    btnreport.Enabled = false;
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
                ddlEmploymentstatus.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_STATUS"]);
                ddlreasonforendofemployment.Text = Convert.ToString(ds.Tables[0].Rows[0]["REAGON_FOR_END_EMP"]);
                txtcomments.Text = Convert.ToString(ds.Tables[0].Rows[0]["Comments"]);
                ViewState["Email"] = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                Session["Email"] = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                ViewState["CurrentTable"] = ds.Tables[1];
                GD_FamilyDetails.DataSource = ds.Tables[1];
                GD_FamilyDetails.DataBind();
                int rowIndex = 0;
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    Label labelrow = (Label)GD_FamilyDetails.Rows[i].Cells[i].FindControl("labelrow");
                    DropDownList ddlREL_TYPE_ID = (DropDownList)GD_FamilyDetails.Rows[i].Cells[1].FindControl("ddlREL_TYPE_ID");
                    TextBox lblFML_MEMBER_NAME = (TextBox)GD_FamilyDetails.Rows[i].Cells[2].FindControl("lblFML_MEMBER_NAME");
                    DropDownList ddlchkdob = (DropDownList)GD_FamilyDetails.Rows[i].Cells[3].FindControl("ddlchkdob");
                    TextBox dobcal = (TextBox)GD_FamilyDetails.Rows[i].Cells[4].FindControl("dobcal");
                    TextBox lblMTHLY_INCOME = (TextBox)GD_FamilyDetails.Rows[i].Cells[5].FindControl("lblMTHLY_INCOME");
                    TextBox lblOCCUPATION = (TextBox)GD_FamilyDetails.Rows[i].Cells[6].FindControl("lblOCCUPATION");
                    DropDownList chkDEPENT_ON_EMP = (DropDownList)GD_FamilyDetails.Rows[i].Cells[7].FindControl("chkDEPENT_ON_EMP");
                    DropDownList lblSTAY_WITH_EMP = (DropDownList)GD_FamilyDetails.Rows[i].Cells[8].FindControl("lblSTAY_WITH_EMP");
                    DropDownList lblSPOUSE_WORKING = (DropDownList)GD_FamilyDetails.Rows[i].Cells[9].FindControl("lblSPOUSE_WORKING");
                    TextBox lblSPOUSE_WORKING_IN = (TextBox)GD_FamilyDetails.Rows[i].Cells[10].FindControl("lblSPOUSE_WORKING_IN");
                    DropDownList lblSPOUSE_MEDICAL_FROM_OFFICE = (DropDownList)GD_FamilyDetails.Rows[i].Cells[11].FindControl("lblSPOUSE_MEDICAL_FROM_OFFICE");
                    DropDownList lblSPOUSE_LTC_FROM_OFFICE = (DropDownList)GD_FamilyDetails.Rows[i].Cells[12].FindControl("lblSPOUSE_LTC_FROM_OFFICE");

                    ddlREL_TYPE_ID.ClearSelection();
                    chkDEPENT_ON_EMP.ClearSelection();
                    lblSTAY_WITH_EMP.ClearSelection();
                    lblSPOUSE_WORKING.ClearSelection();
                    ddlchkdob.ClearSelection();
                    lblSPOUSE_MEDICAL_FROM_OFFICE.ClearSelection();
                    lblSPOUSE_LTC_FROM_OFFICE.ClearSelection();

                    labelrow.Text = Convert.ToString(ds.Tables[1].Rows[i]["RowNumber"]);
                    lblFML_MEMBER_NAME.Text = Convert.ToString(ds.Tables[1].Rows[i]["FML_MEMBER_NAME"]);
                    lblFML_MEMBER_NAME.ReadOnly = true;
                    ddlREL_TYPE_ID.Items.FindByText(Convert.ToString(ds.Tables[1].Rows[i]["REL_TYPE_ID"])).Selected = true;
                    ddlREL_TYPE_ID.Enabled = false;
                    ddlchkdob.Items.FindByText(Convert.ToString(ds.Tables[1].Rows[i]["IsActualDate"])).Selected = true;
                    dobcal.Text = Convert.ToString(ds.Tables[1].Rows[i]["DOB"]);
                    dobcal.Enabled = false;
                    lblMTHLY_INCOME.Text = Convert.ToString(ds.Tables[1].Rows[i]["MTHLY_INCOME"]);
                    lblMTHLY_INCOME.Enabled = false;
                    lblOCCUPATION.Text = Convert.ToString(ds.Tables[1].Rows[i]["OCCUPATION"]);
                    lblOCCUPATION.ReadOnly = true;
                    if (Convert.ToString(ds.Tables[1].Rows[i]["DEPENT_ON_EMP"]) == "1")
                    {
                        chkDEPENT_ON_EMP.Items.FindByText(Convert.ToString(ds.Tables[1].Rows[i]["DEPENT_ON_EMP"])).Selected = true;
                    }
                    if (Convert.ToString(ds.Tables[1].Rows[i]["STAY_WITH_EMP"]) == "1")
                    {
                        lblSTAY_WITH_EMP.Items.FindByText(Convert.ToString(ds.Tables[1].Rows[i]["STAY_WITH_EMP"])).Selected = true;
                    }
                    if (Convert.ToString(ds.Tables[1].Rows[i]["SPOUSE_WORKING"]) == "1")
                    {
                        lblSPOUSE_WORKING.Items.FindByText(Convert.ToString(ds.Tables[1].Rows[i]["SPOUSE_WORKING"])).Selected = true;
                    }
                    lblSPOUSE_WORKING_IN.Text = Convert.ToString(ds.Tables[1].Rows[i]["SPOUSE_WORKING_IN"]);
                    lblSPOUSE_MEDICAL_FROM_OFFICE.Items.FindByText(Convert.ToString(ds.Tables[1].Rows[i]["SPOUSE_MEDICAL_FROM_OFFICE"])).Selected = true;
                    lblSPOUSE_LTC_FROM_OFFICE.Items.FindByText(Convert.ToString(ds.Tables[1].Rows[i]["SPOUSE_LTC_FROM_OFFICE"])).Selected = true;
                    rowIndex++;
                }
            }
            else
            {
                SetInitialRow();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        int k = 1;//SaveHeader();
        int rowIndex = 0;
        for (int i = 1; i <= GD_FamilyDetails.Rows.Count; i++)
        {
            TextBox lblFML_MEMBER_NAME = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[2].FindControl("lblFML_MEMBER_NAME");
            DropDownList ddlchkdob = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[3].FindControl("ddlchkdob");
            TextBox dobcal = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[4].FindControl("dobcal");
            rowIndex++;
            if (lblFML_MEMBER_NAME.Text == "" || dobcal.Text == "")
            { return; }
        }
        if (k > 0)
        {
            View();
            int m = SaveItemDetails();
            if (m > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
                ViewState["CurrentTable"] = null;
                string Message = "Mr./Ms. Gaurav Mehta,The Family Particular of Mr./Mrs." + Convert.ToString(Session["Name"]) + @" has been sent for approval.";
               // SendEmail(Message, "Submission Family Particular", "Gaurav.mehta@icsi.edu");
                string value = txtEMPCode.Text + "~" + 1;
                ScriptManager.RegisterStartupScript(this, GetType(), "Fetch", "Preview('" + value + @"');", true);
               // Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                
                BindUserDetails(txtEMPCode.Text);
            }
        }
    }
    /// <summary>
    /// Set Initial Row For GridViewDetails
    /// </summary>
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("FML_MEMBER_NAME", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("REL_TYPE_ID", typeof(string)));//for TextBox value 
        dt.Columns.Add(new DataColumn("IsActualDate", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("DOB", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("MTHLY_INCOME", typeof(string)));//for TextBox value 
        dt.Columns.Add(new DataColumn("OCCUPATION", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("DEPENT_ON_EMP", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("STAY_WITH_EMP", typeof(string)));//for TextBox value  
        dt.Columns.Add(new DataColumn("SPOUSE_WORKING", typeof(string)));//for TextBox value  
        dt.Columns.Add(new DataColumn("SPOUSE_WORKING_IN", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("SPOUSE_MEDICAL_FROM_OFFICE", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("SPOUSE_LTC_FROM_OFFICE", typeof(string)));//for TextBox value  
        dt.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));//for TextBox value     
        dt.Columns.Add(new DataColumn("Comments", typeof(string)));//for TextBox value  
        dr = dt.NewRow();
        dr["RowNumber"] = 0;
        dr["FML_MEMBER_NAME"] = string.Empty;
        dr["REL_TYPE_ID"] = string.Empty;
        dr["IsActualDate"] = string.Empty;
        dr["DOB"] = string.Empty;
        dr["MTHLY_INCOME"] = string.Empty;
        dr["OCCUPATION"] = string.Empty;
        dr["DEPENT_ON_EMP"] = string.Empty;
        dr["STAY_WITH_EMP"] = string.Empty;
        dr["SPOUSE_WORKING"] = string.Empty;
        dr["SPOUSE_WORKING_IN"] = string.Empty;
        dr["SPOUSE_MEDICAL_FROM_OFFICE"] = string.Empty;
        dr["SPOUSE_LTC_FROM_OFFICE"] = string.Empty;
        dr["EMP_CODE"] = string.Empty;
        dr["Comments"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTable"] = dt;
        //Bind the Gridview   
        GD_FamilyDetails.DataSource = dt;
        GD_FamilyDetails.DataBind();
    }
    /// <summary>
    /// Bind DropDownlist
    /// </summary>
    /// <param name="ddlREL_TYPE_ID"></param>
    public void FillDropDownList(DropDownList ddlREL_TYPE_ID)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd1 = new SqlCommand();
        cmd1.Connection = con1;
        cmd1.CommandText = "GetRel_TypeMST";
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@EMPCode", SqlDbType.NVarChar).Value = txtEMPCode.Text;
        cmd1.Parameters.Add("@Form", SqlDbType.NVarChar).Value = "Family Particulars";
        con1.Open();
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.Fill(dt1);
        con1.Close();
        ddlREL_TYPE_ID.ClearSelection();
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            ddlREL_TYPE_ID.DataSource = dt1;
            ddlREL_TYPE_ID.DataTextField = "TYPE_NAME";
            ddlREL_TYPE_ID.DataValueField = "TYPE_ID";
            ddlREL_TYPE_ID.DataBind();
            ddlREL_TYPE_ID.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    /// <summary>
    /// Add New Row In GridView
    /// </summary>
    private void AddNewRowToGrid()
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = 0;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference 
               
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    Label labelrow = (Label)GD_FamilyDetails.Rows[i].Cells[0].FindControl("labelrow");
                    DropDownList ddlREL_TYPE_ID = (DropDownList)GD_FamilyDetails.Rows[i].Cells[1].FindControl("ddlREL_TYPE_ID");
                    TextBox lblFML_MEMBER_NAME = (TextBox)GD_FamilyDetails.Rows[i].Cells[2].FindControl("lblFML_MEMBER_NAME");
                    DropDownList ddlchkdob = (DropDownList)GD_FamilyDetails.Rows[i].Cells[3].FindControl("ddlchkdob");
                    TextBox dobcal = (TextBox)GD_FamilyDetails.Rows[i].Cells[4].FindControl("dobcal");
                    TextBox lblMTHLY_INCOME = (TextBox)GD_FamilyDetails.Rows[i].Cells[5].FindControl("lblMTHLY_INCOME");
                    TextBox lblOCCUPATION = (TextBox)GD_FamilyDetails.Rows[i].Cells[6].FindControl("lblOCCUPATION");
                    DropDownList chkDEPENT_ON_EMP = (DropDownList)GD_FamilyDetails.Rows[i].Cells[7].FindControl("chkDEPENT_ON_EMP");
                    DropDownList lblSTAY_WITH_EMP = (DropDownList)GD_FamilyDetails.Rows[i].Cells[8].FindControl("lblSTAY_WITH_EMP");
                    DropDownList lblSPOUSE_WORKING = (DropDownList)GD_FamilyDetails.Rows[i].Cells[9].FindControl("lblSPOUSE_WORKING");
                    TextBox lblSPOUSE_WORKING_IN = (TextBox)GD_FamilyDetails.Rows[i].Cells[10].FindControl("lblSPOUSE_WORKING_IN");
                    DropDownList lblSPOUSE_MEDICAL_FROM_OFFICE = (DropDownList)GD_FamilyDetails.Rows[i].Cells[11].FindControl("lblSPOUSE_MEDICAL_FROM_OFFICE");
                    DropDownList lblSPOUSE_LTC_FROM_OFFICE = (DropDownList)GD_FamilyDetails.Rows[i].Cells[12].FindControl("lblSPOUSE_LTC_FROM_OFFICE");

                    dtCurrentTable.Rows[i]["RowNumber"] = labelrow.Text == "" ? "0" : labelrow.Text;
                    dtCurrentTable.Rows[i]["FML_MEMBER_NAME"] = lblFML_MEMBER_NAME.Text;
                    dtCurrentTable.Rows[i]["IsActualDate"] = ddlchkdob.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["DOB"] = dobcal.Text;
                    dtCurrentTable.Rows[i]["MTHLY_INCOME"] = lblMTHLY_INCOME.Text;
                    dtCurrentTable.Rows[i]["OCCUPATION"] = lblOCCUPATION.Text;
                    dtCurrentTable.Rows[i]["DEPENT_ON_EMP"] = chkDEPENT_ON_EMP.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["STAY_WITH_EMP"] = lblSTAY_WITH_EMP.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["SPOUSE_WORKING"] = lblSPOUSE_WORKING.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["SPOUSE_WORKING_IN"] = lblSPOUSE_WORKING_IN.Text;
                    dtCurrentTable.Rows[i]["SPOUSE_MEDICAL_FROM_OFFICE"] = lblSPOUSE_MEDICAL_FROM_OFFICE.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["SPOUSE_LTC_FROM_OFFICE"] = lblSPOUSE_LTC_FROM_OFFICE.SelectedItem.Text;

                    // Update the DataRow with the DDL Selected Items   
                    dtCurrentTable.Rows[i]["REL_TYPE_ID"] = ddlREL_TYPE_ID.SelectedItem.Text;
                }
                //Rebind the Grid with the current data to reflect changes  
                ViewState["CurrentTable"] = dtCurrentTable;
                GD_FamilyDetails.DataSource = dtCurrentTable;
                GD_FamilyDetails.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks   
        SetPreviousData();
    }
    /// <summary>
    /// SetPrevious value In Data Table
    /// </summary>
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label labelrow = (Label)GD_FamilyDetails.Rows[rowIndex].Cells[0].FindControl("labelrow");
                    DropDownList ddlREL_TYPE_ID = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[1].FindControl("ddlREL_TYPE_ID");
                    TextBox lblFML_MEMBER_NAME = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[2].FindControl("lblFML_MEMBER_NAME");
                    DropDownList ddlchkdob = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[3].FindControl("ddlchkdob");
                    TextBox dobcal = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[4].FindControl("dobcal");
                    TextBox lblMTHLY_INCOME = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[5].FindControl("lblMTHLY_INCOME");
                    TextBox lblOCCUPATION = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[6].FindControl("lblOCCUPATION");
                    DropDownList chkDEPENT_ON_EMP = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[7].FindControl("chkDEPENT_ON_EMP");
                    DropDownList lblSTAY_WITH_EMP = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[8].FindControl("lblSTAY_WITH_EMP");
                    DropDownList lblSPOUSE_WORKING = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[9].FindControl("lblSPOUSE_WORKING");
                    TextBox lblSPOUSE_WORKING_IN = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[10].FindControl("lblSPOUSE_WORKING_IN");
                    DropDownList lblSPOUSE_MEDICAL_FROM_OFFICE = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[11].FindControl("lblSPOUSE_MEDICAL_FROM_OFFICE");
                    DropDownList lblSPOUSE_LTC_FROM_OFFICE = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[12].FindControl("lblSPOUSE_LTC_FROM_OFFICE");  
                    
                    if (i < dt.Rows.Count - 1)
                    {
                        //Assign the value from DataTable to the TextBox   
                        labelrow.Text = dt.Rows[i]["RowNumber"].ToString();
                        lblFML_MEMBER_NAME.Text = dt.Rows[i]["FML_MEMBER_NAME"].ToString();
                        lblFML_MEMBER_NAME.ReadOnly = true;
                        dobcal.Text = dt.Rows[i]["DOB"].ToString();
                        dobcal.ReadOnly = true;
                        lblMTHLY_INCOME.Text = dt.Rows[i]["MTHLY_INCOME"].ToString();
                        lblMTHLY_INCOME.ReadOnly = true;
                        lblOCCUPATION.Text = dt.Rows[i]["OCCUPATION"].ToString();
                        lblOCCUPATION.ReadOnly = true;
                        ddlchkdob.ClearSelection();
                        ddlchkdob.Items.FindByText(dt.Rows[i]["IsActualDate"].ToString()).Selected = true;
                        chkDEPENT_ON_EMP.ClearSelection();
                        chkDEPENT_ON_EMP.Items.FindByText(dt.Rows[i]["DEPENT_ON_EMP"].ToString()).Selected = true;
                        lblSTAY_WITH_EMP.ClearSelection();
                        lblSTAY_WITH_EMP.Items.FindByText(dt.Rows[i]["STAY_WITH_EMP"].ToString()).Selected = true;
                        lblSPOUSE_WORKING.ClearSelection();
                        lblSPOUSE_WORKING.Items.FindByText(dt.Rows[i]["SPOUSE_WORKING"].ToString()).Selected = true;
                        lblSPOUSE_WORKING_IN.Text = dt.Rows[i]["SPOUSE_WORKING_IN"].ToString();
                        lblSPOUSE_LTC_FROM_OFFICE.ClearSelection();
                        lblSPOUSE_MEDICAL_FROM_OFFICE.ClearSelection();
                        lblSPOUSE_MEDICAL_FROM_OFFICE.Items.FindByText(Convert.ToString(dt.Rows[i]["SPOUSE_MEDICAL_FROM_OFFICE"])).Selected = true;
                        lblSPOUSE_LTC_FROM_OFFICE.Items.FindByText(Convert.ToString(dt.Rows[i]["SPOUSE_LTC_FROM_OFFICE"])).Selected = true;
                        ddlREL_TYPE_ID.ClearSelection();
                        ddlREL_TYPE_ID.Items.FindByText(dt.Rows[i]["REL_TYPE_ID"].ToString()).Selected = true;
                    }
                    rowIndex++;
                }
            }
        }
    }
    /// <summary>
    /// Add New Row Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnAddFamilyDetails_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GD_FamilyDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)FV
        //{
        //    try
        //    {
        //        DataTable dt = (DataTable)ViewState["CurrentTable"];
        //        ImageButton lb = (ImageButton)e.Row.FindControl("btnimagedelete");
        //        DropDownList ddlREL_TYPE_ID = (DropDownList)e.Row.FindControl("ddlREL_TYPE_ID");
        //        FillDropDownList(ddlREL_TYPE_ID);
        //        if (lb != null)
        //        {
        //            if (dt.Rows.Count > 1)
        //            {
        //                if (e.Row.RowIndex == dt.Rows.Count - 1)
        //                {
        //                    lb.Visible = false;
        //                }
        //            }
        //            else
        //            {
        //                lb.Visible = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex) { ex.Message.ToString(); }
        //}
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GD_FamilyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                ImageButton lb = (ImageButton)e.Row.FindControl("btnimagedelete");
                DropDownList ddlREL_TYPE_ID = (DropDownList)e.Row.FindControl("ddlREL_TYPE_ID");
                FillDropDownList(ddlREL_TYPE_ID);
                if (lb != null)
                {
                    if (dt.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dt.Rows.Count)
                        {
                            lb.Visible = false;
                        }
                    }
                    else
                    {
                        lb.Visible = false;
                    }
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
        }
    }
    /// <summary>
    /// Delete Row From GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnimagedelete_Click(object sender, EventArgs e)
    {
        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        DataTable dt = null;
        if (ViewState["CurrentTable"] != null)
        {
            Label labelrow = (Label)GD_FamilyDetails.Rows[rowID].Cells[0].FindControl("labelrow");
            dt = (DataTable)ViewState["CurrentTable"];
            if (labelrow.Text == "0" || labelrow.Text == "")
            {
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        ViewState["CurrentTable"] = dt;
                        //ResetRowID(dt);                              
                        GD_FamilyDetails.DataSource = dt;
                        GD_FamilyDetails.DataBind();
                        SetPreviousData();
                      // BindUserDetails(txtEMPCode.Text);
                    }
                }
            }
            else
            {
                View();
                try
                {
                    TextBox lblFML_MEMBER_NAME = (TextBox)GD_FamilyDetails.Rows[rowID].Cells[0].FindControl("lblFML_MEMBER_NAME");

                    SqlParameter[] pr = {                     
                                    new SqlParameter("@EMP_CODE", txtEMPCode.Text), 
                                    new SqlParameter("@Empname",lblFML_MEMBER_NAME.Text), 
                                   new SqlParameter("@aa","1"), 
                                  };
                    DataTable dt1 = new DataTable();
                    dt1 = dbClass.getData("Proc_Getdatacheckanddelete", pr);
                    if (dt1.Rows.Count <= 1)
                    {
                        try
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "DeleteRow";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = txtEMPCode.Text;
                            cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(labelrow.Text);
                            cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "8";
                            con.Open();
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {

                                EMPCode = Convert.ToString(Session["UserName"]);
                                txtEMPCode.Text = EMPCode;
                                BindUserDetails(txtEMPCode.Text);
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertDelete", "alertDelete();", true);
                            }
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            BindUserDetails(txtEMPCode.Text);
                        }
                        ViewState["CurrentTable"] = dt;
                        //Re bind the GridView for the updated data  
                        GD_FamilyDetails.DataSource = dt;
                        GD_FamilyDetails.DataBind();
                        //Set Previous Data on Postbacks  
                        SetPreviousData();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Do not delete ...!! ');", true);
                    }                                    
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    BindUserDetails(txtEMPCode.Text);
                }
            }
        }
        //Store the current data in ViewState for future reference  
       
        // updatefamily.Update();
    }
    /// <summary>
    /// 
    /// </summary>
    protected void btnfillalldata_Click(object sender, EventArgs e)
    {
        FillUPDate();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnreset_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.AbsoluteUri);
        //Response.Redirect("FamilyDetails.aspx");
        BindUserDetails(txtEMPCode.Text);
    }
    /// <summary>
    /// GetUpdate View
    /// </summary>
    public void View()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        int rowIndex = 0;
        if (GD_FamilyDetails.Rows.Count > 0)
        {
            for (int i = 1; i <= GD_FamilyDetails.Rows.Count; i++)
            {
                //extract the TextBox values   
                Label labelrow = (Label)GD_FamilyDetails.Rows[rowIndex].Cells[0].FindControl("labelrow");
                DropDownList ddlREL_TYPE_ID = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[1].FindControl("ddlREL_TYPE_ID");                
                TextBox lblFML_MEMBER_NAME = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[2].FindControl("lblFML_MEMBER_NAME");
                    DropDownList ddlchkdob = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[3].FindControl("ddlchkdob");
                    TextBox dobcal = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[4].FindControl("dobcal");
                    TextBox lblMTHLY_INCOME = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[5].FindControl("lblMTHLY_INCOME");
                    TextBox lblOCCUPATION = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[6].FindControl("lblOCCUPATION");
                    DropDownList chkDEPENT_ON_EMP = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[7].FindControl("chkDEPENT_ON_EMP");
                    DropDownList lblSTAY_WITH_EMP = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[8].FindControl("lblSTAY_WITH_EMP");
                    DropDownList lblSPOUSE_WORKING = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[9].FindControl("lblSPOUSE_WORKING");
                    TextBox lblSPOUSE_WORKING_IN = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[10].FindControl("lblSPOUSE_WORKING_IN");
                    DropDownList lblSPOUSE_MEDICAL_FROM_OFFICE = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[11].FindControl("lblSPOUSE_MEDICAL_FROM_OFFICE");
                    DropDownList lblSPOUSE_LTC_FROM_OFFICE = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[12].FindControl("lblSPOUSE_LTC_FROM_OFFICE");
                    if (labelrow.Text == "")
                    {
                        dtCurrentTable.Rows[i - 1][0] = "0";
                    }
                    else
                    {
                        dtCurrentTable.Rows[i - 1][0] = Convert.ToString(labelrow.Text);
                    }
                    dtCurrentTable.Rows[i - 1][1] = Convert.ToString(lblFML_MEMBER_NAME.Text);
                    dtCurrentTable.Rows[i - 1][2] = Convert.ToString(ddlREL_TYPE_ID.SelectedItem.Text);
                    dtCurrentTable.Rows[i - 1][3] = ddlchkdob.SelectedItem.Text;
                    dtCurrentTable.Rows[i - 1][4] = Convert.ToString(dobcal.Text);
                    dtCurrentTable.Rows[i - 1][5] = Convert.ToString(lblMTHLY_INCOME.Text);
                    dtCurrentTable.Rows[i - 1][6] = Convert.ToString(lblOCCUPATION.Text);
                    dtCurrentTable.Rows[i - 1][7] = chkDEPENT_ON_EMP.SelectedItem.Text;
                    dtCurrentTable.Rows[i - 1][8] = lblSTAY_WITH_EMP.SelectedItem.Text;
                    dtCurrentTable.Rows[i - 1][9] = lblSPOUSE_WORKING.SelectedItem.Text;
                    dtCurrentTable.Rows[i - 1][10] = Convert.ToString(lblSPOUSE_WORKING_IN.Text);
                    dtCurrentTable.Rows[i - 1][11] = lblSPOUSE_MEDICAL_FROM_OFFICE.SelectedItem.Text;
                    dtCurrentTable.Rows[i - 1][12] = lblSPOUSE_LTC_FROM_OFFICE.SelectedItem.Text;
                    rowIndex++;
                }
                ViewState["CurrentTable"] = dtCurrentTable;
            }        
    }
    /// <summary>
    /// FillUpData
    /// </summary>
    public void FillUPDate()
    {
        int rowIndex = 0;
        View();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                GD_FamilyDetails.DataSource = dt;
                GD_FamilyDetails.DataBind();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label labelrow = (Label)GD_FamilyDetails.Rows[rowIndex].Cells[0].FindControl("labelrow");
                    DropDownList ddlREL_TYPE_ID = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[1].FindControl("ddlREL_TYPE_ID");
                    TextBox lblFML_MEMBER_NAME = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[2].FindControl("lblFML_MEMBER_NAME");
                    DropDownList ddlchkdob = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[3].FindControl("ddlchkdob");
                    TextBox dobcal = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[4].FindControl("dobcal");
                    TextBox lblMTHLY_INCOME = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[5].FindControl("lblMTHLY_INCOME");
                    TextBox lblOCCUPATION = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[6].FindControl("lblOCCUPATION");
                    DropDownList chkDEPENT_ON_EMP = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[7].FindControl("chkDEPENT_ON_EMP");
                    DropDownList lblSTAY_WITH_EMP = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[8].FindControl("lblSTAY_WITH_EMP");
                    DropDownList lblSPOUSE_WORKING = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[9].FindControl("lblSPOUSE_WORKING");
                    TextBox lblSPOUSE_WORKING_IN = (TextBox)GD_FamilyDetails.Rows[rowIndex].Cells[10].FindControl("lblSPOUSE_WORKING_IN");
                    DropDownList lblSPOUSE_MEDICAL_FROM_OFFICE = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[11].FindControl("lblSPOUSE_MEDICAL_FROM_OFFICE");
                    DropDownList lblSPOUSE_LTC_FROM_OFFICE = (DropDownList)GD_FamilyDetails.Rows[rowIndex].Cells[12].FindControl("lblSPOUSE_LTC_FROM_OFFICE");
                    ddlREL_TYPE_ID.ClearSelection();
                    chkDEPENT_ON_EMP.ClearSelection();
                    lblSTAY_WITH_EMP.ClearSelection();
                    lblSPOUSE_WORKING.ClearSelection();
                    ddlchkdob.ClearSelection();
                    ddlchkdob.Items.FindByText(Convert.ToString(dt.Rows[i]["IsActualDate"])).Selected = true;
                    labelrow.Text = Convert.ToString(dt.Rows[i]["RowNumber"]);
                    lblFML_MEMBER_NAME.Text = Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]);
                    ddlREL_TYPE_ID.Items.FindByText(Convert.ToString(dt.Rows[i]["REL_TYPE_ID"])).Selected = true;
                    dobcal.Text = Convert.ToString(dt.Rows[i]["DOB"]);
                    lblMTHLY_INCOME.Text = Convert.ToString(dt.Rows[i]["MTHLY_INCOME"]);
                    lblOCCUPATION.Text = Convert.ToString(dt.Rows[i]["OCCUPATION"]);
                    chkDEPENT_ON_EMP.Items.FindByText(Convert.ToString(dt.Rows[i]["DEPENT_ON_EMP"])).Selected = true;
                    lblSTAY_WITH_EMP.Items.FindByText(Convert.ToString(dt.Rows[i]["STAY_WITH_EMP"])).Selected = true;
                    lblSPOUSE_WORKING.Items.FindByText(Convert.ToString(dt.Rows[i]["SPOUSE_WORKING"])).Selected = true;
                    lblSPOUSE_WORKING_IN.Text = Convert.ToString(dt.Rows[i]["SPOUSE_WORKING_IN"]);
                    lblSPOUSE_MEDICAL_FROM_OFFICE.ClearSelection();
                    lblSPOUSE_LTC_FROM_OFFICE.ClearSelection();
                    lblSPOUSE_MEDICAL_FROM_OFFICE.Items.FindByText(Convert.ToString(dt.Rows[i]["SPOUSE_MEDICAL_FROM_OFFICE"])).Selected = true;
                    lblSPOUSE_LTC_FROM_OFFICE.Items.FindByText(Convert.ToString(dt.Rows[i]["SPOUSE_LTC_FROM_OFFICE"])).Selected = true;
                    rowIndex++;
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DataTable GetEmployeeByID(string CODE)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetEmployeeDetailsByEMPCODE";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = CODE;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return dt;
    }
    /// <summary>
    /// Save Header 
    /// </summary>
    /// <returns></returns>
    public int SaveHeader()
    {
        int i = 0;
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "InsertrelationDetails";
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
            cmd.Parameters.Add("@EMPLOYEE_STATUS", SqlDbType.NVarChar).Value = ddlEmploymentstatus.Text;
            cmd.Parameters.Add("@REAGON_FOR_END_EMP", SqlDbType.NVarChar).Value = ddlreasonforendofemployment.Text;
            cmd.Parameters.Add("@MARITAL_STATUS", SqlDbType.NVarChar).Value = ddlmaritalstatus.Text;
            cmd.Parameters.Add("@SEX", SqlDbType.NVarChar).Value = ddlgender.Text;
            cmd.Parameters.Add("@P_ADDRESS", SqlDbType.NVarChar).Value = txtpaddress.Text;
            cmd.Parameters.Add("@C_ADDRESS", SqlDbType.NVarChar).Value = txtcaddress.Text;
            cmd.Parameters.Add("@REGION", SqlDbType.NVarChar).Value = txtreligion.Text;
            cmd.Parameters.Add("@IsApproved", SqlDbType.Int).Value = "0";
            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //lblError.Text = ex.Message.ToString();
        }
        return i;
    }
    /// <summary>
    /// Save ItemDetails
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public int SaveItemDetails()
    {
        int k = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = null;
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "InsertrelationDetails";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[i]["RowNumber"]);
                    cmd.Parameters.Add("@REL_TYPE_ID", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                    if (dt.Rows[i]["REL_TYPE_ID"]=="Other")
                    {
                        cmd.Parameters.Add("@FML_MEMBER_NAME", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["TxtotherRel"]);
                    }
                    cmd.Parameters.Add("@FML_MEMBER_NAME", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]);                   
                    cmd.Parameters.Add("@IsActualDate", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["IsActualDate"]);
                    cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["DOB"]);
                    cmd.Parameters.Add("@MTHLY_INCOME", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["MTHLY_INCOME"]);
                    cmd.Parameters.Add("@OCCUPATION", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["OCCUPATION"]);
                    cmd.Parameters.Add("@DEPENT_ON_EMP", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["DEPENT_ON_EMP"].ToString() == "1" ? "1" : "0");
                    cmd.Parameters.Add("@STAY_WITH_EMP", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["STAY_WITH_EMP"].ToString() == "1" ? "1" : "0");
                    cmd.Parameters.Add("@SPOUSE_WORKING", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["SPOUSE_WORKING"].ToString() == "1" ? "1" : "0");
                    cmd.Parameters.Add("@SPOUSE_WORKING_IN", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["SPOUSE_WORKING_IN"]);
                    cmd.Parameters.Add("@SPOUSE_MEDICAL_FROM_OFFICE", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["SPOUSE_MEDICAL_FROM_OFFICE"]);
                    cmd.Parameters.Add("@SPOUSE_LTC_FROM_OFFICE", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["SPOUSE_LTC_FROM_OFFICE"]);
                    cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = txtEMPCode.Text;
                    con.Open();
                    k = cmd.ExecuteNonQuery();
                    con.Close();

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data Save Successfully Done...!! ');", true);
                BindUserDetails(txtEMPCode.Text);
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
      return k=0;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddnominee_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "GetAllDetailsByEMPCODE1";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = txtEMPCode.Text;
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        {
            Response.Redirect("NomineeDetails.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage1", "alertMessage1();", true);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnreport_Click(object sender, EventArgs e)
    {
        string value = txtEMPCode.Text + "~" + 1;
        ScriptManager.RegisterStartupScript(this, GetType(), "Fetch", "Fetch('" + value + @"');", true);

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GD_FamilyDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Attach")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GD_FamilyDetails.Rows[index];
            TextBox lblFML_MEMBER_NAME = (TextBox)row.FindControl("lblFML_MEMBER_NAME");
            if (lblFML_MEMBER_NAME.Text != "")
            {
                var Values = "UploadAttachment.aspx?EMP=" + txtEMPCode.Text + "&Name=" + lblFML_MEMBER_NAME.Text;
                ScriptManager.RegisterStartupScript(this, GetType(), "UplaodDocs", "UplaodDocs('" + Values + @"');", true);

            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    private void SendEmail(string Message, string subject, string mailto)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("192.168.2.29");
            mail.From = new MailAddress("donotreply@icsi.edu");
            mail.To.Add(mailto);
            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["Email"])) && Convert.ToString(ViewState["Email"]) != "")
            {
                mail.CC.Add(Convert.ToString(ViewState["Email"]));
            }
            mail.Subject = subject;
            mail.Body = Message;
            SmtpServer.Credentials = new System.Net.NetworkCredential("donotreply@icsi.edu", "password@123", "ICSINOIDA");
            SmtpServer.Send(mail);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnpreview_Click(object sender, EventArgs e)
    {
        string value = txtEMPCode.Text + "~" + 1;
        ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlREL_TYPE_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        DropDownList drp = (DropDownList)sender;
        GridViewRow row = (GridViewRow)drp.NamingContainer;
        int rowindex = row.RowIndex;
        DropDownList ddlREL_TYPE_ID = (DropDownList)GD_FamilyDetails.Rows[rowindex].FindControl("ddlREL_TYPE_ID");
        TextBox lblFML_MEMBER_NAME = (TextBox)GD_FamilyDetails.Rows[rowindex].FindControl("lblFML_MEMBER_NAME");
      //  TextBox TxtotherRel = (TextBox)GD_FamilyDetails.Rows[rowindex].FindControl("TxtotherRel"); 
        DropDownList ddlchkdob = (DropDownList)GD_FamilyDetails.Rows[rowindex].FindControl("ddlchkdob");
        TextBox dobcal = (TextBox)GD_FamilyDetails.Rows[rowindex].FindControl("dobcal");
        //if (ddlREL_TYPE_ID.SelectedItem.Text == "Other")
       // {
         //   TxtotherRel.Visible = true;             
       // }
       // else
        //{
          //  TxtotherRel.Visible = false; 
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con1;
            cmd1.CommandText = "GetRel_TypeMSTByRelation";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@EMPCode", SqlDbType.NVarChar).Value = txtEMPCode.Text;
            cmd1.Parameters.Add("@Form", SqlDbType.NVarChar).Value = "Family Particulars";
            cmd1.Parameters.Add("@Relation", SqlDbType.NVarChar).Value = ddlREL_TYPE_ID.SelectedItem.Text;
            con1.Open();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            con1.Close();
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                lblFML_MEMBER_NAME.Text = Convert.ToString(dt1.Rows[0]["FML_MEMBER_NAME"]);
                dobcal.Text = Convert.ToString(dt1.Rows[0]["DOB"]);
                ddlchkdob.ClearSelection();
                ddlchkdob.Items.FindByText(Convert.ToString(dt1.Rows[0]["IsActualDate"])).Selected = true;
            }
            else
            {
                // lblFML_MEMBER_NAME.Text = string.Empty;
            }
       // }
    }

    public string onclick { get; set; }
}
