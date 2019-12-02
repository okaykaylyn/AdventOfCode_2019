<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xml.Linq.dll</Reference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Xml</Namespace>
  <Namespace>System.Xml.Linq</Namespace>
</Query>

void Main()
{
	string[] input = File.ReadAllText(@"c:\Users\Kaylyn\Desktop\AOC\Day2_A.txt").Trim().Split(new[] { "\r\n", "\r", "\n", "," }, StringSplitOptions.None);
	//string[] test ="1,9,10,3,2,3,11,0,99,30,40,50".Split(',');
	//s	input.Dump();
	input[1] = "12";
	input[2] = "2";
	string[] result = RecreateIntcodeComputer(input);
	//result.Dump();
	
	result[0].Dump();
}

public string[] RecreateIntcodeComputer(string[] input)
{
	int i = 0;
	do
	{
		int value = int.Parse(input[i]);

		if (value == 99)
			break;

		else if (value == 1)
		{
			//add
			int i1 = int.Parse(input[i + 1]);
			int i2 = int.Parse(input[i + 2]);
			int result = int.Parse(input[i1]) + int.Parse(input[i2]);

			int newI = int.Parse(input[i + 3]);
			input[newI] = result.ToString();
			i += 4;
		}

		else if (value == 2)
		{
			int i1 = int.Parse(input[i + 1]);
			int i2 = int.Parse(input[i + 2]);
			int result = int.Parse(input[i1]) * int.Parse(input[i2]);

			int newI = int.Parse(input[i + 3]);
			input[newI] = result.ToString();
			i += 4;
		}

	} while (i <= input.Length);

	return input;
}

// Define other methods and classes here
