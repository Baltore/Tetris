using System;
using System.Windows.Forms;

namespace Tetris.UI
{
    public partial class OptionMenu : Form
    {
        public OptionMenu()
        {
            // Configuration de la fenêtre des options
            this.Text = "Tetris - Options";
            this.ClientSize = new System.Drawing.Size(400, 300); // Taille de la fenêtre

            // Créer un label pour la vitesse
            Label speedLabel = new Label
            {
                Text = "Game Speed",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(150, 50)
            };

            // Créer un TrackBar pour ajuster la vitesse
            TrackBar speedTrackBar = new TrackBar
            {
                Minimum = 1,
                Maximum = 10,
                Value = 5, // Vitesse initiale
                Size = new System.Drawing.Size(200, 45),
                Location = new System.Drawing.Point(100, 100)
            };

            speedTrackBar.Scroll += (sender, e) =>
            {
                // Afficher la vitesse sélectionnée (on pourrait l'utiliser pour ajuster la vitesse de jeu)
                int speed = speedTrackBar.Value;
                Console.WriteLine($"Speed: {speed}"); // Affiche la vitesse dans la console
            };

            // Créer un bouton pour revenir au menu principal
            Button backButton = new Button
            {
                Text = "Back",
                Size = new System.Drawing.Size(200, 50),
                Location = new System.Drawing.Point(100, 200)
            };
            backButton.Click += BackButton_Click;

            // Ajouter les contrôles à la fenêtre
            this.Controls.Add(speedLabel);
            this.Controls.Add(speedTrackBar);
            this.Controls.Add(backButton);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // Revenir au menu principal
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }
    }
}
