using System;

namespace Tetris.Game
{
    public class ScoreManager
    {
        public int Score { get; private set; } // Propriété pour accéder au score actuel
        public event Action<int> OnScoreUpdated; // Événement pour notifier des mises à jour

        public ScoreManager()
        {
            Score = 0;
        }

         public void AddPoints(int linesCleared)
        {
            int points = linesCleared switch
            {
                1 => 40,   // 1 ligne
                2 => 100,  // 2 lignes
                3 => 300,  // 3 lignes
                4 => 1200, // Tetris (4 lignes)
                _ => 0
            };

            Score += points;
            OnScoreUpdated?.Invoke(Score); // Déclenche l'événement
        }

        public void Reset()
        {
            Score = 0;
            OnScoreUpdated?.Invoke(Score); // Notifie que le score est réinitialisé
        }
    }
}
