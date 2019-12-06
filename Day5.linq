
void Main()
{

	string[] input = File.ReadAllText(@"c:\Users\Kaylyn\Desktop\AOC\day5.txt").Trim().Split(new[] { "\r\n", "\r", "\n", "," }, StringSplitOptions.None);
	Computer intCode = new Computer(input);

	Console.WriteLine(intCode.Outputs);
}

// Define other methods and classes here
public enum OpCodes
{
	Add = 01,
	Multiply,
	Input,
	Output,
	JumpTrue,
	JumpFalse,
	LessThan,
	Equals,
	Stop = 99
}
public enum Parameters
{
	Position = 0,
	Immediate = 1
}
public class Computer
{
	public string[] Program { get; set; }
	public string[] Cache { get; set; }
	public List<(int mode, int param)> Data { get; set; }
	public int C { get; set; }
	public List<int> Outputs { get; set; }

	public int OpCode { get; set; }

	public Computer(string[] input)
	{
		Outputs = new List<int>();
		Program = input;
		Cache = input;

		SwitchOn();
	}

	private void SwitchOn()
	{
		C = 0;

		while (OpCode != 99)
		{
			Data = new List<(int mode, int param)>();
			string instruction = Program[C];
			while (instruction.Length != 5)
			{
				instruction = "0" + instruction;
			}

			OpCode = DeriveOpCode(instruction);
			int outPut;
			switch (OpCode)
			{
				case (int)OpCodes.Add:
					instruction = instruction.Remove(instruction.Length - 2, 2);
					Data = GetModesAndParams(instruction);
					outPut = int.Parse(Program[C++].ToString());
					PerformAddition(outPut);
					break;
				case (int)OpCodes.Input:
					int input = 5;
					PerformWrite(input, instruction);
					C++;
					break;
				case (int)OpCodes.Multiply:
					instruction = instruction.Remove(instruction.Length - 2, 2);
					Data = GetModesAndParams(instruction);
					outPut = int.Parse(Program[C].ToString());
					PerformMultiplication(outPut);
					C++;
					break;
				case (int)OpCodes.Output:
					PerformRead(instruction);
					C++;
					break;
				case (int)OpCodes.JumpTrue:
					instruction = instruction.Remove(instruction.Length - 2, 2);
					Data = GetModesAndParams(instruction, true);
					PerformTrueJump();
					break;
				case (int)OpCodes.JumpFalse:
					instruction = instruction.Remove(instruction.Length - 2, 2);
					Data = GetModesAndParams(instruction, true);
					PerformFalseJump();
					break;
				case (int)OpCodes.LessThan:
					instruction = instruction.Remove(instruction.Length - 2, 2);
					Data = GetModesAndParams(instruction);
					outPut = int.Parse(Program[C].ToString());
					PerformLessThan(outPut);
					C++;
					break;
				case (int)OpCodes.Equals:
					instruction = instruction.Remove(instruction.Length - 2, 2);
					Data = GetModesAndParams(instruction);
					outPut = int.Parse(Program[C].ToString());
					PerformEquality(outPut);
					C++;
					break;
				case (int)OpCodes.Stop:
					break;
			}
		};
	}

	void PerformFalseJump()
	{
		int[] values = GetValues(Data);

		if (values[0] == 0)
		{
			C = values[1];
		}
		else
			C+=3;
	}

	void PerformEquality(int o)
	{
		int[] values = GetValues(Data);

		if (values[0] == values[1])
		{
			Program[o] = "1";
		}
		else
		{
			Program[o] = "0";
		}
	}

	void PerformLessThan(int o)
	{
		int[] values = GetValues(Data);

		if (values[0] < values[1])
		{
			Program[o] = "1";
		}
		else
		{
			Program[o] = "0";
		}
	}

	void PerformTrueJump()
	{
		int[] values = GetValues(Data);
		int toCompare = values[0];
		if (toCompare != 0)
		{
			C = values[1];
		}
		else
			C += 3;
	}

	void PerformAddition(int outPut)
	{
		int[] values = GetValues(Data);

		int r = values[0] + values[1];
		int o = outPut;
		Program[o] = r.ToString();
	}

	void PerformRead(string instruction)
	{
		int a = int.Parse(Program[++C].ToString());
		int add = int.Parse(Program[a].ToString());
		Outputs.Add(add);
	}

	private void PerformWrite(int input, string instruction)
	{
		int a = int.Parse(Program[++C].ToString());
		Program[a] = input.ToString();
	}

	void PerformMultiplication(int outPut)
	{
		int[] values = GetValues(Data);

		int r = values[0] * values[1];
		int o = outPut;
		Program[o] = r.ToString();
	}

	int[] GetValues(List<(int mode, int param)> data)
	{
		int[] values = new int[3];
		int c = 0;

		for (int i = 0; i < 2; i++)
		{
			(int m, int p) = data[i];
			switch (m)
			{
				case (int)Parameters.Position:
					values[i] = int.Parse(Program[p]);
					break;
				case (int)Parameters.Immediate:
					values[i] = int.Parse(p.ToString());
					break;
			}
		}

		return values;
	}

	List<(int mode, int param)> GetModesAndParams(string instruction, bool isJump = false)
	{
		int k = C;

		List<(int mode, int param)> data = new List<(int mode, int param)>();

		instruction = ModifyInstruction(instruction);

		for (int j = instruction.Count() - 1; j >= 0; j--)
		{
			int mode = int.Parse(instruction[j].ToString());
			int param = int.Parse(Program[++k].ToString());
			data.Add((mode, param));
		}

		if (!isJump)
		{
			int newc = k;
			C = newc;
		}

		return data;
	}

	string ModifyInstruction(string instruction)
	{
		string result = instruction;
		while (result.Length != 3)
		{
			result = "0" + instruction;
		}

		return result;
	}

	(int m1, int m2, int m3) GetModes(string instruction, int k)
	{
		int mode1, mode2, mode3;

		if (k == 3)
		{
			mode1 = int.Parse(instruction[k].ToString());
			mode2 = int.Parse(instruction[k - 1].ToString());
			mode3 = int.Parse(instruction[k - 2].ToString());
		}
		else if (k == 2)
		{
			mode1 = instruction[k];
			mode2 = instruction[k - 1];
			mode3 = 0;
		}
		else
		{
			mode1 = instruction[k];
			mode2 = 0;
			mode3 = 0;
		}

		return (mode1, mode2, mode3);
	}

	private int DeriveOpCode(string instruction)
	{
		int k = instruction.Length - 1;

		string b = instruction[k].ToString();
		string a = instruction[k - 1].ToString();
		string result = a + b;
		return int.Parse(result);
	}
}




