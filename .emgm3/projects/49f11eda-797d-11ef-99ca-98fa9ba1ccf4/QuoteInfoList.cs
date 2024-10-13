using GMSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static trade.api.GetBorrowableInstrumentsRsp;

namespace MultiCodeDataEventDriven
{
    public class QuoteInfoList
    {
        public ArrayList quoteInfos; //股票的K线数组
        public QuoteInfoList()
        {
            quoteInfos = new ArrayList();
        }
        public QuoteInfoList(GMDataList<Bar> bars)
        {
            //为股票的K线数组赋值
            setquoteInfos(bars);
        }
        //为股票的K线数组赋值
        public void setquoteInfos(GMDataList<Bar> bars)
        {
            quoteInfos = new ArrayList();
            for (int i = 0; i < bars.data.Count; i++)
            {
                TechnicalIndicators technicalIndicators = new TechnicalIndicators();
                //将bars内容复制到quoteInfo实例里
                MapperProp(bars.data[i], ref technicalIndicators);
                //涨跌幅信息
                technicalIndicators.riseAndFallRang = new RiseAndFallRang();

                //KDJ技术指标
                technicalIndicators.kdj = new KDJ();
                //BOLL技术指标
                technicalIndicators.boll = new BOLL();
                //均线MA技术指标
                technicalIndicators.ma = new MA();
                //MACD技术指标
                technicalIndicators.macd = new MACD();
                //RSI技术指标
                technicalIndicators.rsi = new RSI();
                //BBI技术指标
                technicalIndicators.bbi = new BBI();

                quoteInfos.Add(technicalIndicators);
            }
            //计算各K线的技术指标值
            for (int i = 0; i < quoteInfos.Count; i++)
            {
                //涨跌幅信息
                new RiseAndFallRang(ref quoteInfos, i);
                //KDJ技术指标
                new KDJ(ref quoteInfos, i);
                //BOLL技术指标
                new BOLL(ref quoteInfos, i);
                //均线MA技术指标
                new MA(ref quoteInfos, i);
                //MACD技术指标
                new MACD(ref quoteInfos, i);
                //RSI技术指标
                new RSI(ref quoteInfos, i);
                //BBI技术指标
                new BBI(ref quoteInfos, i);
            }
        }
        //将bar的值复制到QuoteInfo实例中
        private  void MapperProp(Bar bar, ref TechnicalIndicators technicalIndicators)
        {
            technicalIndicators.bar.symbol = bar.symbol;               //证券交易所加股票代码
            technicalIndicators.bar.bob = bar.bob;                     //bar的开始时间
            technicalIndicators.bar.eob = bar.eob;                     //bar的结束时间
            technicalIndicators.bar.open = bar.open;                   //开盘价
            technicalIndicators.bar.close = bar.close;                 //收盘价
            technicalIndicators.bar.high = bar.high;                   //最高价
            technicalIndicators.bar.low = bar.low;                     //<最低价
            technicalIndicators.bar.volume = bar.volume;               //成交量
            technicalIndicators.bar.amount = bar.amount;               //成交金额
            technicalIndicators.bar.preClose = bar.preClose;           //昨收盘价，只有日频数据赋值

            technicalIndicators.bar.position = bar.position;           //持仓量
            technicalIndicators.bar.frequency = bar.frequency;         //bar频度
        }

    }
}
