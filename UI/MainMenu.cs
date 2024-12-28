using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris.UI
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            // Configuration de la fenêtre du menu
            this.Text = "Tetris - Main Menu";
            this.ClientSize = new System.Drawing.Size(500, 500); // Taille de la fenêtre
            this.BackColor = Color.Blue; // Fond bleu

            // Ajout du titre "Tetris"
            Label titleLabel = new Label
            {
                Text = "TETRIS",
                Font = new Font("Arial", 36, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new System.Drawing.Size(400, 60),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point((this.ClientSize.Width - 400) / 2, 20) // Centré en haut
            };
            this.Controls.Add(titleLabel);

            // Ajout du logo façon Tetris avec des blocs colorés
            Panel logoPanel = CreateTetrisLogo();
            logoPanel.Location = new Point((this.ClientSize.Width - logoPanel.Width) / 2, 100);
            this.Controls.Add(logoPanel);

            // Création des boutons avec position centrée
            Button playButton = CreateButton("Play", new Point((this.ClientSize.Width - 200) / 2, 220), Color.Green, PlayButton_Click);
            Button optionsButton = CreateButton("Options", new Point((this.ClientSize.Width - 200) / 2, 290), Color.Cyan, OptionsButton_Click);
            Button quitButton = CreateButton("Quit", new Point((this.ClientSize.Width - 200) / 2, 360), Color.Gray, QuitButton_Click);

            // Ajout des boutons à la fenêtre
            this.Controls.Add(playButton);
            this.Controls.Add(optionsButton);
            this.Controls.Add(quitButton);
        }

        private Panel CreateTetrisLogo()
        {
            // Panel pour contenir le logo
            Panel panel = new Panel
            {
                Size = new Size(350, 50),
                BackColor = Color.Transparent
            };

            // Couleurs des blocs façon Tetris
            Color[] blockColors = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Cyan, Color.Purple };

            // Génération des blocs colorés
            for (int i = 0; i < 6; i++)
            {
                Panel block = new Panel
                {
                    Size = new Size(40, 40),
                    BackColor = blockColors[i],
                    Location = new Point(i * 60, 10) // Espacement horizontal
                };
                block.BorderStyle = BorderStyle.FixedSingle; // Contour noir
                panel.Controls.Add(block);
            }

            return panel;
        }

        private Button CreateButton(string text, Point location, Color backColor, EventHandler onClick)
        {
            Button button = new Button
            {
                Text = text,
                Size = new System.Drawing.Size(200, 50),
                Location = location,
                BackColor = backColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
            };

            button.FlatAppearance.BorderColor = Color.Black;
            button.FlatAppearance.BorderSize = 2;

            // Ajout de l'événement Click
            button.Click += onClick;

            return button;
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
