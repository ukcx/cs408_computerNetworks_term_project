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
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_post = new System.Windows.Forms.Button();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_request = new System.Windows.Forms.Button();
            this.listUsers = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.followBox = new System.Windows.Forms.TextBox();
            this.followButton = new System.Windows.Forms.Button();
            this.followedSweets = new System.Windows.Forms.Button();
            this.users = new System.Windows.Forms.RichTextBox();
            this.button_get_sweets = new System.Windows.Forms.Button();
            this.button_get_followed = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.MySweets = new System.Windows.Forms.CheckedListBox();
            this.button_get_follower = new System.Windows.Forms.Button();
            this.textBox_block = new System.Windows.Forms.TextBox();
            this.button_block = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button_refresh = new System.Windows.Forms.Button();
            this.follow_list = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(89, 34);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(263, 22);
            this.textBox_ip.TabIndex = 4;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(89, 70);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(263, 22);
            this.textBox_port.TabIndex = 5;
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(89, 146);
            this.button_connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(129, 25);
            this.button_connect.TabIndex = 7;
            this.button_connect.Text = "connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(10, 408);
            this.logs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(581, 260);
            this.logs.TabIndex = 12;
            this.logs.TabStop = false;
            this.logs.Text = "";
            // 
            // textBox_message
            // 
            this.textBox_message.Enabled = false;
            this.textBox_message.Location = new System.Drawing.Point(89, 361);
            this.textBox_message.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(161, 22);
            this.textBox_message.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 364);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Sweet:";
            // 
            // button_post
            // 
            this.button_post.Enabled = false;
            this.button_post.Location = new System.Drawing.Point(267, 356);
            this.button_post.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_post.Name = "button_post";
            this.button_post.Size = new System.Drawing.Size(87, 32);
            this.button_post.TabIndex = 15;
            this.button_post.Text = "post";
            this.button_post.UseVisualStyleBackColor = true;
            this.button_post.Click += new System.EventHandler(this.button_post_Click);
            // 
            // button_disconnect
            // 
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Location = new System.Drawing.Point(225, 146);
            this.button_disconnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(128, 25);
            this.button_disconnect.TabIndex = 8;
            this.button_disconnect.Text = "disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(91, 105);
            this.textBox_username.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(261, 22);
            this.textBox_username.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Username:";
            // 
            // button_request
            // 
            this.button_request.Enabled = false;
            this.button_request.Location = new System.Drawing.Point(89, 265);
            this.button_request.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_request.Name = "button_request";
            this.button_request.Size = new System.Drawing.Size(264, 33);
            this.button_request.TabIndex = 11;
            this.button_request.Text = "request sweets";
            this.button_request.UseVisualStyleBackColor = true;
            this.button_request.Click += new System.EventHandler(this.button_request_Click);
            // 
            // listUsers
            // 
            this.listUsers.Enabled = false;
            this.listUsers.Location = new System.Drawing.Point(91, 194);
            this.listUsers.Margin = new System.Windows.Forms.Padding(4);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(264, 28);
            this.listUsers.TabIndex = 9;
            this.listUsers.Text = "list users";
            this.listUsers.UseVisualStyleBackColor = true;
            this.listUsers.Click += new System.EventHandler(this.listUsers_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 322);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Following:";
            // 
            // followBox
            // 
            this.followBox.Enabled = false;
            this.followBox.Location = new System.Drawing.Point(89, 319);
            this.followBox.Margin = new System.Windows.Forms.Padding(4);
            this.followBox.Name = "followBox";
            this.followBox.Size = new System.Drawing.Size(161, 22);
            this.followBox.TabIndex = 12;
            // 
            // followButton
            // 
            this.followButton.Enabled = false;
            this.followButton.Location = new System.Drawing.Point(268, 314);
            this.followButton.Margin = new System.Windows.Forms.Padding(4);
            this.followButton.Name = "followButton";
            this.followButton.Size = new System.Drawing.Size(87, 32);
            this.followButton.TabIndex = 13;
            this.followButton.Text = "follow";
            this.followButton.UseVisualStyleBackColor = true;
            this.followButton.Click += new System.EventHandler(this.followButton_Click);
            // 
            // followedSweets
            // 
            this.followedSweets.Enabled = false;
            this.followedSweets.Location = new System.Drawing.Point(89, 230);
            this.followedSweets.Margin = new System.Windows.Forms.Padding(4);
            this.followedSweets.Name = "followedSweets";
            this.followedSweets.Size = new System.Drawing.Size(264, 28);
            this.followedSweets.TabIndex = 10;
            this.followedSweets.Text = "sweets from followed";
            this.followedSweets.UseVisualStyleBackColor = true;
            this.followedSweets.Click += new System.EventHandler(this.followedSweets_Click);
            // 
            // users
            // 
            this.users.Location = new System.Drawing.Point(362, 34);
            this.users.Name = "users";
            this.users.ReadOnly = true;
            this.users.Size = new System.Drawing.Size(229, 354);
            this.users.TabIndex = 18;
            this.users.TabStop = false;
            this.users.Text = "";
            // 
            // button_get_sweets
            // 
            this.button_get_sweets.Location = new System.Drawing.Point(0, 0);
            this.button_get_sweets.Name = "button_get_sweets";
            this.button_get_sweets.Size = new System.Drawing.Size(75, 23);
            this.button_get_sweets.TabIndex = 0;
            // 
            // button_get_followed
            // 
            this.button_get_followed.Enabled = false;
            this.button_get_followed.Location = new System.Drawing.Point(612, 640);
            this.button_get_followed.Name = "button_get_followed";
            this.button_get_followed.Size = new System.Drawing.Size(140, 28);
            this.button_get_followed.TabIndex = 21;
            this.button_get_followed.Text = "get followed users";
            this.button_get_followed.UseVisualStyleBackColor = true;
            this.button_get_followed.Click += new System.EventHandler(this.button_get_followed_Click);
            // 
            // button_delete
            // 
            this.button_delete.Enabled = false;
            this.button_delete.Location = new System.Drawing.Point(612, 284);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(286, 41);
            this.button_delete.TabIndex = 18;
            this.button_delete.Text = "delete selected";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(607, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 25);
            this.label6.TabIndex = 26;
            this.label6.Text = "My Sweets";
            // 
            // MySweets
            // 
            this.MySweets.ColumnWidth = 1000;
            this.MySweets.Enabled = false;
            this.MySweets.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MySweets.FormattingEnabled = true;
            this.MySweets.HorizontalScrollbar = true;
            this.MySweets.Location = new System.Drawing.Point(612, 70);
            this.MySweets.Name = "MySweets";
            this.MySweets.Size = new System.Drawing.Size(286, 194);
            this.MySweets.TabIndex = 17;
            this.MySweets.UseCompatibleTextRendering = true;
            this.MySweets.SelectedIndexChanged += new System.EventHandler(this.MySweets_SelectedIndexChanged);
            // 
            // button_get_follower
            // 
            this.button_get_follower.Enabled = false;
            this.button_get_follower.Location = new System.Drawing.Point(758, 640);
            this.button_get_follower.Name = "button_get_follower";
            this.button_get_follower.Size = new System.Drawing.Size(140, 28);
            this.button_get_follower.TabIndex = 22;
            this.button_get_follower.Text = "get my followers";
            this.button_get_follower.UseVisualStyleBackColor = true;
            this.button_get_follower.Click += new System.EventHandler(this.button_get_follower_Click);
            // 
            // textBox_block
            // 
            this.textBox_block.Enabled = false;
            this.textBox_block.Location = new System.Drawing.Point(661, 359);
            this.textBox_block.Name = "textBox_block";
            this.textBox_block.Size = new System.Drawing.Size(146, 22);
            this.textBox_block.TabIndex = 19;
            // 
            // button_block
            // 
            this.button_block.Enabled = false;
            this.button_block.Location = new System.Drawing.Point(813, 356);
            this.button_block.Name = "button_block";
            this.button_block.Size = new System.Drawing.Size(85, 27);
            this.button_block.TabIndex = 20;
            this.button_block.Text = "block";
            this.button_block.UseVisualStyleBackColor = true;
            this.button_block.Click += new System.EventHandler(this.button_block_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(609, 362);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 17);
            this.label7.TabIndex = 30;
            this.label7.Text = "Block:";
            // 
            // button_refresh
            // 
            this.button_refresh.Enabled = false;
            this.button_refresh.Location = new System.Drawing.Point(822, 37);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(75, 23);
            this.button_refresh.TabIndex = 16;
            this.button_refresh.Text = "refresh";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // follow_list
            // 
            this.follow_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.follow_list.Location = new System.Drawing.Point(612, 408);
            this.follow_list.Name = "follow_list";
            this.follow_list.ReadOnly = true;
            this.follow_list.Size = new System.Drawing.Size(285, 226);
            this.follow_list.TabIndex = 32;
            this.follow_list.TabStop = false;
            this.follow_list.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 690);
            this.Controls.Add(this.follow_list);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button_block);
            this.Controls.Add(this.textBox_block);
            this.Controls.Add(this.button_get_follower);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.MySweets);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_get_followed);
            this.Controls.Add(this.users);
            this.Controls.Add(this.followedSweets);
            this.Controls.Add(this.followButton);
            this.Controls.Add(this.followBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listUsers);
            this.Controls.Add(this.button_request);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.button_post);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "client";
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
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_post;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_request;
        private System.Windows.Forms.Button listUsers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox followBox;
        private System.Windows.Forms.Button followButton;
        private System.Windows.Forms.Button followedSweets;
        private System.Windows.Forms.RichTextBox users;
        private System.Windows.Forms.Button button_get_sweets;
        private System.Windows.Forms.Button button_get_followed;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox MySweets;
        private System.Windows.Forms.Button button_get_follower;
        private System.Windows.Forms.TextBox textBox_block;
        private System.Windows.Forms.Button button_block;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.RichTextBox follow_list;
    }
}

