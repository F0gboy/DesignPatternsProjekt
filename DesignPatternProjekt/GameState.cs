using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternProjekt
{
    public enum GameStates
    {
        StartMenu,
        KeybindMenu,
        OptionMenu,
        Playing,
        Paused,
    }
    public static class GameState
    {
        public delegate void StateEvent();
        public static event StateEvent OnChangeState;

        private static GameStates currentState = GameStates.StartMenu;
        public static GameStates CurrentState
        {
            get => currentState;
            set
            {
                currentState = value;
                if (OnChangeState != null) {
                    OnChangeState();
                }
            }
        }
    }
}
