using System;
using OKRT.JumpyCube.Main.Code.Views;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="HasObstacleView"/> struct.
    /// This component contains the data of the <see cref="ObstacleView"/>.
    /// </summary>
    [Serializable]
    struct HasObstacleView
    {
        public ObstacleView view;
    }
}