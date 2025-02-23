using Application.Models.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace petrotest
{
    public class TorneoHub : Hub
    {
        public async Task SendMessage(string room, string user, string message)
        {
            await Clients.Group(room).SendAsync("ReceiveMessage", user, message);
            await ClearJudgeMessages(room);
        }

        public async Task SendMessageToCompetitors(string room, string message)
        {
            await Clients.Group(room).SendAsync("ReceiveMessageToCompetitors", message);
        }

        public async Task SendJudgeMessage(string room, string user, string message)
        {
            await Clients.Group(room).SendAsync("ReceiveJudgeMessage", user, message);
        }

        public async Task SendCompetitorMessage(string room, string user, string message)
        {
            await Clients.Group(room).SendAsync("ReceiveCompetitorMessage", user, message);
        }
        public async Task ShowButtonToCompetitors(string room)
        {
            await Clients.Group(room).SendAsync("ShowButtonToCompetitors");
        }

        public async Task CompetitorPressedButton(string room, string userName)
        {
            await Clients.Group(room).SendAsync("CompetitorPressedButton", userName);
            await Clients.Group(room).SendAsync("NotifyJudgeOnCompetitorPress", userName);
            
        }

        public async Task NotifyJudgeOnCompetitorPress(string room, string userName)
        {
            await Clients.Group(room).SendAsync("NotifyJudgeOnCompetitorPress", userName);
        }
        public async Task NotifyCompetitorOnJudgeMessage(string room, string message)
        {
            await Clients.Group(room).SendAsync("NotifyCompetitorOnJudgeMessage", message);
        }

        public async Task ClearJudgeMessages(string room)
        {
            await Clients.Group(room).SendAsync("ClearJudgeMessages");
        }
        public async Task AddToGroup(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);

            var isJudge = Context.User.IsInRole(Role.Juez);
            var isCompetitor = Context.User.IsInRole(Role.Competidor);

            if (isJudge)
            {
                await Clients.Group(room).SendAsync("ShowWho", $"Se conectó {Context.ConnectionId} como Juez");
            }
            else if (isCompetitor)
            {
                await Clients.Group(room).SendAsync("ShowWho", $"Se conectó {Context.ConnectionId} como Competidor");
            }
            else
            {
                await Clients.Group(room).SendAsync("ShowWho", $"Se conectó {Context.ConnectionId}");
            }
        }

    }
}
