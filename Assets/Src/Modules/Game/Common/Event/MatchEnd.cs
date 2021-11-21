using UnityEngine;

namespace PubData
{
    public class MatchEnd
    {
        PlayerSide _winSide;
        string _winReason;

        public PlayerSide WinSide => this._winSide;
        public string WinReason => this._winReason;

        public MatchEnd(PlayerSide winSide, string winReason)
        {
            this._winSide = winSide;
            this._winReason = winReason;
        }   
    }
}