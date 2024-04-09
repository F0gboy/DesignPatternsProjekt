using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternProjekt
{
    class InputHandler
    {
        private static InputHandler instance;
        public static InputHandler Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputHandler();
                return instance;
            }
        }
        private InputHandler() { }
        private Dictionary<Keys, ICommand> keybindsUpdate = new Dictionary<Keys, ICommand>();
        private Dictionary<Keys, ICommand> keybindsButtonDown = new Dictionary<Keys, ICommand>();
        private Stack<ICommand> executedCommands = new Stack<ICommand>();
        private Stack<ICommand> undoneCommands = new Stack<ICommand>();
        private List<ICommand> externalCommands = new List<ICommand>();
        public void AddUpdateCommand(Keys inputKey, ICommand command)
        {
            keybindsUpdate.Add(inputKey, command);
        }

        public void AddButtonDownCommand(Keys inputKey, ICommand command)
        {
            keybindsButtonDown.Add(inputKey, command);
        }

        public void ExecuteCommand(ICommand command)
        {
            externalCommands.Add(command);
        }
        private KeyboardState previousKeyState;

        // Execute all commands
        public void Execute()
        {
            KeyboardState keyState = Keyboard.GetState();
            foreach (ICommand command in externalCommands)
            {
                command.Execute();
                executedCommands.Push(command);
                undoneCommands.Clear();
            }
            externalCommands.Clear();

            // Check if a key is pressed
            foreach (var pressedKey in keyState.GetPressedKeys())
            {
                if (keybindsUpdate.TryGetValue(pressedKey, out ICommand cmd))
                {
                    cmd.Execute();
                }
                if (!previousKeyState.IsKeyDown(pressedKey) && keyState.IsKeyDown(pressedKey))
                {
                    if (keybindsButtonDown.TryGetValue(pressedKey, out ICommand cmdBd))
                    {
                        cmdBd.Execute();
                        executedCommands.Push(cmdBd);
                        undoneCommands.Clear();
                    }

                    if (pressedKey == Keys.O)
                    {
                        Redo();
                    }
                    if (pressedKey == Keys.P)
                    {
                        Undo();
                    }
                }
            }
            previousKeyState = keyState;
        }

        // Undo the last command
        private void Undo()
        {
            if (executedCommands.Count > 0)
            {
                ICommand commandToUndo = executedCommands.Pop();
                commandToUndo.Undo();
                undoneCommands.Push(commandToUndo);
            }
        }

        // Redo the last command
        public void Redo()
        {
            if (undoneCommands.Count > 0)
            {
                ICommand commandToRedo = undoneCommands.Pop();
                commandToRedo.Execute();
                executedCommands.Push(commandToRedo);
            }
        }
    }   
}
