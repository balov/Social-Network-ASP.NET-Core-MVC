using System;

namespace SocialNetwork.Web.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToRenderablePictureString(this byte[] array)
        {
            if (array == null)
            {
                return string.Empty;
            }
            return "data:image; base64," + Convert.ToBase64String(array);
        }
    }
}