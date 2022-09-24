using UnityEngine;

namespace Yarde.GameData
{
    [CreateAssetMenu(menuName = "Data/Gameplay")]
    public class GameplayData : ScriptableObject
    {
        public float TickLenght;
        public float DayLength;
        public float DayNightRatio;

        public float FoodLoss;
        public float FoodLossModifier;
        
        public float AttacksModifier;
    }
}