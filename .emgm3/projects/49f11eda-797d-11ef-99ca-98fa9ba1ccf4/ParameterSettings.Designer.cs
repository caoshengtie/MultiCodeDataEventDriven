namespace MultiCodeDataEventDriven
{
    partial class ParameterSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.parameterSelectcomboBox = new System.Windows.Forms.ComboBox();
            this.parameterSelectLabel = new System.Windows.Forms.Label();
            this.parameterInfogroupBox = new System.Windows.Forms.GroupBox();
            this.parameterValue = new System.Windows.Forms.Label();
            this.savebutton = new System.Windows.Forms.Button();
            this.DictEntryDataGridView = new System.Windows.Forms.DataGridView();
            this.parameterInfogroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DictEntryDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // parameterSelectcomboBox
            // 
            this.parameterSelectcomboBox.FormattingEnabled = true;
            this.parameterSelectcomboBox.Location = new System.Drawing.Point(90, 12);
            this.parameterSelectcomboBox.Name = "parameterSelectcomboBox";
            this.parameterSelectcomboBox.Size = new System.Drawing.Size(121, 20);
            this.parameterSelectcomboBox.TabIndex = 1;
            this.parameterSelectcomboBox.SelectedIndexChanged += new System.EventHandler(this.parameterSelectcomboBox_SelectedIndexChanged);
            // 
            // parameterSelectLabel
            // 
            this.parameterSelectLabel.AutoSize = true;
            this.parameterSelectLabel.Location = new System.Drawing.Point(28, 15);
            this.parameterSelectLabel.Name = "parameterSelectLabel";
            this.parameterSelectLabel.Size = new System.Drawing.Size(53, 12);
            this.parameterSelectLabel.TabIndex = 2;
            this.parameterSelectLabel.Text = "选择参数";
            // 
            // parameterInfogroupBox
            // 
            this.parameterInfogroupBox.Controls.Add(this.DictEntryDataGridView);
            this.parameterInfogroupBox.Controls.Add(this.parameterValue);
            this.parameterInfogroupBox.Location = new System.Drawing.Point(30, 49);
            this.parameterInfogroupBox.Name = "parameterInfogroupBox";
            this.parameterInfogroupBox.Size = new System.Drawing.Size(1408, 433);
            this.parameterInfogroupBox.TabIndex = 3;
            this.parameterInfogroupBox.TabStop = false;
            this.parameterInfogroupBox.Text = "参数信息";
            // 
            // parameterValue
            // 
            this.parameterValue.AutoSize = true;
            this.parameterValue.Location = new System.Drawing.Point(219, 35);
            this.parameterValue.Name = "parameterValue";
            this.parameterValue.Size = new System.Drawing.Size(41, 12);
            this.parameterValue.TabIndex = 2;
            this.parameterValue.Text = "参数值";
            // 
            // savebutton
            // 
            this.savebutton.Location = new System.Drawing.Point(30, 504);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(75, 23);
            this.savebutton.TabIndex = 4;
            this.savebutton.Text = "保存";
            this.savebutton.UseVisualStyleBackColor = true;
            this.savebutton.Click += new System.EventHandler(this.savebutton_Click);
            // 
            // DictEntryDataGridView
            // 
            this.DictEntryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DictEntryDataGridView.Location = new System.Drawing.Point(16, 21);
            this.DictEntryDataGridView.Name = "DictEntryDataGridView";
            this.DictEntryDataGridView.RowTemplate.Height = 23;
            this.DictEntryDataGridView.Size = new System.Drawing.Size(1386, 382);
            this.DictEntryDataGridView.TabIndex = 4;
            // 
            // ParameterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1527, 686);
            this.Controls.Add(this.savebutton);
            this.Controls.Add(this.parameterSelectLabel);
            this.Controls.Add(this.parameterInfogroupBox);
            this.Controls.Add(this.parameterSelectcomboBox);
            this.Name = "ParameterSettings";
            this.Text = "参数设置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ParameterSettings_Load);
            this.parameterInfogroupBox.ResumeLayout(false);
            this.parameterInfogroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DictEntryDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox parameterSelectcomboBox;
        private System.Windows.Forms.Label parameterSelectLabel;
        private System.Windows.Forms.GroupBox parameterInfogroupBox;
        private System.Windows.Forms.Label parameterValue;
        private System.Windows.Forms.Button savebutton;
        private System.Windows.Forms.DataGridView DictEntryDataGridView;
    }
}