namespace StringProcessor.WinForms.Demo
{
    partial class MainForm
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
			this.startProcessing_btn = new System.Windows.Forms.Button();
			this.processStatus_lv = new System.Windows.Forms.ListView();
			this.ProcessingMessage_Column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// startProcessing_btn
			// 
			this.startProcessing_btn.Location = new System.Drawing.Point(12, 12);
			this.startProcessing_btn.Name = "startProcessing_btn";
			this.startProcessing_btn.Size = new System.Drawing.Size(92, 23);
			this.startProcessing_btn.TabIndex = 0;
			this.startProcessing_btn.Text = "Start Processing";
			this.startProcessing_btn.UseVisualStyleBackColor = true;
			this.startProcessing_btn.Click += new System.EventHandler(this.startProcessing_btn_Click);
			// 
			// processStatus_lv
			// 
			this.processStatus_lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProcessingMessage_Column});
			this.processStatus_lv.HideSelection = false;
			this.processStatus_lv.Location = new System.Drawing.Point(12, 41);
			this.processStatus_lv.Name = "processStatus_lv";
			this.processStatus_lv.Size = new System.Drawing.Size(776, 397);
			this.processStatus_lv.TabIndex = 1;
			this.processStatus_lv.UseCompatibleStateImageBehavior = false;
			this.processStatus_lv.View = System.Windows.Forms.View.List;
			// 
			// ProcessingMessage_Column
			// 
			this.ProcessingMessage_Column.Text = "Processing Message";
			this.ProcessingMessage_Column.Width = 600;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.processStatus_lv);
			this.Controls.Add(this.startProcessing_btn);
			this.Name = "MainForm";
			this.Text = "Form1";
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startProcessing_btn;
        private System.Windows.Forms.ListView processStatus_lv;
        private System.Windows.Forms.ColumnHeader ProcessingMessage_Column;
    }
}

