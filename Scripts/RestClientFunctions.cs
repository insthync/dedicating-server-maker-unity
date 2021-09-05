using System.Collections.Generic;
using DedicatingServerMatchMaker.Data;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Net;
using System.Threading.Tasks;

namespace DedicatingServerMatchMaker
{
    public static class RestClientFunctions
    {
        public struct Result
        {
            public bool Success { get; set; }
        }

        public struct Result<T>
        {
            public bool Success { get; set; }
            public T Data { get; set; }
        }

        public static async Task<Result<LobbyRoomInfo>> GetRoomInfo(string url, string roomId)
        {
            UnityWebRequest req = await SendRequestAsync(url + "/" + roomId);
            bool success = req.result == UnityWebRequest.Result.Success;
            LobbyRoomInfo data = null;
            if (success)
                data = JsonConvert.DeserializeObject<LobbyRoomInfo>(req.downloadHandler.text);
            return new Result<LobbyRoomInfo>()
            {
                Success = success,
                Data = data,
            };
        }

        public static async Task<Result> GameServerReady(string url, string roomId, Dictionary<string, object> options)
        {
            UnityWebRequest req = await SendRequestAsync(url + "/game-server/ready/" + roomId, JsonConvert.SerializeObject(options));
            return new Result()
            {
                Success = req.result == UnityWebRequest.Result.Success,
            };
        }

        public static async Task<UnityWebRequest> SendRequestAsync(string url)
        {
            UnityWebRequest req = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET)
            {
                downloadHandler = new DownloadHandlerBuffer()
            };
            req.SetRequestHeader("Content-Type", "application/json");
            await new AsyncOperationWrapper(req.SendWebRequest());
            return req;
        }

        public static async Task<UnityWebRequest> SendRequestAsync(string url, string json)
        {
            UnityWebRequest req = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)
            {
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json)),
                downloadHandler = new DownloadHandlerBuffer()
            };
            req.SetRequestHeader("Content-Type", "application/json");
            await new AsyncOperationWrapper(req.SendWebRequest());
            return req;
        }

        private class AsyncOperationWrapper
        {
            public UnityWebRequestAsyncOperation AsyncOp { get; }
            public AsyncOperationWrapper(UnityWebRequestAsyncOperation unityOp)
            {
                AsyncOp = unityOp;
            }

            public AsyncOperationAwaiter GetAwaiter()
            {
                return new AsyncOperationAwaiter(this);
            }
        }

        private class AsyncOperationAwaiter : INotifyCompletion
        {
            private UnityWebRequestAsyncOperation asyncOp;
            private System.Action continuation;
            public bool IsCompleted { get { return asyncOp.isDone; } }

            public AsyncOperationAwaiter(AsyncOperationWrapper wrapper)
            {
                asyncOp = wrapper.AsyncOp;
                asyncOp.completed += OnRequestCompleted;
            }

            public void GetResult()
            {
                asyncOp.completed -= OnRequestCompleted;
            }

            public void OnCompleted(System.Action continuation)
            {
                this.continuation = continuation;
            }

            private void OnRequestCompleted(AsyncOperation _)
            {
                continuation?.Invoke();
                continuation = null;
            }
        }
    }
}
