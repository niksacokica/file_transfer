
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
            this.main_split = new System.Windows.Forms.SplitContainer();
            this.send_file_but = new System.Windows.Forms.Button();
            this.ip_port_chat = new System.Windows.Forms.TextBox();
            this.sel_send_lab = new System.Windows.Forms.Label();
            this.file_path_chat = new System.Windows.Forms.TextBox();
            this.open_file_but = new System.Windows.Forms.Button();
            this.secondary_split = new System.Windows.Forms.SplitContainer();
            this.sent_files_prog_label = new System.Windows.Forms.Label();
            this.send_list = new file_transfer.ListViewScroll();
            this.sent_file_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sent_file_size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sent_progress_bar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.receive_split = new System.Windows.Forms.SplitContainer();
            this.stop_rec_but = new System.Windows.Forms.Button();
            this.start_rec_but = new System.Windows.Forms.Button();
            this.your_ip_port_chat = new System.Windows.Forms.TextBox();
            this.ip_port_server_lab = new System.Windows.Forms.Label();
            this.rec_list = new file_transfer.ListViewScroll();
            this.rec_file_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rec_file_size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rec_progress_bar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rec_files_prog_lab = new System.Windows.Forms.Label();
            this.open_file_dialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.main_split)).BeginInit();
            this.main_split.Panel1.SuspendLayout();
            this.main_split.Panel2.SuspendLayout();
            this.main_split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondary_split)).BeginInit();
            this.secondary_split.Panel1.SuspendLayout();
            this.secondary_split.Panel2.SuspendLayout();
            this.secondary_split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.receive_split)).BeginInit();
            this.receive_split.Panel1.SuspendLayout();
            this.receive_split.Panel2.SuspendLayout();
            this.receive_split.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_split
            // 
            this.main_split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.main_split.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.main_split.IsSplitterFixed = true;
            this.main_split.Location = new System.Drawing.Point(12, 12);
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
            this.main_split.Size = new System.Drawing.Size(760, 337);
            this.main_split.SplitterDistance = 61;
            this.main_split.TabIndex = 2;
            // 
            // send_file_but
            // 
            this.send_file_but.Location = new System.Drawing.Point(660, 29);
            this.send_file_but.Name = "send_file_but";
            this.send_file_but.Size = new System.Drawing.Size(75, 22);
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
            this.open_file_but.Location = new System.Drawing.Point(330, 29);
            this.open_file_but.Name = "open_file_but";
            this.open_file_but.Size = new System.Drawing.Size(75, 22);
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
            this.secondary_split.Panel1.Controls.Add(this.sent_files_prog_label);
            this.secondary_split.Panel1.Controls.Add(this.send_list);
            // 
            // secondary_split.Panel2
            // 
            this.secondary_split.Panel2.Controls.Add(this.receive_split);
            this.secondary_split.Size = new System.Drawing.Size(760, 272);
            this.secondary_split.SplitterDistance = 380;
            this.secondary_split.TabIndex = 0;
            this.secondary_split.TabStop = false;
            // 
            // sent_files_prog_label
            // 
            this.sent_files_prog_label.AutoSize = true;
            this.sent_files_prog_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sent_files_prog_label.Location = new System.Drawing.Point(3, 3);
            this.sent_files_prog_label.Name = "sent_files_prog_label";
            this.sent_files_prog_label.Size = new System.Drawing.Size(138, 16);
            this.sent_files_prog_label.TabIndex = 1;
            this.sent_files_prog_label.Text = "Sent files progress";
            // 
            // send_list
            // 
            this.send_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.send_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sent_file_name,
            this.sent_file_size,
            this.sent_progress_bar});
            this.send_list.GridLines = true;
            this.send_list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.send_list.HideSelection = false;
            this.send_list.Location = new System.Drawing.Point(4, 22);
            this.send_list.Name = "send_list";
            this.send_list.Size = new System.Drawing.Size(371, 245);
            this.send_list.TabIndex = 0;
            this.send_list.TabStop = false;
            this.send_list.UseCompatibleStateImageBehavior = false;
            this.send_list.View = System.Windows.Forms.View.Details;
            this.send_list.Scroll += new System.Windows.Forms.ScrollEventHandler(this.send_list_Scroll);
            this.send_list.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.send_list_ColumnWidthChanging);
            // 
            // sent_file_name
            // 
            this.sent_file_name.Text = "File Name";
            this.sent_file_name.Width = 150;
            // 
            // sent_file_size
            // 
            this.sent_file_size.Text = "File Size (B)";
            this.sent_file_size.Width = 67;
            // 
            // sent_progress_bar
            // 
            this.sent_progress_bar.Text = "Progress Bar";
            this.sent_progress_bar.Width = 130;
            // 
            // receive_split
            // 
            this.receive_split.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.receive_split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receive_split.IsSplitterFixed = true;
            this.receive_split.Location = new System.Drawing.Point(0, 0);
            this.receive_split.Name = "receive_split";
            this.receive_split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // receive_split.Panel1
            // 
            this.receive_split.Panel1.Controls.Add(this.stop_rec_but);
            this.receive_split.Panel1.Controls.Add(this.start_rec_but);
            this.receive_split.Panel1.Controls.Add(this.your_ip_port_chat);
            this.receive_split.Panel1.Controls.Add(this.ip_port_server_lab);
            // 
            // receive_split.Panel2
            // 
            this.receive_split.Panel2.Controls.Add(this.rec_list);
            this.receive_split.Panel2.Controls.Add(this.rec_files_prog_lab);
            this.receive_split.Size = new System.Drawing.Size(376, 272);
            this.receive_split.SplitterDistance = 60;
            this.receive_split.TabIndex = 0;
            // 
            // stop_rec_but
            // 
            this.stop_rec_but.BackColor = System.Drawing.Color.Red;
            this.stop_rec_but.Enabled = false;
            this.stop_rec_but.Location = new System.Drawing.Point(275, 29);
            this.stop_rec_but.Name = "stop_rec_but";
            this.stop_rec_but.Size = new System.Drawing.Size(75, 22);
            this.stop_rec_but.TabIndex = 3;
            this.stop_rec_but.Text = "Stop";
            this.stop_rec_but.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.stop_rec_but.UseVisualStyleBackColor = false;
            this.stop_rec_but.Click += new System.EventHandler(this.stop_rec_but_Click);
            // 
            // start_rec_but
            // 
            this.start_rec_but.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.start_rec_but.Location = new System.Drawing.Point(180, 29);
            this.start_rec_but.Name = "start_rec_but";
            this.start_rec_but.Size = new System.Drawing.Size(75, 22);
            this.start_rec_but.TabIndex = 2;
            this.start_rec_but.Text = "Start";
            this.start_rec_but.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.start_rec_but.UseVisualStyleBackColor = false;
            this.start_rec_but.Click += new System.EventHandler(this.start_rec_but_Click);
            // 
            // your_ip_port_chat
            // 
            this.your_ip_port_chat.ForeColor = System.Drawing.Color.Gray;
            this.your_ip_port_chat.Location = new System.Drawing.Point(10, 30);
            this.your_ip_port_chat.Name = "your_ip_port_chat";
            this.your_ip_port_chat.Size = new System.Drawing.Size(150, 20);
            this.your_ip_port_chat.TabIndex = 1;
            this.your_ip_port_chat.Text = "Your IP and PORT here.";
            this.your_ip_port_chat.Enter += new System.EventHandler(this.your_ip_port_chat_Enter);
            this.your_ip_port_chat.Leave += new System.EventHandler(this.your_ip_port_chat_Leave);
            // 
            // ip_port_server_lab
            // 
            this.ip_port_server_lab.AutoSize = true;
            this.ip_port_server_lab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ip_port_server_lab.Location = new System.Drawing.Point(3, 3);
            this.ip_port_server_lab.Name = "ip_port_server_lab";
            this.ip_port_server_lab.Size = new System.Drawing.Size(224, 16);
            this.ip_port_server_lab.TabIndex = 0;
            this.ip_port_server_lab.Text = "Your IP and PORT for receiving";
            // 
            // rec_list
            // 
            this.rec_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rec_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.rec_file_name,
            this.rec_file_size,
            this.rec_progress_bar});
            this.rec_list.GridLines = true;
            this.rec_list.HideSelection = false;
            this.rec_list.Location = new System.Drawing.Point(3, 23);
            this.rec_list.Name = "rec_list";
            this.rec_list.Size = new System.Drawing.Size(368, 180);
            this.rec_list.TabIndex = 1;
            this.rec_list.TabStop = false;
            this.rec_list.UseCompatibleStateImageBehavior = false;
            this.rec_list.View = System.Windows.Forms.View.Details;
            this.rec_list.Scroll += new System.Windows.Forms.ScrollEventHandler(this.rec_list_Scroll);
            this.rec_list.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.rec_list_ColumnWidthChanging);
            // 
            // rec_file_name
            // 
            this.rec_file_name.Text = "File Name";
            this.rec_file_name.Width = 150;
            // 
            // rec_file_size
            // 
            this.rec_file_size.Text = "File Size (B)";
            this.rec_file_size.Width = 67;
            // 
            // rec_progress_bar
            // 
            this.rec_progress_bar.Text = "Progress Bar";
            this.rec_progress_bar.Width = 130;
            // 
            // rec_files_prog_lab
            // 
            this.rec_files_prog_lab.AutoSize = true;
            this.rec_files_prog_lab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rec_files_prog_lab.Location = new System.Drawing.Point(3, 3);
            this.rec_files_prog_lab.Name = "rec_files_prog_lab";
            this.rec_files_prog_lab.Size = new System.Drawing.Size(177, 16);
            this.rec_files_prog_lab.TabIndex = 0;
            this.rec_files_prog_lab.Text = "Receiving files progress";
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
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "transfer_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Transfer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.transfer_form_FormClosing);
            this.SizeChanged += new System.EventHandler(this.transfer_form_SizeChanged);
            this.main_split.Panel1.ResumeLayout(false);
            this.main_split.Panel1.PerformLayout();
            this.main_split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.main_split)).EndInit();
            this.main_split.ResumeLayout(false);
            this.secondary_split.Panel1.ResumeLayout(false);
            this.secondary_split.Panel1.PerformLayout();
            this.secondary_split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.secondary_split)).EndInit();
            this.secondary_split.ResumeLayout(false);
            this.receive_split.Panel1.ResumeLayout(false);
            this.receive_split.Panel1.PerformLayout();
            this.receive_split.Panel2.ResumeLayout(false);
            this.receive_split.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.receive_split)).EndInit();
            this.receive_split.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox file_path_chat;
        private System.Windows.Forms.Button open_file_but;
        private System.Windows.Forms.OpenFileDialog open_file_dialog;
        private System.Windows.Forms.SplitContainer main_split;
        private System.Windows.Forms.Label sel_send_lab;
        private System.Windows.Forms.TextBox ip_port_chat;
        private System.Windows.Forms.Button send_file_but;
        private System.Windows.Forms.SplitContainer secondary_split;
        private System.Windows.Forms.ColumnHeader sent_file_name;
        private System.Windows.Forms.ColumnHeader sent_file_size;
        private System.Windows.Forms.ColumnHeader sent_progress_bar;
        private System.Windows.Forms.Label sent_files_prog_label;
        private System.Windows.Forms.SplitContainer receive_split;
        private System.Windows.Forms.TextBox your_ip_port_chat;
        private System.Windows.Forms.Label ip_port_server_lab;
        private System.Windows.Forms.Button start_rec_but;
        private System.Windows.Forms.Button stop_rec_but;
        private System.Windows.Forms.Label rec_files_prog_lab;
        private System.Windows.Forms.ColumnHeader rec_file_name;
        private System.Windows.Forms.ColumnHeader rec_file_size;
        private System.Windows.Forms.ColumnHeader rec_progress_bar;
        private ListViewScroll send_list;
        private ListViewScroll rec_list;
    }
}

