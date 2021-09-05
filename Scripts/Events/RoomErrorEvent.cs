using System;
using UnityEngine.Events;

namespace DedicatingServerMatchMaker.Events
{
    /// <summary>
    /// [int code, string message]
    /// </summary>
    [Serializable]
    public class RoomErrorEvent : UnityEvent<int, string>
    {
    }
}
