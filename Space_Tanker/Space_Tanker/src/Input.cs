using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
#endif

namespace Space_Tanker.src
{
    internal class Input
    {
        internal int mouseX, mouseY, onScreenMouseX, onScreenMouseY, last0Touch, last1Touch, touchInterval, totalDx, totalDy, dx, dy;
        internal bool mouse0, click0, click1;
        private bool startMooving;

        MouseState mouseState, lastMouseState;
        private int lastMouseX;
        private int lastMouseY;
        public bool backButtonPressed;
        public bool backButtonClick;
        public bool lastBackButtonPressed;
        internal int backButtonPressedCount;

#if WINDOWS_PHONE
        private int maximumTouchCount;
        TouchPanelCapabilities touchPanelCapabilities;
        TouchCollection touchCollection;
#endif

        internal void setup()
        {
            touchInterval = Game1.config.touchInterval;

#if WINDOWS_PHONE
            touchPanelCapabilities = TouchPanel.GetCapabilities();
            if (touchPanelCapabilities.IsConnected)
            {
                maximumTouchCount = touchPanelCapabilities.MaximumTouchCount;
            }
#endif
        }

        internal void update()
        {
            if (mouse0 || click0 || backButtonPressed)
            {
                Game1.needToDraw = true;
            }

            lastBackButtonPressed = backButtonPressed;

#if WINDOWS
            backButtonPressed = Keyboard.GetState().IsKeyDown(Keys.Escape);
#endif
#if WINDOWS_PHONE
            backButtonPressed = GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed;
#endif

            backButtonClick = !backButtonPressed && lastBackButtonPressed;

            switch (Game1.state)
            {
#if WINDOWS_PHONE
                //case Game1.states.mission:
                #region
                case Game1.states.mission:
                    {
                        touchCollection = TouchPanel.GetState();

                        switch (touchCollection.Count)
                        {
                            case 0:
                                {
                                    click0 = false;
                                    last0Touch = Game1.frameCount;
                                }
                                break;
                            case 1:
                                {
                                    click1 = false;
                                    last1Touch = Game1.frameCount;
                                    getTouchPosition();
                                    mouse0 = ((touchCollection[0].State == TouchLocationState.Pressed) || (touchCollection[0].State == TouchLocationState.Moved));

                                    if (touchCollection[0].State == TouchLocationState.Released)
                                    {
                                        click0 = (Game1.frameCount - last0Touch < Game1.config.touchInterval);
                                    }
                                    else
                                    {
                                        click0 = (touchCollection[0].State == TouchLocationState.Released);
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (Game1.state == Game1.states.mission)
                                    {
                                        if (touchCollection[0].State == TouchLocationState.Released)
                                        {
                                            click1 = true;
                                            getTouchPosition();
                                            mouse0 = false;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                #endregion
#endif
                default:
                    {
                        lastMouseState = mouseState;
                        mouseState = Mouse.GetState();

                        click0 = ((lastMouseState.LeftButton == ButtonState.Pressed) && (mouseState.LeftButton == ButtonState.Released));

                        if (click0)
                        {
                            click0 = (Game1.frameCount - last0Touch < Game1.config.touchInterval);
                            mouse0 = false;
                            getMousePosition();
                        }
                        else
                        {
                            mouse0 = mouseState.LeftButton == ButtonState.Pressed;

                            if (mouse0)
                            {
                                lastMouseX = mouseX;
                                lastMouseY = mouseY;
                                getMousePosition();

                                if (startMooving)
                                {
                                    startMooving = false;
                                }
                                else
                                {
                                    dx = mouseX - lastMouseX;
                                    dy = mouseY - lastMouseY;
                                    totalDx = totalDx + dx;
                                    totalDy = totalDy + dy;
                                }
                            }
                            else
                            {
                                totalDx = 0;
                                totalDy = 0;
                                dx = 0;
                                dy = 0;
                                last0Touch = Game1.frameCount;
                                startMooving = true;
                            }
                        }
                    }
                    break;
            }
        }

#if WINDOWS_PHONE
        private void getTouchPosition()
        {
            mouseX = (int)(((touchCollection[0].Position.X / Game1.display.scale) - Game1.display.displayWidthOver2 + Game1.display.translateX) - Game1.matrix.X + Game1.display.displayWidthOver2);
            mouseY = (int)(-((touchCollection[0].Position.Y / Game1.display.scale) - Game1.display.displayHeightOver2 + Game1.display.translateY) - Game1.matrix.Y - Game1.display.displayHeightOver2);

            onScreenMouseX = (int)((touchCollection[0].Position.X / Game1.display.scale) + Game1.display.translateX);
            onScreenMouseY = (int)((touchCollection[0].Position.Y / Game1.display.scale) + Game1.display.translateY);
        }
#endif

        private void getMousePosition()
        {
            mouseX = (int)(((mouseState.X / Game1.display.scale) - Game1.display.displayWidthOver2 + Game1.display.translateX) - Game1.matrix.X + Game1.display.displayWidthOver2);
            mouseY = (int)(-((mouseState.Y / Game1.display.scale) - Game1.display.displayHeightOver2 + Game1.display.translateY) - Game1.matrix.Y - Game1.display.displayHeightOver2);

            onScreenMouseX = (int)((mouseState.X / Game1.display.scale) + Game1.display.translateX);
            onScreenMouseY = (int)((mouseState.Y / Game1.display.scale) + Game1.display.translateY);
        }
    }
}
