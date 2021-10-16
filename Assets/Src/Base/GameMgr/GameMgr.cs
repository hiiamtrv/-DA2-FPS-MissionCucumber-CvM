using System.Collections;
using System.Collections.Generic;

public static class Gm
{
    public static SoundMgr SoundMgr { get; } = new SoundMgr();

    public static PlayerProfile PlayerProfile { get; } = new PlayerProfile();

    public static void ChangeGui(string guiName)
    {
        EventCenter.Publish(EventId.CHANGE_GUI, guiName);
    }
}
