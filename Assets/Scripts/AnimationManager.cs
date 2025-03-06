using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationManager : MonoBehaviour
{

    private Animator animator;
    private string currentAnimation = "";

    [Range(0f, 5f)] public float animationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {
        if (animator == null) { return; }
        if (animationName == "" || animationName == null) { 
            Debug.Log("No animation has been set for event trigger");
            return;
            }

        // Play only if animation is different
        if( currentAnimation != animationName){
            animator.Play(animationName);
            currentAnimation = animationName;
        }
    }

    public void OverrideAnimation(string animationName)
    {
        if (animator == null) { return; }

        // Overides current animation
        animator.Play(animationName);
        currentAnimation = animationName;
    }

    public void PauseCurrentAnimation()
    {
        if (animator == null) return;

        // Pause Current
        animator.speed = 0;
    }

    public void ResumeCurrentAnimation() 
    {

        if (animator == null) return;

        // resume
        animator.speed = animationSpeed;
    }

    public void SetAnimationTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

}
