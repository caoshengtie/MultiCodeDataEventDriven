using GMSDK;
using rtconf.api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MultiCodeDataEventDriven
{
    public partial class SetupCustomSecurities : Form
    {
        public SetupCustomSecurities()
        {
            InitializeComponent();
        }

        private void SetupCustomSecurities_Load(object sender, EventArgs e)
        {

            //查询数据类型表
            string sql = " select dictName,dictValue from tbDictEntry where dictTypeID = '" + Const.SECURITIES_EXCHANGE + "'  order by sortNo ";
            DataTable dataTableSecuritiesExchange = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];

            //证券交易所
            SecuritiesExchangeSelectcomboBox.DataSource = dataTableSecuritiesExchange;
            SecuritiesExchangeSelectcomboBox.DisplayMember = "dictName"; // 显示的列名
            SecuritiesExchangeSelectcomboBox.ValueMember = "dictValue"; // 绑定的值列名

            //查询数据类型表
            sql = " select dictName,dictValue from tbDictEntry where dictTypeID = '" + Const.SECURITIES_BLOCK + "'  order by sortNo ";
            DataTable dataTableSecuritiesBlock = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];

            //证券板块
            SecuritiesBlockComboBox.DataSource = dataTableSecuritiesBlock;
            SecuritiesBlockComboBox.DisplayMember = "dictName"; // 显示的列名
            SecuritiesBlockComboBox.ValueMember = "dictValue"; // 绑定的值列名


            //GMData<DataTable> constituents = Common.getConstituents(SecuritiesExchangeSelectcomboBox.SelectedValue.ToString(),
            //    SecuritiesBlockComboBox.SelectedValue.ToString());
            ////保存到数据库
            //Common.saveInstrumentinfos(constituents);

            ////查询数据字典表
            //sql = " select symbol, sec_type,exchange,sec_id,sec_name,price_tick,listed_date,delisted_date,weight from tbInstrumentinfos ";
            //DataTable dataTableInstrumentinfos = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];


            //SetupCustomSecuritiesDataGridView.DataSource = dataTableInstrumentinfos;
            ////设置表格标题
            //setCustomSecuritiesDataDataGridVieweHeaderText();
            setSetupCustomSecuritiesDataDataGridViewe(Const.SHSZSE, Const.ALL_BLOCK, SecuritieCodeTextBox.Text);
            ////查询数据字典表
            //sql = " select symbol,sec_name,positionPercent from tbSelectedTradeSymbols ";
            //DataTable dataTableSetupCustomSecurities = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];


            //SelectedTradeSymbolsDataGridView.DataSource = dataTableSetupCustomSecurities;
            ////设置表格标题
            //setSetupCustomSecuritiesDataDataGridVieweHeaderText();
            //设置交易自选股
            setSelectedTradeSymbolsDataDataGridView();
            //设置回测自选股
            setSelectedBackTestSymbolsDataGridView();

        }
        private void setCustomSecuritiesDataDataGridVieweHeaderText()
        {
            //设置列的宽度为100像素
            for (int i = 0; i < 9; i++)
            {
                SetupCustomSecuritiesDataGridView.Columns[i].Width = 100;
            }
            // 设置列标题的文本
            SetupCustomSecuritiesDataGridView.Columns[0].HeaderText = "标的代码";
            SetupCustomSecuritiesDataGridView.Columns[1].HeaderText = "标的种类";
            SetupCustomSecuritiesDataGridView.Columns[2].HeaderText = "交易所代码";
            SetupCustomSecuritiesDataGridView.Columns[3].HeaderText = "代码";
            SetupCustomSecuritiesDataGridView.Columns[4].HeaderText = "名称";
            SetupCustomSecuritiesDataGridView.Columns[5].HeaderText = "最小变动单位";
            SetupCustomSecuritiesDataGridView.Columns[6].HeaderText = "上市日期";
            SetupCustomSecuritiesDataGridView.Columns[7].HeaderText = "退市日期";
            SetupCustomSecuritiesDataGridView.Columns[8].HeaderText = "权重";
        }
        private void setSelectedTradeSymbolsDataDataGridVieweHeaderText()
        {
            //设置列的宽度为100像素
            for (int i = 0; i < 3; i++)
            {
                SelectedTradeSymbolsDataGridView.Columns[i].Width = 100;
            }
            // 设置列标题的文本
            SelectedTradeSymbolsDataGridView.Columns[0].HeaderText = "标的代码";
            SelectedTradeSymbolsDataGridView.Columns[1].HeaderText = "标的名称";
            SelectedTradeSymbolsDataGridView.Columns[2].HeaderText = "仓位比例";

        }
        private void setSelectedBackTestSymbolsDataGridViewHeaderText()
        {
            //设置列的宽度为100像素
            for (int i = 0; i < 3; i++)
            {
                SelectedBackTestSymbolsDataGridView.Columns[i].Width = 100;
            }
            // 设置列标题的文本
            SelectedBackTestSymbolsDataGridView.Columns[0].HeaderText = "标的代码";
            SelectedBackTestSymbolsDataGridView.Columns[1].HeaderText = "标的名称";
            SelectedBackTestSymbolsDataGridView.Columns[2].HeaderText = "仓位比例";

        }
        private void SecuritiesBlockComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!"System.Data.DataRowView".Equals(SecuritiesExchangeSelectcomboBox.SelectedValue.ToString())
            //     && !"System.Data.DataRowView".Equals(SecuritiesBlockComboBox.SelectedValue.ToString()))
            //{
            //    setSetupCustomSecuritiesDataDataGridViewe(SecuritiesExchangeSelectcomboBox.SelectedValue.ToString(),
            //    SecuritiesBlockComboBox.SelectedValue.ToString(), SecuritieCodeTextBox.Text);
            //}
            //else
            //{
            //    setSetupCustomSecuritiesDataDataGridViewe(Const.SHSZSE, Const.ALL_BLOCK, SecuritieCodeTextBox.Text);
            //}
            queryTbSelectedTradeSymbols();
        }
        private void setSetupCustomSecuritiesDataDataGridViewe(string securitiesExchange, string securitiesBlock, string symbol)
        {
            //查询交易标的基本信息
            string sql = " select symbol, sec_type,exchange,sec_id,sec_name,price_tick,listed_date,delisted_date,weight from tbInstrumentinfos "
                + Common.createQueryCriteria(securitiesExchange, securitiesBlock, symbol);

            DataTable dataTableInstrumentinfos = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];

            SetupCustomSecuritiesDataGridView.DataSource = dataTableInstrumentinfos;
            //设置表格标题
            setCustomSecuritiesDataDataGridVieweHeaderText();
        }
        private void setSelectedTradeSymbolsDataDataGridView()
        {
            //查询交易自选股
            string sql = " select symbol,sec_name,positionPercent from tbSelectedTradeSymbols ";
            DataTable dataTableSelectedTradeSymbols = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];


            SelectedTradeSymbolsDataGridView.DataSource = dataTableSelectedTradeSymbols;
            //设置表格标题
            setSelectedTradeSymbolsDataDataGridVieweHeaderText();
        }
        private void setSelectedBackTestSymbolsDataGridView()
        {
            //查询回测自选股
            string sql = " select symbol,sec_name,positionPercent from tbSelectedBackTestSymbols ";
            DataTable dataTableSelectedBackTest = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];


            SelectedBackTestSymbolsDataGridView.DataSource = dataTableSelectedBackTest;
            //设置表格标题
            setSelectedBackTestSymbolsDataGridViewHeaderText();
        }
        private void SecuritiesExchangeSelectcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!"System.Data.DataRowView".Equals(SecuritiesExchangeSelectcomboBox.SelectedValue.ToString())
            //     && !"System.Data.DataRowView".Equals(SecuritiesBlockComboBox.SelectedValue.ToString())) {
            //    setSetupCustomSecuritiesDataDataGridViewe(SecuritiesExchangeSelectcomboBox.SelectedValue.ToString(),
            //        SecuritiesBlockComboBox.SelectedValue.ToString(), SecuritieCodeTextBox.Text);
            //}
            //else
            //{
            //    setSetupCustomSecuritiesDataDataGridViewe(Const.SHSZSE, Const.ALL_BLOCK, SecuritieCodeTextBox.Text);
            //}
            queryTbSelectedTradeSymbols();
        }

        private void DeleteAllButton_Click(object sender, EventArgs e)
        {
            //清除所有交易自选股
            SelectedTradeSymbolsDataGridView.AllowUserToAddRows = false;
            while (SelectedTradeSymbolsDataGridView.Rows.Count > 0)
            {
                SelectedTradeSymbolsDataGridView.Rows.RemoveAt(0); //
            }
            SelectedTradeSymbolsDataGridView.AllowUserToAddRows = true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // 数据源是一个DataTable
            DataTable dataTable = (DataTable)SelectedTradeSymbolsDataGridView.DataSource;

            foreach (DataGridViewRow rowSetupCustomSecurities in SetupCustomSecuritiesDataGridView.SelectedRows)
            {
                //在自选股中检查是否已经存在
                if (rowSetupCustomSecurities.Cells[0].Value != null && isExist(rowSetupCustomSecurities.Cells[0].Value.ToString()) == false)
                {
                    DataRow newRow = dataTable.NewRow();
                    // 填充新行的数据
                    newRow["symbol"] = rowSetupCustomSecurities.Cells[0].Value;
                    newRow["sec_name"] = rowSetupCustomSecurities.Cells[4].Value;
                    newRow["positionPercent"] = "0.00";
                    dataTable.Rows.Add(newRow);
                }
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            //先清空表tbInstrumentinfos
            String sqlStr = "  delete from tbSelectedTradeSymbols ";
            Common.ExeSqlCommand(sqlStr, null, CommandType.Text);

            foreach (DataGridViewRow row in SelectedTradeSymbolsDataGridView.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    //插入数据
                    sqlStr = "  insert into tbSelectedTradeSymbols(symbol,sec_name,positionPercent) values ('" + row.Cells[0].Value.ToString() + "','" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() + "')";
                    Common.ExeSqlCommand(sqlStr, null, CommandType.Text);
                }
            }
            MessageBox.Show("保存完成。");
        }
        private bool isExist(string symbol)
        {
            bool isExist = false;
            // 遍历SelectedTradeSymbolsDataGridView
            foreach (DataGridViewRow rowSelectedTradeSymbols in SelectedTradeSymbolsDataGridView.Rows)
            {
                if (symbol.Equals(rowSelectedTradeSymbols.Cells[0].Value))
                {
                    isExist = true;
                    break;
                }
            }
            //返回值
            return isExist;
        }
        private bool isExistSelectedBackTestSymbols(string symbol)
        {
            bool isExist = false;
            // 遍历SelectedBackTestSymbolsDataGridView
            foreach (DataGridViewRow rowSelectedBackTestSymbols in SelectedBackTestSymbolsDataGridView.Rows)
            {
                if (symbol.Equals(rowSelectedBackTestSymbols.Cells[0].Value))
                {
                    isExist = true;
                    break;
                }
            }
            //返回值
            return isExist;
        }
        private void QueryButton_Click(object sender, EventArgs e)
        {
            queryTbSelectedTradeSymbols();
        }
        private void queryTbSelectedTradeSymbols()
        {
            if (!"System.Data.DataRowView".Equals(SecuritiesExchangeSelectcomboBox.SelectedValue.ToString())
                 && !"System.Data.DataRowView".Equals(SecuritiesBlockComboBox.SelectedValue.ToString()))
            {
                setSetupCustomSecuritiesDataDataGridViewe(SecuritiesExchangeSelectcomboBox.SelectedValue.ToString(),
                    SecuritiesBlockComboBox.SelectedValue.ToString(), SecuritieCodeTextBox.Text);
            }
            else
            {
                setSetupCustomSecuritiesDataDataGridViewe(Const.SHSZSE, Const.ALL_BLOCK, SecuritieCodeTextBox.Text);
            }
        }

        private void AddAllButton_Click(object sender, EventArgs e)
        {
            // 数据源是一个DataTable
            DataTable dataTable = (DataTable)SelectedTradeSymbolsDataGridView.DataSource;

            foreach (DataGridViewRow rowSetupCustomSecurities in SetupCustomSecuritiesDataGridView.Rows)
            {
                //在自选股中检查是否已经存在
                if (rowSetupCustomSecurities.Cells[0].Value != null && isExist(rowSetupCustomSecurities.Cells[0].Value.ToString()) == false)
                {
                    DataRow newRow = dataTable.NewRow();
                    // 填充新行的数据
                    newRow["symbol"] = rowSetupCustomSecurities.Cells[0].Value;
                    newRow["sec_name"] = rowSetupCustomSecurities.Cells[4].Value;
                    newRow["positionPercent"] = "0.00";
                    dataTable.Rows.Add(newRow);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //清除所有交易自选股
            SelectedTradeSymbolsDataGridView.AllowUserToAddRows = false;
            foreach (DataGridViewRow rowSelectedTradeSymbols in SelectedTradeSymbolsDataGridView.SelectedRows)
            {
                SelectedTradeSymbolsDataGridView.Rows.Remove(rowSelectedTradeSymbols); //
            }
            SelectedTradeSymbolsDataGridView.AllowUserToAddRows = true;
        }

        private void BackTestDeleteAllButton_Click(object sender, EventArgs e)
        {
            //清除所有回测自选股
            SelectedBackTestSymbolsDataGridView.AllowUserToAddRows = false;
            while (SelectedBackTestSymbolsDataGridView.Rows.Count > 0)
            {
                SelectedBackTestSymbolsDataGridView.Rows.RemoveAt(0); //
            }
            SelectedBackTestSymbolsDataGridView.AllowUserToAddRows = true;
        }

        private void BackTestAddButton_Click(object sender, EventArgs e)
        {
            // 数据源是一个DataTable
            DataTable dataTable = (DataTable)SelectedBackTestSymbolsDataGridView.DataSource;

            foreach (DataGridViewRow rowCustomSecuritiesData in SetupCustomSecuritiesDataGridView.SelectedRows)
            {
                //在自选股中检查是否已经存在
                if (rowCustomSecuritiesData.Cells[0].Value != null && isExistSelectedBackTestSymbols(rowCustomSecuritiesData.Cells[0].Value.ToString()) == false)
                {
                    DataRow newRow = dataTable.NewRow();
                    // 填充新行的数据
                    newRow["symbol"] = rowCustomSecuritiesData.Cells[0].Value;
                    newRow["sec_name"] = rowCustomSecuritiesData.Cells[4].Value;
                    newRow["positionPercent"] = "0.00";
                    dataTable.Rows.Add(newRow);
                }
            }
        }

        private void BackTestAddAllButton_Click(object sender, EventArgs e)
        {
            // 数据源是一个DataTable
            DataTable dataTable = (DataTable)SelectedBackTestSymbolsDataGridView.DataSource;

            foreach (DataGridViewRow rowSetupCustomSecurities in SetupCustomSecuritiesDataGridView.Rows)
            {
                //在自选股中检查是否已经存在
                if (rowSetupCustomSecurities.Cells[0].Value != null && isExist(rowSetupCustomSecurities.Cells[0].Value.ToString()) == false)
                {
                    DataRow newRow = dataTable.NewRow();
                    // 填充新行的数据
                    newRow["symbol"] = rowSetupCustomSecurities.Cells[0].Value;
                    newRow["sec_name"] = rowSetupCustomSecurities.Cells[4].Value;
                    newRow["positionPercent"] = "0.00";
                    dataTable.Rows.Add(newRow);
                }
            }
        }

        private void BackTestDeleteButton_Click(object sender, EventArgs e)
        {
            //清除所有回测自选股
            SelectedBackTestSymbolsDataGridView.AllowUserToAddRows = false;
            foreach (DataGridViewRow rowSelectedBackTestSymbols in SelectedBackTestSymbolsDataGridView.SelectedRows)
            {
                SelectedBackTestSymbolsDataGridView.Rows.Remove(rowSelectedBackTestSymbols); //
            }
            SelectedBackTestSymbolsDataGridView.AllowUserToAddRows = true;
        }

        private void BackTestSaveButton_Click(object sender, EventArgs e)
        {
            //先清空表tbInstrumentinfos
            String sqlStr = "  delete from tbSelectedBackTestSymbols ";
            Common.ExeSqlCommand(sqlStr, null, CommandType.Text);

            foreach (DataGridViewRow row in SelectedBackTestSymbolsDataGridView.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    //插入数据
                    sqlStr = "  insert into tbSelectedBackTestSymbols(symbol,sec_name,positionPercent) values ('" + row.Cells[0].Value.ToString() + "','" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() + "')";
                    Common.ExeSqlCommand(sqlStr, null, CommandType.Text);
                }
            }
            MessageBox.Show("保存完成。");
        }
    }
}
