﻿using AuthClient.tollgate.usb.dto;
using AuthClient.tollgate.usb.service;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace AuthClient.tollgate.usb.dialog
{
    public partial class USBRegisterDialog : Form
    {
        private string usb_alias;

        public USBRegisterDialog()
        {
            InitializeComponent();
            usbRecognitionTimer.Start();
            registerOKButton.Hide();
            registerCancelButton.Hide();
            progressBar1.Show();

            this.usb_alias = "";
        }

        private void usbRecognitionTimer_Tick(object sender, EventArgs e)
        {
            // --------------- 실시간 탐지 ---------------

            DriveInfo[] driveArray = DriveInfo.GetDrives();

            foreach (DriveInfo item in driveArray)
            {
                // 드라이브 타입 및 Ready 여부 검사
                if (item.IsReady == true && item.DriveType == DriveType.Removable)
                {
                    usbRecognitionTimer.Stop();

                    statusMessageLabel.Text = "다음 USB 장치가 검색되었습니다\r\n";
                    statusMessageLabel.Text += (item.VolumeLabel + "\r\n");
                    this.usb_alias = item.VolumeLabel;
                    statusMessageLabel.Text += "해당 장치를 인증 서버에 등록하시겠습니까?";

                    progressBar1.Hide();
                    registerOKButton.Show();
                    registerCancelButton.Show();
                }
            }
        }

        private void registerOKButton_Click(object sender, EventArgs e)
        {
            string usbID = ReadUSBInfoByRegistry();

            // VID, PID 등의 정보 불러오기 실패
            if(usbID.Length == 0)
            {
                MessageBox.Show("USB 정보를 불러올 수 없습니다.");
                this.DialogResult = DialogResult.Cancel;
            }
            // VID, PID 등의 정보 불러오기 성공
            else
            {
                // USB 정보 문자열 가공
                usbID = usbID.Replace("\\", "");
                usbID = usbID.Replace("&", "");

                USBInfo usbInfo = new USBInfo(Config.GetCurrentUser(), Util.EncryptSHA512(usbID), this.usb_alias);
                USBService us = new USBService();

                if(us.RegisterUSBInfo(usbInfo))
                {
                    this.DialogResult = DialogResult.OK;
                } 
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void registerCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private string ReadUSBInfoByRegistry()
        {
            // 레지스트리 값을 읽어 연결된 USB의 VID, PID 등의 정보를 추출
            string regUSBInfo = "";

            RegistryKey r_key = Registry.LocalMachine;
            RegistryKey r_system = r_key.OpenSubKey("SYSTEM");
            RegistryKey r_currentControlSet = r_system.OpenSubKey("CurrentControlSet");
            RegistryKey r_services = r_currentControlSet.OpenSubKey("Services");
            RegistryKey r_usbstor = r_services.OpenSubKey("USBSTOR");
            RegistryKey r_enum = r_usbstor.OpenSubKey("Enum");

            if (r_currentControlSet != null)
            {
                regUSBInfo = (string)r_enum.GetValue("0");
                r_enum.Close();
            }

            return regUSBInfo;
        }
    }
}
