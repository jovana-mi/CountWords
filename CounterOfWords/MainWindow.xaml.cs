using Microsoft.Win32;
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

namespace CounterOfWords
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        private Dictionary<string, int> dictionary;

        public MainWindow()
        {
            InitializeComponent();
            this.dictionary = new Dictionary<string, int>();
        }

        private void BrowseButtonClick(object sender, RoutedEventArgs arg)
        {
            ReadFile();
            InsertDataSortDescending();
        }

        private void ReadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                this.dictionary = new Dictionary<string, int>();

                try
                {
                    using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string line = sr.ReadLine().Trim();
                            char[] delimiterChars = { ' ', '\t', ',', ':', ';', '!', '?', '.', '"', '\'' };
                            string[] words = line.Split(delimiterChars);

                            foreach (var word in words)
                            {
                                if (word.Length > 1)
                                    if (dictionary.ContainsKey(word.ToLower()))
                                        dictionary[word.ToLower()]++;
                                    else
                                        dictionary.Add(word.ToLower(), 1);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }
            }
        }

        private void InsertDataSortDescending()
        {
            dgWords.Items.Clear();
            foreach (var word in dictionary.OrderByDescending(key => key.Value))
            {
                dgWords.Items.Add(word);
            }
        }
    }
}
