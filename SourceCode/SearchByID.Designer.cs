namespace Wii
{
    partial class SearchByID
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
            this.dtgSelectId = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClosed = new System.Windows.Forms.Button();
            this.clId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSelectId)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgSelectId
            // 
            this.dtgSelectId.AllowUserToResizeRows = false;
            this.dtgSelectId.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgSelectId.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSelectId.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clId});
            this.dtgSelectId.Location = new System.Drawing.Point(12, 12);
            this.dtgSelectId.Name = "dtgSelectId";
            this.dtgSelectId.Size = new System.Drawing.Size(432, 216);
            this.dtgSelectId.TabIndex = 0;
            this.dtgSelectId.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dtgSelectId_EditingControlShowing);
            this.dtgSelectId.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dtgSelectId_UserDeletedRow);
            this.dtgSelectId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtgSelectId_KeyPress);
            this.dtgSelectId.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dtgSelectId_PreviewKeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(279, 234);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClosed
            // 
            this.btnClosed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClosed.Location = new System.Drawing.Point(369, 234);
            this.btnClosed.Name = "btnClosed";
            this.btnClosed.Size = new System.Drawing.Size(75, 23);
            this.btnClosed.TabIndex = 2;
            this.btnClosed.Text = "閉じる";
            this.btnClosed.UseVisualStyleBackColor = true;
            this.btnClosed.Click += new System.EventHandler(this.btnClosed_Click);
            // 
            // clId
            // 
            this.clId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clId.HeaderText = "選曲番号";
            this.clId.MaxInputLength = 10;
            this.clId.Name = "clId";
            // 
            // SearchByID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 269);
            this.Controls.Add(this.btnClosed);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtgSelectId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SearchByID";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "選曲番号入力";
            this.Load += new System.EventHandler(this.SearchByID_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgSelectId)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgSelectId;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClosed;
        private System.Windows.Forms.DataGridViewTextBoxColumn clId;
    }
}