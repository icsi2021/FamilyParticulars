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
public partial class MasterRelation : System.Web.UI.Page
{
    string EMPCode = string.Empty;
    /// <summary>
    /// 
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
                DataTable dtCode = GetEmployeeByID(Convert.ToString(Session["UserName"].ToString()));
                if (dtCode != null && dtCode.Rows.Count > 0)
                {
                    BindUserDetails(Convert.ToString(Session["UserName"].ToString()));
                }
                else if (Gd_master.Rows.Count <= 0 && dtCode.Rows.Count <= 0)
                {
                    SetInitialRow();
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
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetRelationByEmpCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EMPCODE", SqlDbType.NVarChar).Value = Code;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["CurrentTable"] = dt;
                Gd_master.DataSource = dt;
                Gd_master.DataBind();
                int rowIndex = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label labelrow = (Label)Gd_master.Rows[rowIndex].Cells[0].FindControl("labelrow");
                    DropDownList ddlform = (DropDownList)Gd_master.Rows[rowIndex].Cells[2].FindControl("ddlform");
                    TextBox txtfamilyname = (TextBox)Gd_master.Rows[rowIndex].Cells[3].FindControl("txtfamilyname");
                    TextBox txtrelationship = (TextBox)Gd_master.Rows[rowIndex].Cells[4].FindControl("txtrelationship");
                    DropDownList ddlchkdob = (DropDownList)Gd_master.Rows[rowIndex].Cells[5].FindControl("ddlchkdob");
                    TextBox txtdob = (TextBox)Gd_master.Rows[rowIndex].Cells[6].FindControl("txtdob");
                    ddlform.ClearSelection();
                    ddlchkdob.ClearSelection();

                    labelrow.Text = Convert.ToString(dt.Rows[i]["TYPE_ID"]);
                    txtfamilyname.Text = Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]);
                    ddlform.Items.FindByText(Convert.ToString(dt.Rows[i]["Form"])).Selected = true;
                    ddlchkdob.Items.FindByText(Convert.ToString(dt.Rows[i]["IsActualDate"])).Selected = true;
                    txtdob.Text = Convert.ToString(dt.Rows[i]["DOB"]);
                    txtrelationship.Text = Convert.ToString(dt.Rows[i]["TYPE_NAME"]);
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
        View();
        int m = SaveItemDetails();
        if (m > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true); ViewState["CurrentTable"] = null;
        }
    }
    /// <summary>
    /// Set Initial Row For GridViewDetails
    /// </summary>
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("TYPE_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("TYPE_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("DOB", typeof(string)));
        dt.Columns.Add(new DataColumn("IsActualDate", typeof(string)));
        dt.Columns.Add(new DataColumn("FML_MEMBER_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("Form", typeof(string)));

        dr = dt.NewRow();
        dr["TYPE_ID"] = 0;
        dr["TYPE_NAME"] = string.Empty;
        dr["DOB"] = string.Empty;
        dr["IsActualDate"] = string.Empty;
        dr["FML_MEMBER_NAME"] = string.Empty;
        dr["Form"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        Gd_master.DataSource = dt;
        Gd_master.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
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
                    if (Convert.ToString(dt.Rows[i]["TYPE_NAME"]) != "" && Convert.ToString(dt.Rows[i]["IsActualDate"]) != "" && Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]) != "")
                    {
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "InsertMemberRelation";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TYPE_ID", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[i]["TYPE_ID"]);
                        cmd.Parameters.Add("@TYPE_NAME", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["TYPE_NAME"]);
                        cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["DOB"]);
                        cmd.Parameters.Add("@IsActualDate", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["IsActualDate"]);
                        cmd.Parameters.Add("@FML_MEMBER_NAME", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]);
                        cmd.Parameters.Add("@Form", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[i]["Form"]);
                        cmd.Parameters.Add("@EmpCode", SqlDbType.NVarChar).Value = Convert.ToString(Session["UserName"].ToString());
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
        }
        return k;
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
                drCurrentRow["TYPE_ID"] = 0;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["CurrentTable"] = dtCurrentTable;
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    Label labelrow = (Label)Gd_master.Rows[i].Cells[0].FindControl("labelrow");
                    DropDownList ddlform = (DropDownList)Gd_master.Rows[i].Cells[2].FindControl("ddlform");
                    TextBox txtfamilyname = (TextBox)Gd_master.Rows[i].Cells[3].FindControl("txtfamilyname");
                    TextBox txtrelationship = (TextBox)Gd_master.Rows[i].Cells[4].FindControl("txtrelationship");
                    DropDownList ddlchkdob = (DropDownList)Gd_master.Rows[i].Cells[5].FindControl("ddlchkdob");
                    TextBox txtdob = (TextBox)Gd_master.Rows[i].Cells[6].FindControl("txtdob");

                    //Assign the value from DataTable to the TextBox   
                    dtCurrentTable.Rows[i]["TYPE_ID"] = labelrow.Text = "0";
                    dtCurrentTable.Rows[i]["TYPE_NAME"] = txtrelationship.Text;
                    dtCurrentTable.Rows[i]["DOB"] = txtdob.Text;
                    dtCurrentTable.Rows[i]["IsActualDate"] = ddlchkdob.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["FML_MEMBER_NAME"] = txtfamilyname.Text;
                    dtCurrentTable.Rows[i]["Form"] = ddlform.SelectedItem.Text;
                }
                Gd_master.DataSource = dtCurrentTable;
                Gd_master.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
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
                    Label labelrow = (Label)Gd_master.Rows[rowIndex].Cells[0].FindControl("labelrow");
                    DropDownList ddlform = (DropDownList)Gd_master.Rows[rowIndex].Cells[2].FindControl("ddlform");
                    TextBox txtfamilyname = (TextBox)Gd_master.Rows[rowIndex].Cells[3].FindControl("txtfamilyname");
                    TextBox txtrelationship = (TextBox)Gd_master.Rows[rowIndex].Cells[4].FindControl("txtrelationship");
                    DropDownList ddlchkdob = (DropDownList)Gd_master.Rows[rowIndex].Cells[5].FindControl("ddlchkdob");
                    TextBox txtdob = (TextBox)Gd_master.Rows[rowIndex].Cells[6].FindControl("txtdob");
                    if (i < dt.Rows.Count - 1)
                    {
                        ddlform.ClearSelection();
                        ddlchkdob.ClearSelection();

                        labelrow.Text = Convert.ToString(dt.Rows[i]["TYPE_ID"]);
                        txtfamilyname.Text = Convert.ToString(dt.Rows[i]["FML_MEMBER_NAME"]);
                        ddlform.Items.FindByText(Convert.ToString(dt.Rows[i]["Form"])).Selected = true;
                        ddlchkdob.Items.FindByText(Convert.ToString(dt.Rows[i]["IsActualDate"])).Selected = true;
                        txtdob.Text = Convert.ToString(dt.Rows[i]["DOB"]);
                        txtrelationship.Text = Convert.ToString(dt.Rows[i]["TYPE_NAME"]);
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
    protected void btnaddrow_Click(object sender, EventArgs e)
    {
        // New Added By Vinay Singh.
        View();
        AddNewRowToGrid();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gd_master_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                ImageButton lb = (ImageButton)e.Row.FindControl("btnimagedelete");
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
            Label labelrow = (Label)Gd_master.Rows[rowID].Cells[0].FindControl("labelrow");
            dt = (DataTable)ViewState["CurrentTable"];
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
                View();
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "DeleteRowByID";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = Convert.ToString(Session["UserName"].ToString());
                    cmd.Parameters.Add("@RowNumber", SqlDbType.Int).Value = Convert.ToInt32(labelrow.Text);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        BindUserDetails(Convert.ToString(Session["UserName"].ToString()));
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertDelete", "alertDelete();", true);

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
        ViewState["CurrentTable"] = dt;
        //Re bind the GridView for the updated data  
        Gd_master.DataSource = dt;
        Gd_master.DataBind();
        //Set Previous Data on Postbacks  
        SetPreviousData();
        // updatefamily.Update();
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
            cmd.CommandText = "GetRelationByEmpCode";
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
    public void View()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        int rowIndex = 0;
        if (Gd_master.Rows.Count > 0)
        {
            for (int i = 1; i <= Gd_master.Rows.Count; i++)
            {
                //extract the TextBox values   
                Label labelrow = (Label)Gd_master.Rows[rowIndex].Cells[0].FindControl("labelrow");
                DropDownList ddlform = (DropDownList)Gd_master.Rows[rowIndex].Cells[2].FindControl("ddlform");
                TextBox txtfamilyname = (TextBox)Gd_master.Rows[rowIndex].Cells[3].FindControl("txtfamilyname");
                TextBox txtrelationship = (TextBox)Gd_master.Rows[rowIndex].Cells[4].FindControl("txtrelationship");
                DropDownList ddlchkdob = (DropDownList)Gd_master.Rows[rowIndex].Cells[5].FindControl("ddlchkdob");
                TextBox txtdob = (TextBox)Gd_master.Rows[rowIndex].Cells[6].FindControl("txtdob");
                //FillDropDownList(ddlREL_TYPE_ID);              
                if (dtCurrentTable != null && dtCurrentTable.Rows.Count > 0)
                {
                    if (labelrow.Text == "")
                    {
                        dtCurrentTable.Rows[i - 1]["TYPE_ID"] = "0";
                    }
                    else
                    {
                        dtCurrentTable.Rows[i - 1]["TYPE_ID"] = Convert.ToString(labelrow.Text);
                    }
                    dtCurrentTable.Rows[i - 1]["TYPE_NAME"] = txtrelationship.Text;
                    dtCurrentTable.Rows[i - 1]["DOB"] = txtdob.Text;
                    dtCurrentTable.Rows[i - 1]["IsActualDate"] = ddlchkdob.SelectedItem.Text;
                    dtCurrentTable.Rows[i - 1]["FML_MEMBER_NAME"] = txtfamilyname.Text;
                    dtCurrentTable.Rows[i - 1]["Form"] = ddlform.SelectedItem.Text;
                    rowIndex++;
                }
            }
            ViewState["CurrentTable"] = dtCurrentTable;
        }
    }
}
