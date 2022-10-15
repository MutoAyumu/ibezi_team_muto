using UniRx;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuddlerFunction : MonoBehaviour
{
    [SerializeField] float _maxLength;
    [SerializeField] float _intervalChangeLength = 1f;
    [SerializeField] float _intervalRotation = 1f;
    [SerializeField] Transform _cullentSize = null;
    [SerializeField] Transform _center = null;
    [SerializeField] LayerMask _enemyLayer = ~0;
    Sequence _sequence;
    Sequence _sequence2;


    private void Start()
    {
        MuddlerLengthMax(_maxLength, _intervalChangeLength);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            MuddlerRotation(_intervalRotation);
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
        _sequence = DOTween.Sequence();

        var tweener1 = _cullentSize.DOScaleY(maxLength, interval).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        var tweener2 = _cullentSize.DOLocalMoveY(maxLength / 2, interval).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        _sequence.Append(tweener1)
            .Join(tweener2);
    }
    /// <summary>
    /// �}�h���[�̉�]
    /// </summary>
    /// <param name="interval"></param>���]�Ɋ|���鎞��
    public void MuddlerRotation(float interval)
    {
        _sequence2 = DOTween.Sequence();
        var tweener = _center.DORotate(new Vector3(0f, 0f, -360f), interval, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .OnUpdate(() => HitAction())
            .OnComplete(() => _sequence.Play());
        _sequence2.Append(tweener);
    }
    /// <summary>
    /// �}�h���[�̓����蔻��
    /// </summary>
    void HitAction()
    {
        Vector2 size = new Vector2(_cullentSize.localScale.x, _cullentSize.localScale.y);
        Collider2D[] enemiesInRenge = Physics2D.OverlapBoxAll(_cullentSize.position, size, 1f, _enemyLayer);
        if (enemiesInRenge.Length > 0)
        {
            foreach (Collider2D enemy in enemiesInRenge)
            {
                if (enemy.TryGetComponent<EnemyBase>(out EnemyBase e))
                {
                    if (!e.HitFlg)
                    {
                        e.CutCoffeeBean();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_cullentSize.position, _cullentSize.localScale);
    }
}
