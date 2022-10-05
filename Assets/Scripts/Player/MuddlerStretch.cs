using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuddlerStretch : MonoBehaviour
{
    [SerializeField]Transform _cullentSize = null;
    float _cullentLength = 0f;
    Tweener _tweener;
    Sequence _sequence;


    private void Start()
    {
        MuddlerLengthMax(5f,2f);
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            _sequence.Pause();
        }
    }
    /// <summary>
    /// �}�h���[�̐L�k
    /// </summary>
    /// <param name="min"></param>�}�h���[�̍ŒZ��
    /// <param name="max"></param>�}�h���[�̍Œ���
    /// <param name="interval"></param>�L�k�ɂ����鎞��
    public void MuddlerLengthMax(float maxLength, float interval)
    {
        //DOTween.To(() => _cullentLength
        //, x => _cullentLength = x
        //, maxLength
        //, interval);

        _sequence = DOTween.Sequence();

        var tweener = _cullentSize.DOScaleY(maxLength, interval).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        var t = _cullentSize.DOLocalMoveY(maxLength / 2, interval).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        _sequence.Append(tweener)
            .Join(t);
    }
}
