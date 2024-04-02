using UnityEngine;

[CreateAssetMenu(menuName = "SO/DropTable")]
public class DropTableSO : ScriptableObject
{
    [Header("Part Amount")]
    public Vector2Int smallPartAmount;
    public Vector2Int bigPartAmount;

    [Space, Header("Experience")]
    public int experience;
}
