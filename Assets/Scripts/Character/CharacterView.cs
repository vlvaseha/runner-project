using UnityEngine;

namespace Character
{
    public class CharacterView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _viewRoot;
        [SerializeField] private Animator _characterAnimator;

        #endregion

        #region Properties

        public Animator Animator => _characterAnimator;
        public Transform ViewRoot => _viewRoot;

        #endregion
    }
}
