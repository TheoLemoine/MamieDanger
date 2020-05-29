using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Animator))]
    public class HumanoidIKOverride : MonoBehaviour
    {
        [SerializeField] [Range(0f, 1f)] private float weight = 0.7f;
        
        private Animator _animator;
        private Transform[] _goals = new Transform[4];

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            for (int i = 0; i < _goals.Length; i++)
            {
                var goal = (AvatarIKGoal) i;
                var target = _goals[i];
                
                if (target != null)
                {
                    _animator.SetIKPositionWeight(goal, weight);
                    _animator.SetIKPosition(goal, target.position);
                }
                else
                {
                    _animator.SetIKPositionWeight(goal, 0);
                }
            }
        }

        public void SetGoal(AvatarIKGoal goal, Transform target)
        {
            _goals[(int)goal] = target;
        }

        public void UnsetGoal(AvatarIKGoal goal)
        {
            _goals[(int)goal] = null;
        }

        public Transform GetGoal(AvatarIKGoal goal)
        {
            return _goals[(int)goal];
        }
    }
}