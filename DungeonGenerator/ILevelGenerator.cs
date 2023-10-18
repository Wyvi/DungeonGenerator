using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator
{
    public interface ILevelGenerator
    {
        Level GenerateLevel(LevelParameters parameters);
    }
}
