using System;
using System.Collections.Generic;
using ChronicleDimensionProject.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class UIManager : Singleton<UIManager>
    {
        private readonly Dictionary<Type, Node> _nodes = new Dictionary<Type, Node>();

        public void RegisterNode(Node node)
        {
            _nodes[node.GetType()] = node;
            Debug.Log($"Node registered: {node.GetType()}");
        }

        public async UniTask OpenNode<T>() where T : Node
        {
            if (_nodes.TryGetValue(typeof(T), out var node))
            {
                await node.OnOpenIn();
            }
            else
            {
                Debug.LogError($"{typeof(T)} not registered in UIManager");
            }
        }

        public async UniTask SwitchNode<T>() where T : Node
        {
            if (_nodes.TryGetValue(typeof(T), out var node))
            {
                await node.OnOpenOut();
            }
            else
            {
                Debug.LogError($"{typeof(T)} not registered in UIManager");
            }
        }

        public async UniTask CloseNode<T>() where T : Node
        {
            if (_nodes.TryGetValue(typeof(T), out var node))
            {
                await node.OnCloseOut();
            }
            else
            {
                Debug.LogError($"{typeof(T)} not registered in UIManager");
            }
        }
    }
}