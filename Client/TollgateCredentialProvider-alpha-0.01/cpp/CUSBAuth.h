#pragma once


#include <Windows.h>
#include "CTollgateCredential.h"

#include "CUSB.h"


class CUSBAuth
{
public:
    CUSBAuth(void);
	~CUSBAuth(void);
    HRESULT InitAuthThread(CTollgateCredential* pCredential);

private:
    HRESULT _MyRegisterClass(void);         // ������ Ŭ���� ���
    HRESULT _InitInstance();                // ������ ���� �� ����̽� ���� �̺�Ʈ�� ���� ������ ���
    BOOL _ProcessNextMessage();             // �������� �޽��� ó��

    static DWORD WINAPI     _ThreadProc(LPVOID lpParameter);            // ������ Ŭ���� ���/���� �� �޽��� ó��
    static LRESULT CALLBACK _WndProc(HWND, UINT, WPARAM, LPARAM);       // Device Changed �޽��� ó��

    HWND            _hWnd;                      // Handle to this window
    HINSTANCE       _hInst;                     // Current instance

    CTollgateCredential*  _pCredential;
};
