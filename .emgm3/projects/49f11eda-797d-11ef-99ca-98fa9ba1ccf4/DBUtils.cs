using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    public enum LookupType
    {
        lkpFirst,
        lkpMax,
        lkpMin,
        lkpAvg,
        lkpSum,
        lkpCount,
        lkpStdev
    }
    internal static class DBUtils
    {
        // <summary>
        // 执行存储过程
        // </summary>
        // <param name="cmdText"></param>
        // <param name="cmdParms"></param>
        // <param name="cmdType"></param>
        // <returns></returns>
        // <remarks></remarks>
        public static int ExeSqlCommand(String cmdText, SqlParameter[] cmdParms = null, CommandType cmdType = CommandType.Text)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(Const.DB_CONNECT);
            int nAffect = 0;
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                nAffect = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return nAffect;
            }
            catch (Exception ex)
            {
                conn.Close();
                //System.Windows.Forms.MessageBox.Show("ex=" + ex.Message);
                return 0;
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // <summary>
        // 执行参数预处理，将参数添加到Command中去
        // </summary>
        // <param name="cmd">Sql命令对象</param>
        // <param name="conn">Sql连接</param>
        // <param name="trans">事务</param>
        // <param name="cmdType">类型</param>
        // <param name="cmdText">命令文本</param>
        // <param name="cmdParms">参数</param>
        // <remarks></remarks>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, String cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }
        //<summary>
        //执行一个SELECT语句，并返回一个DataSet对象，
        //在不需要DataSet特有的功能时，建议使用SqlDataReader以获得更好的性能
        //</summary>
        //<param name="cmdType">System.Data.cmdType, 如Text, StoredProcedure</param>
        //<param name="cmdText">SQL语句或存储过程名</param>
        //<param name="cmdParameters">SQL语句或存储过程参数</param>
        //<returns>DataSet对象，不会是Nothing，并且肯定有一个Table</returns>
        //<remarks></remarks>
        public static DataSet GetDataSet(CommandType cmdType, String cmdText)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection Conn = new SqlConnection(Const.DB_CONNECT);
            DataSet ds = new DataSet();
            try
            {
                PrepareCommand(cmd, Conn, null, cmdType, cmdText, new SqlParameter[0]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(ds);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                ds.Tables.Add();
                throw ex;
            }
            finally
            {
                Conn.Close();
            }
            Conn.Close();
            Conn.Dispose();
            return ds;
        }
        // <summary>
        // 将空值或者Nothing转换成""
        // </summary>
        // <param name="o"></param> 
        // <returns></returns>
        // <remarks></remarks>
        public static String Nz(Object o)
        {
            if (o == null)
            {
                return "";
            }
            else
            {
                if ("".Equals(o.ToString()) || "0".Equals(o.ToString()))
                {
                    return "";
                }
                else
                {
                    return o.ToString();
                }
            }
        }
        // <summary>
        // 查找数据库中的某个表中的符合某个条件的记录中的某个字段值可以使用域合计函数
        // </summary>
        // <param name="fldName">字段名称</param>
        // <param name="tblName">表名称</param>
        // <param name="filterStr">Where表达式</param>
        // <param name="mLookupType">聚合函数类型</param>
        // <param name="mOrderByStr " >排序条件，请使用 字段名称 Asc 或者 字段名称 Desc 格式</param>
        // <returns>返回Object，如果出现错误或者数据为空，返回""</returns>
        // <remarks></remarks>
        public static Object Dlookup(String fldName, String tblName, String filterStr = "", LookupType mLookupType = LookupType.lkpFirst, String mOrderByStr = "")
        {

            String cmdText = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(Const.DB_CONNECT);
            if (tblName != "" && fldName != "")
            {
                try
                {
                    switch (mLookupType)
                    {
                        case LookupType.lkpAvg:
                            cmdText = "avg(" + fldName + ")";
                            break;
                        case LookupType.lkpCount:
                            cmdText = "Count(" + fldName + ")";
                            break;
                        case LookupType.lkpFirst:
                            cmdText = "Top 1 " + fldName;
                            break;
                        case LookupType.lkpMax:
                            cmdText = "Max(" + fldName + ")";
                            break;
                        case LookupType.lkpMin:
                            cmdText = "Min(" + fldName + ")";
                            break;
                        case LookupType.lkpStdev:
                            cmdText = "Stdev(" + fldName + ")";
                            break;
                        case LookupType.lkpSum:
                            cmdText = "Sum(" + fldName + ")";
                            break;
                    }
                    cmdText = "Select " + cmdText + " From " + tblName;
                    if (filterStr != "")
                    {
                        cmdText += " Where " + filterStr;
                    }
                    {
                        if (mOrderByStr != "")
                            cmdText += " Order By " + mOrderByStr;
                    }

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    cmd.Connection = conn;
                    cmd.CommandText = cmdText;
                    cmd.CommandType = CommandType.Text;
                    Object rt = cmd.ExecuteScalar();
                    if ("".Equals(rt) || rt == null)
                    {
                        rt = "";
                    }
                    return rt;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.Message);
                    return "";
                }
                finally
                {
                    cmd = null;
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            else
            {
                cmd = null;
                conn.Close();
                conn.Dispose();
                conn = null;
                return "";
            }
        }

    }
}
