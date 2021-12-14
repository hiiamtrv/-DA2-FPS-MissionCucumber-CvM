using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUtils
{
    const float MIN_DISTANE_TO_TRIGGER_RUN = 3f;

    static Dictionary<int, Vector3> _lastPosition = new Dictionary<int, Vector3>();

    public static void RecordPosition(GameObject go)
    {
        int goHashCode = go.GetHashCode();
        Vector3 curPos = go.transform.position;
        _lastPosition.Remove(goHashCode);
        _lastPosition.Add(goHashCode, curPos);
    }

    public static bool DidChangePosition(GameObject go)
    {
        int goHashCode = go.GetHashCode();
        Vector3 curPos = go.transform.position;
        if (!_lastPosition.ContainsKey(goHashCode))
        {
            RecordPosition(go);
            return false;
        }
        else
        {
            Vector3 lastPos = _lastPosition[goHashCode];
            float distance = Vector3.Distance(lastPos, curPos);
            return (distance >= MIN_DISTANE_TO_TRIGGER_RUN * Time.deltaTime);
        }
    }
}
