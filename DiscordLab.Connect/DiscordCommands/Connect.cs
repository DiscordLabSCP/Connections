using Discord;
using Discord.WebSocket;
using DiscordLab.Bot.API.Interfaces;
using DiscordLab.Bot.API.Modules;
using MEC;
using Newtonsoft.Json.Linq;

namespace DiscordLab.Connect.DiscordCommands
{
    public class Connect : ISlashCommand
    {
        public SlashCommandBuilder Data { get; } = new()
        {
            Name = Plugin.Instance.Translation.CommandName,
            Description = Plugin.Instance.Translation.CommandDescription
        };

        public async Task Run(SocketSlashCommand command)
        {
            Random random = new ();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string code = new (Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

            JToken codes = WriteableConfig.GetConfig()["TempConnectCodes"] ?? new JObject();
            
            codes[code] = command.User.Id;
            
            WriteableConfig.WriteConfigOption("TempConnectCodes", codes);

            await command.RespondAsync(Plugin.Instance.Translation.CommandResponse.Replace("{commandname}", Plugin.Instance.Translation.CommandName).Replace("{code}", code), ephemeral:true);

            Timing.CallDelayed(360f, () =>
            {
                codes = WriteableConfig.GetConfig()["TempConnectCodes"] ?? new JObject();
                
                codes[code]?.Parent?.Remove();
                
                WriteableConfig.WriteConfigOption("TempConnectCodes", codes);
            });
        }
    }
}