using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DedicatingServerMatchMaker.UI
{
    public class UILobbyRoomCreate : MonoBehaviour
    {
        public InputField inputTitle;
        public InputField inputPassword;

        private void Awake()
        {
            if (inputTitle)
            {
                inputTitle.inputType = InputField.InputType.Standard;
                inputTitle.contentType = InputField.ContentType.Standard;
            }
            if (inputPassword)
            {
                inputPassword.inputType = InputField.InputType.Password;
                inputPassword.contentType = InputField.ContentType.Pin;
                inputPassword.characterLimit = Consts.MAX_PASSWORD_LENGTH;
            }
        }

        public void OnClickCreate()
        {
            Dictionary<string, object> options = new Dictionary<string, object>();
            if (inputTitle && !string.IsNullOrEmpty(inputTitle.text))
                options[Consts.OPTION_TITLE] = inputTitle.text;
            if (inputPassword && !string.IsNullOrEmpty(inputPassword.text))
                options[Consts.OPTION_PASSWORD] = inputPassword.text;
            LobbyClientManager.Instance.CreateRoom(options);
        }
    }
}
