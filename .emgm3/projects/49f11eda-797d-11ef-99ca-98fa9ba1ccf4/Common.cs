using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using GMSDK;
namespace MultiCodeDataEventDriven
{
    public static class Common
    {
        //五分钟间隔交易时间
        private static string[] timeRuleFiveMinute = {"09:30:00","09:35:00","09:40:00","09:45:00","09:50:00","09:55:00","10:00:00","10:05:00","10:10:00","10:15:00","10:20:00","10:25:00","10:30:00",
                "10:35:00","10:40:00","10:45:00","10:50:00","10:55:00","11:00:00","11:05:00","11:10:00","11:15:00","11:20:00","11:25:00","11:30:00","13:05:00","13:10:00","13:15:00",
                "13:20:00","13:25:00","13:30:00","13:35:00","13:40:00","13:45:00","13:50:00","13:55:00","14:00:00","14:05:00","14:10:00","14:15:00","14:20:00","14:25:00","14:30:00","14:35:00",
                "14:40:00","14:45:00","14:50:00","14:55:00","15:00:00"};
        //十五分钟间隔交易时间
        private static string[] timeRuleFifteenMinute = { "09:30:00", "09:45:00", "10:00:00", "10:15:00", "10:30:00", "10:45:00", "11:00:00", "11:15:00", "11:30:00", "13:15:00", "13:30:00", "13:45:00", "14:00:00", "14:15:00", "14:30:00", "14:45:00", "15:00:00" };
        //三十分钟间隔交易时间
        private static string[] timeRuleThirtyMinute = { "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00" };
        //六十分钟间隔交易时间
        private static string[] timeRuleSixtyMinute = { "09:30:00", "10:30:00", "11:30:00", "14:00:00", "15:00:00" };
        //日间隔交易时间
        private static string[] timeRuleDay = { "09:30:00", "14:50:00" };

        //decimal类型转换为float类型
        public static float decimalToFloat(decimal src)
        {
            float retValue = 0f;
            try
            {
                retValue = float.Parse(src.ToString());
            }
            catch
            {
                retValue = 0f;
            }

            //返回值
            return retValue;
        }
        //string类型转换为float类型
        public static float stringToFloat(string src)
        {
            float retValue = 0f;
            try
            {
                retValue = float.Parse(src);
            }
            catch
            {
                retValue = 0f;
            }

            //返回值
            return retValue;
        }
        //decimal类型转换为float类型
        public static double stringToDouble(string src)
        {
            double retValue = 0;
            try
            {
                retValue = double.Parse(src);
            }
            catch
            {
                retValue = 0;
            }

            //返回值
            return retValue;
        }
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
        public static double getAvg(ref ArrayList arrayList, int position, int dictValue)
        {
            //周期合计收盘价
            double sumValue = 0;
            if (position - (dictValue - 1) >= 0)
            {
                for (int i = position - (dictValue - 1); i <= position; i++)
                {
                    sumValue = (sumValue + ((TechnicalIndicators)arrayList[i]).bar.close);
                }
            }
            //返回周期平均值
            return sumValue / dictValue;
        }
        public static double getSTD(ref ArrayList arrayList, int position, int dictValue, double BBIBOLL, string BOLLTyep)
        {
            //标准差
            double sumValue = 0.00;
            double sumBollBBI = 0.00;
            if ("BBIBOLL".Equals(BOLLTyep))
            {
                for (int i = position - (dictValue - 1); i <= position; i++)
                {
                    sumBollBBI = sumBollBBI + ((TechnicalIndicators)arrayList[i]).reserve033;  //BBIBOLLBBI;
                }
            }
            for (int i = position - (dictValue - 1); i <= position; i++)
            {
                if ("BOLL".Equals(BOLLTyep))
                {
                    sumValue = sumValue + Math.Pow(((TechnicalIndicators)arrayList[i]).bar.close - BBIBOLL, 2);
                }
                if ("BBIBOLL".Equals(BOLLTyep))
                {
                    sumValue = sumValue + Math.Pow(((TechnicalIndicators)arrayList[i]).reserve033 - sumBollBBI / double.Parse(dictValue.ToString()), 2);
                }
            }
            sumValue = sumValue / (double.Parse(dictValue.ToString()) - 1);

            //返回标准差
            return Math.Round(Math.Sqrt(sumValue), 2);
        }
        public static int limitUpDown(string symbol, float closeCurrent, float closePrevious, ref decimal limitUpPrice)
        {
            int limitUpDownIdent = 0;
            double LimitRange = 1.1;
            //科创板及创业板股票涨跌幅20%
            if ("68".Equals(symbol.Substring(5, 2)) || "3".Equals(symbol.Substring(5, 1)))
            {
                LimitRange = 0.2;
            }
            else
            {
                LimitRange = 0.1;
            }
            //涨停
            if ((decimal.Parse(closeCurrent.ToString()) - decimal.Parse(Math.Round(Math.Round((closePrevious * (1 + LimitRange)), 4) + 0.0001, 2).ToString())) == 0)
            {
                limitUpDownIdent = Const.Limit_Up;
            }
            //跌停
            if ((decimal.Parse(closeCurrent.ToString()) - decimal.Parse(Math.Round(Math.Round((closePrevious * (1 - LimitRange)), 4) + 0.0001, 2).ToString())) == 0)
            {
                limitUpDownIdent = Const.Limit_Down;
            }
            limitUpPrice = decimal.Parse(Math.Round(Math.Round((closePrevious * (1 + LimitRange)), 4) + 0.0001, 2).ToString());

            return limitUpDownIdent;
        }
        //将数据转换为技术指标类
        public static TechnicalIndicatorsList getTechnicalIndicatorsListByHistoryBarsN(string symbols, string frequency, int n, Adjust adjust)
        {
            TechnicalIndicatorsList technicalIndicatorsList = new TechnicalIndicatorsList();

            //获取从N天内股票日线
            GMDataList<Bar> historyBar = GMApi.HistoryBarsN(symbols, frequency, n, null, adjust);

            technicalIndicatorsList = new TechnicalIndicatorsList(historyBar);

            return technicalIndicatorsList;
        }
        //将数据转换为技术指标类
        public static TechnicalIndicatorsList getTechnicalIndicatorsListByHistoryBars(string symbols, string frequency, string startTime, string endTime, Adjust adjust)
        {
            TechnicalIndicatorsList technicalIndicatorsList = new TechnicalIndicatorsList();

            //获取从N天内股票日线
            GMDataList<Bar> historyBar = GMApi.HistoryBars(symbols, frequency, startTime, endTime, adjust);

            technicalIndicatorsList = new TechnicalIndicatorsList(historyBar);

            return technicalIndicatorsList;
        }
        public static TechnicalIndicatorsList getTechnicalIndicatorsListByHistoryBarsN(string symbols, string frequency, int n, string endTime, Adjust adjust)
        {
            TechnicalIndicatorsList technicalIndicatorsList = new TechnicalIndicatorsList();

            //获取从N天内股票日线
            GMDataList<Bar> historyBar = GMApi.HistoryBarsN(symbols, frequency, n, endTime, adjust);

            technicalIndicatorsList = new TechnicalIndicatorsList(historyBar);

            return technicalIndicatorsList;
        }
        //获取Tick
        public static GMDataList<Tick> getHistoryTicks(string symbols, string startTime, string endTime, Adjust adjust)
        {
            //获取Tick
            GMDataList<Tick> historyTicks = GMApi.HistoryTicks(symbols, startTime, endTime, adjust);

            return historyTicks;
        }
        //获取Tick
        public static GMDataList<Tick> getHistoryTicksN(string symbols, int n, string endTime, Adjust adjust)
        {
            //获取Tick
            GMDataList<Tick> historyTicks = GMApi.HistoryTicksN(symbols, n, endTime, adjust);

            return historyTicks;
        }
        public static GMDataList<Tick> getHistoryTicksN(string symbols, int n, Adjust adjust)
        {
            //获取Tick
            GMDataList<Tick> historyTicks = GMApi.HistoryTicksN(symbols, n, null, adjust);

            return historyTicks;
        }
        //获取Tick
        public static float getTradePrice(Tick tick, int orderSide, Trade trade, float tradePrice)
        {
            float result = 0.0f;

            if (tick == null)
            {
                result = tradePrice;
            } else {
                Quote[] quotes = tick.quotes;
                //如果数据存在
                if (quotes.Length > 0)
                {
                    //买入
                    if (orderSide == Const.ORDERSIDE_BUY)
                    {
                        //委买价一
                        if (Const.BID_PRICE_ONE.Equals(trade.TradeInfomation.BidPrice))
                        {
                            result = quotes[0].bidPrice;
                        }
                        //委买价二
                        if (Const.BID_PRICE_TWO.Equals(trade.TradeInfomation.BidPrice))
                        {
                            result = quotes[1].bidPrice;
                        }
                        //委买价三
                        if (Const.BID_PRICE_THREE.Equals(trade.TradeInfomation.BidPrice))
                        {
                            result = quotes[2].bidPrice;
                        }
                        //委买价四
                        if (Const.BID_PRICE_FOUR.Equals(trade.TradeInfomation.BidPrice))
                        {
                            result = quotes[3].bidPrice;
                        }
                        //委买价五
                        if (Const.BID_PRICE_FIVE.Equals(trade.TradeInfomation.BidPrice))
                        {
                            result = quotes[4].bidPrice;
                        }
                    }
                    //卖出
                    if (orderSide == Const.ORDERSIDE_SELL)
                    {
                        //委卖价一
                        if (Const.ASK_PRICE_ONE.Equals(trade.TradeInfomation.AskPrice))
                        {
                            result = quotes[0].askPrice;
                        }
                        //委卖价二
                        if (Const.ASK_PRICE_TWO.Equals(trade.TradeInfomation.AskPrice))
                        {
                            result = quotes[1].askPrice;
                        }
                        //委卖价三
                        if (Const.ASK_PRICE_THREE.Equals(trade.TradeInfomation.AskPrice))
                        {
                            result = quotes[2].askPrice;
                        }
                        //委卖价四
                        if (Const.ASK_PRICE_FOUR.Equals(trade.TradeInfomation.AskPrice))
                        {
                            result = quotes[3].askPrice;
                        }
                        //委卖价五
                        if (Const.ASK_PRICE_FIVE.Equals(trade.TradeInfomation.AskPrice))
                        {
                            result = quotes[4].askPrice;
                        }
                    }
                }
            }
            return result;
        }
        //获取股市交易时间
        /* 入参 string frequency  频率
         *  返回值 5分钟或者15分钟股市交易时间
         */
        public static string[] getTimeRule(string frequency)
        {
            //五分钟间隔交易时间
            if (Const.FREQUENCY_5MINUT.Equals(frequency)) {
                return timeRuleFiveMinute;
            }
            //十五分钟间隔交易时间
            if (Const.FREQUENCY_15MINUT.Equals(frequency))
            {
                return timeRuleFifteenMinute;
            }
            //三十分钟间隔交易时间
            if (Const.FREQUENCY_30MINUT.Equals(frequency))
            {
                return timeRuleThirtyMinute;
            }
            //六十分钟间隔交易时间
            if (Const.FREQUENCY_60MINUT.Equals(frequency))
            {
                return timeRuleSixtyMinute;
            }
            //日间隔交易时间
            if (Const.FREQUENCY_DAY.Equals(frequency))
            {
                return timeRuleDay;
            }

            return null;
        }
        //获取股市交易时间间隔
        /* 入参 string frequency  频率
         * 入参 timeRule 交易时间
         *  返回值 入参timeRule的前一个交易时间
         */
        public static string getPreviousTimeRule(string frequency, DateTime Now, string timeRule, bool isTick)
        {
            string previousTimeRule = "";
            if (Const.FREQUENCY_5MINUT.Equals(frequency))
            {
                //查找入参timeRule的前一个交易时间间隔
                for (int i = 1; i < timeRuleFiveMinute.Length; i++) {
                    if (timeRule.Equals(timeRuleFiveMinute[i]))
                    {
                        if (isTick)
                        {
                            previousTimeRule = Now.ToString("yyyy-MM-dd") + " " + timeRuleFiveMinute[i - 1];
                        } else {
                            previousTimeRule = Now.AddDays(-20).ToString("yyyy-MM-dd") + " " + timeRuleFiveMinute[i - 1];
                        }
                        break;
                    }
                }
            }
            if (Const.FREQUENCY_15MINUT.Equals(frequency))
            {
                //查找入参timeRule的前一个交易时间间隔
                for (int i = 1; i < timeRuleFifteenMinute.Length; i++)
                {
                    if (timeRule.Equals(timeRuleFifteenMinute[i]))
                    {
                        if (isTick)
                        {
                            previousTimeRule = Now.ToString("yyyy-MM-dd") + " " + timeRuleFifteenMinute[i - 1];
                        } else
                        {
                            previousTimeRule = Now.AddDays(-60).ToString("yyyy-MM-dd") + " " + timeRuleFifteenMinute[i - 1];
                        }
                        break;
                    }
                }
            }
            if (Const.FREQUENCY_30MINUT.Equals(frequency))
            {
                //查找入参timeRule的前一个交易时间间隔
                for (int i = 1; i < timeRuleThirtyMinute.Length; i++)
                {
                    if (timeRule.Equals(timeRuleThirtyMinute[i]))
                    {
                        if (isTick)
                        {
                            previousTimeRule = Now.ToString("yyyy-MM-dd") + " " + timeRuleThirtyMinute[i - 1];
                        } else
                        {
                            previousTimeRule = Now.AddDays(-120).ToString("yyyy-MM-dd") + " " + timeRuleThirtyMinute[i - 1];
                        }
                        break;
                    }
                }
            }
            if (Const.FREQUENCY_60MINUT.Equals(frequency))
            {
                //查找入参timeRule的前一个交易时间间隔
                for (int i = 1; i < timeRuleSixtyMinute.Length; i++)
                {
                    if (timeRule.Equals(timeRuleSixtyMinute[i]))
                    {
                        if (isTick)
                        {
                            previousTimeRule = Now.ToString("yyyy-MM-dd") + " " + timeRuleSixtyMinute[i - 1];
                        } else
                        {
                            previousTimeRule = Now.AddDays(-240).ToString("yyyy-MM-dd") + " " + timeRuleSixtyMinute[i - 1];
                        }
                        break;
                    }
                }
            }
            if (Const.FREQUENCY_DAY.Equals(frequency))
            {
                //查找入参timeRule的前一个交易时间间隔
                for (int i = 1; i < timeRuleDay.Length; i++)
                {
                    if (timeRule.Equals(timeRuleDay[i]))
                    {
                        if (isTick)
                        {
                            previousTimeRule = Now.ToString("yyyy-MM-dd") + " " + timeRuleDay[i];
                        } else
                        {
                            previousTimeRule = Now.AddDays(-960).ToString("yyyy-MM-dd") + " " + timeRuleDay[i];
                        }
                        break;
                    }
                }
            }
            return previousTimeRule;
        }
        //根据参数种类，获取参数值
        public static string getParameterValue(string dictTypeID)
        {
            string parameterValue = "";
            //查询交易自选股表
            string sql = " select dictValue from tbDictEntry where dictTypeID ='" + dictTypeID + "' and isUsedAsParameterValues = 'Y' order by sortNo ";
            DataTable dataTable = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];
            //如果有数据，则取第一条数据
            if (dataTable.Rows.Count > 0)
            {
                parameterValue = (string)dataTable.Rows[0].ItemArray[0];
            }
            return parameterValue;
        }
        //获取指数成份股信息
        public static GMData<DataTable> getConstituents(string securitiesExchange, string securitiesBlock)
        {
            GMData<DataTable> constituents = new GMData<DataTable>();          //指数成份股

            //上海深圳所有股票
            if (Const.SHSZSE.Equals(securitiesExchange) && Const.ALL_BLOCK.Equals(securitiesBlock))
            {
                //查询上证指数成份股
                GMData<DataTable> constituentsSHSE = GMApi.GetConstituents(Const.CONSTITUENTS_SH, DateTime.Now.ToString("yyyy-MM-dd"));

                //查询深证指数成份股
                GMData<DataTable> constituentsSZSE = GMApi.GetConstituents(Const.CONSTITUENTS_SZ, DateTime.Now.ToString("yyyy-MM-dd"));

                //上海深圳所有股票
                constituents = mergeConstituents(securitiesExchange, securitiesBlock, constituentsSHSE, constituentsSZSE);
            }
            //上海所有股票
            if (Const.SHSE.Equals(securitiesExchange) && Const.ALL_BLOCK.Equals(securitiesBlock))
            {
                //查询上证指数成份股
                constituents = GMApi.GetConstituents(Const.CONSTITUENTS_SH, DateTime.Now.ToString("yyyy-MM-dd"));

            }
            //深圳所有股票
            if (Const.SZSE.Equals(securitiesExchange) && Const.ALL_BLOCK.Equals(securitiesBlock))
            {
                //查询深圳指数成份股
                constituents = GMApi.GetConstituents(Const.CONSTITUENTS_SZ, DateTime.Now.ToString("yyyy-MM-dd"));

            }
            //上海所有科创板股票
            if (Const.SHSE.Equals(securitiesExchange) && Const.STI_BLOCK.Equals(securitiesBlock))
            {
                //查询上证指数成份股
                GMData<DataTable> constituentsSHSE = GMApi.GetConstituents(Const.CONSTITUENTS_SH, DateTime.Now.ToString("yyyy-MM-dd"));
                //上海创板股票
                constituents = mergeConstituents(securitiesExchange, securitiesBlock, constituentsSHSE, null);
            }
            //深圳所有创业板股票
            if (Const.SZSE.Equals(securitiesExchange) && Const.CHINEXT.Equals(securitiesBlock))
            {
                //查询深证指数成份股
                GMData<DataTable> constituentsSZSE = GMApi.GetConstituents(Const.CONSTITUENTS_SZ, DateTime.Now.ToString("yyyy-MM-dd"));
                //深圳所有创业板股票
                constituents = mergeConstituents(securitiesExchange, securitiesBlock, null, constituentsSZSE);
            }
            //上海所有主板股票
            if (Const.SHSE.Equals(securitiesExchange) && Const.MAIN_BLOCK.Equals(securitiesBlock))
            {
                //查询上证指数成份股
                GMData<DataTable> constituentsSHSE = GMApi.GetConstituents(Const.CONSTITUENTS_SH, DateTime.Now.ToString("yyyy-MM-dd"));
                //上海所有主板股票
                constituents = mergeConstituents(securitiesExchange, securitiesBlock, constituentsSHSE, null);
            }
            //深圳所有主板股票
            if (Const.SZSE.Equals(securitiesExchange) && Const.MAIN_BLOCK.Equals(securitiesBlock))
            {
                //查询深证指数成份股
                GMData<DataTable> constituentsSZSE = GMApi.GetConstituents(Const.CONSTITUENTS_SZ, DateTime.Now.ToString("yyyy-MM-dd"));
                //深圳所有主板股票
                constituents = mergeConstituents(securitiesExchange, securitiesBlock, null, constituentsSZSE);
            }
            //上海深圳所有主板股票
            if (Const.SHSZSE.Equals(securitiesExchange) && Const.MAIN_BLOCK.Equals(securitiesBlock))
            {
                //查询上证指数成份股
                GMData<DataTable> constituentsSHSE = GMApi.GetConstituents(Const.CONSTITUENTS_SH, DateTime.Now.ToString("yyyy-MM-dd"));

                //查询深证指数成份股
                GMData<DataTable> constituentsSZSE = GMApi.GetConstituents(Const.CONSTITUENTS_SZ, DateTime.Now.ToString("yyyy-MM-dd"));

                //上海深圳所有股票
                constituents = mergeConstituents(securitiesExchange, securitiesBlock, constituentsSHSE, constituentsSZSE);
            }
            return constituents;
        }
        //合并上证和深圳成份股
        public static GMData<DataTable> mergeConstituents(string securitiesExchange, string securitiesBlock, GMData<DataTable> constituentsSHSE = null, GMData<DataTable> constituentsSZSE = null)
        {
            GMData<DataTable> constituentsSE = new GMData<DataTable>();

            //上海深圳所有股票
            if (Const.SHSZSE.Equals(securitiesExchange) && Const.ALL_BLOCK.Equals(securitiesBlock))
            {
                constituentsSE.data = constituentsSHSE.data;
                for (int i = 0; i < constituentsSZSE.data.Rows.Count; i++)
                {
                    constituentsSE.data.Rows.Add(constituentsSZSE.data.Rows[i].ItemArray);
                }
            }
            //上海所有科创板股票
            if (Const.SHSE.Equals(securitiesExchange) && Const.STI_BLOCK.Equals(securitiesBlock))
            {
                constituentsSE.data = constituentsSHSE.data;
                for (int i = 0; i < constituentsSE.data.Rows.Count; i++)
                {
                    if (!"68".Equals(((string)constituentsSE.data.Rows[i].ItemArray[1]).Substring(5, 2)))
                    {
                        constituentsSE.data.Rows.Remove(constituentsSE.data.Rows[i]);
                    }
                }
            }
            //深圳所有创业板股票
            if (Const.SZSE.Equals(securitiesExchange) && Const.CHINEXT.Equals(securitiesBlock))
            {
                constituentsSE.data = constituentsSZSE.data;
                for (int i = 0; i < constituentsSE.data.Rows.Count; i++)
                {
                    if (!"3".Equals(((string)constituentsSE.data.Rows[i].ItemArray[1]).Substring(5, 1)))
                    {
                        constituentsSE.data.Rows.Remove(constituentsSE.data.Rows[i]);
                    }
                }
            }
            //上海所有主板股票
            if (Const.SHSE.Equals(securitiesExchange) && Const.MAIN_BLOCK.Equals(securitiesBlock))
            {
                constituentsSE.data = constituentsSHSE.data;
                for (int i = 0; i < constituentsSE.data.Rows.Count; i++)
                {
                    if ("68".Equals(((string)constituentsSE.data.Rows[i].ItemArray[1]).Substring(5, 2)))
                    {
                        constituentsSE.data.Rows.Remove(constituentsSE.data.Rows[i]);
                    }
                }
            }
            //深圳所有主板股票
            if (Const.SZSE.Equals(securitiesExchange) && Const.MAIN_BLOCK.Equals(securitiesBlock))
            {
                constituentsSE.data = constituentsSZSE.data;
                for (int i = 0; i < constituentsSE.data.Rows.Count; i++)
                {
                    if ("3".Equals(((string)constituentsSE.data.Rows[i].ItemArray[1]).Substring(5, 1)))
                    {
                        constituentsSE.data.Rows.Remove(constituentsSE.data.Rows[i]);
                    }
                }
            }
            //上海深圳所有主板股票
            if (Const.SHSZSE.Equals(securitiesExchange) && Const.MAIN_BLOCK.Equals(securitiesBlock))
            {
                constituentsSE.data = constituentsSHSE.data;
                for (int i = 0; i < constituentsSZSE.data.Rows.Count; i++)
                {
                    constituentsSE.data.Rows.Add(constituentsSZSE.data.Rows[i].ItemArray);
                }
                for (int i = 0; i < constituentsSE.data.Rows.Count; i++)
                {
                    if ("68".Equals(((string)constituentsSE.data.Rows[i].ItemArray[1]).Substring(5, 2))
                        || "3".Equals(((string)constituentsSE.data.Rows[i].ItemArray[1]).Substring(5, 1)))
                    {
                        constituentsSE.data.Rows.Remove(constituentsSE.data.Rows[i]);
                    }
                }
            }
            return constituentsSE;
        }
        //获取指数成份股信息
        public static void saveInstrumentinfos(GMData<DataTable> constituents)
        {
            //先清空表tbInstrumentinfos
            String sqlStr = "  delete from tbInstrumentinfos ";
            Common.ExeSqlCommand(sqlStr, null, CommandType.Text);

            ArrayList instrumentinfos = new ArrayList();
            for (int i = 0; i < constituents.data.Rows.Count; i++)
            {
                Instrumentinfos instrumentinfo = new Instrumentinfos();
                instrumentinfo.Symbol = (string)constituents.data.Rows[i]["symbol"];
                instrumentinfo.Weight = double.Parse(((Single)constituents.data.Rows[i]["weight"]).ToString());
                GMData<DataTable> ds = GMApi.GetInstrumentinfos(instrumentinfo.Symbol);
                if (ds.data.Rows.Count > 0)
                {
                    instrumentinfo.Sec_type = (int)ds.data.Rows[0]["sec_type"];     //1: 股票, 2: 基金, 3: 指数, 4: 期货, 5: 期权, 10: 虚拟合约
                    instrumentinfo.Exchange = (string)ds.data.Rows[0]["exchange"];     //交易所代码
                    instrumentinfo.Sec_id = (string)ds.data.Rows[0]["sec_id"];     //代码
                    instrumentinfo.Sec_name = (string)ds.data.Rows[0]["sec_name"];     //名称
                    instrumentinfo.Price_tick = (double)ds.data.Rows[0]["price_tick"];     //最小变动单位
                    instrumentinfo.Listed_date = (DateTime)ds.data.Rows[0]["listed_date"];     //上市日期
                    instrumentinfo.Delisted_date = (DateTime)ds.data.Rows[0]["delisted_date"];     //退市日期
                }
                instrumentinfos.Add(instrumentinfo);
            }
            //保存数据库
            ClassSaveToDb<Instrumentinfos>.DoSave("tbInstrumentinfos", instrumentinfos, "MultiCodeDataEventDriven.Instrumentinfos", DateTime.MinValue);
        }
        //根据选择项生成查询条件
        public static string createQueryCriteria(string securitiesExchange, string securitiesBlock, string symbol)
        {
            string queryCriteria = " where 1=1 ";
            //上海深圳所有股票
            if (!Const.SHSZSE.Equals(securitiesExchange))
            {
                queryCriteria = queryCriteria + " and exchange ='" + securitiesExchange + "' ";
            }
            //证券板块不为全选
            if (!Const.ALL_BLOCK.Equals(securitiesBlock))
            {
                //主板
                if (Const.MAIN_BLOCK.Equals(securitiesBlock))
                {
                    queryCriteria = queryCriteria + " and substring(symbol,6,2) != '68' and substring(symbol,6,2) != '30' ";
                }
                //创业板
                if (Const.CHINEXT.Equals(securitiesBlock))
                {
                    queryCriteria = queryCriteria + " and substring(symbol,6,2) = '30' ";
                }
            }
            //上海深圳所有股票
            if (!Const.SHSZSE.Equals(securitiesExchange))
            {
                queryCriteria = queryCriteria + " and exchange ='" + securitiesExchange + "' ";
            }
            //股票代码
            if (!"".Equals(symbol))
            {
                queryCriteria = queryCriteria + " and substring(symbol,6,6) like '%" + symbol + "%' ";
            }
            return queryCriteria;
        }
    }
}
