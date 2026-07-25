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
            PlayAgainButton.IsVisible = false;
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
                button.ClassId = animalEmoji[index];
                button.Text = "";
                animalEmoji.RemoveAt(index);
            }
            matchesFound = 0;
            Dispatcher.StartTimer(TimeSpan.FromSeconds(1), TimerTrick);
        }

        int tenthsOfSecondsElapsed = 0;
        private bool TimerTrick()
        {
            if (!this.IsLoaded) return false;

            tenthsOfSecondsElapsed++;

            TimeElapsed.Text = "Time Elapsed: " + (tenthsOfSecondsElapsed / 10F).ToString("0.0s");

            if (PlayAgainButton.IsVisible == true)
            {
                tenthsOfSecondsElapsed = 0;
                return false;
            }
            return true;
        }

        Button lastClicked;
        bool findingMatch = false;
        int matchesFound = 0;
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button buttonClicked)
            {
                // Evita clicar em botões que já foram descobertos (texto limpo mas ClassId removida)
                // ou clicar duas vezes seguidas no mesmíssimo botão aberto
                if (string.IsNullOrEmpty(buttonClicked.ClassId) || buttonClicked == lastClicked)
                {
                    return;
                }
                // Revela o emoji que estava escondido no ClassId
                buttonClicked.Text = buttonClicked.ClassId;
                // SEGUNDO CLIQUE: Comparação
                if (findingMatch)
                {
                    // Se os emojis escondidos no ClassId forem iguais (Acertou!)
                    if (buttonClicked.ClassId == lastClicked.ClassId)
                    {
                        matchesFound++;

                        // Pinta de uma cor diferente para indicar sucesso (ex: cinza ou mantém azul)
                        buttonClicked.BackgroundColor = Colors.LightCyan;
                        lastClicked.BackgroundColor = Colors.LightCyan;

                        // Deixa o texto visível permanentemente, mas limpa o ClassId 
                        // para o código saber que esse par já foi resolvido
                        buttonClicked.ClassId = string.Empty;
                        lastClicked.ClassId = string.Empty;
                    }
                    // Se forem diferentes (Errou!)
                    else
                    {
                        buttonClicked.BackgroundColor = Colors.Red;

                        // "Vira a carta de volta" apagando o texto
                        buttonClicked.Text = "";
                        lastClicked.Text = "";

                        // Restaura a cor original
                        buttonClicked.BackgroundColor = Colors.LightBlue;
                        lastClicked.BackgroundColor = Colors.LightBlue;
                    }

                    // Reseta as variáveis de controle do par
                    lastClicked = null;
                    findingMatch = false;
                }
                // PRIMEIRO CLIQUE: Apenas abre a carta
                else
                {
                    lastClicked = buttonClicked;
                    buttonClicked.BackgroundColor = Colors.Orange; // Cor diferenciada para a primeira carta aberta
                    findingMatch = true;
                }
            }

            if (matchesFound == 8)
            {
                matchesFound = 0;
                AnimalButtons.IsVisible = false;
                PlayAgainButton.IsVisible = true;
            }
        }
    }
}
