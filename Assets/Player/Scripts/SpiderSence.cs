using UnityEngine;

public class SpiderSence : MonoBehaviour
{
    public Renderer[] StatusRenderers;
    public Light StatusLight;


    public float FlikerDelay = 0.15f;
    private float Timer = 0f;

    private bool IsInDanger;

    public float DangerDuration = 3f;
    private float DangerTimer;

    void Start()
    {
        SetNormal();
    }

    void Update()
    {
        if (IsInDanger)
        {
            FlickerLight();

            DangerTimer -= Time.deltaTime;

            if (DangerTimer <= 0)
            {
                SetNormal();
            }
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
        DangerTimer = DangerDuration;

        if (!IsInDanger)
        {
            IsInDanger = true;
            ChangeColor(Color.red);
        }
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