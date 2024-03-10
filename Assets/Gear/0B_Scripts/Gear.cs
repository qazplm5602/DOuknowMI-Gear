using UnityEngine;
using DG.Tweening;

public enum Direction {
    Left = -1,
    None = 0, 
    Right = 1
}

public class Gear : MonoBehaviour
{
    [SerializeField] private bool _head = false;
    public Gear childGear;

    public float duration = 2f;
    [SerializeField] private int _cogAmount = 6;
    public Direction direction;

    private int _currentCog = 0;
    private float _cogAngle;

    private Sequence _seq;

    private void Start() {
        if(_head) {
            childGear.Init((Direction)(-(int)direction), duration);
        }

        _cogAngle = 360f / _cogAmount;
    }

    public void Init(Direction dir, float dur) {
        direction = dir;
        duration = dur;

        if(childGear != null) {
            childGear.Init((Direction)(-(int)dir), dur);
        }
    }

    public void TurnGear() {
        if(_seq != null) {
            _seq.Complete();
        }
        _seq = DOTween.Sequence();

        _seq.Append(transform.DOLocalRotate(new Vector3(0, 0, transform.localEulerAngles.z + _cogAngle * (int)direction), duration)
        .SetEase(Ease.InCubic).OnComplete(PlusGear));

        if(childGear != null) {
            childGear.TurnGear();
        }
    }

    private void PlusGear() {
        ++_currentCog;
        Debug.Log($"{gameObject.name} {_currentCog}번째 기어");

        if(_currentCog == _cogAmount) _currentCog = 0;
    }

    //Test
    private void Update() {
        if(Input.GetMouseButtonDown(0) && _head) {
            TurnGear();
        }
    }
}
