using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DedicatingServerMatchMaker.UI
{
    public class UILobbyRoomPassword : MonoBehaviour
    {
        public Text textTitle;
        public InputField inputPassword;

        public string RoomId { get; set; }

        private string _roomTitle;
        public string RoomTitle
        {
            get { return _roomTitle; }
            set
            {
                _roomTitle = value;
                if (textTitle)
                    textTitle.text = value;
            }
        }

        private void Awake()
        {
            if (inputPassword)
            {
                inputPassword.inputType = InputField.InputType.Password;
                inputPassword.contentType = InputField.ContentType.Pin;
                inputPassword.characterLimit = Consts.MAX_PASSWORD_LENGTH;
            }
        }

        public void OnClickJoin()
        {
            Dictionary<string, object> options = new Dictionary<string, object>();
            if (inputPassword && !string.IsNullOrEmpty(inputPassword.text))
                options[Consts.OPTION_PASSWORD] = inputPassword.text;
            LobbyClientManager.Instance.JoinRoom(RoomId, options);
        }
    }
}