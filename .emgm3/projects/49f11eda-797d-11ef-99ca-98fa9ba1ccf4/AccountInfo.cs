using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    internal class AccountInfo
    {
        #region field
        //同花顺账户信息
        private float availableAmount;       //可用金额
        private float freezeAmount;          //冻结金额
        private float marketValue;          //股票市值
        private float totalAssets;          //总 资 产
        private string shareholderAccount;  //股东帐户
        #endregion
        #region properity
        //可用金额
        public float AvailableAmount
        {
            get { return this.availableAmount; }
            set { this.availableAmount = value; }
        }
        //冻结金额
        public float FreezeAmount
        {
            get { return this.freezeAmount; }
            set { this.freezeAmount = value; }
        }
        //股票市值
        public float MarketValue
        {
            get { return this.marketValue; }
            set { this.marketValue = value; }
        }
        //总 资 产 
        public float TotalAssets
        {
            get { return this.totalAssets; }
            set { this.totalAssets = value; }
        }
        //股东帐户
        public string ShareholderAccount
        {
            get { return this.shareholderAccount; }
            set { this.shareholderAccount = value; }
        }
        #endregion
    }
}
