using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BayatGames.Serialization.Formatters.Json;
using UnityEngine;

public class TeamMaker
{
    public static Dictionary<string, int> GenerateTeams(List<string> listPlayer)
    {
        List<object> cloneList = new List<object>(listPlayer);
        Dictionary<string, int> teamResult = new Dictionary<string, int>();

        float catRatio = (float)(NetworkGame.NUM_CATS_SLOT) / (NetworkGame.NUM_CATS_SLOT + NetworkGame.NUM_MICE_SLOT);
        int numPlayerCat = Mathf.CeilToInt((float)(cloneList.Count) * catRatio);
        if (listPlayer.Count == 1) numPlayerCat = MathUtils.RandomInt(0, 1);
        // if (listPlayer.Count == 1) numPlayerCat = 0;

        int numPlayerMice = listPlayer.Count - numPlayerCat;


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

    public static Dictionary<string, int> GetSpawnIndex(Dictionary<string, int> teamResult)
    {
        Dictionary<string, int> indexes = new Dictionary<string, int>();

        List<int> catIndexes = Enumerable.Range(0, NetworkGame.NUM_CATS_SLOT).ToList();
        List<int> mouseIndexes = Enumerable.Range(0, NetworkGame.NUM_MICE_SLOT).ToList();

        Debug.Log("Check indexes", catIndexes.ToJson(), mouseIndexes.ToJson());

        foreach (var item in teamResult)
        {
            string uid = item.Key;
            CharacterSide side = (CharacterSide)item.Value;
            int newIndex = -1;
            switch (side)
            {
                case CharacterSide.CATS:
                    newIndex = Utils.PickFromList<int>(catIndexes, true);
                    break;

                case CharacterSide.MICE:
                    newIndex = Utils.PickFromList<int>(mouseIndexes, true);
                    break;
            }
            indexes.Add(uid, newIndex);
        }

        return indexes;
    }
}
