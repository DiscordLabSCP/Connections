using CommandSystem;
using DiscordLab.Bot.API.Modules;
using DiscordLab.Connect.API.Features;
using Exiled.API.Features;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordLab.Connect.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Connect : ICommand
    {
        public string Command { get; } = Plugin.Instance.Translation.ServerCommandName;

        public string[] Aliases { get; } = new [] { "con" };

        public string Description { get; } = Plugin.Instance.Translation.ServerCommandDescription;

        public string[] Usage { get; } = new[]
        {
            "%code%"
        };

        public bool Execute(
            ArraySegment<string> arguments,
            ICommandSender sender,
            out string response
        )
        {
            Player.TryGet(sender, out Player player);

            if (player == null)
            {
                response = "Only players can use this command!";
                return false;
            }

            if (arguments.At(0) == null)
            {
                response = Plugin.Instance.Translation.NoCodeProvided;
                return false;
            }
            
            JObject config = WriteableConfig.GetConfig();

            JToken codes = config["TempConnectCodes"];
            
            if (codes == null)
            {
                response = Plugin.Instance.Translation.InvalidCode;
                return false;
            }

            JToken jId = codes[arguments.At(0)];
            
            if (jId == null)
            {
                response = Plugin.Instance.Translation.InvalidCode;
                return false;
            }
            
            ulong code = jId.ToObject<ulong>();

            List<DatabaseConnectedUser> connected = API.Modules.ConnectedAccount.GetConnectedAccounts().ToList();
            
            if (connected.Any(x => x.GameId == player.UserId))
            {
                connected.Remove(connected.First(x => x.GameId == player.UserId));
            }
            
            connected.Add(new ()
            {
                GameId = player.UserId,
                DiscordId = code
            });

            JArray newContent = new();

            foreach (DatabaseConnectedUser account in connected)
            {
                newContent.Add(JObject.FromObject(account));
            }
            
            WriteableConfig.WriteConfigOption("ConnectedUsers", newContent);
            
            jId?.Parent?.Remove();
            
            WriteableConfig.WriteConfigOption("TempConnectCodes", codes);
            
            response = Plugin.Instance.Translation.Connected;
            return true;
        }
    }
}