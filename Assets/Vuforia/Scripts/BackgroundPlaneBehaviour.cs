/*==============================================================================
Copyright (c) 2014 Qualcomm Connected Experiences, Inc. All Rights Reserved.
 
Confidential and Proprietary – Qualcomm Connected Experiences, Inc.
Vuforia is a trademark of QUALCOMM Incorporated, registered in the United States
and other countries. Trademarks of QUALCOMM Incorporated are used with permission.
==============================================================================*/

using System;
using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// The BackgroundPlaneBehaviour class creates a mesh at the far end 
    /// of camera frustum over which video background is rendered.
    /// </summary>
    public class BackgroundPlaneBehaviour : BackgroundPlaneAbstractBehaviour
    {
        void LateUpdate()
        {
            transform.localScale = new Vector3(1920 * 2.2f, 1, 1080 * 4.2f);
        }
    }
}
