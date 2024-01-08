
namespace VehiclePartStore.Views
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnProduct = new System.Windows.Forms.Button();
            this.btnCategory = new System.Windows.Forms.Button();
            this.btnOrder = new System.Windows.Forms.Button();
            this.btnStatis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCustomer
            // 
            this.btnCustomer.BackColor = System.Drawing.Color.White;
            this.btnCustomer.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomer.Image")));
            this.btnCustomer.Location = new System.Drawing.Point(232, 70);
            this.btnCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(171, 71);
            this.btnCustomer.TabIndex = 0;
            this.btnCustomer.Text = "Khách Hàng";
            this.btnCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCustomer.UseVisualStyleBackColor = false;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // btnProduct
            // 
            this.btnProduct.BackColor = System.Drawing.Color.White;
            this.btnProduct.Image = ((System.Drawing.Image)(resources.GetObject("btnProduct.Image")));
            this.btnProduct.Location = new System.Drawing.Point(53, 149);
            this.btnProduct.Margin = new System.Windows.Forms.Padding(4);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(171, 71);
            this.btnProduct.TabIndex = 1;
            this.btnProduct.Text = "Sản Phẩm";
            this.btnProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProduct.UseVisualStyleBackColor = false;
            this.btnProduct.Click += new System.EventHandler(this.btnProduct_Click);
            // 
            // btnCategory
            // 
            this.btnCategory.BackColor = System.Drawing.Color.White;
            this.btnCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnCategory.Image")));
            this.btnCategory.Location = new System.Drawing.Point(53, 70);
            this.btnCategory.Margin = new System.Windows.Forms.Padding(4);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(171, 71);
            this.btnCategory.TabIndex = 2;
            this.btnCategory.Text = "Thể Loại";
            this.btnCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCategory.UseVisualStyleBackColor = false;
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.BackColor = System.Drawing.Color.White;
            this.btnOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnOrder.Image")));
            this.btnOrder.Location = new System.Drawing.Point(232, 149);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(4);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(171, 71);
            this.btnOrder.TabIndex = 3;
            this.btnOrder.Text = "Bán Hàng";
            this.btnOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOrder.UseVisualStyleBackColor = false;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnStatis
            // 
            this.btnStatis.BackColor = System.Drawing.Color.White;
            this.btnStatis.Image = ((System.Drawing.Image)(resources.GetObject("btnStatis.Image")));
            this.btnStatis.Location = new System.Drawing.Point(53, 228);
            this.btnStatis.Margin = new System.Windows.Forms.Padding(4);
            this.btnStatis.Name = "btnStatis";
            this.btnStatis.Size = new System.Drawing.Size(349, 71);
            this.btnStatis.TabIndex = 4;
            this.btnStatis.Text = "Thống Kê";
            this.btnStatis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStatis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStatis.UseVisualStyleBackColor = false;
            this.btnStatis.Click += new System.EventHandler(this.btnStatis_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 331);
            this.Controls.Add(this.btnStatis);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.btnCategory);
            this.Controls.Add(this.btnProduct);
            this.Controls.Add(this.btnCustomer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = " CỬA HÀNG PHỤ TÙNG XE";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Button btnStatis;
    }
}