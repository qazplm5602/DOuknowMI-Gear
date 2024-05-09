using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Statue : InteractiveObject
{
    private bool Did = false;
    private Sprite sibla;
    

    protected override void Awake()
    {
        base.Awake();
        sibla = GetComponent<SpriteRenderer>().sprite;
    }


    public override void Interaction()
    {
        if (!!Did) return;
        IngameUIControl.Instance.STATUEUI.Show("Deus EX Machina의 조각상이다.", "빛 바랜 청년", sibla, new StatueBtnDTO()
        {
            title = "조각상에 기도한다.",
            desc = "체력 10만큼 회복",
            color = new Color(.2f,1,.2f),
            OnSelect = () =>
            {
                
            } 
        }, new StatueBtnDTO()
        {
            title = "조각상의 손에 좆을 비비다.",
            desc = "탐사가 끝날 때까지 공격력 3 증가",
            color = new Color(1, .2f, .2f),
            OnSelect = () =>
            {

            }
        }); ;
        Did = true;
    }

}