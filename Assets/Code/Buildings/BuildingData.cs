using System.Collections.Generic;
using UnityEngine;

namespace Yarde.Buildings
{
    [CreateAssetMenu(menuName = "Buildings/Data")]
    public class BuildingData : ScriptableObject
    {
        public List<ResourceCost> Costs;
        public List<Sprite> Sprites;
        public int MaxLevel => Costs.Count - 1;
    }
}