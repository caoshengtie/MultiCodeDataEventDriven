using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    //封板资金
    internal class SealingFund
    {
        #region field
        private string symbol;                          //证券交易所加股票代码
        private DateTime limitUpDateTime;               //涨停时间
        private double buyAmount1Rate;                   //封板资金占交易金额比例
        private int limitUpNum;                         //涨停板数量
        private string limitUpType;                     //涨停板类型
        private double makeDealQuantityRate;             //与昨天的成交量比值
        #endregion
        #region properity
        //证券交易所加股票代码
        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }
        //涨停时间
        public DateTime LimitUpDateTime
        {
            get { return limitUpDateTime; }
            set { limitUpDateTime = value; }
        }
        //封板资金占交易金额比例
        public double BuyAmount1Rate
        {
            get { return buyAmount1Rate; }
            set { buyAmount1Rate = value; }
        }
        //涨停板数量
        public int LimitUpNum
        {
            get { return limitUpNum; }
            set { limitUpNum = value; }
        }
        //涨停板类型
        public string LimitUpType
        {
            get { return limitUpType; }
            set { limitUpType = value; }
        }
        //与昨天的成交量比值
        public double MakeDealQuantityRate
        {
            get { return makeDealQuantityRate; }
            set { makeDealQuantityRate = value; }
        }
        #endregion

    }
}
