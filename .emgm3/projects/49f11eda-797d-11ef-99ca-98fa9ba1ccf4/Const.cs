using GMSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    internal class Const
    {
        //数据库连接
        public const string DB_CONNECT = "Data Source=MS-AQFABEGNBLRC;Initial Catalog=QuantChan;Integrated Security=True";                //数据库连接字符串
        //证券交易所代码
        public const string EXCH_CODE_SHSE = "SHSE";    //上交所
        public const string EXCH_CODE_SZSE = "SZSE";    //深交所
        public const string SYS_MARKET_START_TRADING_DATE = "2010-01-01";    //系统开始交易时间

        //买入股票返回值
        public const int BUY_STATUS_INSUFFICIENT_FAIL = -2;  //可用金额不足
        public const int BUY_STATUS_FAIL = -1;  //买入失败
        public const int BUY_STATUS_COMPLETED_SUCCESS = 1;  //买入全部成交
        public const int BUY_STATUS_PART_SUCCESS = 2;  //买入部分成交
                                                       //买入股票返回值
                                                       //卖出股票返回值
        public const int SELL_STATUS_FAIL = -1;  //卖出失败
        public const int SELL_STATUS_COMPLETED_SUCCESS = 1;  //卖出全部成交
        public const int SELL_STATUS_PART_SUCCESS = 2;  //卖出部分成交

        //键定义
        public const byte VK_F1 = 0x70;
        public const byte VK_F2 = 0x71;
        public const byte VK_F3 = 0x72;
        public const byte VK_F4 = 0x73;
        public const byte VK_F6 = 0x75;

        public const int KEYEVENTF_EXTENDEDKEY = 0x1;
        public const int KEYEVENTF_KEYUP = 0x2;

        public const int WM_SETTEXT = 0x000C;//发送消息
        public const int WM_LBUTTONDOWN = 0x0201;//按下鼠标左键 
        public const int WM_LBUTTONUP = 0x0202;//释放鼠标左键

        public const int INPUT_MOUSE = 0;
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //常用指数定义
        public const string INDEX_SHSE_000001 = "SHSE.000001"; //上证综合指数
        public const string INDEX_SZSE_399106 = "SZSE.399106"; //深证综合指数
        public const string INDEX_SZSE_399006 = "SZSE.399006"; //创业板指数

        public const string FREQUENCY_MINUT = "60s";    //1分钟
        public const string FREQUENCY_5MINUT = "300s";    //5分钟
        public const string FREQUENCY_15MINUT = "900s";    //15分钟
        public const string FREQUENCY_30MINUT = "1800s";    //30分钟
        public const string FREQUENCY_60MINUT = "3600s";    //60分钟
        public const string FREQUENCY_DAY = "1d";        //日

        public const int FREQUENCY_5MINUT_KLINE_NUMBER = 300;    //5分钟频率K线数量.

        public const int History_Day = 20;     //历史行情数

        public const string Time_Rule_160000 = "16:00:00";
        public const string Time_Rule_145900 = "14:59:00";
        public const string Time_Rule_150000 = "15:00:00";

        //技术指标定义开始
        public const int MA_PARA_3 = 3; //3日均线
        public const int MA_PARA_5 = 5; //5日均线
        public const int MA_PARA_8 = 8; //8日均线
        public const int MA_PARA_10 = 10; //10日均线
        public const int MA_PARA_14 = 14; //14日均线
        public const int MA_PARA_20 = 20; //20日均线
        public const int MA_PARA_30 = 30; //30日均线
        public const int MA_PARA_60 = 60; //60日均线
        public const int MA_PARA_90 = 90; //90日均线
        public const int MA_PARA_120 = 120; //120日均线
        public const int MA_PARA_250 = 250; //250日均线

        public static int[] MA_PARA = { MA_PARA_3, MA_PARA_5, MA_PARA_8, MA_PARA_10, MA_PARA_14, MA_PARA_20, MA_PARA_30, MA_PARA_60, MA_PARA_90, MA_PARA_120, MA_PARA_250 };

        public const int BBI_PARA_3 = 3;         //3日
        public const int BBI_PARA_4 = 4;         //数量为4
        public const int BBI_PARA_6 = 6;         //6日
        public const int BBI_PARA_12 = 12;        //12日
        public const int BBI_PARA_24 = 24;        //24日

        public const string EAST_MONEY_DATA_PATH_ALL = "D:\\股票\\东方财富数据\\全部概念"; //东方财富数据路径
        public const string EAST_MONEY_DATA_PATH_STOCK = "D:\\股票\\东方财富数据\\概念股票"; //东方财富数据路径

        public const int KDJ_PARA_N = 9; //KDJ的N值
        public const int KDJ_PARA_M1 = 3; //KDJ的M1值
        public const int KDJ_PARA_M2 = 3; //KDJ的M2值

        public const int BOLL_PARA_M = 2; //布林参数M
        public const int BOLL_PARA_N_20 = 20; //布林参数N  20
        public const int BOLL_PARA_N_480 = 480; //布林参数N 480
        public const int BOLL_PARA_BBI_N = 11; //多空布林参数N
        public const int BOLL_PARA_BBI_M = 6; //多空布林参数M

        public const int MACD_PARA_12 = 12; //MACD的12日快线移动平均
        public const int MACD_PARA_26 = 26; //MACD的26日慢线移动平均
        public const int MACD_PARA_9 = 9; //MACD的9日移动平均

        public const int RSI_PARA_6 = 6; //RSI的RSI1值
        public const int RSI_PARA_12 = 12; //RSI的RSI2值
        public const int RSI_PARA_24 = 24; //RSI的RSI3值

        //技术指标定义结束

        public const int MACD_DIF_GOLDEN_CROSS_BUY = 1; //MACD指标DIF金叉0
        public const int RSI_GOLDEN_CROSS_BUY = 2;      //RSI1金叉20且RSI1大于RSI2
        public const int BBI_MACD_BUY = 3;              //股价金叉BBI且收盘价连续3天大于BBI且MACD指标DIF大于0
        public const int LIMIT_UP_BUY = 4;              //因为当天股价涨停导致没有买入，次日买入
        public const int RSI_STOP_LOSS_SELL = 11;       //收盘价跌破RSI买入时的开盘价      
        public const int BBI_MACD_SELL = 12;                 //股价死叉BBI且收盘价连续3天小于BBI
        public const int LIMIT_DOWN_SELL = 13;          //因为当天股价跌停导致没有卖出，次日卖出

        //涨跌停
        public const int Limit_Up = 1;         //涨停
        public const int Limit_Down = 2;        //跌停

        public const int Rising_Speed_Para = 5;         //5分钟涨速

        //public const Adjust ADJUST = Adjust.ADJUST_POST; //后复权
        public const Adjust ADJUST = Adjust.ADJUST_PREV; //前复权


        public const string TOKEN = "Token";        //令牌
        public const string STRATEGY_ID = "StrategyId";        //策略运行模式

        public const string TRADE_FREQUENCY = "TradeFrequency";        //交易频率
        public const string STRATEGY_MODE = "StrategyMode";        //策略编号
        public const string BID_PRICE = "BidPrice";        //委买价
        public const string ASK_PRICE = "AskPrice";        //委卖价


        public const string MODE_LIVE = "MODE_LIVE";        //实时模式
        public const string MODE_BACKTEST = "MODE_BACKTEST";        //回测模式

        public const string BID_PRICE_ONE = "BID_PRICE_ONE";        //委买价一
        public const string BID_PRICE_TWO = "BID_PRICE_TWO";        //委买价二
        public const string BID_PRICE_THREE = "BID_PRICE_THREE";        //委买价三
        public const string BID_PRICE_FOUR = "BID_PRICE_FOUR";        //委买价四
        public const string BID_PRICE_FIVE = "BID_PRICE_FIVE";        //委买价五
        public const string BID_PRICE_LIMIT_UP = "BID_PRICE_LIMIT_UP";        //涨停价

        public const string ASK_PRICE_ONE = "ASK_PRICE_ONE";        //委卖价一
        public const string ASK_PRICE_TWO = "ASK_PRICE_TWO";        //委卖价二
        public const string ASK_PRICE_THREE = "ASK_PRICE_THREE";        //委卖价三
        public const string ASK_PRICE_FOUR = "ASK_PRICE_FOUR";        //委卖价四
        public const string ASK_PRICE_FIVE = "ASK_PRICE_FIVE";        //委卖价五
        public const string ASK_PRICE_LIMIT_DOWN = "ASK_PRICE_LIMIT_DOWN";        //跌停价

        public const string BACK_TEST_START_TIME = "BackTestStartTime";        //回测开始时间
        public const string BACK_TEST_END_TIME = "BackTestEndTime";        //回测结束时间

        public const string TRANSACTION_FAILURE_RETRY_TIMES = "TransactionFailureRetryTimes";        //交易失败重试次数


        public const int NUMBER_ONE = 1;         //数字1

        public const int ORDERSIDE_BUY = 1;        //买入
        public const int ORDERSIDE_SELL = 2;        //卖出

        public const int TRADE_KLINE_NUMBER = 100;          //交易时生成技术指标所需K线数量

        public const string SECURITIES_EXCHANGE = "SecuritiesExchange";        //证券交易所
        public const string SECURITIES_BLOCK = "SecuritiesBlock";        //证券板块

        public const string CONSTITUENTS_SH = "SHSE.000001";   //上证综合指数
        public const string CONSTITUENTS_SZ = "SZSE.399106";   //深证综合指数
        public const string CONSTITUENTS_CY = "SZSE.399006";   //创业板指数
        //public const string CONSTITUENTS_CY = "SZSE.399102";   //创业板指数

        public const string SHSZSE = "SHSZSE";   //全选
        public const string SHSE = "SHSE";   //上海证券交易所
        public const string SZSE = "SZSE";   //深圳证券交易所

        public const string ALL_BLOCK = "AllBlock";   //全选
        public const string MAIN_BLOCK = "MainBlock";   //主板
        public const string CHINEXT = "Chinext";   //创业板
        public const string STI_BLOCK = "STIBlock";   //科创板

    }
}
