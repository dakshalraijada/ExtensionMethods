using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        const string EmailAllowedPattern = @"([a-zA-Z0-9_\-\.\+]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})";

        /// <summary>
        /// Checks if the email is valid using Regex
        /// Pattern: @"([a-zA-Z0-9_\-\.\+]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})"
        /// </summary>
        /// <param name="email"></param>
        /// <returns>True if email is valid</returns>
        public static bool IsValidEmail(this string email)
        {
            MatchCollection mc = Regex.Matches(email, EmailAllowedPattern);
            return mc != null && mc.Count == 1;
        }

        /// <summary>
        /// Genearates Friendly Url using Regex
        /// pattern: @"[^A-Za-z0-9_\.~]+", "-")
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GenerateFriendlyUrl(this string value)
        {
            return Regex.Replace(value, @"[^A-Za-z0-9_\.~]+", "-").ToLower();
        }

        /// <summary>
        /// Returns empty string if it's null, empty or white space; otherwise returns the string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEmptyWhenNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "" : value;
        }

        /// <summary>
        /// Generates a random IP Address
        /// </summary>
        /// <returns></returns>
        public static string GetRandomIpAddress()
        {
            Random _random = new Random();
            return string.Format("{0}.{1}.{2}.{3}", _random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255)); ;
        }

        /// <summary>
        /// Uses webclient
        /// </summary>
        /// <param name="url"></param>
        /// <returns>returns MemoryStream</returns>
        public static Stream GetStreamFromUrl(string url)
        {
            byte[] data = null;

            using (var wc = new System.Net.WebClient())
                data = wc.DownloadData(url);

            return new MemoryStream(data);
        }
    }
}
