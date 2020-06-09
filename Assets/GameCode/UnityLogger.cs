using Boo.Lang;
using UnityEngine;

namespace LockdownGames.Assets.GameCode
{
    public class UnityLogger : MonoBehaviour
    {
        private List<string> storedInfo;

        private void Awake()
        {
            storedInfo = new List<string>();
        }

        public void LogInfo(string message)
        {
            Debug.Log(message);
        }

        public void LogVelocity(Vector3 position, Vector3 velocity)
        {
            var end = position + velocity * Time.deltaTime;
            var end2 = end + velocity * Time.deltaTime;
            Debug.DrawLine(position, end, Color.red, 2);
            Debug.DrawLine(end, end2, Color.blue, 2);
        }

        public void StorePosition(Vector2 position, Vector2 targetNode, int lengthOfPathLeft)
        {
            storedInfo.Add($"{position.ToString()} - path node {targetNode.ToString()} - path len {lengthOfPathLeft}");
        }

        public void ClearStoredData()
        {
            storedInfo.Clear();
        }

        public void PrintStoredInfo()
        {
            foreach (var log in storedInfo)
            {
                Debug.Log(log);
            }
        }
    }
}
