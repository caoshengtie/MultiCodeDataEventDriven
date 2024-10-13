using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageFormat = System.Drawing.Imaging.ImageFormat;
using Tesseract;
using System.Data;
using GMSDK;

namespace MultiCodeDataEventDriven
{
    public class Trade
    {
        #region field
        private WindowsTreeNode windowsTreeNode = new WindowsTreeNode();
        private  bool shouldStop = false; // 线程退出标志
        //资金股票信息
        private ArrayList arrayListCapitalSecurity = new ArrayList();
        //当日成交信息
        private ArrayList arrayListIntradayTransaction = new ArrayList();
        //当日委托信息
        private ArrayList arrayListIntradayEntrustment = new ArrayList();
        private string currentAppPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug", "").Replace("bin", "");
        private TradeInfo tradeInfomation = new TradeInfo();
        #endregion
        #region properity

        //股票市场交易信息
        public TradeInfo TradeInfomation
        {
            get { return this.tradeInfomation; }
            set { this.tradeInfomation = value; }
        }
        #endregion
        //买入股票
        public int buyF6(string symbol, string price, string quantity)
        {
            int retStatus = -1;
            //可用余额
            float availableAmount = 0;
            //买入价格
            float buyPrice = 0;
            //买入数量
            float buyQuantity = 0;
            //可用余额
            if (!"".Equals(windowsTreeNode.AccountInfo.AvailableAmount))
            {
                availableAmount =windowsTreeNode.AccountInfo.AvailableAmount;
            }
            //买入价格
            if (!"".Equals(price))
            {
                buyPrice = float.Parse(price);
            }
            //买入数量
            if (!"".Equals(quantity))
            {
                buyQuantity = float.Parse(quantity);
            }
            //可用余额小于买入价格*买入数量
            if (availableAmount < buyPrice * buyQuantity)
            {
                retStatus = Const.BUY_STATUS_INSUFFICIENT_FAIL;
                return retStatus;
            }

            if (windowsTreeNode.WindowIntPtr.SysTreeView32 != IntPtr.Zero)
            {
                // 将该窗口设置为前台窗口
                Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.SysTreeView32);

                // 按下F6键
                Win32.keybd_event(Const.VK_F6, 0x44, Const.KEYEVENTF_EXTENDEDKEY, 0);

                // 释放F6键
                Win32.keybd_event(Const.VK_F6, 0x44, Const.KEYEVENTF_EXTENDEDKEY | Const.KEYEVENTF_KEYUP, 0);
            }
            //小数点后取两位小数
            if (price != null && price.IndexOf(".") >= 0)
            {
                price = price.Substring(0, price.IndexOf(".") + 1)
                    + price.Substring(price.IndexOf(".") + 1, 2);
            }
            bool checkBool = checkBuyF6IntPtr();
            if (checkBool)
            {
                //string market = "上海Ａ股";
                //int index = 0;
                //if ("SHSE".Equals(symbol.Substring(0, 4)))
                //{
                //    index = 0;
                //    market = "上海Ａ股";
                //}
                //if ("SZSE".Equals(symbol.Substring(0, 4)))
                //{
                //    index = 1;
                //    market = "深圳Ａ股";
                //}
                //设置买入股票代码、价格及数量                                                                                                                      //    ////点击重填按钮
                if (windowsTreeNode.WindowIntPtr.BuySymbolF6IntPtr != IntPtr.Zero)
                {
                    Thread.Sleep(1000);
                    // 将该窗口设置为前台窗口
                    Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.BuySymbolF6IntPtr);
                    // 发送Ctrl+C到当前激活的窗口
                    char[] symbolChar = symbol.Substring(5, 6).ToArray();
                    for (int i = 0; i < symbolChar.Length; i++)
                    {
                        SendKeys.SendWait("{" + symbolChar[i] + "}");
                    }
                    Thread.Sleep(1000);
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.BuySymbolF6IntPtr, Const.WM_SETTEXT, IntPtr.Zero, symbol.Substring(5, 6));//发送消息
                    Thread.Sleep(1000);
                }
                //设置买入股票价格
                if (windowsTreeNode.WindowIntPtr.BuyPriceF6IntPtr != IntPtr.Zero)
                {
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.BuyPriceF6IntPtr, Const.WM_SETTEXT, IntPtr.Zero, price);//发送消息
                }
                //设置买入股票数量
                if (windowsTreeNode.WindowIntPtr.BuyQuantityF6IntPtr != IntPtr.Zero)
                {
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.BuyQuantityF6IntPtr, Const.WM_SETTEXT, IntPtr.Zero, quantity);//发送消息
                }

                shouldStop = false; // 设置停止标志
                Thread threadInputEnter = new Thread(new ThreadStart(inputEnter));
                //启动新线程
                threadInputEnter.Start();       // 调用线程，输入回车键。当买入股票出现确认对话框时，模拟输入回车键。
                if (windowsTreeNode.WindowIntPtr.BuyButtonF6IntPtr != IntPtr.Zero)
                {
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.BuyButtonF6IntPtr, Const.WM_LBUTTONDOWN, IntPtr.Zero, null);//鼠标按下按钮
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.BuyButtonF6IntPtr, Const.WM_LBUTTONUP, IntPtr.Zero, null);//释放鼠标           
                }                                                                                                   //点完工具栏的卖按钮后重新加载句柄信息
                windowsTreeNode = new WindowsTreeNode();

                Thread.Sleep(2000);
                if (windowsTreeNode.WindowIntPtr.EntrustmentPromptConfirmButtonIntPtr != IntPtr.Zero)
                {
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.EntrustmentPromptConfirmButtonIntPtr, Const.WM_LBUTTONDOWN, IntPtr.Zero, null);//鼠标按下按钮
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.EntrustmentPromptConfirmButtonIntPtr, Const.WM_LBUTTONUP, IntPtr.Zero, null);//释放鼠标           
                }                                                                                                   //点完工具栏的卖按钮后重新加载句柄信息

                shouldStop = true; // 设置停止标志
                threadInputEnter.Join(); // 等待线程结束
            }
            //查询当日成交

            getIntradayTransaction();
            
            bool hasTransaction = false;
            int transactionQuantity = 0;           //成交数量 
            if (arrayListIntradayTransaction.Count > 0)
            {
                for (int i = 0; i < arrayListIntradayTransaction.Count; i++)
                {
                    if (symbol.Substring(5, 6).Equals(((IntradayTransaction)arrayListIntradayTransaction[i]).SecurityCode)
                        && "证券买入".Equals(((IntradayTransaction)arrayListIntradayTransaction[i]).Operation))
                    {
                        hasTransaction = true;
                        transactionQuantity = transactionQuantity + ((IntradayTransaction)arrayListIntradayTransaction[i]).TransactionQuantity; //成交数量累计
                    }
                }
            }
            if (hasTransaction && transactionQuantity == int.Parse(quantity))
            {
                retStatus = Const.BUY_STATUS_COMPLETED_SUCCESS; //完全成交
            }
            else if (hasTransaction && transactionQuantity > 0 && transactionQuantity < int.Parse(quantity))
            {
                retStatus = Const.BUY_STATUS_PART_SUCCESS; //部分成交
            }
            else
            {
                retStatus = Const.BUY_STATUS_FAIL;
            }

            return retStatus;

        }
        //卖出股票
        public int sellF6(string symbol, string price, string quantity)
        {
            int retStatus = -1;

            if (windowsTreeNode.WindowIntPtr.SysTreeView32 != IntPtr.Zero)
            {
                // 将该窗口设置为前台窗口
                Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.SysTreeView32);
                // 按下F6键
                Win32.keybd_event(Const.VK_F6, 0x45, Const.KEYEVENTF_EXTENDEDKEY, 0);
                // 释放F6键

                Win32.keybd_event(Const.VK_F6, 0x45, Const.KEYEVENTF_EXTENDEDKEY | Const.KEYEVENTF_KEYUP, 0);
            }
            //小数点后取两位小数
            if (price != null && price.IndexOf(".") >= 0)
            {
                price = price.Substring(0, price.IndexOf(".") + 1)
                    + price.Substring(price.IndexOf(".") + 1, 2);
            }
            windowsTreeNode = new WindowsTreeNode();
            bool checkBool = checkSellF6IntPtr();
            if (checkBool)
            {
                //string market = "上海Ａ股";
                //int index = 0;
                //if ("SHSE".Equals(symbol.Substring(0, 4)))
                //{
                //    index = 0;
                //    market = "上海Ａ股";
                //}
                //if ("SZSE".Equals(symbol.Substring(0, 4)))
                //{
                //    index = 1;
                //    market = "深圳Ａ股";
                //}
                //设置卖出股票代码、价格及数量                                                                                                                        //    ////点击重填按钮
                if (windowsTreeNode.WindowIntPtr.SellSymbolF6IntPtr != IntPtr.Zero)
                {
                    Thread.Sleep(1000);
                    // 将该窗口设置为前台窗口
                    Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.SellSymbolF6IntPtr);
                    // 发送Ctrl+C到当前激活的窗口
                    char[] symbolChar = symbol.Substring(5, 6).ToArray();
                    for (int i = 0; i < symbolChar.Length; i++)
                    {
                        SendKeys.SendWait("{" + symbolChar[i] + "}");
                    }
                    Thread.Sleep(1000);
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.SellSymbolF6IntPtr, Const.WM_SETTEXT, IntPtr.Zero, symbol.Substring(5, 6));//发送消息
                    Thread.Sleep(1000);
                }
                if (windowsTreeNode.WindowIntPtr.SellPriceF6IntPtr != IntPtr.Zero)
                {
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.SellPriceF6IntPtr, Const.WM_SETTEXT, IntPtr.Zero, price);//发送消息
                }
                if (windowsTreeNode.WindowIntPtr.SellQuantityF6IntPtr != IntPtr.Zero)
                {
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.SellQuantityF6IntPtr, Const.WM_SETTEXT, IntPtr.Zero, quantity);//发送消息
                }

                shouldStop = false; // 设置停止标志
                Thread threadInputEnter = new Thread(new ThreadStart(inputEnter));
                //启动新线程
                threadInputEnter.Start();       // 调用线程，输入回车键。当买入股票出现确认对话框时，模拟输入回车键。

                if (windowsTreeNode.WindowIntPtr.SellButtonF6IntPtr != IntPtr.Zero)
                {
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.SellButtonF6IntPtr, Const.WM_LBUTTONDOWN, IntPtr.Zero, null);//鼠标按下按钮
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.SellButtonF6IntPtr, Const.WM_LBUTTONUP, IntPtr.Zero, null);//释放鼠标           
                }
                Thread.Sleep(5000);
                if (windowsTreeNode.WindowIntPtr.EntrustmentPromptConfirmButtonIntPtr != IntPtr.Zero)
                {
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.EntrustmentPromptConfirmButtonIntPtr, Const.WM_LBUTTONDOWN, IntPtr.Zero, null);//鼠标按下按钮
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.EntrustmentPromptConfirmButtonIntPtr, Const.WM_LBUTTONUP, IntPtr.Zero, null);//释放鼠标           
                }
                //点完工具栏的卖按钮后重新加载句柄信息

                shouldStop = true; // 设置停止标志
                threadInputEnter.Join(); // 等待线程结束
            }
            //查询当日成交
            getIntradayTransaction();

            bool hasTransaction = false;
            int transactionQuantity = 0;           //成交数量 
            if (arrayListIntradayTransaction.Count > 0)
            {
                for (int i = 0; i < arrayListIntradayTransaction.Count; i++)
                {
                    if (symbol.Substring(5, 6).Equals(((IntradayTransaction)arrayListIntradayTransaction[i]).SecurityCode)
                        && "证券卖出".Equals(((IntradayTransaction)arrayListIntradayTransaction[i]).Operation))
                    {
                        hasTransaction = true;
                        transactionQuantity = transactionQuantity + ((IntradayTransaction)arrayListIntradayTransaction[i]).TransactionQuantity; //成交数量累计
                    }
                }
            }
            if (hasTransaction && transactionQuantity == int.Parse(quantity))
            {
                retStatus = Const.SELL_STATUS_COMPLETED_SUCCESS; //卖出完全成交
            }
            else if (hasTransaction && transactionQuantity > 0 && transactionQuantity < int.Parse(quantity))
            {
                retStatus = Const.SELL_STATUS_PART_SUCCESS; //卖出部分成交
            }
            else
            {
                retStatus = Const.SELL_STATUS_FAIL;    //卖出失败
            }

            return retStatus;
        }
        public void cancelOrder()
        {
            WindowsTreeNode windowsTreeNode = new WindowsTreeNode();

            if (windowsTreeNode.WindowIntPtr.SysTreeView32 != IntPtr.Zero)
            {
                // 将该窗口设置为前台窗口
                Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.SysTreeView32);
                // 按下F3键
                Win32.keybd_event(Const.VK_F3, 0x44, Const.KEYEVENTF_EXTENDEDKEY, 0);
                // 释放F3键
                Win32.keybd_event(Const.VK_F3, 0x44, Const.KEYEVENTF_EXTENDEDKEY | Const.KEYEVENTF_KEYUP, 0);
            }

            shouldStop = false; // 设置停止标志
            Thread threadInputEnter = new Thread(new ThreadStart(inputEnter));
            //启动新线程
            threadInputEnter.Start();       // 调用线程，输入回车键。当买入股票出现确认对话框时，模拟输入回车键。
                                            //点完工具栏的卖按钮后重新加载句柄信息
                                            //撤单
            if (windowsTreeNode.WindowIntPtr.CancelOrderCVirtualGridCtrlIntPtr != IntPtr.Zero)
            {
                // 将该窗口设置为前台窗口
                Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.CancelOrderCVirtualGridCtrlIntPtr);
                if (windowsTreeNode.WindowIntPtr.CancelOrderAllIntPtr != IntPtr.Zero)
                {
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.CancelOrderAllIntPtr, Const.WM_LBUTTONDOWN, IntPtr.Zero, null);//鼠标按下按钮
                    Win32.SendMessage(windowsTreeNode.WindowIntPtr.CancelOrderAllIntPtr, Const.WM_LBUTTONUP, IntPtr.Zero, null);//释放鼠标           
                }
            }
            Thread.Sleep(5000);
            shouldStop = true; // 设置停止标志
            threadInputEnter.Join(); // 等待线程结束

            //windowsTreeNode = new WindowsTreeNode();
            //if (windowsTreeNode.WindowIntPtr.CancelOrderConfirmButtonParentIntPtr != IntPtr.Zero)
            //{
            //    // 将该窗口设置为前台窗口
            //    SetForegroundWindow(windowsTreeNode.WindowIntPtr.CancelOrderConfirmButtonParentIntPtr);
            //    if (windowsTreeNode.WindowIntPtr.CancelOrderConfirmButtonIntPtr != IntPtr.Zero)
            //    {
            //        SendMessage(windowsTreeNode.WindowIntPtr.CancelOrderConfirmButtonIntPtr, Const.WM_LBUTTONDOWN, IntPtr.Zero, null);//鼠标按下按钮
            //        SendMessage(windowsTreeNode.WindowIntPtr.CancelOrderConfirmButtonIntPtr, Const.WM_LBUTTONUP, IntPtr.Zero, null);//释放鼠标           
            //    }
            //}
        }
        public bool checkBuyF6IntPtr()
        {
            bool retBool = false;
            bool loopBuySymbol = false;
            bool loopBuyPrice = false;
            bool loopBuyQuantity = false;
            bool loopBuyButton = false;

            windowsTreeNode = new WindowsTreeNode();
            //买入股票代码、价格及数量
            if (windowsTreeNode.WindowIntPtr.BuySymbolF6IntPtr != IntPtr.Zero)
            {
                loopBuySymbol = true;
            }
            if (windowsTreeNode.WindowIntPtr.BuyPriceF6IntPtr != IntPtr.Zero)
            {
                loopBuyPrice = true;
            }

            if (windowsTreeNode.WindowIntPtr.BuyQuantityF6IntPtr != IntPtr.Zero)
            {
                loopBuyQuantity = true;
            }
            if (windowsTreeNode.WindowIntPtr.BuyButtonF6IntPtr != IntPtr.Zero)
            {
                loopBuyButton = true;
            }
            retBool = loopBuySymbol && loopBuyPrice && loopBuyQuantity && loopBuyButton;
            return retBool;
        }
        public bool checkSellF6IntPtr()
        {
            bool retBool = false;
            bool loopSellSymbol = false;
            bool loopSellPrice = false;
            bool loopSellQuantity = false;
            bool loopSellButton = false;

            windowsTreeNode = new WindowsTreeNode();
            //卖出股票代码、价格及数量
            if (windowsTreeNode.WindowIntPtr.SellSymbolF6IntPtr != IntPtr.Zero)
            {
                loopSellSymbol = true;
            }
            if (windowsTreeNode.WindowIntPtr.SellPriceF6IntPtr != IntPtr.Zero)
            {
                loopSellPrice = true;
            }

            if (windowsTreeNode.WindowIntPtr.SellQuantityF6IntPtr != IntPtr.Zero)
            {
                loopSellQuantity = true;
            }
            if (windowsTreeNode.WindowIntPtr.SellButtonF6IntPtr != IntPtr.Zero)
            {
                loopSellButton = true;
            }
            retBool = loopSellSymbol && loopSellPrice && loopSellQuantity && loopSellButton;
            return retBool;
        }
        public  void inputEnter()
        {
            while (!shouldStop)
            {
                // 确保模拟操作在UI线程中执行               
                SendKeys.SendWait("{Enter}");
                Thread.Sleep(5000); // 等待对话框响应

                Win32.INPUT input = default(Win32.INPUT);
                input.type = Const.INPUT_MOUSE;
                input.mi.dx = 0;
                input.mi.dy = 0;
                input.mi.mouseData = 0;
                input.mi.dwFlags = Const.MOUSEEVENTF_LEFTDOWN;
                input.mi.time = 0;
                input.mi.dwExtraInfo = IntPtr.Zero;

                Win32.SendInput(1, ref input, Marshal.SizeOf(input));

                input.mi.dwFlags = Const.MOUSEEVENTF_LEFTUP;
                Win32.SendInput(1, ref input, Marshal.SizeOf(input));
            }
        }
        public void getIntradayTransaction()
        {
            WindowsTreeNode windowsTreeNode = new WindowsTreeNode();

            if (windowsTreeNode.WindowIntPtr.SysTreeView32 != IntPtr.Zero)
            {
                // 将该窗口设置为前台窗口
                Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.SysTreeView32);
                // 按下F6键
                Win32.keybd_event(Const.VK_F6, 0x44, Const.KEYEVENTF_EXTENDEDKEY, 0);
                // 释放F6键
                Win32.keybd_event(Const.VK_F6, 0x44, Const.KEYEVENTF_EXTENDEDKEY | Const.KEYEVENTF_KEYUP, 0);
            }
            Thread.Sleep(1000);
            //刷新按钮                                                                                                                      //    ////点击重填按钮
            if (windowsTreeNode.WindowIntPtr.RefreshButtonIntPtr != IntPtr.Zero)
            {
                Win32.SendMessage(windowsTreeNode.WindowIntPtr.RefreshButtonIntPtr, Const.WM_LBUTTONDOWN, IntPtr.Zero, null);//鼠标按下按钮
                Win32.SendMessage(windowsTreeNode.WindowIntPtr.RefreshButtonIntPtr, Const.WM_LBUTTONUP, IntPtr.Zero, null);//释放鼠标           
            }
            Thread.Sleep(1000);
            //当日成交
            if (windowsTreeNode.WindowIntPtr.Custom2CVirtualGridCtrlIntPtr != IntPtr.Zero)
            {
                // 将该窗口设置为前台窗口
                Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.Custom2CVirtualGridCtrlIntPtr);
                // 发送Ctrl+e到当前激活的窗口
                SendKeys.SendWait("^e");
                Thread.Sleep(2000);

                // 发送Ctrl+C到当前激活的窗口
                SendKeys.SendWait("^c");

                string verifyCodeFile = createImage();
                string retStr = identifyVerifyCode(verifyCodeFile);
                if (retStr != null && !"".Equals(retStr))
                {
                    VerifyCodeConfirm(retStr);
                }
                arrayListIntradayTransaction = getIntradayTransactionList();
            }
        }
        public string createImage()
        {
            string verifyCodeFile = currentAppPath + @"VerifyCodeImage\\verifyCode.png";

            try
            {
                string screenImg = currentAppPath + @"VerifyCodeImage\\screenImg.png";
                Thread.Sleep(3000);
                CaptureScreenAndSave(screenImg);

                Bitmap fromBmp = new Bitmap(screenImg);

                int length = 67;
                int width = 22;

                Rectangle section5 = new Rectangle(976, 514, length, width);
                Bitmap bmp = getVerifyCode(fromBmp, section5, length, width);
                bmp.Save(verifyCodeFile);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return verifyCodeFile;
        }
        public string identifyVerifyCode(string imagePath)
        {
            string retStr = "";
            using (var engine = new TesseractEngine(currentAppPath + "Tesseract-OCR\\" + @"./tessdata", "chi_sim", EngineMode.Default))
            {
                engine.SetVariable("tessedit_char_whitelist", "0123456789");
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        retStr = page.GetText();
                    }
                }
            }
            //去掉返回值里的"\n"字符
            if (!"".Equals(retStr))
            {
                if (retStr.Length == 5)
                {
                    retStr = retStr.Substring(0, 4);
                }
            }
            return retStr;
        }

        public void CaptureScreenAndSave(string filePath)
        {
            // 创建一个和屏幕一样大小的bitmap
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                // 创建一个Graphics对象
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    // 截取整个屏幕
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }

                // 保存图片
                bitmap.Save(filePath, ImageFormat.Png);
            }
        }
        //从粘贴板中获取当日成交信息
        public  ArrayList getIntradayTransactionList()
        {
            ArrayList list = new ArrayList();
            // 检查剪贴板是否有文本
            if (Clipboard.ContainsText())
            {
                // 读取剪贴板的文本内容
                string clipboardText = Clipboard.GetText();
                string[] line = clipboardText.Split('\n');
                if (line.Length > 1 && line[0].IndexOf("成交时间") >= 0)
                {
                    for (int i = 1; i < line.Length; i++)
                    {
                        string[] column = line[i].Split('\t');
                        //当日成交
                        IntradayTransaction intradayTransaction = new IntradayTransaction();
                        //成交时间
                        intradayTransaction.TransactionTime = column[0];
                        //证券代码
                        intradayTransaction.SecurityCode = column[1].Replace("=", "").Replace("\"", "");
                        //证券名称
                        intradayTransaction.SecurityName = column[2];
                        //操作
                        intradayTransaction.Operation = column[3];
                        //成交数量
                        intradayTransaction.TransactionQuantity = int.Parse(column[4]);
                        //成交均价
                        intradayTransaction.TransactionAveragePrice = float.Parse(column[5]);
                        //成交金额
                        intradayTransaction.TransactionAmount = float.Parse(column[6]);
                        //合同编号
                        intradayTransaction.ContractNumbe = column[7].Replace("=", "").Replace("\"", "");
                        //成交编号
                        intradayTransaction.TransactionNumbe = column[8];
                        //撤单数量
                        intradayTransaction.CancelOrderNumbe = int.Parse(column[9]);
                        //申报编号
                        intradayTransaction.DeclarationNumbe = column[10];
                        //委托属性
                        intradayTransaction.EntrustmentAttribute = column[11];

                        list.Add(intradayTransaction);
                    }
                }
            }
            return list;
        }
        public void VerifyCodeConfirm(string verifyCode)
        {
            WindowsTreeNode windowsTreeNode = new WindowsTreeNode();

            if (windowsTreeNode.WindowIntPtr.VerifyCodeIntPtr != IntPtr.Zero)
            {
                Win32.SendMessage(windowsTreeNode.WindowIntPtr.VerifyCodeIntPtr, Const.WM_SETTEXT, IntPtr.Zero, verifyCode);//发送消息
            }

            if (windowsTreeNode.WindowIntPtr.VerifyCodeConfirmButtonIntPtr != IntPtr.Zero)
            {
                Win32.SendMessage(windowsTreeNode.WindowIntPtr.VerifyCodeConfirmButtonIntPtr, Const.WM_LBUTTONDOWN, IntPtr.Zero, null);//鼠标按下按钮
                Win32.SendMessage(windowsTreeNode.WindowIntPtr.VerifyCodeConfirmButtonIntPtr, Const.WM_LBUTTONUP, IntPtr.Zero, null);//释放鼠标           
            }
        }
        public  Bitmap getVerifyCode(Bitmap srcBmp, Rectangle rectangle, int length, int width)
        {
            Bitmap bmp = new Bitmap(length, width);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(srcBmp, 0, 0, rectangle, GraphicsUnit.Pixel);
            return bmp;
        }
        //计算成交数量
        public  string getTransactionQuantity(string symbol, string price)
        {
            //返回买入股票数量
            string retQuantity = "0";
            //持仓百分比
            float positionPercent = 0;
            ////持仓百分比
            //float dayTradePercent = 0;

            //买入金额
            double buyAmount = 0;

            //通过标的代码查询到此标的持仓等信息
            SelectedTradeSymbols selectedTradeSymbol = TradeInfomation.getSelectedSymbols(symbol);

            if (selectedTradeSymbol != null) {
                //获取股票的持仓比例
                positionPercent = selectedTradeSymbol.PositionPercent;
                ////日内交易比例
                //dayTradePercent = selectedTradeSymbol.DayTradePercent;

                //总 资 产
                float totalAssets = this.windowsTreeNode.AccountInfo.TotalAssets;


                //如果可用金额大于总资产*股票持仓比例
                if (this.windowsTreeNode.AccountInfo.AvailableAmount >=
                    totalAssets * positionPercent)
                {
                    buyAmount = Common.stringToDouble(Math.Round(totalAssets * positionPercent, 2).ToString());
                }
                else
                {
                    buyAmount = Common.stringToDouble(this.windowsTreeNode.AccountInfo.AvailableAmount.ToString());
                }

                retQuantity = (((int)Math.Floor(buyAmount / Common.stringToDouble(price) / 100)) * 100).ToString();
            }
            return retQuantity;
        }
        public  string getSellQuantity(string symbol)
        {
            string retQuantity = "0";
            //获取资金股票
            getCapitalSecurity();
            for (int i = 0; i < this.arrayListCapitalSecurity.Count; i++)
            {
                if (symbol.Substring(5, 6).Equals(((CapitalSecurity)this.arrayListCapitalSecurity[i]).SecurityCode))
                {
                    retQuantity = ((CapitalSecurity)this.arrayListCapitalSecurity[i]).AvailableBalance.ToString();
                    break;
                }
            }
            return retQuantity;
        }
        public void getCapitalSecurity()
        {
            WindowsTreeNode windowsTreeNode = new WindowsTreeNode();

            if (windowsTreeNode.WindowIntPtr.SysTreeView32 != IntPtr.Zero)
            {
                // 将该窗口设置为前台窗口
                Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.SysTreeView32);
                // 按下F6键
                Win32.keybd_event(Const.VK_F6, 0x44, Const.KEYEVENTF_EXTENDEDKEY, 0);
                // 释放F6键
                Win32.keybd_event(Const.VK_F6, 0x44, Const.KEYEVENTF_EXTENDEDKEY | Const.KEYEVENTF_KEYUP, 0);
            }
            Thread.Sleep(1000);
            //刷新按钮                                                                                                                      //    ////点击重填按钮
            if (windowsTreeNode.WindowIntPtr.RefreshButtonIntPtr != IntPtr.Zero)
            {
                Win32.SendMessage(windowsTreeNode.WindowIntPtr.RefreshButtonIntPtr, Const.WM_LBUTTONDOWN, IntPtr.Zero, null);//鼠标按下按钮
                Win32.SendMessage(windowsTreeNode.WindowIntPtr.RefreshButtonIntPtr, Const.WM_LBUTTONUP, IntPtr.Zero, null);//释放鼠标           
            }
            Thread.Sleep(1000);
            //资金股票
            if (windowsTreeNode.WindowIntPtr.Custom2CVirtualGridCtrlIntPtr != IntPtr.Zero)
            {
                // 将该窗口设置为前台窗口
                Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.Custom2CVirtualGridCtrlIntPtr);
                // 发送Ctrl+w到当前激活的窗口
                SendKeys.SendWait("^w");
                Thread.Sleep(2000);
                // 发送Ctrl+C到当前激活的窗口
                SendKeys.SendWait("^c");

                string verifyCodeFile = createImage();
                string retStr = identifyVerifyCode(verifyCodeFile);
                if (retStr != null && !"".Equals(retStr))
                {
                    VerifyCodeConfirm(retStr);
                }
                arrayListCapitalSecurity = getCapitalSecurityList();
            }
        }
        //从粘贴板中获取资金股票信息
        public static ArrayList getCapitalSecurityList()
        {
            ArrayList list = new ArrayList();
            // 检查剪贴板是否有文本
            if (Clipboard.ContainsText())
            {
                // 读取剪贴板的文本内容
                string clipboardText = Clipboard.GetText();
                string[] line = clipboardText.Split('\n');
                if (line.Length > 1 && line[0].IndexOf("股票余额") >= 0)
                {
                    for (int i = 1; i < line.Length; i++)
                    {
                        string[] column = line[i].Split('\t');
                        //资金股票
                        CapitalSecurity capitalSecurity = new CapitalSecurity();
                        //证券名称
                        capitalSecurity.SecurityCode = column[0].Replace("=", "").Replace("\"", "");
                        //证券名称
                        capitalSecurity.SecurityName = column[1];

                        //股票余额
                        capitalSecurity.SecurityBalance = int.Parse(column[2]);
                        //可用余额
                        capitalSecurity.AvailableBalance = int.Parse(column[3]);
                        //冻结数量
                        capitalSecurity.FreezeQuantity = int.Parse(column[4]);
                        //盈亏
                        capitalSecurity.ProfitAndLoss = float.Parse(column[5]);
                        //成本价
                        capitalSecurity.CostPrice = float.Parse(column[6]);
                        //盈亏比例(%)
                        capitalSecurity.ProfitAndLossRatio = float.Parse(column[7]);
                        //市价
                        capitalSecurity.MarketPrice = float.Parse(column[8]);
                        //市值
                        capitalSecurity.MarketValue = float.Parse(column[9]);
                        //买入成本
                        capitalSecurity.PurchaseCost = float.Parse(column[10]);
                        //市场代码
                        capitalSecurity.MarketCode = column[11];
                        //交易市场
                        capitalSecurity.MarketName = column[12];
                        //股东帐户
                        capitalSecurity.ShareholderAccount = column[13].Replace("=", "").Replace("\"", "");
                        //当前持仓
                        capitalSecurity.CurrentPosition = int.Parse(column[14]);
                        //单位数量
                        capitalSecurity.NumberOfUnits = int.Parse(column[15]);
                        list.Add(capitalSecurity);
                    }
                }
            }
            return list;
        }
        //从粘贴板中获取当日委托信息
        public static ArrayList getIntradayEntrustmentList()
        {
            ArrayList list = new ArrayList();
            // 检查剪贴板是否有文本
            if (Clipboard.ContainsText())
            {
                // 读取剪贴板的文本内容
                string clipboardText = Clipboard.GetText();
                string[] line = clipboardText.Split('\n');
                if (line.Length > 1 && line[0].IndexOf("委托时间") >= 0)
                {
                    for (int i = 1; i < line.Length; i++)
                    {
                        string[] column = line[i].Split('\t');
                        //当日委托
                        IntradayEntrustment intradayEntrustment = new IntradayEntrustment();
                        //证券代码
                        intradayEntrustment.SecurityCode = column[0].Replace("=", "").Replace("\"", "");
                        //证券名称
                        intradayEntrustment.SecurityName = column[1];
                        //备注
                        intradayEntrustment.Remarks = column[2];
                        //委托数量
                        intradayEntrustment.EntrustmentQuantity = int.Parse(column[3]);
                        //成交数量
                        intradayEntrustment.TransactionQuantity = int.Parse(column[4]);
                        //委托价格
                        intradayEntrustment.EntrustmentPrice = float.Parse(column[5]);
                        //成交均价
                        intradayEntrustment.TransactionAveragePrice = float.Parse(column[6]);
                        //操作
                        intradayEntrustment.Operation = column[7];
                        //委托时间
                        intradayEntrustment.EntrustmentTime = column[8];
                        //委托日期
                        intradayEntrustment.EntrustmentDate = column[9];
                        //合同编号
                        intradayEntrustment.ContractNumbe = column[10].Replace("=", "").Replace("\"", "");
                        //交易市场
                        intradayEntrustment.MarketName = column[11];
                        //股东账号
                        intradayEntrustment.ShareholderAccount = column[12].Replace("=", "").Replace("\"", "");
                        //申报编号
                        intradayEntrustment.DeclarationNumbe = column[13];
                        //委托属性
                        intradayEntrustment.EntrustmentAttribute = column[14];
                        list.Add(intradayEntrustment);
                    }
                }
            }
            return list;
        }
        //检查同花顺交易软件
        public void checkTHS()
        {
            WindowsTreeNode windowsTreeNode = new WindowsTreeNode();
            if (windowsTreeNode.WindowIntPtr.SysTreeView32 != IntPtr.Zero)
            {
                // 将该窗口设置为前台窗口
                Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.SysTreeView32);
                //// 按下F1键
                //Win32.keybd_event(Const.VK_F1, 0x44, Const.KEYEVENTF_EXTENDEDKEY, 0);
                //// 释放F1键
                //Win32.keybd_event(Const.VK_F1, 0x44, Const.KEYEVENTF_EXTENDEDKEY | Const.KEYEVENTF_KEYUP, 0);
                //Thread.Sleep(1000);
                //// 按下F2键
                //Win32.keybd_event(Const.VK_F2, 0x44, Const.KEYEVENTF_EXTENDEDKEY, 0);
                //// 释放F2键
                //Win32.keybd_event(Const.VK_F2, 0x44, Const.KEYEVENTF_EXTENDEDKEY | Const.KEYEVENTF_KEYUP, 0);
                //Thread.Sleep(1000);
                // 按下F3键
                Win32.keybd_event(Const.VK_F3, 0x44, Const.KEYEVENTF_EXTENDEDKEY, 0);
                // 释放F3键
                Win32.keybd_event(Const.VK_F3, 0x44, Const.KEYEVENTF_EXTENDEDKEY | Const.KEYEVENTF_KEYUP, 0);
                Thread.Sleep(1000);
                // 按下F6键
                Win32.keybd_event(Const.VK_F6, 0x44, Const.KEYEVENTF_EXTENDEDKEY, 0);
                // 释放F6键
                Win32.keybd_event(Const.VK_F6, 0x44, Const.KEYEVENTF_EXTENDEDKEY | Const.KEYEVENTF_KEYUP, 0);
                Thread.Sleep(1000);

                if (windowsTreeNode.WindowIntPtr.BuyButtonF6ParentIntPtr != IntPtr.Zero)
                {
                    // 将该窗口设置为前台窗口
                    Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.BuyButtonF6ParentIntPtr);
                    // 按下F1键
                    Win32.keybd_event(Const.VK_F1, 0x44, Const.KEYEVENTF_EXTENDEDKEY, 0);
                    // 释放F1键
                    Win32.keybd_event(Const.VK_F1, 0x44, Const.KEYEVENTF_EXTENDEDKEY | Const.KEYEVENTF_KEYUP, 0);
                    Thread.Sleep(1000);
                    // 按下F2键
                    Win32.keybd_event(Const.VK_F2, 0x44, Const.KEYEVENTF_EXTENDEDKEY, 0);
                    // 释放F2键
                    Win32.keybd_event(Const.VK_F2, 0x44, Const.KEYEVENTF_EXTENDEDKEY | Const.KEYEVENTF_KEYUP, 0);
                }
                Thread.Sleep(1000);
                //资金股票
                if (windowsTreeNode.WindowIntPtr.Custom2CVirtualGridCtrlIntPtr != IntPtr.Zero)
                {
                    // 将该窗口设置为前台窗口
                    Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.Custom2CVirtualGridCtrlIntPtr);
                    // 发送Ctrl+w到当前激活的窗口
                    SendKeys.SendWait("^w");
                }
                Thread.Sleep(1000);
                //当日成交
                if (windowsTreeNode.WindowIntPtr.Custom2CVirtualGridCtrlIntPtr != IntPtr.Zero)
                {
                    // 将该窗口设置为前台窗口
                    Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.Custom2CVirtualGridCtrlIntPtr);
                    // 发送Ctrl+e到当前激活的窗口
                    SendKeys.SendWait("^e");
                }
                Thread.Sleep(1000);
                //当日委托
                if (windowsTreeNode.WindowIntPtr.Custom2CVirtualGridCtrlIntPtr != IntPtr.Zero)
                {
                    // 将该窗口设置为前台窗口
                    Win32.SetForegroundWindow(windowsTreeNode.WindowIntPtr.Custom2CVirtualGridCtrlIntPtr);
                    // 发送Ctrl+r到当前激活的窗口
                    SendKeys.SendWait("^r");
                }
            }
        }
    }
}
