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
    public partial class ParameterSettings : Form
    {
        public ParameterSettings()
        {
            InitializeComponent();
        }

        private void ParameterSettings_Load(object sender, EventArgs e)
        {
            //查询数据类型表
            string sql = " select dictTypeID,dictTypeName from tbDictType  order by dictTypeID ";
            DataTable dataTableDictType = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];

            parameterSelectcomboBox.DataSource = dataTableDictType;
            parameterSelectcomboBox.DisplayMember = "dictTypeName"; // 显示的列名
            parameterSelectcomboBox.ValueMember = "dictTypeID"; // 绑定的值列名

            setDataGridViewe();

        }

        private void parameterSelectcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!"System.Data.DataRowView".Equals(parameterSelectcomboBox.SelectedValue.ToString()))
            {
                setDataGridViewe();
            }

        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            //string dictTypeID = getParameterID(parameterValuecomboBox.SelectedValue.ToString());

            ////更新参数表
            //String sqlStr = "  update tbParameter set parameterValue = '" + parameterValuecomboBox.SelectedValue.ToString() + "' where parameterID = '" + dictTypeID + "' ";
            //Common.ExeSqlCommand(sqlStr, null, CommandType.Text);
            foreach (DataGridViewRow row in DictEntryDataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (row.Cells[0].Value!=null) {
                        //更新参数值
                        String sqlStr = "  update tbDictEntry set dictValue = '" + row.Cells[3].Value.ToString() + "', isUsedAsParameterValues = '" + row.Cells[5].Value.ToString() + "' where dictTypeID = '" + row.Cells[0].Value.ToString() + "' and dictId = '" + row.Cells[1].Value.ToString() + "' ";
                        Common.ExeSqlCommand(sqlStr, null, CommandType.Text);
                    }
                }
            }
            //for (int i = 0; i < DictEntryDataGridView.Rows.Count-1; i++)
            //{
            //    DataGridViewRow row = DictEntryDataGridView.Rows[i];
            //    for (int j = 0; j < row.Cells.Count; j++)
            //    {
            //        //更新参数值
            //        String sqlStr = "  update tbDictEntry set isUsedAsParameterValues = '" + row.Cells[5].Value.ToString() + "' where dictTypeID = '" + row.Cells[0].Value.ToString() + "' and dictId = '" + row.Cells[1].Value.ToString() + "' ";
            //        Common.ExeSqlCommand(sqlStr, null, CommandType.Text);

            //    }
            //}

            MessageBox.Show("保存完成。");
        }
        //根据 dictValue 查询parameterID
        private string getParameterID(string dictValue)
        {
            string parameterID = "";
            //查询参数表
            string sql = " select dictTypeID from tbDictEntry where dictValue = '" + dictValue + "' ";
            DataTable dataTable = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                parameterID = (string)dataTable.Rows[0].ItemArray[0];
            }
            return parameterID;
        }
        //根据 parameterID 查询parameterValue
        private string getParameterValue(string parameterID)
        {
            string parameterValue = "";
            //查询参数表
            string sql = " select parameterValue from tbParameter where parameterID = '" + parameterID + "' ";
            DataTable dataTable = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                parameterValue = (string)dataTable.Rows[0].ItemArray[0];
            }
            return parameterValue;
        }

        private void setDataGridViewe()
        {
            //查询数据字典表
            string sql = " select dictTypeID,dictId,dictName,dictValue,parentId,isUsedAsParameterValues,sortNo from tbDictEntry where dictTypeID = '" + parameterSelectcomboBox.SelectedValue.ToString() + "'   order by sortNo ";
            DataTable dataTableDictEntry = DBUtils.GetDataSet(CommandType.Text, sql).Tables[0];


            DictEntryDataGridView.DataSource = dataTableDictEntry;
            //设置表格标题
            setDataGridVieweHeaderText();

        }
        private void setDataGridVieweHeaderText()
        {
            //设置列的宽度为100像素
            for (int i = 0; i < 7; i++)
            {
                DictEntryDataGridView.Columns[i].Width = 150;
                //除了第四列(参数值)和第六列(是否作为参数值)，其它列都设置为只读
                if (i != 5 && i != 3) { 
                    DictEntryDataGridView.Columns[i].ReadOnly = true;
                }
            }
            // 设置列标题的文本
            DictEntryDataGridView.Columns[0].HeaderText = "参数种类ID";
            DictEntryDataGridView.Columns[1].HeaderText = "参数ID";
            DictEntryDataGridView.Columns[2].HeaderText = "参数名称";
            DictEntryDataGridView.Columns[3].HeaderText = "参数值";
            DictEntryDataGridView.Columns[4].HeaderText = "父类ID";
            DictEntryDataGridView.Columns[5].HeaderText = "是否作为参数值";
            DictEntryDataGridView.Columns[6].HeaderText = "序号";

        }
    }
}
