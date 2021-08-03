using System;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Linq;
using System.DirectoryServices;

public partial class Default : System.Web.UI.Page
{
    byte msg;
    int flag;

    DatabaseFunctions dbClass = new DatabaseFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUserName.Text == "")
            {
                errLabel.InnerText = "Please Enter Username...!! ";
            }
            else if (txtPassword.Text == "")
            {
                errLabel.InnerText = "Please Enter Password...!! ";
            }
            else
            {

                bool flag = UserExists(txtUserName.Text, txtPassword.Text);
                if (flag)
                {

                    if (Session["UserName"].ToString().ToLower() == "tadaadmin")
                    {
                        Response.Redirect("Dashboard_Admin.aspx");
                    }
                    else if (Session["UserName"].ToString().ToLower() == "tadafinance")
                    {
                        Response.Redirect("Dashboard_Finance.aspx");
                    }
                    else
                    {
                        SqlParameter[] pr = {
                                    new SqlParameter("@EMPCODE",txtUserName.Text),
                                  };
                        DataTable dt = new DataTable();
                        dt = dbClass.getData("GetEmployeeDetailsByEMPCODE", pr);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Session["Email"] = Convert.ToString(dt.Rows[0]["EmailID"]);
                            Response.Redirect("Dashboard.aspx");
                        }
                    }
                }
                else
                {
                    errLabel.InnerText = "Username and Password is incorrect.";
                }
            }
        }
        catch (Exception ex)
        {
            errLabel.InnerText = ex.Message.ToString();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserName"></param>
    /// <returns></returns>
    public bool UserExists(string UserName, string Password)
    {
        bool flag = false;
        try
        {
            DirectoryEntry de = GetDirectoryEntry(UserName, Password);
            DirectorySearcher deSearch = new DirectorySearcher();
            deSearch.SearchRoot = de;
            deSearch.Filter = "(&(objectClass=user)(samaccountname=" + UserName + "))";
            System.DirectoryServices.SearchResult result = deSearch.FindOne();
            if (result != null)
            {
                if (UserName.ToLower() == Convert.ToString(result.Properties["samaccountname"][0]).ToLower())
                {
                    Session["UserName"] = UserName;
                    ResultPropertyCollection rpc = result.Properties;
                    foreach (string rp in rpc.PropertyNames)
                    {
                        if (rp == "displayname")
                        {
                            Session["Name"] = Convert.ToString(rpc["displayname"][0].ToString());
                        }
                        flag = true;
                    }
                }
                else
                {
                    flag = false;
                }
            }
        }
        catch (Exception ex)
        {
            errLabel.InnerText = ex.Message.ToString();
        }
        return flag;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    static DirectoryEntry GetDirectoryEntry(string username, string password)
    {
        DirectoryEntry de = new DirectoryEntry();
        de.Path = "LDAP://192.168.2.5/DC=icsi,DC=edu";
        de.Username = username;
        de.Password = password;
        de.AuthenticationType = AuthenticationTypes.Secure;
        return de;
    }
}
