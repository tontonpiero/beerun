using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeRun
{
    [Serializable]
    public struct UserData
    {
        // resources
        public int Coins;

        // progress
        public int MaxLevel;

        // stats
        public int AppLaunchCount;
        public int GameStartedCount;
        public int GameWonCount;
        public int GameLoseCount;
    }
}
