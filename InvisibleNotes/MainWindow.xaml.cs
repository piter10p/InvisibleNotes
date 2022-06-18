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

namespace InvisibleNotes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FileName = "note.txt";
        private const int LinesCount = 25;

        private int _currentLine = 0;
        private List<string> _lines = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists(FileName))
                NotesLabel.Content = "File note.txt not exists. Create file and fill with your notes.";

            using var noteFile = File.OpenRead(FileName);
            using var reader = new StreamReader(noteFile);
            var content = reader.ReadToEnd();
            _lines = content.Split('\n').ToList();
            ShowPage();

            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void AnyKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (Keyboard.IsKeyDown(Key.Q) && Keyboard.IsKeyDown(Key.Q))
                {
                    Environment.Exit(0);
                }

                if (Keyboard.IsKeyDown(Key.Down) && Keyboard.IsKeyDown(Key.Down))
                {
                    _currentLine++;
                    ShowPage();
                }

                if (Keyboard.IsKeyDown(Key.Up) && Keyboard.IsKeyDown(Key.Up))
                {
                    _currentLine--;
                    ShowPage();
                }
            }
        }

        private void MouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            MessageBox.Show("test");
        }

        private void ShowPage()
        {
            var content = _lines.Skip(_currentLine).Take(LinesCount).ToArray();
            var page = string.Join('\n', content);
            NotesLabel.Content = page;
        }
    }
}
