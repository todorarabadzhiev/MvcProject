using System;

namespace CommonUtilities.Utilities
{
    public static class Utilities
    {
        public const string DbConnectionName = "DefaultConnection";
        public static string ConvertToImage(byte[] fileData)
        {
            if (fileData == null)
            {
                return "no image";
            }

            string base64 = Convert.ToBase64String(fileData);
            string imgSource = String.Format("data:image/jpeg;base64,{0}", base64);

            return imgSource;
        }

        public static byte[] ConvertFromImage(string strData)
        {
            if (string.IsNullOrWhiteSpace(strData))
            {
                throw new ArgumentNullException("Base64String");
            }

            string strBase64 = "base64,";
            string strItem = strData.Substring(strData.IndexOf(strBase64) + strBase64.Length);
            try
            {
                byte[] imageData = Convert.FromBase64String(strItem);
                return imageData;
            }
            catch (FormatException)
            {
                throw new FormatException("Base64String");
            }
        }
    }
}