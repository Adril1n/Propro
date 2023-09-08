using System;

namespace top20
{
	class Program
	{
		static void Main()
		{
			string[] userTopList;
			string[] topSongs = File.ReadAllLines("top.txt");
			string fileName = "list.txt";
			int listLength = 5;
			string emptyElement = "No Song";

			if (File.Exists(fileName))
			{
				userTopList = File.ReadAllLines(fileName);
			}
			else
			{
				userTopList = new string[listLength];
				for (int i = 0; i < listLength; i++)
				{
					userTopList[i] = emptyElement;
				}
				File.WriteAllLines(fileName, userTopList);
			}

			string choice = "";
			while (choice != "5")
			{
				Console.WriteLine("Choose what to do");
				Console.WriteLine("1. Add new songs");
				Console.WriteLine("2. Change song");
				Console.WriteLine("3. Show top songs");
				Console.WriteLine("4. Search for song in top songs");
				Console.WriteLine("5. Quit program.");
				choice = Console.ReadLine();

				Console.WriteLine(choice);

				switch (choice)
				{
					case "1":
						Console.WriteLine("Write your song names");
						for (int i = 0; i < listLength; i++)
						{
							Console.WriteLine("Name");
							string songName = Console.ReadLine();
							userTopList[i] = songName;
						}
						File.WriteAllLines(fileName, userTopList);
						break;

					case "2":
						Console.WriteLine("Song to Change");

                        string songChange = Console.ReadLine();
						int index = Array.IndexOf(userTopList, songChange);
						if (index != -1)
						{
							Console.WriteLine("New Song");
							string newSong = Console.ReadLine();
							userTopList[index] = newSong;
						}
						break;

					case "3":
						Console.WriteLine("\nTop songs are");

						for (int i = 0; i < topSongs.Length; i++)
						{
							Console.WriteLine($"{i}: {topSongs[i]}");
						}
						break;

					case "4":
						Console.WriteLine("\nSearch key");
						string key = Console.ReadLine();
						for (int i = 0; i < listLength; i++)
						{
							if (userTopList[i].ToLower().Contains(key.ToLower()))
							{
								string ranking = "";
								int top_ranking = Array.IndexOf(topSongs, userTopList[i]);
								if (top_ranking != -1)
								{
									ranking = $", Number: {top_ranking + 1}";
								}
								Console.WriteLine($"{userTopList[i]}{ranking}");
							}
						}
						break;

					default:
						Console.WriteLine("Not possible");
						break;
				}
				Console.WriteLine();
				File.WriteAllLines(fileName, userTopList);
			}
		}
	}
}