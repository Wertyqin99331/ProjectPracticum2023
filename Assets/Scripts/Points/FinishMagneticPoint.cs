
using UnityEngine;

public class FinishMagneticPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        print("Trigger");
        if (col.GetComponent<MagneticBall>() && col.GetComponent<MagneticBall>().IsRight)
        {
            LevelManager.Instance.EndLevel();
            Destroy(gameObject);
        }
    }
}
