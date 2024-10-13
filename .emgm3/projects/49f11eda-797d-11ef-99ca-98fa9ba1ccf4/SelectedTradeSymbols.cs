using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    public class SelectedTradeSymbols
    {
        #region field
        private string symbol;              //标的代码
        private string sec_name;              //证券名称

        //private string effectFlag;          //生效标志
        private float positionPercent;          //仓位比例
        //private float dayTradePercent;          //日内交易比例
        //private string dayTradeType;        //日内交易类型
        #endregion
        #region properity
        //标的代码
        public string Symbol
        {
            get { return this.symbol; }
            set { this.symbol = value; }
        }
        //证券名称
        public string Sec_name
        {
            get { return this.sec_name; }
            set { this.sec_name = value; }
        }
        ////生效标志
        //public string EffectFlag
        //{
        //    get { return this.effectFlag; }
        //    set { this.effectFlag = value; }
        //}
        //仓位比例
        public float PositionPercent
        {
            get { return this.positionPercent; }
            set { this.positionPercent = value; }
        }
        ////日内交易比例
        //public float DayTradePercent
        //{
        //    get { return this.dayTradePercent; }
        //    set { this.dayTradePercent = value; }
        //}
        ////日内交易类型
        //public string DayTradeType
        //{
        //    get { return this.dayTradeType; }
        //    set { this.dayTradeType = value; }
        //}
        #endregion
    }
}
