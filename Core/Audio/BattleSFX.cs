using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BattleSFX : BehaviourSingleton<BattleSFX>
{
    [Header("System")]
    public AudioClip victory;

    [Header("Battle")]
    public AudioClip playerAttackNormal;
    public AudioClip playerAttackHeavy;
    public AudioClip[] playerUsePotion;
    public AudioClip[] emptyPotion;
    public AudioClip[] gainDefense;

    [Header("Element")]
    public AudioClip elementClick;
    public AudioClip skillAppear;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        this.audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        AudioSFX.Instance.PlayOneShot(clip);
    }

    public void Play(AudioClip[] clip)
    {
        var ran = Random.Range(0, clip.Length);
        AudioSFX.Instance.PlayOneShot(clip[ran]);
    }

    public void Play(AudioClip clip, float pitch)
    {
        this.audioSource.pitch = pitch;
        this.audioSource.PlayOneShot(clip);
    }
}
