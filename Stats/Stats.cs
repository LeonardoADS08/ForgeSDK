using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Stats
{
    public class Stats
    {
        public static List<string> STATS = new List<string>()
        {
            HEALTH,
            SHIELD,
            WALK_SPEED,
            DODGE_SPEED,
            DODGE_TIME
        };

        public const string HEALTH = "Health";
        public const string SHIELD = "Shield";
        public const string WALK_SPEED = "Walk Speed";
        public const string DODGE_SPEED = "Dodge Speed";
        public const string DODGE_TIME = "Dodge Time";
    }
}
