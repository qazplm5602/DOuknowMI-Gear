using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GearCogEvent : MonoBehaviour {
    public GameObject _player;
    
    // public abstract void Init();
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

    public GearCogEvent LoadModule(Type type, string id, GameObject entity) {
        var moduleContainer = Instantiate(entity, transform);
        moduleContainer.name = $"{id}_Script";

        var script = moduleContainer.GetComponent<GearCogEvent>();
        if (type == Type.Skill) {
            skillEvents[id] = script;
        } else if (type == Type.Link) {
            linkEvents[id] = script;
        }

        return script;
    }

    public bool UnloadModule(Type type, string id) {
        GearCogEvent script;

        if (type == Type.Skill) {
            if (skillEvents.TryGetValue(id, out script)) return false;
            skillEvents.Remove(id);
        } else if (type == Type.Link) {
            if (linkEvents.TryGetValue(id, out script)) return false;
            linkEvents.Remove(id);
        } else return false;

        if (script == null) return false;
        
        Destroy(script.gameObject);
        return true;
    }

    public GearCogEvent GetSkillScript(string id) {
        skillEvents.TryGetValue(id, out var script);
        return script;
    }
    public GearCogEvent GetLinkScript(string id) {
        linkEvents.TryGetValue(id, out var script);
        return script;
    }
}
