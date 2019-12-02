
void Main()
{
	string[] input = File.ReadAllText(@"c:\Users\Kaylyn\Desktop\AOC\Day2_A.txt").Trim().Split(new[] { "\r\n", "\r", "\n", "," }, StringSplitOptions.None);
	string[] cached = File.ReadAllText(@"c:\Users\Kaylyn\Desktop\AOC\Day2_A.txt").Trim().Split(new[] { "\r\n", "\r", "\n", "," }, StringSplitOptions.None);

	string[] result = new string[145];

	for (int v = 0; v <= 99; v++)
	{
		for (int n = 0; n <= 99; n++)
		{
			Array.Copy(cached, input, input.Length);

			Random r = new Random();
			input[1] = v.ToString();
			input[2] = n.ToString();

			result = RecreateIntcodeComputer(input);

			string resultCase = result[0];

			if (resultCase == "19690720")
			{
				Console.WriteLine(100 * int.Parse(input[1]) + int.Parse(input[2]))
				break;
			}
		}
	}
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
