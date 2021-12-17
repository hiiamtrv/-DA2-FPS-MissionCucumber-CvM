using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using PubData;

public class CharacterMgr : MonoBehaviour
{
    static CharacterMgr _ins;
    public static CharacterMgr Ins => _ins;

    int _numMouseAlive = 0;
    public int NumMouseAlive => this._numMouseAlive;

    int _numCatAlive = 0;
    public int NumCatAlive => this._numCatAlive;

    [SerializeField] List<GameObject> _characters;
    public List<GameObject> Characters => _characters;

    void Awake()
    {
        _ins = this;
        
        EventCenter.Subcribe(EventId.CHARACTER_ELIMINATED, (object pubData) => this.CheckEndMatchCondition());
        this.UpdateNumCharacterAlive();
    }

    void UpdateNumCharacterAlive()
    {
        this._numCatAlive = 0;
        this._numMouseAlive = 0;
        this._characters.ForEach(character =>
        {
            if (character == null) return;
            
            CharacterStats stats = character.GetComponent<CharacterStats>();
            if (character.activeInHierarchy && stats != null)
            {
                switch (stats.CharacterSide)
                {
                    case CharacterSide.CATS:
                        this._numCatAlive++;
                        break;
                    case CharacterSide.MICE:
                        this._numMouseAlive++;
                        break;
                }
            }
        });
    }

    void CheckEndMatchCondition()
    {
        this.UpdateNumCharacterAlive();
        if (this._numMouseAlive <= 0 && this._numCatAlive > 0)
        {
            EventCenter.Publish(
                EventId.MATCH_END,
                new MatchEnd(CharacterSide.CATS, WinReason.MICE_ELIMINATED)
            );
        }
        else if (this._numCatAlive <= 0 && this._numMouseAlive > 0)
        {
            EventCenter.Publish(
                EventId.MATCH_END,
                new MatchEnd(CharacterSide.MICE, WinReason.CATS_ELIMINATED)
            );
        }
        else if (this._numCatAlive <= 0 && this._numMouseAlive <= 0)
        {
            EventCenter.Publish(
                EventId.MATCH_END,
                new MatchEnd(CharacterSide.UNDEFINED, WinReason.DRAW)
            );
        }
    }

    public void AddCharacter(GameObject character)
    {
        this._characters.Add(character);
    }

    public void RemoveCharacter(GameObject character)
    {
        this._characters.Remove(character);
    }
}
