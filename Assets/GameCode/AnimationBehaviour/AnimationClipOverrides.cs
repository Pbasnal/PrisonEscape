using System;
using UnityEngine;

namespace GameCode.AnimationBehaviour
{
    public class AnimationClipOverrides : MonoBehaviour
    {
        [Serializable]
        private class AnimationClipOverride
        {
            public string clipNamed;
            public AnimationClip overrideWith;
        }

        [SerializeField] AnimationClipOverride[] clipOverrides;

        public void OverrideAnimationClips(Animator animator)
        {
            AnimatorOverrideController overrideController = new AnimatorOverrideController();
            overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;

            foreach (AnimationClipOverride clipOverride in clipOverrides)
            {
                overrideController[clipOverride.clipNamed] = clipOverride.overrideWith;
            }

            animator.runtimeAnimatorController = overrideController;
        }
    }

}
