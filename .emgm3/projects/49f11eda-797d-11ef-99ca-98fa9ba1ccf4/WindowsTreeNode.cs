using core.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    internal class WindowsTreeNode
    {
        const int nMaxCount = 256;

        #region field
        private TreeNode<Window> windowAll;    //所有窗口      
        private WindowIntPtr windowIntPtr;     //窗口句柄
        private AccountInfo accountInfo;     //账户信息  
        #endregion
        #region properity
        //所有窗口
        public TreeNode<Window> WindowAll
        {
            get { return this.windowAll; }
            set { this.windowAll = value; }
        }
        //窗口句柄
        public WindowIntPtr WindowIntPtr
        {
            get { return this.windowIntPtr; }
            set { this.windowIntPtr = value; }
        }
        //账户信息  
        public AccountInfo AccountInfo
        {
            get { return this.accountInfo; }
            set { this.accountInfo = value; }
        }

        #endregion



        int orderNum = 1;
        public WindowsTreeNode()
        {
            //初始化
            windowIntPtr = new WindowIntPtr();    //窗口句柄
            accountInfo = new AccountInfo();     //账户信息  

            Window window = new Window();
            window.WindowDesc = "桌面";
            window.WindowTitle = "桌面";
            window.WindowClass = "#32769 (桌面)";
            window.WindowLevel = 1;
            window.WindowOrder = 1;
            this.windowAll = new TreeNode<Window>(window);           //同花顺窗口树形

            Win32.EnumWindows(EnumWindowsCallback, IntPtr.Zero);
            foreachTree(this.windowAll);
        }
        public bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam)
        {

            StringBuilder windowText = new StringBuilder(nMaxCount);
            Win32.GetWindowText(hWnd, windowText, nMaxCount);
            StringBuilder windowClassName = new StringBuilder(nMaxCount);
            Win32.GetClassNameW(hWnd, windowClassName, nMaxCount);
            Window window = new Window();
            window.WindowDesc = "";
            window.WindowIntPtr = hWnd;
            window.WindowTitle = windowText.ToString();
            window.WindowClass = windowClassName.ToString();
            window.WindowLevel = 2;
            window.WindowOrder = orderNum;
            TreeNode<Window> nodeWindow = new TreeNode<Window>(window);
            nodeWindow.Parent = this.windowAll;
            this.windowAll.AddChild(nodeWindow);

            EnumerateChildWindowsNode(hWnd, nodeWindow);
            orderNum++;
            return true;
        }
        public static void EnumerateChildWindowsNode(IntPtr parentHandle, TreeNode<Window> parentWindow)
        {
            int index = 0;
            IntPtr hwndChild = IntPtr.Zero;
            while ((hwndChild = Win32.FindWindowEx(parentHandle, hwndChild, null, null)) != IntPtr.Zero)
            {
                index++;
                StringBuilder windowText = new StringBuilder(nMaxCount);
                Win32.GetWindowText(hwndChild, windowText, nMaxCount);
                StringBuilder windowClassName = new StringBuilder(nMaxCount);
                Win32.GetClassNameW(hwndChild, windowClassName, nMaxCount);
                Window parentwindow = parentWindow.Value;
                Window window = new Window();
                window.WindowDesc = "";
                window.WindowIntPtr = hwndChild;
                window.WindowTitle = windowText.ToString();
                window.WindowClass = windowClassName.ToString();
                window.WindowLevel = parentwindow.WindowLevel + 1;
                window.WindowOrder = index;
                TreeNode<Window> nodeWindow = new TreeNode<Window>(window);
                nodeWindow.Parent = parentWindow;
                parentWindow.AddChild(nodeWindow);
                EnumerateChildWindowsNode(hwndChild, nodeWindow);
            }
        }
        public void foreachTree(TreeNode<Window> node)
        {
            foreach (var child in node.Children)
            {
                Window window = child.Value;
                if (window.WindowTitle.IndexOf("交易系统已锁定") >= 0)
                {
                    foreach (var brother in child.Parent.Children)
                    {
                        Window brotherWindow = brother.Value;
                        //可用资金
                        if (brotherWindow.WindowOrder == 3)
                        {
                            WindowIntPtr.AvailableAmountIntPtr = brotherWindow.WindowIntPtr;
                        }
                        //解锁按钮
                        if (brotherWindow.WindowOrder == 12)
                        {
                            WindowIntPtr.UnLockButtonIntPtr = brotherWindow.WindowIntPtr;
                        }
                    }
                }
                if (window.WindowTitle.IndexOf("请输入您的交易密码") >= 0)
                {
                    foreach (var brother in child.Parent.Children)
                    {
                        Window brotherWindow = brother.Value;
                        //密码输入框
                        if (brotherWindow.WindowOrder == 2)
                        {
                            WindowIntPtr.UnLockPasswordIntPtr = brotherWindow.WindowIntPtr;
                        }
                        //解锁按钮
                        if (brotherWindow.WindowOrder == 3)
                        {
                            WindowIntPtr.UnLockConfirmButtonIntPtr = brotherWindow.WindowIntPtr;
                        }
                    }
                }
                //买入[F1]菜单买入股票
                if (window.WindowTitle.IndexOf("买入[B]") >= 0)
                {
                    bool isBrother = false;
                    //定位买入[B]按钮
                    TreeNode<Window> retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder + 1);
                    if (retNode != null)
                    {
                        if ("重填".Equals(retNode.Value.WindowTitle)
                            && searchNodeByTitle(child.Parent, "买入股票"))
                        {
                            isBrother = true;
                            //买入股票重填按钮
                            WindowIntPtr.ResetBuyButtonIntPtr = retNode.Value.WindowIntPtr;
                        }
                    }
                    //买入股票代码
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 2);
                        if (retNode != null)
                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //买入股票代码
                                WindowIntPtr.BuySymbolIntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //买入价格
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 4);
                        if (retNode != null)
                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //买入价格
                                WindowIntPtr.BuyPriceIntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //买入数量
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 6);
                        if (retNode != null)
                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //买入数量
                                WindowIntPtr.BuyQuantityIntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //买入[B]按钮
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 7);
                        if (retNode != null)
                        {
                            if ("买入[B]".Equals(retNode.Value.WindowTitle))
                            {
                                //买入[B]按钮
                                WindowIntPtr.BuyButtonIntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                }
                //卖出[F2]菜单卖出股票
                if (window.WindowTitle.IndexOf("卖出[S]") >= 0)
                {
                    bool isBrother = false;
                    //定位卖出[S]按钮
                    TreeNode<Window> retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder + 1);
                    if (retNode != null)
                    {
                        if ("重填".Equals(retNode.Value.WindowTitle)
                            && searchNodeByTitle(child.Parent, "卖出股票"))
                        {
                            isBrother = true;
                            //卖出股票重填按钮
                            WindowIntPtr.ResetSellButtonIntPtr = retNode.Value.WindowIntPtr;

                        }
                    }
                    //卖出股票代码
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 2);
                        if (retNode != null)
                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //卖出股票代码
                                WindowIntPtr.SellSymbolIntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //卖出价格
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 4);
                        if (retNode != null)
                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //卖出价格
                                WindowIntPtr.SellPriceIntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //卖出数量
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 6);
                        if (retNode != null)
                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //卖出数量
                                WindowIntPtr.SellQuantityIntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //卖出[S]按钮
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 7);
                        if (retNode != null)
                        {
                            if ("卖出[S]".Equals(retNode.Value.WindowTitle))
                            {
                                //卖出[S]按钮
                                WindowIntPtr.SellButtonIntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                }
                //双向委托[F6]菜单买入卖出股票
                if (window.WindowTitle.IndexOf("买入[B]") >= 0)
                {
                    bool isBrother = false;

                    if (searchNodeByTitle(child.Parent, "买入股票[F1]"))
                    {
                        //买入[B]按钮
                        WindowIntPtr.BuyButtonF6IntPtr = child.Value.WindowIntPtr;
                        WindowIntPtr.BuyButtonF6ParentIntPtr = child.Parent.Value.WindowIntPtr;
                        isBrother = true;
                    }
                    //根据与买入股票的限价委托 的相对位置定位
                    TreeNode<Window> retNode = null;

                    //买入股票市场
                    if (isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder - 8);
                        if (retNode != null)
                        {
                            if ("ComboBox".Equals(retNode.Value.WindowClass))
                            {
                                //买入股票市场
                                WindowIntPtr.BuyMarketF6IntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //买入股票代码
                    if (isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder - 6);
                        if (retNode != null)
                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //买入股票代码
                                WindowIntPtr.BuySymbolF6IntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //买入价格
                    if (isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder - 3);
                        if (retNode != null)
                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //买入价格
                                WindowIntPtr.BuyPriceF6IntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //买入数量
                    if (isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder - 1);
                        if (retNode != null)
                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //买入数量
                                WindowIntPtr.BuyQuantityF6IntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }

                    //根据与买入股票的限价委托 的相对位置定位
                    TreeNode<Window> retNodeLimitSell = null;
                    //卖出股票市场
                    if (isBrother)
                    {
                        retNodeLimitSell = searchNodeByTitleAndOrder(child.Parent, "卖出[S]");
                        if (retNodeLimitSell != null)
                        {
                            //卖出[S]按钮
                            WindowIntPtr.SellButtonF6IntPtr = retNodeLimitSell.Value.WindowIntPtr;
                        }
                    }
                    if (retNodeLimitSell != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, retNodeLimitSell.Value.WindowOrder - 8);
                        if (retNode != null)
                        {
                            if ("ComboBox".Equals(retNode.Value.WindowClass))
                            {
                                //卖出股票市场
                                WindowIntPtr.SellMarketF6IntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //卖出股票代码
                    if (retNodeLimitSell != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, retNodeLimitSell.Value.WindowOrder - 6);
                        if (retNode != null)
                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //卖出股票代码
                                WindowIntPtr.SellSymbolF6IntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //卖出价格
                    if (retNodeLimitSell != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, retNodeLimitSell.Value.WindowOrder - 3);
                        if (retNode != null)
                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //卖出价格
                                WindowIntPtr.SellPriceF6IntPtr = retNode.Value.WindowIntPtr;
                            }
                        }
                    }
                    //卖出数量
                    if (retNodeLimitSell != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, retNodeLimitSell.Value.WindowOrder - 1);
                        if (retNode != null)

                        {
                            if ("Edit".Equals(retNode.Value.WindowClass))
                            {
                                //卖出数量
                                WindowIntPtr.SellQuantityF6IntPtr = retNode.Value.WindowIntPtr;

                            }
                        }
                    }
                }

                //查询标题为"Custom2"且类为CVirtualGridCtrl的窗口(持仓(W)、成交(E)、委托(R))
                if ("Custom2".Equals(window.WindowTitle) && "CVirtualGridCtrl".Equals(window.WindowClass))
                {
                    if (searchNodeByTitle(child.Parent.Parent.Parent, "买入股票[F1]"))
                    {
                        WindowIntPtr.Custom2CVirtualGridCtrlIntPtr = child.Value.WindowIntPtr;
                    }
                }
                //您的买入委托已成功提交
                if (window.WindowTitle.IndexOf("您的买入委托已成功提交") >= 0)
                {
                }
                //您的卖出委托已成功提交
                if (window.WindowTitle.IndexOf("您的卖出委托已成功提交") >= 0)
                {
                }
                //验证码
                if (window.WindowTitle.IndexOf("先输入验证码") >= 0)
                {
                    TreeNode<Window> retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder - 1);
                    if (retNode != null)
                    {
                        if ("Edit".Equals(retNode.Value.WindowClass))
                        {
                            //验证码输入框
                            WindowIntPtr.VerifyCodeIntPtr = retNode.Value.WindowIntPtr;

                        }
                    }
                    retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder - 7);
                    if (retNode != null)
                    {
                        if ("Button".Equals(retNode.Value.WindowClass))
                        {
                            //验证码确定按钮
                            WindowIntPtr.VerifyCodeConfirmButtonIntPtr = retNode.Value.WindowIntPtr;

                        }
                    }
                    retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder - 6);
                    if (retNode != null)
                    {
                        if ("Button".Equals(retNode.Value.WindowClass))
                        {
                            //验证码取消按钮
                            WindowIntPtr.VerifyCodeCancelButtonIntPtr = retNode.Value.WindowIntPtr;

                        }
                    }

                }
                //Custom1
                if (window.WindowTitle.IndexOf("Custom1") >= 0)
                {
                    WindowIntPtr.Custom1IntPtr = child.Value.WindowIntPtr;
                }
                //股票代码和证券市场不匹配。
                //提交失败：[101015][证券代码表记录不存在] 
                //[exchange_type= 2,stock_code = 600016]。
                if (window.WindowTitle.IndexOf("101015") >= 0)
                {
                    //定位确定按钮
                    TreeNode<Window> retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder - 2);
                    //确定按钮
                    if (retNode != null)
                    {
                        //确定按钮
                        WindowIntPtr.EntrustmentPromptConfirmButtonIntPtr = retNode.Value.WindowIntPtr;
                    }
                }
                //非委托时间段提交。
                //提交失败：[120022][该功能禁止在目前系统状态下运行]
                //[sys_status= 6,en_sys_status = 12,function81121327。
                if (window.WindowTitle.IndexOf("120022") >= 0)
                {
                    //定位确定按钮
                    TreeNode<Window> retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder - 2);
                    //确定按钮
                    if (retNode != null)
                    {
                        //确定按钮
                        WindowIntPtr.EntrustmentPromptConfirmButtonIntPtr = retNode.Value.WindowIntPtr;
                    }
                }

                if (window.WindowTitle.IndexOf("交易系统已锁定") >= 0)
                {
                    //定位确定按钮
                    TreeNode<Window> retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder - 1);

                    //确定按钮
                    if (retNode != null)
                    {
                        //确定按钮
                        WindowIntPtr.UnLockButtonIntPtr = retNode.Value.WindowIntPtr;
                    }
                }
                //撤单提示
                if (window.WindowTitle.IndexOf("您确认要撤销这") >= 0)
                {
                    //定位全部选中按钮
                    TreeNode<Window> retNode = searchNodeByTitleAndOrder(child.Parent, "是(&Y)");
                    if (retNode != null)
                    {
                        //定位是(&Y)按钮
                        WindowIntPtr.CancelOrderConfirmButtonIntPtr = retNode.Value.WindowIntPtr;
                        WindowIntPtr.CancelOrderConfirmButtonParentIntPtr = child.Parent.Value.WindowIntPtr;
                    }
                    retNode = searchNodeByTitleAndOrder(child.Parent, "否(&N)");
                    if (retNode != null)
                    {
                        //定位否(&N)按钮
                        WindowIntPtr.CancelOrderCancelButtonIntPtr = retNode.Value.WindowIntPtr;
                    }
                }
                if (window.WindowTitle.IndexOf("ＸＸ证券") >= 0)
                {
                    //资金股票CVirtualGridCtrl类句柄
                    TreeNode<Window> retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder + 39);

                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                        if (retNode != null && "CVirtualGridCtrl".Equals(retNode.Value.WindowClass))
                        {
                            WindowIntPtr.CapitalSecurity = retNode.Value.WindowIntPtr;
                        }
                    }
                    //当日成交
                    retNode = searchNodeByOrder(child.Parent.Parent, child.Parent.Value.WindowOrder + 1);

                    if (retNode != null)
                    {
                        retNode = searchNodeByTitleAndOrder(retNode, "您可以在资金对账单查询您的交易佣金标准");
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode.Parent, retNode.Value.WindowOrder + 2);
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                    }
                    if (retNode != null && "CVirtualGridCtrl".Equals(retNode.Value.WindowClass))
                    {
                        WindowIntPtr.IntradayTransaction = retNode.Value.WindowIntPtr;
                    }

                    //当日委托
                    retNode = searchNodeByOrder(child.Parent.Parent, child.Parent.Value.WindowOrder + 2);

                    if (retNode != null)
                    {
                        retNode = searchNodeByTitleAndOrder(retNode, "您可以在资金对账单查询您的交易佣金标准");
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode.Parent, retNode.Value.WindowOrder + 2);
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                    }
                    if (retNode != null && "CVirtualGridCtrl".Equals(retNode.Value.WindowClass))
                    {
                        WindowIntPtr.IntradayEntrustment = retNode.Value.WindowIntPtr;
                    }
                }
                //定位撤单
                if (window.WindowTitle.IndexOf("在委托记录上用鼠标双击或回车即可撤单") >= 0)
                {
                    //定位全部选中按钮
                    TreeNode<Window> retNode = searchNodeByTitleAndOrder(child.Parent, "全部选中");
                    if (retNode != null)
                    {
                        //定位全部选中按钮
                        WindowIntPtr.CancelOrderSelectAllIntPtr = retNode.Value.WindowIntPtr;

                    }
                    retNode = searchNodeByTitleAndOrder(child.Parent, "撤单");
                    if (retNode != null)
                    {
                        //定位撤单按钮
                        WindowIntPtr.CancelOrderIntPtr = retNode.Value.WindowIntPtr;

                    }
                    retNode = searchNodeByTitleAndOrder(child.Parent, "全撤(Z /)");
                    if (retNode != null)
                    {
                        //定位全撤(Z /)按钮
                        WindowIntPtr.CancelOrderAllIntPtr = retNode.Value.WindowIntPtr;

                    }
                    retNode = searchNodeByTitleAndOrder(child.Parent, "撤买(X)");
                    if (retNode != null)
                    {
                        //定位撤买(X)按钮
                        WindowIntPtr.CancelOrderBuyIntPtr = retNode.Value.WindowIntPtr;

                    }
                    retNode = searchNodeByTitleAndOrder(child.Parent, "撤卖(C)");
                    if (retNode != null)
                    {
                        //定位撤卖(C)按钮
                        WindowIntPtr.CancelOrderSellIntPtr = retNode.Value.WindowIntPtr;
                    }
                    //定位撤单CVirtualGrid
                    retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder + 5);
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                    }
                    retNode = searchNodeByOrder(retNode, 1);
                    if (retNode != null)
                    {
                        //定位撤单CVirtualGrid句柄
                        WindowIntPtr.CancelOrderCVirtualGridCtrlIntPtr = retNode.Value.WindowIntPtr;
                    }

                }
                if (window.WindowTitle.IndexOf("网上股票交易系统") >= 0)
                {
                    bool isSon = false;
                    //定位确定按钮
                    TreeNode<Window> retNode = searchNodeByOrder(child, 1);
                    if (retNode != null)
                    {
                        if ("请输入您的交易密码".Equals(retNode.Value.WindowTitle))
                        {
                            isSon = true;
                        }
                    }
                    //密码输入框
                    if (retNode != null && isSon)
                    {
                        retNode = searchNodeByOrder(child, 2);
                        if (retNode != null)
                        {
                            //密码输入框
                            WindowIntPtr.UnLockPasswordIntPtr = retNode.Value.WindowIntPtr;
                        }
                    }
                    //交易解锁确认按钮
                    if (retNode != null && isSon)
                    {
                        retNode = searchNodeByOrder(child, 3);
                        if (retNode != null)
                        {
                            //交易解锁确认按钮
                            WindowIntPtr.UnLockConfirmButtonIntPtr = retNode.Value.WindowIntPtr;
                        }
                    }
                    //交易解锁取消按钮
                    if (retNode != null && isSon)
                    {
                        retNode = searchNodeByOrder(child, 4);
                        if (retNode != null)
                        {
                            //交易解锁取消按钮
                            WindowIntPtr.UnLockCancelButtonIntPtr = retNode.Value.WindowIntPtr;
                        }
                    }
                    //交易解锁退出按钮
                    if (retNode != null && isSon)
                    {
                        retNode = searchNodeByOrder(child, 7);
                        if (retNode != null)
                        {
                            //交易解锁退出按钮
                            WindowIntPtr.UnLockQuitButtonIntPtr = retNode.Value.WindowIntPtr;
                        }
                    }
                    //定位窗口左下角的SysTreeView32
                    retNode = searchNodeByOrder(child, 1);
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 2);
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 4);
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                    }
                    //定位窗口左下角的SysTreeView32
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                        if (retNode != null)
                        {
                            WindowIntPtr.SysTreeView32 = retNode.Value.WindowIntPtr;
                        }
                    }
                    //定位窗口左下角的SysTreeView32IntradayTransaction
                    retNode = searchNodeByOrder(child, 1);
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 2);
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 3);
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                    }
                    //定位窗口左下角的SysTreeView32IntradayTransaction
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                        if (retNode != null)
                        {
                            WindowIntPtr.SysTreeView32IntradayTransaction = retNode.Value.WindowIntPtr;
                        }
                    }
                    ////资金股票CVirtualGridCtrl类句柄
                    //retNode = searchNodeByOrder(child, 1);
                    //if (retNode != null)
                    //{
                    //    retNode = searchNodeByOrder(retNode, 6);
                    //}
                    //if (retNode != null)
                    //{
                    //    retNode = searchNodeByOrder(retNode, 58);
                    //}
                    //if (retNode != null)
                    //{
                    //    retNode = searchNodeByOrder(retNode, 1);
                    //}
                    ////资金股票CVirtualGridCtrl类句柄
                    //if (retNode != null)
                    //{
                    //    retNode = searchNodeByOrder(retNode, 1);
                    //    if (retNode != null)
                    //    {
                    //        WindowIntPtr.CapitalSecurity = retNode.Value.WindowIntPtr;
                    //    }
                    //}
                }
                if (window.WindowTitle.IndexOf("可用金额") >= 0)
                {
                    bool isBrother = false;
                    //定位可用金额、冻结金额、股票市值、总资产
                    TreeNode<Window> retNode = searchNodeByOrder(child.Parent, child.Value.WindowOrder + 1);
                    if (retNode != null)
                    {
                        if ("股票市值".Equals(retNode.Value.WindowTitle))
                        {
                            isBrother = true;
                        }
                    }
                    //刷新按钮
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 1);
                        if (retNode != null)
                        {
                            //刷新按钮
                            WindowIntPtr.RefreshButtonIntPtr = retNode.Value.WindowIntPtr;
                        }
                    }
                    //可用金额
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 5);
                        if (retNode != null)
                        {
                            //可用金额
                            WindowIntPtr.AvailableAmountIntPtr = retNode.Value.WindowIntPtr;
                            this.accountInfo.AvailableAmount = Common.stringToFloat(retNode.Value.WindowTitle);
                        }
                    }
                    //股票市值
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 6);
                        if (retNode != null)
                        {
                            WindowIntPtr.MarketValueIntPtr = retNode.Value.WindowIntPtr;
                            this.accountInfo.MarketValue = Common.stringToFloat(retNode.Value.WindowTitle);
                        }
                    }
                    //总资产
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(child.Parent, 7);
                        if (retNode != null)
                        {
                            WindowIntPtr.TotalAssetsIntPtr = retNode.Value.WindowIntPtr;
                            this.accountInfo.TotalAssets = Common.stringToFloat(retNode.Value.WindowTitle);
                        }
                    }
                    //冻结金额
                    if (retNode != null && isBrother)
                    {
                        retNode = searchNodeByOrder(retNode.Parent, 9);
                        if (retNode != null)
                        {
                            WindowIntPtr.FreezeAmountIntPtr = retNode.Value.WindowIntPtr;
                            this.accountInfo.FreezeAmount = Common.stringToFloat(retNode.Value.WindowTitle);
                        }
                    }
                }
                if (window.WindowTitle.IndexOf("- 同花顺(") >= 0)
                {
                    //定位工具栏的买入卖出按钮
                    //同花顺的第四个子节点
                    TreeNode<Window> retNode = searchNodeByOrder(child, 4);
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 5);
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                    }
                    if (retNode != null)
                    {
                        //工具栏的买按钮
                        WindowIntPtr.ToolBarBuyButtonIntPtr = retNode.Value.WindowIntPtr;
                    }
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode.Parent, 2);
                    }
                    if (retNode != null)
                    {
                        //工具栏的买按钮
                        WindowIntPtr.ToolBarSellButtonIntPtr = retNode.Value.WindowIntPtr;
                    }
                }
                if (window.WindowTitle.IndexOf("用户登录") >= 0)
                {
                    //用户登录的第一个子节点
                    TreeNode<Window> retNode = searchNodeByOrder(child, 1);
                    if (retNode != null)
                    {
                        retNode = searchNodeByOrder(retNode, 1);
                    }
                    if (retNode != null)
                    {
                        //账号
                        WindowIntPtr.AccountIntPtr = retNode.Value.WindowIntPtr;
                    }
                    //用户密码
                    retNode = searchNodeByOrder(child, 2);
                    if (retNode != null)
                    {
                        //账号密码
                        WindowIntPtr.AccountPasswordIntPtr = retNode.Value.WindowIntPtr;
                    }
                    //用户登录确定按钮
                    retNode = searchNodeByOrder(child, 11);
                    if (retNode != null)
                    {
                        //用户登录确定按钮
                        WindowIntPtr.UserLoginButtonIntPtr = retNode.Value.WindowIntPtr;
                    }
                }
                if (window.WindowTitle.IndexOf("查询当前委托") >= 0)
                {
                    foreach (var brother in child.Parent.Children)
                    {
                        Window brotherWindow = brother.Value;
                        //提示确定按钮
                        if (brotherWindow.WindowOrder == 1)
                        {
                            WindowIntPtr.PromptConfirmButtonIntPtr = brotherWindow.WindowIntPtr;
                            WindowIntPtr.PromptConfirmButtonParentIntPtr = brother.Parent.Value.WindowIntPtr;
                        }
                    }
                }
                foreachTree(child);
            }
        }
        public TreeNode<Window> searchNodeByOrder(TreeNode<Window> node, int order)
        {
            foreach (var child in node.Children)
            {
                Window window = child.Value;

                if (window.WindowOrder == order)
                {
                    return child;
                }
            }
            return null;
        }
        public bool searchNodeByTitle(TreeNode<Window> node, string title)
        {
            bool retBool = false;
            foreach (var child in node.Children)
            {
                Window window = child.Value;

                if (window.WindowTitle.IndexOf(title) >= 0)
                {
                    retBool = true;
                    break;
                }
            }
            return retBool;
        }
        public TreeNode<Window> searchNodeByTitleAndOrder(TreeNode<Window> node, string title, int startOrder = 0)
        {
            foreach (var child in node.Children)
            {
                Window window = child.Value;

                if (window.WindowTitle.IndexOf(title) >= 0 &&
                    window.WindowOrder > startOrder)
                {
                    return child;
                }
            }
            return null;
        }
    }
}
