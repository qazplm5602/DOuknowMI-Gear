using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerWeapon
{
    BoltAndNut = 0,
    Wheel,
    Piston,
    Foghorn,
    BoltNutWheelChain

    ,None // dont remove none
}

public class PlayerWeapons : MonoBehaviour
{
    public static PlayerWeapons instance;

    public PlayerWeapon playerWeapon;
    public GameObject[] playerWeaponPrefab;
    public Dictionary<PlayerWeapon, GameObject> WeaponDictionary = new();


    private void Awake()
    {
        instance = this;
        for(int i = 0; i <  playerWeaponPrefab.Length; i++)
        {
            WeaponDictionary[(PlayerWeapon)i] = playerWeaponPrefab[i];
        }
    }

    
}
