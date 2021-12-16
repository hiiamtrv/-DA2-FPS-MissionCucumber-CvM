using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public bool TriggerInteract(GameObject gameObject, bool isAi);
    public bool KeepInteract(GameObject gameObject);
    public void AbortInteract(GameObject gameObject);
    public GameObject GetGameObject();
}
