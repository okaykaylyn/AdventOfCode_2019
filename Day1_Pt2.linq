
void Main()
{
	string[] input = File.ReadAllText(@"c:\Users\Kaylyn\Desktop\AOC\Day1_A.txt").Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
	//string[] testInput = new string[] { "1969" };

	double result = CalculateFuelRequirements(input);
	Console.WriteLine(result);
}

double CalculateFuelRequirements(string[] input)
{
	double result = 0;

	foreach (string line in input)
	{
		double.TryParse(line, out double asNumber);
		if (asNumber > 0)
		{
			double resultOfDiv = DivideByThree(asNumber);
			double resultOfSubtraction = resultOfDiv - 2;
			double resultPtTwo = CalculateFuelForFuel(resultOfSubtraction);
			result += resultOfSubtraction + resultPtTwo;
		}
	}

	return result;
}

double CalculateFuelForFuel(double num)
{
	if (num <= 0)
		return 0;
		
	double fuelForFuel = 0;

	double temp = DivideByThree(num);
	temp -= 2;
	fuelForFuel += temp;
	
	if (fuelForFuel <=0)
		return 0;

	return fuelForFuel + CalculateFuelForFuel(fuelForFuel);
}

double DivideByThree(double asNumber)
{
	return Convert.ToDouble(Math.Floor(asNumber / 3));
}

// Define other methods and classes here
