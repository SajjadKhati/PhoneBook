namespace PhoneBookPresentationLayer
{
    partial class PhoneBookForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PersonsDataGridView = new System.Windows.Forms.DataGridView();
            this.SearchSpecificGroupBox = new System.Windows.Forms.GroupBox();
            this.PhoneNumberLabel = new System.Windows.Forms.Label();
            this.NationalCodeLabel = new System.Windows.Forms.Label();
            this.LastNameLabel = new System.Windows.Forms.Label();
            this.FirstNameLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.PhoneNumberTextBox = new System.Windows.Forms.TextBox();
            this.NationalCodeTextBox = new System.Windows.Forms.TextBox();
            this.LastNameTextBox = new System.Windows.Forms.TextBox();
            this.FirstNameTextBox = new System.Windows.Forms.TextBox();
            this.PersonListGroupBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PersonCountLabel = new System.Windows.Forms.Label();
            this.AddPersonButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PersonsDataGridView)).BeginInit();
            this.SearchSpecificGroupBox.SuspendLayout();
            this.PersonListGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PersonsDataGridView
            // 
            this.PersonsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PersonsDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.PersonsDataGridView.Location = new System.Drawing.Point(0, 89);
            this.PersonsDataGridView.MultiSelect = false;
            this.PersonsDataGridView.Name = "PersonsDataGridView";
            this.PersonsDataGridView.Size = new System.Drawing.Size(1054, 402);
            this.PersonsDataGridView.TabIndex = 7;
            this.PersonsDataGridView.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.PersonsDataGridView_ColumnAdded);
            this.PersonsDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.PersonsDataGridView_RowsAdded);
            this.PersonsDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.PersonsDataGridView_RowsRemoved);
            this.PersonsDataGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PersonsDataGridView_MouseClick);
            this.PersonsDataGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PersonsDataGridView_MouseDoubleClick);
            // 
            // SearchSpecificGroupBox
            // 
            this.SearchSpecificGroupBox.Controls.Add(this.PhoneNumberLabel);
            this.SearchSpecificGroupBox.Controls.Add(this.NationalCodeLabel);
            this.SearchSpecificGroupBox.Controls.Add(this.LastNameLabel);
            this.SearchSpecificGroupBox.Controls.Add(this.FirstNameLabel);
            this.SearchSpecificGroupBox.Controls.Add(this.SearchButton);
            this.SearchSpecificGroupBox.Controls.Add(this.PhoneNumberTextBox);
            this.SearchSpecificGroupBox.Controls.Add(this.NationalCodeTextBox);
            this.SearchSpecificGroupBox.Controls.Add(this.LastNameTextBox);
            this.SearchSpecificGroupBox.Controls.Add(this.FirstNameTextBox);
            this.SearchSpecificGroupBox.Location = new System.Drawing.Point(12, 12);
            this.SearchSpecificGroupBox.Name = "SearchSpecificGroupBox";
            this.SearchSpecificGroupBox.Size = new System.Drawing.Size(1060, 120);
            this.SearchSpecificGroupBox.TabIndex = 3;
            this.SearchSpecificGroupBox.TabStop = false;
            this.SearchSpecificGroupBox.Text = "مشخصات جستجو";
            // 
            // PhoneNumberLabel
            // 
            this.PhoneNumberLabel.AutoSize = true;
            this.PhoneNumberLabel.Location = new System.Drawing.Point(478, 67);
            this.PhoneNumberLabel.Name = "PhoneNumberLabel";
            this.PhoneNumberLabel.Size = new System.Drawing.Size(74, 13);
            this.PhoneNumberLabel.TabIndex = 9;
            this.PhoneNumberLabel.Text = "شماره همراه  :";
            // 
            // NationalCodeLabel
            // 
            this.NationalCodeLabel.AutoSize = true;
            this.NationalCodeLabel.Location = new System.Drawing.Point(503, 31);
            this.NationalCodeLabel.Name = "NationalCodeLabel";
            this.NationalCodeLabel.Size = new System.Drawing.Size(49, 13);
            this.NationalCodeLabel.TabIndex = 8;
            this.NationalCodeLabel.Text = "کد ملی  :";
            // 
            // LastNameLabel
            // 
            this.LastNameLabel.AutoSize = true;
            this.LastNameLabel.Location = new System.Drawing.Point(763, 67);
            this.LastNameLabel.Name = "LastNameLabel";
            this.LastNameLabel.Size = new System.Drawing.Size(81, 13);
            this.LastNameLabel.TabIndex = 7;
            this.LastNameLabel.Text = "نام  خانوادگی  :";
            // 
            // FirstNameLabel
            // 
            this.FirstNameLabel.AutoSize = true;
            this.FirstNameLabel.Location = new System.Drawing.Point(815, 31);
            this.FirstNameLabel.Name = "FirstNameLabel";
            this.FirstNameLabel.Size = new System.Drawing.Size(29, 13);
            this.FirstNameLabel.TabIndex = 6;
            this.FirstNameLabel.Text = "نام  :";
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.SearchButton.ForeColor = System.Drawing.Color.Green;
            this.SearchButton.Location = new System.Drawing.Point(247, 67);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 28);
            this.SearchButton.TabIndex = 5;
            this.SearchButton.Text = "جستجو";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // PhoneNumberTextBox
            // 
            this.PhoneNumberTextBox.Location = new System.Drawing.Point(358, 67);
            this.PhoneNumberTextBox.Name = "PhoneNumberTextBox";
            this.PhoneNumberTextBox.Size = new System.Drawing.Size(100, 20);
            this.PhoneNumberTextBox.TabIndex = 4;
            // 
            // NationalCodeTextBox
            // 
            this.NationalCodeTextBox.Location = new System.Drawing.Point(358, 28);
            this.NationalCodeTextBox.Name = "NationalCodeTextBox";
            this.NationalCodeTextBox.Size = new System.Drawing.Size(100, 20);
            this.NationalCodeTextBox.TabIndex = 3;
            // 
            // LastNameTextBox
            // 
            this.LastNameTextBox.Location = new System.Drawing.Point(645, 67);
            this.LastNameTextBox.Name = "LastNameTextBox";
            this.LastNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.LastNameTextBox.TabIndex = 2;
            // 
            // FirstNameTextBox
            // 
            this.FirstNameTextBox.Location = new System.Drawing.Point(645, 28);
            this.FirstNameTextBox.Name = "FirstNameTextBox";
            this.FirstNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.FirstNameTextBox.TabIndex = 1;
            // 
            // PersonListGroupBox
            // 
            this.PersonListGroupBox.Controls.Add(this.panel1);
            this.PersonListGroupBox.Controls.Add(this.AddPersonButton);
            this.PersonListGroupBox.Controls.Add(this.PersonsDataGridView);
            this.PersonListGroupBox.Location = new System.Drawing.Point(12, 138);
            this.PersonListGroupBox.Name = "PersonListGroupBox";
            this.PersonListGroupBox.Size = new System.Drawing.Size(1060, 511);
            this.PersonListGroupBox.TabIndex = 4;
            this.PersonListGroupBox.TabStop = false;
            this.PersonListGroupBox.Text = "لیست افراد";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.PersonCountLabel);
            this.panel1.Location = new System.Drawing.Point(0, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1054, 20);
            this.panel1.TabIndex = 100;
            // 
            // PersonCountLabel
            // 
            this.PersonCountLabel.AutoSize = true;
            this.PersonCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.PersonCountLabel.Location = new System.Drawing.Point(971, 0);
            this.PersonCountLabel.Name = "PersonCountLabel";
            this.PersonCountLabel.Size = new System.Drawing.Size(0, 20);
            this.PersonCountLabel.TabIndex = 101;
            // 
            // AddPersonButton
            // 
            this.AddPersonButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.AddPersonButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.AddPersonButton.ForeColor = System.Drawing.Color.Green;
            this.AddPersonButton.Location = new System.Drawing.Point(954, 19);
            this.AddPersonButton.Name = "AddPersonButton";
            this.AddPersonButton.Size = new System.Drawing.Size(100, 36);
            this.AddPersonButton.TabIndex = 6;
            this.AddPersonButton.Text = "ثبت فرد جدید";
            this.AddPersonButton.UseVisualStyleBackColor = false;
            this.AddPersonButton.Click += new System.EventHandler(this.AddPersonButton_Click);
            // 
            // PhoneBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 661);
            this.Controls.Add(this.PersonListGroupBox);
            this.Controls.Add(this.SearchSpecificGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PhoneBookForm";
            this.Text = "Phone Book";
            this.Load += new System.EventHandler(this.PhoneBookForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PersonsDataGridView)).EndInit();
            this.SearchSpecificGroupBox.ResumeLayout(false);
            this.SearchSpecificGroupBox.PerformLayout();
            this.PersonListGroupBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView PersonsDataGridView;
        private System.Windows.Forms.GroupBox SearchSpecificGroupBox;
        private System.Windows.Forms.GroupBox PersonListGroupBox;
        private System.Windows.Forms.Button AddPersonButton;
        private System.Windows.Forms.Label PersonCountLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox PhoneNumberTextBox;
        private System.Windows.Forms.TextBox NationalCodeTextBox;
        private System.Windows.Forms.TextBox LastNameTextBox;
        private System.Windows.Forms.TextBox FirstNameTextBox;
        private System.Windows.Forms.Label FirstNameLabel;
        private System.Windows.Forms.Label PhoneNumberLabel;
        private System.Windows.Forms.Label NationalCodeLabel;
        private System.Windows.Forms.Label LastNameLabel;
    }
}

