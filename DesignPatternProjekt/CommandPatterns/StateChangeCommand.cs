using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternProjekt.CommandPatterns
{
    class StateChangeCommand : ICommand
    {
        private readonly GameStates gameStates;
        public StateChangeCommand(GameStates gameStates)
        {
            this.gameStates = gameStates;
        }
        public void Execute()
        {
            GameState.CurrentState = gameStates;
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
