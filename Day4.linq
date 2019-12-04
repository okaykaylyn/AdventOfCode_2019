
void Main()
{
	string[] input = "235741-706948".Split('-');

	int lowerBound = int.Parse(input[0]);
	int upperBound = int.Parse(input[1]);

	HashSet<int> candidates = new HashSet<int>();

	//value is within range of input
	for (int i = lowerBound; i <= upperBound; i++)
	{
		int attempt = i;

		//verify 6 digits
		bool six = attempt.ToString().Length >= 6 ? true : false;
		if (!six)
			continue;

		//at least two adjacent digits are the same
		//bool twoAdjacent = CheckAdjacency(attempt)
		
		//only two adjacent
		bool twoAdjacent = CheckForOnlyTwo(attempt);
		if (!twoAdjacent)
			continue;

		//numbers increase/stay the same from left to right
		bool increasing = CheckIncreasing(attempt);
		if (!increasing)
			continue;

		candidates.Add(attempt);
	}

	candidates.Count().Dump();
}

bool CheckForOnlyTwo(int attempt)
{
	string input = attempt.ToString();
	List<int> counts = new List<int>();
	int count = 1;

	for (int i = 0; i < input.Length; i++)
	{
		int current = int.Parse(input[i].ToString());

		if (i + 1 == input.Length)
		{
			if (count <= 2)
				counts.Add(count);
			break;
		}

		int next = int.Parse(input[i + 1].ToString());

		if (next == current)
			count++;
		else
		{
			counts.Add(count);
			count = 1;
		}

	}

	if (counts.Any(c => c == 2))
		return true;
	else
		return false;
}

bool CheckIncreasing(int attempt)
{
	int cached = 0;
	bool isGreater = false;

	foreach (var num in attempt.ToString())
	{
		if (cached == 0)
			cached = num;

		else
		{
			isGreater = num >= cached ? true : false;
			if (!isGreater)
				return false;
			else
				isGreater = true;
		}
		cached = num;
	}

	return isGreater;
}

bool CheckAdjacency(int attempt)
{
	char[] input = attempt.ToString().ToCharArray();

	var adjacentDuplicate = input
	.Skip(1)
	.Where((value, index) => value == input[index])
	.Distinct();

	if (adjacentDuplicate.Any())
	{
		int count = adjacentDuplicate.Count();
		return true;
	}
	else
		return false;
}

// Define other methods and classes here
