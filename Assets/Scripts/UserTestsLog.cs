using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System;
using Debug = UnityEngine.Debug;

public class UserTestsLog : MonoBehaviour
{
    public Stopwatch stopwatch;
    private string userTestsLine;
    private StreamWriter writer;
    private string path;

    // Start is called before the first frame update
    void Start()
    {
        stopwatch = new Stopwatch();
        path = Path.Combine(Application.persistentDataPath, "UsertTests_BOSCH.txt");
		if (!File.Exists(path))
		{
			using (StreamWriter writer = new StreamWriter(path, true))
			{
                //userTestsLine = "time_part1; time_part2; time_part3; time_part4; time_part5; time_part6; time_part7; time_part8; time_part9";
                userTestsLine = "Kit0; Kit1; time_part3; time_part4; time_part5; time_part6; time_part7; time_part8; time_part9";
                writer.WriteLine(userTestsLine);
                //writer.Close();
            }
		}
		else
		{
            userTestsLine = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("PATH: " + Application.persistentDataPath);

    }

    public void StartTimer(bool reset)
	{
		if (reset)
		{
            userTestsLine = "";
            stopwatch.Reset();
		}
        stopwatch.Start();
    }

    public void StopTimer()
	{
        TimeSpan elapsedTime = stopwatch.Elapsed;
        userTestsLine += elapsedTime.ToString() + ";";
        stopwatch.Reset();
    }

    public void SetKitName(string kitName)
	{
        userTestsLine += kitName + ";";
	}
    public void WriteToFile()
    {
        Debug.Log("LOGS: " + userTestsLine);

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(userTestsLine);
            //writer.Close();
        }
    }
}
