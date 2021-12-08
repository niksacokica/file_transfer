
namespace file_transfer
{
    partial class transfer_form
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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_file_start = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.split_1 = new System.Windows.Forms.SplitContainer();
            this.log = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split_1)).BeginInit();
            this.split_1.Panel1.SuspendLayout();
            this.split_1.Panel2.SuspendLayout();
            this.split_1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.ShowItemToolTips = true;
            this.menu.Size = new System.Drawing.Size(984, 24);
            this.menu.TabIndex = 0;
            this.menu.TabStop = true;
            // 
            // menu_file
            // 
            this.menu_file.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menu_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file_start,
            this.stopToolStripMenuItem});
            this.menu_file.Name = "menu_file";
            this.menu_file.Size = new System.Drawing.Size(37, 20);
            this.menu_file.Text = "File";
            this.menu_file.ToolTipText = "Main options";
            // 
            // menu_file_start
            // 
            this.menu_file_start.Name = "menu_file_start";
            this.menu_file_start.Size = new System.Drawing.Size(180, 22);
            this.menu_file_start.Text = "Start";
            this.menu_file_start.ToolTipText = "Allows you to start receiving files from others";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.ToolTipText = "Dissables your ability to receive any more files and cancels any ongoing tranfers" +
    "";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // split_1
            // 
            this.split_1.AllowDrop = true;
            this.split_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split_1.IsSplitterFixed = true;
            this.split_1.Location = new System.Drawing.Point(0, 24);
            this.split_1.Name = "split_1";
            // 
            // split_1.Panel1
            // 
            this.split_1.Panel1.Controls.Add(this.button1);
            // 
            // split_1.Panel2
            // 
            this.split_1.Panel2.Controls.Add(this.log);
            this.split_1.Size = new System.Drawing.Size(984, 437);
            this.split_1.SplitterDistance = 656;
            this.split_1.TabIndex = 1;
            // 
            // log
            // 
            this.log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.log.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.log.Location = new System.Drawing.Point(0, 0);
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(324, 437);
            this.log.TabIndex = 0;
            this.log.Text = "";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(656, 437);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // transfer_form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.split_1);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "transfer_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Transfer";
            this.TopMost = true;
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.split_1.Panel1.ResumeLayout(false);
            this.split_1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split_1)).EndInit();
            this.split_1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem menu_file_start;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.SplitContainer split_1;
    }
}

