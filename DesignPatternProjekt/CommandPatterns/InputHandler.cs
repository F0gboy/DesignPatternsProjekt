using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternProjekt.CommandPatterns
{
    class InputHandler
    {
        private static InputHandler instance;
        public static InputHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputHandler();
                }
                return instance;
            }
        }
        private InputHandler()
        {

        }

        private Dictionary<Keys, ICommand> keybinds = new Dictionary<Keys, ICommand>();

        public void AddCommand(Keys inputKey, ICommand command)
        {
            keybinds.Add(inputKey, command);
        }

        public void Execute()
        {
            KeyboardState keyState = Keyboard.GetState();

            foreach (var pressedKey in keyState.GetPressedKeys())
            {
                if (keybinds.TryGetValue(pressedKey, out ICommand cmd))
                {
                    cmd.Execute();
                }
            }
        }
    }
}
