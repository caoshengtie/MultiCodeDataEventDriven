using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    //资金股票
    internal class CapitalSecurity
    {
        #region field
        private string securityCode;           //证券代码
        private string securityName;           //证券名称
        private int securityBalance;           //股票余额
        private int availableBalance;           //可用余额               
        private int freezeQuantity;    //冻结数量
        private float profitAndLoss;    //盈亏
        private float costPrice;    //成本价
        private float profitAndLossRatio;    //盈亏盈亏比例(%)
        private float marketPrice;    //市价
        private float marketValue;    //市值
        private float purchaseCost;    //买入成本
        private string marketCode;    //市场代码
        private string marketName;    //交易市场
        private string shareholderAccount;    //股东帐户
        private int currentPosition;    //当前持仓
        private int numberOfUnits;    //单位数量
        #endregion
        #region properity
        //证券代码
        public string SecurityCode
        {
            get { return this.securityCode; }
            set { this.securityCode = value; }
        }
        //证券名称
        public string SecurityName
        {
            get { return this.securityName; }
            set { this.securityName = value; }
        }
        //股票余额
        public int SecurityBalance
        {
            get { return this.securityBalance; }
            set { this.securityBalance = value; }
        }
        //可用余额 
        public int AvailableBalance
        {
            get { return this.availableBalance; }
            set { this.availableBalance = value; }
        }
        //冻结数量
        public int FreezeQuantity
        {
            get { return this.freezeQuantity; }
            set { this.freezeQuantity = value; }
        }
        //盈亏
        public float ProfitAndLoss
        {
            get { return this.profitAndLoss; }
            set { this.profitAndLoss = value; }
        }
        //成本价
        public float CostPrice
        {
            get { return this.costPrice; }
            set { this.costPrice = value; }
        }
        //盈亏比例(%)
        public float ProfitAndLossRatio
        {
            get { return this.profitAndLossRatio; }
            set { this.profitAndLossRatio = value; }
        }
        //市价
        public float MarketPrice
        {
            get { return this.marketPrice; }
            set { this.marketPrice = value; }
        }
        //市值
        public float MarketValue
        {
            get { return this.marketValue; }
            set { this.marketValue = value; }
        }
        //买入成本
        public float PurchaseCost
        {
            get { return this.purchaseCost; }
            set { this.purchaseCost = value; }
        }
        //市场代码
        public string MarketCode
        {
            get { return this.marketCode; }
            set { this.marketCode = value; }
        }
        //交易市场
        public string MarketName
        {
            get { return this.marketName; }
            set { this.marketName = value; }
        }
        //股东帐户
        public string ShareholderAccount
        {
            get { return this.shareholderAccount; }
            set { this.shareholderAccount = value; }
        }
        //当前持仓
        public int CurrentPosition
        {
            get { return this.currentPosition; }
            set { this.currentPosition = value; }
        }
        //单位数量
        public int NumberOfUnits
        {
            get { return this.numberOfUnits; }
            set { this.numberOfUnits = value; }
        }
        #endregion
    }
}
