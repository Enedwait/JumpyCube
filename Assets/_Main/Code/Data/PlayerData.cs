using System;

namespace OKRT.JumpyCube.Main.Code.Data
{
    /// <summary>
    /// The <see cref="PlayerData"/> class.
    /// This class contains player data.
    /// </summary>
    [Serializable]
    internal class PlayerData
    {
        public string name;
        public long hiScore;
        public float gameVolume;
        public float musicVolume;
        public float uiVolume;
    }
}
