using UnityEngine;

namespace PubData
{
    public class MatchEnd
    {
        CharacterSide _winSide;
        string _winReason;

        public CharacterSide WinSide => this._winSide;
        public string WinReason => this._winReason;

        public MatchEnd(CharacterSide winSide, string winReason)
        {
            this._winSide = winSide;
            this._winReason = winReason;
        }   
    }
}