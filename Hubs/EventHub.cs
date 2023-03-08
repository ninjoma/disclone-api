using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace disclone_api.Hubs
{
    [SignalRHub]
    public class EventHub : Hub
    {
        public override Task OnConnectedAsync() {
            
            Console.WriteLine(Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }
    }
}