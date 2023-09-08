using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RunningGame.Entities
{
	public abstract class Obstacle : IEntity, ICollisionEntity
	{
		private Player _player;

		public abstract Rectangle CollisionBox { get; }

		public int DrawOrder { get; set; }

		public Vector2 Position { get; protected set; }

		protected Obstacle(Player player, Vector2 position)
		{
			_player = player;
			Position = position;
		}

		public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

		public virtual void Update(GameTime gameTime)
		{
			float new_x = Position.X - _player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			Position = new Vector2(new_x, Position.Y);

			CheckCollisionWithPlayer();
		}

		private void CheckCollisionWithPlayer()
		{
			Rectangle self = CollisionBox;
            Rectangle other = _player.CollisionBox;

			if (self.Intersects(other))
			{
				_player.Die();
			}
        }
	}
}

