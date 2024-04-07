using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolingPair
{
    public PoolingType type;
    public PoolableMono prefab;
    public int count;
}

[CreateAssetMenu(menuName = "SO/Pool/List")]
public class PoolingListSO : ScriptableObject
{
    public List<PoolingPair> list;
}
