using DedicatingServerMatchMaker.Data;
using System;
using UnityEngine.Events;

namespace DedicatingServerMatchMaker.Events
{
    [Serializable]
    public class GameServerOptionsEvent : UnityEvent<GameServerOptions>
    {
    }
}
