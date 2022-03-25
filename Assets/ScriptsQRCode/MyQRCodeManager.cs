using QRTracking;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

public class MyQRCodeManager : MonoBehaviour
{
    public QRCodesManager qRCodesManager;
    public GameObject head;

    private Regex regex = new Regex("^[1-9](-[0-9]{3}){3} / E[0-9]{2}(-[0-9]{2}){2}");
    //public TextMeshPro statusText;

    public Dictionary<string, Pose> qrCodeList;

	public void Start()
	{
        qrCodeList = new Dictionary<string, Pose>();
    }

	public void StartScan()
    {
        // start QR tracking with the press of a button
        qRCodesManager.StartQRTracking();
        //statusText.text = "Started QRCode Tracking";
    }
    public void StopScan()
    {
        // Stop the tracking with the press of a button
        qRCodesManager.StopQRTracking();
        //statusText.text = "Stopped QRCode Tracking";
    }

    public bool AddQRCode(string key, Pose value)
    {
        if(regex.IsMatch(key)){
            string data = regex.Match(key).Value;
            if (qrCodeList.ContainsKey(data))
            {
                qrCodeList.Remove(data);
            }

            qrCodeList.Add(data, value);

            return true;
        }
		else
		{
            return false;
		}
        
    }

    public string CheckNearQR()
    {
        string nearQrKey = null;
        double minDst = 0;
        Vector3 headPos = head.transform.position;

        foreach (KeyValuePair<string, Pose> kvp in qrCodeList)
        {
            double dst = EuclideanDistance(kvp.Value.position, headPos);
            if (nearQrKey == null || (nearQrKey != null && dst < minDst))
            {
                nearQrKey = kvp.Key;
                minDst = dst;
            }
        }

        return nearQrKey;
        //LatestQRCodeDetails.text = "Closest QRCode is " + nearQrKey;

    }

    public bool IsValidRef(string key)
	{
        return regex.IsMatch(key);
    }

    private double EuclideanDistance(Vector3 pos1, Vector3 pos2)
    {
        float dx = (pos2.x - pos1.x);
        float dy = (pos2.y - pos1.y);
        float dz = (pos2.z - pos1.z);

        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
}
