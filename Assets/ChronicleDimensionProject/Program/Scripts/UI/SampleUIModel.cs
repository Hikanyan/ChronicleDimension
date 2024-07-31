namespace ChronicleDimensionProject.UI
{
    public class SampleUIModel
    {
        public string Title { get; set; }
        public int Count { get; set; }

        public SampleUIModel(string title, int count)
        {
            Title = title;
            Count = count;
        }
    }
}