using System;

namespace RunningGame
{
	public static class Program
	{
		static void Main()
		{
			using (var game = new GameInstance())
				game.Run();
		}
	}
}
