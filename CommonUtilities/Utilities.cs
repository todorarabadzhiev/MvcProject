using System;

namespace CommonUtilities.Utilities
{
    public static class Utilities
    {
        public const string NoImage = "no image";
        public const string DbConnectionName = "DefaultConnection";

        private const string ExceptionBase64 = "Base64String";
        private const string StrBase64 = "base64,";
        private const string ImageSource = "data:image/jpeg;base64,{0}";

        public static string ConvertToImage(byte[] fileData)
        {
            if (fileData == null)
            {
                return NoImage;
            }

            string base64 = Convert.ToBase64String(fileData);
            string imgSource = String.Format(ImageSource, base64);

            return imgSource;
        }

        public static byte[] ConvertFromImage(string strData)
        {
            if (string.IsNullOrWhiteSpace(strData))
            {
                throw new ArgumentNullException(ExceptionBase64);
            }

            string strItem = strData.Substring(strData.IndexOf(StrBase64) + StrBase64.Length);
            try
            {
                byte[] imageData = Convert.FromBase64String(strItem);
                return imageData;
            }
            catch (FormatException)
            {
                throw new FormatException(ExceptionBase64);
            }
        }
    }
}