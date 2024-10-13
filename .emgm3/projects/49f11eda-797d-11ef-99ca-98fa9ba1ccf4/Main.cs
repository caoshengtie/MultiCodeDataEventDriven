using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMSDK;

namespace MultiCodeDataEventDriven
{
    public partial class Main : Form
    {
        MyStrategy myStrategy;  //我的策略
        public Main()
        {
            InitializeComponent();
            //初始化量化交易策略
            InitializeStrategy();
        }

        private void BuyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //同花顺交易                   
            Trade traden = new Trade();
            string transactionQuantity = traden.getTransactionQuantity("SZSE.002738", "36.54");
            int retStatus = traden.buyF6("SZSE.002738", "36.54", transactionQuantity);

        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出当前程序吗？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void CheckTHSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trade trade = new Trade();
            //检查同花顺交易软件
            trade.checkTHS();

        }
        private void InitializeStrategy()
        {
            StrategyMode strategyMode = StrategyMode.MODE_LIVE;              //策略运行模式
            //策略运行模式
            //实时模式
            if (Const.MODE_LIVE.Equals(Common.getParameterValue(Const.STRATEGY_MODE)))
            {
                //实时模式
                strategyMode = StrategyMode.MODE_LIVE;
            }
            //回测模式
            if (Const.MODE_BACKTEST.Equals(Common.getParameterValue(Const.STRATEGY_MODE)))
            {
                //回测模式
                strategyMode = StrategyMode.MODE_BACKTEST;
            }
            //令牌
            string token = Common.getParameterValue(Const.TOKEN);
            //策略编号
            string strategyId = Common.getParameterValue(Const.STRATEGY_ID);

            myStrategy = new MyStrategy(token, strategyId, strategyMode);
            //s.SetBacktestConfig("2024-06-25 09:30:00", "2024-09-30 15:00:00");
            //s.Run();
            //System.Console.WriteLine("回测完成！");
            //System.Console.Read();
        }

        private void CancelOrderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Trade trade = new Trade();
            trade.cancelOrder();

        }

        private void SellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //同花顺交易                   
            Trade trade = new Trade();
            string sellQuantity = trade.getSellQuantity("SZSE.002738");
            int retStatus = trade.sellF6("SZSE.002738", "36.54", sellQuantity);

        }

        private void LimitUpFundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trade trade = new Trade();
            //封板资金
            DataDownload dataDownload = new DataDownload();
            dataDownload.sealingFund(trade.TradeInfomation.TradingDate.data[trade.TradeInfomation.TradingDate.data.Count - 1]);
        }

        private void ParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParameterSettings parameterSettings = new ParameterSettings();
            parameterSettings.ShowDialog();
        }

        private void StartBackTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //策略运行模式
            //回测模式
            if (!Const.MODE_BACKTEST.Equals(Common.getParameterValue(Const.STRATEGY_MODE)))
            {
                //回测模式
                MessageBox.Show("当前策略运行模式不是回测模式，请通过菜单【功能】->【参数设置】将回测模式设置为Y！");
                return;
            }
            //回测开始时间
            string backTestStartTime = Common.getParameterValue(Const.BACK_TEST_START_TIME);
            //回测结束时间
            string backTestEndTime = Common.getParameterValue(Const.BACK_TEST_END_TIME);

            myStrategy.SetBacktestConfig(backTestStartTime, backTestEndTime);
            myStrategy.Run();
            System.Console.WriteLine("回测完成！");
        }

        private void StartRealtimeTradingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //策略运行模式
            //实时模式
            if (!Const.MODE_LIVE.Equals(Common.getParameterValue(Const.STRATEGY_MODE)))
            {
                //实时模式
                MessageBox.Show("当前策略运行模式不是实时模式，请通过菜单【功能】->【参数设置】将实时模式设置为Y！");
                return;
            }
            //回测开始时间
            string backTestStartTime = Common.getParameterValue(Const.BACK_TEST_START_TIME);
            //回测结束时间
            string backTestEndTime = Common.getParameterValue(Const.BACK_TEST_END_TIME);

            myStrategy.SetBacktestConfig(backTestStartTime, backTestEndTime);
            myStrategy.Run();
        }

        private void SetupCustomSecuritiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupCustomSecurities setupCustomSecurities = new SetupCustomSecurities();
            setupCustomSecurities.ShowDialog();

        }
    }
}
