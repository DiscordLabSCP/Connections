using DiscordLab.Bot.API.Modules;
using Newtonsoft.Json.Linq;

namespace DiscordLab.Connect.API.Modules
{
    public class ConnectedAccount
    {
        public static IEnumerable<Features.DatabaseConnectedUser> GetConnectedAccounts()
        {
            JToken users = WriteableConfig.GetConfig()["ConnectedUsers"];
            if (users == null)
            {
                WriteableConfig.WriteConfigOption("AdvancedLogging", new JArray());
                return new List<Features.DatabaseConnectedUser>();
            }

            return users.ToObject<IEnumerable<Features.DatabaseConnectedUser>>() ?? new List<Features.DatabaseConnectedUser>();
        }

    }
}