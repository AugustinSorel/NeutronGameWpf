﻿using System;
using System.Collections.Generic;
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
        private bool neutronMoved;
        private GameTimer gameTimer;
        private Ellipse ellipseSelected;
        private bool ai;
        EnumBoard[,] enumBoard;
        EnumMoveDirection enumMoveDirection;

        #endregion

        #region Start New Game

        public void StartNewGame(GameUserControl gameUserControl, string content)
        {
            this.gameUserControl = gameUserControl;
            ai = content.Trim().ToLower() == "ai";
            SetGameVariable();
            ResetGameBoard();
            InitGameBoard();
            SetLabelsColor();
        }

        private void ResetGameBoard()
        {
            gameUserControl.GameBoard.Children.RemoveRange(25, 11);
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
            neutronMoved = true;
            gameTimer = new GameTimer(gameUserControl);
        }
        #endregion

        #region Handle Token click Event

        public void HandleTokenClick(object sender)
        {
            if (sender is Button)
            {
                if (IllegalMove(sender))
                    return;

                UpdateEnumBoard(sender);
                SetGameVar();
                enumMoveDirection = GetMoveDirection(sender);
                new MoveToken(sender, ellipseSelected, enumMoveDirection);
                SetLabelsColor();
                SetAllButtonsStyleToDefault();
                CheckEndGame();

                if (ai && !player1Turn && !neutronMoved)
                {
                    for (int i = 0; i < 5; i++)
                    {
                       Ellipse tokenNeutron;
                        // Display neutron available
                        for (int j = 0; j < 5; j++)
                        {
                            if (enumBoard[j, i] == EnumBoard.NeutronToken) 
                            {
                                tokenNeutron = GameUserControl.Instance.GameBoard.Children
                                .OfType<Ellipse>()
                                .Where(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i)
                                .FirstOrDefault();

                                SetAllButtonsStyleToDefault();
                                ellipseSelected = tokenNeutron;
                                DisplayPieceSelected();
                                break;
                            }
                        }
                    }
                    // move the neutron
                    byte index = 0;
                    List<int> numberOfAvailableMove = new List<int>();
                    foreach (var item in gameUserControl.GameBoard.Children.OfType<Button>())
                    {
                        if (item.Style == gameUserControl.FindResource("SelectedButton") as Style)
                        {
                            // AI can Win
                            if (Grid.GetRow(item) == 4)
                            {
                                HandleTokenClick(item);
                                return;
                            }
                            numberOfAvailableMove.Add(index);  
                        }
                        index++;
                    }

                    for (int i = 0; i < numberOfAvailableMove.Count; i++)
                    {
                        // remove sucidal play
                        if (numberOfAvailableMove[i] < 5)
                        {
                            numberOfAvailableMove.Remove(numberOfAvailableMove[i]);
                            break;
                        }
                    }

                    // play the neutron randomly
                    Random random = new Random();
                    int indexMove = random.Next(0, numberOfAvailableMove.Count);
                    index = 0;
                    foreach (var item in gameUserControl.GameBoard.Children.OfType<Button>())
                    {
                        if (index == numberOfAvailableMove[indexMove])
                        {
                            HandleTokenClick(item);
                            break;
                        }
                        index++;
                    }

                    // selected a random token from player2
                    index = 0;
                    Random random2 = new Random();
                    int indexMove2 = random.Next(0, 4);
                    foreach (var item in gameUserControl.GameBoard.Children.OfType<Ellipse>())
                    {
                        if (item.Style == gameUserControl.FindResource("EllipsePlayer2") as Style)
                        {
                            if (index == indexMove2)
                            {
                                HandleTokenClick(item);
                            }
                            index++;
                        }
                    }

                    // populate the random list of move for the token
                    index = 0;
                    List<int> numberOfAvailableMove2 = new List<int>();
                    foreach (var item in gameUserControl.GameBoard.Children.OfType<Button>())
                    {
                        if (item.Style == gameUserControl.FindResource("SelectedButton") as Style)
                        {
                            numberOfAvailableMove2.Add(index);
                        }
                        index++;
                    }

                    // play the token randomly
                    Random random3 = new Random();
                    int indexMove3 = random.Next(0, numberOfAvailableMove2.Count);
                    index = 0;
                    foreach (var item in gameUserControl.GameBoard.Children.OfType<Button>())
                    {
                        if (index == numberOfAvailableMove2[indexMove3])
                        {
                            HandleTokenClick(item);
                            break;
                        }
                        index++;
                    }
                }
            }
            else if (sender is Ellipse)
            {
                if (Player1SelectPlayer2Token(sender) || Player2SelectPlayer1Token(sender))
                    return;

                if (NeutronMovedAndEllipseIsNotNeutron(sender) || NeutronNotMovedAndEllipseIsNeutron(sender))
                {
                    SetAllButtonsStyleToDefault();
                    gameTimer.StartTimer();
                    ellipseSelected = sender as Ellipse;
                    DisplayPieceSelected();
                }
            }
        }

        private bool BottomWallHited(int startRow, int i)
        {
            return startRow + i == 4;
        }

        private void SetGameVar()
        {
            if (neutronMoved)
            {
                player1Turn ^= true;
                neutronMoved = false;
            }
            else if (!neutronMoved)
            {
                neutronMoved = true;
            }
        }

        private void CheckEndGame()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == 0) // Neutron hited the top wall
                    {
                        if (enumBoard[j, i] == EnumBoard.NeutronToken)
                        {
                            MessageBox.Show("Player1 Won !!!!");
                            CleanBoard();
                            return;
                        }
                    }
                    else if (i == 4) // Neutron hited the bottom wall
                    {
                        if (enumBoard[j, i] == EnumBoard.NeutronToken)
                        {
                            MessageBox.Show("Player2 Won !!!!");
                            CleanBoard();
                            return;
                        }
                    }
                    else if (enumBoard[4, i] == EnumBoard.NeutronToken)  // Neutron hitted the right wall
                    {
                        if (enumBoard[4, i + 1] != EnumBoard.EmptyCell && enumBoard[4, i - 1] != EnumBoard.EmptyCell && // hor
                            enumBoard[4 - 1, i] != EnumBoard.EmptyCell && // verti
                            enumBoard[4 - 1, i + 1] != EnumBoard.EmptyCell && enumBoard[4 - 1, i - 1] != EnumBoard.EmptyCell) // dig
                        {
                            MessageBox.Show("Neutron is trapped");
                            CleanBoard();
                            return;
                        }
                    }
                    else if (enumBoard[0, i] == EnumBoard.NeutronToken)  // Neutron hitted the left wall
                    {
                        if (enumBoard[0, i + 1] != EnumBoard.EmptyCell && enumBoard[0, i - 1] != EnumBoard.EmptyCell && // hor
                            enumBoard[0 + 1, i] != EnumBoard.EmptyCell && // verti
                            enumBoard[0 + 1, i - 1] != EnumBoard.EmptyCell && enumBoard[0 + 1, i + 1] != EnumBoard.EmptyCell) // dig
                        {
                            MessageBox.Show("Neutron is trapped");
                            CleanBoard();
                            return;
                        }
                    }
                    else if (j > 0 && i > 0 && j < 4 && i < 4) // neutron trapped in the middle of the game board
                    {
                        if (enumBoard[j, i] == EnumBoard.NeutronToken)
                        {
                            if (enumBoard[j, i + 1] != EnumBoard.EmptyCell && enumBoard[j, i - 1] != EnumBoard.EmptyCell && // hor
                                enumBoard[j + 1, i] != EnumBoard.EmptyCell && enumBoard[j - 1, i] != EnumBoard.EmptyCell && // verti
                                enumBoard[j - 1, i + 1] != EnumBoard.EmptyCell && enumBoard[j + 1, i + 1] != EnumBoard.EmptyCell && // dig up
                                enumBoard[j + 1, i - 1] != EnumBoard.EmptyCell && enumBoard[j - 1, i - 1] != EnumBoard.EmptyCell)   // dig down
                            {
                                MessageBox.Show("Neutron is trapped");
                                CleanBoard();
                                return;
                            }
                        }                        
                    }
                }
            }
        }

        private void CleanBoard()
        {
            StartNewGame(gameUserControl, gameUserControl.LabelPlayer2Name.Content.ToString());
        }

        private void SetAllButtonsStyleToDefault()
        {
            foreach (var button in gameUserControl.GameBoard.Children.OfType<Button>())
                button.Style = gameUserControl.TryFindResource(typeof(Button)) as Style;
        }

        #region Handle Ellipse Click
        private bool Player2SelectPlayer1Token(object sender)
        {
            return !player1Turn && (sender as Ellipse).Fill.ToString() == GlobalColors.TokenPlayer1.ToString();
        }
        
        private bool Player1SelectPlayer2Token(object sender)
        {
            return player1Turn && (sender as Ellipse).Fill.ToString() == GlobalColors.TokenPlayer2.ToString();
        }
        
        private bool NeutronNotMovedAndEllipseIsNeutron(object sender)
        {
            return !neutronMoved && (sender as Ellipse).Style == gameUserControl.FindResource("EllipseNeutron") as Style;
        }
        
        private bool NeutronMovedAndEllipseIsNotNeutron(object sender)
        {
            return neutronMoved && (sender as Ellipse).Style != gameUserControl.FindResource("EllipseNeutron") as Style;
        }
        
        private void DisplayPieceSelected()
        {
            new DisplayAvailableChoices(ellipseSelected, enumBoard);
        }
        #endregion

        private EnumMoveDirection GetMoveDirection(object sender)
        {
            TokenMovement tokenMovement = new TokenMovement(sender, ellipseSelected);
            return tokenMovement.GetMovementDirection();
        }

        private void UpdateEnumBoard(object sender)
        {
            enumBoard[Grid.GetColumn(ellipseSelected), Grid.GetRow(ellipseSelected)] = EnumBoard.EmptyCell;

            if (neutronMoved)
            {
                if (player1Turn)
                    enumBoard[Grid.GetColumn(sender as Button), Grid.GetRow(sender as Button)] = EnumBoard.Player1Token;
                else
                    enumBoard[Grid.GetColumn(sender as Button), Grid.GetRow(sender as Button)] = EnumBoard.Player2Token;
            }
            else
            {
                enumBoard[Grid.GetColumn(sender as Button), Grid.GetRow(sender as Button)] = EnumBoard.NeutronToken;
            }
        }

        private bool IllegalMove(object sender)
        {
            int row = Grid.GetRow((sender as Button));
            int col = Grid.GetColumn((sender as Button));

            if (enumBoard[col, row] != EnumBoard.EmptyCell)
            {
                DisplayWrongCellClicked(sender);
                return true;
            }

            if ((sender as Button).Style != gameUserControl.FindResource("SelectedButton") as Style)
            {
                DisplayWrongCellClicked(sender);
                return true;
            }

            return false;
        }

        private void DisplayWrongCellClicked(object sender)
        {
            (sender as Button).Style = gameUserControl.FindResource("WrongButton") as Style;
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
