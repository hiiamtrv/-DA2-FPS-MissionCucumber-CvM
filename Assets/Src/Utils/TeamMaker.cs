using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMaker
{
    public static Result GenerateTeams(List<string> listPlayer)
    {
        List<object> cloneList = new List<object>(listPlayer);
        Result teamResult = new Result();

        //pick side cat
        for (var i = 0; i < Mathf.Min(NetworkGame.NUM_PLAYER_CATS, cloneList.Count); i++)
        {
            string playerId = Utils.PickFromList(cloneList, true) as string;
            teamResult.Add(playerId, CharacterSide.CATS);
        }

        //pick side mice
        for (var i = 0; i < Mathf.Min(NetworkGame.NUM_PLAYER_MICE, cloneList.Count); i++)
        {
            string playerId = Utils.PickFromList(cloneList, true) as string;
            teamResult.Add(playerId, CharacterSide.MICE);
        }

        return teamResult;
    }

    public class Result : Dictionary<string, CharacterSide>
    {
        public CharacterSide GetSide(string userId)
        {
            if (this.ContainsKey(userId)) return this[userId];
            else return CharacterSide.UNDEFINED;
        }
    }
}
