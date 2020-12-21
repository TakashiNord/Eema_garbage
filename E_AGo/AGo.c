
#include <windows.h>
#include <defs.h>


//-------------------------------------------------------------------------
// Function declarations

void __noreturn start();
BOOL sub_4021B5();
unsigned __int32 sub_40220E();
void __stdcall __noreturn sub_4025DF(LPVOID lpThreadParameter);
void __stdcall __noreturn StartAddress(LPVOID lpThreadParameter);
HWND sub_40276D();
void __stdcall TimerFunc(HWND a1, UINT a2, UINT_PTR a3, DWORD a4);
LRESULT __stdcall sub_40282E(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);
int __stdcall sub_402902(int, HINSTANCE hInst); // idb
BOOL sub_402992();
// HMODULE __stdcall GetModuleHandleA(LPCSTR lpModuleName);
// void __stdcall ExitProcess(UINT uExitCode);
// LPSTR __stdcall GetCommandLineA();
// BOOL __stdcall CloseHandle(HANDLE hObject);
// BOOL __stdcall CopyFileA(LPCSTR lpExistingFileName, LPCSTR lpNewFileName, BOOL bFailIfExists);
// BOOL __stdcall CreateProcessA(LPCSTR lpApplicationName, LPSTR lpCommandLine, LPSECURITY_ATTRIBUTES lpProcessAttributes, LPSECURITY_ATTRIBUTES lpThreadAttributes, BOOL bInheritHandles, DWORD dwCreationFlags, LPVOID lpEnvironment, LPCSTR lpCurrentDirectory, LPSTARTUPINFOA lpStartupInfo, LPPROCESS_INFORMATION lpProcessInformation);
// HANDLE __stdcall CreateThread(LPSECURITY_ATTRIBUTES lpThreadAttributes, SIZE_T dwStackSize, LPTHREAD_START_ROUTINE lpStartAddress, LPVOID lpParameter, DWORD dwCreationFlags, LPDWORD lpThreadId);
// BOOL __stdcall DeleteFileA(LPCSTR lpFileName);
// void __stdcall ExitThread(DWORD dwExitCode);
// DWORD __stdcall GetPrivateProfileStringA(LPCSTR lpAppName, LPCSTR lpKeyName, LPCSTR lpDefault, LPSTR lpReturnedString, DWORD nSize, LPCSTR lpFileName);
// HLOCAL __stdcall LocalAlloc(UINT uFlags, SIZE_T uBytes);
// HLOCAL __stdcall LocalFree(HLOCAL hMem);
// int __stdcall RtlZeroMemory(_DWORD, _DWORD); weak
// DWORD __stdcall WaitForSingleObject(HANDLE hHandle, DWORD dwMilliseconds);
// HGDIOBJ __stdcall GetStockObject(int i);
// HWND __stdcall CreateWindowExA(DWORD dwExStyle, LPCSTR lpClassName, LPCSTR lpWindowName, DWORD dwStyle, int X, int Y, int nWidth, int nHeight, HWND hWndParent, HMENU hMenu, HINSTANCE hInstance, LPVOID lpParam);
// LRESULT __stdcall DefWindowProcA(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);
// BOOL __stdcall DestroyWindow(HWND hWnd);
// LRESULT __stdcall DispatchMessageA(const MSG *lpMsg);
// HWND __stdcall FindWindowA(LPCSTR lpClassName, LPCSTR lpWindowName);
// BOOL __stdcall GetMessageA(LPMSG lpMsg, HWND hWnd, UINT wMsgFilterMin, UINT wMsgFilterMax);
// LONG __stdcall GetWindowLongA(HWND hWnd, int nIndex);
// BOOL __stdcall KillTimer(HWND hWnd, UINT_PTR uIDEvent);
// HCURSOR __stdcall LoadCursorA(HINSTANCE hInstance, LPCSTR lpCursorName);
// HANDLE __stdcall LoadImageA(HINSTANCE hInst, LPCSTR name, UINT type, int cx, int cy, UINT fuLoad);
// int __stdcall MessageBoxA(HWND hWnd, LPCSTR lpText, LPCSTR lpCaption, UINT uType);
// void __stdcall PostQuitMessage(int nExitCode);
// ATOM __stdcall RegisterClassA(const WNDCLASSA *lpWndClass);
// UINT __stdcall RegisterWindowMessageA(LPCSTR lpString);
// UINT_PTR __stdcall SetTimer(HWND hWnd, UINT_PTR nIDEvent, UINT uElapse, TIMERPROC lpTimerFunc);
// LONG __stdcall SetWindowLongA(HWND hWnd, int nIndex, LONG dwNewLong);
// BOOL __stdcall ShowWindow(HWND hWnd, int nCmdShow);
// BOOL __stdcall TranslateMessage(const MSG *lpMsg);
// BOOL __stdcall UpdateWindow(HWND hWnd);
// int wsprintfA(LPSTR, LPCSTR, ...);
// BOOL __stdcall SetLayeredWindowAttributes(HWND hwnd, COLORREF crKey, BYTE bAlpha, DWORD dwFlags);
// HINSTANCE __stdcall ShellExecuteA(HWND hwnd, LPCSTR lpOperation, LPCSTR lpFile, LPCSTR lpParameters, LPCSTR lpDirectory, INT nShowCmd);
// BOOL __stdcall Shell_NotifyIconA(DWORD dwMessage, PNOTIFYICONDATAA lpData);
// BOOL __stdcall SHGetPathFromIDListA(LPCITEMIDLIST pidl, LPSTR pszPath);
// HRESULT __stdcall SHGetSpecialFolderLocation(HWND hwnd, int csidl, LPITEMIDLIST *ppidl);
// char *__cdecl strcat(char *Dest, const char *Source);
// int __cdecl strcmp(const char *Str1, const char *Str2);
// BOOL __stdcall sndPlaySoundA(LPCSTR pszSound, UINT fuSound);

//-------------------------------------------------------------------------
// Data declarations

int dword_401000; // weak
int dword_401004; // weak
_UNKNOWN unk_401008; // weak
LPDWORD lpThreadId; // idb
LPDWORD dword_40110C; // idb
HANDLE dword_401110; // idb
HANDLE dword_401114; // idb
int dword_401118; // weak
HINSTANCE hInst; // idb
int dword_401120; // weak
struct _NOTIFYICONDATAA Data; // idb
char byte_4020A5[] = { 'П' }; // weak


//----- (00402000) --------------------------------------------------------
void __noreturn start()
{
  LPSTR v0; // eax@1
  _UNKNOWN *v1; // edi@1
  int v2; // edx@1
  LPSTR v3; // esi@1
  char v4; // ah@1
  char v5; // bl@2
  char v6; // al@3
  unsigned __int32 v7; // eax@13

  dword_401000 = (int)GetModuleHandleA(0);
  v0 = GetCommandLineA();
  v1 = &unk_401008;
  v2 = 0;
  v3 = v0;
  v4 = 0;
LABEL_2:
  v5 = 1;
  do
    v6 = *v3++;
  while ( v6 == 32 );
  while ( 1 )
  {
    if ( !v6 )
    {
      *(_BYTE *)v1 = 0;
      *((_BYTE *)v1 + 1) = 0;
      dword_401004 = v2;
      v7 = sub_40220E();
      ExitProcess(v7);
    }
    if ( v6 == 34 )
    {
      v4 ^= 1u;
    }
    else
    {
      if ( v6 == 32 && !v4 )
      {
        *(_BYTE *)v1 = 0;
        v1 = (char *)v1 + 1;
        goto LABEL_2;
      }
      *(_BYTE *)v1 = v6;
      v1 = (char *)v1 + 1;
    }
    v6 = *v3++;
    LOBYTE(v2) = v5 + v2;
    v5 = 0;
  }
}
// 401000: using guessed type int dword_401000;
// 401004: using guessed type int dword_401004;

//----- (004021B5) --------------------------------------------------------
BOOL sub_4021B5()
{
  GetPrivateProfileStringA("Main", "Alarm", "0", *(LPSTR *)&Data.szInfo[152], 2u, *(LPCSTR *)&Data.szInfo[156]);
  *(_DWORD *)&Data.szInfo[32] = strcmp(*(const char **)&Data.szInfo[152], "1");
  return *(_DWORD *)&Data.szInfo[32] == 0;
}

//----- (0040220E) --------------------------------------------------------
unsigned __int32 sub_40220E()
{
  _UNKNOWN *v0; // eax@1
  unsigned __int32 result; // eax@6
  char *v3; // eax@10
  char *v4; // eax@10
  char *v5; // eax@10
  WNDCLASSA WndClass; // [sp+0h] [bp-44h]@14
  MSG Msg; // [sp+28h] [bp-1Ch]@15

  v0 = &unk_401008;
  wsprintfA(&Data.szInfo[36], "%s", v0);
  *(_DWORD *)&Data.szInfo[32] = strcmp("R:\\bin\\.t\\AGo.exe", &Data.szInfo[36]);
  if ( *(_DWORD *)&Data.szInfo[32] )
  {
    result = MessageBoxA(0, "Ошибка в процессе запуска.\r\nОбратитесь к администратору.", "Ошибка", 0x1010u);
  }
  else if ( FindWindowA(0, "Appbar Revisor") )
  {
    result = MessageBoxA(0, "Приложение уже запущено!", "Внимание", 0x1030u);
  }
  else
  {
    ShellExecuteA(
      0,
      "open",
      "cmd.exe",
      "/q /c reg.exe ADD \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\ZoneMap\\Domains\\rsdudb\" /V file /t REG_DWORD /D 1 /F",
      0,
      0);
    result = SHGetSpecialFolderLocation(0, 26, (LPITEMIDLIST *)&Data.szInfo[140]);
    if ( *(_DWORD *)&Data.szInfo[140] )
    {
      *(_DWORD *)&Data.szInfo[144] = LocalAlloc(0x40u, 0x100u);
      *(_DWORD *)&Data.szInfo[148] = LocalAlloc(0x40u, 0x100u);
      SHGetPathFromIDListA(*(LPCITEMIDLIST *)&Data.szInfo[140], *(LPSTR *)&Data.szInfo[144]);
      v3 = strcat(*(char **)&Data.szInfo[148], *(const char **)&Data.szInfo[144]);
      *(_DWORD *)&Data.szInfo[148] = strcat(v3, "\\AGo.wav");
      v4 = (char *)LocalAlloc(0x40u, 0x100u);
      *(_DWORD *)&Data.szInfo[156] = v4;
      v5 = strcat(v4, *(const char **)&Data.szInfo[144]);
      *(_DWORD *)&Data.szInfo[156] = strcat(v5, "\\AGo.cfg");
      LocalFree(*(HLOCAL *)&Data.szInfo[144]);
      *(_DWORD *)&Data.szInfo[152] = LocalAlloc(0x40u, 2u);
      if ( sub_4021B5() )
        CopyFileA("R:\\bin\\.t\\alarm.wav", *(LPCSTR *)&Data.szInfo[148], 0);
      memset(&Data.szTip[84], 0, 0x44u);
      memset(&Data.szInfo[16], 0, 0x10u);
      if ( CreateProcessA(
             "R:\\bin\\Appbar.bin",
             0,
             0,
             0,
             0,
             0x20u,
             0,
             "R:\\bin\\",
             (LPSTARTUPINFOA)&Data.szTip[84],
             (LPPROCESS_INFORMATION)&Data.szInfo[16]) )
      {
        hInst = GetModuleHandleA(0);
        WndClass.lpszClassName = "Appbar Revisor";
        WndClass.hInstance = hInst;
        WndClass.lpfnWndProc = sub_40282E;
        WndClass.hCursor = LoadCursorA(0, (LPCSTR)0x7F00);
        WndClass.hIcon = 0;
        WndClass.lpszMenuName = 0;
        WndClass.hbrBackground = (HBRUSH)GetStockObject(2);
        WndClass.style = 3;
        WndClass.cbClsExtra = 0;
        WndClass.cbWndExtra = 0;
        RegisterClassA(&WndClass);
        *(_DWORD *)&Data.szTip[64] = RegisterWindowMessageA("TaskbarCreated");
        *(_DWORD *)&Data.szInfo[136] = CreateWindowExA(
                                         0,
                                         "Appbar Revisor",
                                         "Appbar Revisor",
                                         0xCF0000u,
                                         2147483648,
                                         2147483648,
                                         2147483648,
                                         2147483648,
                                         0,
                                         0,
                                         hInst,
                                         0);
        ShowWindow(*(HWND *)&Data.szInfo[136], 0);
        UpdateWindow(*(HWND *)&Data.szInfo[136]);
        dword_401110 = CreateThread(0, 0, (LPTHREAD_START_ROUTINE)StartAddress, 0, 0, lpThreadId);
        *(_DWORD *)&Data.szTip[68] = SetTimer(0, 0, 0x1388u, TimerFunc);
        while ( GetMessageA(&Msg, 0, 0, 0) )
        {
          TranslateMessage(&Msg);
          DispatchMessageA(&Msg);
        }
        result = Msg.wParam;
      }
      else
      {
        result = MessageBoxA(
                   0,
                   "Ошибка в процессе запуска панели оператора РСДУ2.\r\nОбратитесь к администратору.",
                   "Ошибка",
                   0x1010u);
      }
    }
  }
  return result;
}

//----- (004025DF) --------------------------------------------------------
void __stdcall __noreturn sub_4025DF(LPVOID lpThreadParameter)
{
  while ( dword_401118 == 1 )
    sndPlaySoundA(*(LPCSTR *)&Data.szInfo[148], 0x12u);
  ExitThread(0);
}
// 401118: using guessed type int dword_401118;

//----- (0040260B) --------------------------------------------------------
void __stdcall __noreturn StartAddress(LPVOID lpThreadParameter)
{
  while ( WaitForSingleObject(*(HANDLE *)&Data.szInfo[16], 0x64u) == 258 )
    ;
  CloseHandle(*(HANDLE *)&Data.szInfo[16]);
  CloseHandle(*(HANDLE *)&Data.szInfo[20]);
  sub_402992();
  if ( !sub_40276D() && *(_DWORD *)&Data.szTip[72] )
  {
    dword_401118 = 1;
    if ( sub_4021B5() )
      dword_401114 = CreateThread(0, 0, (LPTHREAD_START_ROUTINE)sub_4025DF, 0, 0, dword_40110C);
    MessageBoxA(
      0,
      "Панель оператора РСДУ2 закрыта.\r\nКлиент сигнальной системы и информационные панели не работают.\r\nПри необходимости закройте все приложения РСДУ2 и запустите панель оператора повторно.",
      "Внимание",
      0x1040u);
    dword_401118 = 0;
  }
  *(_DWORD *)&Data.szTip[76] = 1;
  ExitThread(0);
}
// 401118: using guessed type int dword_401118;

//----- (0040276D) --------------------------------------------------------
HWND sub_40276D()
{
  HWND result; // eax@2

  if ( FindWindowA("#32770", "Appbar") )
    result = 0;
  else
    result = FindWindowA(0, "Appbar");
  return result;
}

//----- (004027A6) --------------------------------------------------------
void __stdcall TimerFunc(HWND a1, UINT a2, UINT_PTR a3, DWORD a4)
{
  HWND v4; // eax@4
  LONG v5; // ST14_4@5
  HWND hWnd; // [sp+0h] [bp-8h]@4

  if ( *(_DWORD *)&Data.szTip[76] )
    DestroyWindow(*(HWND *)&Data.szInfo[136]);
  if ( !*(_DWORD *)&Data.szTip[72] )
  {
    v4 = sub_40276D();
    hWnd = v4;
    if ( v4 )
    {
      *(_DWORD *)&Data.szTip[72] = 1;
      v5 = GetWindowLongA(v4, -20);
      SetWindowLongA(hWnd, -20, v5 | 0x80000);
      SetLayeredWindowAttributes(hWnd, 0, 0xBEu, 2u);
      if ( !*(_DWORD *)&Data.szTip[76] )
        sub_402902(*(int *)&Data.szInfo[136], hInst);
    }
  }
}

//----- (0040282E) --------------------------------------------------------
LRESULT __stdcall sub_40282E(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam)
{
  if ( Msg == *(_DWORD *)&Data.szTip[64] )
    sub_402902((int)hWnd, hInst);
  if ( Msg != 101 )
  {
    if ( Msg == 2 )
    {
      sub_402992();
      KillTimer(hWnd, *(UINT_PTR *)&Data.szTip[68]);
      CloseHandle(dword_401110);
      CloseHandle(dword_401114);
      DeleteFileA(*(LPCSTR *)&Data.szInfo[148]);
      LocalFree(*(HLOCAL *)&Data.szInfo[148]);
      LocalFree(*(HLOCAL *)&Data.szInfo[152]);
      LocalFree(*(HLOCAL *)&Data.szInfo[156]);
      PostQuitMessage(0);
    }
    else
    {
      if ( Msg != 273 )
        return DefWindowProcA(hWnd, Msg, wParam, lParam);
      if ( wParam == 2003 )
        DestroyWindow(hWnd);
    }
  }
  return 0;
}

//----- (00402902) --------------------------------------------------------
BOOL __stdcall sub_402902(int a1, HINSTANCE hInst)
{
  signed int i; // [sp+0h] [bp-4h]@1

  RtlZeroMemory(&Data, 88);
  dword_401120 = (int)LoadImageA(hInst, (LPCSTR)0xC7, 1u, 16, 16, 0);
  Data.cbSize = 88;
  Data.hWnd = (HWND)a1;
  Data.uID = 1000;
  Data.uFlags = 7;
  Data.uCallbackMessage = 101;
  Data.hIcon = (HICON)dword_401120;
  for ( i = 0; i < 64; ++i )
    Data.szTip[i] = byte_4020A5[i];
  return Shell_NotifyIconA(0, &Data);
}
// 402902: could not find valid save-restore pair for esi
// 401120: using guessed type int dword_401120;
// 402B20: using guessed type int __stdcall RtlZeroMemory(_DWORD, _DWORD);

//----- (00402992) --------------------------------------------------------
BOOL sub_402992()
{
  return Shell_NotifyIconA(2u, &Data);
}

// ALL OK, 10 function(s) have been successfully decompiled
