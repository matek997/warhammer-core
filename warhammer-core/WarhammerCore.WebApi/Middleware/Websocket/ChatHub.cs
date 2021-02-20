using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public Task BroadcastMessage( string message) =>
          Clients.All.SendAsync("broadcastMessage", message);

        public Task Echo( string message) =>
            Clients.Client(Context.ConnectionId)
                   .SendAsync("echo", $"{message} (echo from server)");

        /*  public override  Task OnConnectedAsync()
          {
              Groups.AddToGroupAsync(Context.ConnectionId, "messageReceived");

              Clients.All.SendAsync("messageReceived", DateTime.Now.ToString());


               return base.OnConnectedAsync();
          }
          public async Task NewMessage(long username, string message)
          {
              await Clients.All.SendAsync("messageReceived", username, message);
          }*/
        /*      public async Task NewMessage()
              {
                  var context = GlobalHost.ConnectionManager.GetHubContext<Status>();
                  var subscribers = context.Clients;
                  var subscriber = subscribers.Group("foo");
                  subscriber.messageReceived("ello");
              }*/
    }
}