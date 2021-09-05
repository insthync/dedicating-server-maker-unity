namespace DedicatingServerMatchMaker.Schema
{
    public class LobbyRoomState : Colyseus.Schema.Schema
	{
		[Colyseus.Schema.Type(0, "map", typeof(Colyseus.Schema.MapSchema<LobbyPlayer>))]
		public Colyseus.Schema.MapSchema<LobbyPlayer> players = new Colyseus.Schema.MapSchema<LobbyPlayer>();

		[Colyseus.Schema.Type(1, "string")]
		public string managerSessionId = string.Empty;
	}
}
