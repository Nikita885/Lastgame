using UnityEngine;

public class BridgeActivator2D : MonoBehaviour
{
    public GameObject bridgesClose;
    public GameObject bridgesOpen;
    public GameObject keyObject; // объект, который активирует мост

    private bool isActivated = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated && other.gameObject == keyObject)
        {
            ActivateBridge();
            Destroy(keyObject); // удаляем ключ
        }
    }

    private void ActivateBridge()
    {
        if (bridgesClose != null) bridgesClose.SetActive(true);
        if (bridgesOpen != null) bridgesOpen.SetActive(false);
        isActivated = true;
    }
}
