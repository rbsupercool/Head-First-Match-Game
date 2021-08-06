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



namespace Head_First_Match_Game
{


    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSencondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;

            SetupGame();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSencondsElapsed++;
            timeTextBlock.Text = (tenthOfSencondsElapsed / 10f).ToString("0.0s");

            if(matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play Again?";
            }
        }

        private void SetupGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐸","🐸",
                "🦁","🦁",
                "🐺","🐺",
                "🦄","🦄",
                "🐶","🐶",
                "🐵","🐵",
                "🐼","🐼",
                "🐉","🐉",


            };

            Random random = new Random();

            foreach(TextBlock textblock in gameGrid.Children.OfType<TextBlock>())
            {

                if(textblock.Name != "timeTextBlock")
                {
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textblock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }


            }

            timer.Start();
            tenthOfSencondsElapsed = 0;
            matchesFound = 0;

        }


        TextBlock lastClickedTextBox;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if(findingMatch == false)
            {
                lastClickedTextBox = textBlock;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = true;
            }
            else if(findingMatch == true)
            {
                if(textBlock.Text == lastClickedTextBox.Text)
                {
                    textBlock.Visibility = Visibility.Hidden;
                    matchesFound++;
                    
                }
                else
                {
                    lastClickedTextBox.Visibility = Visibility.Visible;
                    
                }

                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchesFound == 8)
            {
                SetupGame();
            }
        }
    }
}
