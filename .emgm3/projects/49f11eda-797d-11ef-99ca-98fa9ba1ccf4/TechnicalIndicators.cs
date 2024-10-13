using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMSDK;

namespace MultiCodeDataEventDriven
{
    internal class TechnicalIndicators
    {
       #region field
        private string symbol;           //证券交易所加股票代码
        private DateTime bob;            //bar的开始时间
        private DateTime eob;            //bar的结束时间
        private float openPrice;              //开盘价
        private float closePrice;             //收盘价
        private float highPrice;              //最高价
        private float lowPrice;               //最低价
        private double volume;           //成交量
        private double amount;           //成交金额
        private float preClose;          //昨收盘价，只有日频数据赋值
        private System.Int64 position;   //持仓量
        private string frequency;        //bar频度
        //private float openPriceChan;          //缠论开盘价
        //private float closePriceChan;         //缠论收盘价
        private double bbiValue; //BBI指标的BBI值
        private double difValue; //MACD指标的DIF值
        private double deaValue; //MACD指标的DEA值
        private double macdValue; //MACD指标的MACD值
        private double rsi1Value; //RSI指标的RSI1值
        private double rsi2Value; //RSI指标的RSI2值
        private double rsi3Value; //RSI指标的RSI3值
        private double riseAndFallPerc; //涨跌百分比
        private double riseAndFallPrice; //涨跌价格
        private double amplPerc; //振幅百分比
        private int limitUpDownIdent; //涨跌停标识位
        private double risingSpeed; //涨速
        private double makeDealQuantityRate; //与前一周期的成交量比值
        private OrderSide orderSide; //委托方向
        private int dealType; //成交类型
        private double dealPrice; //成交价格
        private double stopLossPrice; //止损价格
        private double kValue; //KDJ指标的K值
        private double dValue; //KDJ指标的D值
        private double jValue; //KDJ指标的J值
        private double ma3; //3日均线
        private double ma5; //5日均线
        private double ma8; //8日均线
        private double ma10; //10日均线
        private double ma14; //14日均线
        private double ma20; //20日均线
        private double ma30; //30日均线
        private double ma60; //60日均线
        private double ma90; //90日均线
        private double ma120; //120日均线
        private double ma250; //250日均线
        private double boll20_ub; //参数20布林线上轨
        private double boll20_boll; //参数20布林线中轨
        private double boll20_lb; //参数20布林线下轨
        private double boll_bbi_ub; //多空布林线上轨
        private double boll_bbi_boll; //多空布林线中轨
        private double boll_bbi_lb; //多空布林线下轨
        private double boll480_ub; //参数480布林线上轨
        private double boll480_boll; //参数480布林线中轨
        private double boll480_lb; //参数480布林线下轨
        #endregion
        #region properity
        //证券交易所加股票代码
        public string Symbol
        {
            get { return this.bar.symbol; }
            set { this.bar.symbol = value; }
        }
        //bar的开始时间
        public DateTime Bob
        {
            get { return this.bar.bob; }
            set { this.bar.bob = value; }
        }
        //bar的结束时间
        public DateTime Eob
        {
            get { return this.bar.eob; }
            set { this.bar.eob = value; }
        }
        //开盘价
        public float OpenPrice
        {
            get { return this.bar.open; }
            set { this.bar.open = value; }
        }
        //收盘价
        public float ClosePrice
        {
            get { return this.bar.close; }
            set { this.bar.close = value; }
        }
        //最高价
        public float HighPrice
        {
            get { return this.bar.high; }
            set { this.bar.high = value; }
        }
        //最低价
        public float LowPrice
        {
            get { return this.bar.low; }
            set { this.bar.low = value; }
        }
        //成交量
        public double Volume
        {
            get { return this.bar.volume; }
            set { this.bar.volume = value; }
        }
        //成交金额
        public double Amount
        {
            get { return this.bar.amount; }
            set { this.bar.amount = value; }
        }
        //昨收盘价，只有日频数据赋值
        public float PreClose
        {
            get { return this.bar.preClose; }
            set { this.bar.preClose = value; }
        }
        //持仓量
        public System.Int64 Position
        {
            get { return this.bar.position; }
            set { this.bar.position = value; }
        }
        //bar频度
        public string Frequency
        {
            get { return this.bar.frequency; }
            set { this.bar.frequency = value; }
        }
        //开盘价
        //public float OpenPriceChan
        //{
        //    get { return openPriceChan; }
        //    set { openPriceChan = value; }
        //}
        //收盘价
        //public float ClosePriceChan
        //{
        //    get { return closePriceChan; }
        //    set { closePriceChan = value; }
        //}
        public double BbiValue  //BBI指标的BBI值
        {
            get { return this.bbi.bbi; }
            set { bbiValue = this.bbi.bbi; }
        }
        public double DifValue  //MACD指标的DIF值
        {
            get { return this.macd.dif; }
            set { difValue = this.macd.dif; }
        }
        public double DeaValue  //MACD指标的DEA值
        {
            get { return this.macd.dea; }
            set { deaValue = this.macd.dea; }
        }
        public double MacdValue  //MACD指标的MACD值
        {
            get { return this.macd.macd; }
            set { macdValue = this.macd.macd; }
        }
        public double Rsi1Value  //RSI指标的RSI1值
        {
            get { return this.rsi.rsi1; }
            set { rsi1Value = this.rsi.rsi1; ; }
        }
        public double Rsi2Value  //RSI指标的RSI2值
        {
            get { return this.rsi.rsi2; }
            set { rsi2Value = this.rsi.rsi2; }
        }
        public double Rsi3Value  //RSI指标的RSI3值
        {
            get { return this.rsi.rsi3; }
            set { rsi3Value = this.rsi.rsi3; }
        }
        public double RiseAndFallPerc  //涨跌百分比
        {
            get { return this.riseAndFallRang.riseAndFallPerc; }
            set { riseAndFallPerc = this.riseAndFallRang.riseAndFallPerc; }
        }
        public double RiseAndFallPrice  //涨跌价格
        {
            get { return this.riseAndFallRang.riseAndFallPrice; }
            set { riseAndFallPrice = this.riseAndFallRang.riseAndFallPrice; }
        }
        public double AmplPerc  //振幅百分比
        {
            get { return this.riseAndFallRang.amplPerc; }
            set { amplPerc = this.riseAndFallRang.amplPerc; }
        }
        public int LimitUpDownIdent  //涨跌停标识位
        {
            get { return this.riseAndFallRang.limitUpDownIdent; }
            set { limitUpDownIdent = this.riseAndFallRang.limitUpDownIdent; }
        }
        public double RisingSpeed  //涨速
        {
            get { return this.riseAndFallRang.risingSpeed; }
            set { risingSpeed = this.riseAndFallRang.risingSpeed; }
        }
        public double MakeDealQuantityRate  //与前一周期的成交量比值
        {
            get { return this.riseAndFallRang.makeDealQuantityRate; }
            set { makeDealQuantityRate = this.riseAndFallRang.makeDealQuantityRate; }
        }
        public OrderSide OrderSide  //委托方向
        {
            get { return this.dealInfo.orderSide; }
            set { orderSide = this.dealInfo.orderSide; }
        }
        public int DealType  //成交类型
        {
            get { return this.dealInfo.dealType; }
            set { dealType = this.dealInfo.dealType; }
        }
        public double DealPrice  //成交价格
        {
            get { return this.dealInfo.dealPrice; }
            set { dealPrice = this.dealInfo.dealPrice; }
        }
        public double StopLossPrice  //止损价格
        {
            get { return this.dealInfo.stopLossPrice; }
            set { stopLossPrice = this.dealInfo.stopLossPrice; }
        }
        public double KValue  //KDJ指标的K值
        {
            get { return this.kdj.k; }
            set { kValue = this.kdj.k; }
        }
        public double DValue  //KDJ指标的D值
        {
            get { return this.kdj.d; }
            set { dValue = this.kdj.d; }
        }
        public double JValue  //KDJ指标的J值
        {
            get { return this.kdj.j; }
            set { jValue = this.kdj.j; }
        }
        public double Ma3  //3日均线
        {
            get { return this.ma.ma3; }
            set { ma3 = this.ma.ma3; }
        }
        public double Ma5  //5日均线
        {
            get { return this.ma.ma5; }
            set { ma5 = this.ma.ma5; }
        }
        public double Ma8  //8日均线
        {
            get { return this.ma.ma8; }
            set { ma8 = this.ma.ma8; }
        }
        public double Ma10  //10日均线
        {
            get { return this.ma.ma10; }
            set { ma10 = this.ma.ma10; }
        }
        public double Ma14  //14日均线
        {
            get { return this.ma.ma14; }
            set { ma14 = this.ma.ma14; }
        }
        public double Ma20  //20日均线
        {
            get { return this.ma.ma20; }
            set { ma20 = this.ma.ma20; }
        }
        public double Ma30  //30日均线
        {
            get { return this.ma.ma30; }
            set { ma30 = this.ma.ma30; }
        }
        public double Ma60  //60日均线
        {
            get { return this.ma.ma60; }
            set { ma60 = this.ma.ma60; }
        }
        public double Ma90  //90日均线
        {
            get { return this.ma.ma90; }
            set { ma90 = this.ma.ma90; }
        }
        public double Ma120  //120日均线
        {
            get { return this.ma.ma120; }
            set { ma120 = this.ma.ma120; }
        }
        public double Ma250  //250日均线
        {
            get { return this.ma.ma250; }
            set { ma250 = this.ma.ma250; }
        }
        public double Boll20_ub  //参数20布林线上轨
        {
            get { return this.boll.boll20_ub; }
            set { boll20_ub = this.boll.boll20_ub; }
        }
        public double Boll20_boll  //参数20布林线中轨
        {
            get { return this.boll.boll20_boll; }
            set { boll20_boll = this.boll.boll20_boll; }
        }
        public double Boll20_lb  //参数20布林线下轨
        {
            get { return this.boll.boll20_lb; }
            set { boll20_lb = this.boll.boll20_lb; }
        }
        public double Boll_bbi_ub  //多空布林线上轨
        {
            get { return this.boll.boll_bbi_ub; }
            set { boll_bbi_ub = boll.boll_bbi_ub; }
        }
        public double Boll_bbi_boll  //多空布林线中轨
        {
            get { return this.boll.boll_bbi_boll; }
            set { boll_bbi_boll = this.boll.boll_bbi_boll; }
        }
        public double Boll_bbi_lb  //多空布林线下轨
        {
            get { return this.boll.boll_bbi_lb; }
            set { boll_bbi_lb = this.boll.boll_bbi_lb; }
        }
        public double Boll480_ub  //参数480布林线上轨
        {
            get { return this.boll.boll480_ub; }
            set { boll480_ub = this.boll.boll480_ub; }
        }
        public double Boll480_boll  //参数480布林线中轨
        {
            get { return this.boll.boll480_boll; }
            set { boll480_boll = this.boll.boll480_boll; }
        }
        public double Boll480_lb  //参数480布林线下轨
        {
            get { return this.boll.boll480_lb; }
            set { boll480_lb = this.boll.boll480_lb; }
        }
        #endregion
        public ChanBar bar = new ChanBar();
        public RiseAndFallRang riseAndFallRang;
        public DealInfo dealInfo;  //成交信息
        public BBI bbi;              //BBI指标
        public MACD macd;            //MACD指标
        public RSI rsi;              //RSI指标
        public KDJ kdj;            //KDJ指标
        public MA ma;            //均线MA指标
        public BOLL boll;            //布林指标
        public double reserve001;  //保留技术指标值001  RSI MAX1  指标值
        public double reserve002;  //保留技术指标值002  RSI ABS1  指标值
        public double reserve003;  //保留技术指标值003  RSI MAX2  指标值
        public double reserve004;  //保留技术指标值004  RSI ABS2  指标值
        public double reserve005;  //保留技术指标值005  RSI MAX3  指标值        
        public double reserve006;  //保留技术指标值006  RSI ABS3  指标值 
        public double reserve007;  //保留技术指标值007  MACD  EMA_SHORT  指标值
        public double reserve008;  //保留技术指标值008  MACD  EMA_LONG   指标值 
        public double reserve009;  //保留技术指标值009  MACD  DIFF       指标值      
        public double reserve010;  //保留技术指标值010  MACD  DEA        指标值
        public double reserve011;  //保留技术指标值011  MACD  MACD       指标值
        public double reserve012;  //保留技术指标值012  KDJ  K           指标值
        public double reserve013;  //保留技术指标值013  KDJ  D           指标值
        public double reserve031;  //保留技术指标值031  BOLL 参数20平均值      指标值
        public double reserve032;  //保留技术指标值032  BOLL 参数480平均值     指标值
        public double reserve033;  //保留技术指标值033  BOLL bbi(3日均线，6日均线，12日均线，24日均线相加 /4)            指标值

        public string Action01;
        public string Action02;
        public string Action03;
        public string Action04;
        public string Action05;
        public string Action06;
        public string Action07;
        public string Action08;
        public string Action09;
        public string Action10;


        public TechnicalIndicators()
        {
            //涨跌幅信息
            this.riseAndFallRang = new RiseAndFallRang();
            this.ma =new MA();
            this.bbi= new BBI();              //BBI指标
            this.macd = new MACD();            //MACD指标
            this.rsi = new RSI();              //RSI指标
            this.kdj = new KDJ();            //KDJ指标
            this.ma = new MA();            //均线MA指标
            this.boll = new BOLL();            //布林指标
            this.dealInfo = new DealInfo();  //成交信息
        }
};
    public class TechnicalIndicatorsList
    {

        public ArrayList technicalIndicatorsList = new ArrayList();            //技术指标List
        //构造函数
        public TechnicalIndicatorsList()
        {
            technicalIndicatorsList = new ArrayList();            //技术指标List
        }
        public  TechnicalIndicatorsList(GMDataList<Bar> bars)
        {
            this.technicalIndicatorsList = new ArrayList();
            for (int i = 0; i < bars.data.Count; i++)
            {
                TechnicalIndicators technicalIndicators = new TechnicalIndicators();
                technicalIndicators.bar.symbol = bars.data[i].symbol;//标的代码
                technicalIndicators.bar.bob = bars.data[i].bob;   //bar的开始时间
                technicalIndicators.bar.eob = bars.data[i].eob;   //bar的结束时间
                technicalIndicators.bar.open = bars.data[i].open;   //bar的开盘价
                technicalIndicators.bar.close = bars.data[i].close;   //bar的收盘价
                technicalIndicators.bar.high = bars.data[i].high;   //bar的最高价
                technicalIndicators.bar.low = bars.data[i].low;   //bar的最低价
                technicalIndicators.bar.volume = bars.data[i].volume;   //bar的成交量
                technicalIndicators.bar.amount = bars.data[i].amount;   //bar的成交金额
                technicalIndicators.bar.preClose = bars.data[i].preClose;   //昨收盘价，只有日频数据赋值
                technicalIndicators.bar.position = bars.data[i].position;   //持仓量
                technicalIndicators.bar.frequency = bars.data[i].frequency;   //bar频度
                technicalIndicators.bar.highChan = bars.data[i].high;   //bar的最高价
                technicalIndicators.bar.lowChan = bars.data[i].low;   //bar的最低价
                this.technicalIndicatorsList.Add(technicalIndicators);
                
            }
            //计算各K线的技术指标值
            for (int i = 0; i < technicalIndicatorsList.Count; i++)
            {
                //涨跌幅信息
                new RiseAndFallRang(ref technicalIndicatorsList, i);
                //KDJ技术指标
                new KDJ(ref technicalIndicatorsList, i);
                //BOLL技术指标
                new BOLL(ref technicalIndicatorsList, i);
                //均线MA技术指标
                new MA(ref technicalIndicatorsList, i);
                //MACD技术指标
                new MACD(ref technicalIndicatorsList, i);
                //RSI技术指标
                new RSI(ref technicalIndicatorsList, i);
                //BBI技术指标
                new BBI(ref technicalIndicatorsList, i);
                //计算缠论K线
                Chan.kLineChan(ref technicalIndicatorsList, i);
            }
            if (technicalIndicatorsList.Count>0) { 
                //处理包含K线
                Chan.containChan(ref technicalIndicatorsList);
                //划分笔的顶和底
                Chan.topBottomChan(ref technicalIndicatorsList);
                ////划分线段
                //Chan.lineSegmentChan(ref technicalIndicatorsList);
            }
        }
        public TechnicalIndicatorsList(ArrayList technicalList)
        {
            this.technicalIndicatorsList = new ArrayList();

            for (int i = 0; i < technicalList.Count; i++)
            {
                Bar bars = ((TechnicalIndicators)technicalList[i]).bar;
                TechnicalIndicators technicalIndicators = new TechnicalIndicators();
                technicalIndicators.bar.symbol = bars.symbol;//标的代码
                technicalIndicators.bar.bob = bars.bob;   //bar的开始时间
                technicalIndicators.bar.eob = bars.eob;   //bar的结束时间
                technicalIndicators.bar.open = bars.open;   //bar的开盘价
                technicalIndicators.bar.close = bars.close;   //bar的收盘价
                technicalIndicators.bar.high = bars.high;   //bar的最高价
                technicalIndicators.bar.low = bars.low;   //bar的最低价
                technicalIndicators.bar.volume = bars.volume;   //bar的成交量
                technicalIndicators.bar.amount = bars.amount;   //bar的成交金额
                technicalIndicators.bar.preClose = bars.preClose;   //昨收盘价，只有日频数据赋值
                technicalIndicators.bar.position = bars.position;   //持仓量
                technicalIndicators.bar.frequency = bars.frequency;   //bar频度
                technicalIndicators.bar.highChan = bars.high;   //bar的最高价
                technicalIndicators.bar.lowChan = bars.low;   //bar的最低价
                this.technicalIndicatorsList.Add(technicalIndicators);

            }
            //计算各K线的技术指标值
            for (int i = 0; i < technicalIndicatorsList.Count; i++)
            {
                //涨跌幅信息
                new RiseAndFallRang(ref technicalIndicatorsList, i);
                //KDJ技术指标
                new KDJ(ref technicalIndicatorsList, i);
                //BOLL技术指标
                new BOLL(ref technicalIndicatorsList, i);
                //均线MA技术指标
                new MA(ref technicalIndicatorsList, i);
                //MACD技术指标
                new MACD(ref technicalIndicatorsList, i);
                //RSI技术指标
                new RSI(ref technicalIndicatorsList, i);
                //BBI技术指标
                new BBI(ref technicalIndicatorsList, i);
                //计算缠论K线
                Chan.kLineChan(ref technicalIndicatorsList, i);
            }
            if (technicalIndicatorsList.Count > 0)
            {
                //处理包含K线
                Chan.containChan(ref technicalIndicatorsList);
                //划分笔的顶和底
                Chan.topBottomChan(ref technicalIndicatorsList);
                ////划分线段
                //Chan.lineSegmentChan(ref technicalIndicatorsList);
            }
        }
    };
    //记录MA指标
    public class MA
    {
        public double ma3 = 3; //3日均线
        public double ma5 = 5; //5日均线
        public double ma8 = 8; //8日均线
        public double ma10 = 10; //10日均线
        public double ma14 = 14; //14日均线
        public double ma20 = 20; //20日均线
        public double ma30 = 30; //30日均线
        public double ma60 = 60; //60日均线
        public double ma90 = 90; //90日均线
        public double ma120 = 120; //120日均线
        public double ma250 = 250; //250日均线
                                   //构造函数
        public MA()
        {
        }
        public MA(ref ArrayList TechnicalIndicatorss, int position)
        {
            //设置MA指标
            setMA(ref TechnicalIndicatorss, position);
        }

        //设置MA指标
        public void setMA(ref ArrayList arrayList, int position)
        {
            double[] average = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int j = 0; j < Const.MA_PARA.Length; j++)
            {
                if (position >= Const.MA_PARA[j] - 1)
                {
                    average[j] = Common.getAvg(ref arrayList, position, Const.MA_PARA[j]);
                }
            }
            ((TechnicalIndicators)arrayList[position]).ma.ma3 = Math.Round(average[0], 2);  //3天均线
            ((TechnicalIndicators)arrayList[position]).ma.ma5 = Math.Round(average[1], 2);  //5天均线
            ((TechnicalIndicators)arrayList[position]).ma.ma8 = Math.Round(average[2], 2);  //8天均线
            ((TechnicalIndicators)arrayList[position]).ma.ma10 = Math.Round(average[3], 2);  //10天均线
            ((TechnicalIndicators)arrayList[position]).ma.ma14 = Math.Round(average[4], 2);  //14天均线
            ((TechnicalIndicators)arrayList[position]).ma.ma20 = Math.Round(average[5], 2);  //20天均线
            ((TechnicalIndicators)arrayList[position]).ma.ma30 = Math.Round(average[6], 2);  //30天均线
            ((TechnicalIndicators)arrayList[position]).ma.ma60 = Math.Round(average[7], 2);  //60天均线
            ((TechnicalIndicators)arrayList[position]).ma.ma90 = Math.Round(average[8], 2);  //90天均线
            ((TechnicalIndicators)arrayList[position]).ma.ma120 = Math.Round(average[9], 2);  //120天均线
            ((TechnicalIndicators)arrayList[position]).ma.ma250 = Math.Round(average[10], 2); //250天均线
        }
    };
    //记录MA指标
    public class BOLL
    {
        public double boll20_ub; //参数20布林线上轨
        public double boll20_boll; //参数20布林线中轨
        public double boll20_lb; //参数20布林线下轨
        public double boll_bbi_ub; //多空布林线上轨
        public double boll_bbi_boll; //多空布林线中轨
        public double boll_bbi_lb; //多空布林线下轨
        public double boll480_ub; //参数480布林线上轨
        public double boll480_boll; //参数480布林线中轨
        public double boll480_lb; //参数480布林线下轨

        //构造函数
        public BOLL()
        {
        }
        public BOLL(ref ArrayList TechnicalIndicatorss, int position)
        {
            //设置布林指标
            setBOLL(ref TechnicalIndicatorss, position);
        }

        //设置布林指标
        public void setBOLL(ref ArrayList arrayList, int position)
        {
            //double[] average = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //for (int j = 0; j < Const.MA_PARA.Length; j++)
            //{
            //    if (position >= Const.MA_PARA[j] - 1)
            //    {
            //        average[j] = Common.getAvg(ref arrayList, position, Const.MA_PARA[j]);
            //    }
            //}
            /*多空布林线计算公式
            'CV:=CLOSE;
            'BBIBOLL:(MA(CV,3)+MA(CV,6)+MA(CV,12)+MA(CV,24))/4;
            'UPR:BBIBOLL+M*STD(BBIBOLL,N);
            'DWN:BBIBOLL-M*STD(BBIBOLL,N); */
            double[] average = { 0, 0, 0, 0, 0 };
            double bbiBoll;
            double std;

            if (position >= Const.BOLL_PARA_N_20 - 1)
            {
                average[4] = Common.getAvg(ref arrayList, position, Const.BOLL_PARA_N_20);
                //布林线
                ((TechnicalIndicators)arrayList[position]).reserve031 = double.Parse(average[4].ToString());
                ((TechnicalIndicators)arrayList[position]).boll.boll20_boll = Math.Round(double.Parse(average[4].ToString()), 2);     //BOLL
                std = Const.BOLL_PARA_M * Common.getSTD(ref arrayList, position, Const.BOLL_PARA_N_20, ((TechnicalIndicators)arrayList[position]).reserve031, "BOLL");
                ((TechnicalIndicators)arrayList[position]).boll.boll20_ub = Math.Round(((TechnicalIndicators)arrayList[position]).reserve031 + std, 2);  //BOLLUP
                ((TechnicalIndicators)arrayList[position]).boll.boll20_lb = Math.Round(((TechnicalIndicators)arrayList[position]).reserve031 - std, 2);  //BOLLDOWN
            }
            if (position >= Const.BOLL_PARA_N_480 - 1)
            {
                average[4] = Common.getAvg(ref arrayList, position, Const.BOLL_PARA_N_480);
                //布林线
                ((TechnicalIndicators)arrayList[position]).reserve032 = double.Parse(average[4].ToString());
                ((TechnicalIndicators)arrayList[position]).boll.boll480_boll = Math.Round(double.Parse(average[4].ToString()), 2);     //BOLL
                std = Const.BOLL_PARA_M * Common.getSTD(ref arrayList, position, Const.BOLL_PARA_N_480, ((TechnicalIndicators)arrayList[position]).reserve032, "BOLL");
                ((TechnicalIndicators)arrayList[position]).boll.boll480_ub = Math.Round(((TechnicalIndicators)arrayList[position]).reserve032 + std, 2);  //BOLLUP
                ((TechnicalIndicators)arrayList[position]).boll.boll480_lb = Math.Round(((TechnicalIndicators)arrayList[position]).reserve032 - std, 2);  //BOLLDOWN
            }
            if (position >= 23)
            {
                average[0] = Common.getAvg(ref arrayList, position, 3);
                average[1] = Common.getAvg(ref arrayList, position, 6);
                average[2] = Common.getAvg(ref arrayList, position, 12);
                average[3] = Common.getAvg(ref arrayList, position, 24);

                bbiBoll = double.Parse(((average[0] + average[1] + average[2] + average[3]) / 4).ToString());
                //多空布林线
                ((TechnicalIndicators)arrayList[position]).reserve033 = bbiBoll;
                ((TechnicalIndicators)arrayList[position]).boll.boll_bbi_boll = double.Parse(Math.Round(bbiBoll, 2).ToString());    //BBIBOLLBBI
                if (position >= 23 + Const.BOLL_PARA_BBI_N - 1)
                {
                    std = Const.BOLL_PARA_BBI_M * Common.getSTD(ref arrayList, position, Const.BOLL_PARA_BBI_N, ((TechnicalIndicators)arrayList[position]).reserve033, "BBIBOLL");
                    ((TechnicalIndicators)arrayList[position]).boll.boll_bbi_ub = Math.Round(((TechnicalIndicators)arrayList[position]).reserve033 + std, 2);  //BBIBOLLUP
                    ((TechnicalIndicators)arrayList[position]).boll.boll_bbi_lb = Math.Round(((TechnicalIndicators)arrayList[position]).reserve033 - std, 2);  //BBIBOLLDOWN
                }
            }
        }
    };
    //记录KDJ指标
    public class KDJ
    {
        public double k;              //KDJ指标的K值
        public double d;              //KDJ指标的D值
        public double j;             //KDJ指标的J值
        //构造函数
        public KDJ()
        {
        }
        public KDJ(ref ArrayList TechnicalIndicatorss, int position)
        {
            //设置KDJ指标
            setKDJ(ref TechnicalIndicatorss, position);

        }
        //设置KDJ指标
        public void setKDJ(ref ArrayList arrayList, int position)
        {
            double gpHigh = 0;         //周期内的最高价
            double gpLow = 0;         //周期内的最低价
            int startPos, endPos;
            double KValue = 0;
            double DValue = 0;
            int intN = Const.KDJ_PARA_N;
            int M1 = Const.KDJ_PARA_M1;
            int M2 = Const.KDJ_PARA_M2;

            //总记录数大于等于KDJ中的N值
            if (arrayList.Count >= intN)
            {
                if (position >= intN - 1)
                {
                    startPos = position - (intN - 1);
                    endPos = position;
                }
                else
                {
                    startPos = 0;
                    endPos = position;
                }
            }
            else
            { //总记录数小于等于KDJ中的N值
                startPos = 0;
                endPos = arrayList.Count - 1;
            }
            for (int i = startPos; i <= endPos; i++)
            {
                //如果是第一条记录则用此记录的最高价和最低价初始化变量gpHigh和gpLow
                if (i == startPos)
                {
                    gpHigh = ((TechnicalIndicators)arrayList[i]).bar.high;
                    gpLow = ((TechnicalIndicators)arrayList[i]).bar.low;
                }
                else
                {
                    //如果当前记录的最高价大于变量gpHigh,则将此最高价赋值给变量gpHigh
                    if (((TechnicalIndicators)arrayList[i]).bar.high > gpHigh)
                    {
                        gpHigh = ((TechnicalIndicators)arrayList[i]).bar.high;
                    }
                    //如果当前记录的最低价小于变量gpLow,则将此最低价赋值给变量gpLow
                    if (((TechnicalIndicators)arrayList[i]).bar.low < gpLow)
                    {
                        gpLow = ((TechnicalIndicators)arrayList[i]).bar.low;
                    }
                }
            }
            //以日KDJ数值的计算为例,其计算公式为：n日RSV=（Cn－Ln）÷（Hn－Ln）×100 
            //公式中,Cn为第n日收盘价；Ln为n日内的最低价；Hn为n日内的最高价。RSV值始终在1—100间波动。
            //若无前一日K 值与D值,则可分别用50来代替。

            double KYesterday, DYesterday;
            if (position == 0)
            {
                if (gpHigh - gpLow == 0)
                {
                    if (((TechnicalIndicators)arrayList[position]).bar.close == gpLow)
                    {
                        KValue = 0;
                    }
                    else if (((TechnicalIndicators)arrayList[position]).bar.close == gpHigh)
                    {
                        KValue = 100.0;
                    }
                    else
                    {
                        KValue = 50;
                    }
                }
                else
                {
                    KValue = (((TechnicalIndicators)arrayList[position]).bar.close - gpLow) / (gpHigh - gpLow) * 100;
                }

                DValue = KValue;
                ((TechnicalIndicators)arrayList[position]).reserve012 = KValue;
                ((TechnicalIndicators)arrayList[position]).kdj.k = Math.Round(((TechnicalIndicators)arrayList[position]).reserve012, 2);
                ((TechnicalIndicators)arrayList[position]).reserve013 = DValue;
                ((TechnicalIndicators)arrayList[position]).kdj.d = Math.Round(((TechnicalIndicators)arrayList[position]).reserve013, 2);
                ((TechnicalIndicators)arrayList[position]).kdj.j = Math.Round(3 * KValue - 2 * DValue, 2);
            }
            else
            {
                KYesterday = ((TechnicalIndicators)arrayList[position - 1]).reserve012;
                DYesterday = ((TechnicalIndicators)arrayList[position - 1]).reserve013;
                if (gpHigh - gpLow == 0)
                {
                    KValue = (M1 - 1.0) / M1 * KYesterday + 1.0 / M1 * 100;
                }
                else
                {
                    KValue = ((M1 - 1.0) / M1 * KYesterday + 1.0 / M1 * ((((TechnicalIndicators)arrayList[position]).bar.close - gpLow) / (gpHigh - gpLow) * 100));
                }

                DValue = ((M2 - 1.0) / M2 * DYesterday + 1.0 / M2 * KValue);
                ((TechnicalIndicators)arrayList[position]).reserve012 = KValue;
                ((TechnicalIndicators)arrayList[position]).kdj.k = Math.Round(((TechnicalIndicators)arrayList[position]).reserve012, 2);
                ((TechnicalIndicators)arrayList[position]).reserve013 = DValue;
                ((TechnicalIndicators)arrayList[position]).kdj.d = Math.Round(((TechnicalIndicators)arrayList[position]).reserve013, 2);
                ((TechnicalIndicators)arrayList[position]).kdj.j = Math.Round(3 * KValue - 2 * DValue, 2);
            }
        }
    };
    //记录MACD指标
    public class MACD
    {
        public double dif;              //MACD指标的DIF值
        public double dea;              //MACD指标的DEA值
        public double macd;             //MACD指标的MACD值
        //构造函数
        public MACD()
        {
        }
        public MACD(ref ArrayList TechnicalIndicatorss, int position)
        {
            //设置MACD指标
            setMACD(ref TechnicalIndicatorss, position);
            //如果查询记录数大于等于24+3
            if (position >= Const.BBI_PARA_24 - 1 + 3)
            {
                //设置成交信息
                setDealInfo(ref TechnicalIndicatorss, position);
            }
        }
        //设置成交信息
        public void setDealInfo(ref ArrayList arrayList, int position)
        {
            if (position > 0)
            {
                //如果MACD的DIF指标金叉0
                if (((TechnicalIndicators)arrayList[position - 1]).macd.dif < 0
                    && ((TechnicalIndicators)arrayList[position]).macd.dif >= 0)
                {
                    //MACD指标DIF金叉0买入
                    ((TechnicalIndicators)arrayList[position]).dealInfo.orderSide = OrderSide.OrderSide_Buy;   //委托方向 买入
                    ((TechnicalIndicators)arrayList[position]).dealInfo.dealType = Const.MACD_DIF_GOLDEN_CROSS_BUY;
                    ((TechnicalIndicators)arrayList[position]).dealInfo.dealPrice = ((TechnicalIndicators)arrayList[position]).bar.close;
                }
            }
        }
        //设置MACD指标
        public void setMACD(ref ArrayList arrayList, int position)
        {
            int dictValue0 = Const.MACD_PARA_12;
            int dictValue1 = Const.MACD_PARA_26;
            int dictValue2 = Const.MACD_PARA_9;
            if (position == 0)
            {
                ((TechnicalIndicators)arrayList[position]).reserve007 = ((TechnicalIndicators)arrayList[position]).bar.close;     //EMA_SHORT
                ((TechnicalIndicators)arrayList[position]).reserve008 = ((TechnicalIndicators)arrayList[position]).bar.close;     //EMA_LONG
                ((TechnicalIndicators)arrayList[position]).macd.dif = 0.00;   //MACD的DIF值
                ((TechnicalIndicators)arrayList[position]).macd.dea = 0.00;   //MACD的DEA值
                ((TechnicalIndicators)arrayList[position]).macd.macd = 0.00;   //MACD的MACD值
            }
            else
            {
                /* 计算公式
                'EMA（12）= 前一日EMA（12）×11/13＋今日收盘价×2/13 
                'EMA（26）= 前一日EMA（26）×25/27＋今日收盘价×2/27             
                '  DIFF = 今日EMA(12) - 今日EMA(26)
                'DEA（MACD）= 前一日DEA×8/10＋今日DIF×2/10              
                'BAR=2×(DIFF－DEA)*/

                ((TechnicalIndicators)arrayList[position]).reserve007 = ((TechnicalIndicators)arrayList[position - 1]).reserve007 * (dictValue0 - 1) / (dictValue0 + 1) + ((TechnicalIndicators)arrayList[position]).bar.close * 2 / (dictValue0 + 1);
                ((TechnicalIndicators)arrayList[position]).reserve008 = ((TechnicalIndicators)arrayList[position - 1]).reserve008 * (dictValue1 - 1) / (dictValue1 + 1) + ((TechnicalIndicators)arrayList[position]).bar.close * 2 / (dictValue1 + 1);
                ((TechnicalIndicators)arrayList[position]).reserve009 = (((TechnicalIndicators)arrayList[position]).reserve007 - ((TechnicalIndicators)arrayList[position]).reserve008);
                ((TechnicalIndicators)arrayList[position]).reserve010 = (((TechnicalIndicators)arrayList[position - 1]).reserve010 * (dictValue2 - 1) / (dictValue2 + 1) + ((TechnicalIndicators)arrayList[position]).reserve009 * 2 / (dictValue2 + 1));
                ((TechnicalIndicators)arrayList[position]).reserve011 = (2 * (((TechnicalIndicators)arrayList[position]).reserve009 - ((TechnicalIndicators)arrayList[position]).reserve010));

                ((TechnicalIndicators)arrayList[position]).macd.dif = Math.Round((((TechnicalIndicators)arrayList[position]).reserve009), 2);
                ((TechnicalIndicators)arrayList[position]).macd.dea = Math.Round((((TechnicalIndicators)arrayList[position]).reserve010), 2);
                ((TechnicalIndicators)arrayList[position]).macd.macd = Math.Round(((TechnicalIndicators)arrayList[position]).reserve011, 2);
            }
        }
    };
    //记录BBI指标
    public class BBI
    {
        public double bbi;              //BBI指标的BBI值
        //构造函数
        public BBI()
        {
        }
        public BBI(ref ArrayList TechnicalIndicatorss, int position)
        {
            //设置BBI指标
            setBBI(ref TechnicalIndicatorss, position);
            //如果查询记录数大于等于24+3
            if (position >= Const.BBI_PARA_24 - 1 + 3)
            {
                //设置成交信息
                setDealInfo(ref TechnicalIndicatorss, position);
            }
        }
        //设置成交信息
        public void setDealInfo(ref ArrayList arrayList, int position)
        {
            if (position >= Const.BBI_PARA_24 - 1 + 3)
            {
                //股价金叉BBI且收盘价连续3天大于BBI且MACD指标DIF大于0
                if (((TechnicalIndicators)arrayList[position]).macd.dif >= 0)
                {
                    if (((TechnicalIndicators)arrayList[position]).bar.close >= ((TechnicalIndicators)arrayList[position]).bbi.bbi
                        && ((TechnicalIndicators)arrayList[position - 1]).bar.close >= ((TechnicalIndicators)arrayList[position - 1]).bbi.bbi
                        && ((TechnicalIndicators)arrayList[position - 2]).bar.close >= ((TechnicalIndicators)arrayList[position - 2]).bbi.bbi
                        && ((TechnicalIndicators)arrayList[position - 3]).bar.close < ((TechnicalIndicators)arrayList[position - 3]).bbi.bbi)
                    {
                        //股价金叉BBI且收盘价连续3天大于BBI且MACD指标DIF大于0买入
                        ((TechnicalIndicators)arrayList[position]).dealInfo.orderSide = OrderSide.OrderSide_Buy;   //委托方向 买入
                        ((TechnicalIndicators)arrayList[position]).dealInfo.dealType = Const.BBI_MACD_BUY;
                        ((TechnicalIndicators)arrayList[position]).dealInfo.dealPrice = ((TechnicalIndicators)arrayList[position]).bar.close;
                    }
                    //if (((TechnicalIndicators)arrayList[position]).bar.close >= ((TechnicalIndicators)arrayList[position]).bbi.bbi
                    //    && ((TechnicalIndicators)arrayList[position - 1]).bar.close >= ((TechnicalIndicators)arrayList[position - 1]).bbi.bbi
                    //    && ((TechnicalIndicators)arrayList[position - 2]).bar.close >= ((TechnicalIndicators)arrayList[position - 2]).bbi.bbi)
                    //{
                    //    //股价金叉BBI且收盘价连续3天大于BBI且MACD指标DIF大于0买入
                    //    ((TechnicalIndicators)arrayList[position]).dealInfo.orderSide = OrderSide.OrderSide_Buy;   //委托方向 买入
                    //    ((TechnicalIndicators)arrayList[position]).dealInfo.dealType = Const.BBI_MACD_BUY;
                    //    ((TechnicalIndicators)arrayList[position]).dealInfo.dealPrice = ((TechnicalIndicators)arrayList[position]).bar.close;
                    //}
                }
                //股价死叉BBI且收盘价连续3天小于BBI
                if (((TechnicalIndicators)arrayList[position]).bar.close < ((TechnicalIndicators)arrayList[position]).bbi.bbi
                    && ((TechnicalIndicators)arrayList[position - 1]).bar.close < ((TechnicalIndicators)arrayList[position - 1]).bbi.bbi
                    && ((TechnicalIndicators)arrayList[position - 2]).bar.close < ((TechnicalIndicators)arrayList[position - 2]).bbi.bbi
                    && ((TechnicalIndicators)arrayList[position - 3]).bar.close >= ((TechnicalIndicators)arrayList[position - 3]).bbi.bbi)
                {
                    ////且MACD指标DIF大于0
                    //if (((TechnicalIndicators)arrayList[position]).Macd.macd >= 0)
                    //{
                    //    //股价金叉BBI且收盘价连续3天大于BBI且MACD指标DIF大于0买入
                    //    ((TechnicalIndicators)arrayList[position]).DealInfo.orderSide = OrderSide.OrderSide_Unknown.ToString();   //委托方向 不明
                    //    ((TechnicalIndicators)arrayList[position]).DealInfo.dealType = Const.BBI_MACD_STOP_LOSS_SELL;
                    //    ((TechnicalIndicators)arrayList[position]).DealInfo.dealPrice = ((TechnicalIndicators)arrayList[position]).Close;
                    //    ((TechnicalIndicators)arrayList[position]).DealInfo.stopLossPrice = ((TechnicalIndicators)arrayList[position]).Open;
                    //}
                    ////且MACD指标DIF小于0
                    //if (((TechnicalIndicators)arrayList[position]).Macd.macd < 0)
                    //{
                    ////股价死叉BBI且收盘价连续3天小于BBI
                    ((TechnicalIndicators)arrayList[position]).dealInfo.orderSide = OrderSide.OrderSide_Sell;   //委托方向 卖出
                    ((TechnicalIndicators)arrayList[position]).dealInfo.dealType = Const.BBI_MACD_SELL;
                    ((TechnicalIndicators)arrayList[position]).dealInfo.dealPrice = ((TechnicalIndicators)arrayList[position]).bar.close;
                    //}
                }
                ////股价死叉BBI且收盘价连续3天小于BBI
                //if (((TechnicalIndicators)arrayList[position]).bar.close < ((TechnicalIndicators)arrayList[position]).bbi.bbi
                //    && ((TechnicalIndicators)arrayList[position - 1]).bar.close < ((TechnicalIndicators)arrayList[position - 1]).bbi.bbi
                //    && ((TechnicalIndicators)arrayList[position - 2]).bar.close < ((TechnicalIndicators)arrayList[position - 2]).bbi.bbi)
                //{
                //    ////股价死叉BBI且收盘价连续3天小于BBI
                //    ((TechnicalIndicators)arrayList[position]).dealInfo.orderSide = OrderSide.OrderSide_Sell;   //委托方向 卖出
                //    ((TechnicalIndicators)arrayList[position]).dealInfo.dealType = Const.BBI_MACD_SELL;
                //    ((TechnicalIndicators)arrayList[position]).dealInfo.dealPrice = ((TechnicalIndicators)arrayList[position]).bar.close;                  
                //}
            }
        }
        //获取bbi的值
        public void setBBI(ref ArrayList TechnicalIndicatorss, int position)
        {
            //如果查询记录数大于等于24
            if (position >= Const.BBI_PARA_24 - 1)
            {
                /*多空均线计算公式
                'CV:=CLOSE;
                'BBIBOLL:(MA(CV,3)+MA(CV,6)+MA(CV,12)+MA(CV,24))/4;  */

                this.bbi = Math.Round((Common.getAvg(ref TechnicalIndicatorss, position, Const.BBI_PARA_3)
                    + Common.getAvg(ref TechnicalIndicatorss, position, Const.BBI_PARA_6)
                    + Common.getAvg(ref TechnicalIndicatorss, position, Const.BBI_PARA_12)
                    + Common.getAvg(ref TechnicalIndicatorss, position, Const.BBI_PARA_24))
                    / Const.BBI_PARA_4, 2);
            }
            else
            {
                this.bbi = 0;
            }
            ((TechnicalIndicators)TechnicalIndicatorss[position]).bbi.bbi = this.bbi;
        }
    };
    //记录RSI指标
    public class RSI
    {
        public double rsi1;              //RSI指标的RSI1值
        public double rsi2;              //RSI指标的RSI2值
        public double rsi3;              //RSI指标的RSI2值
        //构造函数
        public RSI()
        {
        }
        public RSI(ref ArrayList TechnicalIndicatorss, int position)
        {
            //设置RSI指标
            setRSI(ref TechnicalIndicatorss, position);
            //如果查询记录数大于等于24+3
            if (position >= Const.BBI_PARA_24 - 1 + 3)
            {
                //设置成交信息
                setDealInfo(ref TechnicalIndicatorss, position);
            }
        }
        //设置成交信息
        public void setDealInfo(ref ArrayList arrayList, int position)
        {
            if (position > 0)
            {
                ////RSI1金叉20且RSI1大于RSI2
                //if (((TechnicalIndicators)arrayList[position - 1]).rsi.rsi1 < 20
                //    && ((TechnicalIndicators)arrayList[position]).rsi.rsi1 >= 20
                //    && ((TechnicalIndicators)arrayList[position]).rsi.rsi1 > ((TechnicalIndicators)arrayList[position]).rsi.rsi2)
                //{
                //    //如果RSI的RSI1指标金叉20且RSI1金叉RSI2买入
                //    ((TechnicalIndicators)arrayList[position]).dealInfo.orderSide = OrderSide.OrderSide_Buy;   //委托方向 买入
                //    ((TechnicalIndicators)arrayList[position]).dealInfo.dealType = Const.RSI_GOLDEN_CROSS_BUY;
                //    ((TechnicalIndicators)arrayList[position]).dealInfo.dealPrice = ((TechnicalIndicators)arrayList[position]).bar.close;
                //    ((TechnicalIndicators)arrayList[position]).dealInfo.stopLossPrice = ((TechnicalIndicators)arrayList[position]).bar.open;  //止损价格
                //}
            }
        }
        //获取rsi1、rsi2、rsi3的值
        public void setRSI(ref ArrayList arrayList, int position)
        {
            int dictValue0 = Const.RSI_PARA_6;
            int dictValue1 = Const.RSI_PARA_12;
            int dictValue2 = Const.RSI_PARA_24;

            if (position == 0)
            {
                ((TechnicalIndicators)arrayList[position]).reserve001 = 0.00;            //MAX1 
                ((TechnicalIndicators)arrayList[position]).reserve002 = 0.00;            //ABS1
                ((TechnicalIndicators)arrayList[position]).reserve003 = 0.00;            //MAX2
                ((TechnicalIndicators)arrayList[position]).reserve004 = 0.00;            //ABS2
                ((TechnicalIndicators)arrayList[position]).reserve005 = 0.00;            //MAX3
                ((TechnicalIndicators)arrayList[position]).reserve006 = 0.00;            //ABS3
                ((TechnicalIndicators)arrayList[position]).rsi.rsi1 = 0.00;            //RSI1
                ((TechnicalIndicators)arrayList[position]).rsi.rsi2 = 0.00;            //RSI2
                ((TechnicalIndicators)arrayList[position]).rsi.rsi3 = 0.00;            //RSI3
            }
            else
            {
                /*  计算RSI指标函数
                '* LC:=REF(CLOSE,1);
                '* RSI1:SMA(MAX(CLOSE-LC,0),N1,1)/SMA(ABS(CLOSE-LC),N1,1)*100;
                '* RSI2:SMA(MAX(CLOSE-LC,0),N2,1)/SMA(ABS(CLOSE-LC),N2,1)*100;
                '* RSI3:SMA(MAX(CLOSE-LC,0),N3,1)/SMA(ABS(CLOSE-LC),N3,1)*100; */
                ((TechnicalIndicators)arrayList[position]).reserve001 = (Math.Max(((TechnicalIndicators)arrayList[position]).bar.close - ((TechnicalIndicators)arrayList[position - 1]).bar.close, 0) + (dictValue0 - 1) * ((TechnicalIndicators)arrayList[position - 1]).reserve001) / dictValue0;
                ((TechnicalIndicators)arrayList[position]).reserve002 = (Math.Abs(((TechnicalIndicators)arrayList[position]).bar.close - ((TechnicalIndicators)arrayList[position - 1]).bar.close) + (dictValue0 - 1) * ((TechnicalIndicators)arrayList[position - 1]).reserve002) / dictValue0;
                ((TechnicalIndicators)arrayList[position]).reserve003 = (Math.Max(((TechnicalIndicators)arrayList[position]).bar.close - ((TechnicalIndicators)arrayList[position - 1]).bar.close, 0) + (dictValue1 - 1) * ((TechnicalIndicators)arrayList[position - 1]).reserve003) / dictValue1;
                ((TechnicalIndicators)arrayList[position]).reserve004 = (Math.Abs(((TechnicalIndicators)arrayList[position]).bar.close - ((TechnicalIndicators)arrayList[position - 1]).bar.close) + (dictValue1 - 1) * ((TechnicalIndicators)arrayList[position - 1]).reserve004) / dictValue1;
                ((TechnicalIndicators)arrayList[position]).reserve005 = (Math.Max(((TechnicalIndicators)arrayList[position]).bar.close - ((TechnicalIndicators)arrayList[position - 1]).bar.close, 0) + (dictValue2 - 1) * ((TechnicalIndicators)arrayList[position - 1]).reserve005) / dictValue2;
                ((TechnicalIndicators)arrayList[position]).reserve006 = (Math.Abs(((TechnicalIndicators)arrayList[position]).bar.close - ((TechnicalIndicators)arrayList[position - 1]).bar.close) + (dictValue2 - 1) * ((TechnicalIndicators)arrayList[position - 1]).reserve006) / dictValue2;

                if (((TechnicalIndicators)arrayList[position]).reserve002 != 0)
                {
                    ((TechnicalIndicators)arrayList[position]).rsi.rsi1 = Math.Round((((TechnicalIndicators)arrayList[position]).reserve001 / ((TechnicalIndicators)arrayList[position]).reserve002 * 100), 2);
                }
                else
                {
                    ((TechnicalIndicators)arrayList[position]).rsi.rsi1 = Math.Round((((TechnicalIndicators)arrayList[position]).reserve001 / 1 * 100), 2);
                }
                if (((TechnicalIndicators)arrayList[position]).reserve004 != 0)
                {
                    ((TechnicalIndicators)arrayList[position]).rsi.rsi2 = Math.Round((((TechnicalIndicators)arrayList[position]).reserve003 / ((TechnicalIndicators)arrayList[position]).reserve004 * 100), 2);
                }
                else
                {
                    ((TechnicalIndicators)arrayList[position]).rsi.rsi2 = Math.Round((((TechnicalIndicators)arrayList[position]).reserve003 / 1 * 100), 2);
                }
                if (((TechnicalIndicators)arrayList[position]).reserve006 != 0)
                {
                    ((TechnicalIndicators)arrayList[position]).rsi.rsi3 = Math.Round((((TechnicalIndicators)arrayList[position]).reserve005 / ((TechnicalIndicators)arrayList[position]).reserve006 * 100), 2);
                }
                else
                {
                    ((TechnicalIndicators)arrayList[position]).rsi.rsi3 = Math.Round((((TechnicalIndicators)arrayList[position]).reserve005 / 1 * 100), 2);
                }
            }
        }
    };
    public class DealInfo
    {
        public OrderSide orderSide;           //委托方向
        public int dealType = 0;              //成交类型
        public double dealPrice;          //成交价格
        public double stopLossPrice;      //止损价格
    }
    //记录涨跌价格和涨跌百分比
    public class RiseAndFallRang
    {
        public double riseAndFallPerc;              //涨跌百分比
        public double riseAndFallPrice;             //涨跌价格
        public double amplPerc;                     //振幅百分比
        public int limitUpDownIdent;             //涨跌停标识位
        public double risingSpeed;               //涨速
        public double makeDealQuantityRate;             //与前一周期的成交量比值
        public double quantityYesterdayRate;             //与昨天成交量平均值比值
        //构造函数
        public RiseAndFallRang()
        {
        }
        public RiseAndFallRang(ref ArrayList TechnicalIndicatorss, int position)
        {
            setRiseAndFallRang(ref TechnicalIndicatorss, position);
        }
        public void setRiseAndFallRang(ref ArrayList arrayList, int position)
        {
            if (position > 0)
            {
                //计算涨跌幅
                if (((TechnicalIndicators)arrayList[position - 1]).bar.close > 0)
                {
                    //涨跌幅 计算公式为：(当前交易日收盘价 - 前一交易日收盘价)/前一交易日收盘价
                    ((TechnicalIndicators)arrayList[position]).riseAndFallRang.riseAndFallPerc = Math.Round(((((TechnicalIndicators)arrayList[position]).bar.close - ((TechnicalIndicators)arrayList[position - 1]).bar.close) / ((TechnicalIndicators)arrayList[position - 1]).bar.close), 4) * 100;
                    //涨跌 计算公式为：当前交易日收盘价 - 前一交易日收盘价
                    ((TechnicalIndicators)arrayList[position]).riseAndFallRang.riseAndFallPrice = Math.Round((((TechnicalIndicators)arrayList[position]).bar.close - ((TechnicalIndicators)arrayList[position - 1]).bar.close), 2);
                    //振幅 计算公式为：(当前交易最高价 - 当前交易最低价)/前一交易日收盘价
                    ((TechnicalIndicators)arrayList[position]).riseAndFallRang.amplPerc = Math.Round(((((TechnicalIndicators)arrayList[position]).bar.high - ((TechnicalIndicators)arrayList[position - 1]).bar.low) / ((TechnicalIndicators)arrayList[position - 1]).bar.close), 4) * 100;
                    decimal limitUpPrice = 0;
                    ((TechnicalIndicators)arrayList[position]).riseAndFallRang.limitUpDownIdent = Common.limitUpDown(((TechnicalIndicators)arrayList[position]).bar.symbol, ((TechnicalIndicators)arrayList[position]).bar.close, ((TechnicalIndicators)arrayList[position - 1]).bar.close, ref limitUpPrice);

                }
            }
            if ((position > Const.Rising_Speed_Para))
            {
                //计算涨速
                if (((TechnicalIndicators)arrayList[position - Const.Rising_Speed_Para]).bar.close > 0)
                {
                    ((TechnicalIndicators)arrayList[position]).riseAndFallRang.risingSpeed = Math.Round((((TechnicalIndicators)arrayList[position]).bar.close - ((TechnicalIndicators)arrayList[position - Const.Rising_Speed_Para]).bar.close) / ((TechnicalIndicators)arrayList[position - Const.Rising_Speed_Para]).bar.close, 4) * 100;
                }
            }
            //计算与前一周期的成交量比值
            if ((position > Const.Rising_Speed_Para + Const.Rising_Speed_Para))
            {
                double previousVolume = 0;
                int count = 0;
                for (int j = 0; j < arrayList.Count - Const.Rising_Speed_Para; j++)
                {
                    count++;
                    previousVolume = previousVolume + ((TechnicalIndicators)arrayList[j]).bar.volume;
                }
                previousVolume = previousVolume / count;
                double currentVolume = 0;
                count = 0;
                for (int j = arrayList.Count - Const.Rising_Speed_Para; j < arrayList.Count; j++)
                {
                    count++;
                    currentVolume = currentVolume + ((TechnicalIndicators)arrayList[j]).bar.volume;
                }
                currentVolume = currentVolume / count;

                if (previousVolume > 0)
                {
                    ((TechnicalIndicators)arrayList[position]).riseAndFallRang.makeDealQuantityRate = Math.Round(currentVolume / previousVolume, 2);
                }
            }
        }
    }
}
