using UnityEngine;
using Yarde.Gameplay.GameData;
using Yarde.Utils.Logger;

namespace Yarde.Gameplay.Buildings
{
    public class Tent : Building
    {
        public Tent(GameState state) : base(state)
        {
            Data = Resources.Load<BuildingData>("Tent");
            Level.Value = 1;
        }

        public override void Downgrade(GameLoop gameLoop)
        {
            base.Downgrade(gameLoop);
            if (Level == 0)
            {
                gameLoop.OnLoss.Invoke("Your village is destroyed! Protect it with walls and fire next time.");
            }
        }
    }
}