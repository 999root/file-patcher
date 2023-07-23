using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilePatcherGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeText(object sender, RoutedEventArgs e)
        {
            displayTextBlock.Text = "New Phrase!";
        }

        private void Patch(object sender, RoutedEventArgs e)
        {
            string targetProgramPath = TargetProgramPath.Text;
            string targetDLLPath = TargetDLLPath.Text;

            string the_patch = Patcher.ApplyPatch(targetDLLPath, targetProgramPath);
            displayTextBlock.Text = the_patch;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            TargetProgramPath.Text = "";
            TargetDLLPath.Text = "";
            displayTextBlock.Text = "";
        }
    }

    public static class Patcher
    {
        public static string ApplyPatch(string TargetFilePath, string PatchFilePath)
        {
            try
            {
                string Result = string.Empty;

                // Read the content of the patch file into a byte array
                byte[] PatchData = File.ReadAllBytes(PatchFilePath);

                // Open the target file with read and write capabilities
                using (FileStream PatchedFile = new FileStream(TargetFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    // Determine the size of the target file
                    long fileSize = PatchedFile.Length;

                    // Check if the target file is large enough to accommodate the patch
                    if (fileSize >= PatchData.Length)
                    {
                        // Apply the patch by overwriting the target file content with the patch data
                        PatchedFile.Seek(0, SeekOrigin.Begin);
                        PatchedFile.Write(PatchData, 0, PatchData.Length);
                        Result = "Success!";
                    }
                    else
                    {
                        Result = "Error applying patch!";
                    }
                    return Result;
                }
            }
            catch (Exception e)
            {
                string error = $"{e}";
                return error;
            }
        }
    }
}
