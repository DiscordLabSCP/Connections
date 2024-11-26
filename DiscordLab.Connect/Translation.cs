using Exiled.API.Interfaces;

namespace DiscordLab.Connect
{
    public class Translation : ITranslation
    {
        public string CommandName { get; set; } = "connect";
        public string CommandDescription { get; set; } = "Connect your Discord account to your Steam account.";
        public string DisconnectCommandName { get; set; } = "disconnect";
        public string DisconnectCommandDescription { get; set; } = "Disconnect your Discord account from your Steam account.";
        public string NoAccount { get; set; } = "You don't have an account connected!";
        public string Disconnected { get; set; } = "You have successfully disconnected your account!";
        public string ServerCommandName { get; set; } = "connect";
        public string ServerCommandDescription { get; set; } = "Connect your Discord account to your Steam account. Run the connect command in the Discord to get your code";
        public string CommandResponse { get; set; } =
            "Here is your code, please connect to the server and run `.{commandname} {code}` in the client console. This code will expire in 5 minutes.";
        public string NoCodeProvided { get; set; } = "You need to provide a code!";
        public string InvalidCode { get; set; } = "Invalid code!";
        public string Connected { get; set; } = "You have successfully connected your account!";
    }
}