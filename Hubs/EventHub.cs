using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace disclone_api.Hubs
{
    [SignalRHub]
    public class EventHub : Hub
    {
        public override Task OnConnectedAsync() {
            
            var accessToken = Context.GetHttpContext().Request.Query["access_token"];
            return base.OnConnectedAsync();
        }
    }
}