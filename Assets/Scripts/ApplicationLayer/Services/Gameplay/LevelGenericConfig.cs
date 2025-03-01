using UnityEngine;

namespace ApplicationLayer.Services.Gameplay
{
    [CreateAssetMenu(fileName = "LevelGenericConfig", menuName = "ScriptableObjects/Level Generic Config", order = 0)]
    public class LevelGenericConfig : ScriptableObject
    {
        [Header("Board")]
        public Vector2Int BoardRowsRange = new Vector2Int(4,9);
        public Vector2Int BoardColumnsRange = new Vector2Int(4,9);
    }
}