using ConsoleRpgStage1.UI;
using ConsoleRpgStage1.World;

var worldFactory = new WorldFactory();
var world = worldFactory.CreateDefault();

var renderer = new Renderer();
var frame = renderer.BuildFrame(world);

Console.WriteLine(frame);
