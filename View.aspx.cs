using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
public partial class View : System.Web.UI.Page
{
    string EMPCode = string.Empty;
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
                EMPCode = Convert.ToString(Request.QueryString["ID"]);
                BindUserDetails(EMPCode);
                //chkfamilyhardcopy.Enabled=false;
                //chkprovidenthardcopy.Enabled = false;
                //chknewpensionhardcopy.Enabled = false;
                //chkgratuityhardcopy.Enabled = false;
                //chkbenevolenthardcopy.Enabled = false;
                //chkencashmenthardcopy.Enabled = false;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Code"></param>
    /// 
    int Flag = 0;
    int Flag1 = 0;
    int Flag2 = 0;
    int Flag3 = 0;
    int Flag4 = 0;
    int Flag5 = 0;
    int Flag6 = 0; 
    public void BindUserDetails(string Code)
    {
        try 
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetAllDetailsByEMPCODEByForm1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = Code;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtEMPCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMP_CODE"]);
                txtName.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                txtdepartment.Text = Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT"]);
                txtdesignation.Text = Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION"]);
                txtfinancialyear.Text = Convert.ToString(ds.Tables[0].Rows[0]["FN_YR"]);
                txtDocDate.Text = Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[0]["DOB"])).ToShortDateString();
                //ddlreasonforendofemployment.Text = Convert.ToString(ds.Tables[0].Rows[0]["REAGON_FOR_END_EMP"]);
                txtpaddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["P_ADDRESS"]);
                txtcaddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["C_ADDRESS"]);
                // txtreligion.Text = Convert.ToString(ds.Tables[0].Rows[0]["REGION"]);
                ddlgender.Text = Convert.ToString(ds.Tables[0].Rows[0]["SEX"]);
                ddlmaritalstatus.Text = Convert.ToString(ds.Tables[0].Rows[0]["MARITAL_STATUS"]);
                ddlEmploymentstatus.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_STATUS"]);
                //txtcomments.Text = Convert.ToString(ds.Tables[0].Rows[0]["comments"]);

            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                GD_FamilyDetails.DataSource = ds.Tables[1];
                GD_FamilyDetails.DataBind();

                //if (Convert.ToInt32(ds.Tables[1].Rows[0]["IsApproved"]) >= 1 && Convert.ToInt32(ds.Tables[1].Rows[0]["IsApproved"]) != 2) { chkfamilyapproved.Checked = true; chkfamilyreject.Enabled = false; }
                //if (chkfamilyapproved.Checked == true)
                //{
                //    chkfamilyhardcopy.Enabled = true;
                //    chkfamilyapproved.Enabled = false;
                //}
                //else
                //{
                //    chkfamilyhardcopy.Enabled = false;
                //}
                //if (Convert.ToInt32(ds.Tables[1].Rows[0]["IsApproved"]) == 2) { chkfamilyreject.Checked = true; }
                //if (Convert.ToInt32(ds.Tables[1].Rows[0]["IsApproved"]) >= 3)
                //{
                //    chkfamilyhardcopy.Checked = true;
                //    chkfamilyapproved.Enabled = false;
                //    chkfamilyreject.Enabled = false;
                //    chkfamilyhardcopy.Enabled = false;
                //}
                //if (Convert.ToInt32(ds.Tables[1].Rows[0]["Ishardcopy"]) >= 1 && Convert.ToInt32(ds.Tables[1].Rows[0]["Ishardcopy"]) != 3) { chkfamilyapproved.Checked = true; chkfamilyreject.Enabled = false; }
                //if (Convert.ToInt32(ds.Tables[1].Rows[0]["Ishardcopy"]) == 2) { chkfamilyreject.Checked = true; }



                var vvalapp = from varvalues1 in ds.Tables[1].AsEnumerable()
                              where varvalues1.Field<int>("IsApproved") >= 0 
                              select new
                              {
                                  Approvd1 = varvalues1.Field<int>("IsApproved") 
                              };

                foreach (var item1 in vvalapp)
                {
                    if (item1.Approvd1.ToString() == "0")
                    {
                        chkfamilyapproved.Checked = false;
                        chkfamilyhardcopy.Checked = false;
                        chkfamilyapproved.Enabled = true;
                        chkfamilyreject.Enabled = true;
                        chkfamilyhardcopy.Enabled = false;
                        Flag = 0;
                    }
                    else if (item1.Approvd1.ToString() == "2")
                    {
                        chkfamilyapproved.Checked = false;
                        chkfamilyhardcopy.Checked = false;
                        chkfamilyapproved.Enabled = false;
                        chkfamilyreject.Enabled = false;
                        chkfamilyreject.Checked = true;
                        chkfamilyhardcopy.Enabled = false;
                        Flag = 0;
                    }
                    else
                    {
                        chkfamilyapproved.Checked = true;
                        chkfamilyhardcopy.Checked = false;
                        chkfamilyapproved.Enabled = false;
                        chkfamilyreject.Enabled = false;
                        chkfamilyhardcopy.Enabled = true;
                        Flag = 3;
                    }
                }



                if (Flag == 3)
                {
                    var vval = from varvalues in ds.Tables[1].AsEnumerable()
                               where varvalues.Field<int>("Ishardcopy") >= 0
                               select new
                               {
                                   hardcopy = varvalues.Field<int>("Ishardcopy")
                               };

                    foreach (var item in vval)
                    {
                        if (item.hardcopy.ToString() == "0")
                        {
                            chkfamilyapproved.Checked = true;
                            chkfamilyhardcopy.Checked = false;
                            chkfamilyapproved.Enabled = false;
                            chkfamilyreject.Enabled = false;
                            chkfamilyhardcopy.Enabled = true;                          
                        }
                        else
                        {
                            chkfamilyhardcopy.Checked = true;
                            chkfamilyapproved.Enabled = false;
                            chkfamilyreject.Enabled = false;
                            chkfamilyhardcopy.Enabled = false;
                        }
                    }
                }

            }
            if (ds != null && ds.Tables[1].Rows.Count == 0)
            {
                DIVFamilyDetails.Visible = false;
            }
            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                GD_ProvidentFund.DataSource = ds.Tables[2];
                GD_ProvidentFund.DataBind();

                //if (Convert.ToInt32(ds.Tables[2].Rows[0]["IsApproved"]) >= 1 && Convert.ToInt32(ds.Tables[2].Rows[0]["IsApproved"]) != 2) { chkprovidentapproved.Checked = true; chkprovidentrejected.Enabled = false; }
                //if (chkprovidentapproved.Checked == true)
                //{
                //    chkprovidenthardcopy.Enabled = true;
                //    chkprovidentapproved.Enabled = false; 
                //}
                //else
                //{
                //    chkprovidenthardcopy.Enabled = false; 
                //}
                //if (Convert.ToInt32(ds.Tables[2].Rows[0]["IsApproved"]) == 2) { chkprovidentrejected.Checked = true; }
                //if (Convert.ToInt32(ds.Tables[2].Rows[0]["IsApproved"]) >= 3)
                //{
                //    chkprovidenthardcopy.Checked = true;
                //    chkprovidentapproved.Enabled = false;
                //    chkprovidentrejected.Enabled = false;
                //    chkprovidenthardcopy.Enabled = false;
                //}

                //if (Convert.ToInt32(ds.Tables[2].Rows[0]["Ishardcopy"]) >= 1 && Convert.ToInt32(ds.Tables[2].Rows[0]["Ishardcopy"]) != 2) { chkprovidentapproved.Checked = true; chkprovidentrejected.Enabled = false; }
                //if (Convert.ToInt32(ds.Tables[2].Rows[0]["Ishardcopy"]) == 2) { chkprovidentrejected.Checked = true; }

               
                var vvalapp1 = from varvalues2 in ds.Tables[2].AsEnumerable()
                              where varvalues2.Field<int>("IsApproved") >= 0
                              select new
                              {
                                  Approvd2 = varvalues2.Field<int>("IsApproved")
                              };

                foreach (var item2 in vvalapp1)
                {
                    if (item2.Approvd2.ToString() == "0")
                    {
                        chkprovidentapproved.Checked = false;
                        chkprovidenthardcopy.Checked = false;
                        chkprovidentapproved.Enabled = true;
                        chkprovidentrejected.Enabled = true;
                        chkprovidenthardcopy.Enabled = false;
                        Flag1 = 0;
                    }
                    else if (item2.Approvd2.ToString() == "2")
                    {
                        chkprovidentapproved.Checked = false;
                        chkprovidenthardcopy.Checked = false;
                        chkprovidentapproved.Enabled = false;
                        chkprovidentrejected.Enabled = false;
                        chkprovidentrejected.Checked = true;
                        chkprovidenthardcopy.Enabled = false;
                        Flag1 = 0;
                    }
                    else
                    {
                        chkprovidentapproved.Checked = true;
                        chkprovidenthardcopy.Checked = false;
                        chkprovidentapproved.Enabled = false;
                        chkprovidentrejected.Enabled = false;
                        chkprovidenthardcopy.Enabled = true;
                        Flag1 = 3;
                    }
                }


                if (Flag1 == 3)
                {
                    var vval1 = from varvalues in ds.Tables[2].AsEnumerable()
                               where varvalues.Field<int>("Ishardcopy") >= 0
                               select new
                               {
                                   hardcopy = varvalues.Field<int>("Ishardcopy")
                               };

                    foreach (var item in vval1)
                    {
                        if (item.hardcopy.ToString() == "0")
                        {
                            chkprovidentapproved.Checked = true;
                            chkprovidenthardcopy.Checked = false;
                            chkprovidentapproved.Enabled = false;
                            chkprovidentrejected.Enabled = false;
                            chkprovidenthardcopy.Enabled = true;
                        }
                        else
                        {
                            chkprovidenthardcopy.Checked = true;
                            chkprovidentapproved.Enabled = false;
                            chkprovidentrejected.Enabled = false;
                            chkprovidenthardcopy.Enabled = false;
                        }
                    }
                }

                //if (Convert.ToInt32(ds.Tables[2].Rows[0]["Ishardcopy"]) >= 3)
                //{
                //    chkprovidenthardcopy.Checked = true;
                //    chkprovidentapproved.Enabled = false;
                //    chkprovidentrejected.Enabled = false;
                //    chkprovidenthardcopy.Enabled = false;
                //}
            }
            if (ds != null && ds.Tables[2].Rows.Count == 0)
            {
                DivProvidentFund.Visible = false;
            }
            if (ds != null && ds.Tables[3].Rows.Count > 0)
            {
                GD_EmployeePensionTrust.DataSource = ds.Tables[3];
                GD_EmployeePensionTrust.DataBind();

                //if (Convert.ToInt32(ds.Tables[3].Rows[0]["IsApproved"]) >= 1 && Convert.ToInt32(ds.Tables[3].Rows[0]["IsApproved"]) != 2) { chkpensionapproved.Checked = true; chkpensionrejected.Enabled = false; }
                //if (chkpensionapproved.Checked == true)
                //{
                //    chknewpensionhardcopy.Enabled = true;
                //    chkpensionapproved.Enabled = false;
                //}
                //else
                //{
                //    chknewpensionhardcopy.Enabled = false;
                //}
                //if (Convert.ToInt32(ds.Tables[3].Rows[0]["IsApproved"]) == 2) { chkpensionrejected.Checked = true; }
                //if (Convert.ToInt32(ds.Tables[3].Rows[0]["IsApproved"]) >= 3)
                //{
                //    chkpensionhardcopy.Checked = true;
                //    chkpensionapproved.Enabled = false;
                //    chkpensionrejected.Enabled = false;
                //    chkpensionhardcopy.Enabled = false;
                //}

                //if (Convert.ToInt32(ds.Tables[3].Rows[0]["Ishardcopy"]) >= 1 && Convert.ToInt32(ds.Tables[3].Rows[0]["Ishardcopy"]) != 2) { chkpensionapproved.Checked = true; chkpensionrejected.Enabled = false; }
                //if (Convert.ToInt32(ds.Tables[3].Rows[0]["Ishardcopy"]) == 2) { chkpensionrejected.Checked = true; }


                var vvalapp2 = from varvalues3 in ds.Tables[3].AsEnumerable()
                               where varvalues3.Field<int>("IsApproved") >= 0
                               select new
                               {
                                   Approvd3 = varvalues3.Field<int>("IsApproved")
                               };

                foreach (var item3 in vvalapp2)
                {
                    if (item3.Approvd3.ToString() == "0")
                    {
                        chkpensionapproved.Checked = false;
                        chkpensionhardcopy.Checked = false;
                        chkpensionapproved.Enabled = true;
                        chkpensionrejected.Enabled = true;
                        chkpensionhardcopy.Enabled = false;
                        Flag2 = 0;
                    }
                    else if (item3.Approvd3.ToString() == "2")
                    {
                        chkpensionapproved.Checked = false;
                        chkpensionhardcopy.Checked = false;
                        chkpensionapproved.Enabled = false;
                        chkpensionrejected.Enabled = false;
                        chkpensionrejected.Checked = true;
                        chkpensionhardcopy.Enabled = false;
                        Flag2 = 0;
                    }
                    else
                    {
                        chkpensionapproved.Checked = true;
                        chkpensionhardcopy.Checked = false;
                        chkpensionapproved.Enabled = false;
                        chkpensionrejected.Enabled = false;
                        chkpensionhardcopy.Enabled = true;
                        Flag2 = 3;
                    }
                }


                if (Flag2 == 3)
                {
                    var vval2 = from varvalues in ds.Tables[3].AsEnumerable()
                               where varvalues.Field<int>("Ishardcopy") >= 0
                               select new
                               {
                                   hardcopy = varvalues.Field<int>("Ishardcopy")
                               };

                    foreach (var item in vval2)
                    {
                        if (item.hardcopy.ToString() == "0")
                        {
                            chkprovidentapproved.Checked = true;
                            chkprovidenthardcopy.Checked = false;
                            chkprovidentapproved.Enabled = false;
                            chkprovidentrejected.Enabled = false;
                            chkprovidenthardcopy.Enabled = true;
                        }
                        else
                        {
                            chkprovidenthardcopy.Checked = true;
                            chkprovidentapproved.Enabled = false;
                            chkprovidentrejected.Enabled = false;
                            chkprovidenthardcopy.Enabled = false;
                        }
                    }
                }

                //if (Convert.ToInt32(ds.Tables[3].Rows[0]["Ishardcopy"]) >= 3)
                //{
                //    chkpensionhardcopy.Checked = true;
                //    chkpensionapproved.Enabled = false;
                //    chkpensionrejected.Enabled = false;
                //    chkpensionhardcopy.Enabled = false;
                //}
            }
            if (ds != null && ds.Tables[3].Rows.Count == 0)
            {
                DivEmployeePensionTrust.Visible = false;
            }
            if (ds != null && ds.Tables[4].Rows.Count > 0)
            {
                GD_Gratuity.DataSource = ds.Tables[4];
                GD_Gratuity.DataBind();

                //if (Convert.ToInt32(ds.Tables[4].Rows[0]["IsApproved"]) >= 1 && Convert.ToInt32(ds.Tables[4].Rows[0]["IsApproved"]) != 2) { chkgratuityapproved.Checked = true; chkgratuityrejected.Enabled = false; }
                //if (chkgratuityapproved.Checked == true)
                //{
                //    chkgratuityhardcopy.Enabled = true;
                //    chkgratuityapproved.Enabled = false;
                //}
                //else
                //{
                //    chkgratuityhardcopy.Enabled = false;
                //}
                //if (Convert.ToInt32(ds.Tables[4].Rows[0]["IsApproved"]) == 2) { chkgratuityrejected.Checked = true; }
                //if (Convert.ToInt32(ds.Tables[4].Rows[0]["IsApproved"]) >= 3)
                //{
                //    chkgratuityhardcopy.Checked = true;
                //    chkgratuityapproved.Enabled = false;
                //    chkgratuityhardcopy.Enabled = false;
                //    chkgratuityrejected.Enabled = false;
                //}
                //if (Convert.ToInt32(ds.Tables[4].Rows[0]["Ishardcopy"]) >= 1 && Convert.ToInt32(ds.Tables[4].Rows[0]["Ishardcopy"]) != 2) { chkgratuityapproved.Checked = true; chkgratuityrejected.Enabled = false; }
                //if (Convert.ToInt32(ds.Tables[4].Rows[0]["Ishardcopy"]) == 2) { chkgratuityrejected.Checked = true; }


                var vvalapp3 = from varvalues4 in ds.Tables[4].AsEnumerable()
                               where varvalues4.Field<int>("IsApproved") >= 0
                               select new
                               {
                                   Approvd4 = varvalues4.Field<int>("IsApproved")
                               };

                foreach (var item4 in vvalapp3)
                {
                    if (item4.Approvd4.ToString() == "0")
                    {
                        chkgratuityapproved.Checked = false;
                        chkgratuityhardcopy.Checked = false;
                        chkgratuityapproved.Enabled = true;
                        chkgratuityrejected.Enabled = true;
                        chkgratuityhardcopy.Enabled = false;
                        Flag3 = 0;
                    }
                    else if (item4.Approvd4.ToString() == "2")
                    {
                        chkgratuityapproved.Checked = false;
                        chkgratuityhardcopy.Checked = false;
                        chkgratuityapproved.Enabled = false;
                        chkgratuityrejected.Enabled = false;
                        chkgratuityrejected.Checked = true;
                        chkgratuityhardcopy.Enabled = false;
                        Flag3 = 0;
                    }
                    else
                    {
                        chkgratuityapproved.Checked = true;
                        chkgratuityhardcopy.Checked = false;
                        chkgratuityapproved.Enabled = false;
                        chkgratuityrejected.Enabled = false;
                        chkgratuityhardcopy.Enabled = true;
                        Flag3 = 3;
                    }
                }


                if (Flag3 == 3)
                {
                    var vval3 = from varvalues in ds.Tables[4].AsEnumerable()
                                where varvalues.Field<int>("Ishardcopy") >= 0
                                select new
                                {
                                    hardcopy = varvalues.Field<int>("Ishardcopy")
                                };

                    foreach (var item in vval3)
                    {
                        if (item.hardcopy.ToString() == "0")
                        {
                            chkgratuityapproved.Checked = true;
                            chkgratuityhardcopy.Checked = false;
                            chkgratuityapproved.Enabled = false;
                            chkgratuityrejected.Enabled = false;
                            chkgratuityhardcopy.Enabled = true;
                        }
                        else
                        {
                            chkgratuityhardcopy.Checked = true;
                            chkgratuityapproved.Enabled = false;
                            chkgratuityrejected.Enabled = false;
                            chkgratuityhardcopy.Enabled = false;
                        }
                    }
                }

                //if (Convert.ToInt32(ds.Tables[4].Rows[0]["Ishardcopy"]) >= 3)
                //{
                //    chkgratuityhardcopy.Checked = true;
                //    chkgratuityapproved.Enabled = false;
                //    chkgratuityhardcopy.Enabled = false;
                //    chkgratuityrejected.Enabled = false;
                //}
            }
            if (ds != null && ds.Tables[4].Rows.Count == 0)
            {
                DivGratuity.Visible = false;
            }
            if (ds != null && ds.Tables[5].Rows.Count > 0)
            {
                gridviewnewpension.DataSource = ds.Tables[5];
                gridviewnewpension.DataBind();

                //if (Convert.ToInt32(ds.Tables[5].Rows[0]["IsApproved"]) >= 1 && Convert.ToInt32(ds.Tables[5].Rows[0]["IsApproved"]) != 2) { chknewpensionapproved.Checked = true; chknewpensionrejected.Enabled = false; }
                //if (chknewpensionapproved.Checked == true)
                //{
                //    chknewpensionhardcopy.Enabled = true;
                //    chknewpensionapproved.Enabled = false;
                //}
                //else
                //{
                //    chknewpensionhardcopy.Enabled = false;
                //}
                //if (Convert.ToInt32(ds.Tables[5].Rows[0]["IsApproved"]) == 2) { chknewpensionrejected.Checked = true; }
                //if (Convert.ToInt32(ds.Tables[5].Rows[0]["IsApproved"]) >= 3)
                //{
                //    chknewpensionhardcopy.Checked = true;
                //    chknewpensionapproved.Enabled = false;
                //    chknewpensionrejected.Enabled = false;
                //    chknewpensionhardcopy.Enabled = false;
                //}
                //if (Convert.ToInt32(ds.Tables[5].Rows[0]["Ishardcopy"]) >= 1 && Convert.ToInt32(ds.Tables[5].Rows[0]["Ishardcopy"]) != 2) { chknewpensionapproved.Checked = true; chknewpensionrejected.Enabled = false; }
                //if (Convert.ToInt32(ds.Tables[5].Rows[0]["Ishardcopy"]) == 2) { chknewpensionrejected.Checked = true; }


                var vvalapp4 = from varvalues5 in ds.Tables[5].AsEnumerable()
                               where varvalues5.Field<int>("IsApproved") >= 0
                               select new
                               {
                                   Approvd5 = varvalues5.Field<int>("IsApproved") 
                               };

                foreach (var item5 in vvalapp4)
                {
                    if (item5.Approvd5.ToString() == "0")
                    {
                        chknewpensionapproved.Checked = false;
                        chknewpensionhardcopy.Checked = false;
                        chknewpensionapproved.Enabled = true;
                        chknewpensionrejected.Enabled = true;
                        chknewpensionhardcopy.Enabled = false;
                        Flag4 = 0;
                    }
                    else if (item5.Approvd5.ToString() == "2")
                    {
                        chknewpensionapproved.Checked = false;
                        chknewpensionhardcopy.Checked = false;
                        chknewpensionapproved.Enabled = false;
                        chknewpensionrejected.Enabled = false;
                        chknewpensionrejected.Checked = true;
                        chknewpensionhardcopy.Enabled = false;
                        Flag4 = 0;
                    }
                    else
                    {
                        chknewpensionapproved.Checked = true;
                        chknewpensionhardcopy.Checked = false;
                        chknewpensionapproved.Enabled = false;
                        chknewpensionrejected.Enabled = false;
                        chknewpensionhardcopy.Enabled = true;
                        Flag4 = 3;
                    }
                }


                if (Flag4 == 3)
                {
                    var vval4 = from varvalues in ds.Tables[5].AsEnumerable()
                                where varvalues.Field<int>("Ishardcopy") >= 0
                                select new
                                {
                                    hardcopy = varvalues.Field<int>("Ishardcopy")
                                };

                    foreach (var item in vval4)
                    {
                        if (item.hardcopy.ToString() == "0")
                        {
                            chknewpensionapproved.Checked = true;
                            chknewpensionhardcopy.Checked = false;
                            chknewpensionapproved.Enabled = false;
                            chknewpensionrejected.Enabled = false;
                            chknewpensionhardcopy.Enabled = true;
                        }
                        else
                        {
                            chknewpensionhardcopy.Checked = true;
                            chknewpensionapproved.Enabled = false;
                            chknewpensionrejected.Enabled = false;
                            chknewpensionhardcopy.Enabled = false;
                        }
                    }
                }
                //if (Convert.ToInt32(ds.Tables[5].Rows[0]["Ishardcopy"]) >= 3)
                //{
                //    chknewpensionhardcopy.Checked = true;
                //    chknewpensionapproved.Enabled = false;
                //    chknewpensionrejected.Enabled = false;
                //    chknewpensionhardcopy.Enabled = false;
                //}
               
            }
            if (ds != null && ds.Tables[5].Rows.Count == 0)
            {
                DivNewPensionTrust.Visible = false;
            }
            if (ds != null && ds.Tables[6].Rows.Count > 0)
            {
                GD_BenevolentFund.DataSource = ds.Tables[6];
                GD_BenevolentFund.DataBind();

                //if (Convert.ToInt32(ds.Tables[6].Rows[0]["IsApproved"]) >= 1 && Convert.ToInt32(ds.Tables[6].Rows[0]["IsApproved"]) != 2) { chkbenevelentapproved.Checked = true; chkbenevolentrejected.Enabled = false; }
                //if (chkbenevelentapproved.Checked == true)
                //{
                //    chkbenevolenthardcopy.Enabled=true;
                //    chkbenevelentapproved.Enabled = false;
                //}
                //else
                //{
                //    chkbenevolenthardcopy.Enabled = false;
                //}
                //if (Convert.ToInt32(ds.Tables[6].Rows[0]["IsApproved"]) == 2) { chkbenevolentrejected.Checked = true; }
                //if (Convert.ToInt32(ds.Tables[6].Rows[0]["IsApproved"]) >= 3)
                //{
                //    chkbenevolenthardcopy.Checked = true;
                //    chkbenevelentapproved.Enabled = false;
                //    chkbenevolenthardcopy.Enabled = false;
                //    chkbenevolentrejected.Enabled = false;
                //}
                //if (Convert.ToInt32(ds.Tables[6].Rows[0]["Ishardcopy"]) >= 1 && Convert.ToInt32(ds.Tables[6].Rows[0]["Ishardcopy"]) != 2) { chkbenevelentapproved.Checked = true; chkbenevolentrejected.Enabled = false; }
                //if (Convert.ToInt32(ds.Tables[6].Rows[0]["Ishardcopy"]) == 2) { chkbenevolentrejected.Checked = true; }

                var vvalapp5 = from varvalues6 in ds.Tables[6].AsEnumerable()
                               where varvalues6.Field<int>("IsApproved") >= 0
                               select new
                               {
                                   Approvd6 = varvalues6.Field<int>("IsApproved")
                               };

                foreach (var item6 in vvalapp5)
                {
                    if (item6.Approvd6.ToString() == "0")
                    {
                        chkbenevelentapproved.Checked = false;
                        chkbenevolenthardcopy.Checked = false;
                        chkbenevelentapproved.Enabled = true;
                        chkbenevolentrejected.Enabled = true;
                        chkbenevolenthardcopy.Enabled = false;
                        Flag5 = 0;
                    }
                    else if (item6.Approvd6.ToString() == "2")
                    {
                        chkbenevelentapproved.Checked = false;
                        chkbenevolenthardcopy.Checked = false;
                        chkbenevelentapproved.Enabled = false;
                        chkbenevolentrejected.Enabled = false;
                        chkbenevolentrejected.Checked = true;
                        chkbenevolenthardcopy.Enabled = false;
                        Flag5 = 0;
                    }
                    else
                    {
                        chkbenevelentapproved.Checked = true;
                        chkbenevolenthardcopy.Checked = false;
                        chkbenevelentapproved.Enabled = false;
                        chkbenevolentrejected.Enabled = false;
                        chkbenevolenthardcopy.Enabled = true;
                        Flag5 = 3;
                    }
                }

                if (Flag5 == 3)
                {
                    var vval5 = from varvalues in ds.Tables[6].AsEnumerable()
                                where varvalues.Field<int>("Ishardcopy") >= 0
                                select new
                                {
                                    hardcopy = varvalues.Field<int>("Ishardcopy")
                                };

                    foreach (var item in vval5)
                    {
                        if (item.hardcopy.ToString() == "0")
                        {
                            chkbenevelentapproved.Checked = true;
                            chkbenevolenthardcopy.Checked = false;
                            chkbenevelentapproved.Enabled = false;
                            chkbenevolentrejected.Enabled = false;
                            chkbenevolenthardcopy.Enabled = true;
                        }
                        else
                        {
                            chkbenevolenthardcopy.Checked = true;
                            chkbenevelentapproved.Enabled = false;
                            chkbenevolentrejected.Enabled = false;
                            chkbenevolenthardcopy.Enabled = false;
                        }
                    }
                }
                //if (Convert.ToInt32(ds.Tables[6].Rows[0]["Ishardcopy"]) >= 3)
                //{
                //    chkbenevolenthardcopy.Checked = true;
                //    chkbenevelentapproved.Enabled = false;
                //    chkbenevolenthardcopy.Enabled = false;
                //    chkbenevolentrejected.Enabled = false;
                //}
            }
            if (ds != null && ds.Tables[6].Rows.Count == 0)
            {
                DivBenevolentFund.Visible = false;
            }
            if (ds != null && ds.Tables[7].Rows.Count > 0)
            {
                GD_Encashment.DataSource = ds.Tables[7];
                GD_Encashment.DataBind();

                //if (Convert.ToInt32(ds.Tables[7].Rows[0]["IsApproved"]) >= 1 && Convert.ToInt32(ds.Tables[7].Rows[0]["IsApproved"]) != 2) { chkencashmentapproved.Checked = true; chkencashmentrejected.Enabled = false; }
                //if (chkencashmentapproved.Checked == true)
                //{
                //    chkencashmenthardcopy.Enabled = true;
                //    chkencashmentapproved.Enabled = false;
                //}
                //else
                //{
                //    chkencashmenthardcopy.Enabled = false;
                //}
                //if (Convert.ToInt32(ds.Tables[7].Rows[0]["IsApproved"]) == 2) { chkencashmentrejected.Checked = true; }
                //if (Convert.ToInt32(ds.Tables[7].Rows[0]["IsApproved"]) >= 3)
                //{
                //    chkencashmenthardcopy.Checked = true;
                //    chkencashmenthardcopy.Enabled = false;
                //    chkencashmentrejected.Enabled = false;
                //    chkencashmentapproved.Enabled = false;
                //}
                //if (Convert.ToInt32(ds.Tables[7].Rows[0]["Ishardcopy"]) >= 1 && Convert.ToInt32(ds.Tables[7].Rows[0]["Ishardcopy"]) != 2) { chkencashmentapproved.Checked = true; chkencashmentrejected.Enabled = false; }
                //if (Convert.ToInt32(ds.Tables[7].Rows[0]["Ishardcopy"]) == 2) { chkencashmentrejected.Checked = true; }



                var vvalapp6 = from varvalues7 in ds.Tables[7].AsEnumerable()
                               where varvalues7.Field<int>("IsApproved") >= 0
                               select new
                               {
                                   Approvd7 = varvalues7.Field<int>("IsApproved")
                               };

                foreach (var item7 in vvalapp6)
                {
                    if (item7.Approvd7.ToString() == "0")
                    {
                        chkencashmentapproved.Checked = false;
                        chkencashmenthardcopy.Checked = false;
                        chkencashmentapproved.Enabled = true;
                        chkencashmentrejected.Enabled = true;
                        chkencashmenthardcopy.Enabled = false;
                        Flag6 = 0;
                    }
                    else if (item7.Approvd7.ToString() == "2")
                    {
                        chkencashmentapproved.Checked = false;
                        chkencashmenthardcopy.Checked = false;
                        chkencashmentapproved.Enabled = false;
                        chkencashmentrejected.Enabled = false;
                        chkencashmentrejected.Checked = true;
                        chkencashmenthardcopy.Enabled = false;
                        Flag6 = 0;
                    }
                    else
                    {
                        chkencashmentapproved.Checked = true;
                        chkencashmenthardcopy.Checked = false;
                        chkencashmentapproved.Enabled = false;
                        chkencashmentrejected.Enabled = false;
                        chkencashmenthardcopy.Enabled = true;
                        Flag6 = 3;
                    }
                }


                if (Flag6 == 3)
                {
                    var vval6 = from varvalues in ds.Tables[7].AsEnumerable()
                                where varvalues.Field<int>("Ishardcopy") >= 0
                                select new
                                {
                                    hardcopy = varvalues.Field<int>("Ishardcopy")
                                };

                    foreach (var item in vval6)
                    {
                        if (item.hardcopy.ToString() == "0")
                        {
                            chkencashmentapproved.Checked = true;
                            chkencashmenthardcopy.Checked = false;
                            chkencashmentapproved.Enabled = false;
                            chkencashmentrejected.Enabled = false;
                            chkencashmenthardcopy.Enabled = true;
                        }
                        else
                        {
                            chkencashmenthardcopy.Checked = true;
                            chkencashmentapproved.Enabled = false;
                            chkencashmentrejected.Enabled = false;
                            chkencashmenthardcopy.Enabled = false;
                        }
                    }
                }








                //if (Convert.ToInt32(ds.Tables[7].Rows[0]["Ishardcopy"]) >= 3)
                //{
                //    chkencashmenthardcopy.Checked = true;
                //    chkencashmenthardcopy.Enabled = false;
                //    chkencashmentrejected.Enabled = false;
                //    chkencashmentapproved.Enabled = false;
                //}
               
            }
            if (ds != null && ds.Tables[7].Rows.Count == 0)
            {
                DivEnchasment.Visible = false;
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
    protected void btnsavefamily_Click(object sender, EventArgs e)
    {
        string value = string.Empty;
        if (chkfamilyapproved.Checked == true)
        { value = txtEMPCode.Text + "~" + "1" + "~" + "Family" + "~" + Session["EmailView"].ToString(); }
        if (chkfamilyreject.Checked == true)
        { value = txtEMPCode.Text + "~" + "2" + "~" + "Family" + "~" + Session["EmailView"].ToString(); }
        if (chkfamilyhardcopy.Checked == true)
        {
            value = txtEMPCode.Text + "~" + "3" + "~" + "Family" + "~" + Session["EmailView"].ToString();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "showmessage", "showmessage('" + value + "');", true);



    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnprovident_Click(object sender, EventArgs e)
    {
        string value = string.Empty;
        if (chkprovidentapproved.Checked == true)
        { value = txtEMPCode.Text + "~" + "1" + "~" + "Provident" + "~" + Session["EmailView"].ToString(); }
        if (chkprovidentrejected.Checked == true)
        { value = txtEMPCode.Text + "~" + "2" + "~" + "Provident" + "~" + Session["EmailView"].ToString(); }
        if (chkprovidenthardcopy.Checked == true)
        {
            value = txtEMPCode.Text + "~" + "3" + "~" + "Provident" + "~" + Session["EmailView"].ToString();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "showmessage", "showmessage('" + value + "');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnpension_Click(object sender, EventArgs e)
    {
        string value = string.Empty;
        if (chkpensionapproved.Checked == true)
        { value = txtEMPCode.Text + "~" + "1" + "~" + "Pension" + "~" + Session["EmailView"].ToString(); }
        if (chkpensionrejected.Checked == true)
        { value = txtEMPCode.Text + "~" + "2" + "~" + "Pension" + "~" + Session["EmailView"].ToString(); }
        if (chkpensionhardcopy.Checked == true)
        {
            value = txtEMPCode.Text + "~" + "3" + "~" + "Pension" + "~" + Session["EmailView"].ToString();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "showmessage", "showmessage('" + value + "');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnnewpension_Click(object sender, EventArgs e)
    {
        string value = string.Empty;
        if (chknewpensionapproved.Checked == true)
        { value = txtEMPCode.Text + "~" + "1" + "~" + "NewPension" + "~" + Session["EmailView"].ToString(); }
        if (chknewpensionrejected.Checked == true)
        { value = txtEMPCode.Text + "~" + "2" + "~" + "NewPension" + "~" + Session["EmailView"].ToString(); }
        if (chknewpensionhardcopy.Checked == true)
        {
            value = txtEMPCode.Text + "~" + "3" + "~" + "NewPension" + "~" + Session["EmailView"].ToString();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "showmessage", "showmessage('" + value + "');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btngratuity_Click(object sender, EventArgs e)
    {
        string value = string.Empty;
        if (chkgratuityapproved.Checked == true)
        { value = txtEMPCode.Text + "~" + "1" + "~" + "Gratuity" + "~" + Session["EmailView"].ToString(); }
        if (chkgratuityrejected.Checked == true)
        { value = txtEMPCode.Text + "~" + "2" + "~" + "Gratuity" + "~" + Session["EmailView"].ToString(); }
        if (chkgratuityhardcopy.Checked == true)
        {
            value = txtEMPCode.Text + "~" + "3" + "~" + "Gratuity" + "~" + Session["EmailView"].ToString();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "showmessage", "showmessage('" + value + "');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnbenevolent_Click(object sender, EventArgs e)
    {
        string value = string.Empty;
        if (chkbenevelentapproved.Checked == true)
        { value = txtEMPCode.Text + "~" + "1" + "~" + "Benevolent" + "~" + Session["EmailView"].ToString(); }
        if (chkbenevolentrejected.Checked == true)
        { value = txtEMPCode.Text + "~" + "2" + "~" + "Benevolent" + "~" + Session["EmailView"].ToString(); }
        if (chkbenevolenthardcopy.Checked == true)
        {
            value = txtEMPCode.Text + "~" + "3" + "~" + "Benevolent" + "~" + Session["EmailView"].ToString();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "showmessage", "showmessage('" + value + "');", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnencashment_Click(object sender, EventArgs e)
    {
        string value = string.Empty;
        if (chkencashmentapproved.Checked == true)
        { value = txtEMPCode.Text + "~" + "1" + "~" + "Encash" + "~" + Session["EmailView"].ToString(); }
        if (chkencashmentrejected.Checked == true)
        { value = txtEMPCode.Text + "~" + "2" + "~" + "Encash" + "~" + Session["EmailView"].ToString(); }
        if (chkencashmenthardcopy.Checked == true)
        {
            value = txtEMPCode.Text + "~" + "3" + "~" + "Encash" + "~" + Session["EmailView"].ToString();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "showmessage", "showmessage('" + value + "');", true);
    }
    protected void chkfamilyapproved_CheckedChanged(object sender, EventArgs e)
    {
        chkfamilyapproved.Checked=true;
        chkfamilyreject.Checked=false;
        chkfamilyhardcopy.Enabled = false;
    }
    protected void chkfamilyreject_CheckedChanged(object sender, EventArgs e)
    {
        chkfamilyapproved.Checked = false;
        chkfamilyreject.Checked = true;
        chkfamilyhardcopy.Enabled = false;
    }
    protected void chkfamilyhardcopy_CheckedChanged(object sender, EventArgs e)
    {
        chkfamilyapproved.Checked = true;
        chkfamilyreject.Checked = false;
        chkfamilyhardcopy.Checked = true;
    }
    protected void chkprovidentapproved_CheckedChanged(object sender, EventArgs e)
    {
        chkprovidentapproved.Checked=true;
        chkprovidentrejected.Checked = false;
        chkprovidenthardcopy.Enabled = false;
    }
    protected void chkprovidentrejected_CheckedChanged(object sender, EventArgs e)
    {
        chkprovidentapproved.Checked = false;
        chkprovidentrejected.Checked = true;
        chkprovidenthardcopy.Enabled = false;
    }
    protected void chkprovidenthardcopy_CheckedChanged(object sender, EventArgs e)
    {
        chkprovidentapproved.Checked = true;
        chkprovidentrejected.Checked = false;
        chkprovidenthardcopy.Checked = true;
    }
    protected void chkgratuityapproved_CheckedChanged(object sender, EventArgs e)
    {
        chkgratuityapproved.Checked=true;
            chkgratuityrejected.Checked = false;
            chkgratuityhardcopy.Enabled = false;
    }
    protected void chkgratuityrejected_CheckedChanged(object sender, EventArgs e)
    {
        chkgratuityapproved.Checked = false;
        chkgratuityrejected.Checked = true;
        chkgratuityhardcopy.Enabled = false;
    }
    protected void chkgratuityhardcopy_CheckedChanged(object sender, EventArgs e)
    {
        chkgratuityapproved.Checked = true;
        chkgratuityrejected.Checked = false;
        chkgratuityhardcopy.Checked = true;
    }
    protected void chkbenevelentapproved_CheckedChanged(object sender, EventArgs e)
    {
        chkbenevelentapproved.Checked = true;
            chkbenevolentrejected.Checked = false;
            chkbenevolenthardcopy.Enabled = false;
    }
    protected void chkbenevolentrejected_CheckedChanged(object sender, EventArgs e)
    {
        chkbenevelentapproved.Checked = false;
        chkbenevolentrejected.Checked = true;
        chkbenevolenthardcopy.Enabled = false;
    }
    protected void chkbenevolenthardcopy_CheckedChanged(object sender, EventArgs e)
    {
        chkbenevelentapproved.Checked = true;
        chkbenevolentrejected.Checked = false;
        chkbenevolenthardcopy.Checked = true;
    }
    protected void chkencashmentapproved_CheckedChanged(object sender, EventArgs e)
    {
        chkencashmentapproved.Checked = true;
            chkencashmentrejected.Checked = false;
            chkencashmenthardcopy.Enabled = false;
    }
    protected void chkencashmentrejected_CheckedChanged(object sender, EventArgs e)
    {
        chkencashmentapproved.Checked = false;
        chkencashmentrejected.Checked = true;
        chkencashmenthardcopy.Enabled = false;
    }
    protected void chkencashmenthardcopy_CheckedChanged(object sender, EventArgs e)
    {
        chkencashmentapproved.Checked = true;
        chkencashmentrejected.Checked = false;
        chkencashmenthardcopy.Checked = true;
    }
    protected void chknewpensionapproved_CheckedChanged(object sender, EventArgs e)
    {
        chknewpensionapproved.Checked=true;
            chknewpensionrejected.Checked=false;
            chknewpensionhardcopy.Enabled = false;

    }
    protected void chknewpensionrejected_CheckedChanged(object sender, EventArgs e)
    {
        chknewpensionapproved.Checked = false;
        chknewpensionrejected.Checked = true;
        chknewpensionhardcopy.Enabled = false;
    }
    protected void chknewpensionhardcopy_CheckedChanged(object sender, EventArgs e)
    {
        chknewpensionapproved.Checked = true;
        chknewpensionrejected.Checked = false;
        chknewpensionhardcopy.Checked = true;
    }
}
