namespace DedicatingServerMatchMaker.Data
{
    [System.Serializable]
    public partial class LobbyRoomInfo
    {
        public string roomId = string.Empty;
        public string roomName = string.Empty;
        public int maxClients = 0;
        public int maxTeams = 0;
        public LobbyRoomAnnotations annotations = new LobbyRoomAnnotations();
    }
}
