using System.IO;
using System.Windows;

namespace MusicXmlLowerCaser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Load file
            string originalContent = File.ReadAllText(@"D:\OneDrive2\OneDrive\SmartScore\Let it snow\Let It Snow.xml");

            // Modify content
            string modifiedContent = ToLowerCase.Convert(originalContent);

            // Save a copy
            File.WriteAllText(@"D:\OneDrive2\OneDrive\SmartScore\Let it snow\Let It Snow_lowerCase.xml", modifiedContent);
        }
    }
}