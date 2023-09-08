class Program
{
	static void Main(string[] args)
	{
		Random rng = new();

		Dictionary<string, int> KeyCorrelation = new();
		KeyCorrelation.Add("a", -1);
		KeyCorrelation.Add("d", 1);

		int maxHealth = 3;
		int height = 5;
		int width = 5;

		int round = 1;
		string[,] map = new string[width, height];
		List<List<int>> bombs = new();

		string highscore_file_name = "highscore.txt";

		if (!File.Exists(highscore_file_name))
		{
			File.Create(highscore_file_name);			
		}

		int high_score = 0;

		try
		{
			high_score = Convert.ToInt32(File.ReadAllLines(highscore_file_name)[0]);
		}
		catch
		{
			File.WriteAllLines(highscore_file_name, new string[] { "0" });
		}
		
		void InitGame()
		{
			round = 1;

			map = new string[width, height];
			bombs = new List<List<int>>();

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					map[x, y] = "   ";
				}
			}
		}

		void UpdateBombs()
		{
			for (int i = 0; i < bombs.Count; i++) {bombs[i][1]++;}
			for (int i = 0; i < bombs.Count; i++)
			{
				if (bombs[i][1] >= height)
				{
					bombs.RemoveAt(i);
				}
			}

			int bomb_x = rng.Next(0, width);
			List<int> item = new() { bomb_x, 0 };
			bombs.Add(item);
		}

		int CalculateDamage(int p_x)
		{
			for (int i = 0; i < bombs.Count; i++)
			{
				if (bombs[i][0] == p_x && bombs[i][1] == height - 1)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					return -1;
				}
			}
			Console.ResetColor();
			return 0;
		}

		void WriteMap(int p_x)
		{
			string[,] new_map = map.Clone() as string[,];

			for (int x = 0; x < width; x++)
			{
				Console.Write(" - ");
			}
			Console.WriteLine();

			for (int i = 0; i < bombs.Count; i++)
			{
				new_map[bombs[i][0], bombs[i][1]] = " 0 ";
			}

			new_map[p_x, height - 1] = " X ";

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					string a = new_map[x, y];
					//if (a == " 0 ")
					//{
					//	Console.BackgroundColor = ConsoleColor.DarkRed;
					//	Console.Write(a);
					//	Console.ResetColor();
					//}
					//else
					//{
                    Console.Write(a);
                    //}
				}
				Console.WriteLine();
			}

			for (int x = 0; x < width; x++)
			{
				Console.Write(" - ");
			}

		}

		double GetModifier(int since_hit)
		{
			return Math.Pow(Convert.ToDouble(1.1F), Convert.ToDouble(since_hit / 4));
		}

		void Start()
		{
			Console.WriteLine("The Game Begins!");

			int health = maxHealth;
			double score = 0d;
			int since_hit = 0;
			double modifier;

			int player_x = Convert.ToInt32(width / 2);

			while (health > 0)
			{
				UpdateBombs();

				int damage = CalculateDamage(player_x);
				health += damage;
				if (damage < 0) {since_hit = 0;}
				else {since_hit++;}
				modifier = GetModifier(since_hit);

				Console.Clear();

				Console.WriteLine($"Round {round}");
				WriteMap(player_x);
				Console.WriteLine($"\nCurrent Health: {health}");
				Console.WriteLine($"Score: {Convert.ToInt32(score)}");
				Console.WriteLine($"Since Hit: {since_hit} (Mod: {Math.Round(modifier, 2)})");
				if (health <= 0) { break; }

				Console.WriteLine("Move (a == \"left\", d == \"right\")");
				Console.WriteLine("Press 'q' to quit.");

				/// Can't stand still
				//string dir = "";

				//while (!KeyCorrelation.ContainsKey(dir)) {dir = Console.ReadLine();}
				//player_x = Math.Max(Math.Min(player_x + KeyCorrelation[dir], width - 1), 0);

				/// Can stand still
				string dir = Console.ReadLine();

				try {player_x = Math.Max(Math.Min(player_x + KeyCorrelation[dir.ToLower()], width - 1), 0);}
				catch
				{
					if (dir.ToLower() == "q")
					{
						break;
					}
				}


				round++;
				score += Convert.ToInt32(modifier);

				if (score > high_score)
				{
					File.WriteAllLines(highscore_file_name, new string[] {Convert.ToString(score)});
				}
			}
            Console.ResetColor();
            Console.Write($"\nGame Over!\nRounds Survived: {round}\nFinal Score: {score}");
            if (score > high_score) { Console.Write(" (New Highscore!)"); }
			else { Console.Write($"\nHighscore: {high_score}\n"); }
            Console.ReadKey();

			Console.Clear();
		}

		void Configure(string choice)
		{
			switch (choice)
			{
				case "1":
					Console.WriteLine($"New max health [{maxHealth}] ('x' -> 'reset'):");
					string new_health = Console.ReadLine();

					try
					{
						maxHealth = Convert.ToInt32(new_health);
					}
					catch
					{
						if (new_health.ToLower() == "x")
						{
							maxHealth = 3;
						}
					}

					break;

				case "2":
					Console.WriteLine($"New map dimensions [{width}x{height}]:");
					string dim_string = Console.ReadLine();
					string[] dims = dim_string.Split('x');

					try
					{
						width = Convert.ToInt32(dims[0]);
						height = Convert.ToInt32(dims[1]);
					}
					catch { }

					break;

				case "3":
					File.WriteAllLines(highscore_file_name, new string[] { "0" });

					break;

				case "4":
					break;

				default:
					break;
			}
		}


		string choice = "0";
		while (choice != "3")
		{
			Console.WriteLine("Choose what to do.");
			Console.WriteLine("1. Play");
			Console.WriteLine("2. Config");
			Console.WriteLine("3. Quit");
			choice = Console.ReadLine();
			Console.WriteLine();

			switch (choice)
			{
				case "1":
					InitGame();
					Start();

					break;

				case "2":
					Console.Clear();

					Console.WriteLine("Choose what to configure");
					Console.WriteLine("1. Max Health");
					Console.WriteLine("2. Map Dimensions");
					Console.WriteLine("3. Reset Highscore");
					Console.WriteLine("4. Return");
					string config_choice = Console.ReadLine();
					Console.WriteLine();

					Configure(config_choice);

					Console.Clear();
					break;

				case "3":
					Console.Clear();
					break;

				default:
					Console.WriteLine("Invalid Input");
					break;
			}
		}
	}
}