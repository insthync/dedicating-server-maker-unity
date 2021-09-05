using DedicatingServerMatchMaker.RoomAvailable;
using System.Threading.Tasks;
using UnityEngine;

namespace DedicatingServerMatchMaker.UI
{
    public class UILobbyRoomList : MonoBehaviour
    {
        public UILobbyRoomListEntry entryPrefab;
        public Transform entryContainer;
        public UILobbyRoomPassword uiRoomPassword;

        private void Start()
        {
            LoadAvailableLobby(1000);
        }

        public async void LoadAvailableLobby(int milliseondsDelay)
        {
            await Task.Delay(milliseondsDelay);
            
            for (int i = entryContainer.childCount - 1; i >= 0; --i)
            {
                Destroy(entryContainer.GetChild(i).gameObject);
            }
            LobbyRoomAvailable[] rooms = await LobbyClientManager.Client.GetAvailableRooms<LobbyRoomAvailable>(Consts.ROOM_NAME);
            for (int i = 0; i < rooms.Length; ++i)
            {
                UILobbyRoomListEntry newRoomUI = Instantiate(entryPrefab, entryContainer);
                newRoomUI.uiRoomList = this;
                newRoomUI.RoomId = rooms[i].roomId;
                newRoomUI.RoomTitle = rooms[i].metadata.title;
                newRoomUI.HasPassword = rooms[i].metadata.hasPassword;
                newRoomUI.gameObject.SetActive(true);
            }
        }

        public void ShowUIRoomPassword(string roomId, string roomTitle)
        {
            uiRoomPassword.RoomId = roomId;
            uiRoomPassword.RoomTitle = roomTitle;
            uiRoomPassword.gameObject.SetActive(true);
        }
    }
}
