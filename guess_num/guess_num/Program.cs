Random rng = new Random();

Console.WriteLine("Guess the Number Game.\n");

string choice = "0";
while (choice != "3")
{
	Console.WriteLine("What do you want to do?");
	Console.WriteLine("1. Start the Guessing Game");
	Console.WriteLine("2. Config");
	Console.WriteLine("3. Quit");
	choice = Console.ReadLine();
	Console.WriteLine();

	int MaxNum = 10;

	switch (choice)
	{
		case "1":
			int player_n = 0;

			Console.WriteLine("Computer Choosing a Number...");
			Thread.Sleep(500);
			int rand_n = rng.Next(1, MaxNum);

			while (player_n != rand_n)
			{
				Console.WriteLine($"Choose a number between 1-{MaxNum}");
				int ch_n = 0;

				try {ch_n = Convert.ToUInt16(Console.ReadLine());}

				catch
				{
					Console.WriteLine("Not a number");
					continue;
				}

				player_n = ch_n;
				if (player_n < rand_n) {Console.WriteLine($"Your guess ({player_n}) was to low.");}
				else if (player_n > rand_n) {Console.WriteLine($"Your guess ({player_n}) was to high.");}
			}

			Console.WriteLine($"You guessed right, the number was: {rand_n}\n\n");
			break;

        case "2":
			string config_choice = "0";
			while (config_choice != "2")
			{
				Console.WriteLine("Choose a setting to configure");
                Console.WriteLine("1. Change max number");
                Console.WriteLine("2. Return");
				config_choice = Console.ReadLine();

				switch (config_choice)
				{
					case "1":
						Console.WriteLine($"Write a new max number (current: {MaxNum})");
						int new_n = Convert.ToInt32(Console.ReadLine());
						MaxNum = new_n;

						break;

					case "2":
						break;

					default:
						Console.WriteLine("Invalid Choice");
						break;
				}
			}

            break;

        case "3":
			break;

		default:
			Console.WriteLine("Invalid Choice");
			break;
	}

}