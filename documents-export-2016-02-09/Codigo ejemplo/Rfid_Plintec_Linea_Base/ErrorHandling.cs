﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace RFid_Plintec
{
    /// <summary>
    /// 异常处理对象，集中处理操作DLL时返回的错误码；
    /// </summary>
    public class ErrorHandling
    {
        private static string[] errorMsg = { 
                                           "Success!", "Input configuration parameter invalid!", "No login!", "Authenticate fail!",
                                           "Socket error!", "Not enough memory!", "Device no response!", "Function's argument error!",
                                           "Error response from device!", "Operation is not supported!"
                                           };

        /// <summary>
        /// 获取错误说明信息；
        /// </summary>
        /// <param name="eCode">tagErrorCode</param>
        /// <returns>错误说明信息</returns>
        public static string GetErrorMsg(DevControl.tagErrorCode eCode)
        {
            Debug.Assert((uint)eCode < errorMsg.Length);

            return errorMsg[(uint)eCode];
        }

        /// <summary>
        /// 集中处理操作返回错误码，并生成错误说明信息；
        /// </summary>
        /// <param name="eCode">tagErrorCode</param>
        /// <returns>错误说明信息</returns>
        public static string HandleError(DevControl.tagErrorCode eCode)
        {
            string errorMsg;

            switch (eCode)
            {
                case DevControl.tagErrorCode.DM_ERR_OK:
                case DevControl.tagErrorCode.DM_ERR_PARA:
                case DevControl.tagErrorCode.DM_ERR_AUTHFAIL:
                case DevControl.tagErrorCode.DM_ERR_TIMEOUT:
                case DevControl.tagErrorCode.DM_ERR_OPR:
                    errorMsg = GetErrorMsg(eCode);
                    break;

                case DevControl.tagErrorCode.DM_ERR_NOAUTH:
                    errorMsg = GetErrorMsg(eCode);
                    break;

                case DevControl.tagErrorCode.DM_ERR_SOCKET:
                case DevControl.tagErrorCode.DM_ERR_MEM:
                case DevControl.tagErrorCode.DM_ERR_MATCH:
                case DevControl.tagErrorCode.DM_ERR_ARG:
                    errorMsg = GetErrorMsg(eCode);
                    Log.WriteError(errorMsg);
                    errorMsg = "Soft internal error!";
                    break;

                default:
                    errorMsg = "No support this tagErrorCode!";
                    Debug.Fail(errorMsg);
                    break;
            };

            return errorMsg;
        }

        /// <summary>
        /// 集中处理操作返回错误码，并生成错误说明信息；
        /// </summary>
        /// <param name="eCode">tagErrorCode</param>
        /// <returns>错误说明信息</returns>
        public static string HandleDeviceError(DevControl.tagErrorCode eCode, DeviceClass device)
        {
            string errorMsg;

            if (eCode == DevControl.tagErrorCode.DM_ERR_NOAUTH)
            {
                LoginForm loginform = new LoginForm();
                DialogResult result = loginform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    eCode = device.Login(loginform.UserName, loginform.Password);
                }
            }

            errorMsg = HandleError(eCode);

            return errorMsg;
        }
    }
}
