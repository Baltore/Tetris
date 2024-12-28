using System;
using System.Timers;

namespace Tetris.Game
{
    public class TimerManager
    {
        private Timer gameTimer;
        private int elapsedSeconds; // Temps écoulé en secondes
        public event Action<int> OnTimeUpdated; // Événement déclenché à chaque mise à jour du temps
        public event Action<int> OnThresholdReached; // Événement déclenché à certains seuils

        private readonly int[] thresholds = { 60, 120, 200 }; // Seuils en secondes
        private int nextThresholdIndex;

        public TimerManager()
        {
            elapsedSeconds = 0;
            nextThresholdIndex = 0;
            gameTimer = new Timer(1000); // Tick toutes les secondes
            gameTimer.Elapsed += (s, e) =>
            {
                elapsedSeconds++;
                OnTimeUpdated?.Invoke(elapsedSeconds); // Notifie les abonnés

                if (nextThresholdIndex < thresholds.Length && elapsedSeconds == thresholds[nextThresholdIndex])
                {
                    OnThresholdReached?.Invoke(thresholds[nextThresholdIndex]);
                    nextThresholdIndex++;
                }
            };
        }

        public int ElapsedTime => elapsedSeconds;

        public void Start()
        {
            gameTimer.Start();
        }

        public void Pause()
        {
            gameTimer.Stop();
        }

        public void Reset()
        {
            elapsedSeconds = 0;
            nextThresholdIndex = 0;
            OnTimeUpdated?.Invoke(elapsedSeconds);
        }
    }
}