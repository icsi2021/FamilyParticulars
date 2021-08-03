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

public partial class MainFile : System.Web.UI.MasterPage
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblwelcomemessage.Text = Convert.ToString(Session["Name"]);
            lbldate.Text = System.DateTime.Now.ToLongDateString();
            lblLabel.Text = Convert.ToString(Session["UserName"]);
            string URL = Request.Url.AbsolutePath.ToString();
            if (Convert.ToString(Session["UserName"]).ToLower() == "tadaadmin")
            {
                btndashboard.Visible = true;
                btnedit.Visible = false;
                btnmasterdata.Visible = false;
                btnRefresh.Visible = false;
                btnreport.Visible = false;
                btnfamilydetail.Visible = false;
                btnnomineedetails.Visible = false;
                btnnewemployee.Visible = false;
                btnaddrelation.Visible = false;
                btnAtachment.Visible = false;
            }
            else if (Convert.ToString(Session["UserName"]).ToLower() == "tadafinance")
            {
                btndashboard.Visible = true;
                btnedit.Visible = false;
                btnmasterdata.Visible = false;
                btnRefresh.Visible = false;
                btnreport.Visible = true;
                btnfamilydetail.Visible = false;
                btnnomineedetails.Visible = false;
                btnnewemployee.Visible = true;
                btnAtachment.Visible = true;
            }
            else
            {
                btndashboard.Visible = false;
                btnedit.Visible = false;
                btnmasterdata.Visible = true;
                btnRefresh.Visible = false;
                btnreport.Visible = true;
                btnfamilydetail.Visible = true;
                btnnomineedetails.Visible = true;
                btnnewemployee.Visible = false;
                btnAtachment.Visible = false;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void buttonlogout_Click(object sender, EventArgs e)
    {
        Session["UserName"] = string.Empty;
        Session["sub1"] = string.Empty;
        Session.Clear();
        Session.Abandon();
        Session.Remove("UserName");
        Response.Redirect("Default.aspx");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnback_Click(object sender, EventArgs e)
    {
        if (lblwelcomemessage.Text.ToLower() == "tadaadmin")
        {
            Response.Redirect("Dashboard_Admin.aspx");
            // btnback.Visible = true;
            Session["sub1"] = string.Empty;
        }
        else if (lblwelcomemessage.Text.ToLower() == "tadafinance")
        {
            Response.Redirect("Dashboard_Finance.aspx");
        }
        else
        {
            Response.Redirect("Dashboard.aspx");
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnchangepassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("Forget.aspx?UserName=" + lblLabel.Text);
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        if (lblwelcomemessage.Text.ToLower() == "tadaadmin")
        {
            Response.Redirect("ReportFinlAdmin.aspx");
        }
        else
        {
            Response.Redirect("ReportFinl.aspx");
        }
    }
    /// <summary>
    /// Set Permission to Edit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnedit_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowEdit", "ShowEdit();", true);
    }
    /// <summary>
    /// Redirect to Dashboard Page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btndashboard_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["UserName"]).ToLower() == "tadafinance")
        {
            Response.Redirect("AdminDetails.aspx");
        }
        else if (Convert.ToString(Session["UserName"]).ToLower() == "tadaadmin")
        {
            Response.Redirect("AdminDetails.aspx");
        }
        else
        {
            Response.Redirect("FamilyDetails.aspx");
        }
    }
    /// <summary>
    /// Redirect to Master Data Entry Page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnmasterdata_Click(object sender, EventArgs e)
    {
        Response.Redirect("Relation_Master.aspx");
        //  ScriptManager.RegisterStartupScript(this, GetType(), "MasterRelation", "MasterRelation();", true);
    }
    /// <summary>
    /// Nominee Details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnnomineedetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("NomineeDetails.aspx");
    }
    /// <summary>
    /// Family Details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnfamilydetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("FamilyDetails.aspx");
    }
    /// <summary>
    /// Add New Employee
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnnewemployee_Click(object sender, EventArgs e)
    { Response.Redirect("AddEmployee.aspx"); }
    protected void btnaddrelation_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddRelation.aspx");
    }
    protected void Brndasboard_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["UserName"]).ToLower() == "tadafinance")
        {
            Response.Redirect("Dashboard_Finance.aspx");
        }
        else if (Convert.ToString(Session["UserName"]).ToLower() == "tadaadmin")
        {
            Response.Redirect("Dashboard_Admin.aspx");
        }
        else
        {
            Response.Redirect("Dashboard.aspx");
        }


    }
    protected void btnRefresh_Click(object sender, EventArgs e)
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
    public string OracleConn()
    {
        string Host = "192.168.2.20";
        string Port = "1530";
        string Service = "PROD";
        string UserID = "apps";
        string Password = "IcsiProd2017";
        return String.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + Host + ")(PORT=" + Port + "))(CONNECT_DATA=(SERVICE_NAME=" + Service + ")));User Id=" + UserID + ";Password=" + Password + ";");

    }
    protected void btnAtachment_Click(object sender, EventArgs e)
    {
        if (lblwelcomemessage.Text.ToLower() == "tadaadmin")
        {
            Response.Redirect("Attachment.aspx");
        }
    }
}
