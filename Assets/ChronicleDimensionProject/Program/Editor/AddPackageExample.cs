using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Hikanyan.Core
{
    public class AddPackageExample : MonoBehaviour
    {
        private static List<AddRequest> Requests = new List<AddRequest>(); // Requestsリストの追加

        static void AddPackage(string menuName, string packageId)
        {
            var request = Client.Add(packageId);
            Requests.Add(request);
            EditorApplication.update += Progress; // 更新を登録
        }

        // メニューアイテムを個々に設定
        [MenuItem("HikanyanTools/Add UniRx")]
        static void AddUniRx() => AddPackage("Add UniRx",
            "https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts");

        [MenuItem("HikanyanTools/Add UniTask")]
        static void AddUniTask() => AddPackage("Add UniTask",
            "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask");

        [MenuItem("HikanyanTools/Add VContainer")]
        static void AddVContainer() => AddPackage("Add VContainer",
            "https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer");


        static void Progress()
        {
            for (int i = 0; i < Requests.Count; i++)
            {
                AddRequest request = Requests[i];

                if (!request.IsCompleted)
                    continue;

                switch (request.Status)
                {
                    case StatusCode.Success:
                        Debug.Log("Installed: " + request.Result.packageId);
                        break;
                    case >= StatusCode.Failure:
                        Debug.Log(request.Error.message);
                        break;
                }

                Requests.RemoveAt(i);
                i--;
            }

            if (Requests.Count == 0)
            {
                EditorApplication.update -= Progress;
            }
        }
    }
}