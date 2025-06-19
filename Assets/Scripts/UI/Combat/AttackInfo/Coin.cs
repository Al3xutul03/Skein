using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private bool isHeads = true;
    private Image interior;

    public void Initialize() { interior = transform.GetChild(1).GetComponent<Image>(); }

    public void SetCoin(bool isHeads)
    {
        this.isHeads = isHeads;
        if (isHeads) { interior.color = Color.white; }
        else { interior.color = Color.black; }
    }
}
