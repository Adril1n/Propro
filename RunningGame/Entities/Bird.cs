using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using RunningGame.Graphics;

namespace RunningGame.Entities
{
	public class Bird : Obstacle
	{
        private const int DEFAULT_CACTUS_TEXTURE_X = 220;
        private const int DEFAULT_CACTUS_TEXTURE_Y = 0;
        private const int DEFAULT_CACTUS_SPRITE_WIDTH = 35;
        private const int DEFAULT_CACTUS_SPRITE_HEIGTH = 35;

        private Sprite _sprite;

        private Player _player;

        public override Rectangle CollisionBox
        {
            get
            {
                Rectangle box = new Rectangle((int)Math.Round(Position.X), (int)Math.Round(Position.Y), _sprite.Width, _sprite.Height);
                box.Inflate(-Convert.ToInt32(_sprite.Width * 0.1), -Convert.ToInt32(_sprite.Height * 0.1));
                return box;
            }
        }

        public Bird(Player player, Vector2 position, Texture2D spriteSheet) : base(player, position)
		{
            // can add animation here
            _player = player;
            _sprite = new Sprite(spriteSheet, DEFAULT_CACTUS_TEXTURE_X, DEFAULT_CACTUS_TEXTURE_Y, DEFAULT_CACTUS_SPRITE_WIDTH, DEFAULT_CACTUS_SPRITE_HEIGTH);
            _sprite.BaseColor = Color.OrangeRed;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, Position);
        }
    }
}

