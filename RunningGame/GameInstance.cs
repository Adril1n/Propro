using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Diagnostics;

using RunningGame.Entities;
using RunningGame.Graphics;

namespace RunningGame
{
    public class GameInstance : Game
    {
        public const string GAME_TITLE = "Run!";

        private const string SPRITE_SHEET_FILE_NAME = "sprite_sheet";

        public const int WINDOW_WIDTH = 600;
        public const int WINDOW_HEIGHT = 150;

        public const int PLAYER_START_X = 1;
        public const int PLAYER_START_Y = WINDOW_HEIGHT-Player.PLAYER_SPRITE_HEIGHT;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _spriteSheetTexture;

        private InputController _inputController;   

        private Player _player;

        private EntityManager _entityManager;
        private ObstacleManager _obstacleManager;

        private KeyboardState _prevKeyboardState;

        public GameState State { get; private set; }


        public GameInstance()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

             _entityManager = new EntityManager();
            _ = GameState.Initial;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.Title = GAME_TITLE;

            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spriteSheetTexture = Content.Load<Texture2D>(SPRITE_SHEET_FILE_NAME);

            _player = new Player(_spriteSheetTexture, new Vector2(PLAYER_START_X, PLAYER_START_Y));
            _player.DrawOrder = 500;
            _player.Died += playerDied;

            _inputController = new InputController(_player);

            _obstacleManager = new ObstacleManager(_entityManager, _player, _spriteSheetTexture);

            _entityManager.AddEntity(_player);
            _entityManager.AddEntity(_obstacleManager);
        }

        private void playerDied(object sender, EventArgs e)
        {
            State = GameState.Over;
            _obstacleManager.IsEnabled = false;
            // Game over 

            Console.WriteLine("Game Over!");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();

            if (State == GameState.Running)
            {
                _inputController.ProcessControls(gameTime);
            }

            else if (State == GameState.Initial)
            {
                bool isStartKeyPressed = keyboardState.IsKeyDown(Keys.Space);
                bool wasStartKeyPressed = _prevKeyboardState.IsKeyDown(Keys.Space);

                if (isStartKeyPressed && !wasStartKeyPressed)
                {
                    StartGame();
                }
            }

            _entityManager.Update(gameTime);

            _prevKeyboardState = keyboardState;
        }

        private bool StartGame()
        {
            if (State != GameState.Initial) { return false; }

            // could add other stuff here like setting "_scoreBoard.Score = 0;"
            State = GameState.Running;
            _player.Initialize();

            _obstacleManager.IsEnabled = true;

            return true;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            _entityManager.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}