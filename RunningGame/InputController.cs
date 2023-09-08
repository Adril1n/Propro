using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using RunningGame.Entities;

namespace RunningGame
{
	public class InputController
	{
        private KeyboardState _prevKeyboardState;
        private Player _player;

        private bool IsBlocked;

        public InputController(Player player) { _player = player; }

		public void ProcessControls(GameTime gameTime)
		{
			KeyboardState keyboardState = Keyboard.GetState();

			if (!GetIsBlocked())
			{
                bool isJumpKeyPressed = keyboardState.IsKeyDown(Keys.Up)  || keyboardState.IsKeyDown(Keys.Space);
                bool wasJumpKeyPressed = _prevKeyboardState.IsKeyDown(Keys.Up) || _prevKeyboardState.IsKeyDown(Keys.Space);

				if (isJumpKeyPressed && !wasJumpKeyPressed)
				{
					_player.Jump();
					//Console.WriteLine("JUmped");
				}
            }

			_prevKeyboardState = keyboardState;
			IsBlocked = false;
		}

		public bool GetIsBlocked()
		{
			return IsBlocked;
		}

		public void BlockInput()
		{
			IsBlocked = true;
		}
	}
}

