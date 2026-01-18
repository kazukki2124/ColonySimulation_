using UnityEngine;

public class SEManager : MonoBehaviour
{
    /// <summary>
    /// SEManagerをどこからでも呼べるようにstatic変数を用意します
    /// static修飾子を付けると、ゲーム実行中にどこからでも参照することが出来ます。
    /// </summary>
    public static SEManager Instance;

    /// <summary>
    /// AudioSourceは音を鳴らすスピーカーの役割をするコンポーネント
    /// </summary>
    private AudioSource SEAudioSource;

    /// <summary>
    /// Startが実行される前に実行されるメソッド
    /// 主に初期化等を行う時に使われる
    /// </summary>
    private void Awake()
    {
        Instance = this;
        if(SEAudioSource == null)
        {
            //AddComponentはこのクラスが追加されたGameObjectに、
            //指定したコンポーネントを追加したいときに使います。
            SEAudioSource = this.gameObject.AddComponent<AudioSource>();
            //"SEVolume"という文字列が鍵となっているので、鍵を使ってfloatを呼び出す
            SEAudioSource.volume = PlayerPrefs.GetFloat("SEVolume");
        }
    }

    /// <summary>
    /// SEを再生する為のメソッド
    /// 引数をAudioClip(mp3ファイル等)の音源をAudioSourceに再生させる
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlaySE(AudioClip audioClip)
    {
        SEAudioSource.PlayOneShot(audioClip);
    }

    public void ChangeSEVolume(float value)
    {
        SEAudioSource.volume = value;
        SavaSEVolume();
    }

    private void SavaSEVolume()
    {
        PlayerPrefs.SetFloat("SEVolume", SEAudioSource.volume);
        PlayerPrefs.Save();
    }
}
