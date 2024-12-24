using System;

namespace Tetris.Game
{
    public class PauseManager
    {
        private bool isPaused;
        public event Action<bool> OnPauseStateChanged;

        public bool IsPaused() => isPaused;

        public void TogglePause()
        {
            isPaused = !isPaused;
            OnPauseStateChanged?.Invoke(isPaused);
        }

        public void SetPause(bool pause)
        {
            if (isPaused != pause)
            {
                isPaused = pause;
                OnPauseStateChanged?.Invoke(isPaused);
            }
        }
    }
}