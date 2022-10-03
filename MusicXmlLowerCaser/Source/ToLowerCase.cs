using System.Text;
using System.Text.RegularExpressions;

namespace MusicXmlLowerCaser
{
    // <text>SNOW,</text>

    internal class ToLowerCase
    {
        public static string Convert(string xmlText, OptionsModel options)
        {
            StringBuilder result = new StringBuilder();

            if (xmlText != null)
            {
                // Read all lines
                string[] lines = xmlText.Split(new char[] { '\n', '\r' });

                // TODO: Fix each line <text> line
                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        result.AppendLine(Update(line, options));
                    }
                }

            }

            return result.ToString();
        }

        private static string Update(string line, OptionsModel options)
        {
            StringBuilder modifiedResult = new StringBuilder();

            if (!string.IsNullOrEmpty(line))
            {
                string pattern = @"(.*)\<text\>(.*)\<\/text\>(.*)";

                Regex r = new Regex(pattern, RegexOptions.CultureInvariant);

                Match m = r.Match(line);

                if (m.Success && m.Groups.Count == 4)
                {
                    modifiedResult.Append(m.Groups[1].Value); // Part before "<text>"
                    modifiedResult.Append("<text>");
                    modifiedResult.Append(CorrectText(m.Groups[2].Value, options));
                    modifiedResult.Append("</text>");
                    modifiedResult.Append(m.Groups[3].Value); // Part after "</text>"
                }
                else
                {
                    // Nothing to do here, put original back
                    modifiedResult.Append(line);
                }
            }

            return modifiedResult.ToString();
        }

        private static string CorrectText(string uncorrected, OptionsModel options)
        {
            StringBuilder modifiedResult = new StringBuilder();

            if (!string.IsNullOrEmpty(uncorrected))
            {
                string text = uncorrected;

                if (options.InitialUppercase)
                {
                    text = MakeInitialUpperCase(text);
                }

                if (options.AssertTrailingSpace)
                {
                    text = AssertTrailingSpace(text);
                }

                modifiedResult.Append(text);
            }
            else
            {
                modifiedResult.Append(uncorrected);
            }

            return modifiedResult.ToString();
        }

        private static string MakeInitialUpperCase(string text)
        {
            if (!string.IsNullOrEmpty(text) && text.Length > 1)
            {
                return text[0].ToString().ToUpper() + text[1..].ToLower();
            }

            return text;
        }

        private static string AssertTrailingSpace(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return text.TrimEnd() + ' ';
            }

            return text;
        }
    }
}