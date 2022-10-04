using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MusicXmlLowerCaser
{
    // <text>SNOW,</text>

    internal class Modifier
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
                if (!ModifyTextLine(line, options, modifiedResult) &&
                    !ModifyMidiChannelLine(line, options, modifiedResult))
                {
                    // Nothing to do here, put original back
                    modifiedResult.Append(line);
                }
            }

            return modifiedResult.ToString();
        }

        private static bool ModifyTextLine(string line, OptionsModel options, StringBuilder modifiedResult)
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
                return true;
            }

            return false;
        }

        private static bool ModifyMidiChannelLine(string line, OptionsModel options, StringBuilder modifiedResult)
        {
            string pattern = @"(.*)\<midi\-channel\>(.*)\<\/midi\-channel\>(.*)";

            Regex r = new Regex(pattern, RegexOptions.CultureInvariant);

            Match m = r.Match(line);

            if (m.Success && m.Groups.Count == 4)
            {
                modifiedResult.Append(m.Groups[1].Value); // Part before "<midi-channel>"
                modifiedResult.Append("<midi-channel>");
                modifiedResult.Append(CorrectMidiChannel(m.Groups[2].Value, options));
                modifiedResult.Append("</midi-channel>");
                modifiedResult.Append(m.Groups[3].Value); // Part after "</midi-channel>"
                return true;
            }

            return false;
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

        private static string CorrectMidiChannel(string uncorrected, OptionsModel options)
        {
            StringBuilder modifiedResult = new StringBuilder();

            if (!string.IsNullOrEmpty(uncorrected))
            {
                string text = uncorrected;

                if (options.SetAllChannelsToOne)
                {
                    text = SetAllChannelsToOne(text);
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
                return text.TrimEnd().TrimStart() + ' ';
            }

            return text;
        }

        private static string SetAllChannelsToOne(string text)
        {
            if (int.TryParse(text, out var _))
            {
                return "1";
            }

            return text;
        }
    }
}