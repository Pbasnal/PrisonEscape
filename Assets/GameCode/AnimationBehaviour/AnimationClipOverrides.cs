using System;

using LockdownGames.GameCode.Mechanics.InventorySystem;

using UnityEngine;

namespace LockdownGames.GameCode.AnimationBehaviour
{
    public abstract class AnimationClipOverrides : Equippment
    {
        [Serializable]
        private class AnimationClipOverride
        {
            public string clipNamed;
            public AnimationClip overrideWith;
        }

        [SerializeField] private AnimationClipOverride[] clipOverrides;

        private RuntimeAnimatorController originalAnimatorController;
        private Animator modifiedAnimator;

        public void OverrideAnimationClips(Animator animator)
        {
            AnimatorOverrideController overrideController = new AnimatorOverrideController();
            overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;

            foreach (AnimationClipOverride clipOverride in clipOverrides)
            {
                overrideController[clipOverride.clipNamed] = clipOverride.overrideWith;
            }

            originalAnimatorController = animator.runtimeAnimatorController;
            animator.runtimeAnimatorController = overrideController;
            modifiedAnimator = animator;
        }

        public void Revert()
        {
            modifiedAnimator.runtimeAnimatorController = originalAnimatorController;
            modifiedAnimator = null;
        }
    }

}
