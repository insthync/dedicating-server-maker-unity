namespace DedicatingServerMatchMaker.Schema
{
    public class LobbyPlayer : Colyseus.Schema.Schema
	{
		[Colyseus.Schema.Type(0, "string")]
		public string sessionId = string.Empty;

		[Colyseus.Schema.Type(1, "string")]
		public string id = string.Empty;

		[Colyseus.Schema.Type(2, "string")]
		public string name = string.Empty;

		[Colyseus.Schema.Type(3, "uint8")]
		public byte team = 0;

		[Colyseus.Schema.Type(4, "uint8")]
		public byte state = 0;
	}
}
