namespace test
{
    partial class ThucDon
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
            this.In_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Tim_textBox = new System.Windows.Forms.TextBox();
            this.Tim_label = new System.Windows.Forms.Label();
            this.Sua_button = new System.Windows.Forms.Button();
            this.Xoa_button3 = new System.Windows.Forms.Button();
            this.Them_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DanhMuc_comboBox = new System.Windows.Forms.ComboBox();
            this.Gia_textBox = new System.Windows.Forms.TextBox();
            this.Ten_textBox = new System.Windows.Forms.TextBox();
            this.DanhMuc_label = new System.Windows.Forms.Label();
            this.Gia_label = new System.Windows.Forms.Label();
            this.TSP_label = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // In_button
            // 
            this.In_button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.In_button.Location = new System.Drawing.Point(152, 345);
            this.In_button.Name = "In_button";
            this.In_button.Size = new System.Drawing.Size(242, 42);
            this.In_button.TabIndex = 10;
            this.In_button.Text = "In danh sách Menu";
            this.In_button.UseVisualStyleBackColor = false;
            this.In_button.Click += new System.EventHandler(this.In_button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Turquoise;
            this.groupBox2.Controls.Add(this.Tim_textBox);
            this.groupBox2.Controls.Add(this.Tim_label);
            this.groupBox2.Controls.Add(this.Sua_button);
            this.groupBox2.Controls.Add(this.Xoa_button3);
            this.groupBox2.Controls.Add(this.Them_button);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(45, 402);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 209);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chức năng";
            // 
            // Tim_textBox
            // 
            this.Tim_textBox.Location = new System.Drawing.Point(13, 146);
            this.Tim_textBox.Name = "Tim_textBox";
            this.Tim_textBox.Size = new System.Drawing.Size(164, 30);
            this.Tim_textBox.TabIndex = 8;
            this.Tim_textBox.TextChanged += new System.EventHandler(this.Tim_textBox_TextChanged);
            // 
            // Tim_label
            // 
            this.Tim_label.AutoSize = true;
            this.Tim_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tim_label.ForeColor = System.Drawing.Color.Black;
            this.Tim_label.Location = new System.Drawing.Point(15, 109);
            this.Tim_label.Name = "Tim_label";
            this.Tim_label.Size = new System.Drawing.Size(172, 20);
            this.Tim_label.TabIndex = 8;
            this.Tim_label.Text = "Tìm kiếm sản phẩm";
            // 
            // Sua_button
            // 
            this.Sua_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sua_button.ForeColor = System.Drawing.Color.Black;
            this.Sua_button.Location = new System.Drawing.Point(208, 50);
            this.Sua_button.Name = "Sua_button";
            this.Sua_button.Size = new System.Drawing.Size(75, 34);
            this.Sua_button.TabIndex = 2;
            this.Sua_button.Text = "Sửa";
            this.Sua_button.UseVisualStyleBackColor = true;
            this.Sua_button.Click += new System.EventHandler(this.Sua_button_Click);
            // 
            // Xoa_button3
            // 
            this.Xoa_button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Xoa_button3.ForeColor = System.Drawing.Color.Black;
            this.Xoa_button3.Location = new System.Drawing.Point(107, 51);
            this.Xoa_button3.Name = "Xoa_button3";
            this.Xoa_button3.Size = new System.Drawing.Size(75, 33);
            this.Xoa_button3.TabIndex = 1;
            this.Xoa_button3.Text = "Xóa";
            this.Xoa_button3.UseVisualStyleBackColor = true;
            this.Xoa_button3.Click += new System.EventHandler(this.Xoa_button3_Click);
            // 
            // Them_button
            // 
            this.Them_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Them_button.ForeColor = System.Drawing.Color.Black;
            this.Them_button.Location = new System.Drawing.Point(13, 51);
            this.Them_button.Name = "Them_button";
            this.Them_button.Size = new System.Drawing.Size(75, 33);
            this.Them_button.TabIndex = 0;
            this.Them_button.Text = "Thêm";
            this.Them_button.UseVisualStyleBackColor = true;
            this.Them_button.Click += new System.EventHandler(this.Them_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Turquoise;
            this.groupBox1.Controls.Add(this.DanhMuc_comboBox);
            this.groupBox1.Controls.Add(this.Gia_textBox);
            this.groupBox1.Controls.Add(this.Ten_textBox);
            this.groupBox1.Controls.Add(this.DanhMuc_label);
            this.groupBox1.Controls.Add(this.Gia_label);
            this.groupBox1.Controls.Add(this.TSP_label);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(45, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 306);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin Menu";
            // 
            // DanhMuc_comboBox
            // 
            this.DanhMuc_comboBox.FormattingEnabled = true;
            this.DanhMuc_comboBox.Location = new System.Drawing.Point(163, 183);
            this.DanhMuc_comboBox.Name = "DanhMuc_comboBox";
            this.DanhMuc_comboBox.Size = new System.Drawing.Size(164, 33);
            this.DanhMuc_comboBox.TabIndex = 7;
            this.DanhMuc_comboBox.SelectedIndexChanged += new System.EventHandler(this.DanhMuc_comboBox_SelectedIndexChanged);
            // 
            // Gia_textBox
            // 
            this.Gia_textBox.Location = new System.Drawing.Point(163, 129);
            this.Gia_textBox.Name = "Gia_textBox";
            this.Gia_textBox.Size = new System.Drawing.Size(164, 30);
            this.Gia_textBox.TabIndex = 6;
            // 
            // Ten_textBox
            // 
            this.Ten_textBox.Location = new System.Drawing.Point(163, 68);
            this.Ten_textBox.Name = "Ten_textBox";
            this.Ten_textBox.Size = new System.Drawing.Size(164, 30);
            this.Ten_textBox.TabIndex = 5;
            // 
            // DanhMuc_label
            // 
            this.DanhMuc_label.AutoSize = true;
            this.DanhMuc_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DanhMuc_label.ForeColor = System.Drawing.Color.Black;
            this.DanhMuc_label.Location = new System.Drawing.Point(15, 190);
            this.DanhMuc_label.Name = "DanhMuc_label";
            this.DanhMuc_label.Size = new System.Drawing.Size(94, 20);
            this.DanhMuc_label.TabIndex = 3;
            this.DanhMuc_label.Text = "Danh mục";
            // 
            // Gia_label
            // 
            this.Gia_label.AutoSize = true;
            this.Gia_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gia_label.ForeColor = System.Drawing.Color.Black;
            this.Gia_label.Location = new System.Drawing.Point(15, 129);
            this.Gia_label.Name = "Gia_label";
            this.Gia_label.Size = new System.Drawing.Size(73, 20);
            this.Gia_label.TabIndex = 2;
            this.Gia_label.Text = "Đơn giá";
            // 
            // TSP_label
            // 
            this.TSP_label.AutoSize = true;
            this.TSP_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TSP_label.ForeColor = System.Drawing.Color.Black;
            this.TSP_label.Location = new System.Drawing.Point(15, 68);
            this.TSP_label.Name = "TSP_label";
            this.TSP_label.Size = new System.Drawing.Size(40, 20);
            this.TSP_label.TabIndex = 1;
            this.TSP_label.Text = "Tên";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Turquoise;
            this.groupBox3.Controls.Add(this.listView1);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Red;
            this.groupBox3.Location = new System.Drawing.Point(530, 33);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(682, 306);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Danh sách các món ăn và thức uống";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(6, 38);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(670, 225);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Mã";
            this.columnHeader1.Width = 178;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Danh mục";
            this.columnHeader2.Width = 165;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tên";
            this.columnHeader3.Width = 159;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Đơn giá";
            this.columnHeader4.Width = 323;
            // 
            // ThucDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 743);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.In_button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ThucDon";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.ThucDon_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button In_button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Tim_textBox;
        private System.Windows.Forms.Label Tim_label;
        private System.Windows.Forms.Button Sua_button;
        private System.Windows.Forms.Button Xoa_button3;
        private System.Windows.Forms.Button Them_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox DanhMuc_comboBox;
        private System.Windows.Forms.TextBox Gia_textBox;
        private System.Windows.Forms.TextBox Ten_textBox;
        private System.Windows.Forms.Label DanhMuc_label;
        private System.Windows.Forms.Label Gia_label;
        private System.Windows.Forms.Label TSP_label;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}