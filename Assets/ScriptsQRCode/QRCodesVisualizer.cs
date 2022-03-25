using System;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using Microsoft.MixedReality.QR;

#if WINDOWS_UWP
using Windows.Perception.Spatial;
#endif

using TMPro;

namespace QRTracking
{
    public class QRCodesVisualizer : MonoBehaviour
    {
        public GameObject qrCodePrefab;
        public MyQRCodeManager myQRCodeManager;
        public HandMenu handMenu;

        private System.Collections.Generic.SortedDictionary<System.Guid, GameObject> qrCodesObjectsList;
        private bool clearExisting = false;
#if WINDOWS_UWP
        private SpatialCoordinateSystem CoordinateSystem = null;
#endif
        struct ActionData
        {
            public enum Type
            {
                Added,
                Updated,
                Removed
            };
            public Type type;
            public Microsoft.MixedReality.QR.QRCode qrCode;

            public ActionData(Type type, Microsoft.MixedReality.QR.QRCode qRCode) : this()
            {
                this.type = type;
                qrCode = qRCode;
            }
        }

        private System.Collections.Generic.Queue<ActionData> pendingActions = new Queue<ActionData>();
        void Awake()
        {

        }

        // Use this for initialization
        void Start()
        {
            Debug.Log("QRCodesVisualizer start");
            qrCodesObjectsList = new SortedDictionary<System.Guid, GameObject>();

            // listen to any event changes on QRCOdeManager
            QRCodesManager.Instance.QRCodesTrackingStateChanged += Instance_QRCodesTrackingStateChanged;
            QRCodesManager.Instance.QRCodeAdded += Instance_QRCodeAdded;
            QRCodesManager.Instance.QRCodeUpdated += Instance_QRCodeUpdated;
            QRCodesManager.Instance.QRCodeRemoved += Instance_QRCodeRemoved;
            if (qrCodePrefab == null)
            {
                throw new System.Exception("Prefab not assigned");
            }
        }
        // call this whenever the state has changed - line 120 of QRCodesManager - this method is invoked when we start QR Tracking from QRCodesManager
        private void Instance_QRCodesTrackingStateChanged(object sender, bool status)
        {
            if (!status)
            {
                clearExisting = true;
            }
        }

        // listen to QRCodesManager changes on QRCodeVisualizer
        private void Instance_QRCodeAdded(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            Debug.Log("QRCodesVisualizer Instance_QRCodeAdded");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Added, e.Data));
            }
        }

        private void Instance_QRCodeUpdated(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            Debug.Log("QRCodesVisualizer Instance_QRCodeUpdated");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Updated, e.Data)); // Enqueue adds an object to the end of the Queue
            }
        }

        private void Instance_QRCodeRemoved(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            Debug.Log("QRCodesVisualizer Instance_QRCodeRemoved");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Removed, e.Data));
            }
        }

        private void HandleEvents()
        {
            lock (pendingActions)
            {
                while (pendingActions.Count > 0)
                {
                    var action = pendingActions.Dequeue(); // removes an element from the queue FIFO approach
                    if (action.type == ActionData.Type.Added)
                    {

                        //GameObject qrCodeObject = Instantiate(qrCodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        GameObject qrCodeObject = qrCodePrefab;
                        qrCodeObject.SetActive(true);
                        qrCodeObject.GetComponent<SpatialGraphCoordinateSystem>().Id = action.qrCode.SpatialGraphNodeId;
                        //qrCodeObject.GetComponent<QRCode>().qrCode = action.qrCode;
                        qrCodesObjectsList.Add(action.qrCode.Id, qrCodeObject); //QRcode added
                    }
                    else if (action.type == ActionData.Type.Updated)
                    {
                        if (!qrCodesObjectsList.ContainsKey(action.qrCode.Id))
                        {
                            //GameObject qrCodeObject = Instantiate(qrCodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                            GameObject qrCodeObject = qrCodePrefab;
                            qrCodeObject.SetActive(true);
                            qrCodeObject.GetComponent<SpatialGraphCoordinateSystem>().Id = action.qrCode.SpatialGraphNodeId;
                            //qrCodeObject.GetComponent<QRCode>().qrCode = action.qrCode;
                            qrCodesObjectsList.Add(action.qrCode.Id, qrCodeObject);
                        }

                        Pose pose;
                        var found = GetPoseFromSpatialNode(action.qrCode.SpatialGraphNodeId, out pose);
						if (found)
						{
                            if(myQRCodeManager.AddQRCode(action.qrCode.Data, pose))
							{
                                //Debug.Log("New QRCode added to dict! -> " + action.qrCode.Data);
                                handMenu.EndQrDetection();
                                myQRCodeManager.StopScan();
                            }

                            
                        }
                    }
                    else if (action.type == ActionData.Type.Removed)
                    {
                        if (qrCodesObjectsList.ContainsKey(action.qrCode.Id))
                        {
                            Destroy(qrCodesObjectsList[action.qrCode.Id]);
                            qrCodesObjectsList.Remove(action.qrCode.Id);
                        }
                    }
                }
            }
            if (clearExisting)
            {
                clearExisting = false;
                foreach (var obj in qrCodesObjectsList)
                {
                    Destroy(obj.Value);
                }
                qrCodesObjectsList.Clear();

            }
        }

        private bool GetPoseFromSpatialNode(System.Guid nodeId, out Pose pose)
        {

            bool found = false;
            pose = Pose.identity;

#if WINDOWS_UWP
                
                CoordinateSystem = Windows.Perception.Spatial.Preview.SpatialGraphInteropPreview.CreateCoordinateSystemForNode(nodeId);
                

                if (CoordinateSystem != null)
                {
                    //info.text += "\ngot coordinate";
                    Quaternion rotation = Quaternion.identity;
                    Vector3 translation = new Vector3(0.0f, 0.0f, 0.0f);

                    SpatialCoordinateSystem rootSpatialCoordinateSystem = (SpatialCoordinateSystem)System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(UnityEngine.XR.WSA.WorldManager.GetNativeISpatialCoordinateSystemPtr());

                    // Get the relative transform from the unity origin
                    System.Numerics.Matrix4x4? relativePose = CoordinateSystem.TryGetTransformTo(rootSpatialCoordinateSystem);

                    if (relativePose != null)
                    {
                        //info.text += "\n got relative pose";
                        System.Numerics.Vector3 scale;
                        System.Numerics.Quaternion rotation1;
                        System.Numerics.Vector3 translation1;
       
                        System.Numerics.Matrix4x4 newMatrix = relativePose.Value;

                        // Platform coordinates are all right handed and unity uses left handed matrices. so we convert the matrix
                        // from rhs-rhs to lhs-lhs 
                        // Convert from right to left coordinate system
                        newMatrix.M13 = -newMatrix.M13;
                        newMatrix.M23 = -newMatrix.M23;
                        newMatrix.M43 = -newMatrix.M43;

                        newMatrix.M31 = -newMatrix.M31;
                        newMatrix.M32 = -newMatrix.M32;
                        newMatrix.M34 = -newMatrix.M34;

                        System.Numerics.Matrix4x4.Decompose(newMatrix, out scale, out rotation1, out translation1);
                        translation = new Vector3(translation1.X, translation1.Y, translation1.Z);
                        rotation = new Quaternion(rotation1.X, rotation1.Y, rotation1.Z, rotation1.W);
                        pose = new Pose(translation, rotation);
                        found = true;
                      

                        // can be used later using gameObject.transform.SetPositionAndRotation(pose.position, pose.rotation);
                        //Debug.Log("Id= " + id + " QRPose = " +  pose.position.ToString("F7") + " QRRot = "  +  pose.rotation.ToString("F7"));
                    } else {
                          //info.text += "\nrelative pos NULL";
                    }
                } else {
                  //info.text += "\ncannot retrieve coordinate";
                }
                
#endif
            return found;

        }

        // Update is called once per frame
        void Update()
        {
            HandleEvents();
        }
    }

}