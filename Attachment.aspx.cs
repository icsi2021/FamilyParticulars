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
using System.IO;
using System.Collections.Generic;
using System.Text;
public partial class Attachment : System.Web.UI.Page 
{
    string EMPCODE = string.Empty;
    string MemberName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])) || Session["UserName"] == null)
                { Response.Redirect("Default.aspx"); }
                else
                {
                    EMPCODE = Request.QueryString["EMP"].ToString();
                    MemberName = Request.QueryString["Name"].ToString();
                    ViewState["EMPCODE"] = EMPCODE;
                    ViewState["MemberName"] = MemberName;
                    string URL = Request.Url.AbsolutePath.ToString();
                    if (URL.Contains("/AdminDetails.aspx") || MemberName == "0")
                    {
                        FileUpload.Visible = false;
                        btnUpload.Visible = false;
                        GridView.Columns[4].Visible = false;
                    }
                    else
                    {
                        FileUpload.Visible = true;
                        btnUpload.Visible = true;
                    }
                   // BindGrid();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "Select EMPCODE, FILENAME,Data,MEMBERNAME,ContentType from ATTACHMENT where EMPCODE='" + TxtEmpCode.Text + "'";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                   // ViewState["MemberName"] = Convert.ToString(dt.Rows[0]["MEMBERNAME"]);
                    GridView.DataSource = dt;
                    GridView.DataBind();
                    con.Close();
                }
            }
        }
    }
    private void GetDetail() 
    {
        string constr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "Select EMP_CODE,REL_TYPE_ID from FAMILY_DETAIL where EMP_CODE='" + TxtEmpCode.Text + "'";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["MemberName"] = Convert.ToString(dt.Rows[0]["REL_TYPE_ID"]);
                   // GridView.DataSource = dt;
                    //GridView.DataBind();Z
                    con.Close();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Not Exist Family Detail');", true);
                    return;
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtEmpCode.Text == "" )
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please enter employee code');", true);
                return;
            }
            if (FileUpload.HasFile)
            {
                GetDetail();
                string extn = Path.GetExtension(FileUpload.PostedFile.FileName);
                if (extn.ToLower() == ".pdf")
                {
                    string filename = Path.GetFileName(FileUpload.PostedFile.FileName);
                    string contentType = FileUpload.PostedFile.ContentType;
                    using (Stream fs = FileUpload.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            string constr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(constr))
                            {
                                string query = "insert into ATTACHMENT values (@Empcode,@FILENAME,@Data,@MEMBERNAME,@ContentType)";
                                using (SqlCommand cmd = new SqlCommand(query))
                                {
                                    cmd.Connection = con;
                                    cmd.Parameters.AddWithValue("@Empcode", TxtEmpCode.Text);
                                    cmd.Parameters.AddWithValue("@FILENAME", filename);
                                    cmd.Parameters.AddWithValue("@Data", bytes);
                                    cmd.Parameters.AddWithValue("@MEMBERNAME", ViewState["MemberName"].ToString());
                                    cmd.Parameters.AddWithValue("@ContentType", contentType);
                                    con.Open();
                                    int i = cmd.ExecuteNonQuery();
                                    con.Close();
                                    if (i > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage1", "alertMessage1();", true);
                }
                BindGrid();
            }
        }
        catch (Exception ex) { ex.Message.ToString(); }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Download")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                Label lblempcode = (Label)row.FindControl("lblempcode");
                Label lblmembername = (Label)row.FindControl("lblmembername");
                Label lblfilename = (Label)row.FindControl("lblfilename");
                byte[] bytes;
                string filename, contentType;
                string constr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "Select EMPCODE, FILENAME,Data,MEMBERNAME,ContentType from ATTACHMENT where EMPCODE='" + lblempcode.Text + "' and MEMBERNAME='" + lblmembername.Text + "' and FILENAME='" + lblfilename.Text + "'";
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader str = cmd.ExecuteReader())
                        {
                            str.Read();
                            bytes = (byte[])str["Data"];
                            int length = bytes.Length;
                            contentType = str["ContentType"].ToString();
                            filename = str["FILENAME"].ToString();
                        }
                        con.Close();
                    }
                }
                if (bytes != null)
                {
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                    BinaryWriter bw = new BinaryWriter(Response.OutputStream);
                    bw.Write(bytes);
                    bw.Close();
                    Response.ContentType = contentType;
                    Response.End();
                }
            }
            if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                Label lblempcode = (Label)row.FindControl("lblempcode");
                Label lblmembername = (Label)row.FindControl("lblmembername");
                Label lblfilename = (Label)row.FindControl("lblfilename");
                byte[] bytes;
                string filename, contentType;
                string constr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "Delete from ATTACHMENT where EMPCODE='" + lblempcode.Text + "' and MEMBERNAME='" + lblmembername.Text + "' and FILENAME='" + lblfilename.Text + "'";
                        cmd.Connection = con;
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
        catch (Exception ex) { ex.Message.ToString(); }
    }
}
