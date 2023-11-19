using DungeonGenerator;
using DungeonGenerator.Generators;
using DungeonGenerator.Structures;

var ruggedCave = new LevelParameters(30, 30, 0.4);
var bigCave = new LevelParameters(30, 30, 0.6);


var caveGenerator = new CaveGenerator();
var dungeonLevel = caveGenerator.GenerateLevel(bigCave);
dungeonLevel.WriteToConsole();

dungeonLevel = caveGenerator.GenerateLevel(ruggedCave);
dungeonLevel.WriteToConsole();


var roomGenerator = new RoomGenerator();
var roomLevel = roomGenerator.GenerateLevel(bigCave);
roomLevel.WriteToConsole();

roomLevel = roomGenerator.GenerateLevel(ruggedCave);
roomLevel.WriteToConsole();
