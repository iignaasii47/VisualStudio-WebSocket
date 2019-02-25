using System;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebServiceMVVM
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void B_Connect_Click(object sender, RoutedEventArgs e)
        {
            string user = this.TB_User.Text;

            // TODO: conectar con el websocket pasando el user
        }

        private void B_Send_Click(object sender, RoutedEventArgs e)
        {
            string message = this.TB_Message.Text;

            // TODO: enviar el message al websocket para broadcastear cosas
        }

        // TODO: recepcion del broadcasteamiento de mensajes
        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            // TODO: no tengo ni flowers de si esto funciona o no
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
