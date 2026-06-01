using UnityEngine;
using UnityEngine.AI;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
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

        if (playerData.MyTeam == Team.Red)
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
        else
        {
            animatorGeneral.SetBool("isWalking", false);
            animatorMonitor.SetBool("isWalking", false);
            
            Vector3 forward = player.transform.forward;

            float idleX = 0f;
            float idleY = 0f;

            if (Mathf.Abs(forward.x) > Mathf.Abs(forward.z))
            {
                idleX = forward.x > 0 ? 1f : -1f;
            }
            else
            {
                idleY = forward.z > 0 ? 1f : -1f;
            }

            animatorGeneral.SetFloat("VelocityX", idleX);
            animatorGeneral.SetFloat("VelocityY", idleY);
            animatorMonitor.SetFloat("VelocityX", idleX);
            animatorMonitor.SetFloat("VelocityY", idleY);

        }





    }
}
