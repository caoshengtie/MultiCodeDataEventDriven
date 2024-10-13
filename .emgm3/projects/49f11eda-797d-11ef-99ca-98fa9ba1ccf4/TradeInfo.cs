using GMSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static core.api.AccountChannel;

namespace MultiCodeDataEventDriven
{
    public class TradeInfo
    {
        #region field
        private ArrayList selectedTradeSymbolsList;              //自选交易标的集合
        private GMDataList<DateTime> tradingDate;                //股票市场交易日
        private string tradeFrequency;              //交易频率
        private StrategyMode strategyMode;              //策略运行模式
        private string token;                                   //标记
        private string strategyId;                              //策略ID
        private string bidPrice;                                   //委买价
        private string askPrice;                                   //委卖价
        private int transactionFailureRetryTimes;                                   //交易失败重试次数
        #endregion
        #region properity
        //标的代码
        public ArrayList SelectedTradeSymbolsList
        {
            get { return this.selectedTradeSymbolsList; }
            set { this.selectedTradeSymbolsList = value; }
        }
        //股票市场交易日
        public GMDataList<DateTime> TradingDate
        {
            get { return this.tradingDate; }
            set { this.tradingDate = value; }
        }
        //交易频率
        public string TradeFrequency
        {
            get { return this.tradeFrequency; }
            set { this.tradeFrequency = value; }
        }
        //策略运行模式
        public StrategyMode StrategyMode
        {
            get { return this.strategyMode; }
            set { this.strategyMode = value; }
        }
        //标志
        public string Token
        {
            get { return this.token; }
            set { this.token = value; }
        }
        //策略ID
        public string StrategyId
        {
            get { return this.strategyId; }
            set { this.strategyId = value; }
        }
        //委买价
        public string BidPrice
        {
            get { return this.bidPrice; }
            set { this.bidPrice = value; }
        }
        //委卖价
        public string AskPrice
        {
            get { return this.askPrice; }
            set { this.askPrice = value; }
        }
        //交易失败重试次数
        public int TransactionFailureRetryTimes
        {
            get { return this.transactionFailureRetryTimes; }
            set { this.transactionFailureRetryTimes = value; }
        }
        #endregion
        public TradeInfo()
        {
            this.selectedTradeSymbolsList = getSelectedTradeSymbols();
            //股票市场交易日
            this.tradingDate = GMApi.GetTradingDates(Const.EXCH_CODE_SHSE, Const.SYS_MARKET_START_TRADING_DATE, DateTime.Now.ToString("yyyy-MM-dd"));
            //交易频率
            this.tradeFrequency = Common.getParameterValue(Const.TRADE_FREQUENCY);

            //委买价
            this.bidPrice = Common.getParameterValue(Const.BID_PRICE);
            //委卖价
            this.askPrice = Common.getParameterValue(Const.ASK_PRICE);
            //交易失败重试次数
            this.transactionFailureRetryTimes = int.Parse(Common.getParameterValue(Const.TRANSACTION_FAILURE_RETRY_TIMES));
            //策略运行模式
            //实时模式
            if (Const.MODE_LIVE.Equals(Common.getParameterValue(Const.STRATEGY_MODE)))
            {
                //实时模式
                this.strategyMode = StrategyMode.MODE_LIVE;
            }
            //回测模式
            if (Const.MODE_BACKTEST.Equals(Common.getParameterValue(Const.STRATEGY_MODE)))
            {
                //回测模式
                this.strategyMode = StrategyMode.MODE_BACKTEST;
            }
        }

        public ArrayList getSelectedTradeSymbols()
        {
            ArrayList selectedTradeSymbols = new ArrayList();
            //查询交易自选股表
            string sql = " select t.symbol,t.sec_name,t.positionPercent from tbSelectedTradeSymbols t  order by t.symbol ";
            DataTable dataTable = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                SelectedTradeSymbols selectedTradeSymbol = new SelectedTradeSymbols();
                //标的代码
                selectedTradeSymbol.Symbol = (string)dataTable.Rows[i].ItemArray[0];
                //证券名称
                selectedTradeSymbol.Sec_name = (string)dataTable.Rows[i].ItemArray[1];
                //仓位比例
                selectedTradeSymbol.PositionPercent = Common.decimalToFloat((decimal)dataTable.Rows[2].ItemArray[1]);
                selectedTradeSymbols.Add(selectedTradeSymbol);
            }

            return selectedTradeSymbols;
        }
        //通过symbol(标的代码)查询持仓、做T用的仓位等信息
        public SelectedTradeSymbols getSelectedSymbols(string symbol)
        {
            SelectedTradeSymbols selectedTradeSymbol = new SelectedTradeSymbols();
            for (int i = 0; i < this.selectedTradeSymbolsList.Count; i++)
            {
                if (symbol.Equals(((SelectedTradeSymbols)this.selectedTradeSymbolsList[i]).Symbol))
                {
                    selectedTradeSymbol = (SelectedTradeSymbols)this.selectedTradeSymbolsList[i];
                    break;
                }
            }

            return selectedTradeSymbol;
        }
    }
}
