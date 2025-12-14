using System.IO;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using UnityEngine;


namespace LobbyPl.Lobby
{
    /// <summary>
    /// The event class for the lobby system.
    /// </summary>
    public class LobbyEvent
    {
        /// <summary>
        /// Introducing the all the functions of the lobby system.
        /// </summary>
        private SchematicObject _schematic;
        private void OnPlayerVer(VerifiedEventArgs ev)
        {
            //Spawns the schematic if it is not already spawned
            if (_schematic == null)
            {
                var path = Path.Combine(Main.Instance.Config.PathDir, Main.Instance.Config.SchematicName);
                _schematic = ObjectSpawner.SpawnSchematic(
                    path,
                    Vector3.zero,
                    Quaternion.identity
                );
            }
            //Loads the lobby lock system
            if (Player.List.Count < Main.Instance.Config.MaxPlayersInLobby)
            {
                ev.Player.Teleport(_schematic.Position);
                Round.IsLobbyLocked = true;
            }
            else
            {
                Map.Broadcast(10, "Round is beggining!");
                Round.IsLobbyLocked = false;
            }
        }
        
        //Cleans up the schematic on round start
        private void OnRoundStarted()
        {
            _schematic?.Destroy();
            _schematic = null;
        }
        
        /// <summary>
        /// Register and de-register events.
        /// </summary>
        public void Register()
        {
            Exiled.Events.Handlers.Player.Verified += OnPlayerVer;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
        }
        public void Unregister()
        {
            Exiled.Events.Handlers.Player.Verified -= OnPlayerVer;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
        }
    }
    
}