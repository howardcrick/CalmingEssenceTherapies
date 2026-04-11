namespace CalmingEssenceTherapies.App
{
    public static class Constants
    {
        public static string RestUrl = DeviceInfo.Platform == DevicePlatform.Android
        ? "https://10.0.2.2:7165"
        : "https://localhost:7165";
    }
}
