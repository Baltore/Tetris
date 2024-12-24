using System;
using System.Windows.Forms;

namespace Tetris.UI
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            // Configuration de la fenêtre du menu
            this.Text = "Tetris - Main Menu";
            this.ClientSize = new System.Drawing.Size(400, 300); // Taille de la fenêtre

            // Création des boutons
            Button playButton = new Button
            {
                Text = "Play",
                Size = new System.Drawing.Size(200, 50),
                Location = new System.Drawing.Point(100, 50)
            };
            playButton.Click += PlayButton_Click;

            Button optionsButton = new Button
            {
                Text = "Options",
                Size = new System.Drawing.Size(200, 50),
                Location = new System.Drawing.Point(100, 120)
            };
            optionsButton.Click += OptionsButton_Click;

            Button quitButton = new Button
            {
                Text = "Quit",
                Size = new System.Drawing.Size(200, 50),
                Location = new System.Drawing.Point(100, 190)
            };
            quitButton.Click += QuitButton_Click;

            // Ajout des boutons à la fenêtre
            this.Controls.Add(playButton);
            this.Controls.Add(optionsButton);
            this.Controls.Add(quitButton);
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            GameUI gameUI = new GameUI();
            gameUI.Show();
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            OptionMenu optionMenu = new OptionMenu();
            optionMenu.Show();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
