using Discord.WebSocket;
using Exiled.API.Features;
using PluginAPI.Events;

namespace DiscordLab.Connect.API.Features
{
    public class ConnectedAccount(DatabaseConnectedUser user)
    {
        public string GameId { get; set; } = user.GameId;
        public ulong DiscordId { get; set; } = user.DiscordId;

        public Player Player => Player.List.FirstOrDefault(x => GameId == x.UserId);
        public SocketGuildUser User => Bot.Handlers.DiscordBot.Instance.Guild.GetUser(DiscordId);
    }
}