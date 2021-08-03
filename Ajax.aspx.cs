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
using System.Net.Mail;
using System.Globalization;
public partial class Ajax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.QueryString.Count > 0)
        {
            ProcessValues(HttpContext.Current.Request.QueryString["ReqCase"].ToString(), HttpContext.Current.Request.QueryString["ReqVal"].ToString());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strcase"></param>
    /// <param name="strValue"></param>
    public void ProcessValues(string strcase, string strValue)
    {
        switch (strcase)
        {
            case "StatusApiCall":
                string value = strValue;
                if (value.Split('~')[1] == "0") { Response.Write(GetAllReport(strValue)); }
                else if (value.Split('~')[1] != "0") { Response.Write(GetData(strValue)); }
                break;
            case "SetMessage":
                Response.Write(SetMessage(strValue));
                break;
            case "SetMessageNew":
                Response.Write(SetMessageNew(strValue));
                break;

        }
        Response.End();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string GetData(string value)
    {

        string Entity = string.Empty;
        string empcode = value.Split('~')[0];
        string Index = value.Split('~')[1];
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "GetAllDetailsByEMPCODEReport";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = empcode;
        cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Index;
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        string EMPNAME = string.Empty;
        string Designation = string.Empty;
        string Marital = string.Empty;
        if (ds.Tables.Count > 1)
        {
            if (ds.Tables[0] != null || ds.Tables[1] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    EMPNAME = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                    Designation = Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION"]);
                    Marital = Convert.ToString(ds.Tables[0].Rows[0]["MARITAL_STATUS"]);
                    if (Index == "1" || Index == "0")
                    {
                        Entity += @"<div>
            <p style='text-align:center'><img src='Images/Header.png'/></p>
            <p><strong>DECLARATION OF FAMILY PARTICULARS</strong></p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='29'>
                            <p><b>1.</b></p>
                        </td>
                        <td width='272'>
                            <p><b>Name of the employee</b></p>
                        </td>
                        <td width='443'>
                            <b>" + Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]) + @"</b>
                        </td>
                    </tr>
                    <tr>
                        <td width='29'>
                            <p><b>2.</b></p>
                        </td>
                        <td width='272'>
                            <p><b>Designation</b></p>
                        </td>
                        <td width='443'>
                        <b>" + Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION"]) + @"</b>
                        </td>
                    </tr>
                    <tr>
                        <td width='29'>
                            <p><b>3.</b></p>
                        </td>
                        <td width='272'>
                            <p><b>Marital Status ( Married/ Unmarried)</b></p>
                        </td>
                        <td width='443'>
                            <b>" + Convert.ToString(ds.Tables[0].Rows[0]["MARITAL_STATUS"]) + @"</b>
                        </td>
                    </tr>
                </tbody>
            </table>";
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            Entity += @"<p><strong>PARTICULARS OF FAMILY MEMBERS</strong></p>
            <p><strong>[ Fill in details of all family members i.e&nbsp; Husband/Wife/Children/ Parents
                    or in laws (in case of married females] </strong>
            </p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='192'>
                            <p>
                                <strong>Name</strong></p>
                        </td>
                        <td width='96'>
                            <p>
                                <strong>Relationship with the employee</strong></p>
                        </td>
                        <td width='72'>
                            <p>
                                <strong>Date of Birth</strong></p>
                        </td>
                        <td width='48'>
                            <p>
                                <strong>Age</strong></p>
                        </td>
                        <td width='72'>
                            <p>
                                <strong>Monthly income</strong></p>
                        </td>
                        <td width='108'>
                            <p>
                                <strong>Profession/ Occupation</strong></p>
                        </td>
                        <td width='84'>
                            <p>
                                <strong>Dependent on Employee Yes/No</strong></p>
                        </td>
                        <td width='72'>
                            <p>
                                <strong>Whether staying with the employee </strong>
                            </p>
                            <p>
                                <strong>Yes/No</strong></p>
                        </td>
                    </tr>
                </tbody>";
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                string Age = "10";
                                // DateTime dateOfBirth = Convert.ToDateTime(Convert.ToString(ds.Tables[1].Rows[i]["DOB"]));
                                string datedb = Convert.ToString(ds.Tables[1].Rows[i]["DOB"]);
                                string[] dd = null;
                                string date = string.Empty;
                                if (datedb.Contains("/"))
                                {
                                    dd = datedb.Split('/');
                                    date = dd[0] + "/" + dd[1] + "/" + dd[2];
                                }
                                else
                                {
                                    dd = datedb.Split('-');
                                    date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                                }
                                // DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                                DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                                string Dept = Convert.ToString(ds.Tables[1].Rows[i]["DEPENT_ON_EMP"]) == "1" ? "Yes" : "No";
                                string Stay = Convert.ToString(ds.Tables[1].Rows[i]["STAY_WITH_EMP"]) == "1" ? "Yes" : "No";
                                Entity += @"<tr>
                          <td width='192'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                          <td width='96'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                          <td width='72'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["DOB"]) + @"</p></td>
                          <td width='48'><p>" + Age + @"</p></td>
                          <td width='72'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["MTHLY_INCOME"]) + @"</p></td>
                          <td width='108'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["OCCUPATION"]) + @"</p></td>
                          <td width='84'><p>" + Dept + @"</p></td>
                          <td width='72'><p>" + Stay + @"</p></td>
                          <tr>";
                            }
                            Entity += @"</table>";
                            Entity += @"<p><strong>&nbsp;</strong>( For the definition of Dependents please refer <strong>AnnexureA </strong>)</p>
            <p>I hereby declare that :-</p>
            <ul>
                <li>The information given above is complete, true and correct to the best of my knowledge
                    and belief:</li>
                <li>I shall inform Directorates of HR and Finance &amp; Accounts about any change in
                    the above</li>
                <li>(i) <strong>(*)</strong> My spouse is employed in_________________________________________________________________</li>
            </ul>
            <p>
                and he/she is not availing any medical benefit/ LTC from that office as per enclosed
                certificate issued by the employer</p>
            <p>
                OR</p>
            <p>
                (ii) <strong>(</strong><strong>*)</strong> &nbsp;My spouse is not employed in any
                office.</p>
            <p>
                <strong>&nbsp;&nbsp;&nbsp;&nbsp; (*)</strong> &nbsp;Strike out if not applicable.</p>
            <p>
                &nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='348'>
                            <p><strong>Employee Signature </strong>
                            </p>
                        </td>
                        <td colspan='2' width='396'>
                            <p><strong>Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Signature</p>
                        </td>
                        <td width='192'>
                        </td>
                        <td width='166'>
                            <p>Signature</p>
                        </td>
                        <td width='230'>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Name</p>
                        </td>
                        <td width='192'>
                            " + EMPNAME + @"
                        </td>
                        <td width='166'>
                            <p>Name</p>
                        </td>
                        <td width='230'>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Designation</p>
                        </td>
                        <td width='192'>
                            " + Designation + @"
                        </td>
                        <td width='166'>
                            <p>Designation</p>
                        </td>
                        <td width='230'>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Place</p>
                        </td>
                        <td width='192'>
                        </td>
                        <td width='166'>
                            <p>Date</p>
                        </td>
                        <td width='230'>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Date</p>
                        </td>
                        <td width='192'>
                        </td>
                        <td width='166'>
                        </td>
                        <td width='230'>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                Forwarded to Directorate of F&amp;A &nbsp;on _______________&nbsp; &nbsp;&nbsp;-
                <strong>( for Office use only )</strong></p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>
                        </td>
                        <td width='240'>
                        </td>
                        <td width='228'>
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                         <p>Signature</p>
                        </td>
                        <td width='240'>
                        </td>
                        <td width='228'>
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                         <p>(HR)</p>
                        </td>
                        <td width='240'>
                        </td>
                        <td width='228'>
                        </td>
                    </tr>
                </tbody>
            </table>";
                        }
                    }
                }
                //Benevoent Fund
                if (Index == "5" || Index == "0")
                {
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p>The Secretary</p>
            <p>ICSI Employees Benevolent Fund</p>
            <p>C/o The Institute of Company Secretaries of India</p>
            <p>ICSI &nbsp;House, 22, Institutional Area , Lodi Road, NewDelhi-110003</p>
            <p>&nbsp;</p>
            <p>Dear Sir,</p>
            <p>I hereby apply for admission as a subscriber member of the ICSI Employee's Benevolent
                Fund. I have read the Memorandum of Association &amp; Bye Laws of the Fund and I
                agree to abide by them and or as amended hereafter I give below the necessary particulars.</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='60'><p>1</p></td>
                        <td width='326'><p>Name in Full</p></td>
                        <td width='334'></td></tr>
                        <tr><td width='60'><p>2</p></td>
                        <td width='326'><p>Sex( M/F)</p></td>
                        <td width='334'></td>
                    </tr>
                    <tr>
                        <td width='60'><p>3</p></td>
                        <td width='326'><p>Marital Status ( Married/ Unmarried)</p></td>
                        <td width='334'></td>
                    </tr>
                    <tr><td width='60'><p>4</p></td>
                        <td width='326'><p>
                                Bank Account Number with the name of the bank &amp; Branch&nbsp; in case you have
                                an account with Canara bank</p>
                        </td>
                        <td width='334'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='60'>
                            <p>5</p>
                        </td>
                        <td colspan='2' width='660'>
                            <p>Present /&nbsp; Communication&nbsp; Address</p>                           
                        </td>
                    </tr>
                </tbody>
            </table>
            <p><strong>Name(s) of Dependents : ( Please mention the names of dependents only ) </strong></p>
            <p>( Note: For the definition of Dependents please refer <strong>Annexure A </strong>)</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='43'>
                            <p><strong>S.No.</strong></p>
                        </td>
                        <td width='161'>
                            <p><strong>Name{s)</strong></p>
                        </td>
                        <td width='90'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='78'>
                            <p><strong>Age</strong></p>
                        </td>
                        <td width='348'>
                            <p><strong>Relation to subscriber</strong></p>
                        </td>
                    </tr>";
                        int Cnt = 0;
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            Cnt = Cnt + 1;
                            string Age = "10";
                            string datedb = Convert.ToString(ds.Tables[1].Rows[i]["DOB"]);
                            string[] dd = null;
                            string date = string.Empty;
                            if (datedb.Contains("/"))
                            {
                                dd = datedb.Split('/');
                                date = dd[0] + "/" + dd[1] + "/" + dd[2];
                            }
                            else
                            {
                                dd = datedb.Split('-');
                                date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                            }
                            // DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                            DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                            Entity += @"<tr>
                        <td width='43'><p>" + Cnt + @"</p></td>
                        <td width='161'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='90'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["DOB"]) + @"</p></td>
                        <td width='78'><p>" + Age + @"</p></td>
                        <td width='348'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                    </tr>";
                        }
                        Entity += @"</tbody>
            </table>
            <p><strong>&nbsp;</strong></p>
            <p><strong>Declaration</strong></p>
            <p>
                I hereby authorise that the monthly contribution towards ICSI Employees' Benevolent
                Fund may be deducted every month from my salary and be added to the Corpus of the
                Fund.</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='348'>
                            <p>
                                <strong>Details of Employee</strong></p>
                        </td>
                        <td colspan='2' width='372'>
                            <p>
                                <strong>Details of Witness</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>
                                Signature</p>
                           
                        </td>
                        <td width='192'>
                            
                        </td>
                        <td width='166'>
                            <p>
                                Signature</p>
                        </td>
                        <td width='206'>
                          
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Name</p>
                           
                        </td>
                        <td width='192'>
                            " + EMPNAME + @"
                        </td>
                        <td width='166'>
                            <p>
                                Name</p>
                        </td>
                        <td width='206'>
                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>
                                Designation</p>
                            
                        </td>
                        <td width='192'>
                            " + Designation + @"
                        </td>
                        <td width='166'>
                            <p>
                                Designation</p>
                        </td>
                        <td width='206'>
                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Place</p>                          
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Date</p>
                        </td>
                        <td width='206'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Date</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>                            
                        </td>
                        <td width='206'>                           
                        </td>
                    </tr>
                </tbody>
            </table>           
            <p>
                Forwarded to Directorate of F&amp;A on _______________&nbsp; &nbsp;- <strong>( for Office
                    use only ) </strong>
            </p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='240'>                          
                        </td>
                        <td width='228'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='240'>                          
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                </tbody>
            </table>";
                    }
                }
                //Encashment Fund
                if (Index == "6" || Index == "0")
                {
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p><strong>FORM OF NOMINATION UNDER THE PAYMENT EL/HPL </strong><strong>ENCASHMENT AMOUNT</strong></p>
            <p><strong>&nbsp;</strong></p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='183'>
                            <p>Name of the employee</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Sex (M/F)</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Father's / Husband&rsquo;s Name</p>
                        </td>
                        <td width='549'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Marital Status</p>
                        </td>
                        <td width='549'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Date of Birth</p>
                        </td>
                        <td width='549'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Permanent Address</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>                           
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                I hereby nominate the person(s) mentioned below to receive the amount of EL/HPL
                Encashment &nbsp;in the event of my death before the amount becomes payable, or
                having become payable, has not been paid and direct that the said amount shall be
                distributed among the said person(s) in the manner shown against their name.</p>
            <p><strong>&nbsp;</strong></p>
            <p><strong>&nbsp;</strong></p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='163'>
                            <p><strong>Name of nominee or nominees</strong></p>
                        </td>
                        <td width='197'>
                            <p><strong>Nominee's relationship with the employee</strong></p>
                        </td>
                        <td width='151'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='65'>
                            <p><strong>Age</strong></p>
                        </td>
                        <td width='156'>
                            <p><strong>Percentage of&nbsp; Amount of share to be paid to each nominee</strong></p>
                        </td>
                    </tr>";
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            string Age = "10";
                            string datedb = Convert.ToString(ds.Tables[1].Rows[i]["DOB"]);
                            string[] dd = null;
                            string date = string.Empty;
                            if (datedb.Contains("/"))
                            {
                                dd = datedb.Split('/');
                                date = dd[0] + "/" + dd[1] + "/" + dd[2];
                            }
                            else
                            {
                                dd = datedb.Split('-');
                                date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                            }
                            //  DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                            DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                            Entity += @"<tr>
                        <td width='197'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='151'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                        <td width='65'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["DOB"]) + @"</p></td>
                        <td width='156'><p>" + Age + @"</p></td>
                        <td width='197'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["EncashShare"]) + @"</p></td>
                    </tr>";
                        }
                        Entity += @"</tbody>
            </table>            
            <p>
                Dated __________________________ ( mention the Date/ Month/Year)</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='348'>
                            <p>
                                <strong>Employee Signature </strong>
                            </p>
                            <p>
                                <strong>&nbsp;</strong></p>
                        </td>
                        <td colspan='2' width='384'>
                            <p><strong>&nbsp;Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Signature</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Signature</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Name</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Name</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>
                                Designation</p>
                            
                        </td>
                        <td width='192'>
                            
                        </td>
                        <td width='166'>
                            <p>Designation</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Place</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Place</p>                            
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Date</p>                            
                        </td>
                        <td width='192'>                           
                        </td>
                        <td width='166'>
                            <p>Date</p>                           
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                </tbody>
            </table>           
            <p>
                Forwarded to Directorate of F&amp;A &nbsp;on _______________&nbsp; &nbsp;&nbsp;-
                <strong>( for Office use only )</strong> &nbsp;&nbsp;</p>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>                          
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                           
                        </td>
                    </tr>
                </tbody>
            </table>";
                    }
                }
                // Gratuity Fund
                if (Index == "4" || Index == "0")
                {
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p><strong>FORM OF NOMINATION &lsquo;FORM NO. 40 A&rsquo;</strong></p>
            <p>&nbsp;(for persons not covered under the Payment of Gratuity Act)</p>
            <p>Gratuity Fund</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='183'>
                            <p>Name of the employee</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Sex</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Religion</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Father's Name</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Husband&rsquo;s Name</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr><td width='183'>
                            <p>Marital Status</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Date of Birth</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Permanent Address</p>
                        </td>
                        <td width='549'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>                            
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                I hereby nominate the person(s) mentioned below to receive the amount of Gratuity
                in the event of my death before the amount becomes payable, or having become payable,
                has not been paid and direct that the said amount shall be distributed among the
                said person(s) in the manner shown against their name.</p>
            <p>
                <strong>&nbsp;</strong></p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='288'>
                            <p><strong>Name and address of nominee or nominees</strong></p>
                        </td>
                        <td width='108'>
                            <p><strong>Nominee's relationship </strong><strong>with the employee</strong></p>
                        </td>
                        <td width='96'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='108'>
                            <p><strong>Age of nominee</strong></p>
                        </td>
                        <td width='132'>
                            <p><strong>Amount or share of gratuity to be paid to each nominee</strong></p>
                        </td>
                    </tr>";
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            string Age = "10";
                            string datedb = Convert.ToString(ds.Tables[1].Rows[i]["DOB"]);
                            string[] dd = null;
                            string date = string.Empty;
                            if (datedb.Contains("/"))
                            {
                                dd = datedb.Split('/');
                                date = dd[0] + "/" + dd[1] + "/" + dd[2];
                            }
                            else
                            {
                                dd = datedb.Split('-');
                                date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                            }
                            //  DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                            DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                            Entity += @"<tr>
                        <td width='288'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='108'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                        <td width='96'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["DOB"]) + @"</p></td>
                        <td width='108'><p>" + Age + @"</p></td>
                        <td width='132'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["GratuityShare"]) + @"</p></td>
                    </tr>";
                        }
                        Entity += @"</tbody>
            </table>            
            <ol>
                <li>Certified that I have no family and should I acquire a family hereafter, the above
                    mentioned should be deemed as cancelled.</li>
                <li>Certified that my father / mother is /are dependent upon me.</li>
            </ol>
            <p>Dated___________________(mention the Date/Month/Year )</p>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='240'>
                            <p><strong>Employee Signature </strong>
                            </p>                            
                        </td>
                        <td colspan='2' width='252'>
                            <p><strong>1<sup>st</sup>&nbsp; Witness Signature</strong></p>
                        </td>
                        <td colspan='2' width='240'>
                            <p><strong>2<sup>nd</sup> Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Signature</p>                            
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='108'>
                            <p>Signature</p>
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='96'>
                            <p>Signature</p>
                        </td>
                        <td width='144'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Name</p>                            
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='108'>
                            <p>Name</p>
                        </td>
                        <td width='144'>
                        " + EMPNAME + @"                            
                        </td>
                        <td width='96'>
                            <p>Name</p>
                        </td>
                        <td width='144'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Designation</p>                            
                        </td>
                        <td width='144'>  
                        " + Designation + @"                          
                        </td>
                        <td width='108'>
                            <p>Designation</p>
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='96'>
                            <p>Designation</p>
                        </td>
                        <td width='144'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Place</p>                           
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='108'>
                            <p>Place</p>                          
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='96'>
                            <p>Place</p>                            
                        </td>
                        <td width='144'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Date</p>                            
                        </td>
                        <td width='144'>
                            <p>&nbsp;</p>
                        </td>
                        <td width='108'>
                            <p>Date</p>                            
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='96'>
                            <p>Date</p>                            
                        </td>
                        <td width='144'>                            
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                <strong>____________________________________________________( for Office use only )</strong>
                _________________________________________</p>
            <p>&nbsp;</p>
            <p>
                Certified that the above declaration has been signed by Shri / Smt. _____________________________
                before me after he/she has read the entries.</p>
            <p>
                ____________________</p>
            <p>&nbsp;Signature of the Trustee</p>";
                    }
                }
                //Pension Trust
                if (Index == "3" || Index == "0")
                {
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {

                        Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p>
                Forwarded to Directorate of F&amp;A&nbsp; on _______________&nbsp; &nbsp;&nbsp;</p>            
            <p>
                The Trustees of the Institute of Company</p>
            <p>
                Secretaries of India Employees New Pension Fund Trust</p>
            <p>
                &ldquo;ICSI House&rdquo;</p>
            <p>
                22, Institutional Area</p>
            <p>
                Lodi Road,</p>
            <p>
                New Delhi &ndash; 110 003</p>
            <p>
                &nbsp;</p>
            <p>
                I,_________________________________________________(NAME OF THE MEMBER IN BLOCK
                LETTERS) s/o/d/o/w/o Shri/Smt._________________________________ hereby nominate
                the persons mentioned below to receive the amount that may stand to my credit in
                the New Pension Fund Trust in the event of my death before that amount has become
                payable or having become payable, and has not been paid, direct that the said amount
                shall be distributed among the said persons in the manner shown against their names
                :</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='163'>
                            <p><strong>Name of nominee or nominees</strong></p>
                        </td>
                        <td width='197'>
                            <p><strong>Nominee's relationship with the employee</strong></p>
                        </td>
                        <td width='151'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='65'>
                            <p><strong>Age</strong></p>
                        </td>
                        <td width='156'>
                            <p><strong>Percentage of&nbsp; Amount of share to be paid to each nominee</strong></p>
                        </td>
                    </tr>";
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            string Age = "10";
                            string datedb = Convert.ToString(ds.Tables[1].Rows[i]["DOB"]);
                            string[] dd = null;
                            string date = string.Empty;
                            if (datedb.Contains("/"))
                            {
                                dd = datedb.Split('/');
                                date = dd[0] + "/" + dd[1] + "/" + dd[2];
                            }
                            else
                            {
                                dd = datedb.Split('-');
                                date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                            }
                            // DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                            DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                            Entity += @"<tr>
                        <td width='163'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='197'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                        <td width='151'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["DOB"]) + @"</p></td>
                        <td width='65'><p>" + Age + @"</p></td>
                        <td width='156'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["PensionShare"]) + @"</p></td>
                    </tr>";
                        }
                        Entity += @"</tbody>
            </table>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='348'>
                            <p><strong>Employee Signature </strong>
                            </p>
                            <p><strong>&nbsp;</strong></p>
                        </td>
                        <td colspan='2' width='384'>
                            <p><strong>&nbsp;Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Signature</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Signature</p>
                        </td>
                        <td width='218'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Name</p>                            
                        </td>
                        <td width='192'>
                        " + EMPNAME + @"                            
                        </td>
                        <td width='166'>
                            <p>Name</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Designation</p>                            
                        </td>
                        <td width='192'> 
                        " + Designation + @"                          
                        </td>
                        <td width='166'>
                            <p>Designation</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Place</p>                           
                        </td>
                        <td width='192'>
                            
                        </td>
                        <td width='166'>
                            <p>Place</p>                            
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Date</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Date</p>                            
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                </tbody>
            </table>            
            <p>
                Forwarded to Directorate of F&amp;A&nbsp; on _______________&nbsp; &nbsp;&nbsp;-
                <strong>( for Office use only )</strong> &nbsp;&nbsp;</p>
            <p>nbsp;</p>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='240'>                           
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                </tbody>
            </table>";
                    }
                }
                //New Pension Fund Trsut
                if (Index == "9" || Index == "0")
                {
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {

                        Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p>
                Forwarded to Directorate of F&amp;A&nbsp; on _______________&nbsp; &nbsp;&nbsp;</p>            
            <p>
                The Trustees of the Institute of Company</p>
            <p>
                Secretaries of India Employees New Pension Fund Trust</p>
            <p>
                &ldquo;ICSI House&rdquo;</p>
            <p>
                22, Institutional Area</p>
            <p>
                Lodi Road,</p>
            <p>
                New Delhi &ndash; 110 003</p>
            <p>
                &nbsp;</p>
            <p>
                I,_________________________________________________(NAME OF THE MEMBER IN BLOCK
                LETTERS) s/o/d/o/w/o Shri/Smt._________________________________ hereby nominate
                the persons mentioned below to receive the amount that may stand to my credit in
                the New Pension Fund Trust in the event of my death before that amount has become
                payable or having become payable, and has not been paid, direct that the said amount
                shall be distributed among the said persons in the manner shown against their names
                :</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='163'>
                            <p><strong>Name of nominee or nominees</strong></p>
                        </td>
                        <td width='197'>
                            <p><strong>Nominee's relationship with the employee</strong></p>
                        </td>
                        <td width='151'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='65'>
                            <p><strong>Age</strong></p>
                        </td>
                        <td width='156'>
                            <p><strong>Percentage of&nbsp; Amount of share to be paid to each nominee</strong></p>
                        </td>
                    </tr>";
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            string Age = "10";
                            string datedb = Convert.ToString(ds.Tables[1].Rows[i]["DOB"]);
                            string[] dd = null;
                            string date = string.Empty;
                            if (datedb.Contains("/"))
                            {
                                dd = datedb.Split('/');
                                date = dd[0] + "/" + dd[1] + "/" + dd[2];
                            }
                            else
                            {
                                dd = datedb.Split('-');
                                date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                            }
                            // DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                            DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                            Entity += @"<tr>
                        <td width='163'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='197'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                        <td width='151'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["DOB"]) + @"</p></td>
                        <td width='65'><p>" + Age + @"</p></td>
                        <td width='156'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["PensionShare"]) + @"</p></td>
                    </tr>";
                        }
                        Entity += @"</tbody>
            </table>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='348'>
                            <p><strong>Employee Signature </strong>
                            </p>
                            <p><strong>&nbsp;</strong></p>
                        </td>
                        <td colspan='2' width='384'>
                            <p><strong>&nbsp;Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Signature</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Signature</p>
                        </td>
                        <td width='218'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Name</p>                            
                        </td>
                        <td width='192'>
                        " + EMPNAME + @"                            
                        </td>
                        <td width='166'>
                            <p>Name</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Designation</p>                            
                        </td>
                        <td width='192'> 
                        " + Designation + @"                          
                        </td>
                        <td width='166'>
                            <p>Designation</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Place</p>                           
                        </td>
                        <td width='192'>
                            
                        </td>
                        <td width='166'>
                            <p>Place</p>                            
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Date</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Date</p>                            
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                </tbody>
            </table>            
            <p>
                Forwarded to Directorate of F&amp;A&nbsp; on _______________&nbsp; &nbsp;&nbsp;-
                <strong>( for Office use only )</strong> &nbsp;&nbsp;</p>
            <p>nbsp;</p>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='240'>                           
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                </tbody>
            </table>";
                    }
                }
                // Provident Fund
                if (Index == "2" || Index == "0")
                {
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p><strong>(Form of modifying previous Nomination under Provident Fund)</strong></p>
            <p>No.</p>
            <p>Folio No.</p>
            <p>The Trustees of the Institute of Company</p>
            <p>Secretaries of India Employees Provident Fund</p>
            <p>&ldquo;ICSI House&rdquo;</p>
            <p>22, Institutional Area</p>
            <p>Lodi Road,</p>
            <p>New Delhi &ndash; 110 003</p>
            <p>&nbsp;</p>
            <p>
                I,_________________________________________________(NAME OF THE MEMBER IN BLOCK
                LETTERS) s/o/d/o/w/o Shri/Smt.________________________________ employed as____________________________
                in the service of the Institute of Company Secretaries of&nbsp; India hereby cancel
                the nomination made by me previously as regards the disposal of the amount that
                may stand&nbsp; to my credit in the provident fund in the event of my death before
                that amount has become payable or, having&nbsp; become payable, has not been paid,
                and direct that the said amount shall be distributed among the said persons in the
                manner shown against their names:</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='125'>
                            <p>
                                <strong>Name and Address of&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Nominee or Nominees</strong></p>
                        </td>
                        <td width='150'>
                            <p><strong>Nominee's relationship with the employee</strong></p>
                        </td>
                        <td width='87'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='62'>
                            <p><strong>Age</strong></p>
                        </td>
                        <td width='162'>
                            <p>
                                <strong>If the nominee is a minor, state&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; the name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    of the guardian&nbsp; and the relationship with the member</strong></p>
                        </td>
                        <td width='150'>
                            <p><strong>**Amount or</strong></p>
                            <p><strong>Share of accumulations in the Provident Fund to be paid to each nominee</strong></p>
                        </td>
                    </tr>";
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            string Age = "10";
                            string datedb = Convert.ToString(ds.Tables[1].Rows[i]["DOB"]);
                            string[] dd = null;
                            string date = string.Empty;
                            if (datedb.Contains("/"))
                            {
                                dd = datedb.Split('/');
                                date = dd[0] + "/" + dd[1] + "/" + dd[2];
                            }
                            else
                            {
                                dd = datedb.Split('-');
                                date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                            }
                            // DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                            DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                            string ISMinor = Convert.ToString(ds.Tables[1].Rows[i]["ProvidentIsMinor"]) == "1" ? "Yes" : "No";
                            Entity += @"<tr>
                        <td width='125'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='150'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                        <td width='87'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["DOB"]) + @"</p></td>
                        <td width='62'><p>" + Age + @"</p></td>
                        <td width='162'><p>" + ISMinor + @"</p></td>
                        <td width='150'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["ProvidentShare"]) + @"</p></td>
                    </tr>";
                        }
                        Entity += @"</tbody>
            </table>           
            <p>
                Certified that my marital status is________________________(State whether unmarried,
                married or widow/widower)</p>
            <p>&nbsp;</p>
            <p>
                *Certified that I have no family as defined in the explanation under clause 6 of
                the Trust Deed and should I acquire a family hereafter, the above nomination should
                be deemed as cancelled.</p>
            <p>&nbsp;</p>
            <p>
                **Certified that my father/mother/sister(s)/minor brother(s) is/are dependent upon
                me.</p>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='240'>
                            <p><strong>Employee Signature </strong></p>                            
                        </td>
                        <td colspan='2' width='252'>
                            <p><strong>1<sup>st</sup>&nbsp; Witness Signature</strong></p>
                        </td>
                        <td colspan='2' width='256'>
                            <p><strong>2<sup>nd</sup> Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Signature</p>                            
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='108'>
                            <p>Signature</p>
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='96'>
                            <p>Signature</p>
                        </td>
                        <td width='160'>                          
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Name</p>                            
                        </td>
                        <td width='144'>
                        " + EMPNAME + @"                            
                        </td>
                        <td width='108'>
                            <p>Name</p>
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='96'>
                            <p>Name</p>
                        </td>
                        <td width='160'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Designation</p>                           
                        </td>
                        <td width='144'>
                            " + Designation + @"                          
                        </td>
                        <td width='108'>
                            <p>Designation</p>
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='96'>
                            <p>Designation</p>
                        </td>
                        <td width='160'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Date</p>                            
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='108'>                           
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='96'>
                            <p>Date</p>
                        </td>
                        <td width='160'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Address</p>                            
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='108'>
                            <p>Address</p>
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='96'>
                            <p>Address</p>
                        </td>
                        <td width='160'>                          
                        </td>
                    </tr>
                </tbody>
            </table>            
            <p>
                Forwarded to Directorate of F&amp;A&nbsp; on _______________&nbsp; &nbsp;&nbsp;-
                <strong>( for Office use only )</strong> &nbsp;&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>                            
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='140'>                           
                        </td>
                        <td width='128'>                            
                        </td>
                            <td width='264'>
                            <p>Date</p>
                        </td>
                        <td width='140'>                           
                        </td>
                        <td width='128'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='140'>                            
                        </td>
                        <td width='128'>                           
                        </td>
                            <td width='264'>
                            <p>Place</p>
                        </td>
                        <td width='140'>                           
                        </td>
                        <td width='128'>                            
                        </td>
                    </tr>
                </tbody>
            </table>";
                    }
                }
                if (Index == "7")
                {
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p>
                The Secretary</p>
            <p>
                ICSI Employee's Club</p>
            <p>
                C/o The Institute of Company Secretaries of India</p>
            <p>
                ICSI House, 22, Institutional Area</p>
            <p>
                Lodi Road</p>
            <p>
                New Delhi-110003</p>
            <p>
                Dear Sir,</p>
            <p>
                I hereby apply for admission as a member of the ICSI Employees' Club. I have read
                the Bye laws of the club and I agree to abide by them and or as amended hereafter.
                I give below the necessary particulars.</p>            
            <p>
                &nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='48'>
                            <p>1</p>
                        </td>
                        <td width='326'>
                            <p>Name in Full</p>                            
                        </td>
                        <td width='334'> 
                            " + EMPNAME + @"                              
                        </td>
                    </tr>
                    <tr>
                        <td width='48'>
                            <p>2</p>
                        </td>
                        <td width='326'>
                            <p>Designation</p>                            
                        </td>
                        <td width='334'> 
                            " + Designation + @"                             
                        </td>
                    </tr>
                    <tr>
                        <td width='48'>
                            <p>3</p>
                        </td>
                        <td width='326'>
                            <p>Marital Status ( Married/ Unmarried)</p>                            
                        </td>
                        <td width='334'> 
                            " + Marital + @"                           
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>I hereby declare and authorise that the monthly contribution towards ICSI Employees
                Club may be deducted every month from my salary and be credited to the ICSI Employees
                Club account</p>            
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='288'>
                            <p>Place :</p>
                        </td>
                        <td width='420'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='288'>
                            <p>Date :" + System.DateTime.Now.ToShortDateString() + @"</p>
                        </td>
                        <td width='420'>
                            <p>Signature of the Employee :</p>                            
                        </td>
                    </tr>
                </tbody>
            </table>            
            <p>&nbsp;</p>
            <p>
                Forwarded to President, ICSI Employee&nbsp; Club &nbsp;on _______________ - <strong>
                    ( for Office use only )</strong></p>           
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>                            
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='140'>                           
                        </td>
                        <td width='128'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                </tbody>
            </table>";
                    }
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data not fund');", true);
            // return;
        }
        return Entity;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string SetMessage(string value)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "ApproveReject";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = value.Split('~')[0];
        cmd.Parameters.Add("@IsApproved", SqlDbType.NVarChar).Value = value.Split('~')[2];
        cmd.Parameters.Add("@comments", SqlDbType.NVarChar).Value = value.Split('~')[3];
        con.Open();
        int j = cmd.ExecuteNonQuery();
        if (j > 0)
        {
            if (value.Split('~')[2] != "0")
            {
                // SendEmail("Approved  By HR", value.Split('~')[1]);
            }
            else
            {
                // SendEmail("Rejected By HR", value.Split('~')[1]);
            }
        }
        con.Close();
        return j.ToString();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string SetMessageNew(string value)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "ApproveRejectByForm";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = value.Split('~')[0];
        cmd.Parameters.Add("@IsApproved", SqlDbType.NVarChar).Value = value.Split('~')[1];
        cmd.Parameters.Add("@Form", SqlDbType.NVarChar).Value = value.Split('~')[2];
        cmd.Parameters.Add("@comments", SqlDbType.NVarChar).Value = value.Split('~')[4];
        con.Open();
        int j = cmd.ExecuteNonQuery();
        if (j > 0)
        {
            if (value.Split('~')[1] == "1")
            {
                string Subject = "Approval of" + value.Split('~')[2];
                string Message = "Mr./Ms. (" + Session["NameView"].ToString() + "),(" + Session["DesignationView"].ToString() + ")Your (" + value.Split('~')[2] + ") has been approved by the HR Directorate. Please take a printout of the Form, Fill the Forms/ Forms in all respects and send the signed copy to the HR Directorate. HR Directorate";
                SendEmail(Message, value.Split('~')[3], Subject);
            }
            else if (value.Split('~')[1] == "2")
            {
                string Subject = "Rejection of" + value.Split('~')[2];
                string Message = "Mr./Ms. (" + Session["NameView"].ToString() + "),(" + Session["DesignationView"].ToString() + ")Your (" + value.Split('~')[2] + ") has been rejected by the HR Directorate. The following are the Comments of the HR Directorate" + value.Split('~')[4] + ".”Please resubmit the Form after making necessary changes.  HR Directorate";
                SendEmail(Message, value.Split('~')[3], Subject);
            }
            else if (value.Split('~')[1] == "3")
            {
                string Subject = "Receipt of HardCopy of" + value.Split('~')[2];
                string Message = "Mr./Ms. (" + Session["NameView"].ToString() + "),(" + Session["DesignationView"].ToString() + ")The hardcopy of your (" + value.Split('~')[2] + ") has been received by the HR Directorate.HR Directorate";
                SendEmail(Message, value.Split('~')[3], Subject);
            }
        }
        con.Close();
        return j.ToString();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="Email"></param>
    private void SendEmail(string Message, string Email, string Subject)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("192.168.2.29");
            mail.From = new MailAddress("donotreply@icsi.edu");
            mail.To.Add(Email);
            if (!string.IsNullOrEmpty(Email) && Email != "")
            {
                mail.CC.Add(Email);
            }
            mail.Subject = Subject;
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
    /// <param name="value"></param>
    /// <returns></returns>
    public string GetAllReport(string value)
    {
        string Entity = string.Empty;
        string empcode = value.Split('~')[0];
        string Index = value.Split('~')[1];
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "GetAllDetailsByEMPCODEReport";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = empcode;
        cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Index;
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        string EMPNAME = string.Empty;
        string Designation = string.Empty;
        string Marital = string.Empty;
        if (ds.Tables[0] != null || ds.Tables[1] != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                EMPNAME = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                Designation = Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION"]);
                Marital = Convert.ToString(ds.Tables[0].Rows[0]["MARITAL_STATUS"]);
                if (Index == "0")
                {
                    Entity += @"<div>
            <p style='text-align:center'><img src='Images/Header.png'/></p>
            <p><strong>DECLARATION OF FAMILY PARTICULARS</strong></p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='29'>
                            <p><b>1.</b></p>
                        </td>
                        <td width='272'>
                            <p><b>Name of the employee</b></p>
                        </td>
                        <td width='443'>
                            <b>" + Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]) + @"</b>
                        </td>
                    </tr>
                    <tr>
                        <td width='29'>
                            <p><b>2.</b></p>
                        </td>
                        <td width='272'>
                            <p><b>Designation</b></p>
                        </td>
                        <td width='443'>
                        <b>" + Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION"]) + @"</b>
                        </td>
                    </tr>
                    <tr>
                        <td width='29'>
                            <p><b>3.</b></p>
                        </td>
                        <td width='272'>
                            <p><b>Marital Status ( Married/ Unmarried)</b></p>
                        </td>
                        <td width='443'>
                            <b>" + Convert.ToString(ds.Tables[0].Rows[0]["MARITAL_STATUS"]) + @"</b>
                        </td>
                    </tr>
                </tbody>
            </table>";
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        Entity += @"<p><strong>PARTICULARS OF FAMILY MEMBERS</strong></p>
            <p><strong>[ Fill in details of all family members i.e&nbsp; Husband/Wife/Children/ Parents
                    or in laws (in case of married females] </strong>
            </p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='192'>
                            <p>
                                <strong>Name</strong></p>
                        </td>
                        <td width='96'>
                            <p>
                                <strong>Relationship with the employee</strong></p>
                        </td>
                        <td width='72'>
                            <p>
                                <strong>Date of Birth</strong></p>
                        </td>
                        <td width='48'>
                            <p>
                                <strong>Age</strong></p>
                        </td>
                        <td width='72'>
                            <p>
                                <strong>Monthly income</strong></p>
                        </td>
                        <td width='108'>
                            <p>
                                <strong>Profession/ Occupation</strong></p>
                        </td>
                        <td width='84'>
                            <p>
                                <strong>Dependent on Employee Yes/No</strong></p>
                        </td>
                        <td width='72'>
                            <p>
                                <strong>Whether staying with the employee </strong>
                            </p>
                            <p>
                                <strong>Yes/No</strong></p>
                        </td>
                    </tr>
                </tbody>";
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            string Age = "10";
                            string datedb = Convert.ToString(ds.Tables[1].Rows[i]["DOB"]);
                            string[] dd = null;
                            string date = string.Empty;
                            if (datedb.Contains("/"))
                            {
                                dd = datedb.Split('/');
                                date = dd[0] + "/" + dd[1] + "/" + dd[2];
                            }
                            else
                            {
                                dd = datedb.Split('-');
                                date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                            }
                            // DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                            DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                            string Dept = Convert.ToString(ds.Tables[1].Rows[i]["DEPENT_ON_EMP"]) == "1" ? "Yes" : "No";
                            string Stay = Convert.ToString(ds.Tables[1].Rows[i]["STAY_WITH_EMP"]) == "1" ? "Yes" : "No";
                            Entity += @"<tr>
                          <td width='192'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                          <td width='96'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                          <td width='72'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["DOB"]) + @"</p></td>
                          <td width='48'><p>" + Age + @"</p></td>
                          <td width='72'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["MTHLY_INCOME"]) + @"</p></td>
                          <td width='108'><p>" + Convert.ToString(ds.Tables[1].Rows[i]["OCCUPATION"]) + @"</p></td>
                          <td width='84'><p>" + Dept + @"</p></td>
                          <td width='72'><p>" + Stay + @"</p></td>
                          <tr>";
                        }
                        Entity += @"</table>";
                        Entity += @"<p><strong>&nbsp;</strong>( For the definition of Dependents please refer <strong>AnnexureA </strong>)</p>
            <p>I hereby declare that :-</p>
            <ul>
                <li>The information given above is complete, true and correct to the best of my knowledge
                    and belief:</li>
                <li>I shall inform Directorates of HR and Finance &amp; Accounts about any change in
                    the above</li>
                <li>(i) <strong>(*)</strong> My spouse is employed in_________________________________________________________________</li>
            </ul>
            <p>
                and he/she is not availing any medical benefit/ LTC from that office as per enclosed
                certificate issued by the employer</p>
            <p>
                OR</p>
            <p>
                (ii) <strong>(</strong><strong>*)</strong> &nbsp;My spouse is not employed in any
                office.</p>
            <p>
                <strong>&nbsp;&nbsp;&nbsp;&nbsp; (*)</strong> &nbsp;Strike out if not applicable.</p>
            <p>
                &nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='348'>
                            <p><strong>Employee Signature </strong>
                            </p>
                        </td>
                        <td colspan='2' width='396'>
                            <p><strong>Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Signature</p>
                        </td>
                        <td width='192'>
                        </td>
                        <td width='166'>
                            <p>Signature</p>
                        </td>
                        <td width='230'>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Name</p>
                        </td>
                        <td width='192'>
                            " + EMPNAME + @"
                        </td>
                        <td width='166'>
                            <p>Name</p>
                        </td>
                        <td width='230'>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Designation</p>
                        </td>
                        <td width='192'>
                            " + Designation + @"
                        </td>
                        <td width='166'>
                            <p>Designation</p>
                        </td>
                        <td width='230'>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Place</p>
                        </td>
                        <td width='192'>
                        </td>
                        <td width='166'>
                            <p>Date</p>
                        </td>
                        <td width='230'>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Date</p>
                        </td>
                        <td width='192'>
                        </td>
                        <td width='166'>
                        </td>
                        <td width='230'>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                Forwarded to Directorate of F&amp;A &nbsp;on _______________&nbsp; &nbsp;&nbsp;-
                <strong>( for Office use only )</strong></p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>
                        </td>
                        <td width='240'>
                        </td>
                        <td width='228'>
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                         <p>Signature</p>
                        </td>
                        <td width='240'>
                        </td>
                        <td width='228'>
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                         <p>(HR)</p>
                        </td>
                        <td width='240'>
                        </td>
                        <td width='228'>
                        </td>
                    </tr>
                </tbody>
            </table>";
                    }
                }
            }
            //Benevoent Fund
            if (Index == "0")
            {
                if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                {
                    Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p>The Secretary</p>
            <p>ICSI Employees Benevolent Fund</p>
            <p>C/o The Institute of Company Secretaries of India</p>
            <p>ICSI &nbsp;House, 22, Institutional Area , Lodi Road, NewDelhi-110003</p>
            <p>&nbsp;</p>
            <p>Dear Sir,</p>
            <p>I hereby apply for admission as a subscriber member of the ICSI Employee's Benevolent
                Fund. I have read the Memorandum of Association &amp; Bye Laws of the Fund and I
                agree to abide by them and or as amended hereafter I give below the necessary particulars.</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='60'><p>1</p></td>
                        <td width='326'><p>Name in Full</p></td>
                        <td width='334'>" + EMPNAME + @"</td></tr>
                        <tr><td width='60'><p>2</p></td>
                        <td width='326'><p>Sex( M/F)</p></td>
                        <td width='334'></td>
                    </tr>
                    <tr>
                        <td width='60'><p>3</p></td>
                        <td width='326'><p>Marital Status ( Married/ Unmarried)</p></td>
                        <td width='334'>" + Marital + @"</td>
                    </tr>
                    <tr><td width='60'><p>4</p></td>
                        <td width='326'><p>
                                Bank Account Number with the name of the bank &amp; Branch&nbsp; in case you have
                                an account with Canara bank</p>
                        </td>
                        <td width='334'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='60'>
                            <p>5</p>
                        </td>
                        <td colspan='2' width='660'>
                            <p>Present /&nbsp; Communication&nbsp; Address</p>                           
                        </td>
                    </tr>
                </tbody>
            </table>
            <p><strong>Name(s) of Dependents : ( Please mention the names of dependents only ) </strong></p>
            <p>( Note: For the definition of Dependents please refer <strong>Annexure A </strong>)</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='43'>
                            <p><strong>S.No.</strong></p>
                        </td>
                        <td width='161'>
                            <p><strong>Name{s)</strong></p>
                        </td>
                        <td width='90'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='78'>
                            <p><strong>Age</strong></p>
                        </td>
                        <td width='348'>
                            <p><strong>Relation to subscriber</strong></p>
                        </td>
                    </tr>";
                    int Cnt = 0;
                    for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                    {
                        Cnt = Cnt + 1;
                        string Age = "10";
                        string datedb = Convert.ToString(ds.Tables[5].Rows[i]["DOB"]);
                        string[] dd = null;
                        string date = string.Empty;
                        if (datedb.Contains("/"))
                        {
                            dd = datedb.Split('/');
                            date = dd[0] + "/" + dd[1] + "/" + dd[2];
                        }
                        else
                        {
                            dd = datedb.Split('-');
                            date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                        }
                        // DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                        DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                        Entity += @"<tr>
                        <td width='43'><p>" + Cnt + @"</p></td>
                        <td width='161'><p>" + Convert.ToString(ds.Tables[5].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='90'><p>" + Convert.ToString(ds.Tables[5].Rows[i]["DOB"]) + @"</p></td>
                        <td width='78'><p>" + Age + @"</p></td>
                        <td width='348'><p>" + Convert.ToString(ds.Tables[5].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                    </tr>";
                    }
                    Entity += @"</tbody>
            </table>
            <p><strong>&nbsp;</strong></p>
            <p><strong>Declaration</strong></p>
            <p>
                I hereby authorise that the monthly contribution towards ICSI Employees' Benevolent
                Fund may be deducted every month from my salary and be added to the Corpus of the
                Fund.</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='348'>
                            <p>
                                <strong>Details of Employee</strong></p>
                        </td>
                        <td colspan='2' width='372'>
                            <p>
                                <strong>Details of Witness</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>
                                Signature</p>
                           
                        </td>
                        <td width='192'>
                            
                        </td>
                        <td width='166'>
                            <p>
                                Signature</p>
                        </td>
                        <td width='206'>
                          
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Name</p>
                           
                        </td>
                        <td width='192'>
                            " + EMPNAME + @"
                        </td>
                        <td width='166'>
                            <p>
                                Name</p>
                        </td>
                        <td width='206'>
                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>
                                Designation</p>
                            
                        </td>
                        <td width='192'>
                            " + Designation + @"
                        </td>
                        <td width='166'>
                            <p>
                                Designation</p>
                        </td>
                        <td width='206'>
                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Place</p>                          
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Date</p>
                        </td>
                        <td width='206'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Date</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>                            
                        </td>
                        <td width='206'>                           
                        </td>
                    </tr>
                </tbody>
            </table>           
            <p>
                Forwarded to Directorate of F&amp;A on _______________&nbsp; &nbsp;- <strong>( for Office
                    use only ) </strong>
            </p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='240'>                          
                        </td>
                        <td width='228'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='240'>                          
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                </tbody>
            </table>";
                }
            }
            //Encashment Fund
            if (Index == "0")
            {
                if (ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
                {
                    Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p><strong>FORM OF NOMINATION UNDER THE PAYMENT EL/HPL </strong><strong>ENCASHMENT AMOUNT</strong></p>
            <p><strong>&nbsp;</strong></p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='183'>
                            <p>Name of the employee</p>
                        </td>
                        <td width='549'>  
                        " + EMPNAME + @"                          
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Sex (M/F)</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Father's / Husband&rsquo;s Name</p>
                        </td>
                        <td width='549'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Marital Status</p>
                        </td>
                        <td width='549'> 
                        " + Marital + @"                          
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Date of Birth</p>
                        </td>
                        <td width='549'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Permanent Address</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>                           
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                I hereby nominate the person(s) mentioned below to receive the amount of EL/HPL
                Encashment &nbsp;in the event of my death before the amount becomes payable, or
                having become payable, has not been paid and direct that the said amount shall be
                distributed among the said person(s) in the manner shown against their name.</p>
            <p><strong>&nbsp;</strong></p>
            <p><strong>&nbsp;</strong></p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='163'>
                            <p><strong>Name of nominee or nominees</strong></p>
                        </td>
                        <td width='197'>
                            <p><strong>Nominee's relationship with the employee</strong></p>
                        </td>
                        <td width='151'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='65'>
                            <p><strong>Age</strong></p>
                        </td>
                        <td width='156'>
                            <p><strong>Percentage of&nbsp; Amount of share to be paid to each nominee</strong></p>
                        </td>
                    </tr>";
                    for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                    {
                        string Age = "10";
                        string datedb = Convert.ToString(ds.Tables[6].Rows[i]["DOB"]);
                        string[] dd = null;
                        string date = string.Empty;
                        if (datedb.Contains("/"))
                        {
                            dd = datedb.Split('/');
                            date = dd[0] + "/" + dd[1] + "/" + dd[2];
                        }
                        else
                        {
                            dd = datedb.Split('-');
                            date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                        }
                        //  DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                        DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                        Entity += @"<tr>
                        <td width='197'><p>" + Convert.ToString(ds.Tables[6].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='151'><p>" + Convert.ToString(ds.Tables[6].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                        <td width='65'><p>" + Convert.ToString(ds.Tables[6].Rows[i]["DOB"]) + @"</p></td>
                        <td width='156'><p>" + Age + @"</p></td>
                        <td width='197'><p>" + Convert.ToString(ds.Tables[6].Rows[i]["EncashShare"]) + @"</p></td>
                    </tr>";
                    }
                    Entity += @"</tbody>
            </table>            
            <p>
                Dated __________________________ ( mention the Date/ Month/Year)</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='348'>
                            <p>
                                <strong>Employee Signature </strong>
                            </p>
                            <p>
                                <strong>&nbsp;</strong></p>
                        </td>
                        <td colspan='2' width='384'>
                            <p><strong>&nbsp;Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Signature</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Signature</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Name</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Name</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>
                                Designation</p>
                            
                        </td>
                        <td width='192'>
                            
                        </td>
                        <td width='166'>
                            <p>Designation</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Place</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Place</p>                            
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Date</p>                            
                        </td>
                        <td width='192'>                           
                        </td>
                        <td width='166'>
                            <p>Date</p>                           
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                </tbody>
            </table>           
            <p>
                Forwarded to Directorate of F&amp;A &nbsp;on _______________&nbsp; &nbsp;&nbsp;-
                <strong>( for Office use only )</strong> &nbsp;&nbsp;</p>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>                          
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                           
                        </td>
                    </tr>
                </tbody>
            </table>";
                }
            }
            // Gratuity Fund
            if (Index == "0")
            {
                if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                {
                    Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p><strong>FORM OF NOMINATION &lsquo;FORM NO. 40 A&rsquo;</strong></p>
            <p>&nbsp;(for persons not covered under the Payment of Gratuity Act)</p>
            <p>Gratuity Fund</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='183'>
                            <p>Name of the employee</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Sex</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Religion</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Father's Name</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Husband&rsquo;s Name</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr><td width='183'>
                            <p>Marital Status</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Date of Birth</p>
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>
                            <p>Permanent Address</p>
                        </td>
                        <td width='549'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='183'>                            
                        </td>
                        <td width='549'>                            
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                I hereby nominate the person(s) mentioned below to receive the amount of Gratuity
                in the event of my death before the amount becomes payable, or having become payable,
                has not been paid and direct that the said amount shall be distributed among the
                said person(s) in the manner shown against their name.</p>
            <p>
                <strong>&nbsp;</strong></p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='288'>
                            <p><strong>Name and address of nominee or nominees</strong></p>
                        </td>
                        <td width='108'>
                            <p><strong>Nominee's relationship </strong><strong>with the employee</strong></p>
                        </td>
                        <td width='96'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='108'>
                            <p><strong>Age of nominee</strong></p>
                        </td>
                        <td width='132'>
                            <p><strong>Amount or share of gratuity to be paid to each nominee</strong></p>
                        </td>
                    </tr>";
                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                    {
                        string Age = "10";
                        string datedb = Convert.ToString(ds.Tables[4].Rows[i]["DOB"]);
                        string[] dd = null;
                        string date = string.Empty;
                        if (datedb.Contains("/"))
                        {
                            dd = datedb.Split('/');
                            date = dd[0] + "/" + dd[1] + "/" + dd[2];
                        }
                        else
                        {
                            dd = datedb.Split('-');
                            date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                        }
                        //  DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                        DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                        Entity += @"<tr>
                        <td width='288'><p>" + Convert.ToString(ds.Tables[4].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='108'><p>" + Convert.ToString(ds.Tables[4].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                        <td width='96'><p>" + Convert.ToString(ds.Tables[4].Rows[i]["DOB"]) + @"</p></td>
                        <td width='108'><p>" + Age + @"</p></td>
                        <td width='132'><p>" + Convert.ToString(ds.Tables[4].Rows[i]["GratuityShare"]) + @"</p></td>
                    </tr>";
                    }
                    Entity += @"</tbody>
            </table>            
            <ol>
                <li>Certified that I have no family and should I acquire a family hereafter, the above
                    mentioned should be deemed as cancelled.</li>
                <li>Certified that my father / mother is /are dependent upon me.</li>
            </ol>
            <p>Dated___________________(mention the Date/Month/Year )</p>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='240'>
                            <p><strong>Employee Signature </strong>
                            </p>                            
                        </td>
                        <td colspan='2' width='252'>
                            <p><strong>1<sup>st</sup>&nbsp; Witness Signature</strong></p>
                        </td>
                        <td colspan='2' width='240'>
                            <p><strong>2<sup>nd</sup> Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Signature</p>                            
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='108'>
                            <p>Signature</p>
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='96'>
                            <p>Signature</p>
                        </td>
                        <td width='144'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Name</p>                            
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='108'>
                            <p>Name</p>
                        </td>
                        <td width='144'>
                        " + EMPNAME + @"                            
                        </td>
                        <td width='96'>
                            <p>Name</p>
                        </td>
                        <td width='144'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Designation</p>                            
                        </td>
                        <td width='144'>  
                        " + Designation + @"                          
                        </td>
                        <td width='108'>
                            <p>Designation</p>
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='96'>
                            <p>Designation</p>
                        </td>
                        <td width='144'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Place</p>                           
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='108'>
                            <p>Place</p>                          
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='96'>
                            <p>Place</p>                            
                        </td>
                        <td width='144'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Date</p>                            
                        </td>
                        <td width='144'>
                            <p>&nbsp;</p>
                        </td>
                        <td width='108'>
                            <p>Date</p>                            
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='96'>
                            <p>Date</p>                            
                        </td>
                        <td width='144'>                            
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                <strong>____________________________________________________( for Office use only )</strong>
                _________________________________________</p>
            <p>&nbsp;</p>
            <p>
                Certified that the above declaration has been signed by Shri / Smt. _____________________________
                before me after he/she has read the entries.</p>
            <p>
                ____________________</p>
            <p>&nbsp;Signature of the Trustee</p>";
                }
            }
            //Pension Trust
            if (Index == "0")
            {
                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {

                    Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p>
                Forwarded to Directorate of F&amp;A&nbsp; on _______________&nbsp; &nbsp;&nbsp;</p>            
            <p>
                The Trustees of the Institute of Company</p>
            <p>
                Secretaries of India Employees New Pension Fund Trust</p>
            <p>
                &ldquo;ICSI House&rdquo;</p>
            <p>
                22, Institutional Area</p>
            <p>
                Lodi Road,</p>
            <p>
                New Delhi &ndash; 110 003</p>
            <p>
                &nbsp;</p>
            <p>
                I,_________________________________________________(NAME OF THE MEMBER IN BLOCK
                LETTERS) s/o/d/o/w/o Shri/Smt._________________________________ hereby nominate
                the persons mentioned below to receive the amount that may stand to my credit in
                the New Pension Fund Trust in the event of my death before that amount has become
                payable or having become payable, and has not been paid, direct that the said amount
                shall be distributed among the said persons in the manner shown against their names
                :</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='163'>
                            <p><strong>Name of nominee or nominees</strong></p>
                        </td>
                        <td width='197'>
                            <p><strong>Nominee's relationship with the employee</strong></p>
                        </td>
                        <td width='151'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='65'>
                            <p><strong>Age</strong></p>
                        </td>
                        <td width='156'>
                            <p><strong>Percentage of&nbsp; Amount of share to be paid to each nominee</strong></p>
                        </td>
                    </tr>";
                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        string Age = "10";
                        string datedb = Convert.ToString(ds.Tables[3].Rows[i]["DOB"]);
                        string[] dd = null;
                        string date = string.Empty;
                        if (datedb.Contains("/"))
                        {
                            dd = datedb.Split('/');
                            date = dd[0] + "/" + dd[1] + "/" + dd[2];
                        }
                        else
                        {
                            dd = datedb.Split('-');
                            date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                        }
                        // DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                        DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        // DateTime dateOfBirth = Convert.ToDateTime(Convert.ToString(ds.Tables[1].Rows[i]["DOB"]));
                        Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                        Entity += @"<tr>
                        <td width='163'><p>" + Convert.ToString(ds.Tables[3].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='197'><p>" + Convert.ToString(ds.Tables[3].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                        <td width='151'><p>" + Convert.ToString(ds.Tables[3].Rows[i]["DOB"]) + @"</p></td>
                        <td width='65'><p>" + Age + @"</p></td>
                        <td width='156'><p>" + Convert.ToString(ds.Tables[3].Rows[i]["PensionShare"]) + @"</p></td>
                    </tr>";
                    }
                    Entity += @"</tbody>
            </table>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='348'>
                            <p><strong>Employee Signature </strong>
                            </p>
                            <p><strong>&nbsp;</strong></p>
                        </td>
                        <td colspan='2' width='384'>
                            <p><strong>&nbsp;Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Signature</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Signature</p>
                        </td>
                        <td width='218'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Name</p>                            
                        </td>
                        <td width='192'>
                        " + EMPNAME + @"                            
                        </td>
                        <td width='166'>
                            <p>Name</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Designation</p>                            
                        </td>
                        <td width='192'> 
                        " + Designation + @"                          
                        </td>
                        <td width='166'>
                            <p>Designation</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Place</p>                           
                        </td>
                        <td width='192'>
                            
                        </td>
                        <td width='166'>
                            <p>Place</p>                            
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Date</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Date</p>                            
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                </tbody>
            </table>            
            <p>
                Forwarded to Directorate of F&amp;A&nbsp; on _______________&nbsp; &nbsp;&nbsp;-
                <strong>( for Office use only )</strong> &nbsp;&nbsp;</p>
            <p>nbsp;</p>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='240'>                           
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                </tbody>
            </table>";
                }
            }
            //New Pension Fund Trsut
            if (Index == "0")
            {
                if (ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
                {

                    Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p>
                Forwarded to Directorate of F&amp;A&nbsp; on _______________&nbsp; &nbsp;&nbsp;</p>            
            <p>
                The Trustees of the Institute of Company</p>
            <p>
                Secretaries of India Employees New Pension Fund Trust</p>
            <p>
                &ldquo;ICSI House&rdquo;</p>
            <p>
                22, Institutional Area</p>
            <p>
                Lodi Road,</p>
            <p>
                New Delhi &ndash; 110 003</p>
            <p>
                &nbsp;</p>
            <p>
                I,_________________________________________________(NAME OF THE MEMBER IN BLOCK
                LETTERS) s/o/d/o/w/o Shri/Smt._________________________________ hereby nominate
                the persons mentioned below to receive the amount that may stand to my credit in
                the New Pension Fund Trust in the event of my death before that amount has become
                payable or having become payable, and has not been paid, direct that the said amount
                shall be distributed among the said persons in the manner shown against their names
                :</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='163'>
                            <p><strong>Name of nominee or nominees</strong></p>
                        </td>
                        <td width='197'>
                            <p><strong>Nominee's relationship with the employee</strong></p>
                        </td>
                        <td width='151'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='65'>
                            <p><strong>Age</strong></p>
                        </td>
                        <td width='156'>
                            <p><strong>Percentage of&nbsp; Amount of share to be paid to each nominee</strong></p>
                        </td>
                    </tr>";
                    for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                    {
                        string Age = "10";
                        string datedb = Convert.ToString(ds.Tables[7].Rows[i]["DOB"]);
                        string[] dd = null;
                        string date = string.Empty;
                        if (datedb.Contains("/"))
                        {
                            dd = datedb.Split('/');
                            date = dd[0] + "/" + dd[1] + "/" + dd[2];
                        }
                        else
                        {
                            dd = datedb.Split('-');
                            date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                        }
                        //  DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                        DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                        Entity += @"<tr>
                        <td width='163'><p>" + Convert.ToString(ds.Tables[7].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='197'><p>" + Convert.ToString(ds.Tables[7].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                        <td width='151'><p>" + Convert.ToString(ds.Tables[7].Rows[i]["DOB"]) + @"</p></td>
                        <td width='65'><p>" + Age + @"</p></td>
                        <td width='156'><p>" + Convert.ToString(ds.Tables[7].Rows[i]["PensionShare"]) + @"</p></td>
                    </tr>";
                    }
                    Entity += @"</tbody>
            </table>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='348'>
                            <p><strong>Employee Signature </strong>
                            </p>
                            <p><strong>&nbsp;</strong></p>
                        </td>
                        <td colspan='2' width='384'>
                            <p><strong>&nbsp;Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Signature</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Signature</p>
                        </td>
                        <td width='218'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Name</p>                            
                        </td>
                        <td width='192'>
                        " + EMPNAME + @"                            
                        </td>
                        <td width='166'>
                            <p>Name</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Designation</p>                            
                        </td>
                        <td width='192'> 
                        " + Designation + @"                          
                        </td>
                        <td width='166'>
                            <p>Designation</p>
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Place</p>                           
                        </td>
                        <td width='192'>
                            
                        </td>
                        <td width='166'>
                            <p>Place</p>                            
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='156'>
                            <p>Date</p>                            
                        </td>
                        <td width='192'>                            
                        </td>
                        <td width='166'>
                            <p>Date</p>                            
                        </td>
                        <td width='218'>                            
                        </td>
                    </tr>
                </tbody>
            </table>            
            <p>
                Forwarded to Directorate of F&amp;A&nbsp; on _______________&nbsp; &nbsp;&nbsp;-
                <strong>( for Office use only )</strong> &nbsp;&nbsp;</p>
            <p>nbsp;</p>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='240'>                           
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                </tbody>
            </table>";
                }
            }
            // Provident Fund
            if (Index == "0")
            {
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    Entity += @"<p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p><strong>(Form of modifying previous Nomination under Provident Fund)</strong></p>
            <p>No.</p>
            <p>Folio No.</p>
            <p>The Trustees of the Institute of Company</p>
            <p>Secretaries of India Employees Provident Fund</p>
            <p>&ldquo;ICSI House&rdquo;</p>
            <p>22, Institutional Area</p>
            <p>Lodi Road,</p>
            <p>New Delhi &ndash; 110 003</p>
            <p>&nbsp;</p>
            <p>
                I,_________________________________________________(NAME OF THE MEMBER IN BLOCK
                LETTERS) s/o/d/o/w/o Shri/Smt.________________________________ employed as____________________________
                in the service of the Institute of Company Secretaries of&nbsp; India hereby cancel
                the nomination made by me previously as regards the disposal of the amount that
                may stand&nbsp; to my credit in the provident fund in the event of my death before
                that amount has become payable or, having&nbsp; become payable, has not been paid,
                and direct that the said amount shall be distributed among the said persons in the
                manner shown against their names:</p>
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='125'>
                            <p>
                                <strong>Name and Address of&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Nominee or Nominees</strong></p>
                        </td>
                        <td width='150'>
                            <p><strong>Nominee's relationship with the employee</strong></p>
                        </td>
                        <td width='87'>
                            <p><strong>Date of Birth</strong></p>
                        </td>
                        <td width='62'>
                            <p><strong>Age</strong></p>
                        </td>
                        <td width='162'>
                            <p>
                                <strong>If the nominee is a minor, state&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; the name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    of the guardian&nbsp; and the relationship with the member</strong></p>
                        </td>
                        <td width='150'>
                            <p><strong>**Amount or</strong></p>
                            <p><strong>Share of accumulations in the Provident Fund to be paid to each nominee</strong></p>
                        </td>
                    </tr>";
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        string Age = "10";
                        string datedb = Convert.ToString(ds.Tables[2].Rows[i]["DOB"]);
                        string[] dd = null;
                        string date = string.Empty;
                        if (datedb.Contains("/"))
                        {
                            dd = datedb.Split('/');
                            date = dd[0] + "/" + dd[1] + "/" + dd[2];
                        }
                        else
                        {
                            dd = datedb.Split('-');
                            date = dd[0] + "/" + dd[1] + "/" + "19" + dd[2];
                        }
                        // DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                        DateTime dateOfBirth = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Age = (DateTime.Now.Year - dateOfBirth.Year).ToString();
                        string ISMinor = Convert.ToString(ds.Tables[2].Rows[i]["ProvidentIsMinor"]) == "1" ? "Yes" : "No";
                        Entity += @"<tr>
                        <td width='125'><p>" + Convert.ToString(ds.Tables[2].Rows[i]["FML_MEMBER_NAME"]) + @"</p></td>
                        <td width='150'><p>" + Convert.ToString(ds.Tables[2].Rows[i]["REL_TYPE_ID"]) + @"</p></td>
                        <td width='87'><p>" + Convert.ToString(ds.Tables[2].Rows[i]["DOB"]) + @"</p></td>
                        <td width='62'><p>" + Age + @"</p></td>
                        <td width='162'><p>" + ISMinor + @"</p></td>
                        <td width='150'><p>" + Convert.ToString(ds.Tables[2].Rows[i]["ProvidentShare"]) + @"</p></td>
                    </tr>";
                    }
                    Entity += @"</tbody>
            </table>           
            <p>
                Certified that my marital status is________________________(State whether unmarried,
                married or widow/widower)</p>
            <p>&nbsp;</p>
            <p>
                *Certified that I have no family as defined in the explanation under clause 6 of
                the Trust Deed and should I acquire a family hereafter, the above nomination should
                be deemed as cancelled.</p>
            <p>&nbsp;</p>
            <p>
                **Certified that my father/mother/sister(s)/minor brother(s) is/are dependent upon
                me.</p>            
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td colspan='2' width='240'>
                            <p><strong>Employee Signature </strong></p>                            
                        </td>
                        <td colspan='2' width='252'>
                            <p><strong>1<sup>st</sup>&nbsp; Witness Signature</strong></p>
                        </td>
                        <td colspan='2' width='256'>
                            <p><strong>2<sup>nd</sup> Witness Signature</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Signature</p>                            
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='108'>
                            <p>Signature</p>
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='96'>
                            <p>Signature</p>
                        </td>
                        <td width='160'>                          
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Name</p>                            
                        </td>
                        <td width='144'>
                        " + EMPNAME + @"                            
                        </td>
                        <td width='108'>
                            <p>Name</p>
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='96'>
                            <p>Name</p>
                        </td>
                        <td width='160'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Designation</p>                           
                        </td>
                        <td width='144'>
                            " + Designation + @"                          
                        </td>
                        <td width='108'>
                            <p>Designation</p>
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='96'>
                            <p>Designation</p>
                        </td>
                        <td width='160'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Date</p>                            
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='108'>                           
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='96'>
                            <p>Date</p>
                        </td>
                        <td width='160'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='96'>
                            <p>Address</p>                            
                        </td>
                        <td width='144'>                            
                        </td>
                        <td width='108'>
                            <p>Address</p>
                        </td>
                        <td width='144'>                           
                        </td>
                        <td width='96'>
                            <p>Address</p>
                        </td>
                        <td width='160'>                          
                        </td>
                    </tr>
                </tbody>
            </table>            
            <p>
                Forwarded to Directorate of F&amp;A&nbsp; on _______________&nbsp; &nbsp;&nbsp;-
                <strong>( for Office use only )</strong> &nbsp;&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>                            
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='240'>                           
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                           
                        </td>
                    </tr>
                </tbody>
            </table>";
                }
            }
            if (Index == "0")
            {
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    Entity += @"<br/><br/><p style='text-align:center;page-break-before: always'><img src='Images/Header.png'/></p>
            <p>
                The Secretary</p>
            <p>
                ICSI Employee's Club</p>
            <p>
                C/o The Institute of Company Secretaries of India</p>
            <p>
                ICSI House, 22, Institutional Area</p>
            <p>
                Lodi Road</p>
            <p>
                New Delhi-110003</p>
            <p>
                Dear Sir,</p>
            <p>
                I hereby apply for admission as a member of the ICSI Employees' Club. I have read
                the Bye laws of the club and I agree to abide by them and or as amended hereafter.
                I give below the necessary particulars.</p>            
            <p>
                &nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='48'>
                            <p>1</p>
                        </td>
                        <td width='326'>
                            <p>Name in Full</p>                            
                        </td>
                        <td width='334'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='48'>
                            <p>2</p>
                        </td>
                        <td width='326'>
                            <p>Designation</p>                            
                        </td>
                        <td width='334'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='48'>
                            <p>3</p>
                        </td>
                        <td width='326'>
                            <p>Marital Status ( Married/ Unmarried)</p>                            
                        </td>
                        <td width='334'>                            
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>I hereby declare and authorise that the monthly contribution towards ICSI Employees
                Club may be deducted every month from my salary and be credited to the ICSI Employees
                Club account</p>            
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='288'>
                            <p>Place :</p>
                        </td>
                        <td width='420'>                           
                        </td>
                    </tr>
                    <tr>
                        <td width='288'>
                            <p>Date :</p>
                        </td>
                        <td width='420'>
                            <p>Signature of the Employee :</p>                            
                        </td>
                    </tr>
                </tbody>
            </table>            
            <p>&nbsp;</p>
            <p>
                Forwarded to President, ICSI Employee&nbsp; Club &nbsp;on _______________ - <strong>
                    ( for Office use only )</strong></p>           
            <p>&nbsp;</p>
            <table width='100%' border='1px solid'>
                <tbody>
                    <tr>
                        <td width='264'>                            
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>Signature</p>
                        </td>
                        <td width='240'>                           
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                    <tr>
                        <td width='264'>
                            <p>(HR)</p>
                        </td>
                        <td width='240'>                            
                        </td>
                        <td width='228'>                            
                        </td>
                    </tr>
                </tbody>
            </table>";
                }
            }
        }
        return Entity;
    }
}
