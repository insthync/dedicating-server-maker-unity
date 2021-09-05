using DedicatingServerMatchMaker.Schema;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace DedicatingServerMatchMaker.UI
{
    public class UILobbyRoom : MonoBehaviour
    {
        public UILobbyRoomPlayer entryPrefab;
        public Transform entryContainer;
        public Text textCountDown;

        private Coroutine countDownCoroutine = null;
        private readonly Dictionary<string, UILobbyRoomPlayer> uiRoomPlayers = new Dictionary<string, UILobbyRoomPlayer>();

        private void OnEnable()
        {
            if (textCountDown)
                textCountDown.text = string.Empty;
            LobbyClientManager.Instance.onRoomError.AddListener(OnError);
            LobbyClientManager.Instance.onRoomLeave.AddListener(OnLeave);
            UpdateRoomState(LobbyClientManager.CurrentRoom.State);
        }

        private void OnDisable()
        {
            LobbyClientManager.Instance.onRoomError.RemoveListener(OnError);
            LobbyClientManager.Instance.onRoomLeave.RemoveListener(OnLeave);
        }

        private void OnError(int code, string message)
        {

        }

        private void OnLeave(int code)
        {

        }

        private void UpdateRoomState(LobbyRoomState state)
        {
            for (int i = entryContainer.childCount - 1; i >= 0; --i)
            {
                Destroy(entryContainer.GetChild(i).gameObject);
            }
            uiRoomPlayers.Clear();
            foreach (var playerKey in state.players.Keys)
            {
                UILobbyRoomPlayer newRoomUI = Instantiate(entryPrefab, entryContainer);
                newRoomUI.Player = state.players[(string)playerKey];
                newRoomUI.gameObject.SetActive(true);
                uiRoomPlayers[(string)playerKey] = newRoomUI;
            }
        }

        public void OnClickStartGame()
        {
            LobbyClientManager.Instance.StartGame();
        }
    }
}
