using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMaker
{
    public static Dictionary<string, int> GenerateTeams(List<string> listPlayer)
    {
        List<object> cloneList = new List<object>(listPlayer);
        Dictionary<string, int> teamResult = new Dictionary<string, int>();

        int numPlayerMice = 0;
        // int numPlayerMice = cloneList.Count / 2 + MathUtils.RandomInt(0, cloneList.Count % 2);
        int numPlayerCat = cloneList.Count - numPlayerMice;
        
        //pick side mice
        for (var i = 0; i < numPlayerMice; i++)
        {
            string playerId = Utils.PickFromList(cloneList, true) as string;
            teamResult.Add(playerId, ((int)CharacterSide.MICE));
        }

        //pick side cat
        for (var i = 0; i < numPlayerCat; i++)
        {
            string playerId = Utils.PickFromList(cloneList, true) as string;
            teamResult.Add(playerId, ((int)CharacterSide.CATS));
        }

        return teamResult;
    }
}
