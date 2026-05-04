namespace CalmingEssenceTherapies.App
{
    public static class Constants
    {
        public static string RestUrl = DeviceInfo.Platform == DevicePlatform.Android
        ? "http://10.0.2.2:5273"
        : "https://localhost:7165";

        public static readonly List<int> DurationHours = Enumerable.Range(0, 13).ToList();
        public static readonly List<int> DurationMinutes = Enumerable.Range(0, 12).Select(x => x * 5).ToList();
    }
}
