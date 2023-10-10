using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator
{
    internal interface ILevelGenerator
    {
        Level GenerateLevel(LevelParameters parameters);
    }
}
