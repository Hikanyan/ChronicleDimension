using ChronicleDimensionProject.Common;
using UnityEngine;

namespace ChronicleDimensionProject.InGame.NovelGame
{
    public class NovelGameManager : AbstractSingletonMonoBehaviour<NovelGameManager>
    {
        protected override bool UseDontDestroyOnLoad => false;
    }
}