using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for DatabaseFunctions
/// </summary>
public class DatabaseFunctions
{
    SqlConnection con;
    SqlCommand com;
	public DatabaseFunctions()
	{
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
	}

    public DataTable getData(string spName, params SqlParameter[] pr)
    {
        con.Open();
        com = new SqlCommand(spName, con);
        com.CommandType = CommandType.StoredProcedure;
        if (pr != null)
        {
            for (int i = 0; i < pr.Length; i++)
            {
                com.Parameters.Add(pr[i]);
            }
        }
        SqlDataReader dr = com.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(dr);
        con.Close();
        return dt;
    }
    public int InsertData(string spName, [Optional]SqlParameter[] pr)
    {
        con.Open();
        com = new SqlCommand(spName, con);
        com.CommandType = CommandType.StoredProcedure;
        if (pr != null)
        {
            for (int i = 0; i < pr.Length; i++)
            {
                com.Parameters.Add(pr[i]);
            }
        }
        int Result = com.ExecuteNonQuery();
        con.Close();
        return Result;
    }

    public void InsertUpdateDelete(string spName, [Optional]SqlParameter[] pr)
    {
        con.Open();
        com = new SqlCommand(spName, con);
        com.CommandType = CommandType.StoredProcedure;
        if (pr != null)
        {
            for (int i = 0; i < pr.Length; i++)
            {
                com.Parameters.Add(pr[i]);
            }
        }
        com.ExecuteNonQuery();
        con.Close();       
    }
    public double Match(string spName, [Optional]SqlParameter[] pr)
    {
        con.Open();
        com = new SqlCommand(spName, con);
        com.CommandType = CommandType.StoredProcedure;
        if (pr != null)
        {
            for (int i = 0; i < pr.Length; i++)
            {
                com.Parameters.Add(pr[i]);
            }
        }
        double result = Convert.ToDouble(com.ExecuteScalar());
        con.Close();

        return result;
    }

    public DataSet Setdata(string spName, params SqlParameter[] pr)
    {
        SqlDataAdapter adap = new SqlDataAdapter();
        com = new SqlCommand(spName, con);
        com.CommandType = CommandType.StoredProcedure;
        if (pr != null)
        {
            for (int i = 0; i < pr.Length; i++)
            {
                com.Parameters.Add(pr[i]);
            }
        }
        adap.SelectCommand = com;
        DataSet ds = new DataSet();

        adap.Fill(ds);
        return ds;

    }

}