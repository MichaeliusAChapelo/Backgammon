﻿using System;
using System.Collections.Generic;

namespace ModelDLL
{
    public class PlayerInterface
    {
        private readonly CheckerColor color;
        private readonly BackgammonGame bg;
        private Player player;

        public PlayerInterface(BackgammonGame bg, CheckerColor color, Player player)
        {
            this.color = color;
            this.bg = bg;
            this.player = player;
        }

        internal bool HasPlayer()
        {
            return player != null;
        }

        public void SetPlayerIfNull(Player player)
        {
            if(this.player == null)
            {
                this.player = player;
            }
        }

        public bool IsMyTurn()
        {
            return bg.playerToMove() == color;
        }

        public HashSet<int> GetMoveableCheckers()
        {
            if (!IsMyTurn())
            {
                throw new InvalidOperationException("Player interface for player " + color + " tried to get moveable checkers when not his turn");
            }
            return new HashSet<int>(bg.GetMoveableCheckers());
        }

        public List<int> GetMovesLeft()
        {
            if (!IsMyTurn())
            {
                throw new InvalidOperationException("Player" + color + " tried to get moves left when not his turn");
            }
            return bg.GetMovesLeft();
        }

        public GameBoardState GetGameBoardState()
        {
            return bg.GetGameBoardState();
        }

        public HashSet<int> GetLegalMovesForChecker(int position)
        {
            return bg.GetLegalMovesFor(this.color, position);
        }

        public List<int> move(int intialPosition, int targetPosition)
        {
            bg.Move(this.color, intialPosition, targetPosition);
            return new List<int>();
        }

        public void move(int from, List<int> moves)
        {
            foreach(int i in moves)
            {
                bg.Move(color, from, i);
                from += (color == CheckerColor.White ? -i : i);
            }
        }

        internal void TurnStarted()
        {
            if (player != null)
                player.TurnStarted();
        }

        internal void TurnEnded()
        {
            if (player != null)
                player.TurnEnded();
        }

        internal void MakeMove()
        {
            if (player != null) player.MakeMove();
        }

    }
}