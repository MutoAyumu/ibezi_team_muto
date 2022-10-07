using UniRx;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuddlerStretch : MonoBehaviour
{
    [SerializeField] Transform _cullentSize = null;
    [SerializeField] Transform _center = null;
    [SerializeField] float _maxLength;
    Tweener _tweener;
    Sequence _sequence;
    Sequence _sequence2;


    private void Start()
    {
        MuddlerLengthMax(_maxLength, 2f);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            MuddlerRotation(1f);
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
    public void MuddlerRotation(float interval)
    {
        _sequence2 = DOTween.Sequence();
        var t = _center.DORotate(new Vector3(0f, 0f, -360f), interval, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .OnComplete(() => _sequence.Play());
        _sequence2.Append(t);
    }
}
