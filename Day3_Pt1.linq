<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Collections.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xml.Linq.dll</Reference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Xml</Namespace>
  <Namespace>System.Xml.Linq</Namespace>
  <Namespace>static UserQuery</Namespace>
</Query>

void Main()
{
	string[] input = File.ReadAllText(@"c:\Users\Kaylyn\Desktop\AOC\day3.txt").Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
	string[] test = new string[] { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" };
	string[] wire1 = input[0].Split(',');
	string[] wire2 = input[1].Split(',');

	WirePlotter firstWire = new WirePlotter();
	firstWire.CompileCoordinates(wire1);

	WirePlotter secondWire = new WirePlotter();
	secondWire.CompileCoordinates(wire2);


	var intersects = firstWire.AllCoords.Intersect(secondWire.AllCoords);
	List<int> manhattanDistances = new List<int>();

	foreach (var coord in intersects)
	{
		manhattanDistances.Add(Math.Abs(coord.X) + Math.Abs(coord.Y));
	}

	Console.WriteLine(manhattanDistances.Where(x => x != 0).OrderByDescending(m => m).Min(c => c));
}
// Define other methods and classes here
public class Coordinates : HashSet<(int X, int Y)>
{ }

class WirePlotter
{
	public int cachedX { get; set; }
	public int cachedY { get; set; }
	public Coordinates AllCoords { get; set; }

	public WirePlotter()
	{
		AllCoords = new Coordinates();
		(cachedX, cachedY) = (0, 0);
		AllCoords.Add((cachedX, cachedY));
	}

	public void CompileCoordinates(string[] path)
	{
		foreach (string instruction in path)
		{
			char director = instruction[0];
			int distance = int.Parse(instruction.Remove(0, 1));

			FeedCoordinates(director, distance);
		}
	}

	private void FeedCoordinates(char director, int distance)
	{
		int xDiff = 0;
		int yDiff = 0;

		switch (director)
		{
			case 'R':
				xDiff = Math.Abs(1);
				break;
			case 'U':
				yDiff = Math.Abs(1);
				break;
			case 'D':
				yDiff = -1;
				break;
			case 'L':
				xDiff = -1;
				break;
		}

		for (int i = 0; i < distance; i++)
		{
			(int localX, int localY) = (cachedX, cachedY);
			(int newX, int newY) = (localX + xDiff, localY + yDiff);
			AllCoords.Add((newX, newY));
			(cachedX, cachedY) = (newX, newY);
		}
	}
}