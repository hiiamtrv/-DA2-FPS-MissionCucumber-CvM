using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMaker
{
    public static Dictionary<string, int> GenerateTeams(List<string> listPlayer)
    {
        List<object> cloneList = new List<object>(listPlayer);
        Dictionary<string, int> teamResult = new Dictionary<string, int>();

        int numPlayerMice = NetworkGame.NUM_PLAYER_MICE;
        int numPlayerCat = NetworkGame.NUM_PLAYER_CATS;

        //pick side cat
        for (var i = 0; i < Mathf.Min(NetworkGame.NUM_PLAYER_CATS, cloneList.Count); i++)
        {
            string playerId = Utils.PickFromList(cloneList, true) as string;
            teamResult.Add(playerId, ((int)CharacterSide.CATS));
        }
        
        //pick side mice
        for (var i = 0; i < Mathf.Min(NetworkGame.NUM_PLAYER_MICE, cloneList.Count); i++)
        {
            string playerId = Utils.PickFromList(cloneList, true) as string;
            teamResult.Add(playerId, ((int)CharacterSide.MICE));
        }

        return teamResult;
    }
}
