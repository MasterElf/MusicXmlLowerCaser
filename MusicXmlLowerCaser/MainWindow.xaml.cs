using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace MusicXmlLowerCaser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OptionsModel optionsModel = new OptionsModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = optionsModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (optionsModel != null && 
                File.Exists(optionsModel.InputFile) && !string.IsNullOrEmpty(optionsModel.InputFile)
                && !string.IsNullOrEmpty(optionsModel.OutputFileName))
            {
                // Load file
                string originalContent = File.ReadAllText(optionsModel.InputFile);

                // Modify content
                string modifiedContent = Modifier.Convert(originalContent, optionsModel);

                // Save a copy
                string? outputDirectoryName = Path.GetDirectoryName(optionsModel.InputFile);

                if (!string.IsNullOrEmpty(outputDirectoryName))
                {
                    string outputPath = Path.Combine(outputDirectoryName, optionsModel.OutputFileName);
                    File.WriteAllText(outputPath, modifiedContent);
                }
            }
        }

        private void InputFilePathButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is OptionsModel optionsModel)
            {
                // Open load dialogue
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Xml|*.xml";
                openFileDialog.DefaultExt = "xml";
                openFileDialog.CheckPathExists = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.InitialDirectory = Path.GetDirectoryName(optionsModel.InputFile);

                if (openFileDialog.ShowDialog() == true)
                {
                    // Load model from file
                    optionsModel.InputFile = openFileDialog.FileName;
                }
            }
        }
    }
}