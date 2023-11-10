﻿using DungeonGenerator;
using DungeonGenerator.Cave;
using DungeonGenerator.Rooms;

var ruggedCave = new LevelParameters(30, 30, 0.4, new DungeonSettings());
var bigCave = new LevelParameters(30, 30, 0.6, new DungeonSettings());


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
