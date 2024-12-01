
using UnityEngine;
public class BarrelsScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= 0)
        {
            //Destroy(gameObject);
        }
    }

    private void OnCollisionEnter()
    {
        PlayerController.Instance.FuelComponent.AddFuel(10);
        Destroy(gameObject);
    }
}
