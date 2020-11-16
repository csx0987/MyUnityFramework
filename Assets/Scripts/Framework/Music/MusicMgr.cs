using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : SingletonBase<MusicMgr>
{
    private AudioSource _bkMusic = null;

    private float _bkValue = 1.0f;

    private GameObject _soundObj = null;
    
    private List<AudioSource> _soundList = new List<AudioSource>();

    private float _soundValue = 1.0f;

    public MusicMgr()
    {
        MonoMgr.Instance.AddUpdateListener(Update);
    }

    private void Update()
    {
        for (int i = _soundList.Count - 1; i >= 0; --i)
        {
            if (!_soundList[i].isPlaying)
            {
                GameObject.Destroy(_soundList[i]);
                _soundList.RemoveAt(i);
            }
        }
    }

    public void PlayBkMusic(string name)
    {
        if (_bkMusic == null)
        {
            GameObject obj = new GameObject("BkMusic");
            _bkMusic = obj.AddComponent<AudioSource>();
        }
        ResMgr.Instance.LoadAsync<AudioClip>("Music/BK/" + name, (clip) =>
        {
            _bkMusic.clip = clip;
            _bkMusic.loop = true;
            _bkMusic.volume = _bkValue;
            _bkMusic.Play();
        });
    }

    public void PauseBkMusic()
    {
        if (_bkMusic == null) return;
        _bkMusic.Pause();
    }

    public void StopBkMusic()
    {
        if (_bkMusic == null) return;
        _bkMusic.Stop();
    }

    public void ChangeBkValue(float v)
    {
        _bkValue = v;
        if (_bkMusic == null) return;
        _bkMusic.volume = _bkValue;
    }

    public void PlaySound(string name, bool isLoop, UnityAction<AudioSource> callback)
    {
        if (_soundObj == null)
        {
            _soundObj = new GameObject("Sound");
        }
        
        ResMgr.Instance.LoadAsync<AudioClip>("Music/Sound/"+name, (clip) =>
        {
            AudioSource source = _soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = _soundValue;
            source.Play();
            _soundList.Add(source);
            callback?.Invoke(source);
        });
    }

    public void ChangeSoundValue(float value)
    {
        _soundValue = value;
        for (int i = 0; i < _soundList.Count; i++)
        {
            _soundList[i].volume = value;
        }
    }

    public void StopSound(AudioSource source)
    {
        if (_soundList.Contains(source))
        {
            _soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
}
