using UnityEngine;

namespace Systems
{
    public class GameStateService : MonoBehaviour
    {
        public static GameStateService Instance;

        public GameStateFlags Flags { get; private set; }

        private void Awake()
        {
            Instance = this;
            Flags = new GameStateFlags();
        }

        public void SetPaused(bool value) => Flags.isPaused = value;
        public void SetCombat(bool value) => Flags.isInCombat = value;
        public void SetGameOver(bool value) => Flags.isGameOver = value;
    }

    public class GameStateFlags
    {
        public bool isPaused;
        public bool isInCombat;
        public bool isGameOver;
    }
}
