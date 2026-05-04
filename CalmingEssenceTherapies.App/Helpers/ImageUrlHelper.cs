namespace CalmingEssenceTherapies.App.Helpers
{
    public static class ImageUrlHelper
    {
        public static string GetImageUrl(this string url)
        {
            return Constants.RestUrl + url;
        }
    }
}
