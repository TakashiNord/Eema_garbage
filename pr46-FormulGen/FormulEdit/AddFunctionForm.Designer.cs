namespace RSDU.Components.FormulEdit
{
    partial class AddFunctionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFunctionForm));
            this.label1 = new System.Windows.Forms.Label();
            this._cbxCategory = new System.Windows.Forms.ComboBox();
            this._labFuncDesc = new System.Windows.Forms.Label();
            this._btnOK = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._gvFunctions = new System.Windows.Forms.DataGridView();
            this.colFuncName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._gvFunctions)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Из категории:";
            // 
            // _cbxCategory
            // 
            this._cbxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cbxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbxCategory.FormattingEnabled = true;
            this._cbxCategory.Location = new System.Drawing.Point(94, 15);
            this._cbxCategory.Name = "_cbxCategory";
            this._cbxCategory.Size = new System.Drawing.Size(261, 21);
            this._cbxCategory.TabIndex = 1;
            this._cbxCategory.SelectedValueChanged += new System.EventHandler(this.OnCbxCategorySelValueChanged);
            // 
            // _labFuncDesc
            // 
            this._labFuncDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._labFuncDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._labFuncDesc.Location = new System.Drawing.Point(12, 251);
            this._labFuncDesc.Name = "_labFuncDesc";
            this._labFuncDesc.Size = new System.Drawing.Size(343, 53);
            this._labFuncDesc.TabIndex = 2;
            this._labFuncDesc.Text = "Описание";
            // 
            // _btnOK
            // 
            this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOK.Location = new System.Drawing.Point(190, 314);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(75, 23);
            this._btnOK.TabIndex = 4;
            this._btnOK.Text = "ОК";
            this._btnOK.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(280, 314);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 4;
            this._btnCancel.Text = "Отмена";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _gvFunctions
            // 
            this._gvFunctions.AllowUserToAddRows = false;
            this._gvFunctions.AllowUserToDeleteRows = false;
            this._gvFunctions.AllowUserToResizeRows = false;
            this._gvFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gvFunctions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._gvFunctions.BackgroundColor = System.Drawing.SystemColors.Window;
            this._gvFunctions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._gvFunctions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFuncName});
            this._gvFunctions.GridColor = System.Drawing.SystemColors.Control;
            this._gvFunctions.Location = new System.Drawing.Point(12, 42);
            this._gvFunctions.MultiSelect = false;
            this._gvFunctions.Name = "_gvFunctions";
            this._gvFunctions.RowHeadersVisible = false;
            this._gvFunctions.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this._gvFunctions.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this._gvFunctions.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Silver;
            this._gvFunctions.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this._gvFunctions.RowTemplate.Height = 18;
            this._gvFunctions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._gvFunctions.Size = new System.Drawing.Size(343, 198);
            this._gvFunctions.TabIndex = 5;
            this._gvFunctions.VirtualMode = true;
            this._gvFunctions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridFuncCellDoubleClick);
            this._gvFunctions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.OnGridFuncCellFormatting);
            this._gvFunctions.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.OnGridFuncCellValueNeeded);
            this._gvFunctions.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.OnGridFuncCellValuePushed);
            this._gvFunctions.SelectionChanged += new System.EventHandler(this.OnGridFuncSelChanged);
            // 
            // colFuncName
            // 
            this.colFuncName.HeaderText = "Функция";
            this.colFuncName.Name = "colFuncName";
            this.colFuncName.ReadOnly = true;
            // 
            // AddFunctionForm
            // 
            this.AcceptButton = this._btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(367, 349);
            this.Controls.Add(this._gvFunctions);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._labFuncDesc);
            this.Controls.Add(this._cbxCategory);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(280, 242);
            this.Name = "AddFunctionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить функцию";
            this.Load += new System.EventHandler(this.AddFunctionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._gvFunctions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _cbxCategory;
        private System.Windows.Forms.Label _labFuncDesc;
        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.DataGridView _gvFunctions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFuncName;
    }
}