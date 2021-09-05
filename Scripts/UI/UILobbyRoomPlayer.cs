using DedicatingServerMatchMaker.Schema;
using DedicatingServerMatchMaker.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace DedicatingServerMatchMaker.UI
{
    public class UILobbyRoomPlayer : MonoBehaviour
    {
        public Text textPlayerName;
        public GameObject[] readyObjects;
        public GameObject[] notReadyObjects;

        private LobbyPlayer _player;
        public LobbyPlayer Player
        {
            get { return _player; }
            set
            {
                _player = value;
                PlayerName = _player.name;
                IsReady = value.state >= (byte)EPlayerState.Ready;
            }
        }

        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                if (textPlayerName)
                    textPlayerName.text = value;
            }
        }

        private bool _isReady;
        public bool IsReady
        {
            get { return _isReady; }
            set
            {
                _isReady = value;
                if (readyObjects != null && readyObjects.Length > 0)
                {
                    for (int i = 0; i < readyObjects.Length; ++i)
                    {
                        readyObjects[i].SetActive(value);
                    }
                }
                if (notReadyObjects != null && notReadyObjects.Length > 0)
                {
                    for (int i = 0; i < notReadyObjects.Length; ++i)
                    {
                        notReadyObjects[i].SetActive(!value);
                    }
                }
            }
        }
    }
}
