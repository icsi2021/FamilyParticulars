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
public partial class Dashboard_Admin : System.Web.UI.Page 
{
    DatabaseFunctions dbClass = new DatabaseFunctions();
  //  CommonFunctions objClass = new CommonFunctions();
  //  string Str = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])) || Convert.ToString(Session["UserName"]) == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            //            else if (Convert.ToString(Session["UserName"]).ToLower() == "finance")
            //            {
            //                btndashboard.Visible = true;
            //                btnedit.Visible = false;
            //                btnmasterdata.Visible = false;
            //                btnRefresh.Visible = false;
            //                btnreport.Visible = true;
            //                btnresetpassword.Visible = false;
            //                btnfamilydetail.Visible = false;
            //                btnnomineedetails.Visible = false;
            //                btnnewemployee.Visible = false;
            //                btnaddrelation.Visible = false;
            //               // Showdetail();
            //            }
            //            else if (Convert.ToString(Session["UserName"]).ToLower() == "admin")
            //            {
            //                btndashboard.Visible = true;
            //                btnedit.Visible = false;
            //                btnmasterdata.Visible = false;
            //                btnRefresh.Visible = false;
            //                btnreport.Visible = true;
            //                btnresetpassword.Visible = true;
            //                btnfamilydetail.Visible = false;
            //                btnnomineedetails.Visible = false;
            //                btnnewemployee.Visible = true;
            //              //  btnaddrelation.Visible = true;
            //               // Showdetail();
            //            }
            //            else
            //            {
            //                btndashboard.Visible = false;
            //                btnedit.Visible = false;
            //                btnmasterdata.Visible = true;
            //                btnRefresh.Visible = false;
            //                btnreport.Visible = true;
            //                btnresetpassword.Visible = true;
            //                btnfamilydetail.Visible = true;
            //                btnnomineedetails.Visible = true;
            //                btnnewemployee.Visible = false;
            //              //  Showdetail();
            //              //  btnaddrelation.Visible = false;
            //            }
            //           // lblwelcomemessage.Text = "Welcome: " + Convert.ToString(Session["Name"]);
            //        }
            //    }
            //    /// <summary>
            //    /// Refresh Data
            //    /// </summary>;
            //    /// <param name="sender"></param>
            //    /// <param name="e"></param>
            //    protected void btnRefresh_Click(object sender, EventArgs e)
            //    {
            //        try
            //        {
            //            DataTable dt = new DataTable();
            //            string Conn = OracleConn();
            //            string Query = @"SELECT DISTINCT ppf.employee_number, PAF.PERSON_ID,
            //                        ppf.full_name,pjt.NAME 'Designation',otl.NAME 'Department',ppf.attribute11 'Branch',
            //                        FU.EMAIL_ADDRESS,
            //                     nvl((SELECT   PPT.user_person_type FROM apps.per_person_types ppt, apps.per_person_type_usages_f ptu WHERE    ptu.person_id = ppf.person_id
            //                    AND ppt.person_type_id = ptu.person_type_id
            //                    and ppt.DEFAULT_FLAG = 'N'
            //                     and   ppt.user_person_type  IN ( 'On Probation', 'Employee', 'Confirmed in the Institute','Retiree', 'Ex-employee', 'Fixed Term Based')
            //                     AND ROWNUM = 1), 'On Probation')'TYPE',FLOOR(MONTHS_BETWEEN (SYSDATE, ppf.date_of_birth)/ 12) age,ppf.date_of_birth 'DOB',
            //                        ppf.original_date_of_hire 'Hire Date',(SELECT pad.address_line1 || CHR(10) || pad.address_line2 || CHR(10) || pad.address_line3 || CHR(10) || ftt.territory_short_name FROM per_addresses pad, fnd_territories_tl ftt WHERE 1=1 AND ppf.person_id = paf.person_id 
            //                    AND pad.person_id = paf.person_id 
            //                    AND ftt.territory_code(+) = pad.country
            //                    AND ROWNUM = 1
            //                    AND PAD.ADDRESS_TYPE = 'IN_P')'PERMANENT_Address',
            //                    (SELECT pad.address_line1 || CHR(10) || pad.address_line2 || CHR(10) || pad.address_line3 || CHR(10) || ftt.territory_short_name FROM per_addresses pad, fnd_territories_tl ftt
            //       WHERE 1=1
            //        AND ppf.person_id = paf.person_id 
            //                    AND pad.person_id = paf.person_id 
            //                    AND ftt.territory_code(+) = pad.country
            //                    AND ROWNUM = 1
            //                    AND PAD.ADDRESS_TYPE = 'IN_C') CURRENT_ADDRESS,
            //                        DECODE (ppf.sex, 'M', 'MALE', 'F', 'FEMALE') gender,
            //                        apps.hr_general.decode_lookup ('MAR_STATUS', ppf.marital_status) marital_status,apps.hr_general.decode_lookup ('NATIONALITY', ppf.nationality) nationality
            //                        FROM per_all_people_f ppf,
            //                        per_all_assignments_f paf,
            //                        per_jobs_tl pjt,
            //                        hr_all_organization_units otl,apps.per_person_types ppt,apps.per_person_type_usages_f ptu,FND_USER FU
            //                  WHERE 1=1
            //                  AND fu.employee_id = ppf.person_id
            //                  AND paf.person_id = ppf.person_id
            //                     AND  ptu.person_id = ppf.person_id
            //                    AND ppt.person_type_id = ptu.person_type_id
            //                     and   ppt.user_person_type  IN ( 'On Probation', 'Employee', 'Confirmed in the Institute',
            //                     'Retiree', 'Ex-employee', 'Fixed Term Based'
            //                     )
            //                    AND pjt.job_id = paf.job_id
            //                    AND otl.organization_id = paf.organization_id
            //                    AND ppf.person_id NOT IN (
            //                           SELECT DISTINCT person_id
            //                                      FROM per_all_people_f
            //                                     WHERE person_type_id = 9
            //                                       AND effective_start_date < SYSDATE)
            //                    AND ppf.effective_start_date =
            //                                          (SELECT MAX (effective_start_date)
            //                                             FROM per_all_people_f
            //                                            WHERE ppf.person_id = person_id)
            //                    AND paf.effective_end_date =(SELECT MAX (effective_end_date)
            //                                               FROM per_all_assignments_f
            //                                              WHERE paf.person_id = person_id)
            //                    AND (SELECT MAX (effective_end_date)
            //                           FROM per_all_assignments_f
            //                          WHERE paf.person_id = person_id) >= SYSDATE
            //                       AND PPF.EMPLOYEE_NUMBER LIKE 'E%'
            //                       ORDER BY PPF.EMPLOYEE_NUMBER";
            //            OracleConnection conn = new OracleConnection(Conn);
            //            OracleCommand cmdGet = new OracleCommand(Query, conn);
            //            conn.Open();
            //            OracleDataAdapter da = new OracleDataAdapter();
            //            da.Fill(dt);
            //            conn.Close();
            //            if (dt != null && dt.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < dt.Rows.Count; i++)
            //                {

            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            ex.Message.ToString();
            //        }
            //    }
            //    /// <summary>
            //    /// Oracle Connection
            //    /// </summary>
            //    public string OracleConn()
            //    {
            //        string Host = "192.168.2.20";
            //        string Port = "1530";
            //        string Service = "PROD";
            //        string UserID = "apps";
            //        string Password = "IcsiProd2017";
            //        return String.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + Host + ")(PORT=" + Port + "))(CONNECT_DATA=(SERVICE_NAME=" + Service + ")));User Id=" + UserID + ";Password=" + Password + ";");


            //    }
            //    /// <summary>
            //    /// Redirect to Report Page
            //    /// </summary>
            //    /// <param name="sender"></param>
            //    /// <param name="e"></param>
            //    protected void btnreport_Click(object sender, EventArgs e)
            //    {
            //        Response.Redirect("ReportFinlAdmin.aspx");
            //    }
            //    /// <summary>
            //    /// Reset Password
            //    /// </summary>
            //    /// <param name="sender"></param>
            //    /// <param name="e"></param>
            //    protected void btnresetpassword_Click(object sender, EventArgs e) 
            //    {
            //        Response.Redirect("Changepwd1.aspx");
            //       // ScriptManager.RegisterStartupScript(this, GetType(), "ResetPassword", "ResetPassword();", true);
            //    }
            //    /// <summary>
            //    /// Set Permission to Edit
            //    /// </summary>
            //    /// <param name="sender"></param>
            //    /// <param name="e"></param>
            //    protected void btnedit_Click(object sender, EventArgs e) 
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "ShowEdit", "ShowEdit();", true);
            //    }
            //    /// <summary>
            //    /// Redirect to Dashboard Page
            //    /// </summary>
            //    /// <param name="sender"></param>
            //    /// <param name="e"></param>
            //    protected void btndashboard_Click(object sender, EventArgs e)
            //    {
            //        if (Convert.ToString(Session["UserName"]).ToLower() == "finance")
            //        {
            //            Response.Redirect("AdminDetails.aspx");
            //        }
            //        else if (Convert.ToString(Session["UserName"]).ToLower() == "admin")
            //        {
            //            Response.Redirect("AdminDetails.aspx");
            //        }
            //        else
            //        {
            //            Response.Redirect("FamilyDetails.aspx");
            //        }
            //    }
            //    /// <summary>
            //    /// Redirect to Master Data Entry Page.
            //    /// </summary>
            //    /// <param name="sender"></param>
            //    /// <param name="e"></param>
            //    protected void btnmasterdata_Click(object sender, EventArgs e) 
            //    {
            //        Response.Redirect("Relation_Master.aspx");
            //      //  ScriptManager.RegisterStartupScript(this, GetType(), "MasterRelation", "MasterRelation();", true);
            //    }
            //    /// <summary>
            //    /// Nominee Details
            //    /// </summary>
            //    /// <param name="sender"></param>
            //    /// <param name="e"></param>
            //    protected void btnnomineedetails_Click(object sender, EventArgs e)
            //    {
            //        Response.Redirect("NomineeDetails.aspx");
            //    }
            //    /// <summary>
            //    /// Family Details
            //    /// </summary>
            //    /// <param name="sender"></param>
            //    /// <param name="e"></param>
            //    protected void btnfamilydetail_Click(object sender, EventArgs e)
            //    {
            //        Response.Redirect("FamilyDetails.aspx");
            //    }
            //    /// <summary>
            //    /// Add New Employee
            //    /// </summary>
            //    /// <param name="sender"></param>
            //    /// <param name="e"></param>
            //    protected void btnnewemployee_Click(object sender, EventArgs e)
            //    { Response.Redirect("AddEmployee.aspx"); }
            //    protected void btnaddrelation_Click(object sender, EventArgs e)
            //    {
            //        Response.Redirect("AddRelation.aspx");
            //    }
            //    protected void Brndasboard_Click(object sender, EventArgs e)
            //    {
            //        Response.Redirect("Dashboard.aspx");
            //    }

            //    //public void Showdetail()
            //    //{

            //    //    SqlParameter[] pr = {                    
            //    //                              new SqlParameter("@aa","1"),
            //    //                              new SqlParameter("@Uname", Session["UserName"])
            //    //                              };
            //    //    DataTable dt = new DataTable();
            //    //    dt = dbClass.getData("Proc_Getallrecords", pr);
            //    //    if (dt.Rows.Count >= 1)
            //    //    {

            //    //        string App = dt.Rows[0]["IsApproved"].ToString();
            //    //        string Rej = dt.Rows[0]["IsReject"].ToString();
            //    //        string Hardcopy = dt.Rows[0]["Ishardcopy"].ToString();

            //    //        if (App == "1")
            //    //        {
            //    //            lblBen1.Text = "Approved";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen1.Text = "Pending";
            //    //        //}

            //    //        if (App == "2")
            //    //        {
            //    //            lblBen1.Text = "Rejected";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen2.Text = "Pending";
            //    //        //}
            //    //        if (Hardcopy == "3")
            //    //        {
            //    //            lblBen3.Text = "Received";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen3.Text = "NA";
            //    //        //}

            //    //    }



            //    //                 SqlParameter[] pr1 = {                    
            //    //                              new SqlParameter("@aa","2"),
            //    //                              new SqlParameter("@Uname", Session["UserName"])
            //    //                              };
            //    //    DataTable dt1 = new DataTable();
            //    //    dt1 = dbClass.getData("Proc_Getallrecords", pr1);
            //    //    if (dt1.Rows.Count >= 1)
            //    //    {

            //    //        string App = dt1.Rows[0]["IsApproved"].ToString();
            //    //        string Rej = dt1.Rows[0]["IsReject"].ToString();
            //    //        string Hardcopy = dt1.Rows[0]["Ishardcopy"].ToString();

            //    //        if(App == "1")
            //    //        {
            //    //            lblBen4.Text = "Approved";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //  lblBen4.Text = "Pending";
            //    //        //}

            //    //        if (App == "2")
            //    //        {
            //    //            lblBen4.Text = "Rejected";
            //    //        }
            //    //         //else
            //    //         //{
            //    //         //    lblBen4.Text = "Pending";
            //    //         //}
            //    //        if (Hardcopy == "3")
            //    //         {
            //    //             lblBen5.Text = "Received";
            //    //         }
            //    //        // else
            //    //        // {
            //    //         //   lblBen6.Text = "NA";
            //    //        // }

            //    //    }

            //    //    SqlParameter[] pr2 = {                    
            //    //                              new SqlParameter("@aa","3"),
            //    //                              new SqlParameter("@Uname", Session["UserName"])
            //    //                              };
            //    //    DataTable dt2 = new DataTable();
            //    //    dt2 = dbClass.getData("Proc_Getallrecords", pr2);
            //    //    if (dt2.Rows.Count >= 1)
            //    //    {

            //    //        string App = dt2.Rows[0]["IsApproved"].ToString();
            //    //        string Rej = dt2.Rows[0]["IsReject"].ToString();
            //    //        string Hardcopy = dt2.Rows[0]["Ishardcopy"].ToString();

            //    //        if (App == "1")
            //    //        {
            //    //            lblBen7.Text = "Approved";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen7.Text = "Pending";
            //    //        //}

            //    //        if (App == "2")
            //    //        {
            //    //            lblBen7.Text = "Rejected";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen8.Text = "Pending";
            //    //        //}
            //    //        if (Hardcopy == "3")
            //    //        {
            //    //            lblBen8.Text = "Recieved";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen9.Text = "NA";
            //    //        //}

            //    //    }


            //    //    SqlParameter[] pr3 = {                    
            //    //                              new SqlParameter("@aa","4"),
            //    //                              new SqlParameter("@Uname", Session["UserName"])
            //    //                              };
            //    //    DataTable dt3 = new DataTable();
            //    //    dt3 = dbClass.getData("Proc_Getallrecords", pr3);
            //    //    if (dt3.Rows.Count >= 1)
            //    //    {

            //    //        string App = dt3.Rows[0]["IsApproved"].ToString();
            //    //        string Rej = dt3.Rows[0]["IsReject"].ToString();
            //    //        string Hardcopy = dt3.Rows[0]["Ishardcopy"].ToString();

            //    //        if (App == "1")
            //    //        {
            //    //            lblBen10.Text = "Approved";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen10.Text = "Pending";
            //    //        //}

            //    //        if (App == "2")
            //    //        {
            //    //            lblBen10.Text = "Rejected";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen11.Text = "Pending";
            //    //        //}
            //    //        if (Hardcopy == "3")
            //    //        {
            //    //            lblBen11.Text = "Received";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen12.Text = "NA";
            //    //        //}

            //    //    }

            //    //    SqlParameter[] pr4 = {                    
            //    //                              new SqlParameter("@aa","5"),
            //    //                              new SqlParameter("@Uname", Session["UserName"])
            //    //                              };
            //    //    DataTable dt4 = new DataTable();
            //    //    dt4 = dbClass.getData("Proc_Getallrecords", pr4);
            //    //    if (dt4.Rows.Count >= 1)
            //    //    {

            //    //        string App = dt4.Rows[0]["IsApproved"].ToString();
            //    //        string Rej = dt4.Rows[0]["IsReject"].ToString();
            //    //        string Hardcopy = dt4.Rows[0]["Ishardcopy"].ToString();

            //    //        if (App == "1")
            //    //        {
            //    //            lblBen13.Text = "Approved";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen13.Text = "Pending";
            //    //        //}

            //    //        if (App == "2")
            //    //        {
            //    //            lblBen13.Text = "Rejected";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen14.Text = "Pending";
            //    //        //}
            //    //        if (Hardcopy == "3")
            //    //        {
            //    //            lblBen14.Text = "Recieved";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen15.Text = "NA";
            //    //        //}

            //    //    }

            //    //    SqlParameter[] pr5 = {                    
            //    //                              new SqlParameter("@aa","6"),
            //    //                              new SqlParameter("@Uname", Session["UserName"])
            //    //                              };
            //    //    DataTable dt5 = new DataTable();
            //    //    dt5 = dbClass.getData("Proc_Getallrecords", pr5);
            //    //    if (dt5.Rows.Count >= 1)
            //    //    {

            //    //        string App = dt5.Rows[0]["IsApproved"].ToString();
            //    //        string Rej = dt5.Rows[0]["IsReject"].ToString();
            //    //        string Hardcopy = dt5.Rows[0]["Ishardcopy"].ToString();

            //    //        if (App == "1")
            //    //        {
            //    //            lblBen16.Text = "Approved";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen16.Text = "Pending";
            //    //        //}

            //    //        if (App == "2")
            //    //        {
            //    //            lblBen16.Text = "Rejected";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen17.Text = "Pending";
            //    //        //}
            //    //        if (Hardcopy == "3")
            //    //        {
            //    //            lblBen17.Text = "Recieved";
            //    //        }
            //    //        //else
            //    //        //{
            //    //        //    lblBen18.Text = "NA";
            //    //        //}

            //    //    }



            //    //  }

        }
    }
   
}

