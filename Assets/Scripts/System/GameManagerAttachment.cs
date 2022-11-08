using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAttachment : MonoBehaviour
{
    #region �f���Q�[�g
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
    /// Update�ŌĂт���������o�^���Ă���
    /// </summary>
    public void InitCallBack(MonoEvent e)
    {
        _updateEvent = e;
    }
}
