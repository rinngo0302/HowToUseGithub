using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SE : Singleton<SE>
{
    [System.Serializable]
    public struct AudioData
    {
        [SerializeField] public AudioSource AudioSource;
        [SerializeField] public AudioClip Clip;
        [SerializeField, Range(0f, 1f)]
        public float Volume;
    }

    // SE Audio Clip
    [SerializeField] private AudioData[] _audioDatas;

    void Start()
    {
        if (CheckInstance())
        {
            DontDestroyOnLoad(this);
        }
    }

    /// <summary>
    /// SEの再生(ex: SE.Instance.Play(0, false);)
    /// </summary>
    /// <param name="index"> Clipのindex </param>
    /// <param name="isLoop"> ループするか </param>
    public void Play(int index, bool isLoop)
    {
        if (index < 0 || index >= _audioDatas.Length)
        {
            Debug.Log($"インデックスが範囲外です。({index})");
            return;
        }

        if (_audioDatas[index].Clip == null)
        {
            Debug.Log($"AudioClipがないです。{index}");
            return;
        }

        if (_audioDatas[index].AudioSource == null)
        {
            Debug.Log("AudioSourceがアタッチされていません。");
            return;
        }

        _audioDatas[index].AudioSource.loop = isLoop;
        _audioDatas[index].AudioSource.clip = _audioDatas[index].Clip;
        _audioDatas[index].AudioSource.volume = _audioDatas[index].Volume;
        _audioDatas[index].AudioSource.Play();
    }

    /// <summary>
    /// 1回だけSEを流す
    /// </summary>
    /// <param name="index"> Clipのindex </param>
    public void PlayOneShot(int index)
    {
        if (index < 0 || index >= _audioDatas.Length)
        {
            Debug.LogError($"インデックスが範囲外です。({index})");
            return;
        }

        if (_audioDatas[index].Clip == null)
        {
            Debug.LogError($"AudioClipがないです。{index}");
            return;
        }

        if (_audioDatas[index].AudioSource == null)
        {
            Debug.LogError("AudioSourceがアタッチされていません。");
            return;
        }

        _audioDatas[index].AudioSource.volume = _audioDatas[index].Volume;
        _audioDatas[index].AudioSource.PlayOneShot(_audioDatas[index].Clip);
    }

    /// <summary>
    /// SEの再生を止める
    /// </summary>
    public void Stop(int index)
    {
        if (index < 0 || index >= _audioDatas.Length)
        {
            Debug.LogError($"インデックスが範囲外です。({index})");
            return;
        }

        if (_audioDatas[index].AudioSource == null)
        {
            Debug.LogError("AudioSourceがアタッチされていません。");
            return;
        }

        _audioDatas[index].AudioSource.Stop();
    }
}
