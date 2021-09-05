using UnityEngine;
using Colyseus;
using DedicatingServerMatchMaker.Data;
using DedicatingServerMatchMaker.Events;
using DedicatingServerMatchMaker.Schema;
using UnityEngine.Events;
using System.Collections.Generic;

namespace DedicatingServerMatchMaker
{
    public class LobbyClientManager : MonoBehaviour
    {
        public delegate Dictionary<string, object> SetLobbyRoomOptionsDelegate(Dictionary<string, object> options);

        public static LobbyClientManager Instance { get; private set; }
        public static ColyseusClient Client { get; private set; }
        public static ColyseusRoom<LobbyRoomState> CurrentRoom { get; set; }
        public static bool IsManager { get { return CurrentRoom != null && CurrentRoom.SessionId == CurrentRoom.State.managerSessionId; } }

        public string serverAddress = "ws://localhost:2567";
        public UnityEvent onJoinLobby = new UnityEvent();
        public StringEvent onJoinLobbyFailed = new StringEvent();
        public RoomErrorEvent onRoomError = new RoomErrorEvent();
        public Int32Event onRoomLeave = new Int32Event();
        public StringEvent onPlayerLeave = new StringEvent();
        public GameServerOptionsEvent onGameServerReady = new GameServerOptionsEvent();
        public SetLobbyRoomOptionsDelegate onSetLobbyRoomOptions;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Client = new ColyseusClient(serverAddress);
        }

        public async void CreateRoom(Dictionary<string, object> options)
        {
            if (options == null)
                options = new Dictionary<string, object>();
            if (onSetLobbyRoomOptions != null)
                options = onSetLobbyRoomOptions.Invoke(options);
            try
            {
                ColyseusRoom<LobbyRoomState> room = await Client.Create<LobbyRoomState>(Consts.ROOM_NAME, options);
                OnJoinLobby(room);
            }
            catch (System.Exception ex)
            {
                OnJoinLobbyFailed(ex.Message);
            }
        }

        public async void JoinRoom(string roomId, Dictionary<string, object> options)
        {
            if (options == null)
                options = new Dictionary<string, object>();
            if (onSetLobbyRoomOptions != null)
                options = onSetLobbyRoomOptions.Invoke(options);
            try
            {
                ColyseusRoom<LobbyRoomState> room = await Client.JoinById<LobbyRoomState>(roomId, options);
                OnJoinLobby(room);
            }
            catch (System.Exception ex)
            {
                OnJoinLobbyFailed(ex.Message);
            }
        }

        private void OnJoinLobby(ColyseusRoom<LobbyRoomState> room)
        {
            CurrentRoom = room;
            CurrentRoom.OnError += CurrentRoom_OnError;
            CurrentRoom.OnLeave += CurrentRoom_OnLeave;
            CurrentRoom.OnMessage<string>("playerLeave", CurrentRoom_OnPlayerLeave);
            CurrentRoom.OnMessage<GameServerOptions>("gameServerReady", CurrentRoom_OnGameServerReady);
            onJoinLobby.Invoke();
        }

        private void OnJoinLobbyFailed(string message)
        {
            Debug.LogError($"Join Lobby Failed: {message}");
            onJoinLobbyFailed.Invoke(message);
        }

        private void CurrentRoom_OnError(int code, string message)
        {
            onRoomError.Invoke(code, message);
        }

        private void CurrentRoom_OnLeave(int code)
        {
            onRoomLeave.Invoke(code);
        }

        private void CurrentRoom_OnPlayerLeave(string sessionId)
        {
            onPlayerLeave.Invoke(sessionId);
        }

        private void CurrentRoom_OnGameServerReady(GameServerOptions options)
        {
            onGameServerReady.Invoke(options);
        }

        public async void StartGame()
        {
            await CurrentRoom.Send("startGame");
        }
    }
}
