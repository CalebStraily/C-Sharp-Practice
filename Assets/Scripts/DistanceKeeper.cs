using UnityEngine;
using UnityEngine.UI;

public class DistanceKeeper : MonoBehaviour
{
    [SerializeField]
    GameObject player1;
    [SerializeField]
    GameObject player2;
    [SerializeField]
    public float distance;
    
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player1.transform.position, player2.transform.position);
        slider.value = distance;
    }
}
