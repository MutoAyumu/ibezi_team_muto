using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAttachment : MonoBehaviour
{
    #region デリゲート
    public delegate void MonoEvent();
    MonoEvent _updateEvent;
    #endregion

    private void Awake()
    {
        GameManager.Instance.InitUpdateCallback(this);
        GameManager.Instance.Init(this);
    }
    private void Update()
    {
        _updateEvent?.Invoke();
    }

    /// <summary>
    /// Updateで呼びたい処理を登録しておく
    /// </summary>
    public void InitCallBack(MonoEvent e)
    {
        _updateEvent = e;
    }
}
