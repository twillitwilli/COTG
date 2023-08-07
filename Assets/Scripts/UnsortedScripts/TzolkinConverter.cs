using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TzolkinConverter : MonoBehaviour
{
    public string[] numbers, glyphs;

    public void ConvertDate()
    {
        int year = System.DateTime.Today.Year;
        int month = System.DateTime.Today.Month;
        int day = System.DateTime.Today.Day;
    }    
}
