using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoltshotState : MonoBehaviour
{
    
    //_player�� weaponTrm������ o
    //�� ��ũ��Ʈ�� �Ѿ���°� Ư�� ���� ���� ����
    //���� DIctionary�����ŷ� <SkillName, GameObject> �̵��ŷ� �����հ����ͼ� ��ȯ����
    //���� Player���� ĳ�̵� mousePos�� �ִٰ� ħ
    // ���� void�� Enter�� �������Ұǵ� ���� ��¦ ���� �� Air������ �����̰� Ground������ �����̰�
    // ���� Player����State �̷��ſ� �����̴°� +@ �ڵ� ������� �� ����ؼ� �����ϴ� ������ �ؾ��ҰŰ�����
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    void BoltShot()
    {
        Vector2 dir = mousePos - transform.position;
        Quaternion look = Quaternion.LookRotation(dir);
        //GameObject bolt = Instantiate(Skills[Bolt], weaponTrm.position, look);
        //Addforce? �����
        //Raycast�� ������ �ν��ϴ°͵� �����, �� �� ���� ���� �װŷ� �ҵ�
        //�ƴϸ� bolt ������ �ȿ� vec3.up * speed * deltatime �̷��������� ��
    }
}
