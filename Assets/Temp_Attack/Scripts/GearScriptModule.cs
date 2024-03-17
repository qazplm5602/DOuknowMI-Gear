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

    public enum Type {
        Skill,
        Link
    }

    private void Awake() {
        skillEvents = linkEvents = new();
    }

    public void LoadModule(Type type, string id, GameObject entity) {
        var moduleContainer = Instantiate(entity, transform);
        moduleContainer.name = $"{id}_Script";

        var script = moduleContainer.GetComponent<GearCogEvent>();
        if (type == Type.Skill) {
            skillEvents[id] = script;
        } else if (type == Type.Link) {
            linkEvents[id] = script;
        }
    }

    public GearCogEvent GetSkillScript(string id) => skillEvents[id];
    public GearCogEvent GetLinkScript(string id) => linkEvents[id];
}
