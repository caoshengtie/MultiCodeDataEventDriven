using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    //当日成交
    internal class IntradayTransaction
    {
        #region field
        private string transactionTime;           //成交时间
        private string securityCode;           //证券代码
        private string securityName;           //证券名称
        private string operation;           //操作
        private int transactionQuantity;           //成交数量
        private float transactionAveragePrice;    //成交均价
        private float transactionAmount;    //成交金额
        private string contractNumbe;    //合同编号
        private string transactionNumbe;    //成交编号
        private int cancelOrderNumbe;    //撤单数量
        private string declarationNumbe;    // 申报编号
        private string entrustmentAttribute;    //委托属性
        #endregion
        #region properity
        //成交时间
        public string TransactionTime
        {
            get { return this.transactionTime; }
            set { this.transactionTime = value; }
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
        //成交数量
        public int TransactionQuantity
        {
            get { return this.transactionQuantity; }
            set { this.transactionQuantity = value; }
        }
        //成交均价
        public float TransactionAveragePrice
        {
            get { return this.transactionAveragePrice; }
            set { this.transactionAveragePrice = value; }
        }
        //成交金额
        public float TransactionAmount
        {
            get { return this.transactionAmount; }
            set { this.transactionAmount = value; }
        }
        //合同编号
        public string ContractNumbe
        {
            get { return this.contractNumbe; }
            set { this.contractNumbe = value; }
        }
        //成交编号
        public string TransactionNumbe
        {
            get { return this.transactionNumbe; }
            set { this.transactionNumbe = value; }
        }
        //撤单数量
        public int CancelOrderNumbe
        {
            get { return this.cancelOrderNumbe; }
            set { this.cancelOrderNumbe = value; }
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
