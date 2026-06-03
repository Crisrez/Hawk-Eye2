using UnityEngine;
using UnityEngine.AI;

public class AnimationFanatic : MonoBehaviour
{
    [SerializeField] private FanaticController fanaticController;
    [SerializeField] private Animator animatorGeneral, animatorMonitor;
    [SerializeField] private AnimatorOverrideController clipRed, clipBlue;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject player;

    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector2 velocity2D;

    void Start()
    {
        if (agent != null)
        {
            player = agent.gameObject;
        }

        if (fanaticController.TeamTarget() == Team.Red)
        {
            animatorGeneral.runtimeAnimatorController = clipRed;
            animatorMonitor.runtimeAnimatorController = clipRed;
        }
        else
        {
            animatorGeneral.runtimeAnimatorController = clipBlue;
            animatorMonitor.runtimeAnimatorController = clipBlue;
        }
    }

    void Update()
    {
        velocity = agent.velocity;

        velocity2D = new Vector2(velocity.x, velocity.z);

        if (velocity2D.magnitude > 0.1f)
        {
            animatorGeneral.SetBool("isWalking", true);
            animatorMonitor.SetBool("isWalking", true);

            animatorGeneral.SetFloat("VelocityX", velocity2D.x);
            animatorGeneral.SetFloat("VelocityY", velocity2D.y);
            animatorMonitor.SetFloat("VelocityX", velocity2D.x);
            animatorMonitor.SetFloat("VelocityY", velocity2D.y);
        }

    }
}
