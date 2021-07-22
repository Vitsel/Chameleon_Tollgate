﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AuthClient.tollgate.account.service;
using AuthClient.tollgate.otp.service;
using AuthClient.tollgate.rest;

using AuthClient.tollgate.account.dialog;

namespace AuthClient.tollgate.otp.dialog
{
    public partial class OtpTest : Form
    {
        public OtpTest()
        {
            InitializeComponent();
            Config.InitializeBaseURL("localhost:8080",true);
        }


        private void button_Login_Click(object sender, EventArgs e)
        {
            LogOnDialog LOD = new LogOnDialog();
            LOD.Show();
        }

        private void button_Register_Click(object sender, EventArgs e)
        {
            LogOnDialog LOSUD = new LogOnDialog();
            LOSUD.Show();
        }

        private void button_OtpCertification_Click(object sender, EventArgs e)
        {
            OtpService OS = new OtpService();

            if (OS.PostOtpRegister(textBox_ID.Text) == ReturnMessageValue.SUCCESS)
                MessageBox.Show("핸드폰으로 OTP를 발송 했습니다.");
            else
                MessageBox.Show("서버로부터 전송하지 못했습니다.");
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
