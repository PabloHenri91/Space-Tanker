using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.Dynamics;
using System.IO;
using System.Threading;

namespace Space_Tanker.src
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //FPS Counter
#if DEBUG
        private float timeSinceLastUpdate;
        private int updateCount;
        private int ups;

        private float timeSinceLastDraw;
        private int drawCount;
        private int dps;
#endif
        //Tela
        internal static Display display;
        GraphicsDeviceManager graphicsDeviceManager;
        public static SpriteBatch spriteBatch;

        //Meu gerenciador de conteudo adicional.
        //Vai conter o conteudo do jogo, menos da tela de load.
        internal static ContentManager myContentManager;

        //Estados
        internal enum states { mission, loading, mainMenu, hangar, quit };
        internal static states state;
        internal static states nextState;

        private MainMenu mainMenu;
        private Hangar hangar;
        internal static Mission mission;

        //Matriz
        internal static Vector2 matrix;

        //auxiliares
        internal static LoadScreen loadScreen;
        internal bool loadScreenLoaded;
        private bool contentIsEmpty;
        internal static Texture2D voidTexture;
        internal static MemoryCard memoryCard;
        internal static Config config;
        internal static Enemies enemies;
        internal static Players players;

        //Fontes
        internal static SpriteFont Verdana20;
        internal static SpriteFont Verdana12;
        internal static SpriteFont quartzMS20;

        internal static Random random;

        //estradas
        internal static Input input;

        //física
        internal static World world;
        internal static int frameCount;
        internal static RasterizerState scissorTestRasterizerState;

        //Performance
        internal static bool needToDraw;
        private int lastMilliseconds;
        private int elapsedMilliseconds;
        internal static int fps = 30;
        private int frameDuration = (int)(1000f / fps);
        private int last;
        private int elapsed;
        private int oversleep;

        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";//

            // Frame rate is 30 frameDuration by default for Windows Phone.
            IsFixedTimeStep = false;
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000f / fps);
            frameCount = 0;

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromMilliseconds(1000);

            myContentManager = new ContentManager(Services, "Content");
            IsMouseVisible = true;

            random = new Random();
        }

        protected override void Initialize()
        {
            //configurações
            config = new Config();
            enemies = new Enemies();
            players = new Players();
            players.loadPlayers();

            //save, load, new game...
            memoryCard = new MemoryCard();

            scissorTestRasterizerState = new RasterizerState
            {
                ScissorTestEnable = true,
                CullMode = CullMode.None
            };

            //Iniciando o Display
            display = new Display();
            display.setUpDisplay(graphicsDeviceManager);

            //Iniciando enstrada
            input = new Input();
            input.setup();
            

            //definindo estados
            state = states.loading;
            nextState = states.mainMenu;
            loadScreenLoaded = false;
            contentIsEmpty = true;

            //iniciando matriz
            matrix = new Vector2((float)display.displayWidthOver2, -(float)display.displayHeightOver2);

            //iniciando física
            world = new World(Vector2.Zero);

            this.Window.OrientationChanged += onOrientationChanged;

            base.Initialize();
        }

        private void onOrientationChanged(object sender, EventArgs e)
        {
            needToDraw = true;
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Verdana20 = Content.Load<SpriteFont>("Verdana20");
            Verdana12 = Content.Load<SpriteFont>("Verdana12");
            quartzMS20 = Content.Load<SpriteFont>("quartzMS20");
            voidTexture = Content.Load<Texture2D>("void");
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            lastMilliseconds = Environment.TickCount;

            needToDraw = false;
#if DEBUG
            countFPSUpdate(gameTime);
#endif
            input.update();

            if (state == nextState)
            {
                switch (state)
                {
                    case states.mission:
                        mission.doLogic();
                        break;
                    case states.mainMenu:
                        mainMenu.doLogic();
                        break;
                    case states.hangar:
                        hangar.doLogic();
                        break;
                }
            }
            else if (loadScreenLoaded)
            {
                needToDraw = true;
                unloadContent(state);
                switch (nextState)
                {
                    case states.mainMenu:
                        if (mainMenu == null)
                            mainMenu = new MainMenu();
                        if (mainMenu.load())
                            changeState();
                        break;
                    case states.hangar:
                        if (hangar == null)
                            hangar = new Hangar();
                        if (hangar.load())
                            changeState();
                        break;
                    case states.mission:
                        if (mission == null)
                            mission = new Mission();
                        if (mission.load())
                            changeState();
                        break;
                }
            }
            else
            {
                unloadContent(state);
                if (loadScreen == null)
                    loadScreen = new LoadScreen();
                loadScreenLoaded = loadScreen.load(Content);
            }

            if (nextState == states.quit)
                this.Exit();

            base.Update(gameTime);

            frameCount++;
#if DEBUG
            updateCount++;
#endif

            if (state == nextState)
            {
                if (!needToDraw)
                {
#if !DEBUG
                    this.SuppressDraw();
                    Sleep();
#else
                    drawCount--;
#endif
                }
            }
        }

#if DEBUG
        private void countFPSUpdate(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastUpdate += elapsed;

            if (timeSinceLastUpdate > 1)
            {
                ups = updateCount;
                updateCount = 0;
                timeSinceLastUpdate -= 1;
            }
        }
#endif

#if DEBUG
        private void countFPSDraw(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastDraw += elapsed;

            if (timeSinceLastDraw > 1)
            {
                dps = drawCount;
                drawCount = 0;
                timeSinceLastDraw -= 1;
            }
        }
#endif

        private void changeState()
        {
            IsFixedTimeStep = true;
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;

            state = nextState;
            Game1.needToDraw = true;
            contentIsEmpty = false;
            GC.Collect();
        }

        private void unloadContent(states state)
        {
            if (contentIsEmpty == false)
            {
                IsFixedTimeStep = false;
                graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;

                myContentManager.Unload();
                contentIsEmpty = true;

                switch (state)
                {
                    case states.mission:
                        mission = null;
                        break;
                    case states.mainMenu:
                        mainMenu = null;
                        break;
                    case states.hangar:
                        hangar = null;
                        break;
                }
            }

        }

        protected override void Draw(GameTime gameTime)
        {
#if DEBUG
            countFPSDraw(gameTime);
#endif
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, scissorTestRasterizerState, null, display.camera);

            if (state == nextState)
            {
                switch (state)
                {
                    case states.mission:
                        mission.draw();
                        break;
                    case states.mainMenu:
                        mainMenu.draw();
                        break;
                    case states.hangar:
                        hangar.draw();
                        break;
                }
            }
            else if (loadScreenLoaded)
            {
                loadScreen.draw();
            }
#if DEBUG
            spriteBatch.DrawString(Verdana12, "Logic FPS: " + ups, new Vector2(11f, 446f), Color.Black);
            spriteBatch.DrawString(Verdana12, "Logic FPS: " + ups, new Vector2(10f, 445f), Color.White);
            spriteBatch.DrawString(Verdana12, "Draw FPS: " + dps, new Vector2(11f, 461f), Color.Black);
            spriteBatch.DrawString(Verdana12, "Draw FPS: " + dps, new Vector2(10f, 460f), Color.White);
#endif
            spriteBatch.End();
#if DEBUG
            drawCount++;
#endif
            base.Draw(gameTime);

            if (state == nextState)
            {
                Sleep();
            }
        }

        private void Sleep()
        {
            //Resumo. Era pra funcionar assim mas precisou de vários ajustes
            //elapsedMilliseconds = Environment.TickCount - lastMilliseconds;
            //if (elapsedMilliseconds < frameDuration)
            //{
            //    System.Threading.Thread.Sleep(frameDuration - elapsedMilliseconds);
            //}

            elapsedMilliseconds = Environment.TickCount - lastMilliseconds;
            if (elapsedMilliseconds < frameDuration)//Need to sleep?
            {
                if (elapsedMilliseconds + oversleep < frameDuration)
                {
                    last = Environment.TickCount;
                    Thread.Sleep(frameDuration - (elapsedMilliseconds + oversleep));//Sleep
                    elapsed = Environment.TickCount - last;

                    Console.WriteLine(elapsed + elapsedMilliseconds);

                    if (elapsedMilliseconds + elapsed > frameDuration)
                    {
                        oversleep = (((elapsedMilliseconds + elapsed - frameDuration) + oversleep) / 2);
                    }
                }
                else if (oversleep > 0)
                {
                    oversleep--;
                }
            }
        }

        internal static float RandomBetween(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }
    }
}