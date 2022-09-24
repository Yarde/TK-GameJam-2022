using System;
using UnityEngine;

namespace Yarde.Gameplay.GameData
{
    [CreateAssetMenu(menuName = "Data/Gameplay")]
    public class GameplayData : ScriptableObject
    {
        public float TickLenght;
        public float DayLength;
        public float DayNightRatio;

        public float FoodLoss;
        public float FoodLossModifier;
        public int FoodLossCycles;
        
        public float AttacksModifier;
        
        // resources
        public ResourceData WoodData;
        public ResourceData StoneData;
        public ResourceData FoodData;
    }

    [Serializable]
    public struct ResourceData
    {
        public float Gain;
        public float TimeToCollect;
        public float StartAmount;
    }
}