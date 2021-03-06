﻿using System.Windows.Controls;

namespace NeutronGame
{
    /// <summary>
    /// Interaction logic for GameUserControl.xaml
    /// </summary>
    public partial class GameUserControl : UserControl
    {
        #region GameUserControl Instance fields

        private static GameUserControl _instance;
        /// <summary>
        /// return the Instance of the GameUserControl
        /// </summary>
        public static GameUserControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameUserControl();

                return _instance;
            }
        }

        #endregion

        #region ctor
        public GameUserControl()
        {
            InitializeComponent();
            DataContext = Players.Instance;
            Game.Instance.StartNewGame(this, Players.Instance.Player2Name);
        }
        #endregion
    }
}