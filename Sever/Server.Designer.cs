namespace Sever
{
    partial class Server
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Dọn dẹp tài nguyên đang sử dụng.
        /// </summary>
        /// <param name="disposing">true nếu cần giải phóng tài nguyên; false nếu không.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code Designer hỗ trợ

        /// <summary>
        /// Phương thức này được thiết kế tự động, không sửa nội dung trực tiếp bằng trình chỉnh sửa mã.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            txbMessage = new TextBox();
            btnSend = new Button();
            rtbMess = new RichTextBox();
            clbClient = new CheckedListBox();
            btnDelete = new Button();
            btnImage = new Button();
            btnIcon = new Button();
            panel1 = new Panel();
            btnFile = new Button();
            groupBox1 = new GroupBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txbMessage
            // 
            txbMessage.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txbMessage.Location = new Point(65, 537);
            txbMessage.Multiline = true;
            txbMessage.Name = "txbMessage";
            txbMessage.PlaceholderText = "Nhập tin nhắn....";
            txbMessage.Size = new Size(664, 44);
            txbMessage.TabIndex = 1;
            txbMessage.KeyDown += txbMessage_KeyDown;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.FromArgb(192, 255, 255);
            btnSend.Image = (Image)resources.GetObject("btnSend.Image");
            btnSend.Location = new Point(735, 530);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(95, 48);
            btnSend.TabIndex = 2;
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // rtbMess
            // 
            rtbMess.BackColor = Color.White;
            rtbMess.BorderStyle = BorderStyle.FixedSingle;
            rtbMess.ForeColor = SystemColors.HotTrack;
            rtbMess.Location = new Point(24, 12);
            rtbMess.Name = "rtbMess";
            rtbMess.Size = new Size(573, 451);
            rtbMess.TabIndex = 3;
            rtbMess.Text = "";
            // 
            // clbClient
            // 
            clbClient.BackColor = SystemColors.GradientActiveCaption;
            clbClient.FormattingEnabled = true;
            clbClient.Location = new Point(603, 113);
            clbClient.Name = "clbClient";
            clbClient.Size = new Size(313, 356);
            clbClient.TabIndex = 4;
            clbClient.SelectedIndexChanged += clbClient_SelectedIndexChanged;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(192, 255, 255);
            btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
            btnDelete.Location = new Point(280, 9);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(64, 44);
            btnDelete.TabIndex = 5;
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnImage
            // 
            btnImage.BackColor = Color.FromArgb(192, 255, 255);
            btnImage.Image = (Image)resources.GetObject("btnImage.Image");
            btnImage.Location = new Point(469, 6);
            btnImage.Name = "btnImage";
            btnImage.Size = new Size(62, 44);
            btnImage.TabIndex = 6;
            btnImage.UseVisualStyleBackColor = false;
            btnImage.Click += btnImage_Click;
            // 
            // btnIcon
            // 
            btnIcon.BackColor = Color.FromArgb(192, 255, 255);
            btnIcon.Image = (Image)resources.GetObject("btnIcon.Image");
            btnIcon.Location = new Point(82, 6);
            btnIcon.Name = "btnIcon";
            btnIcon.Size = new Size(64, 44);
            btnIcon.TabIndex = 7;
            btnIcon.UseVisualStyleBackColor = false;
            btnIcon.Click += btnIcon_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(192, 255, 255);
            panel1.Controls.Add(btnFile);
            panel1.Controls.Add(btnImage);
            panel1.Controls.Add(btnIcon);
            panel1.Controls.Add(btnDelete);
            panel1.Location = new Point(65, 475);
            panel1.Name = "panel1";
            panel1.Size = new Size(765, 56);
            panel1.TabIndex = 8;
            // 
            // btnFile
            // 
            btnFile.BackColor = Color.FromArgb(192, 255, 255);
            btnFile.Image = (Image)resources.GetObject("btnFile.Image");
            btnFile.Location = new Point(638, 6);
            btnFile.Name = "btnFile";
            btnFile.Size = new Size(64, 44);
            btnFile.TabIndex = 8;
            btnFile.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.ForeColor = Color.FromArgb(128, 255, 255);
            groupBox1.Location = new Point(603, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(313, 95);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "ｌｉｓｔ_ｃｌｉｅｎｔ";
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 64);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(919, 585);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Controls.Add(clbClient);
            Controls.Add(rtbMess);
            Controls.Add(btnSend);
            Controls.Add(txbMessage);
            ForeColor = Color.FromArgb(64, 64, 64);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Server";
            Text = "Server";
            FormClosed += Server_FormClosed;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.TextBox txbMessage;
        private System.Windows.Forms.Button btnSend;
        private RichTextBox rtbMess;
        private CheckedListBox clbClient;
        private Button btnDelete;
        private Button btnImage;
        private Button btnIcon;
        private Panel panel1;
        private Button btnFile;
        private GroupBox groupBox1;
    }
}
