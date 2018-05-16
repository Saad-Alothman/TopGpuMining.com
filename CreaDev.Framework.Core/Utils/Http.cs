namespace CreaDev.Framework.Core.Utils
{
    public class Http
    {
        public static string GetImageMimeType(string extension)
        {
            string mimeType ="Image/"+ extension;
            if (IsSvg(extension))
            {
                mimeType += "+xml";
            }

            return mimeType;

        }

        public static bool IsSvg(string mimeType)
        {
            return mimeType.ToLower() == "svg";
        }
    }
}
