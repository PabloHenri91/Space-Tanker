using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Tanker.src
{
    internal class Display
    {
        internal int displayWidth;
        internal int displayHeight;
        internal int displayWidthOver2;
        internal int displayHeightOver2;
        internal Matrix camera;
        internal float scale;
        internal float translateY;
        internal float translateX;

        internal Viewport screenViewport;
        internal Viewport centerViewport;

        internal void setUpDisplay(GraphicsDeviceManager graphicsDeviceManager)
        {
            //Resolução virtual
            displayWidth = 800;
            displayHeight = 480;
            displayWidthOver2 = displayWidth / 2;
            displayHeightOver2 = displayHeight / 2;

#if WINDOWS
            graphicsDeviceManager.IsFullScreen = true;

            if (graphicsDeviceManager.IsFullScreen)
            {
                if (graphicsDeviceManager.GraphicsDevice.DisplayMode.Width > graphicsDeviceManager.GraphicsDevice.DisplayMode.Height)
                {
                    graphicsDeviceManager.PreferredBackBufferWidth = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
                    graphicsDeviceManager.PreferredBackBufferHeight = graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;
                }
                else
                {
                    graphicsDeviceManager.PreferredBackBufferWidth = graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;
                    graphicsDeviceManager.PreferredBackBufferHeight = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
                }
            }
            else
            {
                graphicsDeviceManager.PreferredBackBufferWidth = displayWidth;
                graphicsDeviceManager.PreferredBackBufferHeight = displayHeight;
            }
            
#endif
#if WINDOWS_PHONE
            graphicsDeviceManager.IsFullScreen = true;

            if (graphicsDeviceManager.GraphicsDevice.DisplayMode.Width > graphicsDeviceManager.GraphicsDevice.DisplayMode.Height)
            {
                graphicsDeviceManager.PreferredBackBufferWidth = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
                graphicsDeviceManager.PreferredBackBufferHeight = graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;
            }
            else
            {
                graphicsDeviceManager.PreferredBackBufferWidth = graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;
                graphicsDeviceManager.PreferredBackBufferHeight = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
            }
#endif

            graphicsDeviceManager.ApplyChanges();

            screenViewport = graphicsDeviceManager.GraphicsDevice.Viewport;
            centerViewport = screenViewport;

            float scaleX = (float)graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth / displayWidth;
            float scaleY = (float)graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight / displayHeight;
            scale = Math.Min(scaleX, scaleY);

            translateX = ((float)graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth - (displayWidth * scale)) / 2f;
            translateY = ((float)graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight - (displayHeight * scale)) / 2f;

            camera = Matrix.CreateScale(scale, scale, 1);

            centerViewport.X = (int)translateX;
            centerViewport.Width = (int)(centerViewport.Width - translateX - translateX);
            centerViewport.Y = (int)translateY;
            centerViewport.Height = (int)(centerViewport.Height - translateY - translateY);

            graphicsDeviceManager.GraphicsDevice.ScissorRectangle = centerViewport.Bounds;
            graphicsDeviceManager.GraphicsDevice.Viewport = centerViewport;

            translateX = -translateX / scale;
            translateY = -translateY / scale;
        }
    }
}