using Colyseus.Schema;

namespace DedicatingServerMatchMaker.Enums
{
    public class LobbyRoomState : Schema
	{
		[Type(0, "map", typeof(MapSchema<LobbyPlayer>))]
		public MapSchema<LobbyPlayer> players = new MapSchema<LobbyPlayer>();

		[Type(1, "string")]
		public string managerSessionId = string.Empty;
	}
}
