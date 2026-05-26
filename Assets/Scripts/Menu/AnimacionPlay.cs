using UnityEngine;
using UnityEngine.UI;

public class AnimacionPlay : MonoBehaviour
{
    [SerializeField] private Rigidbody rg;
    [SerializeField] private Button btn;

    public void DispararPelota()
    {
        btn.interactable = false;
        rg.AddForce(transform.forward * 1000);
    }
}
