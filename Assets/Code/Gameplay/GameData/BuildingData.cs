using System.Collections.Generic;
using UnityEngine;
using Yarde.Gameplay.Buildings;

namespace Yarde.Gameplay.GameData
{
    [CreateAssetMenu(menuName = "Data/Buildings")]
    public class BuildingData : ScriptableObject
    {
        public List<ResourceCost> Costs;
        public List<Sprite> Sprites;
        public int MaxLevel => Costs.Count - 1;
        public float BuildTime;
    }
}