﻿using DiscordLab.Bot.API.Modules;
using DiscordLab.Connect.API.Features;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace DiscordLab.Connect
{
    public class Plugin : Plugin<Config, Translation>
    {
        public override string Name => "DiscordLab.Connect";
        public override string Author => "JayXTQ";
        public override string Prefix => "DL.Connect";
        public override Version Version => new (1, 3, 1);
        public override Version RequiredExiledVersion => new (8, 11, 0);
        public override PluginPriority Priority => PluginPriority.Default;

        public static Plugin Instance { get; private set; }
        
        private HandlerLoader _handlerLoader;

        public override void OnEnabled()
        {
            Instance = this;
            
            _handlerLoader = new ();
            _handlerLoader.Load(Assembly);
            
            base.OnEnabled();
        }
        
        public override void OnDisabled()
        {
            _handlerLoader.Unload();
            _handlerLoader = null;
            
            base.OnDisabled();
        }
    }
}