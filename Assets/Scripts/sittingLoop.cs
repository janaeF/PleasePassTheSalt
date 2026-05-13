using UnityEngine;

public class LoopAnimation : MonoBehaviour
{
    public AnimationClip clip;

    private Animation _anim;

    void Start()
    {
        _anim = gameObject.AddComponent<Animation>();
        clip.wrapMode = WrapMode.Loop;
        _anim.AddClip(clip, clip.name);
        _anim.Play(clip.name);
    }
}