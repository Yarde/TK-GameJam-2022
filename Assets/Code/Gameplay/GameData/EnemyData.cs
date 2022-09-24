using System.Collections.Generic;
using UnityEngine;
using Yarde.Gameplay.Buildings;

namespace Yarde.Gameplay.GameData
{
    [CreateAssetMenu(menuName = "Data/Enemy")]
    public class EnemyData : ScriptableObject
    {
        public float TimeToActivate;
        public float ThresholdToActivate;
        public float ChanceOfOccurence;
        public float TimeToKill;
        public float AttackSpeed;
        public float Threat;
    }
}