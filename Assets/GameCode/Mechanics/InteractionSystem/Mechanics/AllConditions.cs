using UnityEngine;

namespace GameCode.InteractionSystem
{
    public class AllConditions : ResettableScriptableObject
    {
        public Condition[] Conditions;

        private static AllConditions _instance;
        private const string _loadPath = "AllConditions";

        public static AllConditions Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<AllConditions>();
                }

                if (!_instance)
                {
                    _instance = Resources.Load<AllConditions>(_loadPath);
                }

                if (!_instance)
                {
                    Debug.LogError("AllConditions hasn't been created yet. Create it");
                }

                return _instance;
            }
            set { _instance = value; }
        }

        public override void Reset()
        {
            if (Conditions == null)
            {
                return;
            }

            for (int i = 0; i < Conditions.Length; i++)
            {
                Conditions[i].IsSatisfied = false;
            }
        }

        public static bool CheckCondition(Condition requiredCondition)
        {
            Condition[] allConditions = Instance.Conditions;
            Condition globalCondition = null;

            if (allConditions == null || allConditions.Length == 0 || allConditions[0] == null)
            {
                return false;
            }

            for (int i = 0; i < allConditions.Length; i++)
            {
                if (allConditions[i].Hash != requiredCondition.Hash)
                {
                    continue;
                }

                globalCondition = allConditions[i];
                break;
            }

            return globalCondition.IsSatisfied == requiredCondition.IsSatisfied;
        }
    }
}
