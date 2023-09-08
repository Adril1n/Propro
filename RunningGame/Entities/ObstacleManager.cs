using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;


namespace RunningGame.Entities
{
	public class ObstacleManager : IEntity
	{
		private static readonly int[] BIRD_Y_POSITIONS = new int[] { 90, 50, 25 }; 


        private const int X_TO_REMOVE_OBSTACLE = -100;
		private const int DEFAULT_DRAW_ORDER = 10;
		private const double TIME_TO_SPAWN = 1.5f;


        private const int SMALL_CACTUS_Y = 50;
		private const int LARGE_CACTUS_Y = 56;

		private int cactusSpawnRate = 65;
        private int birdSpawnRate = 35;

        private Random _random;

        private readonly EntityManager _entityManager;
        private readonly Player _player;

		private Texture2D _spriteSheet;

		public bool IsEnabled { get; set; }
		public bool CanSpawn => IsEnabled; // can add more stuff here later mabybe 

		public int DrawOrder => 0;

		private double timeSinceLastSpawn;

        public ObstacleManager(EntityManager entityManager, Player player, Texture2D spriteSheet)
		{
			_random = new Random();
			_entityManager = entityManager;
			_player = player;
			_spriteSheet = spriteSheet;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime) {}

		public void Update(GameTime gameTime)
		{
			if (!IsEnabled) { return; }

			if (CanSpawn)
			{
				double timeToSpawn = Math.Max(Math.Min(_random.NextDouble()+0.1, 1.1), 0.9) * TIME_TO_SPAWN;

                if (timeSinceLastSpawn >= timeToSpawn)
				{
                    CreateRandomObstacle();
					timeSinceLastSpawn = 0;
                }
				else
				{
                    timeSinceLastSpawn += gameTime.ElapsedGameTime.TotalSeconds;
                }
			}

			foreach (Obstacle obstacle in _entityManager.GetEntitiesOfType<Obstacle>())
			{
				if (obstacle.Position.X < X_TO_REMOVE_OBSTACLE)
				{
					_entityManager.RemoveEntity(obstacle);
				}
			}
		}

		private void CreateRandomObstacle()
		{
			Obstacle obstacle = null;

			//birdSpawnRate = _scoreBoard.Score >= BIRD_MIN_SPAWN_SCORE ? 100-cactusSpawnRate : 0;

			int r_num = _random.Next(0, cactusSpawnRate + birdSpawnRate + 1);

			if (r_num <= cactusSpawnRate)
			{
				Cactus.CactusSize randomSize = (Cactus.CactusSize)_random.Next((int)Cactus.CactusSize.Small, (int)Cactus.CactusSize.Large + 1);

				float cactusY = randomSize == Cactus.CactusSize.Small ? GameInstance.WINDOW_HEIGHT - SMALL_CACTUS_Y : GameInstance.WINDOW_HEIGHT - LARGE_CACTUS_Y;

				obstacle = new Cactus(_spriteSheet, randomSize, _player, new Vector2(GameInstance.WINDOW_WIDTH, cactusY));
			}

			else
			{
				int birdYIndex = _random.Next(0, BIRD_Y_POSITIONS.Length);
				float birdY = BIRD_Y_POSITIONS[birdYIndex];

				obstacle = new Bird(_player, new Vector2(GameInstance.WINDOW_WIDTH, birdY), _spriteSheet);
			}

			obstacle.DrawOrder = DEFAULT_DRAW_ORDER;
			_entityManager.AddEntity(obstacle);
		}
    }
}

