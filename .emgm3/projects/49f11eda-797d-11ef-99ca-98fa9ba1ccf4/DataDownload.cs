using GMSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiCodeDataEventDriven
{
    internal class DataDownload
    {
        String tradeDate = "";
        String filePath = "";
        int fileNumber = 0;         //需要解析的文件数量
        int fileCount = 0;         //解析的文件数量
        //概念与股票对应关系写入板块股票映射表tbBlockStockcodeMap
        private void eastMoney(String dataPath, String tradeDate, String blockOrStock, String blockName)
        {

            SqlDataAdapter daTbBlockStockcodeMap = new SqlDataAdapter();
            DataSet mdTbDictEntry = new DataSet();
            DataSet mdTbBlockStockcodeMap = new DataSet();
            String tableName = "";

            if ("block".Equals(blockOrStock))
            {
                tableName = "tbEastMoneyBlock";
            }
            if ("stock".Equals(blockOrStock))
            {
                tableName = "tbEastMoneyBlockStock";
            }

            //数据库存取用到的变量
            daTbBlockStockcodeMap = new SqlDataAdapter("Select * From " + tableName, Const.DB_CONNECT);
            daTbBlockStockcodeMap.Fill(mdTbBlockStockcodeMap, tableName);


            LoadEastMoney(dataPath, tradeDate, ref mdTbBlockStockcodeMap, blockOrStock, blockName);

            try
            {

                SqlCommandBuilder cb = new SqlCommandBuilder();
                cb = new SqlCommandBuilder(daTbBlockStockcodeMap);

                DataTable dt = mdTbBlockStockcodeMap.Tables[tableName].GetChanges();
                if (dt != null)
                {
                    daTbBlockStockcodeMap.Update(dt);
                    mdTbBlockStockcodeMap.AcceptChanges();
                    dt.Dispose();
                }
                cb.Dispose();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
        //东方财富所有板块数据读取
        private void LoadEastMoney(String fnStr, String tradeDate, ref DataSet mdTbBlockStockcodeMap, String blockOrStock, String blockName)
        {
            String line = "";
            StreamReader sr = new StreamReader(fnStr, System.Text.Encoding.Default);
            long lineNo = 0;

            sr = new StreamReader(fnStr, System.Text.Encoding.Default);
            while (sr.Peek() > 0)
            {
                line = sr.ReadLine();
                lineNo = lineNo + 1;
                //如果行内容包含字符"	"
                if ((line.IndexOf("	") > 0) && lineNo > 2)
                {
                    line = line.Replace("		", "	");
                    line = line.Replace("			", "	");

                    String[] array = line.ToString().Split('	');
                    Decimal fund = 0;


                    if (array[2].IndexOf("亿") > 0)
                    {
                        fund = decimal.Parse(array[2].Replace("亿", ""));
                    }
                    if (array[2].IndexOf("万") > 0)
                    {
                        fund = decimal.Parse(array[2].Replace("万", ""));
                        fund = fund / 10000;
                    }
                    if (array[2].IndexOf("----") > 0)
                    {
                        fund = 0;
                    }

                    //数据存入内存
                    if ("block".Equals(blockOrStock))
                    {
                        mdTbBlockStockcodeMap.Tables["tbEastMoneyBlock"].Rows.Add(tradeDate, array[0], array[1], fund);
                    }
                    if ("stock".Equals(blockOrStock))
                    {
                        mdTbBlockStockcodeMap.Tables["tbEastMoneyBlockStock"].Rows.Add(tradeDate, blockName, array[0], array[1], fund);
                    }
                }
                //如果行内容包含字符" "
                if ((line.IndexOf(" ") > 0) && lineNo > 1)
                {
                    line = line.Trim();
                    line = line.Replace("          ", " ");
                    line = line.Replace("         ", " ");
                    line = line.Replace("        ", " ");
                    line = line.Replace("       ", " ");
                    line = line.Replace("      ", " ");
                    line = line.Replace("     ", " ");
                    line = line.Replace("    ", " ");
                    line = line.Replace("   ", " ");
                    line = line.Replace("  ", " ");

                    String[] array = line.ToString().Split(' ');
                    if (line.IndexOf("\t") > 0)
                    {
                        array = line.ToString().Split('\t');
                    }

                    //000002    万  科Ａ    2.80亿
                    if (array.Length == 4)
                    {
                        array[1] = array[1] + array[2];
                        array[2] = array[3];
                    }
                    //000735    罗 牛 山    55.3万
                    if (array.Length == 5)
                    {
                        array[1] = array[1] + array[2] + array[3];
                        array[2] = array[4];
                    }
                    if (array.Length == 6)
                    {
                        array[1] = array[1] + array[2] + array[3] + array[4];
                        array[2] = array[5];
                    }

                    Decimal fund = 0;


                    if (array[2].IndexOf("亿") > 0)
                    {
                        fund = decimal.Parse(array[2].Replace("亿", ""));
                    }
                    if (array[2].IndexOf("万") > 0)
                    {
                        fund = decimal.Parse(array[2].Replace("万", ""));
                        fund = fund / 10000;
                    }
                    if (array[2].IndexOf("----") > 0)
                    {
                        fund = 0;
                    }

                    //数据存入内存
                    if ("block".Equals(blockOrStock))
                    {
                        mdTbBlockStockcodeMap.Tables["tbEastMoneyBlock"].Rows.Add(tradeDate, array[0], array[1], fund);
                    }
                    if ("stock".Equals(blockOrStock))
                    {
                        mdTbBlockStockcodeMap.Tables["tbEastMoneyBlockStock"].Rows.Add(tradeDate, blockName, array[0], array[1], fund);
                    }
                }
            }
            sr.Close();
            sr = null;
        }

        public void GetEastMoneyAllFiles(String strDirect, String blockOrStock, bool startDownloadFlag)
        {
            if (strDirect != null && !"".Equals(strDirect))
            {
                //遍历东方财富盘后下载数据文件夹下所有文件。
                System.IO.DirectoryInfo mDirInfo = new System.IO.DirectoryInfo(strDirect);

                try
                {
                    //日线
                    foreach (System.IO.FileInfo mFileInfo in mDirInfo.GetFiles("*.txt"))
                    {
                        if (startDownloadFlag == true)
                        {
                            if ("block".Equals(blockOrStock))
                            {
                                //读取东方财富板块数据
                                eastMoney(mFileInfo.FullName, tradeDate, "block", "");
                            }
                            if ("stock".Equals(blockOrStock))
                            {
                                //读取东方财富板块数据
                                String blockName = mFileInfo.FullName.Replace(".txt", "").Replace(filePath + "\\", "");
                                eastMoney(mFileInfo.FullName, tradeDate, "stock", blockName);
                            }
                            fileCount++;
                            int progress = (int)Math.Round(Double.Parse(fileCount.ToString()) / Double.Parse(fileNumber.ToString()) * 100, 0);
                        }
                        else
                        {
                            fileNumber = fileNumber + 1;
                        }


                    }
                    foreach (System.IO.DirectoryInfo mDir in mDirInfo.GetDirectories())
                    {
                        if (startDownloadFlag == true)
                        {
                            filePath = mDir.FullName;
                            tradeDate = mDir.FullName.Substring(mDir.FullName.Length - 8, 8);
                            //删除数据库中交易日期等于当前日期的记录
                            if ("block".Equals(blockOrStock))
                            {
                                Common.ExeSqlCommand("delete From tbEastMoneyBlock where  tradeDate =" + tradeDate, null, CommandType.Text);
                            }
                            if ("stock".Equals(blockOrStock))
                            {
                                Common.ExeSqlCommand("delete From tbEastMoneyBlockStock where tradeDate =" + tradeDate, null, CommandType.Text);
                            }
                        }
                        GetEastMoneyAllFiles(mDir.FullName, blockOrStock, startDownloadFlag);
                    }
                }
                catch (System.IO.DirectoryNotFoundException ex)
                {
                    System.Console.WriteLine("目录没找到：" + ex.Message);
                }
            }
        }
        //封板资金
        public void sealingFund(DateTime now)
        {
            //先删除数据库
            string sqlStr = " delete from tbLimitUpBuyAmount1Rate  " +
                            " where   convert(date, limitUpDateTime, 20) = convert(date,convert(varchar,'" + now.ToString("yyyy-MM-dd") + "'), 20)";
            Common.ExeSqlCommand(sqlStr, null, CommandType.Text);

            GMData<DataTable> dsSHSE = GMApi.GetConstituents(Const.INDEX_SHSE_000001, DateTime.Now.ToString("yyyy-MM-dd"));
            System.Console.WriteLine("上证开始");
            sealingFundErgodic(dsSHSE, Const.EXCH_CODE_SHSE, now);
            System.Console.WriteLine("上证结束");
            GMData<DataTable> dsSZSE = GMApi.GetConstituents(Const.INDEX_SZSE_399106, DateTime.Now.ToString("yyyy-MM-dd"));
            System.Console.WriteLine("深证开始");
            sealingFundErgodic(dsSZSE, Const.EXCH_CODE_SZSE, now);
            System.Console.WriteLine("深证结束");
            MessageBox.Show("下载完成。");
        }
        //封板资金
        public static void sealingFundErgodic(GMData<DataTable> ds, string exchCode, DateTime now)
        {
            ArrayList sealingFunds = new ArrayList(); //封板资金数组

            for (int i = 0; i < ds.data.Rows.Count; i++)
            {
                string symb = (string)ds.data.Rows[i].ItemArray[1];
                bool cond = false;
                //如果是上证
                if (Const.EXCH_CODE_SHSE.Equals(exchCode))
                {
                    //如果股票代码以6开始
                    if ("6".Equals(symb.Substring(5, 1)))
                    {
                        cond = true;
                    }

                }
                //如果是深证
                if (Const.EXCH_CODE_SZSE.Equals(exchCode))
                {
                    //如果股票代码以0或者3开始
                    if ("0".Equals(symb.Substring(5, 1)) || "3".Equals(symb.Substring(5, 1)))
                    {
                        cond = true;
                    }

                }
                if (cond)
                {
                    //获取从N天内股票日线
                    GMDataList<Bar> historyBar = GMApi.HistoryBarsN(symb, Const.FREQUENCY_DAY, Const.History_Day, now.ToString("yyyy-MM-dd") + " " + Const.Time_Rule_160000, Adjust.ADJUST_NONE);
                    // GMDataList<Bar> historyBar = GMApi.HistoryBars(symb, Const.FREQ_DAY, tbTradingDates.data[tbTradingDates.data.Count - 1].AddMonths(-1).ToString("yyyy-MM-dd") + " " + TimeRule.Time_Rule_Frnn_02, tbTradingDates.data[tbTradingDates.data.Count - 1].ToString("yyyy-MM-dd") + " " + TimeRule.Time_Rule_Aftrn_02, Adjust.ADJUST_NONE);

                    //Bar todayBar = Common.createTodayBarAdjustNone(symb, tbTradingDates.data[tbTradingDates.data.Count - 1].AddDays(-1));
                    ////收盘价
                    //todayBar.preClose = historyBar.data[historyBar.data.Count - 1].close;
                    ////bar频度
                    //todayBar.frequency = historyBar.data[historyBar.data.Count - 1].frequency;

                    //historyBar.data.Add(todayBar);
                    if (historyBar.data.Count > 0)
                    {
                        QuoteInfoList symbListAdjustNone = new QuoteInfoList(historyBar); //存储不复权的日线数据，主要用于判断涨跌停
                        if (((TechnicalIndicators)symbListAdjustNone.quoteInfos[symbListAdjustNone.quoteInfos.Count - 1]).riseAndFallRang.limitUpDownIdent == 1)
                        {
                            System.Console.WriteLine("涨停" + symb + "    日期" + ((TechnicalIndicators)symbListAdjustNone.quoteInfos[symbListAdjustNone.quoteInfos.Count - 1]).bar.bob);
                            SealingFund sealingFund = createSealingFund(symb, now, historyBar, symbListAdjustNone);
                            //当天停牌股票不计入
                            if (sealingFund != null && ((TechnicalIndicators)symbListAdjustNone.quoteInfos[symbListAdjustNone.quoteInfos.Count - 1]).bar.bob.ToString("yyyy-MM-dd").Equals(now.ToString("yyyy-MM-dd")))
                            {
                                sealingFunds.Add(sealingFund);
                            }
                        }
                    }
                }
            }
            //保存数据库
            ClassSaveToDb<SealingFund>.DoSave("tbLimitUpBuyAmount1Rate", sealingFunds, "MultiCodeDataEventDriven.SealingFund", DateTime.MinValue);

        }
        //生成当天的日K线数据
        public static SealingFund createSealingFund(string symb, DateTime nowDate, GMDataList<Bar> historyBar, QuoteInfoList symbListAdjustNone)
        {
            SealingFund sealingFund = new SealingFund();
            sealingFund.Symbol = symb;      //证券交易所加股票代码

            if (decimal.Parse(historyBar.data[historyBar.data.Count - 1].low.ToString()) == decimal.Parse(historyBar.data[historyBar.data.Count - 1].close.ToString()))
            {
                sealingFund.LimitUpType = "一字板";
            }
            else
            {
                sealingFund.LimitUpType = "非一字板";
            }
            double makeDealQuantityRate = 0;
            //昨日成交量
            double preVolume = historyBar.data[historyBar.data.Count - 2].volume;
            //今日日成交量
            double volume = historyBar.data[historyBar.data.Count - 1].volume;

            if (decimal.Parse((preVolume.ToString())) != 0)
            {
                makeDealQuantityRate = Math.Round(volume / preVolume, 2);
            }
            sealingFund.MakeDealQuantityRate = float.Parse(makeDealQuantityRate.ToString());

            // GMDataList<Tick> historyTick = GMApi.HistoryTicksN(symb, 1);
            GMDataList<Tick> historyTick = GMApi.HistoryTicks(symb, nowDate.ToString("yyyy-MM-dd") + " " + Const.Time_Rule_145900, nowDate.ToString("yyyy-MM-dd") + " " + Const.Time_Rule_150000);
            if (historyTick.data.Count == 0)
            {
                return null;
            }
            //成交金额
            double amount = historyBar.data[historyBar.data.Count - 1].amount;
            //封单金额
            double amountSealingFund = historyTick.data[historyTick.data.Count - 1].quotes[0].bidPrice * (historyTick.data[historyTick.data.Count - 1].quotes[0].bidVolume + historyTick.data[historyTick.data.Count - 1].quotes[1].bidVolume);

            double buyAmount1Rate = 0;

            if (decimal.Parse((amount.ToString())) != 0)
            {
                buyAmount1Rate = Math.Round(amountSealingFund / amount, 2);
            }
            sealingFund.BuyAmount1Rate = float.Parse(buyAmount1Rate.ToString());

            int limitUpNum = 0;  //涨停板数量
            for (int i = symbListAdjustNone.quoteInfos.Count - 1; i >= 0; i--)
            {
                if (((TechnicalIndicators)symbListAdjustNone.quoteInfos[i]).riseAndFallRang.limitUpDownIdent == 1)
                {
                    limitUpNum++;
                }
                else
                {
                    break;
                }
            }
            sealingFund.LimitUpNum = limitUpNum;

            //获取今天每分钟的K线数据
            GMDataList<Bar> historyBarMin = GMSDK.GMApi.HistoryBars(symb, Const.FREQUENCY_MINUT, nowDate.ToString("yyyy-MM-dd") + " 09:30:00", nowDate.ToString("yyyy-MM-dd") + " 15:00:00");
            if (historyBarMin.data.Count == 0)
            {
                return null;
            }
            for (int i = 0; i < historyBarMin.data.Count; i++)
            {
                //涨停时间
                if (decimal.Parse((historyBarMin.data[i].open.ToString())) == decimal.Parse((historyBarMin.data[historyBarMin.data.Count - 1].close.ToString())))
                {
                    sealingFund.LimitUpDateTime = historyBarMin.data[i].bob;
                    break;
                }
            }

            return sealingFund;
        }
        //读取东方财富数据
        public void GetEastMoneyData()
        {
            DataDownload dataDownload = new DataDownload();

            //读取东方财富所有板块数据。
            dataDownload.GetEastMoneyAllFiles(Const.EAST_MONEY_DATA_PATH_ALL, "block", true);
            //读取东方财富所有板块数据。
            dataDownload.GetEastMoneyAllFiles(Const.EAST_MONEY_DATA_PATH_STOCK, "stock", true);

            //建立东方财富概念和股票关联关系
            String sqlStr = " MERGE INTO tbEastMoneyBlockStockMap target_tab "
                    + " USING (select b.blockName as blockName_s,b.stockCode as stockCode_s,MAX(b.stockName) as stockName_s from tbEastMoneyBlockStock b "
                    + " where b.blockName not in ('昨日连板','昨日涨停','昨日触板') "
                    + " group by b.blockName,b.stockCode) source ON (blockName=blockName_s and stockCode=stockCode_s) "
                    + " WHEN MATCHED THEN "
                    + " UPDATE SET stockName = stockName_s "
                    + " WHEN NOT MATCHED THEN "
                    + " INSERT(blockName,stockCode,stockName) VALUES (blockName_s,stockCode_s,stockName_s); ";
            Common.ExeSqlCommand(sqlStr, null, CommandType.Text);
            //建立板块信息
            sqlStr = " insert into tbEastMoneyBlockInf(blockName,createDate) "
                + " select t.blockName,min(t.tradeDate) from tbEastMoneyBlock t "
                + " where t.blockName not in(select t1.blockName from tbEastMoneyBlockInf t1) "
                + " group by t.blockName ";
            Common.ExeSqlCommand(sqlStr, null, CommandType.Text);

            MessageBox.Show("下载完成。");
        }
    }
    public static class ClassSaveToDb<T>
    {
        public static void DoSave(string tableName, ArrayList arrayList, string className, DateTime maxBob)
        {
            DataSet MainDs = new DataSet();

            SqlDataAdapter daMain = new SqlDataAdapter("Select  top 0  * From " + tableName, Const.DB_CONNECT);
            daMain.Fill(MainDs, tableName);
            //将数据存入数组中
            //准备存入数据库的内存中
            foreach (T classInstance in arrayList)
            {
                if ("MultiCodeDataEventDriven.TechnicalIndicators".Equals(className))
                {
                    //已经下载的数据就不需要重复下载。
                    TechnicalIndicators technicalIndicators = classInstance as TechnicalIndicators;
                    if (technicalIndicators.Bob <= maxBob)
                    {
                        continue;
                    }
                }
                int index = 0;  //数组下标

                //创建查询返回结果数据内容的实例
                // T queryDataResultContent = (T)Activator.CreateInstance(Type.GetType(className));    //查询返回结果数据内容
                Type queryDataContentType = Type.GetType(className);
                //获取所有方法
                System.Reflection.MethodInfo[] methods = queryDataContentType.GetMethods();
                //获取所有属性
                System.Reflection.PropertyInfo[] properties = queryDataContentType.GetProperties();
                //遍历类的所有属性
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    //遍历类的所有方法
                    foreach (System.Reflection.MethodInfo method in methods)
                    {
                        //方法名是设置属性值的方法时，用反射的方式调用设置属性值的方法
                        if (("get_" + property.Name).Equals(method.Name))
                        {
                            index++;
                            continue;
                        }
                    }
                }
                Object[] objectContent = new Object[index];
                index = 0;
                //遍历类的所有属性
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    //遍历类的所有方法
                    foreach (System.Reflection.MethodInfo method in methods)
                    {
                        //方法名是设置属性值的方法时，用反射的方式调用设置属性值的方法
                        if (("get_" + property.Name).Equals(method.Name))
                        {

                            //用反射的方式调用设置属性值的方法时的参数
                            object[] paras = { };
                            //用反射的方式调用获得属性值的方法
                            objectContent[index] = method.Invoke(classInstance, paras);
                            index++;
                            continue;
                        }
                    }
                }
                MainDs.Tables[tableName].Rows.Add(objectContent);
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(daMain);
            DataTable dt = MainDs.Tables[tableName].GetChanges();
            //保存到数据库
            if (dt != null)
            {
                //try
                //{
                daMain.Update(dt);
                //}
                //catch (Exception ex)
                //{
                //    System.Windows.Forms.MessageBox.Show("添加失败！股票代码为：" + securityCode + "错误信息：" + ex.Message);
                //}
                dt.Dispose();
            }
            MainDs.AcceptChanges();
            cb.Dispose();
        }
        public static void DoSaveByInsert(string tableName, ArrayList arrayList, string className)
        {
            string fieldStr = "";
            DataSet MainDs = new DataSet();

            SqlDataAdapter daMain = new SqlDataAdapter("Select  top 0  * From " + tableName, Const.DB_CONNECT);
            daMain.Fill(MainDs, tableName);
            //将数据存入数组中
            //准备存入数据库的内存中
            foreach (T classInstance in arrayList)
            {
                int index = 0;  //数组下标

                //创建查询返回结果数据内容的实例
                // T queryDataResultContent = (T)Activator.CreateInstance(Type.GetType(className));    //查询返回结果数据内容
                Type queryDataContentType = Type.GetType(className);
                //获取所有方法
                System.Reflection.MethodInfo[] methods = queryDataContentType.GetMethods();
                //获取所有属性
                System.Reflection.PropertyInfo[] properties = queryDataContentType.GetProperties();
                //遍历类的所有属性
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    //遍历类的所有方法
                    foreach (System.Reflection.MethodInfo method in methods)
                    {
                        //方法名是设置属性值的方法时，用反射的方式调用设置属性值的方法
                        if (("get_" + property.Name).Equals(method.Name))
                        {
                            index++;
                            fieldStr = fieldStr + property.Name + ",";
                            continue;
                        }
                    }
                }
                Object[] objectContent = new Object[index];
                index = 0;
                //遍历类的所有属性
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    //遍历类的所有方法
                    foreach (System.Reflection.MethodInfo method in methods)
                    {
                        //方法名是设置属性值的方法时，用反射的方式调用设置属性值的方法
                        if (("get_" + property.Name).Equals(method.Name))
                        {

                            //用反射的方式调用设置属性值的方法时的参数
                            object[] paras = { };
                            //用反射的方式调用获得属性值的方法
                            objectContent[index] = method.Invoke(classInstance, paras);
                            index++;
                            continue;
                        }
                    }
                }
            }

            fieldStr = fieldStr.Substring(0, fieldStr.Length - 1);

            string sqlStr = " insert into " + tableName + "(" + fieldStr + ") ";
            Common.ExeSqlCommand(sqlStr, null, CommandType.Text);
        }
    }
}
