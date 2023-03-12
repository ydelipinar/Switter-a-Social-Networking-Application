namespace client
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_UserName = new System.Windows.Forms.TextBox();
            this.button_feed_request = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_sweet = new System.Windows.Forms.TextBox();
            this.button_followed_feed = new System.Windows.Forms.Button();
            this.button_user_list = new System.Windows.Forms.Button();
            this.textBox_follow = new System.Windows.Forms.TextBox();
            this.button_follow = new System.Windows.Forms.Button();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.button_followed_list = new System.Windows.Forms.Button();
            this.button_follower_list = new System.Windows.Forms.Button();
            this.button_block = new System.Windows.Forms.Button();
            this.textBox_block = new System.Windows.Forms.TextBox();
            this.button_delete = new System.Windows.Forms.Button();
            this.textBox_delete = new System.Windows.Forms.TextBox();
            this.button_get_own_sweets = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(83, 11);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(88, 20);
            this.textBox_ip.TabIndex = 2;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(83, 36);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(1);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(88, 20);
            this.textBox_port.TabIndex = 3;
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(83, 96);
            this.button_connect.Margin = new System.Windows.Forms.Padding(1);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(87, 23);
            this.button_connect.TabIndex = 4;
            this.button_connect.Text = "connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // logs
            // 
            this.logs.BackColor = System.Drawing.SystemColors.Menu;
            this.logs.Location = new System.Drawing.Point(281, 11);
            this.logs.Margin = new System.Windows.Forms.Padding(1);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.logs.Size = new System.Drawing.Size(429, 367);
            this.logs.TabIndex = 5;
            this.logs.Text = "";
            // 
            // button_send
            // 
            this.button_send.Enabled = false;
            this.button_send.Location = new System.Drawing.Point(173, 161);
            this.button_send.Margin = new System.Windows.Forms.Padding(1);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(77, 23);
            this.button_send.TabIndex = 8;
            this.button_send.Text = "send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 65);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "UserName:";
            // 
            // textBox_UserName
            // 
            this.textBox_UserName.Location = new System.Drawing.Point(83, 65);
            this.textBox_UserName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_UserName.Name = "textBox_UserName";
            this.textBox_UserName.Size = new System.Drawing.Size(88, 20);
            this.textBox_UserName.TabIndex = 10;
            // 
            // button_feed_request
            // 
            this.button_feed_request.Enabled = false;
            this.button_feed_request.Location = new System.Drawing.Point(23, 161);
            this.button_feed_request.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_feed_request.Name = "button_feed_request";
            this.button_feed_request.Size = new System.Drawing.Size(147, 23);
            this.button_feed_request.TabIndex = 11;
            this.button_feed_request.Text = "Request All Feed";
            this.button_feed_request.UseVisualStyleBackColor = true;
            this.button_feed_request.Click += new System.EventHandler(this.button_feed_request_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 136);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Sweet";
            // 
            // textBox_sweet
            // 
            this.textBox_sweet.Enabled = false;
            this.textBox_sweet.Location = new System.Drawing.Point(83, 132);
            this.textBox_sweet.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_sweet.Name = "textBox_sweet";
            this.textBox_sweet.Size = new System.Drawing.Size(167, 20);
            this.textBox_sweet.TabIndex = 13;
            // 
            // button_followed_feed
            // 
            this.button_followed_feed.Enabled = false;
            this.button_followed_feed.Location = new System.Drawing.Point(23, 197);
            this.button_followed_feed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_followed_feed.Name = "button_followed_feed";
            this.button_followed_feed.Size = new System.Drawing.Size(148, 26);
            this.button_followed_feed.TabIndex = 14;
            this.button_followed_feed.Text = "Request Followed Feed";
            this.button_followed_feed.UseVisualStyleBackColor = true;
            this.button_followed_feed.Click += new System.EventHandler(this.button_followed_feed_Click);
            // 
            // button_user_list
            // 
            this.button_user_list.Enabled = false;
            this.button_user_list.Location = new System.Drawing.Point(173, 197);
            this.button_user_list.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_user_list.Name = "button_user_list";
            this.button_user_list.Size = new System.Drawing.Size(77, 26);
            this.button_user_list.TabIndex = 15;
            this.button_user_list.Text = "Get User List";
            this.button_user_list.UseVisualStyleBackColor = true;
            this.button_user_list.Click += new System.EventHandler(this.button_user_list_Click);
            // 
            // textBox_follow
            // 
            this.textBox_follow.Enabled = false;
            this.textBox_follow.Location = new System.Drawing.Point(102, 229);
            this.textBox_follow.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_follow.Name = "textBox_follow";
            this.textBox_follow.Size = new System.Drawing.Size(149, 20);
            this.textBox_follow.TabIndex = 16;
            // 
            // button_follow
            // 
            this.button_follow.Enabled = false;
            this.button_follow.Location = new System.Drawing.Point(23, 229);
            this.button_follow.Margin = new System.Windows.Forms.Padding(1);
            this.button_follow.Name = "button_follow";
            this.button_follow.Size = new System.Drawing.Size(64, 28);
            this.button_follow.TabIndex = 17;
            this.button_follow.Text = "follow";
            this.button_follow.UseVisualStyleBackColor = true;
            this.button_follow.Click += new System.EventHandler(this.button_follow_Click);
            // 
            // button_disconnect
            // 
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Location = new System.Drawing.Point(173, 96);
            this.button_disconnect.Margin = new System.Windows.Forms.Padding(1);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(77, 23);
            this.button_disconnect.TabIndex = 18;
            this.button_disconnect.Text = "disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // button_followed_list
            // 
            this.button_followed_list.Enabled = false;
            this.button_followed_list.Location = new System.Drawing.Point(142, 352);
            this.button_followed_list.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_followed_list.Name = "button_followed_list";
            this.button_followed_list.Size = new System.Drawing.Size(108, 26);
            this.button_followed_list.TabIndex = 19;
            this.button_followed_list.Text = "Get Followed List";
            this.button_followed_list.UseVisualStyleBackColor = true;
            this.button_followed_list.Click += new System.EventHandler(this.button_followed_list_Click);
            // 
            // button_follower_list
            // 
            this.button_follower_list.Enabled = false;
            this.button_follower_list.Location = new System.Drawing.Point(23, 352);
            this.button_follower_list.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_follower_list.Name = "button_follower_list";
            this.button_follower_list.Size = new System.Drawing.Size(108, 26);
            this.button_follower_list.TabIndex = 20;
            this.button_follower_list.Text = "Get Follower List";
            this.button_follower_list.UseVisualStyleBackColor = true;
            this.button_follower_list.Click += new System.EventHandler(this.button_follower_list_Click);
            // 
            // button_block
            // 
            this.button_block.Enabled = false;
            this.button_block.Location = new System.Drawing.Point(23, 271);
            this.button_block.Margin = new System.Windows.Forms.Padding(1);
            this.button_block.Name = "button_block";
            this.button_block.Size = new System.Drawing.Size(64, 27);
            this.button_block.TabIndex = 21;
            this.button_block.Text = "block";
            this.button_block.UseVisualStyleBackColor = true;
            this.button_block.Click += new System.EventHandler(this.button_block_Click);
            // 
            // textBox_block
            // 
            this.textBox_block.Enabled = false;
            this.textBox_block.Location = new System.Drawing.Point(102, 271);
            this.textBox_block.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_block.Name = "textBox_block";
            this.textBox_block.Size = new System.Drawing.Size(149, 20);
            this.textBox_block.TabIndex = 22;
            // 
            // button_delete
            // 
            this.button_delete.Enabled = false;
            this.button_delete.Location = new System.Drawing.Point(23, 313);
            this.button_delete.Margin = new System.Windows.Forms.Padding(1);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(108, 25);
            this.button_delete.TabIndex = 23;
            this.button_delete.Text = "delete sweet";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // textBox_delete
            // 
            this.textBox_delete.Enabled = false;
            this.textBox_delete.Location = new System.Drawing.Point(142, 313);
            this.textBox_delete.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_delete.Name = "textBox_delete";
            this.textBox_delete.Size = new System.Drawing.Size(109, 20);
            this.textBox_delete.TabIndex = 24;
            // 
            // button_get_own_sweets
            // 
            this.button_get_own_sweets.Enabled = false;
            this.button_get_own_sweets.Location = new System.Drawing.Point(189, 50);
            this.button_get_own_sweets.Margin = new System.Windows.Forms.Padding(1);
            this.button_get_own_sweets.Name = "button_get_own_sweets";
            this.button_get_own_sweets.Size = new System.Drawing.Size(61, 35);
            this.button_get_own_sweets.TabIndex = 25;
            this.button_get_own_sweets.Text = "Get Own Sweets";
            this.button_get_own_sweets.UseVisualStyleBackColor = true;
            this.button_get_own_sweets.Click += new System.EventHandler(this.button_get_own_sweets_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(757, 409);
            this.Controls.Add(this.button_get_own_sweets);
            this.Controls.Add(this.textBox_delete);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.textBox_block);
            this.Controls.Add(this.button_block);
            this.Controls.Add(this.button_follower_list);
            this.Controls.Add(this.button_followed_list);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.button_follow);
            this.Controls.Add(this.textBox_follow);
            this.Controls.Add(this.button_user_list);
            this.Controls.Add(this.button_followed_feed);
            this.Controls.Add(this.textBox_sweet);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_feed_request);
            this.Controls.Add(this.textBox_UserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_UserName;
        private System.Windows.Forms.Button button_feed_request;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_sweet;
        private System.Windows.Forms.Button button_followed_feed;
        private System.Windows.Forms.Button button_user_list;
        private System.Windows.Forms.TextBox textBox_follow;
        private System.Windows.Forms.Button button_follow;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Button button_followed_list;
        private System.Windows.Forms.Button button_follower_list;
        private System.Windows.Forms.Button button_block;
        private System.Windows.Forms.TextBox textBox_block;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.TextBox textBox_delete;
        private System.Windows.Forms.Button button_get_own_sweets;
    }
}

