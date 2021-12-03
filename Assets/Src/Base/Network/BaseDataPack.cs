//base packet for sending, override this to extend detailed packet
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using BayatGames.Serialization.Formatters.Json;

public class BaseDataPack
{
    int _readIdx;
    List<string> _packetData;
    //TODO: Declare mode values about data fields

    public BaseDataPack()
    {
        this._readIdx = 0;
        this._packetData = new List<string>();
    }

    public virtual void ReadData(EventData eventData)
    {
        Debug.Log("LOG TEAM DATA: ", eventData.Code, JsonFormatter.SerializeObject(eventData.CustomData));
        string[] jsonDatas = (string[])eventData.CustomData;
        this._packetData = new List<string>(jsonDatas);

        //TODO: Override this to customize data read (data -> fields)
        //TODO: Remember call base.ReadData(data)
    }

    public virtual void WriteData()
    {
        //TODO: Override this to use another data write methods (field -> data)
    }

    protected void PutValue(object value)
    {
        string jsonData = JsonFormatter.SerializeObject(value);
        this._packetData.Add(jsonData);
    }

    protected T GetNextValue<T>()
    {
        if (this._readIdx >= this._packetData.Count)
        {
            UnityEngine.Debug.LogWarningFormat(
                "Index out of bound --- class: {1} --- index: {2}", this.ToString(), this._readIdx
            );
            return default(T);
        }
        else
        {
            string jsonData = this._packetData[this._readIdx];
            this._readIdx++;
            object value = JsonFormatter.DeserializeObject(jsonData, typeof(T));
            return (T)value;
        }
    }

    object Read(int customIndex)
    {
        return this._packetData[customIndex];
    }

    public object[] ForSend => this._packetData.ToArray();
}