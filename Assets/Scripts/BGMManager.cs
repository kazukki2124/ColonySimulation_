using UnityEngine;

public class BGMManager : MonoBehaviour
{

    public static BGMManager Instance;

    private AudioSource bgmAudioSouce;

    /// <summary>
    /// InGameというのはゲーム本編の事です。
    /// InGameの反対はOutGameで、
    /// 例えば装備画面やクエストの選択画面など、遊び本来の部分以外の事を指します。
    /// </summary>
    public AudioClip InGameBGM;

    private void Awake()
    {
        Instance = this;
        if (bgmAudioSouce == null)
        {
            bgmAudioSouce = this.gameObject.AddComponent<AudioSource>();
            //"BGMVolume"という文字列が鍵となっているので、鍵を使ってfloatを呼び出す
            bgmAudioSouce.volume = PlayerPrefs.GetFloat("BGMVolume");
        }
    }

    private void Start()
    {
        PlayBGM(InGameBGM);
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmAudioSouce.clip == bgmClip)
        {
            return;
        }
        //再生する音源を設定
        bgmAudioSouce.clip = bgmClip;
        //ループ再生出来るように設定
        bgmAudioSouce.loop = true;
        //音源を再生
        bgmAudioSouce.Play();
    }

    /// <summary>
    /// 外部のSliderから音量を調整する
    /// </summary>
    /// <param name="value"></param>
    public void ChangeBGMVolume(float value)
    {
        bgmAudioSouce.volume = value;
        SavaBGMVolume();
    }

    /// <summary>
    /// PlayerPrefsを使って音量の値を保存する
    /// </summary>
    private void SavaBGMVolume()
    {
        //"BGMVolume"という文字列を鍵にして、floatの値を保存します
        PlayerPrefs.SetFloat("BGMVolume", bgmAudioSouce.volume);
        PlayerPrefs.Save();
    } 
}
