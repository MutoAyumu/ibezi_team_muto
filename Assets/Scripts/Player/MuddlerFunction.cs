using UniRx;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuddlerFunction : MonoBehaviour
{
    [Header("VaryingLengths")]
    [SerializeField] float _maxLength;
    [SerializeField] float _varyingInterval = 1f;
    [SerializeField] Transform _currentSize = null;

    [Header("Rotation")]
    [SerializeField] float _rotationInterval = 1f;
    [SerializeField] Transform _center = null;
    [SerializeField] Ease _rotationEase = Ease.Linear;
    [SerializeField] VaryingTimeScaler _timeScaler;

    [Header("HitAction")]
    [SerializeField] LayerMask _enemyLayer = ~0;


    Sequence _varyingSequence;
    Sequence _rotateSequence;

    private void Start()
    {
        VaryingLengths(_maxLength, _varyingInterval);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Rotation(_rotationInterval);
            _varyingSequence.Pause();
        }
    }
    /// <summary>
    /// マドラーの伸縮
    /// </summary>
    /// <param name="min"></param>マドラーの最短尺
    /// <param name="max"></param>マドラーの最長尺
    /// <param name="interval"></param>伸縮にかける時間
    public void VaryingLengths(float maxLength, float interval)
    {
        _varyingSequence = DOTween.Sequence();

        var tweener1 = _currentSize.DOScaleY(maxLength, interval).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        var tweener2 = _currentSize.DOLocalMoveY(maxLength / 2, interval).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        _varyingSequence.Append(tweener1)
            .Join(tweener2);
    }
    /// <summary>
    /// マドラーの回転
    /// </summary>
    /// <param name="interval"></param>一回転に掛かる時間
    public void Rotation(float interval)
    {
        _timeScaler?.SlowDown();

        _rotateSequence = DOTween.Sequence();
        var tweener = _center.DORotate(new Vector3(0f, 0f, -360f), interval, RotateMode.FastBeyond360)
            .SetEase(_rotationEase)
            .OnUpdate(() => HitAction())
            .OnComplete(() => _varyingSequence.Play());
        _rotateSequence.Append(tweener);
    }
    /// <summary>
    /// マドラーの当たり判定
    /// </summary>
    void HitAction()
    {
        Vector2 size = new Vector2(_currentSize.localScale.x, _currentSize.localScale.y);
        Collider2D[] enemiesInRenge = Physics2D.OverlapBoxAll(_currentSize.position, size, 1f, _enemyLayer);
        if (enemiesInRenge.Length > 0)
        {
            foreach (Collider2D enemy in enemiesInRenge)
            {
                if (enemy.TryGetComponent<EnemyBase>(out EnemyBase e))
                {
                    if (!e.HitFlg)
                    {
                        e.Damage();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var t = _currentSize ? _currentSize : this.transform;

        Gizmos.DrawWireCube(t.position, t.localScale);
    }
}
