using System.Collections.Generic;

namespace Towers_Of_Hanoi
{
    public sealed class GameLogicManager
    {
        private static readonly GameLogicManager s_instance = new GameLogicManager();
        public static GameLogicManager Instance => s_instance;

        public int NumberOfLevels { get; private set; }
        public List<string> SelectedColors { get; private set; } = new List<string>();

        private GameLogicManager() { }

        public void InitializeGame(int levels, List<string> colors)
        {
            NumberOfLevels = levels;
            SelectedColors = new List<string>(colors);
        }
    }
}
