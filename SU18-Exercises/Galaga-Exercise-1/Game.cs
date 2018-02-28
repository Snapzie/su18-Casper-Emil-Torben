using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    public class Game {
        private Window win;
        private Entity player;

        public Game() {
// look at the Window.cs file for possible constructors.
// We recommend using 500 × 500 as window dimensions,
// which is most easily done using a predefined aspect ratio.
            win = new Window("Cool Game", 500, 500);

            player = new Entity(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), 
                new Image(Path.Combine("Assets", "Images", "Player.png")));
        }

        public void GameLoop() {
            while (win.IsRunning()) {
                win.PollEvents();
                win.Clear();
                win.SwapBuffers();
                
                player.RenderEntity();
            }
        }
    }
}