using CommunityToolkit.Mvvm.ComponentModel;

namespace MusicXmlLowerCaser
{
    internal class OptionsModel : ObservableObject
    {
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
    }
}