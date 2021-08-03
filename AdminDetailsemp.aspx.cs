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
public partial class AdminDetailsemp : System.Web.UI.Page 
{
    string aa = "";
    string str="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])) || Convert.ToString(Session["UserName"]) == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                aa = Request.QueryString["Myid"].ToString();
                str = aa;
                str = str.Replace("'", String.Empty);
                hddstr1.Text = str.ToString();
                gridUserDetails.DataBind();
                gridUserDetails1.DataBind();
                gridUserDetails2.DataBind();
                gridUserDetails3.DataBind();
                gridUserDetails4.DataBind();
                gridUserDetails5.DataBind();
                //BindGrid();
               // BindGrid_ProvidentShare();
               // BindGrid_NewPensionTrust();
            }
            if (Convert.ToString(Session["UserName"]) == "Finance")
            {
            //    btnedit.Visible = false;
            //    btnfetch.Visible = false;
            //    btnnonedit.Visible = false;
            //    btnallreport.Visible = false;
            //    gridUserDetails.Columns[12].Visible = false;
            //    gridUserDetails.Columns[15].Visible = false;
            //    gridUserDetails.Columns[16].Visible = false;
            //    if (gridUserDetails.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < gridUserDetails.Rows.Count; i++)
            //        {
            //            Label lblstatus = (Label)gridUserDetails.Rows[i].FindControl("lblstatus");
            //            if (lblstatus.Text == "REJECTED")
            //            {
            //                gridUserDetails.Rows[i].Visible = false;
            //            }
            //        }
            //    }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approved")
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gridUserDetails.Rows[index];
                LinkButton lnktext = (LinkButton)row.FindControl("lnkapproved");
                Label empcode = (Label)row.FindControl("lblempcode");
                Label lblemail = (Label)row.FindControl("lblemail");
                int i = 0;
                if (lnktext.Text == "ACTIVE")
                {
                    i = 0;
                }
                else { i = 1; }
                string value = empcode.Text + "~" + lblemail.Text + "~" + i;
                ScriptManager.RegisterStartupScript(this, GetType(), "showmessage", "showmessage('" + value + "');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Refresh", "Refresh();", true);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblError.Text = ex.Message.ToString();
            }
        }
        if (e.CommandName == "View")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridUserDetails.Rows[index];
            Label empcode = (Label)row.FindControl("lblempcode");            
            Response.Redirect("View.aspx?ID=" + empcode.Text);
        }
        if (e.CommandName == "APPROVE")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridUserDetails.Rows[index];
            Label empcode = (Label)row.FindControl("lblempcode");
            Label lblemail = (Label)row.FindControl("lblemail");
            Label lblempname = (Label)row.FindControl("lblempname");
            Label lbldesignation = (Label)row.FindControl("lbldesignation");
            Session["EmailView"] = lblemail.Text;
            Session["NameView"] = lblempname.Text;
            Session["DesignationView"] = lbldesignation.Text;
            var Values = "View.aspx?ID=" + empcode.Text;
            ScriptManager.RegisterStartupScript(this, GetType(), "View", "View('" + Values + @"');", true);
            //Response.Redirect("View.aspx?ID=" + empcode.Text);
        }
        if (e.CommandName == "Login")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridUserDetails.Rows[index];
            Label empcode = (Label)row.FindControl("lblempcode");            
            LinkButton lnkactive = (LinkButton)row.FindControl("lnkactive");
            int i = 0;
            if (lnkactive.Text == "ACTIVE")
            {
                i = 0;
            }
            else { i = 1; }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "LoginActive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = empcode.Text;
            cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = i;
            con.Open();
            int k = cmd.ExecuteNonQuery();
            con.Close();
            BindGrid();
        }
        if (e.CommandName == "Report")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridUserDetails.Rows[index];
            Label empcode = (Label)row.FindControl("lblempcode");
            //Response.Redirect("Report.aspx?ID=" + empcode.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "Fetch", "Fetch('" + empcode.Text+"~"+"0" + @"');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "Refresh", "Refresh();", true);
            gridUserDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (e.CommandName == "Editable")
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gridUserDetails.Rows[index];
                LinkButton lnkedit = (LinkButton)row.FindControl("lnkedit");
                Label empcode = (Label)row.FindControl("lblempcode");
                int i = 0;
                if (lnkedit.Text == "Non Edit")
                {
                    i = 0;
                }
                else { i = 1; }
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Editable";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = empcode.Text;
                cmd.Parameters.Add("@IsEdit", SqlDbType.NVarChar).Value = i.ToString();
                con.Open();
                int k = cmd.ExecuteNonQuery();
                con.Close();
                BindGrid();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblError.Text = ex.Message.ToString();
            }
        }
        if (e.CommandName == "Reset")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridUserDetails.Rows[index];
            LinkButton lnkreset = (LinkButton)row.FindControl("lnkreset");
            Label empcode = (Label)row.FindControl("lblempcode");
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                if (!string.IsNullOrEmpty(empcode.Text))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "Resetpassword";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = empcode.Text;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        lblError.Visible = true;
                        lblError.Text = "Password Reset.. Login Again!";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        if (e.CommandName == "Attach")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridUserDetails.Rows[index];
            Label lblempcode = (Label)row.FindControl("lblempcode");
            if (lblempcode.Text != "")
            {
                var Values = "UploadAttachment.aspx?EMP=" + lblempcode.Text + "&Name=0";
                ScriptManager.RegisterStartupScript(this, GetType(), "UplaodDocs", "UplaodDocs('" + Values + @"');", true);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void BindGrid()
    {
        try
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])) || Session["UserName"] == null)
            { Response.Redirect("Default.aspx"); }
            else
            {
              string qq =  Session["UserName"].ToString();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
               // cmd.CommandText = "GetEmployeeDetails";
                cmd.CommandText = "Proc_GetAlldata_hardcopyapprove";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@aa", '1');
                cmd.Parameters.Add("@EMP_CODE", hddstr1.Text); //Session["UserName"].ToString());
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridUserDetails.DataSource = dt;
                    gridUserDetails.DataBind();
                    gridUserDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con1;
                // cmd.CommandText = "GetEmployeeDetails";
                cmd1.CommandText = "Proc_GetAlldata_hardcopyapprove";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@aa", '2');
                cmd1.Parameters.Add("@EMP_CODE", hddstr1.Text);//Session["UserName"].ToString());
                con1.Open();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt1);
                con1.Close();
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    gridUserDetails1.DataSource = dt1;
                    gridUserDetails1.DataBind();
                    gridUserDetails1.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    gridUserDetails1.DataSource = null;
                    gridUserDetails1.DataBind();
                }


                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con2;
                // cmd.CommandText = "GetEmployeeDetails";
                cmd2.CommandText = "Proc_GetAlldata_hardcopyapprove";
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@aa", '3');
                cmd2.Parameters.Add("@EMP_CODE", hddstr1.Text);//Session["UserName"].ToString());
                con2.Open();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                con2.Close();
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    gridUserDetails2.DataSource = dt2;
                    gridUserDetails2.DataBind();
                    gridUserDetails2.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    gridUserDetails2.DataSource = null;
                    gridUserDetails2.DataBind();
                }


                SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = con3;
                // cmd.CommandText = "GetEmployeeDetails";
                cmd3.CommandText = "Proc_GetAlldata_hardcopyapprove";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@aa", '4');
                cmd3.Parameters.Add("@EMP_CODE", hddstr1.Text);//Session["UserName"].ToString());
                con3.Open();
                DataTable dt3 = new DataTable();
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                da3.Fill(dt3);
                con3.Close();
                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    gridUserDetails3.DataSource = dt3;
                    gridUserDetails3.DataBind();
                    gridUserDetails3.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    gridUserDetails3.DataSource = null;
                    gridUserDetails3.DataBind();
                }

                SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                SqlCommand cmd4 = new SqlCommand();
                cmd4.Connection = con4;
                // cmd.CommandText = "GetEmployeeDetails";
                cmd4.CommandText = "Proc_GetAlldata_hardcopyapprove";
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.Parameters.Add("@aa", '5');
                cmd4.Parameters.Add("@EMP_CODE", hddstr1.Text);//Session["UserName"].ToString());
                con4.Open();
                DataTable dt4 = new DataTable();
                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                da4.Fill(dt4);
                con4.Close();
                if (dt4 != null && dt4.Rows.Count > 0)
                {
                    gridUserDetails4.DataSource = dt4;
                    gridUserDetails4.DataBind();
                    gridUserDetails4.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    gridUserDetails4.DataSource = null;
                    gridUserDetails4.DataBind();
                }

                SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                SqlCommand cmd5 = new SqlCommand();
                cmd5.Connection = con5;
                // cmd.CommandText = "GetEmployeeDetails";
                cmd5.CommandText = "Proc_GetAlldata_hardcopyapprove";
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.Parameters.Add("@aa", '6');
                cmd5.Parameters.Add("@EMP_CODE", hddstr1.Text);//Session["UserName"].ToString());
                con5.Open();
                DataTable dt5 = new DataTable();
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                da5.Fill(dt5);
                con5.Close();
                if (dt5 != null && dt5.Rows.Count > 0)
                {
                    gridUserDetails5.DataSource = dt5;
                    gridUserDetails5.DataBind();
                    gridUserDetails5.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    gridUserDetails5.DataSource = null;
                    gridUserDetails5.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            lblError.Text = ex.Message.ToString();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "Refresh", "Refresh();", true);
    }  

   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void gridUserDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gridUserDetails.PageIndex = e.NewPageIndex;
    //    BindGrid();
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnedit_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "AllEditable";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@IsEdit", SqlDbType.NVarChar).Value = 0;
        con.Open();
        int k = cmd.ExecuteNonQuery();
        con.Close();
        BindGrid();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnnonedit_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "AllEditable";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@IsEdit", SqlDbType.NVarChar).Value = 1;
        con.Open();
        int k = cmd.ExecuteNonQuery();
        con.Close();
        BindGrid();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnallreport_Click(object sender, EventArgs e)
    {
        Response.Redirect("Detail.aspx");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnfetch_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string Conn = OracleConn();
            string Query = @"SELECT DISTINCT ppf.employee_number, PAF.PERSON_ID,
                        ppf.full_name,pjt.NAME 'Designation',otl.NAME 'Department',ppf.attribute11 'Branch',
                        FU.EMAIL_ADDRESS,
                     nvl((SELECT   PPT.user_person_type FROM apps.per_person_types ppt, apps.per_person_type_usages_f ptu WHERE    ptu.person_id = ppf.person_id
                    AND ppt.person_type_id = ptu.person_type_id
                    and ppt.DEFAULT_FLAG = 'N'
                     and   ppt.user_person_type  IN ( 'On Probation', 'Employee', 'Confirmed in the Institute','Retiree', 'Ex-employee', 'Fixed Term Based')
                     AND ROWNUM = 1), 'On Probation')'TYPE',FLOOR(MONTHS_BETWEEN (SYSDATE, ppf.date_of_birth)/ 12) age,ppf.date_of_birth 'DOB',
                        ppf.original_date_of_hire 'Hire Date',(SELECT pad.address_line1 || CHR(10) || pad.address_line2 || CHR(10) || pad.address_line3 || CHR(10) || ftt.territory_short_name FROM per_addresses pad, fnd_territories_tl ftt WHERE 1=1 AND ppf.person_id = paf.person_id 
                    AND pad.person_id = paf.person_id 
                    AND ftt.territory_code(+) = pad.country
                    AND ROWNUM = 1
                    AND PAD.ADDRESS_TYPE = 'IN_P')'PERMANENT_Address',
                    (SELECT pad.address_line1 || CHR(10) || pad.address_line2 || CHR(10) || pad.address_line3 || CHR(10) || ftt.territory_short_name FROM per_addresses pad, fnd_territories_tl ftt
       WHERE 1=1
        AND ppf.person_id = paf.person_id 
                    AND pad.person_id = paf.person_id 
                    AND ftt.territory_code(+) = pad.country
                    AND ROWNUM = 1
                    AND PAD.ADDRESS_TYPE = 'IN_C') CURRENT_ADDRESS,
                        DECODE (ppf.sex, 'M', 'MALE', 'F', 'FEMALE') gender,
                        apps.hr_general.decode_lookup ('MAR_STATUS', ppf.marital_status) marital_status,apps.hr_general.decode_lookup ('NATIONALITY', ppf.nationality) nationality
                        FROM per_all_people_f ppf,
                        per_all_assignments_f paf,
                        per_jobs_tl pjt,
                        hr_all_organization_units otl,apps.per_person_types ppt,apps.per_person_type_usages_f ptu,FND_USER FU
                  WHERE 1=1
                  AND fu.employee_id = ppf.person_id
                  AND paf.person_id = ppf.person_id
                     AND  ptu.person_id = ppf.person_id
                    AND ppt.person_type_id = ptu.person_type_id
                     and   ppt.user_person_type  IN ( 'On Probation', 'Employee', 'Confirmed in the Institute',
                     'Retiree', 'Ex-employee', 'Fixed Term Based'
                     )
                    AND pjt.job_id = paf.job_id
                    AND otl.organization_id = paf.organization_id
                    AND ppf.person_id NOT IN (
                           SELECT DISTINCT person_id
                                      FROM per_all_people_f
                                     WHERE person_type_id = 9
                                       AND effective_start_date < SYSDATE)
                    AND ppf.effective_start_date =
                                          (SELECT MAX (effective_start_date)
                                             FROM per_all_people_f
                                            WHERE ppf.person_id = person_id)
                    AND paf.effective_end_date =(SELECT MAX (effective_end_date)
                                               FROM per_all_assignments_f
                                              WHERE paf.person_id = person_id)
                    AND (SELECT MAX (effective_end_date)
                           FROM per_all_assignments_f
                          WHERE paf.person_id = person_id) >= SYSDATE
                       AND PPF.EMPLOYEE_NUMBER LIKE 'E%'
                       ORDER BY PPF.EMPLOYEE_NUMBER";
            OracleConnection conn = new OracleConnection(Conn);
            OracleCommand cmdGet = new OracleCommand(Query, conn);
            conn.Open();
            OracleDataAdapter da = new OracleDataAdapter();
            da.Fill(dt);
            conn.Close();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                  
                }
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
    /// <returns></returns>
    public string OracleConn()
    {
        string Host = "192.168.2.20";
        string Port = "1530";
        string Service = "PROD";
        string UserID = "apps";
        string Password = "IcsiProd2017";
        return String.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + Host + ")(PORT=" + Port + "))(CONNECT_DATA=(SERVICE_NAME=" + Service + ")));User Id=" + UserID + ";Password=" + Password + ";");
    }
    /// <summary>
    /// 
    /// </summary>
    private void SendEmail(string Message, string Email)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("192.168.2.29");
            mail.From = new MailAddress("donotreply@icsi.edu");
            mail.To.Add("vinay.singh@in2ittech.com");
            if (!string.IsNullOrEmpty(Email) && Email != "")
            {
                mail.CC.Add(Email);
            }
            mail.Subject = "Test Mail";
            mail.Body = Message;
            SmtpServer.Credentials = new System.Net.NetworkCredential("donotreply@icsi.edu", "password@123", "ICSINOIDA");
            SmtpServer.Send(mail);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
    protected void submit1_Click(object sender, EventArgs e)
    {
        try
        {
            //family detail

            SqlConnection con15 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);//and Apprd ='1' and Status='1'
            SqlCommand cmd15 = new SqlCommand();
            cmd15.Connection = con15;
            // cmd.CommandText = "GetEmployeeDetails";
            cmd15.CommandText = "Select * from FAMILY_DETAIL where  ModiDate between '" + dtpFromDate.Value + "' AND '" + dtpToDate.Value + "' and EMP_CODE='" + hddstr1.Text + "' and Apprd ='1' and Status='1' order by ModiDate asc";
           // cmd15.CommandType = CommandType.StoredProcedure;
            //  cmd5.Parameters.Add("@aa", '6');
            // cmd5.Parameters.Add("@EMP_CODE", "E0107");//Session["UserName"].ToString());
            con15.Open();
            DataTable dt15 = new DataTable();
            SqlDataAdapter da15 = new SqlDataAdapter(cmd15);
            da15.Fill(dt15);
            con15.Close();
            //gridUserDetails.Columns.Clear();
            if (dt15 != null && dt15.Rows.Count > 0)
            {
                gridUserDetails.DataSource = dt15;
                gridUserDetails.DataBind();
                gridUserDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                gridUserDetails.DataSource = null;
                gridUserDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        // Providen Dtail
        try
        {
            SqlConnection con16 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);//and Apprd ='1' and Status='1'
            SqlCommand cmd16 = new SqlCommand();
            cmd16.Connection = con16;
            // cmd.CommandText = "GetEmployeeDetails";
            cmd16.CommandText = "Select * from ProvidentShare where  ModiDate between '" + dtpFromDate.Value + "' AND '" + dtpToDate.Value + "' and EMP_CODE='" + hddstr1.Text + "' and Apprd ='1' and Status='1' order by ModiDate asc";
           // cmd15.CommandType = CommandType.StoredProcedure;
            //  cmd5.Parameters.Add("@aa", '6');
            // cmd5.Parameters.Add("@EMP_CODE", "E0107");//Session["UserName"].ToString());
            con16.Open();
            DataTable dt16 = new DataTable();
            SqlDataAdapter da16 = new SqlDataAdapter(cmd16);
            da16.Fill(dt16);
            con16.Close();
           // gridUserDetails1.Columns.Clear();
            if (dt16 != null && dt16.Rows.Count > 0)
            {
                gridUserDetails1.DataSource = dt16;
                gridUserDetails1.DataBind();
                gridUserDetails1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                gridUserDetails1.DataSource = null;
                gridUserDetails1.DataBind();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

        // Pension Dtail
        try
        {
            SqlConnection con17 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd17 = new SqlCommand();
            cmd17.Connection = con17;
            // cmd.CommandText = "GetEmployeeDetails";
            cmd17.CommandText = "Select * from NewPensionTrust where  ModiDate between '" + dtpFromDate.Value + "' AND '" + dtpToDate.Value + "' and EMP_CODE='" + hddstr1.Text + "' and Apprd ='1' and Status='1' order by ModiDate asc";
            // cmd15.CommandType = CommandType.StoredProcedure;
            //  cmd5.Parameters.Add("@aa", '6');
            // cmd5.Parameters.Add("@EMP_CODE", "E0107");//Session["UserName"].ToString());
            con17.Open();
            DataTable dt17 = new DataTable();
            SqlDataAdapter da17 = new SqlDataAdapter(cmd17);
            da17.Fill(dt17);
            con17.Close();
           // gridUserDetails2.Columns.Clear();
            if (dt17 != null && dt17.Rows.Count > 0)
            {
                gridUserDetails2.DataSource = dt17;
                gridUserDetails2.DataBind();
                gridUserDetails2.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                gridUserDetails2.DataSource = null;
                gridUserDetails2.DataBind();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        //GratuityShare
        try
        {
            SqlConnection con18 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd18 = new SqlCommand();
            cmd18.Connection = con18;
            // cmd.CommandText = "GetEmployeeDetails";
            cmd18.CommandText = "Select * from GratuityShare where  ModiDate between '" + dtpFromDate.Value + "' AND '" + dtpToDate.Value + "' and EMP_CODE='" + hddstr1.Text + "' and Apprd ='1' and Status='1' order by ModiDate asc";
            // cmd15.CommandType = CommandType.StoredProcedure;
            //  cmd5.Parameters.Add("@aa", '6');
            // cmd5.Parameters.Add("@EMP_CODE", "E0107");//Session["UserName"].ToString());
            con18.Open();
            DataTable dt18 = new DataTable();
            SqlDataAdapter da18 = new SqlDataAdapter(cmd18);
            da18.Fill(dt18);
            con18.Close();
           // gridUserDetails3.Columns.Clear();
            if (dt18 != null && dt18.Rows.Count > 0)
            {
                gridUserDetails3.DataSource = dt18;
                gridUserDetails3.DataBind();
                gridUserDetails3.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                gridUserDetails3.DataSource = null;
                gridUserDetails3.DataBind();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

        //BenevolentShare
        try
        {
            SqlConnection con19 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd19 = new SqlCommand();
            cmd19.Connection = con19;
            // cmd.CommandText = "GetEmployeeDetails";
            cmd19.CommandText = "Select * from BenevolentShare where  ModiDate between '" + dtpFromDate.Value + "' AND '" + dtpToDate.Value + "' and EMP_CODE='" + hddstr1.Text + "' and Apprd ='1' and Status='1' order by ModiDate asc";
            // cmd15.CommandType = CommandType.StoredProcedure;
            //  cmd5.Parameters.Add("@aa", '6');
            // cmd5.Parameters.Add("@EMP_CODE", "E0107");//Session["UserName"].ToString());
            con19.Open();
            DataTable dt19 = new DataTable();
            SqlDataAdapter da19 = new SqlDataAdapter(cmd19);
            da19.Fill(dt19);
            con19.Close();
           // gridUserDetails4.Columns.Clear();
            if (dt19 != null && dt19.Rows.Count > 0)
            {
                gridUserDetails4.DataSource = dt19;
                gridUserDetails4.DataBind();
                gridUserDetails4.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                gridUserDetails4.DataSource = null;
                gridUserDetails4.DataBind();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

        //EncashShare
        try
        {
            SqlConnection con20 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd20 = new SqlCommand();
            cmd20.Connection = con20;
            // cmd.CommandText = "GetEmployeeDetails";
            cmd20.CommandText = "Select * from BenevolentShare where  ModiDate between '" + dtpFromDate.Value + "' AND '" + dtpToDate.Value + "' and EMP_CODE='" + hddstr1.Text + "' and Apprd ='1' and Status='1' order by ModiDate asc";
            // cmd15.CommandType = CommandType.StoredProcedure;
            //  cmd5.Parameters.Add("@aa", '6');
            // cmd5.Parameters.Add("@EMP_CODE", "E0107");//Session["UserName"].ToString());
            con20.Open();
            DataTable dt20 = new DataTable();
            SqlDataAdapter da20 = new SqlDataAdapter(cmd20);
            da20.Fill(dt20);
            con20.Close();
           // gridUserDetails5.Columns.Clear();
            if (dt20 != null && dt20.Rows.Count > 0)
            {
                gridUserDetails5.DataSource = dt20;
                gridUserDetails5.DataBind();
                gridUserDetails5.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                gridUserDetails5.DataSource = null;
                gridUserDetails5.DataBind();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
}
