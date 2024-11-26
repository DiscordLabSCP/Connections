using Exiled.API.Interfaces;

namespace DiscordLab.Connect
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}