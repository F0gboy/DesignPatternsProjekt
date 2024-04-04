using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternProjekt.CommandPatterns
{
    class ShootCommand : ICommand
    {
        private Player player;
        public ShootCommand(Player player)
        {
            this.player = player;
        }
        public void Execute()
        {
            player.Shoot();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
