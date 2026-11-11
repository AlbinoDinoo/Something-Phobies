using UnityEngine;

public class DayNightCycle : MonoBehaviour
{   
    [SerializeField]
    private Transform SunOri;

    [SerializeField]
    private Transform Camara;

    private void Update()
    {
        
        SunOri.Rotate(0.03f,0,0);

        //SunOri.rotation = Camara.rotation;
        //SunOri.Rotate(180f,0,0);
    }
}
