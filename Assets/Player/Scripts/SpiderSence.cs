using UnityEngine;

public class SpiderStatus : MonoBehaviour
{
    public Renderer[] StatusRenderers;
    public Light StatusLight;


    public float FlikerDelay = 0.15f;
    private float Timer = 0f;

    private bool IsInDanger;

    void Start()
    {
        SetNormal();
    }

    void Update()
    {
        if (IsInDanger)
        {
            FlickerLight();
        }


        print(IsInDanger);
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetDanger();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SetNormal();
        }
    }

    public void SetDanger()
    {
        IsInDanger = true;
        ChangeColor(Color.red);
    }

    public void SetNormal()
    {
        IsInDanger = false;
        ChangeColor(Color.green);
        StatusLight.intensity = 2f;
    }

    void ChangeColor(Color NewColor)
    {
        foreach (Renderer Part in StatusRenderers)
        {
            Part.material.color = NewColor;
        }

        StatusLight.color = NewColor;
    }

    void FlickerLight()
    {
       Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            StatusLight.intensity = Random.Range(0f, 4f);

            Timer = FlikerDelay;
        }
    }
}