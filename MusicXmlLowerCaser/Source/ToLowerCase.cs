using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MusicXmlLowerCaser
{
    // <text>SNOW,</text>

    internal class ToLowerCase
    {
        public static string Convert(string xmlText)
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
                        result.AppendLine(Update(line));
                    }
                }

            }

            return result.ToString();
        }

        private static string Update(string line)
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
                    modifiedResult.Append(CorrectCase(m.Groups[2].Value));
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

        private static string CorrectCase(string uncorrected)
        {
            StringBuilder modifiedResult = new StringBuilder();

            if (!string.IsNullOrEmpty (uncorrected) && uncorrected.Length > 1)
            {
                modifiedResult.Append(uncorrected[0]);
                modifiedResult.Append(uncorrected.Substring(1).ToLower());
            }
            else
            {
                modifiedResult.Append (uncorrected);
            }

            return modifiedResult.ToString();
        }
    }
}