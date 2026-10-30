namespace Shared1
{
    public class MessageHelper
    {
        public static string FormatMessage(string command, string key, string value = "")
        {
            if (value != "")
            {
                return $"{command} {key} {value}";
            }
            else
            {
                return $"{command} {key}";
            }
        }
    }
}