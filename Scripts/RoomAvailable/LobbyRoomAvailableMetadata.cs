using DedicatingServerMatchMaker.Data;

namespace DedicatingServerMatchMaker.RoomAvailable
{
    [System.Serializable]
    public class LobbyRoomAvailableMetadata
    {
        public string title = string.Empty;
        public LobbyRoomAnnotations annotations = new LobbyRoomAnnotations();
        public bool hasPassword = false;
    }
}
