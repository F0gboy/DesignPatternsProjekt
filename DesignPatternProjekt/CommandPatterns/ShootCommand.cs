using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DesignPatternProjekt
{
    class ShootCommand : ICommand
    {
        private Fortress player;
        public ShootCommand(Fortress player)
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
