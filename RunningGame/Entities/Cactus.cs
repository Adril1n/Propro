using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using RunningGame.Graphics;

namespace RunningGame.Entities
{
	public class Cactus : Obstacle
	{
		public enum CactusSize
		{
			Small = 0,
			//Medium = 1,
			Large = 1
		}

        private const int SMALL_CACTUS_TEXTURE_X = 100;
        private const int SMALL_CACTUS_TEXTURE_Y = 0;
        private const int SMALL_CACTUS_SPRITE_WIDTH = 40;
        private const int SMALL_CACTUS_SPRITE_HEIGTH = 50;

        private const int LARGE_CACTUS_TEXTURE_X = 150;
        private const int LARGE_CACTUS_TEXTURE_Y = 0;
        private const int LARGE_CACTUS_SPRITE_WIDTH = 60;
        private const int LARGE_CACTUS_SPRITE_HEIGTH = 56;

        public CactusSize Size { get; }

        public Sprite Sprite { get; private set; }


        public override Rectangle CollisionBox
		{
			get
			{
				Rectangle box = new Rectangle((int)Math.Round(Position.X), (int)Math.Round(Position.Y), Sprite.Width, Sprite.Height);

				box.Inflate(-Convert.ToInt32(Sprite.Width*0.3), -Convert.ToInt32(Sprite.Height * 0.2));

				return box;
			}
		}

		public Cactus(Texture2D spriteSheet, CactusSize size, Player player, Vector2 position) : base(player, position)
		{
			Size = size;
			Sprite = CreateSprite(spriteSheet);
		}




		private Sprite CreateSprite(Texture2D spriteSheet)
		{
			Sprite sprite = null;

			int spriteWidth = 0;
            int spriteHeight = 0;
            int cactusX = 0;
            int cactusY = 0;

			if (Size == CactusSize.Small)
			{
				spriteWidth = SMALL_CACTUS_SPRITE_WIDTH;
                spriteHeight = SMALL_CACTUS_SPRITE_HEIGTH;

                cactusX = SMALL_CACTUS_TEXTURE_X;
                cactusY = SMALL_CACTUS_TEXTURE_Y;
            }

			else if (Size == CactusSize.Large)
			{
                spriteWidth = LARGE_CACTUS_SPRITE_WIDTH;
                spriteHeight = LARGE_CACTUS_SPRITE_HEIGTH;

                cactusX = LARGE_CACTUS_TEXTURE_X;
                cactusY = LARGE_CACTUS_TEXTURE_Y;
            }

			sprite = new Sprite(spriteSheet, cactusX, cactusY, spriteWidth, spriteHeight);

			sprite.BaseColor = Color.Green;

			return sprite;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
			Sprite.Draw(spriteBatch, Position);
        }
    }
}

