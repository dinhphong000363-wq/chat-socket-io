using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Chat_DinhPhong
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
            Connect();
        }

        IPEndPoint IP;
        Socket client;

        
        void Connect()
        {
            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.Connect(IP);
                MessageBox.Show("Kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Lắng nghe tin nhắn từ server
                Thread listen = new Thread(Receive);
                listen.IsBackground = true;
                listen.Start();
            }
            catch
            {
                MessageBox.Show("Không thể kết nối với server!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CloseConnection()
        {
            if (client != null && client.Connected)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }

        void Send()
        {
            if (!string.IsNullOrEmpty(txbMessage.Text))
            {
                try
                {
                    string message = txbMessage.Text;
                    byte[] data = Serialize(message);
                    client.Send(data); 

                    AddMessage("You", message); 
                    txbMessage.Clear(); 
                }
                catch
                {
                    MessageBox.Show("Không thể gửi tin nhắn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[8192 * 8 * 8 * 8];
                    int size = client.Receive(data);

                    if (size > 0)
                    {
                        try
                        {
                            // Kiểm tra xem dữ liệu có phải hình ảnh không
                            using (MemoryStream ms = new MemoryStream(data, 0, size))
                            {
                                Image receivedImage = Image.FromStream(ms);

                                // Hiển thị hình ảnh
                                DisplayImage(receivedImage, "Server");
                                continue;
                            }
                        }
                        catch
                        {
                            // Không phải hình ảnh, xử lý như văn bản
                            string message = Deserialize(data);
                            AddMessage("Server", message);
                        }
                    }
                }
            }
            catch
            {
                CloseConnection();
            }
        }

        void DisplayImage(Image image, string sender)
        {
            try
            {
                if (rtbMess.InvokeRequired)
                {
                    rtbMess.Invoke(new Action(() => DisplayImage(image, sender)));
                    return;
                }

                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                rtbMess.AppendText($"[{timestamp}] {sender} sent an image:" + Environment.NewLine);

                // Thay đổi kích thước ảnh trước khi hiển thị
                Image resizedImage = ResizeImage(image, 4000, 4000); // Giới hạn kích thước ảnh là 200x200

                Clipboard.SetImage(resizedImage); // Copy hình ảnh đã resize vào clipboard
                rtbMess.Paste();                  // Dán hình ảnh vào RichTextBox
                rtbMess.AppendText(Environment.NewLine);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Image processing error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Clipboard error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        void AddMessage(string sender, string message)
        {
            if (rtbMess.InvokeRequired)
            {
                rtbMess.Invoke(new Action(() => AddMessage(sender, message)));
                return;
            }

            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string formattedMessage = $"[{timestamp}] {sender}: {message}";

            
            rtbMess.Font = new Font("Segoe UI Emoji", 12); 
            rtbMess.AppendText(formattedMessage + Environment.NewLine); 
        }


        byte[] Serialize(string message)
        {
            return Encoding.UTF8.GetBytes(message);
        }

        string Deserialize(byte[] data)
        {
            return Encoding.UTF8.GetString(data).TrimEnd('\0');
        }

        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseConnection();
        }

        private void btnSent_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void rtbMess_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void clbClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void txbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                e.Handled = true; 
                Send(); 
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteMessages();
        }
        void DeleteMessages()
        {
            if (rtbMess.InvokeRequired)
            {
                rtbMess.Invoke(new Action(() => DeleteMessages()));
                return;
            }

            
            rtbMess.Clear();
        }

        private void btnIcon_Click(object sender, EventArgs e)
        {
            FlowLayoutPanel emojiPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                Size = new Size(300, 200),
                Location = new Point(10, 320) // Vị trí hiển thị bảng emoji
            };

            // Danh sách các emoji
            string[] emojis = new string[]
            {
                "\U0001F600", "\U0001F609", "\U0001F44D", "\U0001F622", "\U0001F60E",
                "\U0001F618", "\U0001F60D", "\U0001F61E", "\U0001F636", "\U0001F910",
                "\U0001F496", "\U0001F319", "\U0001F525", "\U0001F4A9", "\U0001F4A3",
                "\U0001F4A8", "\U0001F4A6", "\U0001F4A5", "\U0001F4AB", "\U0001F4AC",
                "\U0001F4AD", "\U0001F6C0", "\U0001F6C1", "\U0001F6C2", "\U0001F6C3",
                "\U0001F6C4", "\U0001F6C5", "\U0001F6CB", "\U0001F6CC", "\U0001F6CD",
                "\U0001F44F", "\U0001F631", "\U0001F637", "\U0001F639", "\U0001F61C",
                "\U0001F633", "\U0001F643", "\U0001F923", "\U0001F602", "\U0001F609",
                "\U0001F615", "\U0001F635", "\U0001F631", "\U0001F616", "\U0001F624",
                "\U0001F60B", "\U0001F607", "\U0001F62D", "\U0001F62C", "\U0001F92F",
                "\U0001F973", "\U0001F974", "\U0001F975", "\U0001F60F", "\U0001F62E",
                "\U0001F60A", "\U0001F618", "\U0001F92A", "\U0001F9D0", "\U0001F607"
            };

            // Tạo các nút emoji
            foreach (var emoji in emojis)
            {
                Button emojiButton = new Button
                {
                    Text = emoji,
                    Font = new Font("Segoe UI Emoji", 20),
                    Size = new Size(50, 50)
                };

                emojiButton.Click += (s, args) =>
                {
                    txbMessage.Text += emoji; // Chèn emoji vào khung nhập liệu
                    emojiPanel.Hide();        // Ẩn bảng emoji sau khi chọn
                };

                emojiPanel.Controls.Add(emojiButton);
            }

            this.Controls.Add(emojiPanel);
            emojiPanel.BringToFront();
        }




        private void btnImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Chọn hình ảnh để gửi";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Tải hình ảnh và thay đổi kích thước
                        using (Image originalImage = Image.FromFile(openFileDialog.FileName))
                        {
                            Image resizedImage = ResizeImage(originalImage, 4000, 4000); // Giới hạn kích thước gửi là 300x300

                            // Chuyển đổi hình ảnh đã resize thành mảng byte
                            using (MemoryStream ms = new MemoryStream())
                            {
                                // Bạn có thể chọn định dạng lưu trữ hình ảnh (JPEG, PNG, v.v.)
                                resizedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                byte[] imageBytes = ms.ToArray();

                                // Gửi hình ảnh tới server
                                client.Send(imageBytes);

                                // Hiển thị hình ảnh đã gửi lên giao diện
                                DisplayImage(resizedImage, "You");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi gửi hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            int newWidth = image.Width;
            int newHeight = image.Height;

            // Kiểm tra nếu kích thước hiện tại vượt quá giới hạn
            if (image.Width > maxWidth || image.Height > maxHeight)
            {
                float aspectRatio = (float)image.Width / image.Height;

                if (aspectRatio > 1) // Hình ảnh ngang
                {
                    newWidth = maxWidth;
                    newHeight = (int)(maxWidth / aspectRatio);
                }
                else // Hình ảnh dọc hoặc vuông
                {
                    newHeight = maxHeight;
                    newWidth = (int)(maxHeight * aspectRatio);
                }
            }

            Bitmap resizedImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return resizedImage;
        }

    }
}
