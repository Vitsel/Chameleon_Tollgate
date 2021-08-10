﻿using AuthClient.tollgate.otp.service;
using System;
using System.Windows.Forms;

namespace AuthClient.tollgate.otp.dialog
{
    public partial class OtpTest : Form
    {
        public OtpTest()
        {
            InitializeComponent();
            Config.InitializeBaseURL("localhost:8080", true);
        }


        private void button_Login_Click(object sender, EventArgs e)
        {

        }

        private void button_Register_Click(object sender, EventArgs e)
        {

        }

        private void button_OtpCertification_Click(object sender, EventArgs e)
        {
            OtpService OS = new OtpService();

            if (OS.PostOtpRegister(textBox_ID.Text) == ReturnMessageValue.SUCCESS)
                MessageBox.Show("성공");
            else
                MessageBox.Show("실패");
        }

        private void button_OtpSend_Click(object sender, EventArgs e)
        {
            OtpService OS = new OtpService();
            if (OS.PostOtpVerify(textBox_ID.Text, textBox_InputOtp.Text) == ReturnMessageValue.SUCCESS)
                MessageBox.Show("인증 성공");
            else
                MessageBox.Show("인증 실패");

        }

    }
}
