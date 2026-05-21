using UnityEngine;

public class RatingScoreManager : MonoBehaviour
{
    [SerializeField] private string[] tagsFulbo;
    [SerializeField] private string[] tagsSoccer;

    [SerializeField] private float ratingLosePerSecond = 0.1f;
    [SerializeField] private float ratingGainFulbo = 0.2f;
    [SerializeField] private float ratingGainSoccer = 0.2f;

    [SerializeField] private float sizeAreaMultiplier = 1f;

    void OnEnable()
    {
        // Initialize if necessary
    }

    void Update()
    {
        //Debug.Log("Fulbo Score: " + GameData.GetScoreFulbo());
        //Debug.Log("Soccer Score: " + GameData.GetScoreSoccer());

        sizeAreaMultiplier = Mathf.Lerp(2f, 0.5f, Mathf.InverseLerp(3,7, transform.localScale.x));

        if (GameData.GetScoreFulbo() > 0)
        {
            GameData.SetScoreFulbo(-ratingLosePerSecond * Time.deltaTime);
        }

        if (GameData.GetScoreSoccer() > 0)
        {
            GameData.SetScoreSoccer(-ratingLosePerSecond * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Colliding with: " + other.gameObject.name);

        foreach (string tag in tagsFulbo)
        {
            if (other.CompareTag(tag))
            {
                //Debug.Log("Fulbo: " + tag);
                GameData.SetScoreFulbo(ratingGainFulbo * Time.deltaTime * sizeAreaMultiplier);
                return;
            }
        }
        foreach (string tag in tagsSoccer)
        {
            if (other.CompareTag(tag))
            {
                //Debug.Log("Soccer: " + tag);
                GameData.SetScoreSoccer(ratingGainSoccer * Time.deltaTime * sizeAreaMultiplier);
            }
        }
    }

    void OnDisable()
    {
        // Clean up if necessary
    }
}
