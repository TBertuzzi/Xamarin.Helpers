using System;
using System.IO;
using System.Net;

namespace Xamarin.Helpers
{
    public class ImageHelper
    {
        public static Stream GetImageStreamFromUrl(string url)
        {
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                
                if (imageBytes == null || imageBytes.Length <= 0) return null;
                
                Stream stream = new MemoryStream(imageBytes);
                return stream;
            }
        }

        public static byte[] GetImageByteFromUrl(string url)
        {
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    return imageBytes;
                }
            }
            return null;
        }

        public static string ConvertToBase64(Stream stream)
        {
            var ms = new MemoryStream();
            stream.CopyTo(ms);

            var bytes = ms.ToArray();

            return Convert.ToBase64String(bytes);
        }

        public static byte[] ConvertToByteArray(Stream stream)
        {
            var ms = new MemoryStream();
            stream.CopyTo(ms);

            return ms.ToArray();
        }
    }
}