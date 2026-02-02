namespace RSDU.Components
{
    partial class FormuleEditComponent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._gridParams = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._checkFormul = new System.Windows.Forms.Button();
            this._btnAddParam = new System.Windows.Forms.Button();
            this._btnAddFunc = new System.Windows.Forms.Button();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._formula = new System.Windows.Forms.RichTextBox();
            this._checkFormulResult = new System.Windows.Forms.TextBox();
            this._mainSplitContainer = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this._gridParams)).BeginInit();
            this._mainSplitContainer.Panel1.SuspendLayout();
            this._mainSplitContainer.Panel2.SuspendLayout();
            this._mainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _gridParams
            // 
            this._gridParams.AllowUserToAddRows = false;
            this._gridParams.AllowUserToDeleteRows = false;
            this._gridParams.AllowUserToResizeRows = false;
            this._gridParams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._gridParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._gridParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this._gridParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridParams.Location = new System.Drawing.Point(0, 0);
            this._gridParams.MultiSelect = false;
            this._gridParams.Name = "_gridParams";
            this._gridParams.ReadOnly = true;
            this._gridParams.RowHeadersVisible = false;
            this._gridParams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._gridParams.Size = new System.Drawing.Size(533, 225);
            this._gridParams.StandardTab = true;
            this._gridParams.TabIndex = 0;
            this._gridParams.VirtualMode = true;
            this._gridParams.DoubleClick += new System.EventHandler(this.OnListDoubleClick);
            this._gridParams.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.OnCellValueNeeded);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 30F;
            this.Column1.HeaderText = "Ключ";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Параметр";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // _checkFormul
            // 
            this._checkFormul.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._checkFormul.Location = new System.Drawing.Point(405, 326);
            this._checkFormul.Name = "_checkFormul";
            this._checkFormul.Size = new System.Drawing.Size(128, 26);
            this._checkFormul.TabIndex = 1;
            this._checkFormul.Text = "Проверить формулу";
            this._checkFormul.UseVisualStyleBackColor = true;
            this._checkFormul.Click += new System.EventHandler(this.OnFormuleCheckClick);
            // 
            // _btnAddParam
            // 
            this._btnAddParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnAddParam.Enabled = false;
            this._btnAddParam.Location = new System.Drawing.Point(402, 32);
            this._btnAddParam.Name = "_btnAddParam";
            this._btnAddParam.Size = new System.Drawing.Size(128, 26);
            this._btnAddParam.TabIndex = 2;
            this._btnAddParam.Text = "Добавить параметр...";
            this._btnAddParam.UseVisualStyleBackColor = true;
            this._btnAddParam.Click += new System.EventHandler(this.OnAddParamClick);
            // 
            // _btnAddFunc
            // 
            this._btnAddFunc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnAddFunc.Enabled = false;
            this._btnAddFunc.Location = new System.Drawing.Point(402, 0);
            this._btnAddFunc.Name = "_btnAddFunc";
            this._btnAddFunc.Size = new System.Drawing.Size(128, 26);
            this._btnAddFunc.TabIndex = 1;
            this._btnAddFunc.Text = "Добавить функцию....";
            this._btnAddFunc.UseVisualStyleBackColor = true;
            this._btnAddFunc.Click += new System.EventHandler(this.OnAddFunctionClick);
            // 
            // _formula
            // 
            this._formula.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._formula.DetectUrls = false;
            this._formula.HideSelection = false;
            this._formula.Location = new System.Drawing.Point(0, 0);
            this._formula.MaxLength = 1024;
            this._formula.Name = "_formula";
            this._formula.Size = new System.Drawing.Size(396, 91);
            this._formula.TabIndex = 0;
            this._formula.Text = "";
            this._formula.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
            this._formula.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            // 
            // _checkFormulResult
            // 
            this._checkFormulResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._checkFormulResult.Location = new System.Drawing.Point(3, 326);
            this._checkFormulResult.Multiline = true;
            this._checkFormulResult.Name = "_checkFormulResult";
            this._checkFormulResult.ReadOnly = true;
            this._checkFormulResult.Size = new System.Drawing.Size(396, 35);
            this._checkFormulResult.TabIndex = 10;
            this._checkFormulResult.TabStop = false;
            this._checkFormulResult.Text = "В этой формуле все верно..,\r\nа вдруг нет?...";
            // 
            // _mainSplitContainer
            // 
            this._mainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._mainSplitContainer.Location = new System.Drawing.Point(3, 0);
            this._mainSplitContainer.Name = "_mainSplitContainer";
            this._mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _mainSplitContainer.Panel1
            // 
            this._mainSplitContainer.Panel1.Controls.Add(this._formula);
            this._mainSplitContainer.Panel1.Controls.Add(this._btnAddParam);
            this._mainSplitContainer.Panel1.Controls.Add(this._btnAddFunc);
            this._mainSplitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._mainSplitContainer.Panel1MinSize = 66;
            // 
            // _mainSplitContainer.Panel2
            // 
            this._mainSplitContainer.Panel2.Controls.Add(this._gridParams);
            this._mainSplitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._mainSplitContainer.Size = new System.Drawing.Size(533, 320);
            this._mainSplitContainer.SplitterDistance = 91;
            this._mainSplitContainer.TabIndex = 0;
            // 
            // FormuleEditComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._mainSplitContainer);
            this.Controls.Add(this._checkFormulResult);
            this.Controls.Add(this._checkFormul);
            this.MinimumSize = new System.Drawing.Size(332, 143);
            this.Name = "FormuleEditComponent";
            this.Size = new System.Drawing.Size(536, 364);
            ((System.ComponentModel.ISupportInitialize)(this._gridParams)).EndInit();
            this._mainSplitContainer.Panel1.ResumeLayout(false);
            this._mainSplitContainer.Panel2.ResumeLayout(false);
            this._mainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _gridParams;
        private System.Windows.Forms.Button _checkFormul;
        private System.Windows.Forms.Button _btnAddParam;
        private System.Windows.Forms.Button _btnAddFunc;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.RichTextBox _formula;
        private System.Windows.Forms.TextBox _checkFormulResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.SplitContainer _mainSplitContainer;
    }
}
