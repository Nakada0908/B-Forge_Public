using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    public static PlayerAnime Instans;

    Animator playerAnime;

    private void Awake()
    {
        if (Instans != null)
        {
            Destroy(Instans);
            return;
        }

        Instans = this;
    }

    private void Start()
    {
        playerAnime = GetComponent<Animator>();
    }


}
