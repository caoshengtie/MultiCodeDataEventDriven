//////////////////////////////////////////////////////////////////////////
//多个代码数据事件驱动
//策略描述：
//策略订阅多个代码数据，触发策略

using GMSDK;
using rtconf.api;
using System;
using System.Windows.Forms;
using System.Text;
using System.Collections;

namespace MultiCodeDataEventDriven
{
    public class MyStrategy : Strategy
    {
        public MyStrategy(string token, string strategyId, StrategyMode mode) : base(token, strategyId, mode) { }
        //交易信息
        Trade trade;

        //当前时间的前一时间的索引
        private int previousTimeRuleIndex = 0;

        GMDataList<Account> account;           //查询量化测试交易账号

        //重写OnInit事件， 进行策略开发
        public override void OnInit()
        {
            System.Console.WriteLine("OnInit");

            //交易信息
            trade = new Trade();
            StrategyMode strategyMode = trade.TradeInfomation.StrategyMode;              //策略运行模式
            if (strategyMode == StrategyMode.MODE_LIVE)   //实时模式
            {
                System.Console.WriteLine("测试同花顺交易软件开始。");
                trade.checkTHS();
                System.Console.WriteLine("测试同花顺交易软件结束。");
            }
            //查询交易账号
            account = GetAccounts();

            StringBuilder symbols = new StringBuilder(1024 * 1024);
            //获取交易股票代码
            for (int i = 0; i < trade.TradeInfomation.SelectedTradeSymbolsList.Count; i++)
            {
                SelectedTradeSymbols selectedTradeSymbols = (SelectedTradeSymbols)trade.TradeInfomation.SelectedTradeSymbolsList[i];
                if (selectedTradeSymbols.Symbol != null && !"".Equals(selectedTradeSymbols.Symbol))
                {
                    symbols.Append(selectedTradeSymbols.Symbol);
                }
                if (i < trade.TradeInfomation.SelectedTradeSymbolsList.Count - 1)
                {
                    symbols.Append(",");
                }
            }
            System.Console.WriteLine("自定义交易股票为："+ symbols.ToString());

            //订阅通策医疗和中矿资源，bar频率为5分钟
            //Subscribe(symbols.ToString(), trade.TradeInfomation.TradeFrequency);


            string[] timeRule = Common.getTimeRule(trade.TradeInfomation.TradeFrequency);

            //设置定时任务
            for (int i = 1; i < timeRule.Length; i++)
            {
                Schedule("1d", timeRule[i]);
            }

            return;
        }
        //定时任务触发事件
        public override void OnSchedule(string data_rule, string timeRule)
        {
            System.Console.WriteLine(Now().ToString("yyyy-MM-dd HH:mm:ss"));

            //遍历交易自选股票
            for (int i = 0; i < trade.TradeInfomation.SelectedTradeSymbolsList.Count; i++)
            {
                SelectedTradeSymbols selectedTradeSymbols = (SelectedTradeSymbols)trade.TradeInfomation.SelectedTradeSymbolsList[i];
                ////开始时间
                ////string startTime = Common.getPreviousTimeRule(trade.TradeInfomation.TradeFrequency, trade, timeRule,true);
                //string startTime = Common.getPreviousTimeRule(trade.TradeInfomation.TradeFrequency, Now(), timeRule, true);

                ////结束时间
                ////string endTime = trade.TradeInfomation.TradingDate.data[trade.TradeInfomation.TradingDate.data.Count - 1].ToString("yyyy-MM-dd") + " " + timeRule;
                string endTime = Now().ToString("yyyy-MM-dd") + " " + timeRule;

                ////GMDataList<Tick> historyTicks = Common.getHistoryTicks(selectedTradeSymbols.Symbol, startTime, endTime, Const.ADJUST);
                GMDataList<Tick> historyTicks = Common.getHistoryTicksN(selectedTradeSymbols.Symbol, Const.NUMBER_ONE, endTime, Const.ADJUST);

                ////开始时间
                //startTime = Common.getPreviousTimeRule(trade.TradeInfomation.TradeFrequency, Now(), timeRule, false);
                ////TechnicalIndicatorsList technicalIndicatorsList = Common.getTechnicalIndicatorsListByHistoryBars(selectedTradeSymbols.Symbol, trade.TradeInfomation.TradeFrequency, startTime, endTime, Const.ADJUST);
                TechnicalIndicatorsList technicalIndicatorsList = Common.getTechnicalIndicatorsListByHistoryBarsN(selectedTradeSymbols.Symbol, trade.TradeInfomation.TradeFrequency, Const.TRADE_KLINE_NUMBER, endTime, Const.ADJUST);


                if (technicalIndicatorsList.technicalIndicatorsList.Count > 0)
                {
                    if (historyTicks.data.Count > 0) {
                        tradeBBIAndRSI(ref technicalIndicatorsList.technicalIndicatorsList, selectedTradeSymbols.Symbol, historyTicks.data[historyTicks.data.Count - 1]);
                    } else {
                        tradeBBIAndRSI(ref technicalIndicatorsList.technicalIndicatorsList, selectedTradeSymbols.Symbol, null);
                    }
                }
            }
        }
        //重写OnBar事件
        public override void OnBar(Bar bar)
        {
            //System.Console.WriteLine("OnBar：");
            //System.Console.WriteLine("{0, -50}{1}", "代码：", bar.symbol);
            //System.Console.WriteLine("{0, -50}{1}", "bar的开始时间：", bar.bob);
            //System.Console.WriteLine("{0, -50}{1}", "bar的结束时间：", bar.eob);
            //System.Console.WriteLine("{0, -50}{1}", "开盘价：", bar.open);
            //System.Console.WriteLine("{0, -50}{1}", "收盘价：", bar.close);
            //System.Console.WriteLine("{0, -50}{1}", "最高价：", bar.high);
            //System.Console.WriteLine("{0, -50}{1}", "最低价：", bar.low);
            //System.Console.WriteLine("{0, -50}{1}", "成交量：", bar.volume);
            //System.Console.WriteLine("{0, -50}{1}", "成交金额：", bar.amount);
            //System.Console.WriteLine("{0, -50}{1}", "前收盘价：", bar.preClose);
            //System.Console.WriteLine("{0, -50}{1}", "持仓量：", bar.position);
            //System.Console.WriteLine("{0, -50}{1}", "bar频度：", bar.frequency);

            System.Console.WriteLine(Now().ToString("yyyy-MM-dd HH:mm:ss"));

            //遍历交易自选股票
            for (int i = 0; i < trade.TradeInfomation.SelectedTradeSymbolsList.Count; i++)
            {
                SelectedTradeSymbols selectedTradeSymbols = (SelectedTradeSymbols)trade.TradeInfomation.SelectedTradeSymbolsList[i];

                GMDataList<Tick> historyTicks = Common.getHistoryTicksN(selectedTradeSymbols.Symbol, Const.NUMBER_ONE, Const.ADJUST);

                //获取行情
                TechnicalIndicatorsList technicalIndicatorsList = Common.getTechnicalIndicatorsListByHistoryBarsN(selectedTradeSymbols.Symbol, trade.TradeInfomation.TradeFrequency, Const.TRADE_KLINE_NUMBER, Const.ADJUST);

                if (technicalIndicatorsList.technicalIndicatorsList.Count > 0)
                {
                    if (historyTicks.data.Count > 0)
                    {
                        tradeBBIAndRSI(ref technicalIndicatorsList.technicalIndicatorsList, selectedTradeSymbols.Symbol, historyTicks.data[historyTicks.data.Count - 1]);
                    }
                    else
                    {
                        tradeBBIAndRSI(ref technicalIndicatorsList.technicalIndicatorsList, selectedTradeSymbols.Symbol, null);
                    }
                }
            }

        }
        //BBI多空均线交易
        public void tradeBBIAndRSI(ref ArrayList returnArrayList, string symbols, Tick tick)
        {
            float tradePrice = 0.0f;
            int i = returnArrayList.Count - 1;
            //if (Now().ToString("yyyy-MM-dd").Equals(((TechnicalIndicators)returnArrayList[i]).Bob.ToString("yyyy-MM-dd")))
            //{
            //如果出现买入信号
            if (((TechnicalIndicators)returnArrayList[i]).dealInfo.orderSide == OrderSide.OrderSide_Buy)
            {
                //同花顺交易                   
                trade = new Trade();
                StrategyMode strategyMode = trade.TradeInfomation.StrategyMode;              //策略运行模式
                if (strategyMode == StrategyMode.MODE_LIVE)   //实时模式
                {
                    //交易失败重试次数
                    int transactionFailureRetryTimes = trade.TradeInfomation.TransactionFailureRetryTimes;

                    for (int j = 1; j <= transactionFailureRetryTimes; j++)
                    {
                        //交易数量
                        string transactionQuantity = trade.getTransactionQuantity(symbols, tradePrice.ToString());
                        //交易价格
                        tradePrice = Common.getTradePrice(tick, Const.ORDERSIDE_BUY, trade, ((TechnicalIndicators)returnArrayList[i]).bar.close);

                        int retStatus = trade.buyF6(symbols, tradePrice.ToString(), transactionQuantity);
                        //买入全部成交
                        if (retStatus == Const.BUY_STATUS_COMPLETED_SUCCESS)
                        {
                            break;
                        }
                        else //全部撤单
                        {
                            trade.cancelOrder();
                        }
                    }
                }
                //量化交易测试账户买入
                bool retPosition = getPosition(symbols);

                //如果没有持仓则买入股票
                if (retPosition == false)
                {
                    GMData<Order> o = OrderPercent(symbols, 1, OrderSide.OrderSide_Buy, OrderType.OrderType_Limit, PositionEffect.PositionEffect_Open, ((TechnicalIndicators)returnArrayList[i]).bar.close);
                    if (o.status == 0)   //该判断仅表示函数调用无异常
                    {
                        GMDataList<ExecRpt> execRpt = GetExecutionReports(account.data[0].accountId);
                        for (int j = 0; j < execRpt.data.Count; j++)
                        {
                            ExecRpt rpt = execRpt.data[j];
                            if (rpt.createdAt == Now())
                            {
                                System.Console.WriteLine("股价上穿BBI多空均线后连续3天站在多空均线上方买入 买入日期：" + rpt.createdAt + " 买入价格：" + rpt.price + " 买入数量：" + rpt.volume + " 买入金额：" + rpt.amount + " 手续费：" + rpt.commission);
                            }
                        }
                    }
                }
            }
            //如果出现卖出信号
            if (((TechnicalIndicators)returnArrayList[i]).dealInfo.orderSide == OrderSide.OrderSide_Sell)
            {
                //同花顺交易                   
                trade = new Trade();

                StrategyMode strategyMode = trade.TradeInfomation.StrategyMode;              //策略运行模式
                if (strategyMode == StrategyMode.MODE_LIVE)   //实时模式
                {
                    //交易失败重试次数
                    int transactionFailureRetryTimes = trade.TradeInfomation.TransactionFailureRetryTimes;

                    for (int j = 1; j <= transactionFailureRetryTimes; j++)
                    {
                        //交易数量
                        string sellQuantity = trade.getSellQuantity(symbols);
                        //交易价格
                        tradePrice = Common.getTradePrice(tick, Const.ORDERSIDE_SELL, trade, ((TechnicalIndicators)returnArrayList[i]).bar.close);
                        int retStatus = trade.sellF6(symbols, tradePrice.ToString(), sellQuantity);
                        //卖出全部成交
                        if (retStatus == Const.SELL_STATUS_COMPLETED_SUCCESS)
                        {
                            break;
                        }
                        else //全部撤单
                        {
                            trade.cancelOrder();
                        }
                    }
                }
                //量化交易测试账户买入
                //GMData<Order> o = OrderVolume(symbols, int.Parse(volume.ToString()), OrderSide.OrderSide_Sell, OrderType.OrderType_Limit, PositionEffect.PositionEffect_Close, sellPrice);
                bool retPosition = getPosition(symbols);

                //如果有持仓则卖出股票
                if (retPosition == true)
                {
                    GMDataList<Order> o = OrderCloseAll();
                    if (o.status == 0)   //该判断仅表示函数调用无异常
                    {
                        GMDataList<ExecRpt> execRpt = GetExecutionReports(account.data[0].accountId);
                        for (int j = 0; j < execRpt.data.Count; j++)
                        {
                            ExecRpt rpt = execRpt.data[j];
                            if (rpt.createdAt == Now())
                            {
                                System.Console.WriteLine("股价下穿BBI多空均线后连续3天站在多空均线下方卖出 卖出日期：" + rpt.createdAt + " 卖出价格：" + rpt.price + " 卖出数量：" + rpt.volume + " 卖出金额：" + rpt.amount + " 手续费：" + rpt.commission);
                                //System.Console.WriteLine(" 买入日期：" + rpt.createdAt + " 买入价格：" + rpt.price + " 买入数量：" + rpt.volume + " 买入金额：" + rpt.amount + " 手续费：" + rpt.commission);
                            }
                        }
                    }
                }
            }
            //}
        }
        private bool getPosition(string symbols)
        {
            bool retPosition = false;
            //查询持仓
            GMDataList<Position> position = GetPosition(account.data[0].accountId);
            for (int j = 0; j<position.data.Count; j++)
            {
                Position position1 = (Position)position.data[j];
                if (symbols.Equals(position1.symbol))
                {
                    retPosition = true;
                }
            }
            return retPosition;
        }
    }
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());

            //StrategyMode strategyMode = StrategyMode.MODE_LIVE;              //策略运行模式
            ////策略运行模式
            ////实时模式
            //if (Const.MODE_LIVE.Equals(Common.getParameterValue(Const.STRATEGY_MODE)))
            //{
            //    //实时模式
            //    strategyMode = StrategyMode.MODE_LIVE;
            //}
            ////回测模式
            //if (Const.MODE_BACKTEST.Equals(Common.getParameterValue(Const.STRATEGY_MODE)))
            //{
            //    //回测模式
            //    strategyMode = StrategyMode.MODE_BACKTEST;
            //}
            //MyStrategy s = new MyStrategy("4147773824d7ab8c0b50869894327206e5bd8b30", "49f11eda-797d-11ef-99ca-98fa9ba1ccf4", strategyMode);
            //s.SetBacktestConfig("2024-06-25 09:30:00", "2024-09-30 15:00:00");
            //s.Run();
            //System.Console.WriteLine("回测完成！");
            //System.Console.Read();

        }
    }
}