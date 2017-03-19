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
    }
}