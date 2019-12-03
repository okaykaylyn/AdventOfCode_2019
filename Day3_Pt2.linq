<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xml.Linq.dll</Reference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Xml</Namespace>
  <Namespace>System.Xml.Linq</Namespace>
</Query>

void Main()
{
	string[] input = File.ReadAllText(@"c:\Users\Kaylyn\Desktop\AOC\day3.txt").Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
	string[] test = new string[] { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" };
	string[] wire1 = input[0].Split(',');
	string[] wire2 = input[1].Split(',');

	Dictionary<(int, int), int> firstWire = ImprovedWirePlotter(wire1);
	Dictionary<(int, int), int> secondWire = ImprovedWirePlotter(wire2);
	
	var intersections = firstWire.Keys.Intersect(secondWire.Keys);
	var closest = intersections.Where(s => s.Item1!=0 && s.Item2!=0).Min(i => TaxiCabGeometry(i.Item1, i.Item2));
	var stepsTaken = intersections.Where(s => s.Item1!=0 && s.Item2!=0).Min(i => firstWire[i]+ secondWire[i]);
	
	Console.WriteLine(closest);
	Console.WriteLine(stepsTaken);
}

public int TaxiCabGeometry(int x, int y) {
	return Math.Abs(x)+Math.Abs(y);
}

public class CachedCoord
{
	public int X { get; set; }
	public int Y { get; set; }
	public CachedCoord() {}
	public CachedCoord(int x, int y) {
		X=x;
		Y=y;
	}
}

public Dictionary<(int, int), int> ImprovedWirePlotter(string[] path)
{
	Dictionary<(int, int), int> d = new Dictionary<(int, int), int>();
	CachedCoord cache = new CachedCoord(0,0);
	int steps = 0;
	
	d.Add((0,0),0);
	steps++;

	foreach (string instruction in path)
	{
		int xDiff = 0;
		int yDiff = 0;
		
		char director = instruction[0];
		int distance = int.Parse(instruction.Remove(0, 1));

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
			try
			{
				d.Add((cache.X + xDiff, cache.Y + yDiff), steps++);
			}
			catch (Exception ex)
			{ }
			cache = new CachedCoord(cache.X + xDiff,cache.Y + yDiff);
		}
	}
	return d;
}

