using DiscordLab.Bot.API.Modules;
using DiscordLab.Connect.API.Features;
using Newtonsoft.Json.Linq;

namespace DiscordLab.Connect.API.Extensions
{
    public static class ConnectedAccount
    {
        public static Features.ConnectedAccount ToConnectedAccount(this ulong id)
        {
            return Modules.ConnectedAccount.GetConnectedAccounts().FirstOrDefault(x => x.DiscordId == id).ToConnectedAccount();
        }

        public static Features.ConnectedAccount ToConnectedAccount(this string id)
        {
            return Modules.ConnectedAccount.GetConnectedAccounts().FirstOrDefault(x => x.GameId == id).ToConnectedAccount();
        }

        public static Features.ConnectedAccount ToConnectedAccount(this DatabaseConnectedUser user)
        {
            return new (user);
        }
    }
}