using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using System.Globalization;
using TMPro;

public class Manager : MonoBehaviour
{

    public string name;
    public int day;
    public int month;
    public int year;
    public int daysOfMonth = 0;
    public int hour;
    public int minute;
    public string[] daysArray;
    
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_Dropdown dropdownDate;
    [SerializeField] private TMP_Dropdown dropdownHour;
    [SerializeField] private TMP_Dropdown dropdownMinute;
    [SerializeField] private TMP_Dropdown dropdownAmPm;
    [SerializeField] private string savedName;


    void SetName()
    {
        if (inputName.text != null)
        {
            name = inputName.text;
        }
    }   

    private void SetDays()
    {
        month = DateTime.Now.Month;
        year = DateTime.Now.Year;
        day = DateTime.Now.Day;
        
        daysOfMonth = DateTime.DaysInMonth(year, month);
    }

     private void MakeDate()
    {
        daysArray = new string[daysOfMonth];

        for (int i=0; i<daysOfMonth; i++)
        {
            daysArray[i] = new DateTime(year,month,i+1).ToString("MMM dd ddd", new CultureInfo("en-US")).ToUpper();        
        }
    }

    private void InitDropdownDate()
    {
        dropdownDate.ClearOptions();
        
        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

        foreach (string str in daysArray)
        {
            optionList.Add(new TMP_Dropdown.OptionData(str));  
        }
        dropdownDate.AddOptions(optionList);

        dropdownDate.value = day - 1;
    }

    private void InitDropdownAmPm()
    {
        dropdownAmPm.ClearOptions();
        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();
        optionList.Add(new TMP_Dropdown.OptionData("am"));
        optionList.Add(new TMP_Dropdown.OptionData("pm"));
        dropdownAmPm.AddOptions(optionList);
    }

    private void SetDropdownAmPm(string index)
    {
        if (index == "am")
        {
            dropdownAmPm.value = 0;
        }
        else
        {
            dropdownAmPm.value = 1;
        }

    }

    private void InitDropdownHour()
    {
        dropdownHour.ClearOptions();

        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

        for (int i=1; i<13; i++)
        {
            string str = i.ToString();
            if (i<10)
            {
                str = "0" + str;
            }

            optionList.Add(new TMP_Dropdown.OptionData(str));
        }
        
        dropdownHour.AddOptions(optionList);

        int tmpHour = DateTime.Now.Hour;
        string tmpNoon = "am";
        if (tmpHour > 12)
        {
            tmpHour = tmpHour - 13;
            tmpNoon = "pm";
        }
        dropdownHour.value = tmpHour;

        InitDropdownAmPm();
        SetDropdownAmPm(tmpNoon);
    }

    private void InitDropdownMinute()
    {
        dropdownMinute.ClearOptions();

        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < 60; i++)
        {
            string str = i.ToString();
            if (i < 10)
            {
                str = "0" + str;
            }

            optionList.Add(new TMP_Dropdown.OptionData(str));
        }

        dropdownMinute.AddOptions(optionList);

        int tmpMinute = DateTime.Now.Minute;
        
        dropdownMinute.value = tmpMinute;
    }

    private void SendInfo()
    {
        string tmp = GetInfo();

        Debug.Log(tmp);     
    }

    private string GetInfo()
    {
        String dataSet;

        dataSet = name +
            "/" + dropdownDate.options[dropdownDate.value].text +
            "/" + dropdownHour.options[dropdownHour.value].text +
            "/" + dropdownMinute.options[dropdownMinute.value].text +
            "/" + dropdownAmPm.options[dropdownAmPm.value].text;
        return dataSet;
    }
    
    public void TriggerStartWork()
    {
        Debug.Log("Ãâ±Ù");
        SetName();
        SendInfo();        
    }

    public void TriggerEndWork()
    {
        Debug.Log("Åð±Ù");
        SetName();
        SendInfo();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetDays();
        MakeDate();
        InitDropdownDate();
        InitDropdownHour();
        InitDropdownMinute();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
