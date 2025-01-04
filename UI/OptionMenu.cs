using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris.UI
{
    public partial class OptionMenu : Form
    {
        // Propriété statique pour stocker la vitesse du jeu
        public static int GameSpeed { get; private set; } = 5;

        public OptionMenu()
        {
            // Configuration de la fenêtre d'options
            this.Text = "Tetris - Options"; 
            this.ClientSize = new Size(500, 400); 
            this.BackColor = Color.Blue; 

            // Création et ajout du titre "OPTIONS" à la fenêtre
            Label titleLabel = new Label
            {
                Text = "OPTIONS",
                Font = new Font("Arial", 28, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(400, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point((this.ClientSize.Width - 400) / 2, 20)
            };
            this.Controls.Add(titleLabel);

            // Création et ajout du label "Game Speed"
            Label speedLabel = new Label
            {
                Text = "Game Speed",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(200, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point((this.ClientSize.Width - 200) / 2, 100)
            };
            this.Controls.Add(speedLabel);

            // Création et ajout du TrackBar pour ajuster la vitesse du jeu
            TrackBar speedTrackBar = new TrackBar
            {
                Minimum = 1,
                Maximum = 10,
                Value = GameSpeed, 
                TickFrequency = 1,
                Size = new Size(300, 45),
                Location = new Point((this.ClientSize.Width - 300) / 2, 140)
            };
            this.Controls.Add(speedTrackBar);

            // Création et ajout d'un label pour afficher la vitesse actuelle
            Label speedValueLabel = new Label
            {
                Text = $"Speed: {GameSpeed}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(100, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point((this.ClientSize.Width - 100) / 2, 190)
            };
            this.Controls.Add(speedValueLabel);

            // Synchronisation de la valeur de la vitesse avec le TrackBar
            speedTrackBar.Scroll += (sender, e) =>
            {
                GameSpeed = speedTrackBar.Value;
                speedValueLabel.Text = $"Speed: {GameSpeed}";
                Console.WriteLine($"Game Speed updated to: {GameSpeed}"); 
            };

            // Création et ajout du bouton pour revenir au menu principal
            Button backButton = CreateButton("Back", new Point((this.ClientSize.Width - 200) / 2, 250), Color.Gray, BackButton_Click);
            this.Controls.Add(backButton);
        }

        // Méthode pour créer un bouton avec un texte, une couleur et une action
        private Button CreateButton(string text, Point location, Color backColor, EventHandler onClick)
        {
            Button button = new Button
            {
                Text = text,
                Size = new Size(200, 50),
                Location = location,
                BackColor = backColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White
            };

            button.FlatAppearance.BorderColor = Color.Black;
            button.FlatAppearance.BorderSize = 2;

            // Attache l'événement de clic au bouton
            button.Click += onClick;

            return button;
        }

        // Méthode pour gérer l'événement de retour au menu principal
        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }
    }
}
