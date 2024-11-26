using Discord;
using Discord.WebSocket;
using DiscordLab.Bot.API.Interfaces;
using DiscordLab.Bot.API.Modules;
using DiscordLab.Connect.API.Features;
using Newtonsoft.Json.Linq;

namespace DiscordLab.Connect.DiscordCommands
{
    public class Disconnect : ISlashCommand
    {
        public SlashCommandBuilder Data { get; } = new()
        {
            Name = Plugin.Instance.Translation.DisconnectCommandName,
            Description = Plugin.Instance.Translation.DisconnectCommandDescription
        };

        public async Task Run(SocketSlashCommand command)
        {
            List<DatabaseConnectedUser> connected = (WriteableConfig.GetConfig()["ConnectedUsers"] ?? new JArray())
                .ToObject<IEnumerable<DatabaseConnectedUser>>().ToList();
            
            DatabaseConnectedUser account = connected.FirstOrDefault(x => x.DiscordId == command.User.Id);

            if (account == null)
            {
                await command.RespondAsync(Plugin.Instance.Translation.NoAccount, ephemeral: true);
                return;
            }
            
            connected.Remove(account);
            
            WriteableConfig.WriteConfigOption("ConnectedUsers", JArray.FromObject(connected));

            await command.RespondAsync(Plugin.Instance.Translation.Disconnected, ephemeral: true);
        }
    }
}