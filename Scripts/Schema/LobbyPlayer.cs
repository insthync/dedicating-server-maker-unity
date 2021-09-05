using Colyseus.Schema;

namespace DedicatingServerMatchMaker.Enums
{
    public class LobbyPlayer : Schema
	{
		[Type(0, "string")]
		public string sessionId = string.Empty;

		[Type(1, "string")]
		public string id = string.Empty;

		[Type(2, "string")]
		public string name = string.Empty;

		[Type(3, "uint8")]
		public byte team = 0;

		[Type(4, "uint8")]
		public byte state = 0;
	}
}
