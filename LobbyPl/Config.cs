using System;
using System.ComponentModel;
using System.IO;
using Exiled.API.Interfaces;

namespace LobbyPl
{
    /// <summary>
    /// The config class for the LobbyPl plugin.
    /// </summary>
    public class Config : IConfig
    {
        /// <summary>
        /// Main Options
        /// </summary>
        [Description("==== [LobbyPl Main Option] ====")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        /// <summary>
        /// DON'T TOUCH THIS UNLESS YOU KNOW WHAT YOU ARE DOING
        /// </summary>
        [Description("==== [Path's] ====")]
        public string PathDir { get; set; }  =  Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "EXILED",
            "Configs",
            "LobbyPl-Schematics"
        );
        public string SchematicName { get; set; } = "lobby";
        /// <summary>
        /// Lobby Options
        /// </summary>
        [Description("==== [LobbyPl | Options] ====")]
        public int MaxPlayersInLobby { get; set; } = 5;
    }
}