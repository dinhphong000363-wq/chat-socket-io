namespace Chat_DinhPhong
{
    partial class Client
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            txbMessage = new TextBox();
            btnSent = new Button();
            rtbMess = new RichTextBox();
            btnDelete = new Button();
            btnIcon = new Button();
            btnImage = new Button();
            button1 = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // txbMessage
            // 
            txbMessage.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txbMessage.Location = new Point(72, 534);
            txbMessage.Multiline = true;
            txbMessage.Name = "txbMessage";
            txbMessage.PlaceholderText = "Nhập tin nhắn....";
            txbMessage.Size = new Size(390, 48);
            txbMessage.TabIndex = 1;
            txbMessage.KeyDown += txbMessage_KeyDown;
            // 
            // btnSent
            // 
            btnSent.BackColor = Color.Cyan;
            btnSent.Image = (Image)resources.GetObject("btnSent.Image");
            btnSent.Location = new Point(468, 534);
            btnSent.Name = "btnSent";
            btnSent.Size = new Size(64, 44);
            btnSent.TabIndex = 2;
            btnSent.UseVisualStyleBackColor = false;
            btnSent.Click += btnSent_Click;
            // 
            // rtbMess
            // 
            rtbMess.BackColor = Color.FromArgb(255, 128, 128);
            rtbMess.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtbMess.Location = new Point(72, 12);
            rtbMess.Name = "rtbMess";
            rtbMess.Size = new Size(472, 441);
            rtbMess.TabIndex = 3;
            rtbMess.Text = "";
            rtbMess.TextChanged += rtbMess_TextChanged;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(255, 128, 128);
            btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
            btnDelete.Location = new Point(133, 7);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(64, 44);
            btnDelete.TabIndex = 4;
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnIcon
            // 
            btnIcon.BackColor = Color.FromArgb(255, 128, 128);
            btnIcon.Image = (Image)resources.GetObject("btnIcon.Image");
            btnIcon.Location = new Point(20, 7);
            btnIcon.Name = "btnIcon";
            btnIcon.Size = new Size(64, 44);
            btnIcon.TabIndex = 5;
            btnIcon.UseVisualStyleBackColor = false;
            btnIcon.Click += btnIcon_Click;
            // 
            // btnImage
            // 
            btnImage.BackColor = Color.FromArgb(255, 128, 128);
            btnImage.Image = (Image)resources.GetObject("btnImage.Image");
            btnImage.Location = new Point(257, 7);
            btnImage.Name = "btnImage";
            btnImage.Size = new Size(64, 44);
            btnImage.TabIndex = 6;
            btnImage.UseVisualStyleBackColor = false;
            btnImage.Click += btnImage_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 128, 128);
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(372, 7);
            button1.Name = "button1";
            button1.Size = new Size(64, 44);
            button1.TabIndex = 7;
            button1.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(52, 570);
            panel1.TabIndex = 8;
            // 
            // panel2
            // 
            panel2.BackgroundImage = (Image)resources.GetObject("panel2.BackgroundImage");
            panel2.Controls.Add(button1);
            panel2.Controls.Add(btnDelete);
            panel2.Controls.Add(btnIcon);
            panel2.Controls.Add(btnImage);
            panel2.Location = new Point(72, 468);
            panel2.Name = "panel2";
            panel2.Size = new Size(472, 60);
            panel2.TabIndex = 9;
            // 
            // Client
            // 
            AcceptButton = btnSent;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(562, 593);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(rtbMess);
            Controls.Add(btnSent);
            Controls.Add(txbMessage);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Client";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Client";
            FormClosed += Client_FormClosed;
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txbMessage;
        private Button btnSent;
        private RichTextBox rtbMess;
        private Button btnDelete;
        private Button btnIcon;
        private Button btnImage;
        private Button button1;
        private Panel panel1;
        private Panel panel2;
    }
}
