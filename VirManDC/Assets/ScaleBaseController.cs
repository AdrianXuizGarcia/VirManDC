using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBaseController : MonoBehaviour
{
    [SerializeField]
    private Transform baseToScale;
    [SerializeField]
    private float f_scaleLittle;
    [SerializeField]
    private float f_scaleBig;

    private Vector3 scaleLittle;
    private Vector3 scaleBig;

    private bool scaleIsBig = true;

    void Start()
    {
        scaleBig = new Vector3(f_scaleBig,f_scaleBig,f_scaleBig);
        scaleLittle = new Vector3(f_scaleLittle,f_scaleLittle,f_scaleLittle);
        baseToScale.localScale = scaleBig;
    }

    public void ChangeScale () {
        if (scaleIsBig)
             baseToScale.localScale = scaleLittle;
        else
             baseToScale.localScale = scaleBig;
        scaleIsBig = !scaleIsBig;
    }

}
