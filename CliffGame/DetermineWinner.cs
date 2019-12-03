using System;
using System.Collections.Generic;

namespace CliffGame
{
    public static class DetermineWinner
    {
        public static Player WhoWon(List<Player> _playerList) {

            var topPlayer = _playerList[0];

            foreach (Player player in _playerList) {
                if (player.currentScore > topPlayer.currentScore) {
                    topPlayer = player;
                }
            }

            return topPlayer;
        }
    }
}
