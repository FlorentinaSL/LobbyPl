using System;
using System.IO;
using Exiled.API.Enums;
using Exiled.API.Features;
using LobbyPl.Lobby;

namespace LobbyPl
{
    public class Main : Plugin<Config>
    {
        /// <summary>
        /// Lobby system imported.
        /// </summary>
        private LobbyEvent _lobbyEvent;
        /// <summary>
        /// Setting up the instance for the plugin.
        /// </summary>
        public static Main Instance { get; private set; }
        
        /// <summary>
        /// MetaData about the plugin.
        /// </summary>
        public override string Name { get; } = "LobbyPl";
        public override string Author { get; } = "Florentina <3";
        public override string Prefix { get; } = "lobby";
        public override PluginPriority Priority { get; } = PluginPriority.Low;
        public override Version Version { get; } = new Version(1,2,0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 10, 2);

        /// <summary>
        /// On enabling the plugin.
        /// </summary>
        public override void OnEnabled()
        {
            //Set up directory and Instance
            Instance = this;
            CreateSchematicDirectory();

            //Register Events
            _lobbyEvent = new LobbyEvent();
            _lobbyEvent.Register();
            
            Log.Info("==== [LobbyPl | Normal | Plugin] ====");
            Log.Info($"Plugin status: {Main.Instance.Config.IsEnabled}");
            Log.Info($"Author: {Author}");
            Log.Info($"Version: {Version}");
            Log.Info("=====================================");
            base.OnEnabled();
        }

        /// <summary>
        /// On disabling the plugin.
        /// </summary>
        public override void OnDisabled()
        {
            //De-register Events
            _lobbyEvent.Unregister();
            
            Log.Info("[LobbyPl - internal] Plugin has been disabled.");
            base.OnDisabled();
        }

        /// <summary>
        /// System to create the schematic directory if it doesn't exist.
        /// </summary>
        private void CreateSchematicDirectory()
        {
            if (Main.Instance.Config != null)
            {
                string schematicsDir = string.IsNullOrEmpty(Main.Instance.Config.PathDir)
                    ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "EXILED", "Configs", "LobbyPl-Schematics")
                    : Main.Instance.Config.PathDir;
                try
                {
                    if (Directory.Exists(schematicsDir))
                    {
                        return;
                    }
                    else
                    {
                        Log.Info($"[LobbyPl - Directory]: Building The directory in: {schematicsDir}");
                        Directory.CreateDirectory(schematicsDir);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("[LobbyPl - Directory]: Error building Directory (hint: Modify the pathDir config if it is set to default): " + ex);
                }
            }
        }
    }
}