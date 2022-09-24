using UnityEngine;

namespace Yarde.Gameplay.GameData
{
    [CreateAssetMenu(menuName = "Data/Fire")]
    public class FireData : ScriptableObject
    {
        public float ActionTime;
        public float MaxFuel;
        public float ActionWoodCost;
        public float FuelLossOnTick;
        public float FuelGainOnAction;
    }
}