using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;

namespace MusicXmlLowerCaser
{
    internal class OptionsModel : ObservableObject
    {
        private string? inputFile;
        public string? InputFile
        {
            get => inputFile;
            set
            {
                SetProperty(ref inputFile, value);

                if (!string.IsNullOrEmpty(value))
                {
                    string fileName = Path.GetFileNameWithoutExtension(value);

                    OutputFileName = fileName + "_corrected" + Path.GetExtension(value);
                }
            }
        }

        private string? outputFileName;
        public string? OutputFileName
        {
            get => outputFileName;
            set => SetProperty(ref outputFileName, value);
        }

        private bool initialUppercase = true;

        public bool InitialUppercase
        {
            get => initialUppercase;
            set => SetProperty(ref initialUppercase, value);
        }

        private bool assertTrailingSpace = true;

        public bool AssertTrailingSpace
        {
            get => assertTrailingSpace;
            set => SetProperty(ref assertTrailingSpace, value);
        }

        private bool setAllChannelsToOne = true;

        public bool SetAllChannelsToOne
        {
            get => setAllChannelsToOne;
            set => SetProperty(ref setAllChannelsToOne, value);
        }
    }
}