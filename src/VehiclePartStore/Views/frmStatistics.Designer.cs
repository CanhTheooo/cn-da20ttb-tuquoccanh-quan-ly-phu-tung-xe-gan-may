
namespace VehiclePartStore.Views
{
    partial class frmStatistics
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
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnthongke = new System.Windows.Forms.Button();
            this.rdTop5 = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.btnView = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 145);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.Size = new System.Drawing.Size(867, 409);
            this.dgvData.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnthongke);
            this.groupBox1.Controls.Add(this.rdTop5);
            this.groupBox1.Controls.Add(this.rdAll);
            this.groupBox1.Controls.Add(this.btnView);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtTo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtFrom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(867, 145);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chọn thông tin cần lọc";
            // 
            // btnthongke
            // 
            this.btnthongke.Location = new System.Drawing.Point(384, 77);
            this.btnthongke.Name = "btnthongke";
            this.btnthongke.Size = new System.Drawing.Size(100, 28);
            this.btnthongke.TabIndex = 7;
            this.btnthongke.Text = "Thống kê";
            this.btnthongke.UseVisualStyleBackColor = true;
            this.btnthongke.Click += new System.EventHandler(this.btnthongke_Click);
            // 
            // rdTop5
            // 
            this.rdTop5.AutoSize = true;
            this.rdTop5.Location = new System.Drawing.Point(148, 105);
            this.rdTop5.Margin = new System.Windows.Forms.Padding(4);
            this.rdTop5.Name = "rdTop5";
            this.rdTop5.Size = new System.Drawing.Size(183, 20);
            this.rdTop5.TabIndex = 6;
            this.rdTop5.Text = "Top 5 sản phẩm bán chạy";
            this.rdTop5.UseVisualStyleBackColor = true;
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.Checked = true;
            this.rdAll.Location = new System.Drawing.Point(65, 105);
            this.rdAll.Margin = new System.Windows.Forms.Padding(4);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(66, 20);
            this.rdAll.TabIndex = 5;
            this.rdAll.TabStop = true;
            this.rdAll.Text = "Tất cả";
            this.rdAll.UseVisualStyleBackColor = true;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(384, 41);
            this.btnView.Margin = new System.Windows.Forms.Padding(4);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(100, 29);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "Lọc dữ liệu";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Đến Ngày:";
            // 
            // dtTo
            // 
            this.dtTo.Checked = false;
            this.dtTo.CustomFormat = "dd/MM/yyyy";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(148, 73);
            this.dtTo.Margin = new System.Windows.Forms.Padding(4);
            this.dtTo.Name = "dtTo";
            this.dtTo.ShowCheckBox = true;
            this.dtTo.Size = new System.Drawing.Size(165, 22);
            this.dtTo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Từ Ngày:";
            // 
            // dtFrom
            // 
            this.dtFrom.Checked = false;
            this.dtFrom.CustomFormat = "dd/MM/yyyy";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(148, 41);
            this.dtFrom.Margin = new System.Windows.Forms.Padding(4);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.ShowCheckBox = true;
            this.dtFrom.Size = new System.Drawing.Size(165, 22);
            this.dtFrom.TabIndex = 0;
            // 
            // frmStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 554);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmStatistics";
            this.Text = "THỐNG KÊ";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.RadioButton rdTop5;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.Button btnthongke;
    }
}