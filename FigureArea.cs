//For adding new figure add element to dictionary figures in app method of FigureArea class and add method with calculation logic to AreaCalc class
using System;
using System.Collections.Generic;
using System.Linq;

//Class Figure fo keeping figure data: name of figure, number of parameters for every figure and function directive
public class Figure
{
	public string figure {get; set; }
	public int num_side {get; set; }
	public	Func<float[], string> func {get; set; } 
}
//Class AreaCalc contents functions for calculating area of figures
public class AreaCalc
{
	public string circle_area(float [] nums)
	{
		//S circle = 2 * pi * r^2
		if (nums[0] < 0)
			return ("Forbidden parameters. Radius can't be a negative value");
		return(Convert.ToString(Math.PI * Math.Pow(nums[0], 2)));
	}
	public string triangle_area(float [] nums)
	{
		//S triangle = sgrt(p * (p - a) * (p - b) * (p - c)), where p = (a + b + c) / 2
		double result;
		double p;

		foreach (var num in nums)
		{
			if (num < 0)
				return ("Forbidden parameters. Triangel's side can't be a negative value");
		}
		p = (nums[0] + nums[1] + nums[2]) / 2;
		result = Math.Sqrt(p * (p - nums[0]) * (p - nums[1]) * (p - nums[2]));
		if (Double.IsNaN(result))
			return ("Forbidden parameters. Triangel with this parameters are not exist");
		if ((Math.Pow(nums[0], 2) == Math.Pow(nums[1], 2) + Math.Pow(nums[2], 2)) || 
		(Math.Pow(nums[1], 2) == Math.Pow(nums[0], 2) + Math.Pow(nums[2], 2)) ||
		(Math.Pow(nums[2], 2) == Math.Pow(nums[1], 2) + Math.Pow(nums[0], 2)))
			return(Convert.ToString(result) + " - right triangle");
		return(Convert.ToString(result));
	}
}
public class FigureArea
{
	//TestWords function checks parameters - trying to convert it to double and checks number of parameters
	static int testWords(string [] words, Figure x)
	{
		List<float> nums = new List<float>();
		try
		{
			foreach(string word in words)
			{
				float num = (float) Convert.ToDouble(word);
				nums.Add(num);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("Sorry, something went wrong");
			Console.WriteLine("Error: " + e);
			while (true)
			{
				Console.WriteLine("Would you like to try another parameters? (y/n): ");
				string ans = Console.ReadLine();
				if (ans == "n")
					return (0);
				if (ans == "y")
					return (1);
				Console.WriteLine("Sorry, but you need to decide!");
			}
		}
		Console.WriteLine("-----------------------------------------");
		Console.WriteLine("Result: " + x.func(nums.ToArray()));
		return (0);
	}
	//parametersRead function read parameters and if figure didn't select trys to detect it
	static int parametersRead(string num, Dictionary<string, Figure> figures)
	{
		Console.Write("Enter sizes (use space between numbers and commas (or dots fo EN localization) for float numbers): ");
		string str = Console.ReadLine();
		string [] words = str.Split(' ');
		words = words.Where(word => !string.IsNullOrEmpty(word)).ToArray();
		if (!string.IsNullOrEmpty(num))
		{
			if (words.Length != figures[num].num_side || words.Length == 0)
			{
				Console.WriteLine("Sorry, but number of arguments is wrong. Right number is " + figures[num].num_side);
				while (true)
				{
					Console.WriteLine("Would you like to try another parameters? (y/n): ");
					string ans = Console.ReadLine();
					if (ans == "n")
						return (0);
					if (ans == "y")
						return (1);
					Console.WriteLine("Sorry, but you need to decide!");
				}
			}
		}
		else
		{
			foreach(var elem in figures)
			{
				if (elem.Value.num_side == words.Length)
				{
					Console.WriteLine("Probably your figure is " + elem.Value.figure);
					var err = testWords(words, elem.Value);
					if (err == 0)
						return (0);
					return(1);
				}
			}
			Console.WriteLine("Sorry, but programm can't define a figure");
			while (true)
			{
				Console.WriteLine("Would you like to try another parameters? (y/n): ");
				string ans = Console.ReadLine();
				if (ans == "n")
					return (0);
				if (ans == "y")
					return (1);
				Console.WriteLine("Sorry, but you need to decide!");
			}
		}
		var err1 = testWords(words, figures[num]);
		if (err1 == 0)
			return (0);
		return(1);
	}
	//figureChoise method check figure in dict or not
	static void figureChoise(string num, Dictionary<string, Figure> figures)
	{
		if (num == "")
		{
			Console.WriteLine("Program will try to detect figure: ");
				while(true)
					if (parametersRead(null, figures) == 0)
						break ;
			return ;
		}
		if (!figures.ContainsKey(num))
		{
			Console.WriteLine("Sorry, figure is not detected. Wrong figure ID, try one more time, please");
			return ;
		}
		Console.WriteLine("Figure detected - " + figures[num].figure);
		while(true)
			if (parametersRead(num, figures) == 0)
				break ;
	}
	public void app()
	{
		Dictionary<string, Figure> figures = new Dictionary<string, Figure>();
		AreaCalc areaCalc = new AreaCalc();
		string num;
		string ans;
		
		//For adding new figure add new element to dictionary figures
		figures.Add("1", new Figure() {figure = "circle", num_side = 1, func = areaCalc.circle_area});
		figures.Add("2", new Figure() {figure = "triangle", num_side = 3, func = areaCalc.triangle_area});
		Console.WriteLine("This program calculate geometric figures area");
		while (true)
		{
			Console.WriteLine("Choose type of figure");
			foreach (var el in figures)
				Console.WriteLine(el.Key + " - " + el.Value.figure);
			Console.WriteLine("0 - exit");
			Console.WriteLine("If you will keep this field empty, than program will try to define it:");
			num = Console.ReadLine();
			if (num == "0")
			{
				Console.WriteLine("Thanks for using! Bye!");
				break;
			}
			figureChoise(num, figures);
			Console.WriteLine("-----------------------------------------");
			while (true)
			{
				Console.WriteLine("Do you want to calculate something else? (y/n): ");
				ans = Console.ReadLine();
				if (ans == "n")
				{
					Console.WriteLine("Thanks for using! Bye!");
					return ;
				}
				if (ans == "y")
					break ;
				Console.WriteLine("Sorry, but you need to decide!");
			}
		}
	}
}
