using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;


namespace Sever
{
    public partial class Server : Form
    {
        public Server()
        {
            //khởi tạo server
            InitializeComponent();
            StartServer(); 
        }

        IPEndPoint IP;
        Socket serverSocket;
        List<Socket> clientSockets = new List<Socket>(); 

       
        void StartServer()
        {
            //khởi động server
            IP = new IPEndPoint(IPAddress.Any, 9999);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                serverSocket.Bind(IP);
                serverSocket.Listen(10); 

                Thread acceptThread = new Thread(AcceptClients);
                acceptThread.IsBackground = true;
                acceptThread.Start();

                AddMessage("Server started and waiting for clients...");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi động server: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //chấp nhận kết nối từ client
        void AcceptClients()
        {
            try
            {
                while (true)
                {
                    Socket client = serverSocket.Accept();
                    clientSockets.Add(client);// kết nối

                    Thread receiveThread = new Thread(() => ReceiveData(client));
                    receiveThread.IsBackground = true;
                    receiveThread.Start();

                   
                    AddClientToList(client);

                    AddMessage("New client connected.");
                }
            }
            catch (Exception ex)
            {
                AddMessage($"Lỗi khi chấp nhận client: {ex.Message}");
            }
        }


        void AddClientToList(Socket client)
        {
            if (clbClient.InvokeRequired)
            {
                clbClient.Invoke(new Action(() => AddClientToList(client)));
                return;
            }

            string clientInfo = client.RemoteEndPoint.ToString(); 
            clbClient.Items.Add(clientInfo, true);
        }


        //nhận data
        void ReceiveData(Socket client)
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[8192 * 8 * 8 * 8];
                    int receivedBytes = client.Receive(buffer);

                    if (receivedBytes > 0)
                    {
                        try
                        {
                            using (MemoryStream ms = new MemoryStream(buffer, 0, receivedBytes))
                            {
                                Image receivedImage = Image.FromStream(ms);

                                // Resize image before displaying
                                Image resizedImage = ResizeImage(receivedImage, 300, 300);

                                // Display resized image
                                DisplayImage(resizedImage);

                                // Broadcast the image to other clients
                                foreach (Socket otherClient in clientSockets)
                                {
                                    if (otherClient != client && otherClient.Connected)
                                    {
                                        otherClient.Send(buffer, receivedBytes, SocketFlags.None);
                                    }
                                }

                                AddMessage($"[Server]: Hình ảnh nhận từ {client.RemoteEndPoint}");
                                continue;
                            }
                        }
                        catch
                        {
                            string message = Deserialize(buffer);
                            AddMessage($"Client {client.RemoteEndPoint}: {message}");

                            BroadcastMessage(message, client);
                        }
                    }
                }
            }
            catch
            {
                RemoveClientFromList(client);
                clientSockets.Remove(client);
                client.Close();
            }
        }

        private Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            int newWidth = image.Width;
            int newHeight = image.Height;

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

        private void DisplayImage(Image image)
        {
            try
            {
                if (rtbMess.InvokeRequired)
                {
                    rtbMess.Invoke(new Action(() => DisplayImage(image)));
                    return;
                }

                // Resize image với kích thước giới hạn
                Image resizedImage = ResizeImage(image, 4000, 4000);

                // Sao chép hình ảnh vào clipboard và dán vào RichTextBox
                Clipboard.SetImage(resizedImage);
                rtbMess.Paste();
                rtbMess.AppendText(Environment.NewLine);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Image resize error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ExternalException ex)
            {
                MessageBox.Show($"Clipboard error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        void RemoveClientFromList(Socket client)
        {
            if (clbClient.InvokeRequired)
            {
                clbClient.Invoke(new Action(() => RemoveClientFromList(client)));
                return;
            }

            string clientInfo = client.RemoteEndPoint.ToString();
            clbClient.Items.Remove(clientInfo);
        }



        void SendMessageFromServer()
        {
            string message = txbMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                AddMessage($"[Server]: {message}");
                BroadcastMessage($"[Server]: {message}", null); 
                txbMessage.Clear();
            }
        }

     
        void BroadcastMessage(string message, Socket excludeClient)
        {
            byte[] data = Serialize(message);

            for (int i = 0; i < clientSockets.Count; i++)
            {
                var client = clientSockets[i];
                string clientInfo = client.RemoteEndPoint.ToString();

               
                if (client != excludeClient && client.Connected && IsClientChecked(clientInfo))
                {
                    client.Send(data);
                }
            }
        }


        
        bool IsClientChecked(string clientInfo)
        {
            if (clbClient.InvokeRequired)
            {
                return (bool)clbClient.Invoke(new Func<bool>(() => IsClientChecked(clientInfo)));
            }

            int index = clbClient.Items.IndexOf(clientInfo);
            return index != -1 && clbClient.GetItemChecked(index);
        }


        
        void AddMessage(string message)
        {
            if (rtbMess.InvokeRequired)
            {
                rtbMess.Invoke(new Action(() => AddMessage(message)));
                return;
            }

           
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string formattedMessage = $"[{timestamp}] {message}";

            
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


        
        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var client in clientSockets)
            {
                if (client.Connected)
                {
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
            }

            serverSocket.Close();
        }

       
        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessageFromServer();
        }

        private void clbClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void txbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SendMessageFromServer();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedLine();

        }
        void DeleteSelectedLine()
        {
            if (rtbMess.InvokeRequired)
            {
                rtbMess.Invoke(new Action(() => DeleteSelectedLine()));
                return;
            }

           
            int currentLineIndex = rtbMess.GetLineFromCharIndex(rtbMess.SelectionStart);

           
            if (currentLineIndex >= 0)
            {
                
                int start = rtbMess.GetFirstCharIndexFromLine(currentLineIndex);
                int end = rtbMess.GetFirstCharIndexFromLine(currentLineIndex + 1);

               
                if (end == -1)
                {
                    end = rtbMess.TextLength;
                }

                
                string lineToDelete = rtbMess.Text.Substring(start, end - start);

              
                rtbMess.Text = rtbMess.Text.Remove(start, lineToDelete.Length);

               
                rtbMess.SelectionStart = start;
                rtbMess.SelectionLength = 0;
            }
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            if (clbClient.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn client để gửi hình ảnh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Chọn client để gửi
            string selectedClientInfo = clbClient.SelectedItem.ToString();
            Socket selectedClient = clientSockets.Find(client => client.RemoteEndPoint.ToString() == selectedClientInfo);

            if (selectedClient == null || !selectedClient.Connected)
            {
                MessageBox.Show("Client không khả dụng hoặc đã ngắt kết nối.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Chọn hình ảnh để gửi";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);

                        // Gửi dữ liệu hình ảnh tới client
                        selectedClient.Send(imageBytes);

                        // Hiển thị hình ảnh lên giao diện
                        using (Image image = Image.FromFile(openFileDialog.FileName))
                        {
                            DisplayImage(image);
                        }

                        AddMessage($"[Server]: Đã gửi hình ảnh tới {selectedClientInfo}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi gửi hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void btnIcon_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem emojiPanel đã tồn tại hay chưa
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




    }
}
