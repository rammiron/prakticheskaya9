using System;
using System.Collections.Generic;
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

namespace prakticheskaya11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void DrawOnScreen(string data, bool add = false)
        {
            if (add)
            {
                InputTextBox.Text += data;
            }
            else
            {
                InputTextBox.Text = data;
            }
        }
        private void ClearScreenData()
        {
            ResultButton.Cursor = Cursors.No;
            ResultButton.Background = Brushes.Red;
            ResultLabel.Text = "";
            ArrayOutput.Text = "";
            InputTextBox.Text = "";
        }

        private int[] GetArrayFromInput()
        {
            int[] array = new int[12];

            for (int i = 0; i < InputTextBox.LineCount && i < array.Length; i++)
            {
                if (InputTextBox.GetLineText(i).Length <= 0 || int.TryParse(InputTextBox.GetLineText(i), out int bb) == false)
                {
                    continue;
                }
                array[i] = Convert.ToInt32(InputTextBox.GetLineText(i));
            }

            return array;
        }
        private string GetSortedArray(int[] array)
        {
            string result = "";

            int temp;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }

            foreach (var item in array)
            {
                result += $"{item} ";
            }
            return $"Сортированный массив: {result}";
        }


        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputTextBox.LineCount > 12)
            {
                string tempText = "";
                for (int i = 0; i < 12; i++)
                {
                    tempText += InputTextBox.GetLineText(i);
                }
                InputTextBox.Text = tempText;
                InputTextBox.CaretIndex = InputTextBox.Text.Length;
                return;
            }

            string text = InputTextBox.Text;
            if (text.Length == 0) { return; }
            for (int i = 0; i < text.Length; i++)
            {
                var item = text[i];
                if (int.TryParse(item.ToString(), out int temp0) == false && item.ToString() != "\n")
                {
                    if (text.Length == 1) { text = ""; } else { text = text.Remove(i, 1); }
                    DrawOnScreen(text, false);
                    InputTextBox.CaretIndex = InputTextBox.Text.Length;

                }
            }
            ArrayOutput.Text = "";
            if (InputTextBox.Text.Length > 0)
            {
                foreach (var item in GetArrayFromInput())
                {
                    ArrayOutput.Text += item.ToString() + " ";

                }
            }
            if (InputTextBox.LineCount != 12)
            {
                ResultButton.Cursor = Cursors.No;
                ResultButton.Background = Brushes.Red;
            }
            else
            {
                ResultButton.Cursor = Cursors.Hand;
                ResultButton.Background = Brushes.Green;
            }

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearScreenData();
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultButton.Background == Brushes.Red)
            {
                return;
            }

            ResultLabel.Text = GetSortedArray(GetArrayFromInput());

        }
    }
}
