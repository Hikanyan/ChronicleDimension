namespace ChronicleDimensionProject.Common.UI
{
    public interface ILoadSceneView
    {
        void ShowLoading(bool show);
        void UpdateProgress(float progress);
    }
}