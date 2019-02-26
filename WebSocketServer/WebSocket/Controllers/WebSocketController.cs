using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebSocket.Controllers
{
    public class WebSocketController : ApiController
    {
        public HttpResponseMessage Get(string nom)
        {
            HttpContext.Current.AcceptWebSocketRequest(new SocketHandler(nom)); return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }

        private class SocketHandler : WebSocketHandler
        {
            private static readonly WebSocketCollection Sockets = new WebSocketCollection();

            private readonly string _nom;

            public SocketHandler(string nom)
            {
                _nom = nom;
            }

            public override void OnOpen()
            {
                Sockets.Add(this);
                Sockets.Broadcast(_nom + " s'ha connectat.");
            }

            public override void OnMessage(string missatge)
            {
                Sockets.Broadcast(_nom + ": " + missatge);
            }

            public override void OnClose()
            {
                Sockets.Broadcast(_nom + " s'ha desconnectat.");
                // Falta d'esconnectar a l'usuari
            }
        }
    }
}