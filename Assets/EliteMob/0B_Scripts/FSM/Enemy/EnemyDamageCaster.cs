using UnityEngine;
using FSM;

public class EnemyDamageCaster : MonoBehaviour
{
    private Enemy _owner;

    public void SetOwner(Enemy owner) {
        _owner = owner;
    }

    public void Damage() {
        //if(Physics2D.OverlapBox());
    }
}
