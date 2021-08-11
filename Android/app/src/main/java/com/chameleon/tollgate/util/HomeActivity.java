package com.chameleon.tollgate.util;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.chameleon.tollgate.R;
import com.chameleon.tollgate.faceauth.AuthFaceActivity;
import com.chameleon.tollgate.otp.Activity.OtpActivity;
import com.chameleon.tollgate.qr.Activity.QrActivity;

public class HomeActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);
        Context context = this;


        Button btnTest = findViewById(R.id.btn_test);
        btnTest.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent setFace = new Intent(context, AuthFaceActivity.class);

                setFace.addFlags(Intent.FLAG_ACTIVITY_NO_HISTORY);
                setFace.putExtra("mode", "train");
                startActivity(setFace);

            }
        });

        Button btnQr = findViewById(R.id.btn_qr);
        btnQr.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), QrActivity.class);
                startActivity(intent);
                }
        });

        Button btnOtp = findViewById(R.id.btn_otp);
        btnOtp.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), OtpActivity.class);
                startActivity(intent);
            }
        });

        // 로그 사용
        //tollgateLog log = tollgateLog.getInstance(getApplicationContext()); // 인자는 getApplicatioinContext로 해주세여, getBaseContext 안되요~
        //log.put(logPriority.INFO, logMode.NULL, logType.LOGIN, logResult.SUCCESS, "테스트 로그");

    }

}