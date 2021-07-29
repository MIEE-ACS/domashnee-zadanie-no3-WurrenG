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

namespace HW_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<char> cyrillicAlphabet = new List<char> 
            {
            'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з',
            'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р',
            'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ',
                'ъ', 'ы', 'ь', 'э', 'ю', 'я' 
            };
        private List<char> latinAlphabet = new List<char>
            {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
            'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
            's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
        private List<char> alphabet = new List<char>
            {
            'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з',
            'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р',
            'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ',
                'ъ', 'ы', 'ь', 'э', 'ю', 'я'
            };


        public MainWindow()
        {
            InitializeComponent();
        }

        private void encryptor(object sender, RoutedEventArgs e)
        {
            tbEncryptedText.Clear();
            char encryptedChar;
            int codeWordLetter = 0;
            foreach (char i in tbUnencryptedText.Text)
            {
                int letterNumber = alphabet.IndexOf(Char.ToLower(i));
                int codeWordLetterNumber = alphabet.IndexOf(tbCodeWord.Text[codeWordLetter]);

                if (letterNumber != -1)
                {
                    encryptedChar = alphabet[(letterNumber + codeWordLetterNumber) % alphabet.Count];
                    codeWordLetter++;
                    codeWordLetter %= tbCodeWord.Text.Length;
                }
                else 
                    encryptedChar = i;

                tbEncryptedText.Text += encryptedChar;
                
            }

        }

        private void decryptor(object sender, RoutedEventArgs e)
        {
            tbUnencryptedText.Clear();
            char decryptedChar;
            int codeWordLetter = 0;
            foreach (char i in tbEncryptedText.Text)
            {
                int letterNumber = alphabet.IndexOf(Char.ToLower(i));
                int codeWordLetterNumber = alphabet.IndexOf(tbCodeWord.Text[codeWordLetter]);

                if (letterNumber != -1)
                {
                    decryptedChar = alphabet[(letterNumber + alphabet.Count - codeWordLetterNumber) % alphabet.Count];
                    codeWordLetter++;
                    codeWordLetter %= tbCodeWord.Text.Length;
                }
                else
                    decryptedChar = i;

                tbUnencryptedText.Text += decryptedChar;
                
            }

        }

        private void buttonsEnabler(object sender, TextChangedEventArgs e)
        {
            int count = 0;
            string codeWord = tbCodeWord.Text;
            foreach (char i in tbCodeWord.Text)
            {
                bool isHandled = true;
                if (alphabet[1] == 'b')
                    isHandled = !(i > 65 && i < 122);//tbCodeWord.Text
                if (alphabet[1] == 'б')
                    isHandled = !(i > 1040 && i < 1103);
                if (isHandled)
                    codeWord = tbCodeWord.Text.Remove(count, 1);
                count++;
            }
            tbCodeWord.Text = codeWord;

            if (tbEncryptedText.Text != "" && tbCodeWord.Text != "")
                decrypt.IsEnabled = true;
            else
                decrypt.IsEnabled = false;

            if (tbUnencryptedText.Text != "" && tbCodeWord.Text != "")
                encrypt.IsEnabled = true;
            else
                encrypt.IsEnabled = false;
        }

        private void btnSwitcher_Click(object sender, RoutedEventArgs e)
        {
            if (btnSwitcher.Content.ToString() == "English")
            {
                btnSwitcher.Content = "Русский";
                alphabet = latinAlphabet;
            }
            else
            {
                btnSwitcher.Content = "English";
                alphabet = cyrillicAlphabet;
            }
        }
    }
}


/*Приложение для шифрования текста при помощи шифра Виженера (https://ru.wikipedia.org/wiki/Шифр_Виженера ).
* Приложение должно позволять вводить произвольный текст кириллицей, вводить кодовое слово, зашифровывать и расшифровывать текст. 
* Допускается шифрование только кириллических символов, но шифрование латиницы будет плюсом.*/
