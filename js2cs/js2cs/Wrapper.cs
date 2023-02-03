using System;
using System.Text;
using System.Threading;

namespace JS2CS
{
    public class Wrapper
    {
        private static WebSocket webSocket;
        public static Global Controller = new Global();
        public class Global
        {
            public Sockets Socket = new Sockets();
            public int StatusCode;
            public int Index;
            public bool IsNumerical;
            public bool IsPrepared;
            public bool IsAlive;
            public class WebSocketConfig
            {
                public WebSocketConfig(string address = "127.0.0.1", int port = 8080, bool secure = false)
                {
                    this.Address = address;
                    this.Port = port;
                    this.Secure = secure;

                }
                public string Address = "ws://localhost";
                public int Port = 8080;
                public bool Secure = false;
            }
            public class Message
            {
                public Message(string input)
                {
                    Input = input;
                    Output = Encoding.UTF8.GetBytes(Input);
                }
                public string Input;
                public byte[] Output;
            }
            public class Sockets
            {
                public WebSocketConfig Configuration = new WebSocketConfig();
                public Message[] Messages;
                public Message Message;
            }
            public string GetSocketString()
            {
                string secure = !Controller.Socket.Configuration.Secure ? "ws://" : "wss://";
                return secure + Controller.Socket.Configuration.Address + ":" + Controller.Socket.Configuration.Port.ToString();
            }
        }
        public static void Start()
        {
            webSocket = CreateWebsocket();
            webSocket.Connect();
            webSocket.OnMessage += (sender, e) => {
                string[] Sanitize = SanitizeText(e.Data);
                Controller.IsNumerical = false;
                if (int.TryParse(Sanitize[0], out var parsedNumber))
                {
                    Controller.IsNumerical = true;
                    Controller.StatusCode = parsedNumber;
                    Global.Message message = CreateMessage(Sanitize[1]);
                    Controller.Socket.Message = message;
                }
                if (!Controller.IsNumerical) return;
                switch (Controller.StatusCode)
                {
                    case 202: // Accepted by Server
                        Controller.StatusCode = 200;
                        Controller.IsPrepared = true;
                        Controller.Socket.Message.Input = Controller.StatusCode + "|||OK";
                        webSocket.Send(Controller.Socket.Message.Input);
                        Console.WriteLine("202: Accepted");
                        break;
                    case 200: // Recurring
                        Controller.StatusCode = 200;
                        if (Controller.IsPrepared) { Console.WriteLine("200: OK"); Controller.IsPrepared = false; }
                        webSocket.Send(Controller.StatusCode + "|||OK");
                        break;
                }           
            };
            webSocket.OnClose += (sender, e) => Console.WriteLine("499: Client Closed Connection");
            if (!webSocket.IsAlive) Console.WriteLine("503: Service Unavailable");
            else
            {
                Console.WriteLine("201: Created");
                Global.Message message = CreateMessage("201|||Created");
                webSocket.Send(message.Input);
            }
        }
        public static Global.Message CreateMessage(string Msg) { return new Global.Message(Msg); }
        private static WebSocket CreateWebsocket() { return new WebSocket(Controller.GetSocketString()); }
        static string[] SanitizeText(string input) { return input.Split(new string[] { "|||" }, System.StringSplitOptions.None); }       
    }
}