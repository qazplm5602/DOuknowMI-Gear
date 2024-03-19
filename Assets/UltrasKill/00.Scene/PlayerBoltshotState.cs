using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoltshotState : MonoBehaviour
{
    
    //_player의 weaponTrm같은거 o
    //이 스크립트로 넘어오는건 특정 조건 만족 이후
    //대충 DIctionary같은거로 <SkillName, GameObject> 이딴거로 프리팹가져와서 소환때림
    //대충 Player에서 캐싱된 mousePos가 있다고 침
    // 밑의 void는 Enter때 실행을할건데 뭔가 살짝 뭔가 그 Air에서도 움직이고 Ground에서도 움직이고
    // 대충 Player정상State 이런거에 움직이는거 +@ 코드 묶어놓고 걔 상속해서 공격하는 식으로 해야할거같긴함
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    void BoltShot()
    {
        Vector2 dir = mousePos - transform.position;
        Quaternion look = Quaternion.LookRotation(dir);
        //GameObject bolt = Instantiate(Skills[Bolt], weaponTrm.position, look);
        //Addforce? 고민중
        //Raycast로 데미지 인식하는것도 고민중, 총 쪽 무기 쓰면 그거로 할듯
        //아니면 bolt 프리팹 안에 vec3.up * speed * deltatime 이런느낌으로 슛
    }
}
