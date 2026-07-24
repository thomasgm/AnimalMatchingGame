namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            AnimalButtons.IsVisible = false;
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalButtons.IsVisible = true;
            List<string> animalEmoji = new List<string>()
            {
                "🐒","🐒",
                "🐶","🐶",
                "🐱","🐱",
                "🦊","🦊",
                "🐻","🐻",
                "🐼","🐼",
                "🐨","🐨",
                "🐯","🐯"
            };
            foreach (Button button in AnimalButtons.Children.OfType<Button>())
            {
                if (animalEmoji.Count == 0)
                {
                    break;
                }
                int index = new Random().Next(animalEmoji.Count);
                button.Text = animalEmoji[index];
                animalEmoji.RemoveAt(index);
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
