using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    internal class WindowIntPtr
    {
        #region field
        private IntPtr buySymbolIntPtr;           //买入股票代码句柄
        private IntPtr buyPriceIntPtr;           //买入股票价格句柄
        private IntPtr buyQuantityIntPtr;        //买入股票数量句柄
        private IntPtr buyButtonIntPtr;                     //买入按钮句柄
        private IntPtr sellSymbolIntPtr;           //卖出股票代码句柄
        private IntPtr sellPriceIntPtr;           //卖出股票价格句柄
        private IntPtr sellQuantityIntPtr;        //卖出股票数量句柄
        private IntPtr sellButtonIntPtr;          //卖出按钮句柄
        private IntPtr unLockButtonIntPtr;          //交易解锁按钮句柄
        private IntPtr unLockConfirmButtonIntPtr;          //交易解锁确认按钮句柄
        private IntPtr unLockPasswordIntPtr;          //交易解锁密码句柄
        private IntPtr unLockCancelButtonIntPtr;          //交易解锁取消句柄
        private IntPtr unLockQuitButtonIntPtr;          //交易解锁退出句柄
        private IntPtr afxControlBar42sIntPtr;          //类为AfxControlBar42s的句柄
        private IntPtr nodeBySortIntPtr;          //用序号获取到的句柄
        private IntPtr toolBarBuyButtonIntPtr;                     //工具栏的买入按钮句柄
        private IntPtr toolBarSellButtonIntPtr;          //工具栏的卖出按钮句柄
        private IntPtr resetBuyButtonIntPtr;                     //重填买入按钮句柄
        private IntPtr resetSellButtonIntPtr;          //重填卖出按钮句柄
        private IntPtr availableAmountTitleIntPtr;          //可用金额标题句柄
        private IntPtr availableAmountIntPtr;          //可用金额句柄
        private IntPtr freezeAmountIntPtr;          //冻结金额句柄
        private IntPtr marketValueIntPtr;          //股票市值句柄
        private IntPtr totalAssetsIntPtr;          //总 资 产句柄
        private IntPtr accountIntPtr;          //账号句柄
        private IntPtr accountPasswordIntPtr;          //账号密码句柄
        private IntPtr userLoginButtonIntPtr;          //用户登录按钮句柄
        private IntPtr promptConfirmButtonIntPtr;          //提示按钮句柄
        private IntPtr promptConfirmButtonParentIntPtr;          //提示按钮句柄
        private IntPtr refreshButtonIntPtr;          //刷新按钮
        private IntPtr buySymbolF6IntPtr;           //买入股票代码句柄
        private IntPtr buyPriceF6IntPtr;           //买入股票价格句柄
        private IntPtr buyQuantityF6IntPtr;        //买入股票数量句柄
        private IntPtr buyButtonF6IntPtr;                     //买入按钮句柄
        private IntPtr buyButtonF6ParentIntPtr;                     //买入按钮父窗口句柄
        private IntPtr sellSymbolF6IntPtr;           //卖出股票代码句柄
        private IntPtr sellPriceF6IntPtr;           //卖出股票价格句柄
        private IntPtr sellQuantityF6IntPtr;        //卖出股票数量句柄
        private IntPtr sellButtonF6IntPtr;          //卖出按钮句柄
        private IntPtr buyMarketF6IntPtr;           //买入股票交易所
        private IntPtr sellMarketF6IntPtr;           //卖出股票交易所
        private IntPtr entrustmentPromptConfirmButtonIntPtr;        //委托提示确定按钮句柄
        private IntPtr custom1IntPtr;        //定制窗口1句柄
        private IntPtr verifyCodeIntPtr;          //验证码句柄
        private IntPtr verifyCodeConfirmButtonIntPtr;          //验证码确定按钮句柄
        private IntPtr verifyCodeCancelButtonIntPtr;          //验证码取消按钮句柄
        private IntPtr sysTreeView32;           //屏幕左下角sysTreeView32类句柄
        private IntPtr sysTreeView32IntradayTransaction;           //sysTreeView32当日成交类句柄
        private IntPtr capitalSecurity;           //资金股票 CVirtualGridCtrl类句柄
        private IntPtr intradayEntrustment;           //当日委托 CVirtualGridCtrl类句柄
        private IntPtr intradayTransaction;           //当日成交 CVirtualGridCtrl类句柄
        private IntPtr cancelOrderSelectAllIntPtr;    //全部选中句柄
        private IntPtr cancelOrderIntPtr;          //撤单句柄
        private IntPtr cancelOrderAllIntPtr;          //全撤(Z /)句柄
        private IntPtr cancelOrderBuyIntPtr;          //撤买(X)句柄
        private IntPtr cancelOrderSellIntPtr;          //撤卖(C)句柄
        private IntPtr cancelOrderCVirtualGridCtrlIntPtr;          //撤单CVirtualGridCtrl句柄
        private IntPtr cancelOrderConfirmButtonIntPtr;          //撤单提示确认按钮句柄
        private IntPtr cancelOrderConfirmButtonParentIntPtr;          //撤单提示确认按钮父窗口句柄
        private IntPtr cancelOrderCancelButtonIntPtr;          //撤单提示取消按钮句柄
        private IntPtr custom2CVirtualGridCtrlIntPtr;        //标题为"Custom2"且类为CVirtualGridCtrl的窗口(持仓(W)、成交(E)、委托(R))
        #endregion
        #region properity
        //买入股票代码句柄
        public IntPtr BuySymbolIntPtr
        {
            get { return this.buySymbolIntPtr; }
            set { this.buySymbolIntPtr = value; }
        }
        //买入股票价格句柄
        public IntPtr BuyPriceIntPtr
        {
            get { return this.buyPriceIntPtr; }
            set { this.buyPriceIntPtr = value; }
        }
        //买入股票数量句柄
        public IntPtr BuyQuantityIntPtr
        {
            get { return this.buyQuantityIntPtr; }
            set { this.buyQuantityIntPtr = value; }
        }
        //买入按钮句柄
        public IntPtr BuyButtonIntPtr
        {
            get { return this.buyButtonIntPtr; }
            set { this.buyButtonIntPtr = value; }
        }
        //卖出股票代码句柄
        public IntPtr SellSymbolIntPtr
        {
            get { return this.sellSymbolIntPtr; }
            set { this.sellSymbolIntPtr = value; }
        }
        //卖出股票价格句柄
        public IntPtr SellPriceIntPtr
        {
            get { return this.sellPriceIntPtr; }
            set { this.sellPriceIntPtr = value; }
        }
        //卖出股票数量句柄
        public IntPtr SellQuantityIntPtr
        {
            get { return this.sellQuantityIntPtr; }
            set { this.sellQuantityIntPtr = value; }
        }
        //卖出按钮句柄
        public IntPtr SellButtonIntPtr
        {
            get { return this.sellButtonIntPtr; }
            set { this.sellButtonIntPtr = value; }
        }
        //交易锁定按钮句柄
        public IntPtr UnLockButtonIntPtr
        {
            get { return this.unLockButtonIntPtr; }
            set { this.unLockButtonIntPtr = value; }
        }
        //交易解锁确认按钮句柄
        public IntPtr UnLockConfirmButtonIntPtr
        {
            get { return this.unLockConfirmButtonIntPtr; }
            set { this.unLockConfirmButtonIntPtr = value; }
        }
        //交易解锁取消按钮句柄
        public IntPtr UnLockCancelButtonIntPtr
        {
            get { return this.unLockCancelButtonIntPtr; }
            set { this.unLockCancelButtonIntPtr = value; }
        }
        //交易解锁退出按钮句柄
        public IntPtr UnLockQuitButtonIntPtr
        {
            get { return this.unLockQuitButtonIntPtr; }
            set { this.unLockQuitButtonIntPtr = value; }
        }

        //交易解锁密码句柄
        public IntPtr UnLockPasswordIntPtr
        {
            get { return this.unLockPasswordIntPtr; }
            set { this.unLockPasswordIntPtr = value; }
        }
        //类AfxControlBar42句柄
        public IntPtr AfxControlBar42sIntPtr
        {
            get { return this.afxControlBar42sIntPtr; }
            set { this.afxControlBar42sIntPtr = value; }
        }
        //用序号获取到的句柄
        public IntPtr NodeBySortIntPtr
        {
            get { return this.nodeBySortIntPtr; }
            set { this.nodeBySortIntPtr = value; }
        }
        //买入按钮句柄
        public IntPtr ToolBarBuyButtonIntPtr
        {
            get { return this.toolBarBuyButtonIntPtr; }
            set { this.toolBarBuyButtonIntPtr = value; }
        }
        //工具栏的卖出按钮句柄
        public IntPtr ToolBarSellButtonIntPtr
        {
            get { return this.toolBarSellButtonIntPtr; }
            set { this.toolBarSellButtonIntPtr = value; }
        }
        //重填买入按钮句柄
        public IntPtr ResetBuyButtonIntPtr
        {
            get { return this.resetBuyButtonIntPtr; }
            set { this.resetBuyButtonIntPtr = value; }
        }
        //重填卖出按钮句柄
        public IntPtr ResetSellButtonIntPtr
        {
            get { return this.resetSellButtonIntPtr; }
            set { this.resetSellButtonIntPtr = value; }
        }
        //可用金额标题句柄
        public IntPtr AvailableAmountTitleIntPtr
        {
            get { return this.availableAmountTitleIntPtr; }
            set { this.availableAmountTitleIntPtr = value; }
        }
        //可用金额句柄
        public IntPtr AvailableAmountIntPtr
        {
            get { return this.availableAmountIntPtr; }
            set { this.availableAmountIntPtr = value; }
        }
        //冻结金额句柄
        public IntPtr FreezeAmountIntPtr
        {
            get { return this.freezeAmountIntPtr; }
            set { this.freezeAmountIntPtr = value; }
        }
        //股票市值句柄
        public IntPtr MarketValueIntPtr
        {
            get { return this.marketValueIntPtr; }
            set { this.marketValueIntPtr = value; }
        }
        //总 资 产句柄
        public IntPtr TotalAssetsIntPtr
        {
            get { return this.totalAssetsIntPtr; }
            set { this.totalAssetsIntPtr = value; }
        }
        //账号句柄
        public IntPtr AccountIntPtr
        {
            get { return this.accountIntPtr; }
            set { this.accountIntPtr = value; }
        }
        //账号密码句柄
        public IntPtr AccountPasswordIntPtr
        {
            get { return this.accountPasswordIntPtr; }
            set { this.accountPasswordIntPtr = value; }
        }
        //用户登录按钮句柄
        public IntPtr UserLoginButtonIntPtr
        {
            get { return this.userLoginButtonIntPtr; }
            set { this.userLoginButtonIntPtr = value; }
        }
        //提示按钮句柄
        public IntPtr PromptConfirmButtonIntPtr
        {
            get { return this.promptConfirmButtonIntPtr; }
            set { this.promptConfirmButtonIntPtr = value; }
        }
        //提示按钮句柄
        public IntPtr PromptConfirmButtonParentIntPtr
        {
            get { return this.promptConfirmButtonParentIntPtr; }
            set { this.promptConfirmButtonParentIntPtr = value; }
        }
        //刷新按钮
        public IntPtr RefreshButtonIntPtr
        {
            get { return this.refreshButtonIntPtr; }
            set { this.refreshButtonIntPtr = value; }
        }
        //买入股票代码句柄
        public IntPtr BuySymbolF6IntPtr
        {
            get { return this.buySymbolF6IntPtr; }
            set { this.buySymbolF6IntPtr = value; }
        }
        //买入股票价格句柄
        public IntPtr BuyPriceF6IntPtr
        {
            get { return this.buyPriceF6IntPtr; }
            set { this.buyPriceF6IntPtr = value; }
        }
        //买入股票数量句柄
        public IntPtr BuyQuantityF6IntPtr
        {
            get { return this.buyQuantityF6IntPtr; }
            set { this.buyQuantityF6IntPtr = value; }
        }
        //买入按钮句柄
        public IntPtr BuyButtonF6IntPtr
        {
            get { return this.buyButtonF6IntPtr; }
            set { this.buyButtonF6IntPtr = value; }
        }
        //买入按钮父窗口句柄
        public IntPtr BuyButtonF6ParentIntPtr
        {
            get { return this.buyButtonF6ParentIntPtr; }
            set { this.buyButtonF6ParentIntPtr = value; }
        }

        //卖出股票代码句柄
        public IntPtr SellSymbolF6IntPtr
        {
            get { return this.sellSymbolF6IntPtr; }
            set { this.sellSymbolF6IntPtr = value; }
        }
        //卖出股票价格句柄
        public IntPtr SellPriceF6IntPtr
        {
            get { return this.sellPriceF6IntPtr; }
            set { this.sellPriceF6IntPtr = value; }
        }
        //卖出股票数量句柄
        public IntPtr SellQuantityF6IntPtr
        {
            get { return this.sellQuantityF6IntPtr; }
            set { this.sellQuantityF6IntPtr = value; }
        }
        //卖出按钮句柄
        public IntPtr SellButtonF6IntPtr
        {
            get { return this.sellButtonF6IntPtr; }
            set { this.sellButtonF6IntPtr = value; }
        }
        //买入股票交易所
        public IntPtr BuyMarketF6IntPtr
        {
            get { return this.buyMarketF6IntPtr; }
            set { this.buyMarketF6IntPtr = value; }
        }
        //卖出股票交易所
        public IntPtr SellMarketF6IntPtr
        {
            get { return this.sellMarketF6IntPtr; }
            set { this.sellMarketF6IntPtr = value; }
        }
        //委托提示确定按钮句柄
        public IntPtr EntrustmentPromptConfirmButtonIntPtr
        {
            get { return this.entrustmentPromptConfirmButtonIntPtr; }
            set { this.entrustmentPromptConfirmButtonIntPtr = value; }
        }
        //卖出股票交易所
        public IntPtr Custom1IntPtr
        {
            get { return this.custom1IntPtr; }
            set { this.custom1IntPtr = value; }
        }
        //验证码句柄
        public IntPtr VerifyCodeIntPtr
        {
            get { return this.verifyCodeIntPtr; }
            set { this.verifyCodeIntPtr = value; }
        }
        //验证码确定按钮句柄
        public IntPtr VerifyCodeConfirmButtonIntPtr
        {
            get { return this.verifyCodeConfirmButtonIntPtr; }
            set { this.verifyCodeConfirmButtonIntPtr = value; }
        }
        //验证码取消按钮句柄
        public IntPtr VerifyCodeCancelButtonIntPtr
        {
            get { return this.verifyCodeCancelButtonIntPtr; }
            set { this.verifyCodeCancelButtonIntPtr = value; }
        }
        //屏幕左下角sysTreeView32类句柄
        public IntPtr SysTreeView32
        {
            get { return this.sysTreeView32; }
            set { this.sysTreeView32 = value; }
        }
        //屏幕左下角sysTreeView32类当日成交句柄
        public IntPtr SysTreeView32IntradayTransaction
        {
            get { return this.sysTreeView32IntradayTransaction; }
            set { this.sysTreeView32IntradayTransaction = value; }
        }
        //资金股票 CVirtualGridCtrl类句柄
        public IntPtr CapitalSecurity
        {
            get { return this.capitalSecurity; }
            set { this.capitalSecurity = value; }
        }
        //当日委托 CVirtualGridCtrl类句柄
        public IntPtr IntradayEntrustment
        {
            get { return this.intradayEntrustment; }
            set { this.intradayEntrustment = value; }
        }
        //当日成交 CVirtualGridCtrl类句柄
        public IntPtr IntradayTransaction
        {
            get { return this.intradayTransaction; }
            set { this.intradayTransaction = value; }
        }
        //全部选中句柄
        public IntPtr CancelOrderSelectAllIntPtr
        {
            get { return this.cancelOrderSelectAllIntPtr; }
            set { this.cancelOrderSelectAllIntPtr = value; }
        }
        //撤单句柄
        public IntPtr CancelOrderIntPtr
        {
            get { return this.cancelOrderIntPtr; }
            set { this.cancelOrderIntPtr = value; }
        }
        //全撤(Z /)句柄
        public IntPtr CancelOrderAllIntPtr
        {
            get { return this.cancelOrderAllIntPtr; }
            set { this.cancelOrderAllIntPtr = value; }
        }
        //撤买(X)句柄
        public IntPtr CancelOrderBuyIntPtr
        {
            get { return this.cancelOrderBuyIntPtr; }
            set { this.cancelOrderBuyIntPtr = value; }
        }
        //撤卖(C)句柄
        public IntPtr CancelOrderSellIntPtr
        {
            get { return this.cancelOrderSellIntPtr; }
            set { this.cancelOrderSellIntPtr = value; }
        }
        //撤单CVirtualGrid句柄
        public IntPtr CancelOrderCVirtualGridCtrlIntPtr
        {
            get { return this.cancelOrderCVirtualGridCtrlIntPtr; }
            set { this.cancelOrderCVirtualGridCtrlIntPtr = value; }
        }
        //撤单提示确认按钮句柄
        public IntPtr CancelOrderConfirmButtonIntPtr
        {
            get { return this.cancelOrderConfirmButtonIntPtr; }
            set { this.cancelOrderConfirmButtonIntPtr = value; }
        }
        //撤单提示确认按钮父窗口句柄
        public IntPtr CancelOrderConfirmButtonParentIntPtr
        {
            get { return this.cancelOrderConfirmButtonParentIntPtr; }
            set { this.cancelOrderConfirmButtonParentIntPtr = value; }
        }
        //撤单提示取消按钮句柄
        public IntPtr CancelOrderCancelButtonIntPtr
        {
            get { return this.cancelOrderCancelButtonIntPtr; }
            set { this.cancelOrderCancelButtonIntPtr = value; }
        }
        //标题为"Custom2"且类为CVirtualGridCtrl的窗口(持仓(W)、成交(E)、委托(R))
        public IntPtr Custom2CVirtualGridCtrlIntPtr
        {
            get { return this.custom2CVirtualGridCtrlIntPtr; }
            set { this.custom2CVirtualGridCtrlIntPtr = value; }
        }
        #endregion
    }
}
