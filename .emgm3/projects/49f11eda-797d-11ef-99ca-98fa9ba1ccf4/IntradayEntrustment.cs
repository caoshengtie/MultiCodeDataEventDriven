using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    //当日委托
    internal class IntradayEntrustment
    {
        #region field
        private string entrustmentTime;           //委托时间
        private string entrustmentDate;           //委托日期
        private string securityCode;           //证券代码
        private string securityName;           //证券名称
        private string operation;           //操作
        private string remarks;             //	备注
        private int entrustmentQuantity;           //委托数量
        private int transactionQuantity;           //成交数量
        private int cancelOrderNumbe;    //撤单数量
        private float entrustmentPrice;    //委托价格
        private float transactionAveragePrice;    //成交均价
        private string contractNumbe;    //合同编号
        private string marketName;    //交易市场
        private string shareholderAccount;    //股东帐户
        private string declarationNumbe;    // 申报编号
        private string entrustmentAttribute;    //委托属性
        private string entrustmentNumbe;    //委托编号

        #endregion
        #region properity
        //委托日期
        public string EntrustmentDate
        {
            get { return this.entrustmentDate; }
            set { this.entrustmentDate = value; }
        }

        //委托时间
        public string EntrustmentTime
        {
            get { return this.entrustmentTime; }
            set { this.entrustmentTime = value; }
        }
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
        //操作
        public string Operation
        {
            get { return this.operation; }
            set { this.operation = value; }
        }
        //备注
        public string Remarks
        {
            get { return this.remarks; }
            set { this.remarks = value; }
        }
        //委托数量
        public int EntrustmentQuantity
        {
            get { return this.entrustmentQuantity; }
            set { this.entrustmentQuantity = value; }
        }
        //成交数量
        public int TransactionQuantity
        {
            get { return this.transactionQuantity; }
            set { this.transactionQuantity = value; }
        }
        //撤单数量
        public int CancelOrderNumbe
        {
            get { return this.cancelOrderNumbe; }
            set { this.cancelOrderNumbe = value; }
        }
        //委托价格
        public float EntrustmentPrice
        {
            get { return this.entrustmentPrice; }
            set { this.entrustmentPrice = value; }
        }
        //成交均价
        public float TransactionAveragePrice
        {
            get { return this.transactionAveragePrice; }
            set { this.transactionAveragePrice = value; }
        }
        //合同编号
        public string ContractNumbe
        {
            get { return this.contractNumbe; }
            set { this.contractNumbe = value; }
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
        //委托编号
        public string EntrustmentNumbe
        {
            get { return this.entrustmentNumbe; }
            set { this.entrustmentNumbe = value; }
        }
        //申报编号
        public string DeclarationNumbe
        {
            get { return this.declarationNumbe; }
            set { this.declarationNumbe = value; }
        }
        //委托属性
        public string EntrustmentAttribute
        {
            get { return this.entrustmentAttribute; }
            set { this.entrustmentAttribute = value; }
        }
        #endregion
    }
}
