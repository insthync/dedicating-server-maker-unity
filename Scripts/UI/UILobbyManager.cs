using UnityEngine;

namespace DedicatingServerMatchMaker.UI
{
    public class UILobbyManager : MonoBehaviour
    {
        public UILobbyRoomList uiRoomList;
        public UILobbyRoom uiRoom;

        private void Start()
        {
            uiRoom.gameObject.SetActive(false);
            uiRoomList.gameObject.SetActive(true);
            LobbyClientManager.Instance.onJoinLobby.AddListener(OnJoin);
            LobbyClientManager.Instance.onRoomLeave.AddListener(OnLeave);
        }

        private void OnDestroy()
        {
            LobbyClientManager.Instance.onJoinLobby.RemoveListener(OnJoin);
            LobbyClientManager.Instance.onRoomLeave.RemoveListener(OnLeave);
        }

        private void OnJoin()
        {
            uiRoomList.gameObject.SetActive(false);
            uiRoom.gameObject.SetActive(true);
        }

        private void OnLeave(int code)
        {
            uiRoom.gameObject.SetActive(false);
            uiRoomList.gameObject.SetActive(true);
        }
    }
}
