using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr
{
    const float DEFAULT_ALL = 50;
    const float DEFAULT_BGM = 50;
    const float DEFAULT_SFX = 50;

    float _all;
    public float All
    {
        get { return _all; }
        set
        {
            _all = value;
            EventCenter.Publish(
                EventId.SETTING_SOUND_CHANGE,
                new SoundEventParam(SoundChange.ALL, value)
            );
        }
    }

    float _bgm;
    public float Bgm
    {
        get { return _bgm; }
        set
        {
            _bgm = value;
            EventCenter.Publish(
                EventId.SETTING_SOUND_CHANGE,
                new SoundEventParam(SoundChange.BGM, value)
            );
        }
    }

    float _sfx;
    public float Sfx
    {
        get { return _sfx; }
        set
        {
            _sfx = value;
            EventCenter.Publish(
                EventId.SETTING_SOUND_CHANGE,
                new SoundEventParam(SoundChange.SFX, value)
            );
        }
    }

    public SoundMgr()
    {
        if (this.HasLocalData())
        {
            this._all = PlayerPrefs.GetFloat(LocalKey.SETTING_SOUND_ALL);
            this._bgm = PlayerPrefs.GetFloat(LocalKey.SETTING_SOUND_BGM);
            this._sfx = PlayerPrefs.GetFloat(LocalKey.SETTING_SOUND_SFX);
        }
        else
        {
            this._all = DEFAULT_ALL;
            this._bgm = DEFAULT_BGM;
            this._sfx = DEFAULT_SFX;
        }
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetFloat(LocalKey.SETTING_SOUND_ALL, this._all);
        PlayerPrefs.SetFloat(LocalKey.SETTING_SOUND_BGM, this._bgm);
        PlayerPrefs.SetFloat(LocalKey.SETTING_SOUND_SFX, this._sfx);
    }

    bool HasLocalData()
    {
        return PlayerPrefs.HasKey(LocalKey.SETTING_SOUND_ALL);
    }
}

public class SoundEventParam
{
    SoundChange _soundChange;
    public SoundChange GetSoundChange() { return this._soundChange; }

    float _value;
    public float GetValue() { return this._value; }

    public SoundEventParam(SoundChange change, float value)
    {
        this._soundChange = change;
        this._value = value;
    }
}

public enum SoundChange
{
    ALL,
    BGM,
    SFX,
}
