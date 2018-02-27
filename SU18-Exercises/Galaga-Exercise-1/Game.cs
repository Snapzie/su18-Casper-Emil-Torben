using DIKUArcade;

namespace Galaga_Exercise_1 {
    public class Game {
        private Window win;

        public Game() {
// look at the Window.cs file for possible constructors.
// We recommend using 500 × 500 as window dimensions,
// which is most easily done using a predefined aspect ratio.
            win = new Window("Cool Game", 500, 500);
        }

        public void GameLoop() {
            while (win.IsRunning()) {
                win.PollEvents();
                win.Clear();
                win.SwapBuffers();
            }
        }
    }
}