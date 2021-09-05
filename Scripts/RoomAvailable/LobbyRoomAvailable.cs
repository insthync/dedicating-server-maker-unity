using Colyseus;

namespace DedicatingServerMatchMaker.RoomAvailable
{
    [System.Serializable]
    public class LobbyRoomAvailable : ColyseusRoomAvailable
    {
        public LobbyRoomAvailableMetadata metadata;
    }
}
