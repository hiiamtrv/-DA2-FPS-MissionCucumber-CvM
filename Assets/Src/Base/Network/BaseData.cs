//base packet for sending, override this to extend detailed packet
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class BaseData
{
    int _readIdx;
    List<object> _packetData;
    //TODO: Declare mode values about data fields

    public BaseData()
    {
        this._readIdx = 0;
        this._packetData = new List<object>();
    }

    public virtual void ReadData(EventData data)
    {
        //TODO: Override this to customize data read (data -> fields)
        //TODO: Remember call base.ReadData(data)
    }

    public virtual void WriteData(params object[] objs)
    {
        //TODO: Override this to customize data write (field -> data)
    }

    void Write(object value)
    {
        this._packetData.Add(value);
    }

    object Read()
    {
        if (this._readIdx >= this._packetData.Count)
        {
            Debug.LogWarningFormat("Index out of bound --- class: {1} --- index: {2}", this.ToString(), this._readIdx);
            return null;
        }
        else
        {
            object value = this._packetData[this._readIdx];
            this._readIdx++;
            return value;
        }
    }

    object Read(int customIndex)
    {
        return this._packetData[customIndex];
    }

    public object[] ForSend => this._packetData.ToArray();
}