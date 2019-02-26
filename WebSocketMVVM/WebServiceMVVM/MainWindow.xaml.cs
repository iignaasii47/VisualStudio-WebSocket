using System;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.Http;



namespace WebServiceMVVM
{
    public partial class MainWindow : Window
    {
        private string user;
        private CancellationTokenSource cts;
        private ClientWebSocket socket;

        private static readonly HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            cts = new CancellationTokenSource();
            socket = new ClientWebSocket();
        }

        private async void B_Connect_ClickAsync(object sender, RoutedEventArgs e)
        {
            user = TB_User.Text;
            await Start();
        }

        private async void B_Send_ClickAsync(object sender, RoutedEventArgs e)
        {
            string message = this.TB_Message.Text;

            string missatge = TB_Message.Text;
            TB_Message.Text = "";
            if (missatge == "Adeu")
            {
                cts.Cancel();
                return;
            }
            byte[] sendBytes = Encoding.UTF8.GetBytes(missatge);
            var sendBuffer = new ArraySegment<byte>(sendBytes);
            await socket.SendAsync(sendBuffer, WebSocketMessageType.Text, endOfMessage: true, cancellationToken: cts.Token);
        }

        public async Task Start()
        {
            string wsUri = string.Format("ws://localhost:50503/api/websocket?nom={0}", user);
            await socket.ConnectAsync(new Uri(wsUri), cts.Token);
            Console.WriteLine(socket.State);

            await Task.Factory.StartNew(
                async () =>
                {
                    var rcvBytes = new byte[128];
                    var rcvBuffer = new ArraySegment<byte>(rcvBytes);
                    while (true)
                    {
                        WebSocketReceiveResult rcvResult = await socket.ReceiveAsync(rcvBuffer, cts.Token);
                        byte[] msgBytes = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray();
                        string rcvMsg = Encoding.UTF8.GetString(msgBytes);
                        TB_ChatLog.Text += rcvMsg;
                    }
                }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                this.TB_Message.AppendText(result.ToString());
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
