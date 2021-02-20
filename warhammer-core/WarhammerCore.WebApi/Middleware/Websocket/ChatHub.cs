using Microsoft.AspNetCore.SignalR;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WarhammerCore.WebApi.Middleware.Websocket
{
    public class ChatHub : Hub
    {
        public Task BroadcastMessage(string username, string message)
        {
            var x = Context;

            var msg = message.StartsWith("/") ? parseCommand(username, message) : $"{username}: {message}";
            return Clients.All.SendAsync("broadcastMessage", msg);
        }

        private string parseCommand(string username, string command)
        {
            var result = Regex.Split(command, "/r ([0-9]+)d([0-9]+)");
            if (result.Length == 1) return $"{username}: {command}";
            var rand = new Random();
            string msg = $"rolls {result[1]}d{result[2]} -";
            int d = Int32.Parse(result[2]) + 1;
            for (int i = 0; i < Int32.Parse(result[1]); i++) msg += $"'{rand.Next(1, d)}' ";

            return $"{username} {msg}";
        }

        public Task Echo(string message) =>
            Clients.Client(Context.ConnectionId)
                   .SendAsync("echo", $"{message} (echo from server)");
    }
}