using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RunningGame.Graphics;

using System;

namespace RunningGame.Entities
{
	public class Player : IEntity, ICollisionEntity
	{
		public const int PLAYER_SPRITE_SPRITE_SHEET_POS_X = 1;
		public const int PLAYER_SPRITE_SPRITE_SHEET_POS_Y = 1;
		public const int PLAYER_SPRITE_WIDTH = 42;
		public const int PLAYER_SPRITE_HEIGHT = 42;

		private const float INITIAL_VERTICAL_VELOCITY_JUMP = -500f;
		private const float GRAVITY_MAGNITUDE = 1200f;


        private Sprite _runSprite;

		private Random _random;

		public const float INITIAL_SPEED = 150f;
		public const float MAX_SPEED = 1000f;
		private const float ADD_SPEED_P_SECOND = 3f;

        public event EventHandler JumpComplete;
        public event EventHandler Died;

		public PlayerState State { get; private set; }

		public Vector2 Position { get; set; }

		public float Speed { get; private set; }

		public int DrawOrder { get; set; }

		public bool IsAlive { get; private set; }

		private float _verticalVelocity;

		private float _startY;


        public Rectangle CollisionBox
		{
			get
			{
				Rectangle box = new((int)Math.Round(Position.X), (int)Math.Round(Position.Y), PLAYER_SPRITE_WIDTH, PLAYER_SPRITE_HEIGHT);

				// crouch should reduce box.Y and box.Height by a const
				// box.Y += CONST
				// box.Height -= CONST

				return box;
			}
		}

		public Player(Texture2D spriteSheet, Vector2 pos)
		{
			Position = pos;
			State = PlayerState.Idle;

			_random = new Random();

			_runSprite = new Sprite(spriteSheet, PLAYER_SPRITE_SPRITE_SHEET_POS_X, PLAYER_SPRITE_SPRITE_SHEET_POS_Y, PLAYER_SPRITE_WIDTH, PLAYER_SPRITE_HEIGHT);
			_runSprite.BaseColor = Color.BlueViolet;

			_startY = Position.Y;

            IsAlive = true;
		}



		public void Initialize()
		{
			Speed = INITIAL_SPEED;
			State = PlayerState.Running;
			//IsAlive = true;
			//Position = new Vector2(Position.X, P);
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (IsAlive)
			{
				_runSprite.Draw(spriteBatch, Position);
			}
			else
			{
				//Console.WriteLine("DEAD");
			}
		}

		public void Update(GameTime gameTime)
		{
			if (State == PlayerState.Jumping || State == PlayerState.Falling)
			{
				Position = new Vector2(Position.X, Position.Y + _verticalVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
				_verticalVelocity += GRAVITY_MAGNITUDE * (float)gameTime.ElapsedGameTime.TotalSeconds;

				if (_verticalVelocity >= 0)
				{
					State = PlayerState.Falling;
				}

				if (Position.Y >= _startY)
				{
					Position = new Vector2(Position.X, _startY);
					_verticalVelocity = 0f;
					State = PlayerState.Running;

					OnJumpComplete();
				}
			}


			if (State != PlayerState.Idle)
			{
				Speed = Math.Min(Speed + ADD_SPEED_P_SECOND * (float)gameTime.ElapsedGameTime.TotalSeconds, MAX_SPEED);
			}
			else
			{
				if (Speed > MAX_SPEED)
				{
					Speed = MAX_SPEED;
				}
			}
			
		}

		public bool Jump()
		{
			if (State == PlayerState.Jumping || State == PlayerState.Falling) { return false; }

			State = PlayerState.Jumping;
			_verticalVelocity = INITIAL_VERTICAL_VELOCITY_JUMP;

			return true;
        }


		protected virtual void OnJumpComplete()
		{
            EventHandler handler = JumpComplete;
            handler?.Invoke(this, EventArgs.Empty);
        }


        protected virtual void OnDied()
		{
			EventHandler handler = Died;
			handler?.Invoke(this, EventArgs.Empty);
		}

		public bool Die()
		{
			if (!IsAlive) { return false; }

			State = PlayerState.Idle;
			Speed = 0;

			IsAlive = false;

			OnDied();

			return true;
		}
	}
}

