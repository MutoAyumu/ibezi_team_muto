using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class GameManager
{
    /* TODO
    �|�[�Y�̊Ǘ�
    �t�F�[�Y�̊Ǘ�
    */

    #region �v���p�e�B
    /// <summary>
    /// �C���X�^���X
    /// </summary>
    public static GameManager Instance => new GameManager();
    /// <summary>
    /// ���݂̃t�F�[�Y
    /// </summary>
    public IReadOnlyReactiveProperty<int> Phase => _phase;
    public IReadOnlyReactiveProperty<int> Score => _score;

    #endregion

    #region �ϐ�

    IntReactiveProperty _phase = new IntReactiveProperty();
    IntReactiveProperty _score = new IntReactiveProperty();

    bool _isPause;

    #endregion

    #region �C�x���g

    /// <summary>
    /// �|�[�Y���̏���
    /// </summary>
    public event Action OnPause;

    /// <summary>
    /// �|�[�Y�������̏���
    /// </summary>
    public event Action OnResume;

    #endregion

    //�R���X�g���N�^
    public GameManager()
    {
        Debug.Log("New GameManagaer");
    }

    /// <summary>
    /// �����ݒ�
    /// </summary>
    /// <param name="attachment"></param>
    public void Init(GameManagerAttachment attachment)
    {

    }

    void OnUpdate()
    {
        //�Ƃ肠�����̓|�[�Y��������
        if(Input.GetButtonDown("Cancel"))
        {
            //�|�[�Y��
            if (_isPause)
            {
                OnResume?.Invoke();
                Debug.Log("�|�[�Y����");
            }
            else
            {
                OnPause?.Invoke();
                Debug.Log("�|�[�Y�J�n");
            }

            //�t�ɂ���
            _isPause = !_isPause;
        }
    }

    #region �R�[���o�b�N

    public void InitUpdateCallback(GameManagerAttachment attachment)
    {
        attachment.InitCallBack(OnUpdate);
    }

    #endregion
}
