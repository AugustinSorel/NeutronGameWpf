﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeutronGame
{
    class Game
    {
        #region User Control Fields
        private GameUserControl gameUserControl;
        #endregion

        #region Game Instance

        private static Game _instance;

        public static Game Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Game();

                return _instance;
            }
        }

        #endregion

        #region Game Fields

        private bool player1Turn;
        private bool tokenSelected;
        private GameTimer gameTimer;
        private Ellipse ellipseSelected;
        EnumBoard[,] enumBoard;
        EnumMoveDirection enumMoveDirection;

        #endregion

        #region Start New Game

        public void StartNewGame(GameUserControl gameUserControl)
        {
            this.gameUserControl = gameUserControl;
            SetGameVariable();
            InitGameBoard();
            SetLabelsColor();
        }

        private void SetLabelsColor()
        {
            if (player1Turn)
            {
                gameUserControl.LabelPlayer1Name.Foreground = Brushes.White;
                gameUserControl.LabelPlayer2Name.Foreground = GlobalColors.LightGray;
            }
            else
            {
                gameUserControl.LabelPlayer1Name.Foreground = GlobalColors.LightGray;
                gameUserControl.LabelPlayer2Name.Foreground = Brushes.White;
            }
                
        }

        private void SetGameVariable()
        {
            enumBoard = new EnumBoard[5, 5];
            player1Turn = true;
            tokenSelected = false;
            gameTimer = new GameTimer(gameUserControl);
        }
        #endregion

        #region Handle Token click Event

        public void HandleTokenClick(object sender)
        {
            if (sender is Button)
            {
                if (!tokenSelected)
                    return;

                enumMoveDirection = GetMoveDirection(sender);

                if (IllegalMove(sender))
                    return;

                new MoveToken(sender, ellipseSelected, enumMoveDirection);
                UpdateEnumBoard(sender);

                player1Turn ^= true;
                SetLabelsColor();
                tokenSelected = false;

            }
            else if (sender is Ellipse)
            {
                if (!tokenSelected)
                {
                    if (player1Turn && (sender as Ellipse).Fill.ToString() == GlobalColors.TokenPlayer2.ToString() ||
                        !player1Turn && (sender as Ellipse).Fill.ToString() == GlobalColors.TokenPlayer1.ToString())
                    {
                        MessageBox.Show("Wrong Piece");
                        return;
                    }

                    gameTimer.StartTimer();
                    tokenSelected = true;
                    ellipseSelected = sender as Ellipse;
                    DisplayPieceSelected();
                }
                else
                {
                    tokenSelected = false;
                }
            }
        }

        private void DisplayPieceSelected()
        {
            int startCol = Grid.GetColumn(ellipseSelected);
            int startRow = Grid.GetRow(ellipseSelected);

            // Check down
            DrawUp(startCol, startRow);
            DrawDown(startCol, startRow);
            

        }

        private void DrawDown(int startCol, int startRow)
        {
            for (int i = 1; i < 5; i++)
            {
                if (startRow - i < 0)
                    return;

                if (enumBoard[startCol, startRow - i] != EnumBoard.EmptyCell)
                {
                    var elements = gameUserControl.GameBoard.Children.Cast<Button>().
                                First(e => Grid.GetColumn(e) == startCol && Grid.GetRow(e) == startRow - i + 1);


                    var eelement = gameUserControl.GameBoard.Children
                           .OfType<Button>()
                           .Where(e => Grid.GetColumn(e) == startCol && Grid.GetRow(e) == startRow - i + 1)
                           .FirstOrDefault();

                    eelement.Style = gameUserControl.FindResource("SelectedButton") as Style;

                    return;
                }
            }
        }

        private void DrawUp(int startCol, int startRow)
        {
            for (int i = 1; i < 5; i++)
            {
                if (startRow + i > 4)
                    return;

                if (enumBoard[startCol, startRow + i] != EnumBoard.EmptyCell)
                {
                    var elements = gameUserControl.GameBoard.Children.Cast<Button>().
                                First(e => Grid.GetColumn(e) == startCol && Grid.GetRow(e) == startRow + i - 1);


                    var eelement = gameUserControl.GameBoard.Children
                           .OfType<Button>()
                           .Where(e => Grid.GetColumn(e) == startCol && Grid.GetRow(e) == startRow + i - 1)
                           .FirstOrDefault();

                    eelement.Style = gameUserControl.FindResource("SelectedButton") as Style;

                    return;
                }
            }
        }

        private EnumMoveDirection GetMoveDirection(object sender)
        {
            TokenMovement tokenMovement = new TokenMovement(sender, ellipseSelected);
            return tokenMovement.GetMovementDirection();
        }

        private void UpdateEnumBoard(object sender)
        {
            // update the enu...
        }

        private bool IllegalMove(object sender)
        {
            //foreach (var item in enumBoard)
            //{
            //    MessageBox.Show(item.ToString());
            //}

            return false;
        }

        #endregion

        #region Init The Default Game Board;

        private void InitGameBoard()
        {
            new InitGameBoard(gameUserControl, enumBoard);
        }
        
        #endregion
    }
}
