using System.Collections.Generic;
using UnityEngine;
using Yarde.Buildings;

namespace Yarde.GameData
{
    [CreateAssetMenu(menuName = "Data/Buildings")]
    public class BuildingData : ScriptableObject
    {
        public List<ResourceCost> Costs;
        public List<Sprite> Sprites;
        public int MaxLevel => Costs.Count - 1;
    }
}