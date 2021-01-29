using System;
using System.Windows;

namespace NeutronGame
{
    partial class CellBoardEventHandler
    {
        public void CellBoard_Click(object sender, EventArgs e)
        {
            Game.Instance.HandleTokenClick();
        }
    }
}
