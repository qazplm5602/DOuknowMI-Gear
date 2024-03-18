using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GearCogEvent : MonoBehaviour {
    public abstract void Init();
    public abstract void Use();
}

public class GearScriptModule : MonoBehaviour
{
    Dictionary<string, GearCogEvent> skillEvents;
    Dictionary<string, GearCogEvent> linkEvents;
}
