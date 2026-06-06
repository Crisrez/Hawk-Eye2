using UnityEngine;

public class FanaticEnded : MonoBehaviour
{
    float timer = 0f;

    void Update()
    {
        if (GameData.MatchState == MatchState.Ended)
        {
            timer += Time.deltaTime;

            if (timer >= 5f)
                Destroy(this.gameObject);
        }
    }
}
