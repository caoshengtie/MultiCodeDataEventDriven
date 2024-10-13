using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    public class Instrumentinfos
    {
        #region field
        private string symbol;              //	标的代码
        private int sec_type;               //	1: 股票, 2: 基金, 3: 指数, 4: 期货, 5: 期权, 10: 虚拟合约
        private string exchange;            //	见交易所代码
        private string sec_id;              //	代码
        private string sec_name;            //	名称
        private double price_tick;          //	最小变动单位
        private DateTime listed_date;       //	上市日期
        private DateTime delisted_date;     //	退市日期
        private double weight;              //	权重
        #endregion
        #region properity
        //标的代码
        public string Symbol
        {
            get { return this.symbol; }
            set { this.symbol = value; }
        }
        //1: 股票, 2: 基金, 3: 指数, 4: 期货, 5: 期权, 10: 虚拟合约
        public int Sec_type
        {
            get { return this.sec_type; }
            set { this.sec_type = value; }
        }
        //交易所代码
        public string Exchange
        {
            get { return this.exchange; }
            set { this.exchange = value; }
        }
        //代码
        public string Sec_id
        {
            get { return this.sec_id; }
            set { this.sec_id = value; }
        }
        //名称
        public string Sec_name
        {
            get { return this.sec_name; }
            set { this.sec_name = value; }
        }
        //最小变动单位
        public double Price_tick
        {
            get { return this.price_tick; }
            set { this.price_tick = value; }
        }
        //上市日期
        public DateTime Listed_date
        {
            get { return this.listed_date; }
            set { this.listed_date = value; }
        }
        //退市日期
        public DateTime Delisted_date
        {
            get { return this.delisted_date; }
            set { this.delisted_date = value; }
        }
        //权重
        public double Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }
        #endregion
    }
}
