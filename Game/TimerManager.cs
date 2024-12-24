using System;
using System.Timers;

namespace Tetris.Game
{
    public class TimerManager
    {
        private Timer gameTimer;
        private int elapsedSeconds; // Temps écoulé en secondes
        public event Action<int> OnTimeUpdated; // Événement déclenché à chaque mise à jour du temps

        public TimerManager()
        {
            elapsedSeconds = 0;
            gameTimer = new Timer(1000); // Tick toutes les secondes
            gameTimer.Elapsed += (s, e) =>
            {
                elapsedSeconds++;
                OnTimeUpdated?.Invoke(elapsedSeconds); // Notifie les abonnés
            };
        }

        public int ElapsedTime => elapsedSeconds;

        // Démarre le chronomètre
        public void Start()
        {
            gameTimer.Start();
        }

        // Met en pause le chronomètre
        public void Pause()
        {
            gameTimer.Stop();
        }

        // Réinitialise le chronomètre
        public void Reset()
        {
            elapsedSeconds = 0;
            OnTimeUpdated?.Invoke(elapsedSeconds); // Notifie les abonnés
        }
    }
}