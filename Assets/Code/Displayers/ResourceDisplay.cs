using System;
using UnityEngine;

namespace Yarde.Displayers
{
    public class ResourceDisplay : MonoBehaviour
    {
        [SerializeField] private ResourceType type;

        //private TextMeshProUGUI text;
        
        private void Awake()
        {
            
        }
    }

    public enum ResourceType
    {
        Wood,
        Stone,
        Food
    }
}