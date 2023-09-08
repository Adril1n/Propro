using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RunningGame.Entities
{
	public interface IEntity
	{
		int DrawOrder { get; }

		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch, GameTime gameTime);
	}
}

