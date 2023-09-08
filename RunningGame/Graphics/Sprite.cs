using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RunningGame.Graphics
{
	public class Sprite
	{
		public Texture2D Texture { get; set; }

		public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public Color BaseColor { get; set; } = Color.White;

        public Sprite(Texture2D texture, int x, int y, int width, int height)
		{
			Texture = texture;
			X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {
            spriteBatch.Draw(Texture, pos, new Rectangle(X, Y, Width, Height), BaseColor);
        }
	}
}

