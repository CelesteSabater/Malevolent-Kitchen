using UnityEngine;
using System;
using System.Collections;

public class AnimationSystem : Singleton<AnimationSystem>
{
    public void PlayAnimation(Animator anim, string animationName)
    {
        if (anim == null || string.IsNullOrEmpty(animationName))
            return;

        anim.Play(animationName);
    }
}