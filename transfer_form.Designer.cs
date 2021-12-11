
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(transfer_form));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_file_start = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_file_stop = new System.Windows.Forms.ToolStripMenuItem();
            this.main_split = new System.Windows.Forms.SplitContainer();
            this.send_file_but = new System.Windows.Forms.Button();
            this.ip_port_chat = new System.Windows.Forms.TextBox();
            this.sel_send_lab = new System.Windows.Forms.Label();
            this.file_path_chat = new System.Windows.Forms.TextBox();
            this.open_file_but = new System.Windows.Forms.Button();
            this.secondary_split = new System.Windows.Forms.SplitContainer();
            this.send_list = new System.Windows.Forms.ListView();
            this.file_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.file_size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progress_bar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.open_file_dialog = new System.Windows.Forms.OpenFileDialog();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.main_split)).BeginInit();
            this.main_split.Panel1.SuspendLayout();
            this.main_split.Panel2.SuspendLayout();
            this.main_split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondary_split)).BeginInit();
            this.secondary_split.Panel1.SuspendLayout();
            this.secondary_split.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.ShowItemToolTips = true;
            this.menu.Size = new System.Drawing.Size(784, 24);
            this.menu.TabIndex = 10;
            this.menu.TabStop = true;
            // 
            // menu_file
            // 
            this.menu_file.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menu_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file_start,
            this.menu_file_stop});
            this.menu_file.Name = "menu_file";
            this.menu_file.Size = new System.Drawing.Size(37, 20);
            this.menu_file.Text = "File";
            this.menu_file.ToolTipText = "Main options";
            // 
            // menu_file_start
            // 
            this.menu_file_start.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_start.Image")));
            this.menu_file_start.Name = "menu_file_start";
            this.menu_file_start.Size = new System.Drawing.Size(98, 22);
            this.menu_file_start.Text = "Start";
            this.menu_file_start.ToolTipText = "Allows you to start receiving files from others";
            this.menu_file_start.Click += new System.EventHandler(this.menu_file_start_Click);
            // 
            // menu_file_stop
            // 
            this.menu_file_stop.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_stop.Image")));
            this.menu_file_stop.Name = "menu_file_stop";
            this.menu_file_stop.Size = new System.Drawing.Size(98, 22);
            this.menu_file_stop.Text = "Stop";
            this.menu_file_stop.ToolTipText = "Dissables your ability to receive any more files and cancels any ongoing tranfers" +
    "";
            this.menu_file_stop.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // main_split
            // 
            this.main_split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.main_split.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.main_split.IsSplitterFixed = true;
            this.main_split.Location = new System.Drawing.Point(12, 27);
            this.main_split.Name = "main_split";
            this.main_split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // main_split.Panel1
            // 
            this.main_split.Panel1.Controls.Add(this.send_file_but);
            this.main_split.Panel1.Controls.Add(this.ip_port_chat);
            this.main_split.Panel1.Controls.Add(this.sel_send_lab);
            this.main_split.Panel1.Controls.Add(this.file_path_chat);
            this.main_split.Panel1.Controls.Add(this.open_file_but);
            // 
            // main_split.Panel2
            // 
            this.main_split.Panel2.Controls.Add(this.secondary_split);
            this.main_split.Size = new System.Drawing.Size(760, 322);
            this.main_split.SplitterDistance = 59;
            this.main_split.TabIndex = 2;
            // 
            // send_file_but
            // 
            this.send_file_but.Location = new System.Drawing.Point(660, 30);
            this.send_file_but.Name = "send_file_but";
            this.send_file_but.Size = new System.Drawing.Size(75, 20);
            this.send_file_but.TabIndex = 12;
            this.send_file_but.Text = "Send";
            this.send_file_but.UseVisualStyleBackColor = true;
            this.send_file_but.Click += new System.EventHandler(this.send_file_but_Click);
            // 
            // ip_port_chat
            // 
            this.ip_port_chat.ForeColor = System.Drawing.Color.Gray;
            this.ip_port_chat.Location = new System.Drawing.Point(500, 30);
            this.ip_port_chat.Name = "ip_port_chat";
            this.ip_port_chat.Size = new System.Drawing.Size(150, 20);
            this.ip_port_chat.TabIndex = 11;
            this.ip_port_chat.Text = "IP and PORT here.";
            this.ip_port_chat.Enter += new System.EventHandler(this.ip_port_chat_Enter);
            this.ip_port_chat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ip_port_chat_KeyPress);
            this.ip_port_chat.Leave += new System.EventHandler(this.ip_port_chat_Leave);
            // 
            // sel_send_lab
            // 
            this.sel_send_lab.AutoSize = true;
            this.sel_send_lab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sel_send_lab.Location = new System.Drawing.Point(3, 5);
            this.sel_send_lab.Name = "sel_send_lab";
            this.sel_send_lab.Size = new System.Drawing.Size(158, 16);
            this.sel_send_lab.TabIndex = 2;
            this.sel_send_lab.Text = "Select and send a file";
            // 
            // file_path_chat
            // 
            this.file_path_chat.AllowDrop = true;
            this.file_path_chat.ForeColor = System.Drawing.Color.Gray;
            this.file_path_chat.Location = new System.Drawing.Point(20, 30);
            this.file_path_chat.Name = "file_path_chat";
            this.file_path_chat.Size = new System.Drawing.Size(300, 20);
            this.file_path_chat.TabIndex = 10;
            this.file_path_chat.Text = "File path here.";
            this.file_path_chat.WordWrap = false;
            this.file_path_chat.DragDrop += new System.Windows.Forms.DragEventHandler(this.file_path_chat_DragDrop);
            this.file_path_chat.DragOver += new System.Windows.Forms.DragEventHandler(this.file_path_chat_DragOver);
            this.file_path_chat.Enter += new System.EventHandler(this.file_path_chat_Enter);
            this.file_path_chat.Leave += new System.EventHandler(this.file_path_chat_Leave);
            // 
            // open_file_but
            // 
            this.open_file_but.Location = new System.Drawing.Point(330, 30);
            this.open_file_but.Name = "open_file_but";
            this.open_file_but.Size = new System.Drawing.Size(75, 20);
            this.open_file_but.TabIndex = 1;
            this.open_file_but.Text = "Open";
            this.open_file_but.UseVisualStyleBackColor = true;
            this.open_file_but.Click += new System.EventHandler(this.open_file_but_Click);
            // 
            // secondary_split
            // 
            this.secondary_split.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.secondary_split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secondary_split.IsSplitterFixed = true;
            this.secondary_split.Location = new System.Drawing.Point(0, 0);
            this.secondary_split.Name = "secondary_split";
            // 
            // secondary_split.Panel1
            // 
            this.secondary_split.Panel1.Controls.Add(this.send_list);
            this.secondary_split.Size = new System.Drawing.Size(760, 259);
            this.secondary_split.SplitterDistance = 380;
            this.secondary_split.TabIndex = 0;
            // 
            // send_list
            // 
            this.send_list.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.send_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.send_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.file_name,
            this.file_size,
            this.progress_bar});
            this.send_list.HideSelection = false;
            this.send_list.Location = new System.Drawing.Point(4, 3);
            this.send_list.Name = "send_list";
            this.send_list.Size = new System.Drawing.Size(371, 251);
            this.send_list.TabIndex = 0;
            this.send_list.TabStop = false;
            this.send_list.UseCompatibleStateImageBehavior = false;
            this.send_list.View = System.Windows.Forms.View.Details;
            // 
            // file_name
            // 
            this.file_name.Text = "File Name";
            this.file_name.Width = 150;
            // 
            // file_size
            // 
            this.file_size.Text = "File Size (B)";
            this.file_size.Width = 67;
            // 
            // progress_bar
            // 
            this.progress_bar.Text = "Progress Bar";
            this.progress_bar.Width = 150;
            // 
            // open_file_dialog
            // 
            this.open_file_dialog.FileName = "open_file_dialog";
            // 
            // transfer_form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.main_split);
            this.Controls.Add(this.menu);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "transfer_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Transfer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.transfer_form_FormClosing);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.main_split.Panel1.ResumeLayout(false);
            this.main_split.Panel1.PerformLayout();
            this.main_split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.main_split)).EndInit();
            this.main_split.ResumeLayout(false);
            this.secondary_split.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.secondary_split)).EndInit();
            this.secondary_split.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem menu_file_start;
        private System.Windows.Forms.ToolStripMenuItem menu_file_stop;
        private System.Windows.Forms.TextBox file_path_chat;
        private System.Windows.Forms.Button open_file_but;
        private System.Windows.Forms.OpenFileDialog open_file_dialog;
        private System.Windows.Forms.SplitContainer main_split;
        private System.Windows.Forms.Label sel_send_lab;
        private System.Windows.Forms.TextBox ip_port_chat;
        private System.Windows.Forms.Button send_file_but;
        private System.Windows.Forms.SplitContainer secondary_split;
        private System.Windows.Forms.ListView send_list;
        private System.Windows.Forms.ColumnHeader file_name;
        private System.Windows.Forms.ColumnHeader file_size;
        private System.Windows.Forms.ColumnHeader progress_bar;
    }
}

