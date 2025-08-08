// File: Assets/Scripts/Scene/SceneTransitionServices.cs

using UnityEngine;
using UnityEngine.SceneManagement;
using Flags;

namespace Scene
{
    /// <summary>
    /// Handles scene transitions with optional flag setting.
    /// Used for narrative progression and scene control.
    /// </summary>
    public class SceneTransitionServices : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string flagToSetOnTransition;
        [SerializeField] private string targetSceneName;

        /// <summary>
        /// Triggers the scene transition and sets the flag if provided.
        /// </summary>
        public void Transition()
        {
            if (!string.IsNullOrEmpty(flagToSetOnTransition) && flagSystem != null)
            {
                flagSystem.SetFlag(flagToSetOnTransition, true);
                Debug.Log($"[SceneTransitionServices] Flag '{flagToSetOnTransition}' set.");
            }

            if (!string.IsNullOrEmpty(targetSceneName))
            {
                Debug.Log($"[SceneTransitionServices] Loading scene '{targetSceneName}'...");
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                Debug.LogWarning("[SceneTransitionServices] No target scene specified.");
            }
        }
    }
}
