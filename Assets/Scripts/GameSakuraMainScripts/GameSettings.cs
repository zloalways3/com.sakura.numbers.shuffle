using UnityEngine;

namespace GameSakuraMainScripts
{
    [CreateAssetMenu(fileName = "NewGameSettings", menuName = "Game/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public Vector2Int Size;
        public Vector2 Offset;
        public bool TimerEnabled = true;

        public int LevelCount = 20;
        public int LevelRandomSeed = 9999;

    }
}