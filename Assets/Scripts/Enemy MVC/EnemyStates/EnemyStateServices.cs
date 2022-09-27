using UnityEngine;

namespace EnemyServices
{
    // Template class for creating different states of enemy. // Implementation of state machine.
    [RequireComponent(typeof(EnemyView))]
    public class EnemyStateServices : MonoBehaviour
    {
        protected EnemyView enemyView;
        protected EnemyModel enemyModel;

        protected virtual void Awake()
        {
            enemyView = GetComponent<EnemyView>();
        }

        protected virtual void Start()
        {
            enemyModel = enemyView.enemyController.enemyModel;
        }

        // Called when entered in the state // Enables the behaviour of that state. 
        public virtual void OnStateEnter()
        {
            this.enabled = true;
        }

        // Called when exited from the state // Disables the behaviour of that state.
        public virtual void OnStateExit()
        {
            this.enabled = false;
        }

        // To change enemy state from current to new state.
        public void ChangeState(EnemyStateServices newState)
        {
            if (enemyView.currentState != null)
            {
                enemyView.currentState.OnStateExit();
            }

            enemyView.currentState = newState;
            enemyView.currentState.OnStateEnter();
        }
    }
}
