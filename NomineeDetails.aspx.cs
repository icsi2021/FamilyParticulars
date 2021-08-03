using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
public partial class NomineeDetails : System.Web.UI.Page
{
    string EMPCode = string.Empty;
    /// <summary>
    /// Datatable for dtCode..
    /// </summary>
    DataTable dtCode = null;
    /// <summary>ḍḍ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                ViewState["EMPCode"] = EMPCode;
                dtCode = GetEmployeeByID(EMPCode);
                ViewState["dtCode"] = dtCode.Rows.Count;
                if (dtCode != null && dtCode.Rows.Count > 0)
                {
                    if (dtCode.Rows[0]["IsEdit"].ToString() == "1")
                    {
                        BindUserDetailsProvident(EMPCode);
                        BindUserDetailsPension(EMPCode);
                        BindUserDetailsNewPension(EMPCode);
                        BindUserDetailsGratuity(EMPCode);
                        BindUserDetailsEncashment(EMPCode);
                        BindUserDetailsBenevolent(EMPCode);
                        GridViewNewPensionTrust.Enabled = false;
                   // btnreset.Enabled = false;
                        btnsubmit.Enabled = false;
                        GD_BenevolentFund.Enabled = false;
                        GD_EmployeePensionTrust.Enabled = false;
                        GD_Encashment.Enabled = false;
                        GD_Gratuity.Enabled = false;
                        GD_ProvidentFund.Enabled = false;
                             if (Convert.ToInt32(dtCode.Rows[0]["FN_YR"].ToString().Split('-')[2]) < 2005)
                        {
                            DivEmployeePensionTrust.Visible = false;
                            DivProvidentFund.Visible = true;
                        }
                        else
                        {
                            DivEmployeePensionTrust.Visible = true;
                            DivProvidentFund.Visible = false;
                        }
                    }
                    else
                    {
                        BindUserDetailsProvident(EMPCode);
                        BindUserDetailsPension(EMPCode);
                        BindUserDetailsGratuity(EMPCode);
                        BindUserDetailsEncashment(EMPCode);
                        BindUserDetailsBenevolent(EMPCode);
                        BindUserDetailsNewPension(EMPCode);
                        // btnreset.Enabled = true;
                        btnsubmit.Enabled = true;
                        GD_BenevolentFund.Enabled = true;
                        GD_EmployeePensionTrust.Enabled = true;
                        GD_Encashment.Enabled = true;
                        GD_Gratuity.Enabled = true;
                        GD_ProvidentFund.Enabled = true;
                        GridViewNewPensionTrust.Enabled = true;
                        if (Convert.ToInt32(dtCode.Rows[0]["FN_YR"].ToString().Split('-')[2]) < 2005)
                        {
                            DivEmployeePensionTrust.Visible = false;
                            DivProvidentFund.Visible = true;
                        }
                        else
                        {
                            DivEmployeePensionTrust.Visible = true;
                            DivProvidentFund.Visible = false;
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnreset_Click(object sender, EventArgs e)
    {
        // Response.Redirect("NomineeDetails.aspx");
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="CODE"></param>
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
    /// 
    /// </summary>
    /// <param name="ddlREL_TYPE_ID"></param>
    public void FillDropDownList(DropDownList ddl,string Form)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd1 = new SqlCommand();
        cmd1.Connection = con1;
        cmd1.CommandText = "GetRel_TypeMST1";
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@EMPCode", SqlDbType.NVarChar).Value = ViewState["EMPCode"].ToString();
        cmd1.Parameters.Add("@Form", SqlDbType.NVarChar).Value = Form;
        con1.Open();
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.Fill(dt1);
        con1.Close();
        ddl.ClearSelection();
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            ddl.DataSource = dt1;
            ddl.DataTextField = "FML_MEMBER_NAME";
            ddl.DataValueField = "RowNumber";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DataTable GetRowData(string FML_MEMBER_NAME, string Empcode)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetRowIDDetail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = Empcode;
            cmd.Parameters.Add("@FML_MEMBER_NAME", SqlDbType.NVarChar).Value = FML_MEMBER_NAME;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return dt;
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
            if (!string.IsNullOrEmpty(Convert.ToString(Session["Email"])) && Convert.ToString(Session["Email"]) != "")
            {
                mail.CC.Add(Convert.ToString(Session["Email"]));
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
    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        if (ChkConfirm.Checked == true)
        {
            int k = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = null;
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Proc_FamilyParticularStatusChange";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@aa", SqlDbType.NVarChar).Value = 2;
                cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"];
                con.Open();
                k = cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Click CheckBox');", true);
            return;
        }
    }

    #region Provident
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GD_ProvidentFundtxtname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            DataTable dt = null;
            DropDownList lb = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DropDownList GD_ProvidentFundtxtname = (DropDownList)GD_ProvidentFund.Rows[rowID].Cells[1].FindControl("GD_ProvidentFundtxtname");
            if (GD_ProvidentFund.Rows.Count > 0)
            {
                for (int i = 0; i < GD_ProvidentFund.Rows.Count; i++)
                {
                    if (i != rowID)
                    {
                        DropDownList GD_ProvidentFundtxtnamevalue = (DropDownList)GD_ProvidentFund.Rows[i].Cells[1].FindControl("GD_ProvidentFundtxtname");
                        TextBox GD_ProvidentFund_txtrelationship = (TextBox)GD_ProvidentFund.Rows[rowID].Cells[2].FindControl("txtrelationship");
                        TextBox GD_ProvidentFund_dobcal = (TextBox)GD_ProvidentFund.Rows[rowID].Cells[3].FindControl("dobcal");
                        DropDownList GD_ProvidentFund_chkisminor = (DropDownList)GD_ProvidentFund.Rows[rowID].Cells[4].FindControl("chkisminor");
                        TextBox GD_ProvidentFund_txtnameofgurdian = (TextBox)GD_ProvidentFund.Rows[rowID].Cells[5].FindControl("txtnameofgurdian");
                        TextBox GD_ProvidentFund_txtpercentage = (TextBox)GD_ProvidentFund.Rows[rowID].Cells[6].FindControl("txtpercentage");
                        if ((GD_ProvidentFundtxtnamevalue.SelectedItem.Text == GD_ProvidentFundtxtname.SelectedItem.Text))
                        {
                            flag = true;
                            GD_ProvidentFund_txtrelationship.Text = string.Empty;
                            GD_ProvidentFund_dobcal.Text = string.Empty;
                            GD_ProvidentFund_chkisminor.ClearSelection();
                            GD_ProvidentFund_chkisminor.Items.FindByValue("0").Selected = true;
                            GD_ProvidentFund_txtnameofgurdian.Text = string.Empty;
                            GD_ProvidentFund_txtpercentage.Text = "0";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Notification();", true);
                        }
                    }
                }
            }
            if (!flag)
            {
                Label lblrowprovident = (Label)GD_ProvidentFund.Rows[rowID].Cells[0].FindControl("lblrowprovident");
                TextBox GD_ProvidentFund_txtrelationship = (TextBox)GD_ProvidentFund.Rows[rowID].Cells[2].FindControl("txtrelationship");
                TextBox GD_ProvidentFund_dobcal = (TextBox)GD_ProvidentFund.Rows[rowID].Cells[3].FindControl("dobcal");
                DropDownList GD_ProvidentFund_chkisminor = (DropDownList)GD_ProvidentFund.Rows[rowID].Cells[4].FindControl("chkisminor");
                TextBox GD_ProvidentFund_txtnameofgurdian = (TextBox)GD_ProvidentFund.Rows[rowID].Cells[5].FindControl("txtnameofgurdian");
                TextBox GD_ProvidentFund_txtpercentage = (TextBox)GD_ProvidentFund.Rows[rowID].Cells[6].FindControl("txtpercentage");
                dt = GetRowData(GD_ProvidentFundtxtname.SelectedItem.Text, ViewState["EMPCode"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    GD_ProvidentFundtxtname.ClearSelection();
                    //Assign the value from DataTable to the TextBox  Provident Fund  
                    lblrowprovident.Text = Convert.ToString(dt.Rows[0]["RowNumber"]);
                    GD_ProvidentFundtxtname.Items.FindByText(Convert.ToString(dt.Rows[0]["FML_MEMBER_NAME"])).Selected = true;
                    GD_ProvidentFund_txtrelationship.Text = Convert.ToString(dt.Rows[0]["REL_TYPE_ID"]);
                    GD_ProvidentFund_dobcal.Text = Convert.ToString(dt.Rows[0]["DOB"]);






                    //if (Convert.ToString(dt.Rows[0]["IsShowHideProvident"]) == "0")
                    //{
                    //    //if (Convert.ToString(dt.Rows[0]["ProvidentIsMinor"]) == "1")
                    //    //{ GD_ProvidentFund_chkisminor.Checked = true; }
                    //    GD_ProvidentFund_chkisminor.ClearSelection();
                    //    GD_ProvidentFund_chkisminor.Items.FindByValue(Convert.ToString(dt.Rows[0]["ProvidentIsMinor"])).Selected = true;
                    //    GD_ProvidentFund_txtnameofgurdian.Text = Convert.ToString(dt.Rows[0]["ProvidentGurdian"]);
                    //    GD_ProvidentFund_txtpercentage.Text = Convert.ToString(dt.Rows[0]["ProvidentShare"]);
                    //}
                }
                // ViewState["CurrentTableProvident"] = dt;
                ViewProvident();
                //UpdateProvident.Update();
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
    protected void btnimagedeleteProvident_Click(object sender, EventArgs e)
    {
        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        DataTable dt = null;
        if (ViewState["CurrentTableProvident"] != null)
        {
            Label labelrow = (Label)GD_ProvidentFund.Rows[rowID].Cells[0].FindControl("lblrowprovident");
            dt = (DataTable)ViewState["CurrentTableProvident"];
            if (labelrow.Text == "0")
            {
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count-1)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        //ResetRowID(dt);
                    }
                }
            }
            else
            {
                ViewProvident();
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "DeleteRow";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"].ToString();
                    cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(labelrow.Text);
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "2";
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertDelete", "alertDelete();", true);
                        EMPCode = Convert.ToString(Session["UserName"]);
                        BindUserDetailsProvident(EMPCode);
                     //   Response.Redirect("NomineeDetails.aspx");
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
        //Store the current data in ViewState for future reference  
        ViewState["CurrentTableProvident"] = dt;
        //Re bind the GridView for the updated data  
        GD_ProvidentFund.DataSource = dt;
        GD_ProvidentFund.DataBind();
        //Set Previous Data on Postbacks  
        SetPreviousDataProvident();
        // UpdateProvident.Update();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnAddProvident_Click(object sender, EventArgs e)
    {
        if (ViewState["CurrentTableProvident"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableProvident"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = 0;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["CurrentTableProvident"] = dtCurrentTable;
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    Label lblrowprovident = (Label)GD_ProvidentFund.Rows[i].Cells[0].FindControl("lblrowprovident");
                    DropDownList GD_ProvidentFund_txtname = (DropDownList)GD_ProvidentFund.Rows[i].Cells[1].FindControl("GD_ProvidentFundtxtname");
                    TextBox GD_ProvidentFund_txtrelationship = (TextBox)GD_ProvidentFund.Rows[i].Cells[2].FindControl("txtrelationship");
                    TextBox GD_ProvidentFund_dobcal = (TextBox)GD_ProvidentFund.Rows[i].Cells[3].FindControl("dobcal");
                    DropDownList GD_ProvidentFund_chkisminor = (DropDownList)GD_ProvidentFund.Rows[i].Cells[4].FindControl("chkisminor");
                    TextBox GD_ProvidentFund_txtnameofgurdian = (TextBox)GD_ProvidentFund.Rows[i].Cells[5].FindControl("txtnameofgurdian");
                    TextBox GD_ProvidentFund_txtpercentage = (TextBox)GD_ProvidentFund.Rows[i].Cells[6].FindControl("txtpercentage");
                    dtCurrentTable.Rows[i]["RowNumber"] = lblrowprovident.Text = "0";
                    dtCurrentTable.Rows[i]["FML_MEMBER_NAME"] = GD_ProvidentFund_txtname.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["REL_TYPE_ID"] = GD_ProvidentFund_txtrelationship.Text;
                    dtCurrentTable.Rows[i]["DOB"] = GD_ProvidentFund_dobcal.Text;
                    dtCurrentTable.Rows[i]["ProvidentShare"] = GD_ProvidentFund_txtpercentage.Text;
                    dtCurrentTable.Rows[i]["ProvidentIsMinor"] = GD_ProvidentFund_chkisminor.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["ProvidentGurdian"] = GD_ProvidentFund_txtnameofgurdian.Text;
                    dtCurrentTable.Rows[i]["EMP_CODE"] = ViewState["EMPCode"].ToString();
                }
                //Rebind the Grid with the current data to reflect changes   
                GD_ProvidentFund.DataSource = dtCurrentTable;
                GD_ProvidentFund.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks   
        SetPreviousDataProvident();
        // UpdateProvident.Update();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GD_ProvidentFund_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["CurrentTableProvident"];
                ImageButton lb = (ImageButton)e.Row.FindControl("btnimagedeleteProvident");
                DropDownList ddlREL_TYPE_ID = (DropDownList)e.Row.FindControl("GD_ProvidentFundtxtname");
                FillDropDownList(ddlREL_TYPE_ID, "Provident Fund");
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
                    if (dt.Rows[0]["IsApproved"].ToString() == "0")
                    {
                        lb.Enabled = true;
                    }
                    else
                    {
                        lb.Enabled = false;
                    }
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
            finally
            {
                TextBox txtnameofgurdian = (TextBox)e.Row.FindControl("txtnameofgurdian");
                txtnameofgurdian.Enabled = false;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkisminor_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList chkisminor = (DropDownList)sender;
        GridViewRow gvRow = (GridViewRow)chkisminor.NamingContainer;
        int rowID = gvRow.RowIndex;
        DropDownList chk = (DropDownList)GD_ProvidentFund.Rows[rowID].FindControl("chkisminor");
        TextBox txtnameofgurdian = (TextBox)GD_ProvidentFund.Rows[rowID].FindControl("txtnameofgurdian");
        if (chk.SelectedValue == "1")
        {
            txtnameofgurdian.Enabled = true;
        }
        else
        {
            txtnameofgurdian.Enabled = false;
            txtnameofgurdian.Text = string.Empty;
        }
        // UpdateProvident.Update();
    }
    /// <summary>
    /// 
    /// </summary>
    public void ViewProvident()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableProvident"];
        int rowIndex = 0;
        if (dtCurrentTable.Rows.Count > 0)
        {
            for (int i = 1; i <= GD_ProvidentFund.Rows.Count; i++)
            {
                // Provident
                Label lblrowprovident = (Label)GD_ProvidentFund.Rows[rowIndex].Cells[0].FindControl("lblrowprovident");
                DropDownList GD_ProvidentFund_txtname = (DropDownList)GD_ProvidentFund.Rows[rowIndex].Cells[1].FindControl("GD_ProvidentFundtxtname");
                TextBox GD_ProvidentFund_txtrelationship = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                TextBox GD_ProvidentFund_dobcal = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[3].FindControl("dobcal");
                DropDownList GD_ProvidentFund_chkisminor = (DropDownList)GD_ProvidentFund.Rows[rowIndex].Cells[4].FindControl("chkisminor");
                TextBox GD_ProvidentFund_txtnameofgurdian = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[5].FindControl("txtnameofgurdian");
                TextBox GD_ProvidentFund_txtpercentage = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[6].FindControl("txtpercentage");
                if (lblrowprovident.Text == "")
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = "0";
                }
                else
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = Convert.ToString(lblrowprovident.Text);
                }
                dtCurrentTable.Rows[i - 1]["FML_MEMBER_NAME"] = GD_ProvidentFund_txtname.SelectedItem.Text;
                dtCurrentTable.Rows[i - 1]["REL_TYPE_ID"] = Convert.ToString(GD_ProvidentFund_txtrelationship.Text);
                dtCurrentTable.Rows[i - 1]["DOB"] = Convert.ToString(GD_ProvidentFund_dobcal.Text);
                dtCurrentTable.Rows[i - 1]["ProvidentShare"] = Convert.ToString(GD_ProvidentFund_txtpercentage.Text);
                dtCurrentTable.Rows[i - 1]["EMP_CODE"] = ViewState["EMPCode"].ToString();
                dtCurrentTable.Rows[i - 1]["ProvidentIsMinor"] = GD_ProvidentFund_chkisminor.SelectedItem.Text;
                dtCurrentTable.Rows[i - 1]["ProvidentGurdian"] = Convert.ToString(GD_ProvidentFund_txtnameofgurdian.Text);
                rowIndex++;
            }
            ViewState["CurrentTableProvident"] = dtCurrentTable;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Code"></param>
    public void BindUserDetailsProvident(string Code)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetAllDetailsByEMPCODEProvident";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = Code;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IsApproved"].ToString() == "0")
                {
                    btnprovidentprint.Enabled = false;
                    BtnAddProvident.Enabled = true;
                }
                BtnAddProvident.Enabled = false;
                ViewState["CurrentTableProvident"] = ds.Tables[0];
                GD_ProvidentFund.DataSource = ds.Tables[0];
                GD_ProvidentFund.DataBind();
                int rowIndex = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        Label lblrowprovident = (Label)GD_ProvidentFund.Rows[rowIndex].Cells[0].FindControl("lblrowprovident");
                        DropDownList GD_ProvidentFund_txtname = (DropDownList)GD_ProvidentFund.Rows[rowIndex].Cells[1].FindControl("GD_ProvidentFundtxtname");
                        TextBox GD_ProvidentFund_txtrelationship = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_ProvidentFund_dobcal = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        DropDownList GD_ProvidentFund_chkisminor = (DropDownList)GD_ProvidentFund.Rows[rowIndex].Cells[4].FindControl("chkisminor");
                        TextBox GD_ProvidentFund_txtnameofgurdian = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[5].FindControl("txtnameofgurdian");
                        TextBox GD_ProvidentFund_txtpercentage = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[6].FindControl("txtpercentage");
                        GD_ProvidentFund_txtname.ClearSelection();
                        //Assign the value from DataTable to the TextBox  Provident Fund  
                        lblrowprovident.Text = Convert.ToString(ds.Tables[0].Rows[i]["RowNumber"]);
                        GD_ProvidentFund_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_ProvidentFund_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                        GD_ProvidentFund_dobcal.Text = Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                        //if (Convert.ToString(ds.Tables[0].Rows[i]["ProvidentIsMinor"]) == "1")
                        //{ GD_ProvidentFund_chkisminor.Checked = true; }
                        GD_ProvidentFund_chkisminor.ClearSelection();
                        GD_ProvidentFund_chkisminor.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[i]["ProvidentIsMinor"])).Selected = true;
                        GD_ProvidentFund_txtnameofgurdian.Text = Convert.ToString(ds.Tables[0].Rows[i]["ProvidentGurdian"]);
                        GD_ProvidentFund_txtpercentage.Text = Convert.ToString(ds.Tables[0].Rows[i]["ProvidentShare"]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[rowIndex]["ProvidentShare"]) != "0")
                    {
                        Label lblrowprovident = (Label)GD_ProvidentFund.Rows[rowIndex].Cells[0].FindControl("lblrowprovident");
                        DropDownList GD_ProvidentFund_txtname = (DropDownList)GD_ProvidentFund.Rows[rowIndex].Cells[1].FindControl("GD_ProvidentFundtxtname");
                        TextBox GD_ProvidentFund_txtrelationship = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_ProvidentFund_dobcal = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        DropDownList GD_ProvidentFund_chkisminor = (DropDownList)GD_ProvidentFund.Rows[rowIndex].Cells[4].FindControl("chkisminor");
                        TextBox GD_ProvidentFund_txtnameofgurdian = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[5].FindControl("txtnameofgurdian");
                        TextBox GD_ProvidentFund_txtpercentage = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[6].FindControl("txtpercentage");
                        GD_ProvidentFund_txtname.ClearSelection();
                        //Assign the value from DataTable to the TextBox  Provident Fund  
                        lblrowprovident.Text = Convert.ToString(ds.Tables[0].Rows[i]["RowNumber"]);
                        GD_ProvidentFund_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_ProvidentFund_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                        GD_ProvidentFund_dobcal.Text = Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                        //if (Convert.ToString(ds.Tables[0].Rows[i]["ProvidentIsMinor"]) == "1")
                        //{ GD_ProvidentFund_chkisminor.Checked = true; }
                        GD_ProvidentFund_chkisminor.ClearSelection();
                        GD_ProvidentFund_chkisminor.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[i]["ProvidentIsMinor"])).Selected = true;
                        GD_ProvidentFund_txtnameofgurdian.Text = Convert.ToString(ds.Tables[0].Rows[i]["ProvidentGurdian"]);
                        GD_ProvidentFund_txtpercentage.Text = Convert.ToString(ds.Tables[0].Rows[i]["ProvidentShare"]);
                    }
                    rowIndex++;
                }
            }
            else
            {
                SetInitialRowProvident();
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
    private void SetInitialRowProvident()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("FML_MEMBER_NAME", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("REL_TYPE_ID", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("DOB", typeof(string)));//for TextBox value         
        dt.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));//for TextBox value        
        dt.Columns.Add(new DataColumn("ProvidentShare", typeof(string)));//for TextBox value  
        dt.Columns.Add(new DataColumn("ProvidentIsMinor", typeof(string)));//for TextBox value  
        dt.Columns.Add(new DataColumn("ProvidentGurdian", typeof(string)));//for TextBox value  


        dr = dt.NewRow();
        dr["RowNumber"] = 0;
        dr["FML_MEMBER_NAME"] = string.Empty;
        dr["REL_TYPE_ID"] = string.Empty;
        dr["DOB"] = string.Empty;
        dr["EMP_CODE"] = string.Empty;
        dr["ProvidentShare"] = string.Empty;
        dr["ProvidentIsMinor"] = string.Empty;
        dr["ProvidentGurdian"] = string.Empty;

        dt.Rows.Add(dr);
        ViewState["CurrentTableProvident"] = dt;
        GD_ProvidentFund.DataSource = dt;
        GD_ProvidentFund.DataBind();
        //Store the DataTable in ViewState for future reference   

    }
    /// <summary>
    /// 
    /// </summary>
    private void SetPreviousDataProvident()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableProvident"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableProvident"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Provident
                    Label lblrowprovident = (Label)GD_ProvidentFund.Rows[rowIndex].Cells[0].FindControl("lblrowprovident");
                    DropDownList GD_ProvidentFund_txtname = (DropDownList)GD_ProvidentFund.Rows[rowIndex].Cells[1].FindControl("GD_ProvidentFundtxtname");
                    TextBox GD_ProvidentFund_txtrelationship = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                    TextBox GD_ProvidentFund_dobcal = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[3].FindControl("dobcal");
                    DropDownList GD_ProvidentFund_chkisminor = (DropDownList)GD_ProvidentFund.Rows[rowIndex].Cells[4].FindControl("chkisminor");
                    TextBox GD_ProvidentFund_txtnameofgurdian = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[5].FindControl("txtnameofgurdian");
                    TextBox GD_ProvidentFund_txtpercentage = (TextBox)GD_ProvidentFund.Rows[rowIndex].Cells[6].FindControl("txtpercentage");

                    if (i < dt.Rows.Count - 1)
                    {
                        //Assign the value from DataTable to the TextBox  Provident Fund  
                        lblrowprovident.Text = Convert.ToString(dt.Rows[i]["RowNumber"]);
                        GD_ProvidentFund_txtname.Items.FindByText(Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_ProvidentFund_txtrelationship.Text = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        GD_ProvidentFund_dobcal.Text = Convert.ToString(dt.Rows[i]["DOB"]);
                        //if (Convert.ToString(dt.Rows[i]["ProvidentIsMinor"]) == "1")
                        //{ GD_ProvidentFund_chkisminor.Checked = true; }
                        GD_ProvidentFund_chkisminor.ClearSelection();
                        GD_ProvidentFund_chkisminor.Items.FindByValue(Convert.ToString(dt.Rows[i]["ProvidentIsMinor"])).Selected = true;
                        GD_ProvidentFund_txtnameofgurdian.Text = Convert.ToString(dt.Rows[i]["ProvidentGurdian"]);
                        GD_ProvidentFund_txtpercentage.Text = Convert.ToString(dt.Rows[i]["ProvidentShare"]);
                    }
                    rowIndex++;
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void txtpercentage_TextChanged(object sender, EventArgs e)
    //{
    //    int total = 0;
    //    TextBox lb = (TextBox)sender;
    //    GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
    //    int rowID = gvRow.RowIndex;
    //    TextBox txttotal = (TextBox)GD_ProvidentFund.Rows[rowID].Cells[6].FindControl("txtpercentage");
    //    for (int i = 0; i < GD_ProvidentFund.Rows.Count; i++)
    //    {
    //        TextBox GD_ProvidentFund_txtpercentage = (TextBox)GD_ProvidentFund.Rows[i].Cells[6].FindControl("txtpercentage");
    //        if (GD_ProvidentFund_txtpercentage.Text != "" && GD_ProvidentFund_txtpercentage.Text != "0")
    //        {
    //            total = total + Convert.ToInt32(GD_ProvidentFund_txtpercentage.Text != "" ? GD_ProvidentFund_txtpercentage.Text : "0");
    //        }
    //    }
    //    if (total == 100)
    //    {

    //    }
    //    else
    //    {
    //        txttotal.Text = "";
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "PercentageMessage();", true);
    //    }
    //}    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnprovidnet_Click(object sender, EventArgs e)
    {
        int val = 0;
        if (GD_ProvidentFund.Rows.Count > 0)
        {
            for (int i = 0; i < GD_ProvidentFund.Rows.Count; i++)
            {                                
                TextBox GD_ProvidentFund_txtpercentage = (TextBox)GD_ProvidentFund.Rows[i].Cells[6].FindControl("txtpercentage");
                DropDownList chk = (DropDownList)GD_ProvidentFund.Rows[i].Cells[4].FindControl("chkisminor");
                TextBox txtnameofgurdian = (TextBox)GD_ProvidentFund.Rows[i].Cells[5].FindControl("txtnameofgurdian");
                if ((GD_ProvidentFund_txtpercentage.Text == "" || GD_ProvidentFund_txtpercentage.Text == "0") || (chk.SelectedValue == "1" && txtnameofgurdian.Text == ""))
                {
                    val = 1;
                }
            }
            if (val == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessageSave();", true);
                return;
            }
            else
            {
                int total = 0; 
                for (int i = 0; i < GD_ProvidentFund.Rows.Count; i++)
                {
                    TextBox GD_ProvidentFund_txtpercentage = (TextBox)GD_ProvidentFund.Rows[i].Cells[6].FindControl("txtpercentage");
                    if (GD_ProvidentFund_txtpercentage.Text != "" && GD_ProvidentFund_txtpercentage.Text != "0")
                    {
                        total = total + Convert.ToInt32(GD_ProvidentFund_txtpercentage.Text != "" ? GD_ProvidentFund_txtpercentage.Text : "0");
                    }
                }
                if (total == 100)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "PercentageMessage();", true);
                    return;
                }

                ViewProvident();
                int m = SaveItemDetailsProvident((DataTable)ViewState["CurrentTableProvident"]);
                if (m > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
                    string value = ViewState["EMPCode"].ToString() + "~" + 2;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
                }
            }
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public int SaveItemDetailsProvident(DataTable dt)
    {
        int k = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = null;
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProvidentShare"])) && Convert.ToString(dt.Rows[i]["ProvidentShare"]) != "0")
                    {
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "InsertUpdateNominee";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[i]["RowNumber"]);
                        cmd.Parameters.Add("@FML_MEMBER_NAME", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]);
                        cmd.Parameters.Add("@REL_TYPE_ID", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["DOB"]);

                       // cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"];

                        cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["EMP_CODE"]);                        
                        cmd.Parameters.Add("@ProvidentGurdian", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["ProvidentGurdian"]);
                        cmd.Parameters.Add("@ProvidentIsMinor", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["ProvidentIsMinor"]);
                        cmd.Parameters.Add("@ProvidentShare", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["ProvidentShare"]);
                        
                       
                        
                       
                        con.Open();
                        k = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //lblError.Text = ex.Message.ToString();
        }
        if (k > 0)
        {
            string Message = "Mr./Ms. Gaurav Mehta,The Family Particular of Mr./Mrs." + Convert.ToString(Session["Name"]) + @" has been sent for approval.";
            //SendEmail(Message, "Submission Provident Share", "Gaurav.mehta@icsi.edu");
        }
        return k;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnprovidentprint_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 2;
        ScriptManager.RegisterStartupScript(this, GetType(), "Fetch", "Fetch('" + value + @"');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnprovidentpreview_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 2;
        ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnprovidentrefresh_Click(object sender, EventArgs e)
    {
        BindUserDetailsProvident(ViewState["EMPCode"].ToString());
    }
    #endregion

    #region Pension
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GD_EmployeePensionTrusttxtname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            DataTable dt = null;
            DropDownList lb = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DropDownList GD_EmployeePensionTrusttxtname = (DropDownList)GD_EmployeePensionTrust.Rows[rowID].Cells[1].FindControl("GD_EmployeePensionTrusttxtname");
            if (GD_EmployeePensionTrust.Rows.Count > 0)
            {
                for (int i = 0; i < GD_EmployeePensionTrust.Rows.Count; i++)
                {
                    if (i != rowID)
                    {
                        DropDownList GD_EmployeePensionTrusttxtnamevalue = (DropDownList)GD_EmployeePensionTrust.Rows[i].Cells[1].FindControl("GD_EmployeePensionTrusttxtname");
                        TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GD_EmployeePensionTrust.Rows[rowID].Cells[2].FindControl("txtrelationship");
                        TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GD_EmployeePensionTrust.Rows[rowID].Cells[3].FindControl("dobcal");
                        TextBox GD_EmployeePensionTrust_txtpercentagePension = (TextBox)GD_EmployeePensionTrust.Rows[rowID].Cells[4].FindControl("txtpercentagePension");
                        if ((GD_EmployeePensionTrusttxtnamevalue.SelectedItem.Text == GD_EmployeePensionTrusttxtname.SelectedItem.Text))
                        {
                            flag = true;

                            GD_EmployeePensionTrust_txtrelationship.Text = string.Empty;
                            GD_EmployeePensionTrust_dobcal.Text = string.Empty;
                            GD_EmployeePensionTrust_txtpercentagePension.Text = "0";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Notification();", true);
                        }
                    }
                }
            }
            if (!flag)
            {
                Label lblrowpension = (Label)GD_EmployeePensionTrust.Rows[rowID].Cells[0].FindControl("lblrowpension");
                TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GD_EmployeePensionTrust.Rows[rowID].Cells[2].FindControl("txtrelationship");
                TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GD_EmployeePensionTrust.Rows[rowID].Cells[3].FindControl("dobcal");
                TextBox GD_EmployeePensionTrust_txtpercentagePension = (TextBox)GD_EmployeePensionTrust.Rows[rowID].Cells[4].FindControl("txtpercentagePension");
                dt = GetRowData(GD_EmployeePensionTrusttxtname.SelectedItem.Text, ViewState["EMPCode"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    GD_EmployeePensionTrusttxtname.ClearSelection();
                    //Assign the value from DataTable to the TextBox  Provident Fund  
                    lblrowpension.Text = Convert.ToString(dt.Rows[0]["RowNumber"]);
                    GD_EmployeePensionTrusttxtname.Items.FindByText(Convert.ToString(dt.Rows[0]["FML_MEMBER_NAME"])).Selected = true;
                    GD_EmployeePensionTrust_txtrelationship.Text = Convert.ToString(dt.Rows[0]["REL_TYPE_ID"]);
                    GD_EmployeePensionTrust_dobcal.Text = Convert.ToString(dt.Rows[0]["DOB"]);
                    if (Convert.ToString(dt.Rows[0]["IsShowHidePension"]) == "0")
                    {
                        GD_EmployeePensionTrust_txtpercentagePension.Text = Convert.ToString(dt.Rows[0]["PensionShare"]);
                    }
                }
                // ViewState["CurrentTableProvident"] = dt;
                ViewPension();
                //UpdateProvident.Update();
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
    protected void GD_EmployeePensionTrust_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["CurrentTablePension"];
                ImageButton lb = (ImageButton)e.Row.FindControl("btnimagedeletePensiontrust");
                DropDownList ddlREL_TYPE_ID = (DropDownList)e.Row.FindControl("GD_EmployeePensionTrusttxtname");
                FillDropDownList(ddlREL_TYPE_ID, "Pension Fund Trust");
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
                    if (dt.Rows[0]["IsApproved"].ToString() == "0")
                    {
                        lb.Enabled = true;
                    }
                    else
                    {
                        lb.Enabled = false;
                    }
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnimagedeletePensiontrust_Click(object sender, EventArgs e)
    {
        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        DataTable dt = null;
        if (ViewState["CurrentTablePension"] != null)
        {
            Label labelrow = (Label)GD_EmployeePensionTrust.Rows[rowID].Cells[0].FindControl("lblrowpension");
            dt = (DataTable)ViewState["CurrentTablePension"];
            if (labelrow.Text == "0")
            {
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        //ResetRowID(dt);

                    }
                }
            }
            else
            {
                ViewPension();
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "DeleteRow";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"].ToString();
                    cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(labelrow.Text);
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "3";
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertDelete", "alertDelete();", true);
                        BindUserDetailsPension(EMPCode);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
        //Store the current data in ViewState for future reference  
        ViewState["CurrentTablePension"] = dt;
        //Re bind the GridView for the updated data  
        GD_EmployeePensionTrust.DataSource = dt;
        GD_EmployeePensionTrust.DataBind();
        //Set Previous Data on Postbacks  
        SetPreviousDataPension();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnaddpensiontrust_Click(object sender, EventArgs e)
    {
        if (ViewState["CurrentTablePension"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTablePension"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = 0;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["CurrentTablePension"] = dtCurrentTable;
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    Label lblrowpension = (Label)GD_EmployeePensionTrust.Rows[i].Cells[0].FindControl("lblrowpension");
                    DropDownList GD_EmployeePensionTrust_txtname = (DropDownList)GD_EmployeePensionTrust.Rows[i].Cells[1].FindControl("GD_EmployeePensionTrusttxtname");
                    TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GD_EmployeePensionTrust.Rows[i].Cells[2].FindControl("txtrelationship");
                    TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GD_EmployeePensionTrust.Rows[i].Cells[3].FindControl("dobcal");
                    TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GD_EmployeePensionTrust.Rows[i].Cells[4].FindControl("txtpercentagePension");
                    dtCurrentTable.Rows[i]["RowNumber"] = lblrowpension.Text = "0";
                    dtCurrentTable.Rows[i]["FML_MEMBER_NAME"] = GD_EmployeePensionTrust_txtname.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["REL_TYPE_ID"] = GD_EmployeePensionTrust_txtrelationship.Text;
                    dtCurrentTable.Rows[i]["DOB"] = GD_EmployeePensionTrust_dobcal.Text;
                    dtCurrentTable.Rows[i]["PensionShare"] = GD_EmployeePensionTrust_txtpercentage.Text;
                    dtCurrentTable.Rows[i]["EMP_CODE"] = ViewState["EMPCode"].ToString();
                }
                //Rebind the Grid with the current data to reflect changes   
                GD_EmployeePensionTrust.DataSource = dtCurrentTable;
                GD_EmployeePensionTrust.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks   
        SetPreviousDataPension();
    }
    /// <summary>
    /// 
    /// </summary>
    public void ViewPension()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTablePension"];
        int rowIndex = 0;
        if (dtCurrentTable.Rows.Count > 0)
        {
            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            {

                // pension
                Label lblrowpension = (Label)GD_EmployeePensionTrust.Rows[rowIndex].Cells[0].FindControl("lblrowpension");
                DropDownList GD_EmployeePensionTrust_txtname = (DropDownList)GD_EmployeePensionTrust.Rows[rowIndex].Cells[1].FindControl("GD_EmployeePensionTrusttxtname");
                TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[3].FindControl("dobcal");
                TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[4].FindControl("txtpercentagePension");

                if (lblrowpension.Text == "")
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = "0";
                }
                else
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = Convert.ToString(lblrowpension.Text);
                }
                dtCurrentTable.Rows[i - 1]["FML_MEMBER_NAME"] = GD_EmployeePensionTrust_txtname.SelectedItem.Text;
                dtCurrentTable.Rows[i - 1]["REL_TYPE_ID"] = Convert.ToString(GD_EmployeePensionTrust_txtrelationship.Text);
                dtCurrentTable.Rows[i - 1]["DOB"] = Convert.ToString(GD_EmployeePensionTrust_dobcal.Text);
                dtCurrentTable.Rows[i - 1]["PensionShare"] = Convert.ToString(GD_EmployeePensionTrust_txtpercentage.Text);
                dtCurrentTable.Rows[i - 1]["EMP_CODE"] = ViewState["EMPCode"].ToString();
                rowIndex++;
            }
            ViewState["CurrentTablePension"] = dtCurrentTable;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Code"></param>
    public void BindUserDetailsPension(string Code)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetAllDetailsByEMPCODEPension";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = Code;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IsApproved"].ToString() == "0")
                {
                    btnpensionprint.Enabled = false;
                }
                ViewState["CurrentTablePension"] = ds.Tables[0];
                GD_EmployeePensionTrust.DataSource = ds.Tables[0];
                GD_EmployeePensionTrust.DataBind();
                int rowIndex = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        Label lblrowpension = (Label)GD_EmployeePensionTrust.Rows[rowIndex].Cells[0].FindControl("lblrowpension");
                        DropDownList GD_EmployeePensionTrust_txtname = (DropDownList)GD_EmployeePensionTrust.Rows[rowIndex].Cells[1].FindControl("GD_EmployeePensionTrusttxtname");
                        TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[4].FindControl("txtpercentagePension");

                        lblrowpension.Text = Convert.ToString(ds.Tables[0].Rows[i]["RowNumber"]);
                        GD_EmployeePensionTrust_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_EmployeePensionTrust_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                        GD_EmployeePensionTrust_dobcal.Text = Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["DOB"])).ToShortDateString();
                        GD_EmployeePensionTrust_txtpercentage.Text = Convert.ToString(ds.Tables[0].Rows[i]["PensionShare"]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[rowIndex]["PensionShare"]) != "0")
                    {
                        Label lblrowpension = (Label)GD_EmployeePensionTrust.Rows[rowIndex].Cells[0].FindControl("lblrowpension");
                        DropDownList GD_EmployeePensionTrust_txtname = (DropDownList)GD_EmployeePensionTrust.Rows[rowIndex].Cells[1].FindControl("GD_EmployeePensionTrusttxtname");
                        TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[4].FindControl("txtpercentagePension");

                        lblrowpension.Text = Convert.ToString(ds.Tables[0].Rows[i]["RowNumber"]);
                        GD_EmployeePensionTrust_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_EmployeePensionTrust_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                        GD_EmployeePensionTrust_dobcal.Text = Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["DOB"])).ToShortDateString();
                        GD_EmployeePensionTrust_txtpercentage.Text = Convert.ToString(ds.Tables[0].Rows[i]["PensionShare"]);
                    }
                    rowIndex++;
                }
            }
            else
            {
                SetInitialRowPension();
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
    private void SetInitialRowPension()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("FML_MEMBER_NAME", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("REL_TYPE_ID", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("DOB", typeof(string)));//for TextBox value  
        dt.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));//for TextBox value  
        dt.Columns.Add(new DataColumn("PensionShare", typeof(string)));//for TextBox value  
        dr = dt.NewRow();
        dr["RowNumber"] = 0;
        dr["FML_MEMBER_NAME"] = string.Empty;
        dr["REL_TYPE_ID"] = string.Empty;
        dr["DOB"] = string.Empty;
        dr["EMP_CODE"] = string.Empty;
        dr["PensionShare"] = string.Empty;
        dt.Rows.Add(dr);
        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTablePension"] = dt;
        GD_EmployeePensionTrust.DataSource = dt;
        GD_EmployeePensionTrust.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    private void SetPreviousDataPension()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTablePension"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTablePension"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    // pension
                    Label lblrowpension = (Label)GD_EmployeePensionTrust.Rows[rowIndex].Cells[0].FindControl("lblrowpension");
                    DropDownList GD_EmployeePensionTrust_txtname = (DropDownList)GD_EmployeePensionTrust.Rows[rowIndex].Cells[1].FindControl("GD_EmployeePensionTrusttxtname");
                    TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                    TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[3].FindControl("dobcal");
                    TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GD_EmployeePensionTrust.Rows[rowIndex].Cells[4].FindControl("txtpercentagePension");

                    if (i < dt.Rows.Count - 1)
                    {

                        //Assign the value from DataTable to the TextBox  Pension Trust  
                        lblrowpension.Text = Convert.ToString(dt.Rows[i]["RowNumber"]);
                        GD_EmployeePensionTrust_txtname.Items.FindByText(Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_EmployeePensionTrust_txtrelationship.Text = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        GD_EmployeePensionTrust_dobcal.Text = Convert.ToString(dt.Rows[i]["DOB"]);
                        GD_EmployeePensionTrust_txtpercentage.Text = Convert.ToString(dt.Rows[i]["PensionShare"]);

                    }
                    rowIndex++;
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtpercentagePension_TextChanged(object sender, EventArgs e)
    {
        int total = 0;
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txttotal = (TextBox)GD_EmployeePensionTrust.Rows[rowID].Cells[4].FindControl("txtpercentagePension");
        for (int i = 0; i < GD_EmployeePensionTrust.Rows.Count; i++)
        {
            TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GD_EmployeePensionTrust.Rows[i].Cells[4].FindControl("txtpercentagePension");
            if (GD_EmployeePensionTrust_txtpercentage.Text != "" && GD_EmployeePensionTrust_txtpercentage.Text != "0")
            {
                total = total + Convert.ToInt32(GD_EmployeePensionTrust_txtpercentage.Text != "" ? GD_EmployeePensionTrust_txtpercentage.Text : "0");
            }
        }
        if (total == 100)
        {

        }
        else
        {
            txttotal.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "PercentageMessage();", true);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPension_Click(object sender, EventArgs e)
    {
        ViewPension();
        int l = SaveItemDetailsPension((DataTable)ViewState["CurrentTablePension"]);
        if (l > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
            string value = ViewState["EMPCode"].ToString() + "~" + 3;
            ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public int SaveItemDetailsPension(DataTable dt)
    {
        int k = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = null;
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["PensionShare"])) && Convert.ToString(dt.Rows[i]["PensionShare"]) != "0")
                    {
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "InsertFamilyDetailsPension";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[i]["RowNumber"]);
                        cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"];
                        cmd.Parameters.Add("@PensionShare", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["PensionShare"]);
                        cmd.Parameters.Add("@REL_TYPE_ID", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        cmd.Parameters.Add("@FML_MEMBER_NAME", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]);
                        con.Open();
                        k = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                string Message = "Mr./Ms. Gaurav Mehta,The Family Particular of Mr./Mrs." + Convert.ToString(Session["Name"]) + @" has been sent for approval.";
                //  SendEmail(Message, "Submission Pension Trust", "Gaurav.mehta@icsi.edu");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //lblError.Text = ex.Message.ToString();
        }
        return k;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnpensionpreview_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 3;
        ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnpensionrefresh_Click(object sender, EventArgs e)
    {
        BindUserDetailsPension(ViewState["EMPCode"].ToString());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnpensionprint_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 2;
        ScriptManager.RegisterStartupScript(this, GetType(), "Fetch", "Fetch('" + value + @"');", true);
    }
    #endregion

    #region Gratuity
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GD_Gratuitytxtname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            DataTable dt = null;
            DropDownList lb = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DropDownList GD_Gratuitytxtname = (DropDownList)GD_Gratuity.Rows[rowID].Cells[1].FindControl("GD_Gratuitytxtname");
            if (GD_Gratuity.Rows.Count > 0)
            {
                for (int i = 0; i < GD_Gratuity.Rows.Count; i++)
                {
                    if (i != rowID)
                    {
                        DropDownList GD_Gratuitytxtnamevalue = (DropDownList)GD_Gratuity.Rows[i].Cells[1].FindControl("GD_Gratuitytxtname");
                        TextBox GD_Gratuity_txtrelationship = (TextBox)GD_Gratuity.Rows[rowID].Cells[2].FindControl("txtrelationship");
                        TextBox GD_Gratuity_dobcal = (TextBox)GD_Gratuity.Rows[rowID].Cells[3].FindControl("dobcal");
                        TextBox GD_Gratuity_txtpercentagePension = (TextBox)GD_Gratuity.Rows[rowID].Cells[4].FindControl("txtpercentageGratuity");
                        if ((GD_Gratuitytxtnamevalue.SelectedItem.Text == GD_Gratuitytxtname.SelectedItem.Text))
                        {
                            flag = true;
                            GD_Gratuity_txtrelationship.Text = string.Empty;
                            GD_Gratuity_dobcal.Text = string.Empty;
                            GD_Gratuity_txtpercentagePension.Text = "0";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Notification();", true);
                        }
                    }
                }
            }
            if (!flag)
            {
                Label lblrowgratuity = (Label)GD_Gratuity.Rows[rowID].Cells[0].FindControl("lblrowgratuity");
                TextBox GD_Gratuity_txtrelationship = (TextBox)GD_Gratuity.Rows[rowID].Cells[2].FindControl("txtrelationship");
                TextBox GD_Gratuity_dobcal = (TextBox)GD_Gratuity.Rows[rowID].Cells[3].FindControl("dobcal");
                TextBox GD_Gratuity_txtpercentagePension = (TextBox)GD_Gratuity.Rows[rowID].Cells[4].FindControl("txtpercentageGratuity");
                dt = GetRowData(GD_Gratuitytxtname.SelectedItem.Text, ViewState["EMPCode"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    GD_Gratuitytxtname.ClearSelection();
                    //Assign the value from DataTable to the TextBox  Provident Fund  
                    lblrowgratuity.Text = Convert.ToString(dt.Rows[0]["RowNumber"]);
                    GD_Gratuitytxtname.Items.FindByText(Convert.ToString(dt.Rows[0]["FML_MEMBER_NAME"])).Selected = true;
                    GD_Gratuity_txtrelationship.Text = Convert.ToString(dt.Rows[0]["REL_TYPE_ID"]);
                    GD_Gratuity_dobcal.Text = Convert.ToString(dt.Rows[0]["DOB"]);
                    if (Convert.ToString(dt.Rows[0]["IsShowHideGratuity"]) == "0")
                    {
                        GD_Gratuity_txtpercentagePension.Text = Convert.ToString(dt.Rows[0]["GratuityShare"]);
                    }
                }
                // ViewState["CurrentTableProvident"] = dt;
                ViewGratuity();
                //UpdateProvident.Update();
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
    protected void GD_Gratuity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["CurrentTableGratuity"];
                ImageButton lb = (ImageButton)e.Row.FindControl("btnimagedeleteGratuity");
                DropDownList ddlREL_TYPE_ID = (DropDownList)e.Row.FindControl("GD_Gratuitytxtname");                
                FillDropDownList(ddlREL_TYPE_ID, "Gratuity");
                if (lb != null)
                {
                    if (dt.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dt.Rows.Count )
                        {
                            lb.Visible = false;                           
                        }
                    }
                    else
                    {
                        lb.Visible = false;                       
                    }
                    if (dt.Rows[0]["IsApproved"].ToString() == "0")
                    {
                        lb.Enabled = true;
                    }
                    else
                    {
                        lb.Enabled = false;
                    }
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnimagedeleteGratuity_Click(object sender, EventArgs e)
    {

        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        DataTable dt = null;
        if (ViewState["CurrentTableGratuity"] != null)
        {
            Label labelrow = (Label)GD_Gratuity.Rows[rowID].Cells[0].FindControl("lblrowgratuity");
            dt = (DataTable)ViewState["CurrentTableGratuity"];
            if (labelrow.Text == "0")
            {
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        //ResetRowID(dt);

                    }
                }
            }
            else
            {
                ViewGratuity();
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "DeleteRow";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"].ToString();
                    cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(labelrow.Text);
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "4";
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertDelete", "alertDelete();", true);
                        BindUserDetailsGratuity(EMPCode);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
        //Store the current data in ViewState for future reference  
        ViewState["CurrentTableGratuity"] = dt;
        //Re bind the GridView for the updated data  
        GD_Gratuity.DataSource = dt;
        GD_Gratuity.DataBind();
        //Set Previous Data on Postbacks  
        SetPreviousDataGratuity();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnaddgratuity_Click(object sender, EventArgs e)
    {
        if (ViewState["CurrentTableGratuity"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableGratuity"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = 0;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["CurrentTableGratuity"] = dtCurrentTable;
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    Label lblrowgratuity = (Label)GD_Gratuity.Rows[i].Cells[0].FindControl("lblrowgratuity");
                    DropDownList GD_Gratuitytxtname = (DropDownList)GD_Gratuity.Rows[i].Cells[1].FindControl("GD_Gratuitytxtname");
                    TextBox GD_Gratuity_txtrelationship = (TextBox)GD_Gratuity.Rows[i].Cells[2].FindControl("txtrelationship");
                    TextBox GD_Gratuity_dobcal = (TextBox)GD_Gratuity.Rows[i].Cells[3].FindControl("dobcal");
                    TextBox GD_Gratuity_txtpercentage = (TextBox)GD_Gratuity.Rows[i].Cells[4].FindControl("txtpercentageGratuity");

                    dtCurrentTable.Rows[i]["RowNumber"] = lblrowgratuity.Text = "0";
                    dtCurrentTable.Rows[i]["FML_MEMBER_NAME"] = GD_Gratuitytxtname.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["REL_TYPE_ID"] = GD_Gratuity_txtrelationship.Text;
                    dtCurrentTable.Rows[i]["DOB"] = GD_Gratuity_dobcal.Text;
                    dtCurrentTable.Rows[i]["GratuityShare"] = GD_Gratuity_txtpercentage.Text;
                    dtCurrentTable.Rows[i]["EMP_CODE"] = ViewState["EMPCode"].ToString();
                }
                //Rebind the Grid with the current data to reflect changes   
                GD_Gratuity.DataSource = dtCurrentTable;
                GD_Gratuity.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks   
        SetPreviousDataGratuity();
    }
    /// <summary>
    /// 
    /// </summary>
    public void ViewGratuity()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableGratuity"];
        int rowIndex = 0;
        if (dtCurrentTable.Rows.Count > 0)
        {
            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            {

                ///Gratuity
                Label lblrowgratuity = (Label)GD_Gratuity.Rows[rowIndex].Cells[0].FindControl("lblrowgratuity");
                DropDownList GD_Gratuitytxtname = (DropDownList)GD_Gratuity.Rows[rowIndex].Cells[1].FindControl("GD_Gratuitytxtname");
                TextBox GD_Gratuity_txtrelationship = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                TextBox GD_Gratuity_dobcal = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[3].FindControl("dobcal");
                TextBox GD_Gratuity_txtpercentage = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[4].FindControl("txtpercentageGratuity");

                if (lblrowgratuity.Text == "")
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = "0";
                }
                else
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = Convert.ToString(lblrowgratuity.Text);
                }
                dtCurrentTable.Rows[i - 1]["FML_MEMBER_NAME"] = GD_Gratuitytxtname.SelectedItem.Text;
                dtCurrentTable.Rows[i - 1]["REL_TYPE_ID"] = Convert.ToString(GD_Gratuity_txtrelationship.Text);
                dtCurrentTable.Rows[i - 1]["DOB"] = Convert.ToString(GD_Gratuity_dobcal.Text);
                dtCurrentTable.Rows[i - 1]["GratuityShare"] = Convert.ToString(GD_Gratuity_txtpercentage.Text);
                dtCurrentTable.Rows[i - 1]["EMP_CODE"] = ViewState["EMPCode"].ToString();
                rowIndex++;
            }
            ViewState["CurrentTableGratuity"] = dtCurrentTable;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Code"></param>
    public void BindUserDetailsGratuity(string Code)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetAllDetailsByEMPCODEGratuity";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = Code;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {                
                if (ds.Tables[0].Rows[0]["IsApproved"].ToString() == "0")
                {
                    btngratuityprint.Enabled = false;
                    btnaddgratuity.Enabled = true;                   
                }
                ViewState["CurrentTableGratuity"] = ds.Tables[0];
                btnaddgratuity.Enabled = true;               
                GD_Gratuity.DataSource = ds.Tables[0];
                GD_Gratuity.DataBind();
                int rowIndex = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        Label lblrowgratuity = (Label)GD_Gratuity.Rows[rowIndex].Cells[0].FindControl("lblrowgratuity");
                        DropDownList GD_Gratuity_txtname = (DropDownList)GD_Gratuity.Rows[rowIndex].Cells[1].FindControl("GD_Gratuitytxtname");
                        TextBox GD_Gratuity_txtrelationship = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_Gratuity_dobcal = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        TextBox GD_Gratuity_txtpercentage = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[4].FindControl("txtpercentageGratuity");

                        lblrowgratuity.Text = Convert.ToString(ds.Tables[0].Rows[0]["RowNumber"]);
                        GD_Gratuity_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_Gratuity_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                      //  GD_Gratuity_dobcal.Text = Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["DOB"])).ToShortDateString();
                        GD_Gratuity_dobcal.Text = Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                        GD_Gratuity_txtpercentage.Text = Convert.ToString(ds.Tables[0].Rows[i]["GratuityShare"]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[rowIndex]["GratuityShare"]) != "0")
                    {
                        Label lblrowgratuity = (Label)GD_Gratuity.Rows[rowIndex].Cells[0].FindControl("lblrowgratuity");
                        DropDownList GD_Gratuity_txtname = (DropDownList)GD_Gratuity.Rows[rowIndex].Cells[1].FindControl("GD_Gratuitytxtname");
                        TextBox GD_Gratuity_txtrelationship = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_Gratuity_dobcal = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        TextBox GD_Gratuity_txtpercentage = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[4].FindControl("txtpercentageGratuity");

                        lblrowgratuity.Text = Convert.ToString(ds.Tables[0].Rows[0]["RowNumber"]);
                        GD_Gratuity_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_Gratuity_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                      //  GD_Gratuity_dobcal.Text = Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["DOB"])).ToShortDateString();
                        GD_Gratuity_dobcal.Text = Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                        GD_Gratuity_txtpercentage.Text = Convert.ToString(ds.Tables[0].Rows[i]["GratuityShare"]);
                    }
                    rowIndex++;
                }
            }

            else
            {
                SetInitialRowGratuity();
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
    private void SetInitialRowGratuity()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("FML_MEMBER_NAME", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("REL_TYPE_ID", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("DOB", typeof(string)));//for TextBox value   
        
        dt.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));//for TextBox value 
        
        dt.Columns.Add(new DataColumn("GratuityShare", typeof(string)));//for TextBox value  
        

        dr = dt.NewRow();
        dr["RowNumber"] = 0;
        dr["FML_MEMBER_NAME"] = string.Empty;
        dr["REL_TYPE_ID"] = string.Empty;
        
        dr["EMP_CODE"] = string.Empty;
       
        dr["GratuityShare"] = string.Empty;
        
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTableGratuity"] = dt;
        GD_Gratuity.DataSource = dt;
        GD_Gratuity.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    private void SetPreviousDataGratuity()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableGratuity"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableGratuity"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ///Gratuity
                    Label lblrowgratuity = (Label)GD_Gratuity.Rows[rowIndex].Cells[0].FindControl("lblrowgratuity");
                    DropDownList GD_Gratuitytxtname = (DropDownList)GD_Gratuity.Rows[rowIndex].Cells[1].FindControl("GD_Gratuitytxtname");
                    TextBox GD_Gratuity_txtrelationship = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                    TextBox GD_Gratuity_dobcal = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[3].FindControl("dobcal");
                    TextBox GD_Gratuity_txtpercentage = (TextBox)GD_Gratuity.Rows[rowIndex].Cells[4].FindControl("txtpercentageGratuity");

                    if (i < dt.Rows.Count - 1)
                    {
                        //Assign the value from DataTable to the TextBox  Gratuity  
                        lblrowgratuity.Text = Convert.ToString(dt.Rows[i]["RowNumber"]);
                        GD_Gratuitytxtname.Items.FindByText(Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_Gratuity_txtrelationship.Text = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        GD_Gratuity_dobcal.Text = (Convert.ToString(dt.Rows[i]["DOB"]));
                        GD_Gratuity_txtpercentage.Text = Convert.ToString(dt.Rows[i]["GratuityShare"]);
                    }
                    rowIndex++;
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void txtpercentageGratuity_TextChanged(object sender, EventArgs e)
    //{
    //    int total = 0;
    //    TextBox lb = (TextBox)sender;
    //    GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
    //    int rowID = gvRow.RowIndex;
    //    TextBox txttotal = (TextBox)GD_Gratuity.Rows[rowID].Cells[4].FindControl("txtpercentageGratuity");
    //    for (int i = 0; i < GD_Gratuity.Rows.Count; i++)
    //    {
    //        TextBox GD_Gratuity_txtpercentage = (TextBox)GD_Gratuity.Rows[i].Cells[4].FindControl("txtpercentageGratuity");
    //        if (GD_Gratuity_txtpercentage.Text != "" && GD_Gratuity_txtpercentage.Text != "0")
    //        {
    //            total = total + Convert.ToInt32(GD_Gratuity_txtpercentage.Text != "" ? GD_Gratuity_txtpercentage.Text : "0");
    //        }
    //    }
    //    if (total == 100)
    //    {

    //    }
    //    else
    //    {
    //        txttotal.Text = "";
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "PercentageMessage();", true);
    //    }
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btngratuity_Click(object sender, EventArgs e)
    {

        int val = 0;
        if (GD_Gratuity.Rows.Count > 0)
        {
            for (int i = 0; i < GD_Gratuity.Rows.Count; i++)
            {
                TextBox GD_Gratuity_txtpercentage = (TextBox)GD_Gratuity.Rows[i].Cells[4].FindControl("txtpercentageGratuity");
                if ((GD_Gratuity_txtpercentage.Text == "" || GD_Gratuity_txtpercentage.Text == "0"))
                {
                    val = 1;
                }
            }
            if (val == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessageSave();", true);
                return;
            }
            else
            {
                int total = 0;
                for (int i = 0; i < GD_Gratuity.Rows.Count; i++)
                {
                    TextBox GD_Gratuity_txtpercentage = (TextBox)GD_Gratuity.Rows[i].Cells[4].FindControl("txtpercentageGratuity");
                    if (GD_Gratuity_txtpercentage.Text != "" && GD_Gratuity_txtpercentage.Text != "0")
                    {
                        total = total + Convert.ToInt32(GD_Gratuity_txtpercentage.Text != "" ? GD_Gratuity_txtpercentage.Text : "0");
                    }
                }
                if (total == 100)
                {

                }
                else
                {                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "PercentageMessage();", true);
                    return;
                }
                ViewGratuity();
                int k = SaveItemDetailsGratuity((DataTable)ViewState["CurrentTableGratuity"]);
                if (k > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
                    string value = ViewState["EMPCode"].ToString() + "~" + 4;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
                }
            }
        }        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public int SaveItemDetailsGratuity(DataTable dt)
    {
        int k = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = null;
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["GratuityShare"])) && Convert.ToString(dt.Rows[i]["GratuityShare"]) != "0")
                    {
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "InsertFamilyDetailsGratuity";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[i]["RowNumber"]);
                        cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"];
                        cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["DOB"]);
                        cmd.Parameters.Add("@GratuityShare", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["GratuityShare"]);
                        cmd.Parameters.Add("@REL_TYPE_ID", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        cmd.Parameters.Add("@FML_MEMBER_NAME", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]);
                        con.Open();
                        k = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                string Message = "Mr./Ms. Gaurav Mehta,The Family Particular of Mr./Mrs." + Convert.ToString(Session["Name"]) + @" has been sent for approval.";
                //SendEmail(Message, "Submission Gratuity Share", "Gaurav.mehta@icsi.edu");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //lblError.Text = ex.Message.ToString();
        }
        return k;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btngratuitypreview_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 4;
        ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btngratuityrefresh_Click(object sender, EventArgs e)
    {
        BindUserDetailsGratuity(ViewState["EMPCode"].ToString());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btngratuityprint_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 4;
        ScriptManager.RegisterStartupScript(this, GetType(), "Fetch", "Fetch('" + value + @"');", true);
    }
    #endregion

    #region Benevolent
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GD_BenevolentFundtxtname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            DataTable dt = null;
            DropDownList lb = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DropDownList GD_BenevolentFundtxtname = (DropDownList)GD_BenevolentFund.Rows[rowID].Cells[1].FindControl("GD_BenevolentFundtxtname");
            if (GD_BenevolentFund.Rows.Count > 0)
            {
                for (int i = 0; i < GD_BenevolentFund.Rows.Count; i++)
                {
                    if (i != rowID)
                    {
                        DropDownList GD_BenevolentFundtxtnamevalue = (DropDownList)GD_BenevolentFund.Rows[i].Cells[1].FindControl("GD_BenevolentFundtxtname");
                        TextBox GD_BenevolentFund_txtrelationship = (TextBox)GD_BenevolentFund.Rows[rowID].Cells[2].FindControl("txtrelationship");
                        TextBox GD_BenevolentFund_dobcal = (TextBox)GD_BenevolentFund.Rows[rowID].Cells[3].FindControl("dobcal");
                        TextBox GD_BenevolentFund_txtpercentagePension = (TextBox)GD_BenevolentFund.Rows[rowID].Cells[4].FindControl("txtpercentshareBenevolent");
                        if ((GD_BenevolentFundtxtnamevalue.SelectedItem.Text == GD_BenevolentFundtxtname.SelectedItem.Text))
                        {
                            flag = true;
                            GD_BenevolentFund_txtrelationship.Text = string.Empty;
                            GD_BenevolentFund_dobcal.Text = string.Empty;
                            GD_BenevolentFund_txtpercentagePension.Text = "0";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Notification();", true);
                        }
                    }
                }
            }
            if (!flag)
            {
                Label lblrowbenevolent = (Label)GD_BenevolentFund.Rows[rowID].Cells[0].FindControl("lblrowbenevolent");
                TextBox GD_BenevolentFund_txtrelationship = (TextBox)GD_BenevolentFund.Rows[rowID].Cells[2].FindControl("txtrelationship");
                TextBox GD_BenevolentFund_dobcal = (TextBox)GD_BenevolentFund.Rows[rowID].Cells[3].FindControl("dobcal");
                TextBox GD_BenevolentFund_txtpercentagePension = (TextBox)GD_BenevolentFund.Rows[rowID].Cells[4].FindControl("txtpercentshareBenevolent");
                dt = GetRowData(GD_BenevolentFundtxtname.SelectedItem.Text, ViewState["EMPCode"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    GD_BenevolentFundtxtname.ClearSelection();
                    //Assign the value from DataTable to the TextBox  Provident Fund  
                    lblrowbenevolent.Text = Convert.ToString(dt.Rows[0]["RowNumber"]);
                    GD_BenevolentFundtxtname.Items.FindByText(Convert.ToString(dt.Rows[0]["FML_MEMBER_NAME"])).Selected = true;
                    GD_BenevolentFund_txtrelationship.Text = Convert.ToString(dt.Rows[0]["REL_TYPE_ID"]);
                    GD_BenevolentFund_dobcal.Text = Convert.ToString(dt.Rows[0]["DOB"]);
                    if (Convert.ToString(dt.Rows[0]["IsShowHideBenevolent"]) == "0")
                    {
                        GD_BenevolentFund_txtpercentagePension.Text = Convert.ToString(dt.Rows[0]["BenevolentShare"]);
                    }
                }
                // ViewState["CurrentTableProvident"] = dt;
                ViewBenevolent();
                //UpdateProvident.Update();
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
    protected void GD_BenevolentFund_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["CurrentTableBenevolent"];
                ImageButton lb = (ImageButton)e.Row.FindControl("btnimagedeletebenevolent");
                DropDownList ddlREL_TYPE_ID = (DropDownList)e.Row.FindControl("GD_BenevolentFundtxtname");
                FillDropDownList(ddlREL_TYPE_ID, "Benevolent Fund");
                if (lb != null)
                {
                    if (dt.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dt.Rows.Count - 1)
                        {
                            lb.Visible = false;
                        }
                    }
                    else
                    {
                        lb.Visible = false;
                    }
                    if (dt.Rows[0]["IsApproved"].ToString() == "0")
                    {
                        lb.Enabled = true;
                    }
                    else
                    {
                        lb.Enabled = false;
                    }
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnaddbenevolent_Click(object sender, EventArgs e)
    {
        if (ViewState["CurrentTableBenevolent"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableBenevolent"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = 0;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["CurrentTableBenevolent"] = dtCurrentTable;
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    Label lblrowbenevolent = (Label)GD_BenevolentFund.Rows[i].Cells[0].FindControl("lblrowbenevolent");
                    DropDownList GD_BenevolentFund_txtname = (DropDownList)GD_BenevolentFund.Rows[i].Cells[1].FindControl("GD_BenevolentFundtxtname");
                    TextBox GD_BenevolentFund_txtrelationship = (TextBox)GD_BenevolentFund.Rows[i].Cells[2].FindControl("txtrelationship");
                    TextBox GD_BenevolentFund_dobcal = (TextBox)GD_BenevolentFund.Rows[i].Cells[3].FindControl("dobcal");
                    TextBox GD_BenevolentFund_txtpercentshare = (TextBox)GD_BenevolentFund.Rows[i].Cells[4].FindControl("txtpercentshareBenevolent");
                    dtCurrentTable.Rows[i]["RowNumber"] = lblrowbenevolent.Text = "0";
                    dtCurrentTable.Rows[i]["FML_MEMBER_NAME"] = GD_BenevolentFund_txtname.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["REL_TYPE_ID"] = GD_BenevolentFund_txtrelationship.Text;
                    dtCurrentTable.Rows[i]["DOB"] = GD_BenevolentFund_dobcal.Text;
                    dtCurrentTable.Rows[i]["BenevolentShare"] = GD_BenevolentFund_txtpercentshare.Text;
                    dtCurrentTable.Rows[i]["EMP_CODE"] = ViewState["EMPCode"].ToString();

                }
                //Rebind the Grid with the current data to reflect changes   
                GD_BenevolentFund.DataSource = dtCurrentTable;
                GD_BenevolentFund.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks   
        SetPreviousDataBenevolent();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnimagedeletebenevolent_Click(object sender, EventArgs e)
    {
        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        DataTable dt = null;
        if (ViewState["CurrentTableBenevolent"] != null)
        {
            Label labelrow = (Label)GD_BenevolentFund.Rows[rowID].Cells[0].FindControl("lblrowbenevolent");
            dt = (DataTable)ViewState["CurrentTableBenevolent"];
            if (labelrow.Text == "0")
            {
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        //ResetRowID(dt);

                    }
                }
            }
            else
            {
                ViewBenevolent();
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "DeleteRow";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"].ToString();
                    cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(labelrow.Text);
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "5";
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertDelete", "alertDelete();", true);
                        BindUserDetailsBenevolent(EMPCode);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
        //Store the current data in ViewState for future reference  
        ViewState["CurrentTableBenevolent"] = dt;
        //Re bind the GridView for the updated data  
        GD_BenevolentFund.DataSource = dt;
        GD_BenevolentFund.DataBind();
        //Set Previous Data on Postbacks  
        SetPreviousDataBenevolent();
    }
    /// <summary>
    /// 
    /// </summary>
    public void ViewBenevolent()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableBenevolent"];
        int rowIndex = 0;
        if (dtCurrentTable.Rows.Count > 0)
        {
            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            {

                //// Benevolent
                Label lblrowbenevolent = (Label)GD_BenevolentFund.Rows[rowIndex].Cells[0].FindControl("lblrowbenevolent");
                DropDownList GD_BenevolentFundtxtname = (DropDownList)GD_BenevolentFund.Rows[rowIndex].Cells[1].FindControl("GD_BenevolentFundtxtname");
                TextBox GD_BenevolentFund_txtrelationship = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                TextBox GD_BenevolentFund_dobcal = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[3].FindControl("dobcal");
                TextBox GD_BenevolentFund_txtpercentshare = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[4].FindControl("txtpercentshareBenevolent");
                if (lblrowbenevolent.Text == "")
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = "0";
                }
                else
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = Convert.ToString(lblrowbenevolent.Text);
                }
                dtCurrentTable.Rows[i - 1]["FML_MEMBER_NAME"] = GD_BenevolentFundtxtname.SelectedItem.Text;
                dtCurrentTable.Rows[i - 1]["REL_TYPE_ID"] = Convert.ToString(GD_BenevolentFund_txtrelationship.Text);
                dtCurrentTable.Rows[i - 1]["DOB"] = Convert.ToString(GD_BenevolentFund_dobcal.Text);
                dtCurrentTable.Rows[i - 1]["BenevolentShare"] = Convert.ToString(GD_BenevolentFund_txtpercentshare.Text);
                dtCurrentTable.Rows[i - 1]["EMP_CODE"] = ViewState["EMPCode"].ToString();
                rowIndex++;
            }
            ViewState["CurrentTableBenevolent"] = dtCurrentTable;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Code"></param>
    public void BindUserDetailsBenevolent(string Code)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetAllDetailsByEMPCODEBenevolent";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = Code;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["CurrentTableBenevolent"] = ds.Tables[0];
                GD_BenevolentFund.DataSource = ds.Tables[0];
                GD_BenevolentFund.DataBind();
                int rowIndex = 0;
                if (ds.Tables[0].Rows[0]["IsApproved"].ToString() == "0")
                {
                    btnbenevolentprint.Enabled = false;
                    btnaddbenevolent.Enabled = true;
                }
                btnaddbenevolent.Enabled = false;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        Label lblrowbenevolent = (Label)GD_BenevolentFund.Rows[rowIndex].Cells[0].FindControl("lblrowbenevolent");
                        DropDownList GD_BenevolentFund_txtname = (DropDownList)GD_BenevolentFund.Rows[rowIndex].Cells[1].FindControl("GD_BenevolentFundtxtname");
                        TextBox GD_BenevolentFund_txtrelationship = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_BenevolentFund_dobcal = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        TextBox GD_BenevolentFund_txtpercentshare = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[4].FindControl("txtpercentshareBenevolent");
                        lblrowbenevolent.Text = Convert.ToString(ds.Tables[0].Rows[i]["RowNumber"]);
                        GD_BenevolentFund_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_BenevolentFund_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                       // GD_BenevolentFund_dobcal.Text = Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["DOB"])).ToShortDateString();
                        GD_BenevolentFund_dobcal.Text = Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                        GD_BenevolentFund_txtpercentshare.Text = Convert.ToString(ds.Tables[0].Rows[i]["BenevolentShare"]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[rowIndex]["BenevolentShare"]) != "0")
                    {
                        Label lblrowbenevolent = (Label)GD_BenevolentFund.Rows[rowIndex].Cells[0].FindControl("lblrowbenevolent");
                        DropDownList GD_BenevolentFund_txtname = (DropDownList)GD_BenevolentFund.Rows[rowIndex].Cells[1].FindControl("GD_BenevolentFundtxtname");
                        TextBox GD_BenevolentFund_txtrelationship = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_BenevolentFund_dobcal = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        TextBox GD_BenevolentFund_txtpercentshare = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[4].FindControl("txtpercentshareBenevolent");
                        lblrowbenevolent.Text = Convert.ToString(ds.Tables[0].Rows[i]["RowNumber"]);
                        GD_BenevolentFund_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_BenevolentFund_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                     //   GD_BenevolentFund_dobcal.Text = Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["DOB"])).ToShortDateString();
                        GD_BenevolentFund_dobcal.Text = Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                        GD_BenevolentFund_txtpercentshare.Text = Convert.ToString(ds.Tables[0].Rows[i]["BenevolentShare"]);
                    }
                    rowIndex++;
                }
            }

            else
            {
                SetInitialRowBenevolent();
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
    private void SetInitialRowBenevolent()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("FML_MEMBER_NAME", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("REL_TYPE_ID", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("DOB", typeof(string)));//for TextBox value   
        
        dt.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));//for TextBox value  
        dt.Columns.Add(new DataColumn("BenevolentShare", typeof(string)));//for TextBox value  
        

        dr = dt.NewRow();
        dr["RowNumber"] = 0;
        dr["FML_MEMBER_NAME"] = string.Empty;
        dr["REL_TYPE_ID"] = string.Empty;
        dr["DOB"] = string.Empty;
      
        dr["EMP_CODE"] = string.Empty;
        dr["BenevolentShare"] = string.Empty;
        
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTableBenevolent"] = dt;
        GD_BenevolentFund.DataSource = dt;
        GD_BenevolentFund.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    private void SetPreviousDataBenevolent()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableBenevolent"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableBenevolent"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //// Benevolent
                    Label lblrowbenevolent = (Label)GD_BenevolentFund.Rows[rowIndex].Cells[0].FindControl("lblrowbenevolent");
                    DropDownList GD_BenevolentFund_txtname = (DropDownList)GD_BenevolentFund.Rows[rowIndex].Cells[1].FindControl("GD_BenevolentFundtxtname");
                    TextBox GD_BenevolentFund_txtrelationship = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                    TextBox GD_BenevolentFund_dobcal = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[3].FindControl("dobcal");
                    TextBox GD_BenevolentFund_txtpercentshare = (TextBox)GD_BenevolentFund.Rows[rowIndex].Cells[4].FindControl("txtpercentshareBenevolent");

                    if (i < dt.Rows.Count - 1)
                    {
                        //Assign the value from DataTable to the TextBox  Benevolent Fund  
                        lblrowbenevolent.Text = Convert.ToString(dt.Rows[i]["RowNumber"]);
                        GD_BenevolentFund_txtname.Items.FindByText(Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_BenevolentFund_txtrelationship.Text = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        GD_BenevolentFund_dobcal.Text = (Convert.ToString(dt.Rows[i]["DOB"]));
                        GD_BenevolentFund_txtpercentshare.Text = Convert.ToString(dt.Rows[i]["BenevolentShare"]);

                    }
                    rowIndex++;
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void txtpercentshareBenevolent_TextChanged(object sender, EventArgs e)
    //{
    //    int total = 0;
    //    TextBox lb = (TextBox)sender;
    //    GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
    //    int rowID = gvRow.RowIndex;
    //    TextBox txttotal = (TextBox)GD_BenevolentFund.Rows[rowID].Cells[4].FindControl("txtpercentshareBenevolent");
    //    for (int i = 0; i < GD_BenevolentFund.Rows.Count; i++)
    //    {
    //        TextBox GD_BenevolentFund_txtpercentage = (TextBox)GD_BenevolentFund.Rows[i].Cells[4].FindControl("txtpercentshareBenevolent");
    //        if (GD_BenevolentFund_txtpercentage.Text != "" && GD_BenevolentFund_txtpercentage.Text != "0")
    //        {
    //            total = total + Convert.ToInt32(GD_BenevolentFund_txtpercentage.Text != "" ? GD_BenevolentFund_txtpercentage.Text : "0");
    //        }
    //    }
    //    if (total == 100)
    //    {

    //    }
    //    else
    //    {
    //        txttotal.Text = "";
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "PercentageMessage();", true);
    //    }
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnbenevolent_Click(object sender, EventArgs e)
    {
        int val = 0;
        if (GD_BenevolentFund.Rows.Count > 0)
        {
            for (int i = 0; i < GD_BenevolentFund.Rows.Count; i++)
            {  
                TextBox GD_BenevolentFund_txtpercentshare = (TextBox)GD_BenevolentFund.Rows[i].Cells[4].FindControl("txtpercentshareBenevolent");
                if ((GD_BenevolentFund_txtpercentshare.Text == "" || GD_BenevolentFund_txtpercentshare.Text == "0"))
                {
                    val = 1;
                }
            }
            if (val == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessageSave();", true);
                return;
            }
            else
            {
                int total = 0;
                for (int i = 0; i < GD_BenevolentFund.Rows.Count; i++)
                {
                    TextBox GD_BenevolentFund_txtpercentage = (TextBox)GD_BenevolentFund.Rows[i].Cells[4].FindControl("txtpercentshareBenevolent");
                    if (GD_BenevolentFund_txtpercentage.Text != "" && GD_BenevolentFund_txtpercentage.Text != "0")
                    {
                        total = total + Convert.ToInt32(GD_BenevolentFund_txtpercentage.Text != "" ? GD_BenevolentFund_txtpercentage.Text : "0");
                    }
                }
                if (total == 100)
                {

                }
                else
                {                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "PercentageMessage();", true);
                    return;
                }
                ViewBenevolent();
                int j = SaveItemDetailsBenevolent((DataTable)ViewState["CurrentTableBenevolent"]);
                if (j > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
                    string value = ViewState["EMPCode"].ToString() + "~" + 5;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
                }
            }
        }      
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public int SaveItemDetailsBenevolent(DataTable dt)
    {
        int k = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = null;
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["BenevolentShare"])) && Convert.ToString(dt.Rows[i]["BenevolentShare"]) != "0")
                    {
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "InsertFamilyDetailsBenevolent";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[i]["RowNumber"]);
                        cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"];
                        cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["DOB"]);
                        cmd.Parameters.Add("@BenevolentShare", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["BenevolentShare"]);
                        cmd.Parameters.Add("@FML_MEMBER_NAME", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]);
                        cmd.Parameters.Add("@REL_TYPE_ID", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        con.Open();
                        k = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                string Message = "Mr./Ms. Gaurav Mehta,The Family Particular of Mr./Mrs." + Convert.ToString(Session["Name"]) + @" has been sent for approval.";
                //  SendEmail(Message, "Submission Benevolent Share", "Gaurav.mehta@icsi.edu");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //lblError.Text = ex.Message.ToString();
        }
        return k;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnbenevolentpreview_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 5;
        ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnbenevolentrefresh_Click(object sender, EventArgs e)
    {
        BindUserDetailsBenevolent(ViewState["EMPCode"].ToString());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnbenevolentprint_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 5;
        ScriptManager.RegisterStartupScript(this, GetType(), "Fetch", "Fetch('" + value + @"');", true);
    }
    #endregion

    #region Encashment
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GD_Encashmenttxtname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            DataTable dt = null;
            DropDownList lb = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DropDownList GD_Encashmenttxtname = (DropDownList)GD_Encashment.Rows[rowID].Cells[1].FindControl("GD_Encashmenttxtname");
            if (GD_Encashment.Rows.Count > 0)
            {
                for (int i = 0; i < GD_Encashment.Rows.Count; i++)
                {
                    if (i != rowID)
                    {
                        DropDownList GD_Encashmenttxtnamevalue = (DropDownList)GD_Encashment.Rows[i].Cells[1].FindControl("GD_Encashmenttxtname");
                        TextBox GD_Encashment_txtrelationship = (TextBox)GD_Encashment.Rows[rowID].Cells[2].FindControl("txtrelationship");
                        TextBox GD_Encashment_dobcal = (TextBox)GD_Encashment.Rows[rowID].Cells[3].FindControl("dobcal");
                        TextBox GD_Encashment_txtpercentagePension = (TextBox)GD_Encashment.Rows[rowID].Cells[4].FindControl("txtpercentageEncahsment");
                        if ((GD_Encashmenttxtnamevalue.SelectedItem.Text == GD_Encashmenttxtname.SelectedItem.Text))
                        {
                            flag = true;
                            GD_Encashment_txtrelationship.Text = string.Empty;
                            GD_Encashment_dobcal.Text = string.Empty;
                            GD_Encashment_txtpercentagePension.Text = "0";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Notification();", true);
                        }
                    }
                }
            }
            if (!flag)
            {
                Label lblrowencashment = (Label)GD_Encashment.Rows[rowID].Cells[0].FindControl("lblrowencashment");
                TextBox GD_Encashment_txtrelationship = (TextBox)GD_Encashment.Rows[rowID].Cells[2].FindControl("txtrelationship");
                TextBox GD_Encashment_dobcal = (TextBox)GD_Encashment.Rows[rowID].Cells[3].FindControl("dobcal");
                TextBox GD_Encashment_txtpercentagePension = (TextBox)GD_Encashment.Rows[rowID].Cells[4].FindControl("txtpercentageEncahsment");
                dt = GetRowData(GD_Encashmenttxtname.SelectedItem.Text, ViewState["EMPCode"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    GD_Encashmenttxtname.ClearSelection();
                    //Assign the value from DataTable to the TextBox  Provident Fund  
                    lblrowencashment.Text = Convert.ToString(dt.Rows[0]["RowNumber"]);
                    GD_Encashmenttxtname.Items.FindByText(Convert.ToString(dt.Rows[0]["FML_MEMBER_NAME"])).Selected = true;
                    GD_Encashment_txtrelationship.Text = Convert.ToString(dt.Rows[0]["REL_TYPE_ID"]);
                    GD_Encashment_dobcal.Text = Convert.ToString(dt.Rows[0]["DOB"]);
                    if (Convert.ToString(dt.Rows[0]["IsShowHideEncashment"]) == "0")
                    {
                        GD_Encashment_txtpercentagePension.Text = Convert.ToString(dt.Rows[0]["EncashShare"]);
                    }
                }
                // ViewState["CurrentTableProvident"] = dt;
                ViewEncashment();
                //UpdateProvident.Update();
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
    protected void GD_Encashment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["CurrentTableEncashment"];
                ImageButton lb = (ImageButton)e.Row.FindControl("btnimagedeleteencashment");
                DropDownList ddlREL_TYPE_ID = (DropDownList)e.Row.FindControl("GD_Encashmenttxtname");
                FillDropDownList(ddlREL_TYPE_ID, "Encashment Fund");
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
                    if (dt.Rows[0]["IsApproved"].ToString() == "0")
                    {
                        lb.Enabled = true;
                    }
                    else
                    {
                        lb.Enabled = false;
                    }
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnimagedeleteencashment_Click(object sender, EventArgs e)
    {
        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        DataTable dt = null;
        if (ViewState["CurrentTableEncashment"] != null)
        {
            Label labelrow = (Label)GD_Encashment.Rows[rowID].Cells[0].FindControl("lblrowencashment");
            dt = (DataTable)ViewState["CurrentTableEncashment"];
            if (labelrow.Text == "0")
            {
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        //ResetRowID(dt);

                    }
                }
            }
            else
            {
                ViewEncashment();
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "DeleteRow";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"].ToString();
                    cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(labelrow.Text);
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "6";
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertDelete", "alertDelete();", true);
                        BindUserDetailsEncashment(Session["UserName"].ToString());
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
        //Store the current data in ViewState for future reference  
        ViewState["CurrentTableEncashment"] = dt;
        //Re bind the GridView for the updated data  
        GD_Encashment.DataSource = dt;
        GD_Encashment.DataBind();
        //Set Previous Data on Postbacks  
        SetPreviousDataEncashment();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnaddencashment_Click(object sender, EventArgs e)
    {
        if (ViewState["CurrentTableEncashment"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableEncashment"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = 0;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["CurrentTableEncashment"] = dtCurrentTable;
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    Label lblrowencashmenent = (Label)GD_Encashment.Rows[i].Cells[0].FindControl("lblrowencashment");
                    DropDownList GD_Encashment_txtname = (DropDownList)GD_Encashment.Rows[i].Cells[1].FindControl("GD_Encashmenttxtname");
                    TextBox GD_Encashment_txtrelationship = (TextBox)GD_Encashment.Rows[i].Cells[2].FindControl("txtrelationship");
                    TextBox GD_Encashment_dobcal = (TextBox)GD_Encashment.Rows[i].Cells[3].FindControl("dobcal");
                    TextBox GD_Encashment_txtpercentage = (TextBox)GD_Encashment.Rows[i].Cells[4].FindControl("txtpercentageEncahsment");

                    dtCurrentTable.Rows[i]["RowNumber"] = lblrowencashmenent.Text = "0";
                    dtCurrentTable.Rows[i]["FML_MEMBER_NAME"] = GD_Encashment_txtname.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["REL_TYPE_ID"] = GD_Encashment_txtrelationship.Text;
                    dtCurrentTable.Rows[i]["DOB"] = GD_Encashment_dobcal.Text;
                    dtCurrentTable.Rows[i]["EncashShare"] = GD_Encashment_txtpercentage.Text;
                    dtCurrentTable.Rows[i]["EMP_CODE"] = ViewState["EMPCode"].ToString();
                }
                //Rebind the Grid with the current data to reflect changes   
                GD_Encashment.DataSource = dtCurrentTable;
                GD_Encashment.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks   
        SetPreviousDataEncashment();
    }
    /// <summary>
    /// 
    /// </summary>
    public void ViewEncashment()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableEncashment"];
        int rowIndex = 0;
        if (dtCurrentTable.Rows.Count > 0)
        {
            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            {
                ///Encashment
                Label lblrowencashmenent = (Label)GD_Encashment.Rows[rowIndex].Cells[0].FindControl("lblrowencashment");
                DropDownList GD_Encashment_txtname = (DropDownList)GD_Encashment.Rows[rowIndex].Cells[1].FindControl("GD_Encashmenttxtname");
                TextBox GD_Encashment_txtrelationship = (TextBox)GD_Encashment.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                TextBox GD_Encashment_dobcal = (TextBox)GD_Encashment.Rows[rowIndex].Cells[3].FindControl("dobcal");
                TextBox GD_Encashment_txtpercentage = (TextBox)GD_Encashment.Rows[rowIndex].Cells[4].FindControl("txtpercentageEncahsment");
                if (lblrowencashmenent.Text == "")
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = "0";
                }
                else
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = Convert.ToString(lblrowencashmenent.Text);
                }
                dtCurrentTable.Rows[i - 1]["FML_MEMBER_NAME"] = Convert.ToString(GD_Encashment_txtname.SelectedItem.Text);
                dtCurrentTable.Rows[i - 1]["REL_TYPE_ID"] = Convert.ToString(GD_Encashment_txtrelationship.Text);
                dtCurrentTable.Rows[i - 1]["DOB"] = Convert.ToString(GD_Encashment_dobcal.Text);
                dtCurrentTable.Rows[i - 1]["EncashShare"] = Convert.ToString(GD_Encashment_txtpercentage.Text);
                dtCurrentTable.Rows[i - 1]["EMP_CODE"] = ViewState["EMPCode"].ToString();
                rowIndex++;
            }
            ViewState["CurrentTableEncashment"] = dtCurrentTable;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Code"></param>
    public void BindUserDetailsEncashment(string Code)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetAllDetailsByEMPCODEEncashment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = Code;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IsApproved"].ToString() == "0")
                {
                    btnencashprint.Enabled = false;
                    btnaddencashment.Enabled = true;
                }
                btnaddencashment.Enabled = false;
                ViewState["CurrentTableEncashment"] = ds.Tables[0];
                GD_Encashment.DataSource = ds.Tables[0];
                GD_Encashment.DataBind();
                int rowIndex = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        Label lblrowencashmenent = (Label)GD_Encashment.Rows[rowIndex].Cells[0].FindControl("lblrowencashment");
                        DropDownList GD_Encashment_txtname = (DropDownList)GD_Encashment.Rows[rowIndex].Cells[1].FindControl("GD_Encashmenttxtname");
                        TextBox GD_Encashment_txtrelationship = (TextBox)GD_Encashment.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_Encashment_dobcal = (TextBox)GD_Encashment.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        TextBox GD_Encashment_txtpercentage = (TextBox)GD_Encashment.Rows[rowIndex].Cells[4].FindControl("txtpercentageEncahsment");
                        lblrowencashmenent.Text = Convert.ToString(ds.Tables[0].Rows[i]["RowNumber"]);
                        GD_Encashment_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_Encashment_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                       // GD_Encashment_dobcal.Text = Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["DOB"])).ToShortDateString();
                        GD_Encashment_dobcal.Text = Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                        GD_Encashment_txtpercentage.Text = Convert.ToString(ds.Tables[0].Rows[i]["EncashShare"]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[rowIndex]["EncashShare"]) != "0")
                    {
                        Label lblrowencashmenent = (Label)GD_Encashment.Rows[rowIndex].Cells[0].FindControl("lblrowencashment");
                        DropDownList GD_Encashment_txtname = (DropDownList)GD_Encashment.Rows[rowIndex].Cells[1].FindControl("GD_Encashmenttxtname");
                        TextBox GD_Encashment_txtrelationship = (TextBox)GD_Encashment.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_Encashment_dobcal = (TextBox)GD_Encashment.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        TextBox GD_Encashment_txtpercentage = (TextBox)GD_Encashment.Rows[rowIndex].Cells[4].FindControl("txtpercentageEncahsment");
                        lblrowencashmenent.Text = Convert.ToString(ds.Tables[0].Rows[i]["RowNumber"]);
                        GD_Encashment_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_Encashment_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                       // GD_Encashment_dobcal.Text = Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["DOB"])).ToShortDateString();
                        GD_Encashment_dobcal.Text = Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                        GD_Encashment_txtpercentage.Text = Convert.ToString(ds.Tables[0].Rows[i]["EncashShare"]);
                    }
                    rowIndex++;
                }
            }
            else
            {
                SetInitialRowEncashment();
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
    private void SetInitialRowEncashment()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("FML_MEMBER_NAME", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("REL_TYPE_ID", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("DOB", typeof(string)));//for TextBox value   
        
        dt.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));//for TextBox value  
        
        dt.Columns.Add(new DataColumn("EncashShare", typeof(string)));//for TextBox value  
        

        dr = dt.NewRow();
        dr["RowNumber"] = 0;
        dr["FML_MEMBER_NAME"] = string.Empty;
        dr["REL_TYPE_ID"] = string.Empty;
        dr["DOB"] = string.Empty;
       
        dr["EMP_CODE"] = string.Empty;
        dr["EncashShare"] = string.Empty;
        
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTableEncashment"] = dt;
        GD_Encashment.DataSource = dt;
        GD_Encashment.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    private void SetPreviousDataEncashment()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableEncashment"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableEncashment"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ///Encashment
                    Label lblrowencashmenent = (Label)GD_Encashment.Rows[rowIndex].Cells[0].FindControl("lblrowencashment");
                    DropDownList GD_Encashment_txtname = (DropDownList)GD_Encashment.Rows[rowIndex].Cells[1].FindControl("GD_Encashmenttxtname");
                    TextBox GD_Encashment_txtrelationship = (TextBox)GD_Encashment.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                    TextBox GD_Encashment_dobcal = (TextBox)GD_Encashment.Rows[rowIndex].Cells[3].FindControl("dobcal");
                    TextBox GD_Encashment_txtpercentage = (TextBox)GD_Encashment.Rows[rowIndex].Cells[4].FindControl("txtpercentageEncahsment");
                    if (i < dt.Rows.Count - 1)
                    {
                        //Assign the value from DataTable to the TextBox  Encashment  
                        lblrowencashmenent.Text = Convert.ToString(dt.Rows[i]["RowNumber"]);
                        GD_Encashment_txtname.Items.FindByText(Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_Encashment_txtrelationship.Text = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        GD_Encashment_dobcal.Text = (Convert.ToString(dt.Rows[i]["DOB"]));
                        GD_Encashment_txtpercentage.Text = Convert.ToString(dt.Rows[i]["EncashShare"]);
                    }
                    rowIndex++;
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void txtpercentageEncahsment_TextChanged(object sender, EventArgs e)
    //{
    //    int total = 0;
    //    TextBox lb = (TextBox)sender;
    //    GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
    //    int rowID = gvRow.RowIndex;
    //    TextBox txttotal = (TextBox)GD_Encashment.Rows[rowID].Cells[4].FindControl("txtpercentageEncahsment");
    //    for (int i = 0; i < GD_Encashment.Rows.Count; i++)
    //    {
    //        TextBox GD_Encashment_txtpercentage = (TextBox)GD_Encashment.Rows[i].Cells[4].FindControl("txtpercentageEncahsment");
    //        if (GD_Encashment_txtpercentage.Text != "" && GD_Encashment_txtpercentage.Text != "0")
    //        {
    //            total = total + Convert.ToInt32(GD_Encashment_txtpercentage.Text != "" ? GD_Encashment_txtpercentage.Text : "0");
    //        }
    //    }
    //    if (total == 100)
    //    {

    //    }
    //    else
    //    {
    //        txttotal.Text = "";
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "PercentageMessage();", true);
    //    }
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        int val = 0;
        if (GD_Encashment.Rows.Count > 0)
        {
            for (int i = 0; i < GD_Encashment.Rows.Count; i++)
            {
                TextBox GD_Encashment_txtpercentage = (TextBox)GD_Encashment.Rows[i].Cells[4].FindControl("txtpercentageEncahsment");
                if ((GD_Encashment_txtpercentage.Text == "" || GD_Encashment_txtpercentage.Text == "0"))
                {
                    val = 1;
                }
            }
            if (val == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessageSave();", true);
                return;
            }
            else
            {
                int total = 0;
                for (int i = 0; i < GD_Encashment.Rows.Count; i++)
                {
                    TextBox GD_Encashment_txtpercentage = (TextBox)GD_Encashment.Rows[i].Cells[4].FindControl("txtpercentageEncahsment");
                    if (GD_Encashment_txtpercentage.Text != "" && GD_Encashment_txtpercentage.Text != "0")
                    {
                        total = total + Convert.ToInt32(GD_Encashment_txtpercentage.Text != "" ? GD_Encashment_txtpercentage.Text : "0");
                    }
                }
                if (total == 100)
                {

                }
                else
                {                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "PercentageMessage();", true);
                    return;
                }
                ViewEncashment();
                int l = SaveItemDetailsEncash((DataTable)ViewState["CurrentTableEncashment"]);
                if (l > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
                    string value = ViewState["EMPCode"].ToString() + "~" + 6;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
                }
            }
        }        
    }
    /// <summary>
    /// Save ItemDetails
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public int SaveItemDetailsEncash(DataTable dt)
    {
        int k = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = null;
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["EncashShare"])) && Convert.ToString(dt.Rows[i]["EncashShare"]) != "0")
                    {
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "InsertFamilyDetailsEncash";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[i]["RowNumber"]);
                        cmd.Parameters.Add("@FML_MEMBER_NAME", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]);
                        cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"];
                        cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["DOB"]);
                        cmd.Parameters.Add("@EncashShare", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["EncashShare"]);
                        cmd.Parameters.Add("@REL_TYPE_ID", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        con.Open();
                        k = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                string Message = "Mr./Ms. Gaurav Mehta,The Family Particular of Mr./Mrs." + Convert.ToString(Session["Name"]) + @" has been sent for approval.";
                // SendEmail(Message, "Submission Encashment Share", "Gaurav.mehta@icsi.edu");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //lblError.Text = ex.Message.ToString();
        }
        return k;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnencashPreview_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 6;
        ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnencashrefresh_Click(object sender, EventArgs e)
    {
        BindUserDetailsEncashment(ViewState["EMPCode"].ToString());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnencashprint_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 6;
        ScriptManager.RegisterStartupScript(this, GetType(), "Fetch", "Fetch('" + value + @"');", true);
    }
    #endregion

    #region NewPension
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GD_EmployeeNewPensionTrusttxtname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            DataTable dt = null;
            DropDownList lb = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DropDownList GD_EmployeePensionTrusttxtname = (DropDownList)GridViewNewPensionTrust.Rows[rowID].Cells[1].FindControl("GD_EmployeeNewPensionTrusttxtname");
            if (GD_EmployeePensionTrust.Rows.Count > 0)
            {
                for (int i = 0; i < GD_EmployeePensionTrust.Rows.Count; i++)
                {
                    if (i != rowID)
                    {
                        DropDownList GD_EmployeePensionTrusttxtnamevalue = (DropDownList)GridViewNewPensionTrust.Rows[i].Cells[1].FindControl("GD_EmployeeNewPensionTrusttxtname");
                        TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GridViewNewPensionTrust.Rows[rowID].Cells[2].FindControl("txtrelationship");
                        TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GridViewNewPensionTrust.Rows[rowID].Cells[3].FindControl("dobcal");
                        TextBox GD_EmployeePensionTrust_txtpercentagePension = (TextBox)GridViewNewPensionTrust.Rows[rowID].Cells[4].FindControl("txtpercentageNewPension");
                        if ((GD_EmployeePensionTrusttxtnamevalue.SelectedItem.Text == GD_EmployeePensionTrusttxtname.SelectedItem.Text))
                        {
                            flag = true;

                            GD_EmployeePensionTrust_txtrelationship.Text = string.Empty;
                            GD_EmployeePensionTrust_dobcal.Text = string.Empty;
                            GD_EmployeePensionTrust_txtpercentagePension.Text = "0";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Notification();", true);
                        }
                    }
                }
            }
            if (!flag)
            {
                Label lblrowpension = (Label)GridViewNewPensionTrust.Rows[rowID].Cells[0].FindControl("lblrowpension");
                TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GridViewNewPensionTrust.Rows[rowID].Cells[2].FindControl("txtrelationship");
                TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GridViewNewPensionTrust.Rows[rowID].Cells[3].FindControl("dobcal");
                TextBox GD_EmployeePensionTrust_txtpercentagePension = (TextBox)GridViewNewPensionTrust.Rows[rowID].Cells[4].FindControl("txtpercentageNewPension");
               
                dt = GetRowData(GD_EmployeePensionTrusttxtname.SelectedItem.Text, ViewState["EMPCode"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    GD_EmployeePensionTrusttxtname.ClearSelection();
                    //Assign the value from DataTable to the TextBox  Provident Fund  
                    lblrowpension.Text = Convert.ToString(dt.Rows[0]["RowNumber"]);
                    GD_EmployeePensionTrusttxtname.Items.FindByText(Convert.ToString(dt.Rows[0]["FML_MEMBER_NAME"])).Selected = true;
                    GD_EmployeePensionTrust_txtrelationship.Text = Convert.ToString(dt.Rows[0]["REL_TYPE_ID"]);
                    GD_EmployeePensionTrust_dobcal.Text = Convert.ToString(dt.Rows[0]["DOB"]);
                    //if (Convert.ToString(dt.Rows[0]["IsShowHidePension"]) == "0")
                    //{
                    //    GD_EmployeePensionTrust_txtpercentagePension.Text = Convert.ToString(dt.Rows[0]["PensionShare"]);
                    //}
                }
                // ViewState["CurrentTableProvident"] = dt;
                ViewNewPension();
                //UpdateProvident.Update();
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
    protected void GridViewNewPensionTrust_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["CurrentTableNewPension"];
                ImageButton lb = (ImageButton)e.Row.FindControl("btnimagedeleteNewPensiontrust");
                DropDownList ddlREL_TYPE_ID = (DropDownList)e.Row.FindControl("GD_EmployeeNewPensionTrusttxtname");
                FillDropDownList(ddlREL_TYPE_ID, "New Pension Fund Trust");
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
                    if (dt.Rows[0]["IsApproved"].ToString() == "0")
                    {
                        lb.Enabled = true;
                    }
                    else
                    {
                        lb.Enabled = false;
                    }
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnimagedeleteNewPensiontrust_Click(object sender, EventArgs e)
    {
        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        DataTable dt = null;
        if (ViewState["CurrentTableNewPension"] != null)
        {
         
             Label labelrow = (Label)GridViewNewPensionTrust.Rows[rowID].Cells[0].FindControl("lblrowpension");
            dt = (DataTable)ViewState["CurrentTableNewPension"];
            if (labelrow.Text == "0")
            {
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        //ResetRowID(dt);

                    }
                }
            }
            else
            {
                ViewNewPension();
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                    SqlCommand cmd = new SqlCommand();
            
                    cmd.Connection = con;
                    cmd.CommandText = "DeleteRow";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"].ToString();
                    cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(labelrow.Text);
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "7";
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertDelete", "alertDelete();", true);
                        BindUserDetailsNewPension(EMPCode);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
        //Store the current data in ViewState for future reference  
        ViewState["CurrentTableNewPension"] = dt;
        //Re bind the GridView for the updated data  
        GridViewNewPensionTrust.DataSource = dt;
        GridViewNewPensionTrust.DataBind();
        //Set Previous Data on Postbacks  
        SetPreviousDataNewPension();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButtonNewPensionTrust_Click(object sender, EventArgs e)
    {
        if (ViewState["CurrentTableNewPension"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableNewPension"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = 0;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["CurrentTableNewPension"] = dtCurrentTable;
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    Label lblrowpension = (Label)GridViewNewPensionTrust.Rows[i].Cells[0].FindControl("lblrowpension");
                    DropDownList GD_EmployeePensionTrust_txtname = (DropDownList)GridViewNewPensionTrust.Rows[i].Cells[1].FindControl("GD_EmployeeNewPensionTrusttxtname");
                    TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GridViewNewPensionTrust.Rows[i].Cells[2].FindControl("txtrelationship");
                    TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GridViewNewPensionTrust.Rows[i].Cells[3].FindControl("dobcal");
                    TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GridViewNewPensionTrust.Rows[i].Cells[4].FindControl("txtpercentageNewPension");
                    dtCurrentTable.Rows[i]["RowNumber"] = lblrowpension.Text = "0";
                    dtCurrentTable.Rows[i]["FML_MEMBER_NAME"] = GD_EmployeePensionTrust_txtname.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["REL_TYPE_ID"] = GD_EmployeePensionTrust_txtrelationship.Text;
                    dtCurrentTable.Rows[i]["DOB"] = GD_EmployeePensionTrust_dobcal.Text;
                    dtCurrentTable.Rows[i]["PensionShare"] = GD_EmployeePensionTrust_txtpercentage.Text;
                    dtCurrentTable.Rows[i]["EMP_CODE"] = ViewState["EMPCode"].ToString();
                }
                //Rebind the Grid with the current data to reflect changes   
                GridViewNewPensionTrust.DataSource = dtCurrentTable;
                GridViewNewPensionTrust.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks   
        SetPreviousDataNewPension();
    }
    /// <summary>
    /// 
    /// </summary>
    public void ViewNewPension()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableNewPension"];
        int rowIndex = 0;
        if (dtCurrentTable.Rows.Count > 0)
        {
            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            {

                // pension
                Label lblrowpension = (Label)GridViewNewPensionTrust.Rows[rowIndex].Cells[0].FindControl("lblrowpension");
                DropDownList GD_EmployeePensionTrust_txtname = (DropDownList)GridViewNewPensionTrust.Rows[rowIndex].Cells[1].FindControl("GD_EmployeeNewPensionTrusttxtname");
                TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[3].FindControl("dobcal");
                TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[4].FindControl("txtpercentageNewPension");

                if (lblrowpension.Text == "")
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = "0";
                }
                else
                {
                    dtCurrentTable.Rows[i - 1]["RowNumber"] = Convert.ToString(lblrowpension.Text);
                }
                dtCurrentTable.Rows[i - 1]["FML_MEMBER_NAME"] = GD_EmployeePensionTrust_txtname.SelectedItem.Text;
                dtCurrentTable.Rows[i - 1]["REL_TYPE_ID"] = Convert.ToString(GD_EmployeePensionTrust_txtrelationship.Text);
                dtCurrentTable.Rows[i - 1]["DOB"] = Convert.ToString(GD_EmployeePensionTrust_dobcal.Text);
                dtCurrentTable.Rows[i - 1]["PensionShare"] = Convert.ToString(GD_EmployeePensionTrust_txtpercentage.Text);
                dtCurrentTable.Rows[i - 1]["EMP_CODE"] = ViewState["EMPCode"].ToString();
                rowIndex++;
            }
            ViewState["CurrentTableNewPension"] = dtCurrentTable;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Code"></param>
    public void BindUserDetailsNewPension(string Code)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetAllDetailsByEMPCODENewPension";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = Code;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IsApproved"].ToString() == "0")
                {
                    btnNewPensionTrustPrint.Enabled = false;
                    ImageButtonNewPensionTrust.Enabled = true;
                }
                ImageButtonNewPensionTrust.Enabled = false;
                ViewState["CurrentTableNewPension"] = ds.Tables[0];
                GridViewNewPensionTrust.DataSource = ds.Tables[0];
                GridViewNewPensionTrust.DataBind();
                int rowIndex = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        Label lblrowpension = (Label)GridViewNewPensionTrust.Rows[rowIndex].Cells[0].FindControl("lblrowpension");
                        DropDownList GD_EmployeePensionTrust_txtname = (DropDownList)GridViewNewPensionTrust.Rows[rowIndex].Cells[1].FindControl("GD_EmployeeNewPensionTrusttxtname");
                        TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[4].FindControl("txtpercentageNewPension");

                        lblrowpension.Text = Convert.ToString(ds.Tables[0].Rows[i]["RowNumber"]);
                        GD_EmployeePensionTrust_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_EmployeePensionTrust_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                       // GD_EmployeePensionTrust_dobcal.Text = Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["DOB"])).ToShortDateString();
                        GD_EmployeePensionTrust_dobcal.Text = Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                        GD_EmployeePensionTrust_txtpercentage.Text = Convert.ToString(ds.Tables[0].Rows[i]["PensionShare"]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[rowIndex]["PensionShare"]) != "0")
                    {
                        Label lblrowpension = (Label)GridViewNewPensionTrust.Rows[rowIndex].Cells[0].FindControl("lblrowpension");
                        DropDownList GD_EmployeePensionTrust_txtname = (DropDownList)GridViewNewPensionTrust.Rows[rowIndex].Cells[1].FindControl("GD_EmployeeNewPensionTrusttxtname");
                        TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                        TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[3].FindControl("dobcal");
                        TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[4].FindControl("txtpercentageNewPension");

                        lblrowpension.Text = Convert.ToString(ds.Tables[0].Rows[i]["RowNumber"]);
                        GD_EmployeePensionTrust_txtname.Items.FindByText(Convert.ToString(ds.Tables[0].Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_EmployeePensionTrust_txtrelationship.Text = Convert.ToString(ds.Tables[0].Rows[i]["REL_TYPE_ID"]);
                      //  GD_EmployeePensionTrust_dobcal.Text = Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["DOB"])).ToShortDateString();
                        GD_EmployeePensionTrust_dobcal.Text = Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                        GD_EmployeePensionTrust_txtpercentage.Text = Convert.ToString(ds.Tables[0].Rows[i]["PensionShare"]);
                    }
                    rowIndex++;
                }
            }
            else
            {
                SetInitialRowNewPension();
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
    private void SetInitialRowNewPension()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("FML_MEMBER_NAME", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("REL_TYPE_ID", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("DOB", typeof(string)));//for TextBox value   

        dt.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));//for TextBox value  

        dt.Columns.Add(new DataColumn("PensionShare", typeof(string)));//for TextBox value  


        dr = dt.NewRow();
        dr["RowNumber"] = 0;
        dr["FML_MEMBER_NAME"] = string.Empty;
        dr["REL_TYPE_ID"] = string.Empty;
        dr["DOB"] = string.Empty;

        dr["EMP_CODE"] = string.Empty;

        dr["PensionShare"] = string.Empty;

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTableNewPension"] = dt;
        GridViewNewPensionTrust.DataSource = dt;
        GridViewNewPensionTrust.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    private void SetPreviousDataNewPension()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableNewPension"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableNewPension"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    // pension
                    Label lblrowpension = (Label)GridViewNewPensionTrust.Rows[rowIndex].Cells[0].FindControl("lblrowpension");
                    DropDownList GD_EmployeePensionTrust_txtname = (DropDownList)GridViewNewPensionTrust.Rows[rowIndex].Cells[1].FindControl("GD_EmployeeNewPensionTrusttxtname");
                    TextBox GD_EmployeePensionTrust_txtrelationship = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[2].FindControl("txtrelationship");
                    TextBox GD_EmployeePensionTrust_dobcal = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[3].FindControl("dobcal");
                    TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GridViewNewPensionTrust.Rows[rowIndex].Cells[4].FindControl("txtpercentageNewPension");

                    if (i < dt.Rows.Count - 1)
                    {

                        //Assign the value from DataTable to the TextBox  Pension Trust  
                        lblrowpension.Text = Convert.ToString(dt.Rows[i]["RowNumber"]);
                        GD_EmployeePensionTrust_txtname.Items.FindByText(Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"])).Selected = true;
                        GD_EmployeePensionTrust_txtrelationship.Text = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        GD_EmployeePensionTrust_dobcal.Text = Convert.ToString(dt.Rows[i]["DOB"]);
                        GD_EmployeePensionTrust_txtpercentage.Text = Convert.ToString(dt.Rows[i]["PensionShare"]);

                    }
                    rowIndex++;
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void txtpercentageNewPension_TextChanged(object sender, EventArgs e)
    //{
    //    int total = 0;
    //    TextBox lb = (TextBox)sender;
    //    GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
    //    int rowID = gvRow.RowIndex;
    //    TextBox txttotal = (TextBox)GridViewNewPensionTrust.Rows[rowID].Cells[4].FindControl("txtpercentageNewPension");
    //    for (int i = 0; i < GD_EmployeePensionTrust.Rows.Count; i++)
    //    {
    //        TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GridViewNewPensionTrust.Rows[i].Cells[4].FindControl("txtpercentageNewPension");
    //        if (GD_EmployeePensionTrust_txtpercentage.Text != "" && GD_EmployeePensionTrust_txtpercentage.Text != "0")
    //        {
    //            total = total + Convert.ToInt32(GD_EmployeePensionTrust_txtpercentage.Text != "" ? GD_EmployeePensionTrust_txtpercentage.Text : "0");
    //        }
    //    }
    //    if (total == 100)
    //    {

    //    }
    //    else
    //    {
    //        txttotal.Text = "";
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "PercentageMessage();", true);
    //    }
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewPensionTrust_Click(object sender, EventArgs e)
    {
        int val = 0;
        if (GridViewNewPensionTrust.Rows.Count > 0)
        {
            for (int i = 0; i < GridViewNewPensionTrust.Rows.Count; i++)
            {                
                TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GridViewNewPensionTrust.Rows[i].Cells[4].FindControl("txtpercentageNewPension");
                if ((GD_EmployeePensionTrust_txtpercentage.Text == "" || GD_EmployeePensionTrust_txtpercentage.Text == "0"))
                {
                    val = 1;
                }
            }
            if (val == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessageSave();", true);
                return;
            }
            else
            {
                int total = 0;
                for (int i = 0; i < GridViewNewPensionTrust.Rows.Count; i++)
                {
                    TextBox GD_EmployeePensionTrust_txtpercentage = (TextBox)GridViewNewPensionTrust.Rows[i].Cells[4].FindControl("txtpercentageNewPension");
                    if (GD_EmployeePensionTrust_txtpercentage.Text != "" && GD_EmployeePensionTrust_txtpercentage.Text != "0")
                    {
                        total = total + Convert.ToInt32(GD_EmployeePensionTrust_txtpercentage.Text != "" ? GD_EmployeePensionTrust_txtpercentage.Text : "0");
                    }
                }
                if (total == 100)
                {

                }
                else
                {                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "PercentageMessage();", true);
                    return;
                }

                ViewNewPension();
                int l = SaveItemDetailsNewPension((DataTable)ViewState["CurrentTableNewPension"]);
                if (l > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
                    string value = ViewState["EMPCode"].ToString() + "~" + 9;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
                }
            }
        }        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public int SaveItemDetailsNewPension(DataTable dt)
    {
        int k = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = null;
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["PensionShare"])) && Convert.ToString(dt.Rows[i]["PensionShare"]) != "0")
                    {
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "InsertFamilyDetailsNewPension";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[i]["RowNumber"]);
                        cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = ViewState["EMPCode"];
                        cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["DOB"]);
                        cmd.Parameters.Add("@PensionShare", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["PensionShare"]);
                        cmd.Parameters.Add("@FML_MEMBER_NAME", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]);
                        cmd.Parameters.Add("@REL_TYPE_ID", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["REL_TYPE_ID"]);
                        con.Open();
                        k = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                string Message = "Mr./Ms. Gaurav Mehta,The Family Particular of Mr./Mrs." + Convert.ToString(Session["Name"]) + @" has been sent for approval.";
                // SendEmail(Message, "Submission New Pension Trust", "Gaurav.mehta@icsi.edu");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //lblError.Text = ex.Message.ToString();
        }
        return k;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewPensionTrustPreview_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 3;
        ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewPensionRefresh_Click(object sender, EventArgs e)
    {
        BindUserDetailsNewPension(ViewState["EMPCode"].ToString());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewPensionTrustPrint_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 9;
        ScriptManager.RegisterStartupScript(this, GetType(), "Fetch", "Fetch('" + value + @"');", true);
    }
    #endregion

    #region
    protected void btnclubpreview_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 0;
        ScriptManager.RegisterStartupScript(this, GetType(), "Preview", "Preview('" + value + @"');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnclubprint_Click(object sender, EventArgs e)
    {
        string value = ViewState["EMPCode"].ToString() + "~" + 0;
        ScriptManager.RegisterStartupScript(this, GetType(), "Fetch", "Fetch('" + value + @"');", true);
    }
    # endregion
}
