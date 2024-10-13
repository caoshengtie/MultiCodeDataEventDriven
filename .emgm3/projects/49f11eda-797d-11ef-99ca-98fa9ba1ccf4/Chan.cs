using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    internal class Chan
    {
        //计算缠论K线的最高价和最低价
        public static void kLineChan(ref ArrayList arrayList, int position)
        {
            string securityCode = ((TechnicalIndicators)arrayList[position]).bar.symbol; //证券代码
            DateTime transactionDate = ((TechnicalIndicators)arrayList[position]).bar.bob; //交易日期

            if (position == 0)
            {
                ((TechnicalIndicators)arrayList[position]).Action01 = "FIRST";
                ((TechnicalIndicators)arrayList[position]).Action02 = "FIRST";
            }
            else
            {
                //左侧包含(当前股票最高价大于前一天的最高价且当前股票最低价小于前一天的最低价）
                if (((TechnicalIndicators)arrayList[position]).bar.highChan >= ((TechnicalIndicators)arrayList[position - 1]).bar.highChan &&
                    ((TechnicalIndicators)arrayList[position]).bar.lowChan <= ((TechnicalIndicators)arrayList[position - 1]).bar.lowChan)
                {
                    //前一K线是上升K线
                    if (((TechnicalIndicators)arrayList[position - 1]).Action01 != null && ((TechnicalIndicators)arrayList[position - 1]).Action01.IndexOf("UP") >= 0)
                    {
                        ((TechnicalIndicators)arrayList[position]).Action01 = "UP,LEFT_CONTAIN";
                        ((TechnicalIndicators)arrayList[position]).bar.lowChan = ((TechnicalIndicators)arrayList[position - 1]).bar.lowChan;
                        ((TechnicalIndicators)arrayList[position - 1]).bar.highChan = ((TechnicalIndicators)arrayList[position]).bar.highChan;
                    }
                    //前一K线是下降K线
                    if (((TechnicalIndicators)arrayList[position - 1]).Action01 != null && ((TechnicalIndicators)arrayList[position - 1]).Action01.IndexOf("DOWN") >= 0)
                    {
                        ((TechnicalIndicators)arrayList[position]).Action01 = "DOWN,LEFT_CONTAIN";
                        ((TechnicalIndicators)arrayList[position]).bar.highChan = ((TechnicalIndicators)arrayList[position - 1]).bar.highChan;
                        ((TechnicalIndicators)arrayList[position - 1]).bar.lowChan = ((TechnicalIndicators)arrayList[position]).bar.lowChan;
                    }
                }
                //右侧包含(当前股票最高价小于前一天的最高价且当前股票最低价大于后一天的最低价）
                else if (((TechnicalIndicators)arrayList[position]).bar.highChan <= ((TechnicalIndicators)arrayList[position - 1]).bar.highChan &&
                    ((TechnicalIndicators)arrayList[position]).bar.lowChan >= ((TechnicalIndicators)arrayList[position - 1]).bar.lowChan)
                {
                    //前一K线是上升K线
                    if (((TechnicalIndicators)arrayList[position - 1]).Action01 != null && ((TechnicalIndicators)arrayList[position - 1]).Action01.IndexOf("UP") >= 0)
                    {
                        ((TechnicalIndicators)arrayList[position]).Action01 = "UP,RIGHT_CONTAIN";
                        ((TechnicalIndicators)arrayList[position]).bar.highChan = ((TechnicalIndicators)arrayList[position - 1]).bar.highChan;
                        ((TechnicalIndicators)arrayList[position - 1]).bar.lowChan = ((TechnicalIndicators)arrayList[position]).bar.lowChan;
                    }
                    //前一K线是下降K线
                    if (((TechnicalIndicators)arrayList[position - 1]).Action01 != null && ((TechnicalIndicators)arrayList[position - 1]).Action01.IndexOf("DOWN") >= 0)
                    {
                        ((TechnicalIndicators)arrayList[position]).Action01 = "DOWN,RIGHT_CONTAIN";
                        ((TechnicalIndicators)arrayList[position]).bar.lowChan = ((TechnicalIndicators)arrayList[position - 1]).bar.lowChan;
                        ((TechnicalIndicators)arrayList[position - 1]).bar.highChan = ((TechnicalIndicators)arrayList[position]).bar.highChan;
                    }
                }
                //上升K线(当前股票最高价大于前一天的最高价且当前股票最低价大于前一天的最低价）
                else if (((TechnicalIndicators)arrayList[position]).bar.highChan > ((TechnicalIndicators)arrayList[position - 1]).bar.highChan &&
                    ((TechnicalIndicators)arrayList[position]).bar.lowChan > ((TechnicalIndicators)arrayList[position - 1]).bar.lowChan)
                {
                    ((TechnicalIndicators)arrayList[position]).Action01 = "UP";
                }
                //下降K线(当前股票最高价小于前一天的最高价且当前股票最低价小于前一天的最低价）
                else if (((TechnicalIndicators)arrayList[position]).bar.highChan < ((TechnicalIndicators)arrayList[position - 1]).bar.highChan &&
                    ((TechnicalIndicators)arrayList[position]).bar.lowChan < ((TechnicalIndicators)arrayList[position - 1]).bar.lowChan)
                {
                    ((TechnicalIndicators)arrayList[position]).Action01 = "DOWN";
                }
            }
            if (position == arrayList.Count - 1)
            {
                ((TechnicalIndicators)arrayList[position]).Action02 = "END";
            }
        }
        //对包含K线进行处理
        public static void containChan(ref ArrayList arrayList)
        {
            String securityCode = "";  //证券代码
            DateTime transactionDate;     //交易日期

            for (int index = 1; index <= arrayList.Count - 1; index++)
            {
                securityCode = ((TechnicalIndicators)arrayList[index]).bar.symbol; //证券代码
                transactionDate = ((TechnicalIndicators)arrayList[index]).bar.bob; //交易日期
                //左包含K线
                if (((TechnicalIndicators)arrayList[index]).Action01 != null && ((TechnicalIndicators)arrayList[index]).Action01.IndexOf("LEFT_CONTAIN") >= 0)
                {
                    //从当前K线往后找非包含K线
                    for (int i = index - 1; i > 0; i--)
                    {
                        //上升K线(当前股票最高价大于前一天的最高价且当前股票最低价大于前一天的最低价）
                        if (((TechnicalIndicators)arrayList[index]).bar.high > ((TechnicalIndicators)arrayList[i]).bar.high &&
                            ((TechnicalIndicators)arrayList[index]).bar.low > ((TechnicalIndicators)arrayList[i]).bar.low)
                        {
                            ((TechnicalIndicators)arrayList[index]).Action01 = "UP,LEFT_CONTAIN";
                            break;
                        }
                        //下降K线(当前股票最高价小于前一天的最高价且当前股票最低价小于前一天的最低价）
                        else if (((TechnicalIndicators)arrayList[index]).bar.high < ((TechnicalIndicators)arrayList[i]).bar.high &&
                            ((TechnicalIndicators)arrayList[index]).bar.low < ((TechnicalIndicators)arrayList[i]).bar.low)
                        {
                            ((TechnicalIndicators)arrayList[index]).Action01 = "DOWN,LEFT_CONTAIN";
                            break;
                        }
                    }
                }
                //右包含K线
                if (((TechnicalIndicators)arrayList[index]).Action01 != null && ((TechnicalIndicators)arrayList[index]).Action01.IndexOf("RIGHT_CONTAIN") >= 0)
                {
                    int tempIndex = 0;
                    for (int j = index - 1; j > 0; j--)
                    {
                        //非包含
                        if (((TechnicalIndicators)arrayList[index]).Action01 != null && ((TechnicalIndicators)arrayList[j]).Action01.IndexOf("CONTAIN") < 0)
                        {
                            tempIndex = j;
                            break;
                        }
                    }
                    //从当前K线往后找非右包含K线
                    for (int i = index + 1; i <= arrayList.Count - 1; i++)
                    {
                        ////非右包含
                        //if (((TechnicalIndicators)arrayList[i]).Action01.IndexOf("CONTAIN") < 0)
                        //{
                        //    ((TechnicalIndicators)arrayList[index]).Action01 = ((TechnicalIndicators)arrayList[i]).Action01 + ",RIGHT_CONTAIN";
                        //    break;
                        //}
                        //从当前K线往后找非右包含K线
                        //上升K线(当前股票最高价大于前一天的最高价且当前股票最低价大于前一天的最低价）
                        if (((TechnicalIndicators)arrayList[tempIndex]).bar.high > ((TechnicalIndicators)arrayList[i]).bar.high &&
                            ((TechnicalIndicators)arrayList[tempIndex]).bar.low > ((TechnicalIndicators)arrayList[i]).bar.low)
                        {
                            ((TechnicalIndicators)arrayList[index]).Action01 = "DOWN,RIGHT_CONTAIN";
                            break;
                        }
                        //下降K线(当前股票最高价小于前一天的最高价且当前股票最低价小于前一天的最低价）
                        else if (((TechnicalIndicators)arrayList[tempIndex]).bar.high < ((TechnicalIndicators)arrayList[i]).bar.high &&
                            ((TechnicalIndicators)arrayList[tempIndex]).bar.low < ((TechnicalIndicators)arrayList[i]).bar.low)
                        {
                            ((TechnicalIndicators)arrayList[index]).Action01 = "UP,RIGHT_CONTAIN";
                            break;
                        }
                    }
                }
            }
            //for (int index = 1; index <= arrayList.Count - 1; index++)
            //{
            //    securityCode = ((TechnicalIndicators)arrayList[index]).bar.symbol; //证券代码
            //    transactionDate = ((TechnicalIndicators)arrayList[index]).bar.bob; //交易日期
            //    //包含K线
            //    if (((TechnicalIndicators)arrayList[index]).Action01.IndexOf("CONTAIN") >= 0)
            //    {
            //        int tempIndex = 0;
            //        for (int j = index - 1; j > 0; j--)
            //        {
            //            //非包含
            //            if (((TechnicalIndicators)arrayList[j]).Action01.IndexOf("CONTAIN") < 0)
            //            {
            //                tempIndex = j;
            //                break;
            //            }
            //        }
            //        //上升K线(当前股票最高价大于前一天的最高价且当前股票最低价大于前一天的最低价）
            //        if (((TechnicalIndicators)arrayList[tempIndex]).bar.highChan > ((TechnicalIndicators)arrayList[index]).bar.highChan &&
            //            ((TechnicalIndicators)arrayList[tempIndex]).bar.lowChan > ((TechnicalIndicators)arrayList[index]).bar.lowChan)
            //        {
            //            ((TechnicalIndicators)arrayList[index]).Action01 = "DOWN" + ((TechnicalIndicators)arrayList[index]).Action01.Replace("DOWN", "").Replace("UP", "");
            //        }
            //        //下降K线(当前股票最高价小于前一天的最高价且当前股票最低价小于前一天的最低价）
            //        else if (((TechnicalIndicators)arrayList[tempIndex]).bar.highChan < ((TechnicalIndicators)arrayList[index]).bar.highChan &&
            //            ((TechnicalIndicators)arrayList[tempIndex]).bar.lowChan < ((TechnicalIndicators)arrayList[index]).bar.lowChan)
            //        {
            //            ((TechnicalIndicators)arrayList[index]).Action01 = "UP" + ((TechnicalIndicators)arrayList[index]).Action01.Replace("DOWN", "").Replace("UP", "");                           
            //        }
            //    }
            //}

        }
        //计算缠论K线的顶和底
        //public static void topBottomChan(ref ArrayList arrayList)
        //{
        //    String securityCode = "";  //证券代码
        //    DateTime transactionDate;     //交易日期
        //    int nextIndex = 0;           //当前顶底对应的下一个
        //    int topBottomMinIndex = 0;   //顶顶之间或者底底之间的最低K线位置
        //    int topBottomMaxIndex = 0;   //顶顶之间或者底底之间的最高K线位置
        //    int firstIndex = 0;
        //    int secondIndex = 0;
        //    string topBottomString = "";
        //    int previousTopBottomIndex = 0;
        //    int lastTopBottomIndex = 0;

        //    for (int index = 1; index <= arrayList.Count - 1; index++)
        //    {
        //        if (index >= nextIndex)
        //        {
        //            securityCode = ((TechnicalIndicators)arrayList[index]).bar.symbol; //证券代码
        //            transactionDate = ((TechnicalIndicators)arrayList[index]).bar.bob; //交易日期
        //            //        //当前K线和前两根K线组成顶分型
        //            if (((TechnicalIndicators)arrayList[index - 1]).Action01 != null && 
        //                ((TechnicalIndicators)arrayList[index - 1]).Action01.IndexOf("UP") >= 0 &&
        //                ((TechnicalIndicators)arrayList[index]).Action01.IndexOf("DOWN") >= 0 &&
        //                ("".Equals(topBottomString) || "BOTTOM".Equals(topBottomString)))
        //            {
        //                topBottomString = "TOP";
        //                //从当前K线往后找下一个顶分型
        //                for (int i = index + 1; i <= arrayList.Count - 1; i++)
        //                {
        //                    //如果是顶分型
        //                    if (((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("UP") >= 0 &&
        //                        ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("DOWN") >= 0)
        //                    {
        //                        topBottomMinIndex = index;
        //                        topBottomMaxIndex = index;
        //                        //两个顶分型之间找最低K线
        //                        for (int k = index; k <= i - 1; k++)
        //                        {
        //                            //顶顶之间的最低K线位置
        //                            if (((TechnicalIndicators)arrayList[k]).bar.low <= ((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low)
        //                            {
        //                                topBottomMinIndex = k;
        //                            }
        //                            //顶顶之间的最高K线位置
        //                            if (((TechnicalIndicators)arrayList[k]).bar.high >= ((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high)
        //                            {
        //                                topBottomMaxIndex = k;
        //                            }
        //                        }
        //                        firstIndex = topBottomMinIndex;
        //                        //处理2000年10月13日的特殊情形
        //                        if (((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high > ((TechnicalIndicators)arrayList[index - 1]).bar.high)
        //                        {
        //                            firstIndex = index - 1;
        //                        }
        //                        else
        //                        {
        //                            //底顶分型之间找最高K线
        //                            for (int k = index - 1; k <= topBottomMinIndex; k++)
        //                            {
        //                                if (((TechnicalIndicators)arrayList[k]).bar.high > ((TechnicalIndicators)arrayList[firstIndex]).bar.high)
        //                                {
        //                                    firstIndex = k;
        //                                }
        //                            }
        //                        }
        //                        //处理1999年9月10日的特殊情形
        //                        if (((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high > ((TechnicalIndicators)arrayList[index - 1]).bar.high &&
        //                            ((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high > ((TechnicalIndicators)arrayList[i - 1]).bar.high)
        //                        {
        //                            secondIndex = i - 1;
        //                        }
        //                        else
        //                        {
        //                            secondIndex = topBottomMinIndex;
        //                            //底顶分型之间找最高K线
        //                            for (int k = topBottomMinIndex; k <= i - 1; k++)
        //                            {
        //                                if (((TechnicalIndicators)arrayList[k]).bar.high > ((TechnicalIndicators)arrayList[secondIndex]).bar.high)
        //                                {
        //                                    secondIndex = k;
        //                                }
        //                            }
        //                        }
        //                        //顶底之间无共用K线时特殊情况下的处理
        //                        bool condition = false;
        //                        if (((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low <= ((TechnicalIndicators)arrayList[previousTopBottomIndex]).bar.low && previousTopBottomIndex != 0)
        //                        {
        //                            condition = true;
        //                        }
        //                        //else
        //                        //{
        //                        //    //当前笔的长度
        //                        //    double firstBi = Math.Abs(DataConvert<double>.convertDataType((((TechnicalIndicators)arrayList[firstIndex]).bar.high - ((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low).ToString()));
        //                        //    //前一笔的长度
        //                        //    double secondBi = Math.Abs(DataConvert<double>.convertDataType((((TechnicalIndicators)arrayList[secondIndex]).bar.high - ((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low).ToString()));
        //                        //    double result = Math.Round(1 - secondBi / firstBi, 4);
        //                        //    if (result > 0.382)
        //                        //    {
        //                        //        condition = true;
        //                        //    }
        //                        //}
        //                        if ((topBottomMinIndex - (index - 1) < 4 && condition == false) && (i - 1 - topBottomMinIndex >= 4)
        //                            && ((TechnicalIndicators)arrayList[secondIndex]).bar.high > ((TechnicalIndicators)arrayList[firstIndex]).bar.high)
        //                        {
        //                            nextIndex = topBottomMinIndex + 1;
        //                            break;
        //                        }
        //                        //如果顶底之间至少有一根不共用的K线且第二个顶分型的K线最高   处理1998年01月13日虽然没有共用K线，但对前底构成了破坏
        //                        if (((topBottomMinIndex - (index - 1) >= 4 || condition == true) && (i - 1 - topBottomMinIndex >= 4))
        //                            && ((TechnicalIndicators)arrayList[index - 1]).bar.high >= ((TechnicalIndicators)arrayList[firstIndex]).bar.high &&
        //                        ((TechnicalIndicators)arrayList[i - 1]).bar.high >= ((TechnicalIndicators)arrayList[secondIndex]).bar.high)
        //                        {
        //                            ((TechnicalIndicators)arrayList[index - 1]).Action02 = "TOP";
        //                            previousTopBottomIndex = index - 1;
        //                            nextIndex = topBottomMinIndex + 1;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            //当前K线和前两根K线组成底分型
        //            if (((TechnicalIndicators)arrayList[index - 1]).Action01 != null && 
        //                ((TechnicalIndicators)arrayList[index - 1]).Action01.IndexOf("DOWN") >= 0 &&
        //                ((TechnicalIndicators)arrayList[index]).Action01.IndexOf("UP") >= 0 &&
        //                ("".Equals(topBottomString) || "TOP".Equals(topBottomString)))
        //            {
        //                topBottomString = "BOTTOM";
        //                //从当前K线往后找下一个底分型
        //                for (int i = index + 1; i <= arrayList.Count - 1; i++)
        //                {
        //                    //如果是底分型
        //                    if (((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("DOWN") >= 0 &&
        //                        ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("UP") >= 0)
        //                    {
        //                        topBottomMaxIndex = index;
        //                        topBottomMinIndex = index;
        //                        //两个底分型之间找最高K线
        //                        for (int k = index; k <= i - 1; k++)
        //                        {
        //                            if (((TechnicalIndicators)arrayList[k]).bar.high >= ((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high)
        //                            {
        //                                topBottomMaxIndex = k;

        //                            }
        //                            if (((TechnicalIndicators)arrayList[k]).bar.low <= ((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low)
        //                            {
        //                                topBottomMinIndex = k;

        //                            }
        //                        }
        //                        if (((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low < ((TechnicalIndicators)arrayList[index - 1]).bar.low)
        //                        {
        //                            firstIndex = index - 1;
        //                        }
        //                        else
        //                        {
        //                            firstIndex = topBottomMaxIndex;
        //                            //顶底分型之间找最低K线
        //                            for (int k = index - 1; k <= topBottomMaxIndex; k++)
        //                            {
        //                                if (((TechnicalIndicators)arrayList[k]).bar.low < ((TechnicalIndicators)arrayList[firstIndex]).bar.low)
        //                                {
        //                                    firstIndex = k;
        //                                }
        //                            }
        //                        }
        //                        if (((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low < ((TechnicalIndicators)arrayList[index - 1]).bar.low &&
        //                            ((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low < ((TechnicalIndicators)arrayList[i - 1]).bar.low)
        //                        {
        //                            secondIndex = i - 1;
        //                        }
        //                        else
        //                        {
        //                            secondIndex = topBottomMaxIndex;
        //                            //顶底分型之间找最低K线
        //                            for (int k = topBottomMaxIndex; k <= i - 1; k++)
        //                            {
        //                                if (((TechnicalIndicators)arrayList[k]).bar.low < ((TechnicalIndicators)arrayList[secondIndex]).bar.low)
        //                                {
        //                                    secondIndex = k;
        //                                }
        //                            }
        //                        }
        //                        //顶底之间无共用K线时特殊情况下的处理
        //                        bool condition = false;
        //                        if (((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high >= ((TechnicalIndicators)arrayList[previousTopBottomIndex]).bar.high && previousTopBottomIndex != 0)
        //                        {
        //                            condition = true;
        //                        }
        //                        //else
        //                        //{
        //                        //    //当前笔的长度
        //                        //    double firstBi = Math.Abs(DataConvert<double>.convertDataType((((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high - ((TechnicalIndicators)arrayList[firstIndex]).bar.low).ToString()));
        //                        //    //前一笔的长度
        //                        //    double secondBi = Math.Abs(DataConvert<double>.convertDataType((((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high - ((TechnicalIndicators)arrayList[secondIndex]).bar.low).ToString()));
        //                        //    double result = Math.Round(1 - secondBi / firstBi, 4);
        //                        //    if (result > 0.382)
        //                        //    {
        //                        //        condition = true;
        //                        //    }
        //                        //}
        //                        if ((topBottomMaxIndex - (index - 1) < 4 && condition == false) && (i - 1 - topBottomMaxIndex >= 4)
        //                            && ((TechnicalIndicators)arrayList[secondIndex]).bar.low < ((TechnicalIndicators)arrayList[firstIndex]).bar.low)
        //                        {
        //                            nextIndex = topBottomMaxIndex + 1;
        //                            break;
        //                        }
        //                        //如果顶底之间至少有一根不共用的K线且第二个底分型的K线最低
        //                        if (((topBottomMaxIndex - (index - 1) >= 4 || condition == true) && (i - 1 - topBottomMaxIndex >= 4))
        //                            && ((TechnicalIndicators)arrayList[index - 1]).bar.low <= ((TechnicalIndicators)arrayList[firstIndex]).bar.low &&
        //                        ((TechnicalIndicators)arrayList[i - 1]).bar.low <= ((TechnicalIndicators)arrayList[secondIndex]).bar.low)
        //                        {
        //                            ((TechnicalIndicators)arrayList[index - 1]).Action02 = "BOTTOM";
        //                            previousTopBottomIndex = index - 1;
        //                            nextIndex = topBottomMaxIndex + 1;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    //从后往前找到第一个底分型或顶分型
        //    for (int index = arrayList.Count - 1; index >= 1; index--)
        //    {
        //        if ("TOP".Equals(((TechnicalIndicators)arrayList[index]).Action02) ||
        //            "BOTTOM".Equals(((TechnicalIndicators)arrayList[index]).Action02))
        //        {
        //            lastTopBottomIndex = index;
        //            break;
        //        }
        //    }
        //    nextIndex = arrayList.Count - 1;
        //    for (int index = arrayList.Count - 1; index >= 1; index--)
        //    {
        //        if (index - 1 <= lastTopBottomIndex)
        //        {
        //            break;
        //        }
        //        if (index <= nextIndex)
        //        {
        //            securityCode = ((TechnicalIndicators)arrayList[index]).bar.symbol; //证券代码
        //            transactionDate = ((TechnicalIndicators)arrayList[index]).bar.bob; //交易日期
        //            //        //当前K线和前两根K线组成顶分型
        //            if (((TechnicalIndicators)arrayList[index - 1]).Action01.IndexOf("UP") >= 0 &&
        //                ((TechnicalIndicators)arrayList[index]).Action01.IndexOf("DOWN") >= 0 &&
        //                ("".Equals(topBottomString) || "BOTTOM".Equals(topBottomString)))
        //            {
        //                topBottomString = "TOP";
        //                //从当前K线往后找下一个顶分型
        //                for (int i = index - 1; i >= 1; i--)
        //                {
        //                    //如果是顶分型
        //                    if (((TechnicalIndicators)arrayList[i - 1]).Action01 != null &&
        //                        ((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("UP") >= 0 &&
        //                        ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("DOWN") >= 0)
        //                    {
        //                        topBottomMinIndex = index;
        //                        topBottomMaxIndex = index;
        //                        //两个顶分型之间找最低K线
        //                        for (int k = i - 1; k <= index - 1; k++)
        //                        {
        //                            //顶顶之间的最低K线位置
        //                            if (((TechnicalIndicators)arrayList[k]).bar.low <= ((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low)
        //                            {
        //                                topBottomMinIndex = k;
        //                            }
        //                            //顶顶之间的最高K线位置
        //                            if (((TechnicalIndicators)arrayList[k]).bar.high >= ((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high)
        //                            {
        //                                topBottomMaxIndex = k;
        //                            }
        //                        }
        //                        firstIndex = topBottomMinIndex;

        //                        //底顶分型之间找最高K线
        //                        for (int k = topBottomMinIndex; k <= index - 1; k++)
        //                        {
        //                            if (((TechnicalIndicators)arrayList[k]).bar.high > ((TechnicalIndicators)arrayList[firstIndex]).bar.high)
        //                            {
        //                                firstIndex = k;
        //                            }
        //                        }
        //                        //处理1999年9月10日的特殊情形
        //                        if (((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high > ((TechnicalIndicators)arrayList[index - 1]).bar.high &&
        //                            ((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high > ((TechnicalIndicators)arrayList[i - 1]).bar.high)
        //                        {
        //                            secondIndex = i - 1;
        //                        }
        //                        else
        //                        {
        //                            secondIndex = topBottomMinIndex;
        //                            //底顶分型之间找最高K线
        //                            for (int k = i - 1; k <= topBottomMinIndex; k++)
        //                            {
        //                                if (((TechnicalIndicators)arrayList[k]).bar.high > ((TechnicalIndicators)arrayList[secondIndex]).bar.high)
        //                                {
        //                                    secondIndex = k;
        //                                }
        //                            }
        //                        }
        //                        //顶底之间无共用K线时特殊情况下的处理
        //                        //bool condition = false;
        //                        //if (((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low <= ((TechnicalIndicators)arrayList[previousTopBottomIndex]).bar.low && previousTopBottomIndex != 0)
        //                        //{
        //                        //    condition = true;
        //                        //}
        //                        //if ((index - 1 - topBottomMinIndex < 4 && condition == false) && (topBottomMinIndex - (i - 1) >= 4)
        //                        //    && ((TechnicalIndicators)arrayList[secondIndex]).bar.high > ((TechnicalIndicators)arrayList[firstIndex]).bar.high)
        //                        //{
        //                        //    nextIndex = topBottomMinIndex + 1;
        //                        //    break;
        //                        //}
        //                        //如果顶底之间至少有一根不共用的K线且第二个顶分型的K线最高   处理1998年01月13日虽然没有共用K线，但对前底构成了破坏
        //                        if (((index - 1 - topBottomMinIndex >= 4) && (topBottomMinIndex - (i - 1) >= 4))
        //                            && ((TechnicalIndicators)arrayList[index - 1]).bar.high >= ((TechnicalIndicators)arrayList[firstIndex]).bar.high &&
        //                        ((TechnicalIndicators)arrayList[i - 1]).bar.high >= ((TechnicalIndicators)arrayList[secondIndex]).bar.high)
        //                        {
        //                            ((TechnicalIndicators)arrayList[index - 1]).Action02 = "TOP";
        //                            previousTopBottomIndex = index - 1;
        //                            nextIndex = topBottomMinIndex + 1;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            //当前K线和前两根K线组成底分型
        //            if (((TechnicalIndicators)arrayList[index - 1]).Action01.IndexOf("DOWN") >= 0 &&
        //                ((TechnicalIndicators)arrayList[index]).Action01.IndexOf("UP") >= 0 &&
        //                ("".Equals(topBottomString) || "TOP".Equals(topBottomString)))
        //            {
        //                topBottomString = "BOTTOM";
        //                //从当前K线往后找下一个底分型
        //                for (int i = index - 1; i >= 1; i--)
        //                {
        //                    //如果是底分型
        //                    if (((TechnicalIndicators)arrayList[i - 1]).Action01 != null && 
        //                        ((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("DOWN") >= 0 &&
        //                        ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("UP") >= 0)
        //                    {
        //                        topBottomMaxIndex = index;
        //                        topBottomMinIndex = index;
        //                        //两个底分型之间找最高K线
        //                        for (int k = i - 1; k <= index - 1; k++)
        //                        {
        //                            if (((TechnicalIndicators)arrayList[k]).bar.high >= ((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high)
        //                            {
        //                                topBottomMaxIndex = k;

        //                            }
        //                            if (((TechnicalIndicators)arrayList[k]).bar.low <= ((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low)
        //                            {
        //                                topBottomMinIndex = k;

        //                            }
        //                        }
        //                        firstIndex = topBottomMaxIndex;
        //                        //顶底分型之间找最低K线
        //                        for (int k = topBottomMaxIndex; k <= index - 1; k++)
        //                        {
        //                            if (((TechnicalIndicators)arrayList[k]).bar.low < ((TechnicalIndicators)arrayList[firstIndex]).bar.low)
        //                            {
        //                                firstIndex = k;
        //                            }
        //                        }
        //                        if (((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low < ((TechnicalIndicators)arrayList[index - 1]).bar.low &&
        //                            ((TechnicalIndicators)arrayList[topBottomMinIndex]).bar.low < ((TechnicalIndicators)arrayList[i - 1]).bar.low)
        //                        {
        //                            secondIndex = i - 1;
        //                        }
        //                        else
        //                        {
        //                            secondIndex = topBottomMaxIndex;
        //                            //顶底分型之间找最低K线
        //                            for (int k = i - 1; k <= topBottomMaxIndex; k++)
        //                            {
        //                                if (((TechnicalIndicators)arrayList[k]).bar.low < ((TechnicalIndicators)arrayList[secondIndex]).bar.low)
        //                                {
        //                                    secondIndex = k;
        //                                }
        //                            }
        //                        }
        //                        //顶底之间无共用K线时特殊情况下的处理
        //                        //bool condition = false;
        //                        //if (((TechnicalIndicators)arrayList[topBottomMaxIndex]).bar.high >= ((TechnicalIndicators)arrayList[previousTopBottomIndex]).bar.high && previousTopBottomIndex != 0)
        //                        //{
        //                        //    condition = true;
        //                        //}
        //                        //if ((index - 1 - topBottomMaxIndex < 4 && condition == false) && (topBottomMaxIndex - (i - 1) >= 4)
        //                        //    && ((TechnicalIndicators)arrayList[secondIndex]).bar.low < ((TechnicalIndicators)arrayList[firstIndex]).bar.low)
        //                        //{
        //                        //    nextIndex = topBottomMaxIndex + 1;
        //                        //    break;
        //                        //}
        //                        //如果顶底之间至少有一根不共用的K线且第二个底分型的K线最低
        //                        if (((index - 1 - topBottomMaxIndex >= 4) && (topBottomMaxIndex - (i - 1) >= 4))
        //                            && ((TechnicalIndicators)arrayList[index - 1]).bar.low <= ((TechnicalIndicators)arrayList[firstIndex]).bar.low &&
        //                        ((TechnicalIndicators)arrayList[i - 1]).bar.low <= ((TechnicalIndicators)arrayList[secondIndex]).bar.low)
        //                        {
        //                            ((TechnicalIndicators)arrayList[index - 1]).Action02 = "BOTTOM";
        //                            previousTopBottomIndex = index - 1;
        //                            nextIndex = topBottomMaxIndex + 1;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    //nextIndex = 0;
        //    //int currentTopBottomIndex = 0;
        //    ////遍历整个数组,调整顶底的位置
        //    //for (int index = 1; index <= arrayList.Count - 1; index++)
        //    //{
        //    //    if (index >= nextIndex)
        //    //    {
        //    //        //如果当前是顶，则往后找相邻的顶
        //    //        if ("TOP".Equals(((TechnicalIndicators)arrayList[index]).Action02))
        //    //        {
        //    //            //从当前K线往后找下一个顶分型
        //    //            for (int i = index + 1; i <= arrayList.Count - 1; i++)
        //    //            {
        //    //                //如果当前是顶，则往前往后找相邻的顶
        //    //                if ("TOP".Equals(((TechnicalIndicators)arrayList[i]).Action02))
        //    //                {
        //    //                    topBottomIndex = index;
        //    //                    currentTopBottomIndex = index;
        //    //                    //两个顶分型之间找最低K线
        //    //                    for (int k = index + 1; k <= i - 1; k++)
        //    //                    {
        //    //                        if (((TechnicalIndicators)arrayList[k]).bar.low <= ((TechnicalIndicators)arrayList[topBottomIndex]).bar.low)
        //    //                        {
        //    //                            topBottomIndex = k;
        //    //                        }
        //    //                        //如果当前是底
        //    //                        if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[k]).Action02))
        //    //                        {
        //    //                            currentTopBottomIndex = k;
        //    //                        }
        //    //                    }
        //    //                    if (topBottomIndex != currentTopBottomIndex)
        //    //                    {
        //    //                        ((TechnicalIndicators)arrayList[currentTopBottomIndex]).Action02 = "";
        //    //                        ((TechnicalIndicators)arrayList[topBottomIndex]).Action02 = "BOTTOM";
        //    //                    }
        //    //                    nextIndex = topBottomIndex;
        //    //                    break;
        //    //                }
        //    //            }
        //    //        }
        //    //        //如果当前是底，则往后找相邻的底
        //    //        if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[index]).Action02))
        //    //        {
        //    //            //从当前K线往后找下一个底分型
        //    //            for (int i = index + 1; i <= arrayList.Count - 1; i++)
        //    //            {
        //    //                //如果当前是底，则往前往后找相邻的底
        //    //                if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[i]).Action02))
        //    //                {
        //    //                    topBottomIndex = index;
        //    //                    currentTopBottomIndex = index;
        //    //                    //两个底分型之间找最高K线
        //    //                    for (int k = index + 1; k <= i - 1; k++)
        //    //                    {
        //    //                        if (((TechnicalIndicators)arrayList[k]).bar.high >= ((TechnicalIndicators)arrayList[topBottomIndex]).bar.high)
        //    //                        {
        //    //                            topBottomIndex = k;
        //    //                        }
        //    //                        //如果当前是顶
        //    //                        if ("TOP".Equals(((TechnicalIndicators)arrayList[k]).Action02))
        //    //                        {
        //    //                            currentTopBottomIndex = k;
        //    //                        }
        //    //                    }
        //    //                    if (topBottomIndex != currentTopBottomIndex)
        //    //                    {
        //    //                        ((TechnicalIndicators)arrayList[currentTopBottomIndex]).Action02 = "";
        //    //                        ((TechnicalIndicators)arrayList[topBottomIndex]).Action02 = "TOP";
        //    //                    }
        //    //                    nextIndex = topBottomIndex;
        //    //                    break;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}

        //    //for (int index = 1; index <= arrayList.Count - 1; index++)
        //    //{
        //    //    if (index >= nextIndex)
        //    //    {
        //    //        securityCode = ((TechnicalIndicators)arrayList[index]).bar.symbol; //证券代码
        //    //        transactionDate = ((TechnicalIndicators)arrayList[index]).bar.bob; //交易日期
        //    //        //当前K线和前两根K线组成顶分型
        //    //        if (((TechnicalIndicators)arrayList[index - 1]).Action01.IndexOf("UP") >= 0 &&
        //    //            ((TechnicalIndicators)arrayList[index]).Action01.IndexOf("DOWN") >= 0)
        //    //        {
        //    //            //往后找到第一个底分型
        //    //            for (int i = index + 3; i <= arrayList.Count - 1; i++)
        //    //            {
        //    //                //如果K线组合为上升K线、下降K线(顶)时
        //    //                if (((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("UP") >= 0 &&
        //    //                    ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("DOWN") >= 0)
        //    //                {
        //    //                    //后顶的值大于当前顶的值,则当前顶不成立
        //    //                    if (((TechnicalIndicators)arrayList[i - 1]).bar.high > ((TechnicalIndicators)arrayList[index - 1]).bar.high)
        //    //                    {
        //    //                        nextIndex = i;
        //    //                        break;
        //    //                    }
        //    //                }
        //    //                //当前底分型后面的第一个底分型
        //    //                if (((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("DOWN") >= 0 &&
        //    //                    ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("UP") >= 0)
        //    //                {
        //    //                    //底的最低值小于顶的最低值
        //    //                    if (((TechnicalIndicators)arrayList[i - 1]).bar.low < ((TechnicalIndicators)arrayList[index - 1]).bar.low)
        //    //                    {
        //    //                        ((TechnicalIndicators)arrayList[index - 1]).Action02 = "TOP";
        //    //                        nextIndex = i;
        //    //                        break;
        //    //                    }
        //    //                }
        //    //                //最后的一笔先落定，后面如果再破坏的话再重新落定
        //    //                if (i == arrayList.Count - 1)
        //    //                {
        //    //                    ((TechnicalIndicators)arrayList[index - 1]).Action02 = "TOP";
        //    //                    nextIndex = i;
        //    //                    break;
        //    //                }
        //    //            }
        //    //        }
        //    //        //当前K线和前两根K线组成底分型
        //    //        if (((TechnicalIndicators)arrayList[index - 1]).Action01.IndexOf("DOWN") >= 0 &&
        //    //            ((TechnicalIndicators)arrayList[index]).Action01.IndexOf("UP") >= 0)
        //    //        {
        //    //            //往后找到第一个顶分型
        //    //            for (int i = index + 3; i <= arrayList.Count - 2; i++)
        //    //            {
        //    //                //当前底分型后面的第一个底分型
        //    //                if (((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("DOWN") >= 0 &&
        //    //                    ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("UP") >= 0)
        //    //                {
        //    //                    //底的最低值小于顶的最低值
        //    //                    if (((TechnicalIndicators)arrayList[i - 1]).bar.low < ((TechnicalIndicators)arrayList[index - 1]).bar.low)
        //    //                    {
        //    //                        nextIndex = i;
        //    //                        break;
        //    //                    }
        //    //                }
        //    //                //当前底分型后面的第一个顶分型
        //    //                if (((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("UP") >= 0 &&
        //    //                    ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("DOWN") >= 0)
        //    //                {
        //    //                    //顶的最高值大于底的最高值
        //    //                    if (((TechnicalIndicators)arrayList[index - 1]).bar.high < ((TechnicalIndicators)arrayList[i - 1]).bar.high)
        //    //                    {
        //    //                        ((TechnicalIndicators)arrayList[index - 1]).Action02 = "BOTTOM";
        //    //                        nextIndex = i;
        //    //                        break;
        //    //                    }
        //    //                }
        //    //                //最后的一笔先落定，后面如果再破坏的话再重新落定
        //    //                if (i == arrayList.Count - 1)
        //    //                {
        //    //                    ((TechnicalIndicators)arrayList[index - 1]).Action02 = "BOTTOM";
        //    //                    nextIndex = i;
        //    //                    break;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //for (int k = 1; k <= 2; k++)
        //    //{
        //    //    //遍历整个数组,调整顶的位置
        //    //    for (int index = 0; index <= arrayList.Count - 2; index++)
        //    //    {
        //    //        //如果当前是顶，则往前往后找相邻的底
        //    //        if ("TOP".Equals(((TechnicalIndicators)arrayList[index]).Action02))
        //    //        {
        //    //            int topIndexPrevious = index;
        //    //            bool topPreviousExist = false;
        //    //            //往后相邻的底
        //    //            for (int i = index - 1; i >= 0; i--)
        //    //            {
        //    //                //如果K线组合为上升K线、下降K线(顶)时
        //    //                if (((TechnicalIndicators)arrayList[i]).Action01.IndexOf("UP") >= 0 &&
        //    //                    ((TechnicalIndicators)arrayList[i + 1]).Action01.IndexOf("DOWN") >= 0)
        //    //                {

        //    //                    //如果中间有顶大于当前顶
        //    //                    if (((TechnicalIndicators)arrayList[i]).bar.high > ((TechnicalIndicators)arrayList[index]).bar.high)
        //    //                    {
        //    //                        //记录最高顶的位置
        //    //                        topIndexPrevious = i;
        //    //                    }
        //    //                }
        //    //                //如果是底
        //    //                if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[i]).Action02))
        //    //                {
        //    //                    //如果当前顶不是最高顶
        //    //                    if (topIndexPrevious != index)
        //    //                    {
        //    //                        //最高顶和后面一个相邻的底之间的K线数大于等于3
        //    //                        if (topIndexPrevious - i >= 3)
        //    //                        {
        //    //                            topPreviousExist = true;
        //    //                        }
        //    //                    }
        //    //                    break;
        //    //                }
        //    //            }
        //    //            int topIndexNext = index;
        //    //            bool topNextExist = false;
        //    //            for (int i = index + 1; i <= arrayList.Count - 2; i++)
        //    //            {
        //    //                //如果K线组合为上升K线、下降K线(顶)时
        //    //                if (((TechnicalIndicators)arrayList[i]).Action01.IndexOf("UP") >= 0 &&
        //    //                    ((TechnicalIndicators)arrayList[i + 1]).Action01.IndexOf("DOWN") >= 0)
        //    //                {
        //    //                    //如果中间有顶大于当前顶
        //    //                    if (((TechnicalIndicators)arrayList[i]).bar.high > ((TechnicalIndicators)arrayList[index]).bar.high)
        //    //                    {
        //    //                        //记录最高顶的位置
        //    //                        topIndexNext = i;
        //    //                    }
        //    //                }
        //    //                //如果是底
        //    //                if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[i]).Action02))
        //    //                {
        //    //                    //如果当前顶不是最高顶，
        //    //                    if (topIndexNext != index)
        //    //                    {
        //    //                        //最高顶和后面一个相邻的底之间的K线数大于等于3
        //    //                        if (i - topIndexNext >= 3)
        //    //                        {
        //    //                            topNextExist = true;
        //    //                        }
        //    //                    }
        //    //                    break;
        //    //                }
        //    //            }
        //    //            //如果往前有更高的顶且往后没有，则将顶调整到前面的顶
        //    //            if (topPreviousExist == true && topNextExist == false)
        //    //            {
        //    //                ((TechnicalIndicators)arrayList[topIndexPrevious]).Action02 = "TOP";
        //    //                ((TechnicalIndicators)arrayList[index]).Action02 = "";
        //    //            }
        //    //            //如果往后有更高的顶且往前没有，则将顶调整到后面的顶
        //    //            if (topPreviousExist == false && topNextExist == true)
        //    //            {
        //    //                ((TechnicalIndicators)arrayList[topIndexNext]).Action02 = "TOP";
        //    //                ((TechnicalIndicators)arrayList[index]).Action02 = "";
        //    //            }
        //    //            //如果往前和往后都有更高的顶，则将顶调整到最高的顶
        //    //            if (topPreviousExist == true && topNextExist == true)
        //    //            {
        //    //                //如果前顶更高，则调整到前顶
        //    //                if (((TechnicalIndicators)arrayList[topIndexPrevious]).bar.high > ((TechnicalIndicators)arrayList[topIndexNext]).bar.high)
        //    //                {
        //    //                    ((TechnicalIndicators)arrayList[topIndexPrevious]).Action02 = "TOP";
        //    //                    ((TechnicalIndicators)arrayList[index]).Action02 = "";
        //    //                }
        //    //                else
        //    //                {
        //    //                    ((TechnicalIndicators)arrayList[topIndexNext]).Action02 = "TOP";
        //    //                    ((TechnicalIndicators)arrayList[index]).Action02 = "";
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //    //遍历整个数组,调整底的位置
        //    //    for (int index = 0; index <= arrayList.Count - 2; index++)
        //    //    {
        //    //        //如果当前是底，则往前往后找相邻的顶
        //    //        if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[index]).Action02))
        //    //        {
        //    //            int bottomIndexPrevious = index;
        //    //            bool bottomPreviousExist = false;
        //    //            //往后找相邻的顶
        //    //            for (int i = index - 1; index >= 0; index--)
        //    //            {
        //    //                //如果K线组合为下降K线、上升K线(底)时
        //    //                if (((TechnicalIndicators)arrayList[i]).Action01.IndexOf("DOWN") >= 0 &&
        //    //                    ((TechnicalIndicators)arrayList[i + 1]).Action01.IndexOf("UP") >= 0)
        //    //                {
        //    //                    //如果中间有底小于当前底
        //    //                    if (((TechnicalIndicators)arrayList[i]).bar.low < ((TechnicalIndicators)arrayList[index]).bar.low)
        //    //                    {
        //    //                        //记录最低底的位置
        //    //                        bottomIndexPrevious = i;
        //    //                    }
        //    //                }
        //    //                //如果是顶
        //    //                if ("TOP".Equals(((TechnicalIndicators)arrayList[i]).Action02))
        //    //                {

        //    //                    //如果当前底不是最低底
        //    //                    if (bottomIndexPrevious != index)
        //    //                    {
        //    //                        //最低底和后面一个相邻的顶之间的K线数大于等于3
        //    //                        if (bottomIndexPrevious - i >= 3)
        //    //                        {
        //    //                            bottomPreviousExist = true;
        //    //                        }
        //    //                    }
        //    //                    break;
        //    //                }
        //    //            }
        //    //            int bottomIndexNext = index;
        //    //            bool bottomNextExist = false;

        //    //            for (int i = index + 1; index <= arrayList.Count - 2; index++)
        //    //            {
        //    //                //如果K线组合为下降K线、上升K线(底)时
        //    //                if (((TechnicalIndicators)arrayList[i]).Action01.IndexOf("DOWN") >= 0 &&
        //    //                    ((TechnicalIndicators)arrayList[i + 1]).Action01.IndexOf("UP") >= 0)
        //    //                {

        //    //                    //如果中间有底小于当前底
        //    //                    if (((TechnicalIndicators)arrayList[i]).bar.low < ((TechnicalIndicators)arrayList[index]).bar.low)
        //    //                    {

        //    //                        //记录最低底的位置
        //    //                        bottomIndexNext = i;
        //    //                    }
        //    //                }
        //    //                //如果是顶
        //    //                if ("TOP".Equals(((TechnicalIndicators)arrayList[i]).Action02))
        //    //                {
        //    //                    //如果当前底不是最低底
        //    //                    if (bottomIndexNext != index)
        //    //                    {
        //    //                        //最低底和后面一个相邻的顶之间的K线数大于等于3
        //    //                        if (i - bottomIndexNext >= 3)
        //    //                        {
        //    //                            bottomNextExist = true;
        //    //                        }
        //    //                    }
        //    //                    break;
        //    //                }
        //    //            }
        //    //            //如果往前有更低的底且往后没有，则将底调整到前面的底
        //    //            if (bottomPreviousExist == true && bottomNextExist == false)
        //    //            {
        //    //                ((TechnicalIndicators)arrayList[bottomIndexPrevious]).Action02 = "BOTTOM";
        //    //                ((TechnicalIndicators)arrayList[index]).Action02 = "";
        //    //            }
        //    //            //如果往后有更低的底且往前没有，则将底调整到后面的底
        //    //            if (bottomPreviousExist == false && bottomNextExist == true)
        //    //            {
        //    //                ((TechnicalIndicators)arrayList[bottomIndexNext]).Action02 = "BOTTOM";
        //    //                ((TechnicalIndicators)arrayList[index]).Action02 = "";

        //    //            }

        //    //            //如果往前和往后都有更低的底，则将底调整到最低的底
        //    //            if (bottomPreviousExist == true && bottomNextExist == true)
        //    //            {
        //    //                //如果前底更低，则调整到前底
        //    //                if (((TechnicalIndicators)arrayList[bottomIndexPrevious]).bar.low < ((TechnicalIndicators)arrayList[bottomIndexNext]).bar.low)
        //    //                {
        //    //                    ((TechnicalIndicators)arrayList[bottomIndexPrevious]).Action02 = "BOTTOM";
        //    //                    ((TechnicalIndicators)arrayList[index]).Action02 = "";

        //    //                }
        //    //                else
        //    //                {
        //    //                    ((TechnicalIndicators)arrayList[bottomIndexNext]).Action02 = "BOTTOM";
        //    //                    ((TechnicalIndicators)arrayList[index]).Action02 = "";
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //}
        ////计算缠论K线的顶和底
        //public static void topBottomChan(ref ArrayList arrayList)
        //{
        //    for (int i = 1; i <= arrayList.Count - 1; i++)
        //    {
        //        //当前K线和前两根K线组成顶分型
        //        if (((TechnicalIndicators)arrayList[i - 1]).Action01 != null &&
        //            ((TechnicalIndicators)arrayList[i]).Action01 != null &&
        //            ((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("UP") >= 0 &&
        //            ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("DOWN") >= 0)
        //        {
        //            ((TechnicalIndicators)arrayList[i - 1]).Action02 = "TOP";
        //        }
        //        //当前K线和前两根K线组成底分型
        //        if (((TechnicalIndicators)arrayList[i - 1]).Action01 != null &&
        //            ((TechnicalIndicators)arrayList[i]).Action01 != null &&
        //            ((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("DOWN") >= 0 &&
        //            ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("UP") >= 0)
        //        {
        //            ((TechnicalIndicators)arrayList[i - 1]).Action02 = "BOTTOM";
        //        }
        //    }
        //    for (int count = 1; count <= 2; count++)
        //    {

        //        //删除不符合缠论的顶和底
        //        for (int i = 1; i <= arrayList.Count - 1; i++)
        //        {
        //            string topStr = "";
        //            string bottomStr = "";

        //            if (count == 1)
        //            {
        //                topStr = "TOP";
        //                bottomStr = "BOTTOM";
        //            }
        //            if (count == 2)
        //            {
        //                topStr = "TOP,DELETE";
        //                bottomStr = "BOTTOM,DELETE";
        //            }

        //            //如果是顶分型
        //            if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
        //                ((TechnicalIndicators)arrayList[i]).Action02.IndexOf(topStr) >= 0)
        //            {
        //                //前一个顶分型的下标
        //                int previousTopIndex = 0;
        //                //后一个顶分型的下标
        //                int nextTopIndex = 0;
        //                //前一个底分型的下标
        //                int previousBottomIndex = 0;
        //                //后一个底分型的下标
        //                int nextBottomIndex = 0;
        //                //向前找前一个底分型
        //                for (int j = i - 1; j >= 0; j--)
        //                {
        //                    if (count == 1)
        //                    {
        //                        //如果找到底分型
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0)
        //                        {
        //                            previousBottomIndex = j;
        //                            break;
        //                        }
        //                    }
        //                    if (count == 2)
        //                    {
        //                        //如果找到底分型
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        ((TechnicalIndicators)arrayList[j]).Action02.Equals("BOTTOM"))
        //                        {
        //                            previousBottomIndex = j;
        //                            break;
        //                        }
        //                    }
        //                }
        //                //向前找前一个顶分型
        //                for (int j = i - 1; j >= 0; j--)
        //                {
        //                    if (count == 1)
        //                    {
        //                        //如果找到顶分型
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                            ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0)
        //                        {
        //                            previousTopIndex = j;
        //                            break;
        //                        }
        //                    }
        //                    if (count == 2)
        //                    {
        //                        //如果找到顶分型
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                            ((TechnicalIndicators)arrayList[j]).Action02.Equals("TOP"))
        //                        {
        //                            previousTopIndex = j;
        //                            break;
        //                        }
        //                    }
        //                }
        //                //向后找下一个底分型
        //                for (int j = i + 1; j < arrayList.Count; j++)
        //                {
        //                    if (count == 1)
        //                    {
        //                        //如果找到底分型
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        (((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0
        //                        || j == (arrayList.Count - 1)))
        //                        {
        //                            nextBottomIndex = j;
        //                            break;
        //                        }
        //                    }
        //                    if (count == 2)
        //                    {
        //                        //如果找到底分型
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        (((TechnicalIndicators)arrayList[j]).Action02.Equals("BOTTOM")
        //                        || j == (arrayList.Count - 1)))
        //                        {
        //                            nextBottomIndex = j;
        //                            break;
        //                        }
        //                    }
        //                }
        //                //向后找下一个顶分型
        //                for (int j = i + 1; j < arrayList.Count; j++)
        //                {
        //                    if (count == 1)
        //                    {
        //                        //如果找到顶分型
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        (((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0
        //                        || j == arrayList.Count - 1))
        //                        {
        //                            nextTopIndex = j;
        //                            break;
        //                        }
        //                    }
        //                    if (count == 2)
        //                    {
        //                        //如果找到顶分型
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        (((TechnicalIndicators)arrayList[j]).Action02.Equals("TOP")
        //                        || j == arrayList.Count - 1))
        //                        {
        //                            nextTopIndex = j;
        //                            break;
        //                        }
        //                    }
        //                }
        //                if (count == 1)
        //                {
        //                    //如果此顶点是前后顶点中最高点或者
        //                    if ((((TechnicalIndicators)arrayList[i]).bar.high > ((TechnicalIndicators)arrayList[previousTopIndex]).bar.high
        //                        && ((TechnicalIndicators)arrayList[i]).bar.high > ((TechnicalIndicators)arrayList[nextTopIndex]).bar.high)
        //                        || (i >= previousBottomIndex + 4 && nextBottomIndex >= i + 4))
        //                    {
        //                        ((TechnicalIndicators)arrayList[i]).Action02 = "TOP";
        //                    }
        //                    else
        //                    {
        //                        ((TechnicalIndicators)arrayList[i]).Action02 = "TOP,DELETE";
        //                    }
        //                }
        //                if (count == 2)
        //                {
        //                    //
        //                    if ((i >= previousBottomIndex + 4 && nextBottomIndex >= i + 4)
        //                         && (previousTopIndex < previousBottomIndex
        //                        && nextTopIndex > nextBottomIndex))
        //                    {
        //                        ((TechnicalIndicators)arrayList[i]).Action02 = "TOP";
        //                    }
        //                    else
        //                    {
        //                        ((TechnicalIndicators)arrayList[i]).Action02 = "TOP,DELETE";
        //                    }
        //                }
        //            }
        //            //如果是底分型
        //            if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
        //                ((TechnicalIndicators)arrayList[i]).Action02.IndexOf(bottomStr) >= 0)
        //            {
        //                //前一个顶分型的下标
        //                int previousTopIndex = 0;
        //                //后一个顶分型的下标
        //                int nextTopIndex = 0;
        //                //前一个底分型的下标
        //                int previousBottomIndex = 0;
        //                //后一个底分型的下标
        //                int nextBottomIndex = 0;
        //                //向前找前一个顶分型
        //                for (int j = i - 1; j >= 0; j--)
        //                {
        //                    if (count == 1)
        //                    {
        //                        //如果找到顶分型或者已经到第一根K线
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                            ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0)
        //                        {
        //                            previousTopIndex = j;
        //                            break;
        //                        }
        //                    }
        //                    if (count == 2)
        //                    {
        //                        //如果找到顶分型或者已经到第一根K线
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                            ((TechnicalIndicators)arrayList[j]).Action02.Equals("TOP"))
        //                        {
        //                            previousTopIndex = j;
        //                            break;
        //                        }
        //                    }
        //                }
        //                //向前找前一个底分型
        //                for (int j = i - 1; j >= 0; j--)
        //                {
        //                    if (count == 1)
        //                    {
        //                        //如果找到顶分型
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0)
        //                        {
        //                            previousBottomIndex = j;
        //                            break;
        //                        }
        //                    }
        //                    if (count == 2)
        //                    {
        //                        //如果找到顶分型
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        ((TechnicalIndicators)arrayList[j]).Action02.Equals("BOTTOM"))
        //                        {
        //                            previousBottomIndex = j;
        //                            break;
        //                        }
        //                    }
        //                }
        //                //向后找下一个顶分型
        //                for (int j = i + 1; j < arrayList.Count; j++)
        //                {
        //                    if (count == 1)
        //                    {
        //                        //如果找到底分型或者已经到最后一根K线
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        (((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0
        //                        || j == (arrayList.Count - 1)))
        //                        {
        //                            nextTopIndex = j;
        //                            break;
        //                        }
        //                    }
        //                    if (count == 2)
        //                    {
        //                        //如果找到底分型或者已经到最后一根K线
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        (((TechnicalIndicators)arrayList[j]).Action02.Equals("TOP")
        //                        || j == (arrayList.Count - 1)))
        //                        {
        //                            nextTopIndex = j;
        //                            break;
        //                        }
        //                    }
        //                }
        //                //向后找下一个底分型
        //                for (int j = i + 1; j < arrayList.Count; j++)
        //                {
        //                    if (count == 1)
        //                    {
        //                        //如果找到顶分型或者已经到最后一根K线
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        (((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0
        //                        || j == arrayList.Count - 1))
        //                        {
        //                            nextBottomIndex = j;
        //                            break;
        //                        }
        //                    }
        //                    if (count == 2)
        //                    {
        //                        //如果找到顶分型或者已经到最后一根K线
        //                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
        //                        (((TechnicalIndicators)arrayList[j]).Action02.Equals("BOTTOM")
        //                        || j == arrayList.Count - 1))
        //                        {
        //                            nextBottomIndex = j;
        //                            break;
        //                        }
        //                    }
        //                }
        //                if (count == 1)
        //                {
        //                    //如果此底是前后底中最低点或者
        //                    if ((((TechnicalIndicators)arrayList[i]).bar.low < ((TechnicalIndicators)arrayList[previousBottomIndex]).bar.low
        //                        && ((TechnicalIndicators)arrayList[i]).bar.low < ((TechnicalIndicators)arrayList[nextBottomIndex]).bar.low)
        //                        || (i >= previousTopIndex + 4 && nextTopIndex >= i + 4))
        //                    {
        //                        ((TechnicalIndicators)arrayList[i]).Action02 = "BOTTOM";
        //                    }
        //                    else
        //                    {
        //                        ((TechnicalIndicators)arrayList[i]).Action02 = "BOTTOM,DELETE";
        //                    }
        //                }
        //                if (count == 2)
        //                {
        //                    //如果此底是前后底中最低点或者
        //                    if ((i >= previousTopIndex + 4 && nextTopIndex >= i + 4)
        //                        && (previousTopIndex > previousBottomIndex
        //                        && nextTopIndex < nextBottomIndex))
        //                    {
        //                        ((TechnicalIndicators)arrayList[i]).Action02 = "BOTTOM";
        //                    }
        //                    else
        //                    {
        //                        ((TechnicalIndicators)arrayList[i]).Action02 = "BOTTOM,DELETE";
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    //删除带删除标志的顶和底
        //    for (int i = 1; i <= arrayList.Count - 1; i++)
        //    {
        //        if (((TechnicalIndicators)arrayList[i]).Action02 != null
        //            && ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("DELETE") >= 0)
        //        {
        //            ((TechnicalIndicators)arrayList[i]).Action02 = "";
        //        }
        //    }
        //}
        //计算缠论K线的顶和底
        public static void topBottomChan(ref ArrayList arrayList)
        {
            //根据K线关系初步设定缠论K线的顶和底
            upDownChan(ref arrayList);
            //标示不符合条件的笔的顶和底
            setDeleteFlagChan(ref arrayList);
            //第一次删除不符合条件的笔的顶和底
            deleteBiTopBottomChanFirst(ref arrayList);
            //第二次删除不符合条件的笔的顶和底
            deleteBiTopBottomChanSecond(ref arrayList);
            //第三次删除不符合条件的笔的顶和底
            deleteBiTopBottomChanFirst(ref arrayList);
            //设置第一个及最后一个笔形态
            setFirstLastChan(ref arrayList);
            //修改顶和底的位置，使得最高点为顶，最低点为底
            changeBiTopBottomChan(ref arrayList);
            lineSegmentChan(ref arrayList);
        }
        //根据K线关系初步设定缠论K线的顶和底
        public static void upDownChan(ref ArrayList arrayList)
        {
            for (int i = 1; i <= arrayList.Count - 1; i++)
            {
                //当前K线和前两根K线组成顶分型
                if (((TechnicalIndicators)arrayList[i - 1]).Action01 != null &&
                    ((TechnicalIndicators)arrayList[i]).Action01 != null &&
                    ((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("UP") >= 0 &&
                    ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("DOWN") >= 0)
                {
                    ((TechnicalIndicators)arrayList[i - 1]).Action02 = "TOP";
                }
                //当前K线和前两根K线组成底分型
                if (((TechnicalIndicators)arrayList[i - 1]).Action01 != null &&
                    ((TechnicalIndicators)arrayList[i]).Action01 != null &&
                    ((TechnicalIndicators)arrayList[i - 1]).Action01.IndexOf("DOWN") >= 0 &&
                    ((TechnicalIndicators)arrayList[i]).Action01.IndexOf("UP") >= 0)
                {
                    ((TechnicalIndicators)arrayList[i - 1]).Action02 = "BOTTOM";
                }
            }
        }
        //设置第一个及最后一个笔形态
        public static void setFirstLastChan(ref ArrayList arrayList)
        {
            for (int count = 1; count <= 2; count++)
            {
                //第一根K线的笔形态
                string firstStr = "";
                //第一个笔形态K线的索引
                int firstIndex = 0;
                //最后一根K线的笔形态
                for (int i = 1; i <= arrayList.Count - 1; i++)
                {
                    //当前K线和前两根K线组成顶分型
                    if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("TOP") >= 0)
                    {
                        if ("".Equals(firstStr))
                        {
                            firstIndex = i;
                            firstStr = "TOP";
                            break;
                        }
                    }
                    //当前K线和前两根K线组成底分型
                    if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("BOTTOM") >= 0)
                    {
                        if ("".Equals(firstStr))
                        {
                            firstIndex = i;
                            firstStr = "BOTTOM";
                            break;
                        }
                    }
                }

                //最低价索引
                int lowIndex = 0;
                //最高价索引
                int highIndex = 0;

                for (int i = 0; i <= firstIndex; i++)
                {
                    //如果当前K线的最低价小于 用于记录当前最低价K线的最低价
                    if (((TechnicalIndicators)arrayList[i]).bar.low <
                        ((TechnicalIndicators)arrayList[lowIndex]).bar.low)
                    {
                        lowIndex = i;
                    }
                    //如果当前K线的最高价小于 用于记录当前最高价K线的最高价
                    if (((TechnicalIndicators)arrayList[i]).bar.high >
                        ((TechnicalIndicators)arrayList[highIndex]).bar.high)
                    {
                        highIndex = i;
                    }
                }
                //如果第一个是顶分型
                if ("TOP".Equals(firstStr))
                {
                    //如果第一个顶分型 是范围内最高的顶，则可以将最低的K线设置为底分型
                    if (firstIndex == highIndex)
                    {
                        if (firstIndex - lowIndex >= 4)
                        {
                            ((TechnicalIndicators)arrayList[lowIndex]).Action02 = "BOTTOM";
                        }
                    }
                    else
                    {
                        ((TechnicalIndicators)arrayList[firstIndex]).Action02 = "";
                        ((TechnicalIndicators)arrayList[highIndex]).Action02 = "TOP";
                    }
                }
                //如果第一个分型是底分型
                if ("BOTTOM".Equals(firstStr))
                {
                    //如果第一个底分型 是范围内最低的底，则可以将最高的K线设置为顶分型
                    if (firstIndex == lowIndex)
                    {
                        if (firstIndex - highIndex >= 4)
                        {
                            ((TechnicalIndicators)arrayList[highIndex]).Action02 = "TOP";
                        }
                    }
                    else
                    {
                        ((TechnicalIndicators)arrayList[firstIndex]).Action02 = "";
                        ((TechnicalIndicators)arrayList[lowIndex]).Action02 = "BOTTOM";
                    }
                }

                //最后一根K线的笔形态
                string lastStr = "";
                //最后笔形态K线的索引
                int lastIndex = 0;
                for (int i = arrayList.Count - 1; i >= 1; i--)
                {
                    //当前K线和前两根K线组成顶分型
                    if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("TOP") >= 0)
                    {

                        if ("".Equals(lastStr))
                        {
                            lastIndex = i;
                            lastStr = "TOP";
                            break;
                        }
                    }
                    //当前K线和前两根K线组成底分型
                    if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("BOTTOM") >= 0)
                    {
                        if ("".Equals(lastStr))
                        {
                            lastIndex = i;
                            lastStr = "BOTTOM";
                            break;
                        }
                    }
                }

                //最低价索引
                lowIndex = arrayList.Count - 1;
                //最高价索引
                highIndex = arrayList.Count - 1;

                for (int i = arrayList.Count - 1; i >= lastIndex; i--)
                {
                    //如果当前K线的最低价小于 用于记录当前最低价K线的最低价
                    if (((TechnicalIndicators)arrayList[i]).bar.low <
                        ((TechnicalIndicators)arrayList[lowIndex]).bar.low)
                    {
                        lowIndex = i;
                    }
                    //如果当前K线的最高价小于 用于记录当前最高价K线的最高价
                    if (((TechnicalIndicators)arrayList[i]).bar.high >
                        ((TechnicalIndicators)arrayList[highIndex]).bar.high)
                    {
                        highIndex = i;
                    }
                }
                //如果最后一个分型是顶分型
                if ("TOP".Equals(lastStr))
                {
                    //如果最后一个顶分型 是范围内最高的顶，则可以将最低的K线设置为底分型
                    if (lastIndex == highIndex)
                    {
                        if (lowIndex - lastIndex >= 4)
                        {
                            ((TechnicalIndicators)arrayList[lowIndex]).Action02 = "BOTTOM";
                        }
                    }
                    else
                    {
                        ((TechnicalIndicators)arrayList[lastIndex]).Action02 = "";
                        ((TechnicalIndicators)arrayList[highIndex]).Action02 = "TOP";
                    }
                }
                //如果最后一个是底分型
                if ("BOTTOM".Equals(lastStr))
                {
                    //如果最后一个底分型 是范围内最低的底，则可以将最高的K线设置为顶分型
                    if (lastIndex == lowIndex)
                    {
                        if (highIndex - lastIndex >= 4)
                        {
                            ((TechnicalIndicators)arrayList[highIndex]).Action02 = "TOP";
                        }
                    }
                    else
                    {
                        ((TechnicalIndicators)arrayList[lastIndex]).Action02 = "";
                        ((TechnicalIndicators)arrayList[lowIndex]).Action02 = "BOTTOM";
                    }
                }
            }
        }
        //第一次删除不符合条件的笔的顶和底
        public static void deleteBiTopBottomChanFirst(ref ArrayList arrayList)
        {
            int count = 1;
            for (int index = 1; index <= count; index++)
            {
                //删除不符合条件的笔的顶和底
                for (int i = 0; i <= arrayList.Count - 1; i++)
                {
                    //如果是顶分型
                    if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[i]).Action02.Equals("TOP"))
                    {
                        string topBottomStr = "";
                        int nextTopIndex = 0;
                        //后一个底分型的下标
                        int nextBottomIndex = 0;
                        //向后找下一个分型
                        for (int j = i + 1; j < arrayList.Count; j++)
                        {
                            //如果找到底分型
                            if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                            ((TechnicalIndicators)arrayList[j]).Action02.Equals("BOTTOM"))
                            {
                                if ("".Equals(topBottomStr))
                                {
                                    topBottomStr = "BOTTOM";
                                }
                                nextBottomIndex = j;
                                break;
                            }
                            //如果找到顶分型
                            if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                            ((TechnicalIndicators)arrayList[j]).Action02.Equals("TOP"))
                            {
                                if ("".Equals(topBottomStr))
                                {
                                    topBottomStr = "TOP";
                                }
                                nextTopIndex = j;
                                break;
                            }
                        }

                        //如果第一个找到的笔分型是底分型，则将顶底之间的带删除标记的顶和底都删除
                        if ("BOTTOM".Equals(topBottomStr))
                        {
                            for (int j = i + 1; j <= nextBottomIndex - 1; j++)
                            {
                                if (((TechnicalIndicators)arrayList[j]).Action02 != null
                                    && ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("DELETE") >= 0)
                                {
                                    ((TechnicalIndicators)arrayList[j]).Action02 = "";
                                }
                            }
                        }
                        //如果第一个找到的笔分型是底分型,则看两个底之间是否有满足笔顶分型的K线，如果没有则删除较高的底，保留较低的底。
                        if ("TOP".Equals(topBottomStr))
                        {
                            //是否删除标志
                            bool deleteFlag = true;
                            for (int j = i + 1; j <= nextTopIndex - 1; j++)
                            {
                                if (((TechnicalIndicators)arrayList[j]).Action02 != null
                                    && ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0)
                                {
                                    //有满足笔顶分型的K线
                                    if (j >= i + 4 && nextTopIndex >= j + 4)
                                    {
                                        deleteFlag = false;
                                        ((TechnicalIndicators)arrayList[j]).Action02 = "BOTTOM";
                                        break;
                                    }
                                }
                            }
                            if (deleteFlag)
                            {
                                count++;
                                if (((TechnicalIndicators)arrayList[nextTopIndex]).bar.high > ((TechnicalIndicators)arrayList[i]).bar.high)
                                {
                                    ((TechnicalIndicators)arrayList[i]).Action02 = "TOP,DELETE";
                                }
                                else
                                {
                                    ((TechnicalIndicators)arrayList[nextTopIndex]).Action02 = "TOP,DELETE";
                                }
                            }
                        }
                    }
                    //如果是底分型
                    if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[i]).Action02.Equals("BOTTOM"))
                    {
                        string topBottomStr = "";
                        //后一个顶分型的下标
                        int nextTopIndex = 0;
                        //后一个底分型的下标
                        int nextBottomIndex = 0;

                        //向后找下一个顶分型
                        for (int j = i + 1; j < arrayList.Count; j++)
                        {
                            //如果找到顶分型
                            if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                            ((TechnicalIndicators)arrayList[j]).Action02.Equals("BOTTOM"))
                            {
                                if ("".Equals(topBottomStr))
                                {
                                    topBottomStr = "BOTTOM";
                                }
                                nextBottomIndex = j;
                                break;
                            }
                            //如果找到底分型
                            if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                            ((TechnicalIndicators)arrayList[j]).Action02.Equals("TOP"))
                            {
                                if ("".Equals(topBottomStr))
                                {
                                    topBottomStr = "TOP";
                                }
                                nextTopIndex = j;
                                break;
                            }
                        }

                        //如果第一个找到的笔分型是顶分型，则将顶底之间的带删除标记的顶和底都删除
                        if ("TOP".Equals(topBottomStr))
                        {
                            for (int j = i + 1; j <= nextTopIndex - 1; j++)
                            {
                                if (((TechnicalIndicators)arrayList[j]).Action02 != null
                                    && ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("DELETE") >= 0)
                                {
                                    ((TechnicalIndicators)arrayList[j]).Action02 = "";
                                }
                            }
                        }
                        //如果第一个找到的笔分型是底分型，,则看两个顶之间是否有满足笔底分型的K线，如果没有则删除较低的顶，保留较高的顶。
                        if ("BOTTOM".Equals(topBottomStr))
                        {
                            //是否删除标志
                            bool deleteFlag = true;
                            for (int j = i + 1; j <= nextBottomIndex - 1; j++)
                            {
                                if (((TechnicalIndicators)arrayList[j]).Action02 != null
                                    && ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0)
                                {
                                    //有满足笔底分型的K线
                                    if (j >= i + 4 && nextBottomIndex >= j + 4)
                                    {
                                        deleteFlag = false;
                                        ((TechnicalIndicators)arrayList[j]).Action02 = "TOP";
                                        break;
                                    }
                                }
                            }
                            if (deleteFlag)
                            {
                                count++;
                                if (((TechnicalIndicators)arrayList[nextBottomIndex]).bar.low < ((TechnicalIndicators)arrayList[i]).bar.low)
                                {
                                    ((TechnicalIndicators)arrayList[i]).Action02 = "BOTTOM,DELETE";
                                }
                                else
                                {
                                    ((TechnicalIndicators)arrayList[nextBottomIndex]).Action02 = "BOTTOM,DELETE";
                                }
                            }
                        }
                    }
                }
            }
            //删除带删除标志的顶和底
            for (int i = 1; i <= arrayList.Count - 2; i++)
            {
                if (((TechnicalIndicators)arrayList[i]).Action02 != null
                    && ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("DELETE") >= 0)
                {
                    ((TechnicalIndicators)arrayList[i]).Action02 = "";
                }
            }
        }
        //第二次删除不符合条件的笔的顶和底
        //(删除顶底之间不够4根K线的顶或底(由于当前顶与前面的底或后面的底不够4根K线，但当前顶大于两边的顶。反正底也一样))
        public static void deleteBiTopBottomChanSecond(ref ArrayList arrayList)
        {
            //删除不符合条件的笔的顶和底
            for (int i = 0; i <= arrayList.Count - 1; i++)
            {
                //如果是顶分型
                if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                    ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("TOP") >= 0)
                {
                    //后一个底分型的下标
                    int nextBottomIndex = 0;
                    //向后找下一个分型
                    for (int j = i + 1; j < arrayList.Count; j++)
                    {
                        //如果找到底分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0)
                        {

                            nextBottomIndex = j;
                            break;
                        }
                    }
                    //后一个底分型的下标
                    int previousBottomIndex = 0;
                    //向前找下一个分型                        
                    for (int j = i - 1; j >= 0; j--)
                    {
                        //如果找到底分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0)
                        {
                            previousBottomIndex = j;
                            break;
                        }
                    }
                    //如果从当前顶到前一个底或者后一个底的K线其中之一不满足分型K线数最低要求，则将当前顶设置为删除标记。
                    if (i - previousBottomIndex < 4 || nextBottomIndex - i < 4)
                    {
                        ((TechnicalIndicators)arrayList[i]).Action02 = "TOP,DELETE";
                    }
                }
                //如果是底分型
                if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                    ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("BOTTOM") >= 0)
                {
                    //后一个顶分型的下标
                    int nextTopIndex = 0;
                    //向后找下一个顶分型
                    for (int j = i + 1; j < arrayList.Count; j++)
                    {
                        //如果找到顶分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0)
                        {
                            nextTopIndex = j;
                            break;
                        }
                    }
                    //后一个底分型的下标
                    int previousTopIndex = 0;
                    //向前找下一个顶分型
                    for (int j = i - 1; j >= 0; j--)
                    {

                        //如果找到顶分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0)
                        {
                            previousTopIndex = j;
                            break;
                        }
                    }
                    //如果从当前底到前一个顶或者后一个顶的K线其中之一不满足分型K线数最低要求，则将当前底设置为删除标记。
                    if (i - previousTopIndex < 4 || nextTopIndex - i < 4)
                    {
                        ((TechnicalIndicators)arrayList[i]).Action02 = "BOTTOM,DELETE";
                    }
                }
            }

        }
        //修改顶和底的位置，使得最高点为顶，最低点为底
        public static void changeBiTopBottomChan(ref ArrayList arrayList)
        {
            //删除不符合条件的笔的顶和底
            for (int i = 0; i <= arrayList.Count - 1; i++)
            {
                //如果是顶分型
                if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                    ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("TOP") >= 0)
                {
                    //后一个底分型的下标
                    int nextBottomIndex = 0;
                    //向后找下一个分型
                    for (int j = i + 1; j < arrayList.Count; j++)
                    {
                        //如果找到底分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0)
                        {

                            nextBottomIndex = j;
                            break;
                        }
                    }
                    //后一个底分型的下标
                    int previousBottomIndex = 0;
                    //向前找下一个分型                        
                    for (int j = i - 1; j >= 0; j--)
                    {
                        //如果找到底分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0)
                        {
                            previousBottomIndex = j;
                            break;
                        }
                    }
                    //最高点的索引
                    int maxIndex = i;
                    //找两个底之间的最高点
                    for (int j = previousBottomIndex + 1; j < nextBottomIndex; j++)
                    {
                        //找两个底之间的最高点
                        if (((TechnicalIndicators)arrayList[j]).bar.high >
                            ((TechnicalIndicators)arrayList[maxIndex]).bar.high)
                        {
                            maxIndex = j;
                        }
                    }
                    ((TechnicalIndicators)arrayList[i]).Action02 = "";
                    ((TechnicalIndicators)arrayList[maxIndex]).Action02 = "TOP";
                }
                //如果是底分型
                if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                    ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("BOTTOM") >= 0)
                {
                    //后一个顶分型的下标
                    int nextTopIndex = 0;
                    //向后找下一个顶分型
                    for (int j = i + 1; j < arrayList.Count; j++)
                    {
                        //如果找到顶分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0)
                        {
                            nextTopIndex = j;
                            break;
                        }
                    }
                    //后一个底分型的下标
                    int previousTopIndex = 0;
                    //向前找下一个顶分型
                    for (int j = i - 1; j >= 0; j--)
                    {

                        //如果找到顶分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0)
                        {
                            previousTopIndex = j;
                            break;
                        }
                    }
                    //最低点的索引
                    int minIndex = i;
                    //找两个顶之间的最低点
                    for (int j = previousTopIndex + 1; j < nextTopIndex; j++)
                    {
                        //找两个底之间的最高点
                        if (((TechnicalIndicators)arrayList[j]).bar.low <
                            ((TechnicalIndicators)arrayList[minIndex]).bar.low)
                        {
                            minIndex = j;
                        }
                    }
                    ((TechnicalIndicators)arrayList[i]).Action02 = "";
                    ((TechnicalIndicators)arrayList[minIndex]).Action02 = "BOTTOM";
                }
            }

        }
        //标示不符合条件的笔的顶和底
        public static void setDeleteFlagChan(ref ArrayList arrayList)
        {
            //标示不符合条件的笔的顶和底
            for (int i = 0; i <= arrayList.Count - 1; i++)
            {
                //如果是顶分型
                if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                    ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("TOP") >= 0)
                {
                    //前一个顶分型的下标
                    int previousTopIndex = 0;
                    //后一个顶分型的下标
                    int nextTopIndex = 0;
                    //前一个底分型的下标
                    int previousBottomIndex = 0;
                    //后一个底分型的下标
                    int nextBottomIndex = 0;
                    //向前找前一个底分型
                    for (int j = i - 1; j >= 0; j--)
                    {
                        //如果找到底分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0)
                        {
                            previousBottomIndex = j;
                            break;
                        }

                    }
                    //向前找前一个顶分型
                    for (int j = i - 1; j >= 0; j--)
                    {
                        //如果找到顶分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                            ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0)
                        {
                            previousTopIndex = j;
                            break;
                        }
                    }
                    //向后找下一个底分型
                    for (int j = i + 1; j < arrayList.Count; j++)
                    {
                        //如果找到底分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        (((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0
                        || j == (arrayList.Count - 2)))
                        {
                            nextBottomIndex = j;
                            break;
                        }
                    }
                    //向后找下一个顶分型
                    for (int j = i + 1; j < arrayList.Count; j++)
                    {
                        //如果找到顶分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        (((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0
                        || j == arrayList.Count - 2))
                        {
                            nextTopIndex = j;
                            break;
                        }
                    }
                    //如果此顶点是前后顶点中最高点或者 K线满足笔的K线数量要求
                    if ((((TechnicalIndicators)arrayList[i]).bar.high > ((TechnicalIndicators)arrayList[previousTopIndex]).bar.high
                        && ((TechnicalIndicators)arrayList[i]).bar.high > ((TechnicalIndicators)arrayList[nextTopIndex]).bar.high)
                        || (i >= previousBottomIndex + 4 && nextBottomIndex >= i + 4))
                    {
                        ((TechnicalIndicators)arrayList[i]).Action02 = "TOP";
                    }
                    else
                    {
                        ((TechnicalIndicators)arrayList[i]).Action02 = "TOP,DELETE";
                    }
                }
                //如果是底分型
                if (((TechnicalIndicators)arrayList[i]).Action02 != null &&
                    ((TechnicalIndicators)arrayList[i]).Action02.IndexOf("BOTTOM") >= 0)
                {
                    //前一个顶分型的下标
                    int previousTopIndex = 0;
                    //后一个顶分型的下标
                    int nextTopIndex = 0;
                    //前一个底分型的下标
                    int previousBottomIndex = 0;
                    //后一个底分型的下标
                    int nextBottomIndex = 0;
                    //向前找前一个顶分型
                    for (int j = i - 1; j >= 0; j--)
                    {
                        //如果找到顶分型或者已经到第一根K线
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                            ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0)
                        {
                            previousTopIndex = j;
                            break;
                        }
                    }
                    //向前找前一个底分型
                    for (int j = i - 1; j >= 0; j--)
                    {
                        //如果找到顶分型
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        ((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0)
                        {
                            previousBottomIndex = j;
                            break;
                        }
                    }
                    //向后找下一个顶分型
                    for (int j = i + 1; j < arrayList.Count; j++)
                    {
                        //如果找到底分型或者已经到最后一根K线
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        (((TechnicalIndicators)arrayList[j]).Action02.IndexOf("TOP") >= 0
                        || j == (arrayList.Count - 2)))
                        {
                            nextTopIndex = j;
                            break;
                        }
                    }
                    //向后找下一个底分型
                    for (int j = i + 1; j < arrayList.Count; j++)
                    {
                        //如果找到顶分型或者已经到最后一根K线
                        if (((TechnicalIndicators)arrayList[j]).Action02 != null &&
                        (((TechnicalIndicators)arrayList[j]).Action02.IndexOf("BOTTOM") >= 0
                        || j == arrayList.Count - 2))
                        {
                            nextBottomIndex = j;
                            break;
                        }
                    }
                    //如果此底是前后底中最低点或者 K线满足笔的K线数量要求
                    if ((((TechnicalIndicators)arrayList[i]).bar.low < ((TechnicalIndicators)arrayList[previousBottomIndex]).bar.low
                        && ((TechnicalIndicators)arrayList[i]).bar.low < ((TechnicalIndicators)arrayList[nextBottomIndex]).bar.low)
                        || (i >= previousTopIndex + 4 && nextTopIndex >= i + 4))
                    {
                        ((TechnicalIndicators)arrayList[i]).Action02 = "BOTTOM";
                    }
                    else
                    {
                        ((TechnicalIndicators)arrayList[i]).Action02 = "BOTTOM,DELETE";
                    }
                }
            }
        }
        //计算缠论K线的线段
        public static void lineSegmentChan(ref ArrayList arrayList)
        {
            String securityCode = "";  //证券代码
            DateTime transactionDate;     //交易日期
            int nextIndex = 0;
            int topArrayIndex = 0;
            int bottomArrayIndex = 0;

            for (int index = 1; index <= arrayList.Count - 1; index++)
            {
                if (index >= nextIndex)
                {
                    securityCode = ((TechnicalIndicators)arrayList[index]).bar.symbol; //证券代码
                    transactionDate = ((TechnicalIndicators)arrayList[index]).bar.bob; //交易日期

                    //如个当前形态是顶分型
                    if ("TOP".Equals(((TechnicalIndicators)arrayList[index]).Action02))
                    {
                        topArrayIndex++;
                        nextIndex = index + 1;
                    }
                    //如个当前形态是底分型
                    if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[index]).Action02))
                    {
                        bottomArrayIndex++;
                        nextIndex = index + 1;
                    }
                }
            }
            int[] topArray = new int[topArrayIndex];
            int[] bottomArray = new int[bottomArrayIndex];
            topArrayIndex = 0;
            bottomArrayIndex = 0;
            nextIndex = 0;
            for (int index = 1; index <= arrayList.Count - 1; index++)
            {
                if (index >= nextIndex)
                {
                    //如个当前形态是顶分型
                    if ("TOP".Equals(((TechnicalIndicators)arrayList[index]).Action02))
                    {
                        topArray[topArrayIndex] = index;
                        topArrayIndex++;
                        nextIndex = index + 1;
                    }
                    //如个当前形态是底分型
                    if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[index]).Action02))
                    {
                        bottomArray[bottomArrayIndex] = index;
                        bottomArrayIndex++;
                        nextIndex = index + 1;
                    }
                }
            }
            int mixIndex = topArrayIndex;
            string topBottomFlag = "";
            if (bottomArrayIndex < topArrayIndex)
            {
                mixIndex = bottomArrayIndex;
            }

            for (int i = 1; i < mixIndex; i++)
            {

                //下降线段
                if (((TechnicalIndicators)arrayList[topArray[i]]).bar.high < ((TechnicalIndicators)arrayList[topArray[i - 1]]).bar.high &&
                    ((TechnicalIndicators)arrayList[bottomArray[i]]).bar.low < ((TechnicalIndicators)arrayList[bottomArray[i - 1]]).bar.low)
                {
                    if ("".Equals(topBottomFlag) || "BOTTOM".Equals(topBottomFlag))
                    {
                        ((TechnicalIndicators)arrayList[topArray[i - 1]]).Action03 = "TOP";
                        topBottomFlag = "TOP";
                    }
                }
                //上升线段
                if (((TechnicalIndicators)arrayList[topArray[i]]).bar.high > ((TechnicalIndicators)arrayList[topArray[i - 1]]).bar.high &&
                    ((TechnicalIndicators)arrayList[bottomArray[i]]).bar.low > ((TechnicalIndicators)arrayList[bottomArray[i - 1]]).bar.low)
                {
                    if ("".Equals(topBottomFlag) || "TOP".Equals(topBottomFlag))
                    {
                        ((TechnicalIndicators)arrayList[bottomArray[i - 1]]).Action03 = "BOTTOM";
                        topBottomFlag = "BOTTOM";
                    }
                }
            }

            nextIndex = 0;
            int currentTopBottomIndex = 0;
            int topBottomIndex = 0;
            //遍历整个数组,调整顶底的位置
            for (int index = 1; index <= arrayList.Count - 1; index++)
            {
                if (index >= nextIndex)
                {
                    //如果当前是顶，则往后找相邻的顶
                    if ("TOP".Equals(((TechnicalIndicators)arrayList[index]).Action03))
                    {
                        //从当前K线往后找下一个顶分型
                        for (int i = index + 1; i <= arrayList.Count - 1; i++)
                        {
                            //如果当前是顶，则往前往后找相邻的顶
                            if ("TOP".Equals(((TechnicalIndicators)arrayList[i]).Action03))
                            {
                                topBottomIndex = index;
                                currentTopBottomIndex = index;
                                //两个顶分型之间找最低K线
                                for (int k = index + 1; k <= i - 1; k++)
                                {
                                    if (((TechnicalIndicators)arrayList[k]).bar.low <= ((TechnicalIndicators)arrayList[topBottomIndex]).bar.low)
                                    {
                                        topBottomIndex = k;
                                    }
                                    //如果当前是底
                                    if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[k]).Action03))
                                    {
                                        currentTopBottomIndex = k;
                                    }
                                }
                                if (topBottomIndex != currentTopBottomIndex)
                                {
                                    ((TechnicalIndicators)arrayList[currentTopBottomIndex]).Action03 = "";
                                    ((TechnicalIndicators)arrayList[topBottomIndex]).Action03 = "BOTTOM";
                                }
                                nextIndex = topBottomIndex;
                                break;
                            }
                        }
                    }
                    //如果当前是底，则往后找相邻的底
                    if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[index]).Action03))
                    {
                        //从当前K线往后找下一个底分型
                        for (int i = index + 1; i <= arrayList.Count - 1; i++)
                        {
                            //如果当前是底，则往前往后找相邻的底
                            if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[i]).Action03))
                            {
                                topBottomIndex = index;
                                currentTopBottomIndex = index;
                                //两个底分型之间找最高K线
                                for (int k = index + 1; k <= i - 1; k++)
                                {
                                    if (((TechnicalIndicators)arrayList[k]).bar.high >= ((TechnicalIndicators)arrayList[topBottomIndex]).bar.high)
                                    {
                                        topBottomIndex = k;
                                    }
                                    //如果当前是顶
                                    if ("TOP".Equals(((TechnicalIndicators)arrayList[k]).Action03))
                                    {
                                        currentTopBottomIndex = k;
                                    }
                                }
                                if (topBottomIndex != currentTopBottomIndex)
                                {
                                    ((TechnicalIndicators)arrayList[currentTopBottomIndex]).Action03 = "";
                                    ((TechnicalIndicators)arrayList[topBottomIndex]).Action03 = "TOP";
                                }
                                nextIndex = topBottomIndex;
                                break;
                            }
                        }
                    }
                }
            }
        }
        //计算缠论K线的笔中枢
        private static void biCentreChan(ref ArrayList arrayList)
        {
            String securityCode = "";  //证券代码
            DateTime transactionDate;     //交易日期
            int nextIndex = 0;
            int topArrayIndex = 0;
            int bottomArrayIndex = 0;
            bool first = false;
            string stringBottomArray = "";

            for (int index = 1; index <= arrayList.Count - 1; index++)
            {
                if (index >= nextIndex)
                {
                    securityCode = ((TechnicalIndicators)arrayList[index]).bar.symbol; //证券代码
                    transactionDate = ((TechnicalIndicators)arrayList[index]).bar.bob; //交易日期

                    //如个当前形态是顶分型
                    if ("TOP".Equals(((TechnicalIndicators)arrayList[index]).Action03))
                    {
                        if (first == false)
                        {
                            stringBottomArray = "TOP";
                            first = true;
                        }
                        topArrayIndex++;
                        nextIndex = index + 1;
                    }
                    //如个当前形态是底分型
                    if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[index]).Action03))
                    {
                        if (first == false)
                        {
                            stringBottomArray = "BOTTOM";
                            first = true;
                        }
                        bottomArrayIndex++;
                        nextIndex = index + 1;
                    }
                }
            }
            int[] topArray = new int[topArrayIndex];
            int[] bottomArray = new int[bottomArrayIndex];
            topArrayIndex = 0;
            bottomArrayIndex = 0;
            nextIndex = 0;
            for (int index = 1; index <= arrayList.Count - 1; index++)
            {
                if (index >= nextIndex)
                {
                    //如个当前形态是顶分型
                    if ("TOP".Equals(((TechnicalIndicators)arrayList[index]).Action03))
                    {
                        topArray[topArrayIndex] = index;
                        topArrayIndex++;
                        nextIndex = index + 1;
                    }
                    //如个当前形态是底分型
                    if ("BOTTOM".Equals(((TechnicalIndicators)arrayList[index]).Action03))
                    {
                        bottomArray[bottomArrayIndex] = index;
                        bottomArrayIndex++;
                        nextIndex = index + 1;
                    }
                }
            }
            int mixIndex = topArrayIndex;
            if (bottomArrayIndex < topArrayIndex)
            {
                mixIndex = bottomArrayIndex;
            }

            for (int i = 1; i < mixIndex; i++)
            {
                if ("TOP".Equals(stringBottomArray))
                {
                    //for (int j=){
                    //}
                }


            }
        }
    }
}
