unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, AppEvnts, tlhelp32;

type
  TForm1 = class(TForm)
    TrayIcon1: TTrayIcon;
    Button1: TButton;
    ApplicationEvents1: TApplicationEvents;
    Label1: TLabel;
    Timer1: TTimer;
    procedure ApplicationEvents1Minimize(Sender: TObject);
    procedure TrayIcon1DblClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    found_window : HWnd;
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

 { коллбэк-функция проверки очередного найденного окна }
function EnumProc(hWindow: HWnd; Param: LongInt): Boolean; stdcall;
var
  buff: Array[0..255] of Char;
  s, s1: String;
begin
  result := true;
  If Boolean(GetClassName(hWindow, buff, SizeOf( buff ))) then
    begin
      s := AnsiLowerCase(StrPas(buff));
      GetWindowText( hWindow, buff, SizeOf( buff ) );
      s1 := AnsiLowerCase(StrPas(buff));
      // вот тут ищем кусок класса  CVIRTLVDChild  или кусок по имени Панели
      If pos(AnsiLowerCase('TF_DemoForm'), s) > 0 then
        begin
          // Showmessage(s);
          Form1.found_window := hWindow;
          Result := False;
        end;
    end;
end;

function EnumChildProc(WndCtrl : HWND; lParam : Longint) : Boolean; stdcall;
var
  buff : array[Byte] of Char;
  s, s1: String;
begin
  Result := True;

  If Boolean(GetClassName(WndCtrl, buff, SizeOf(buff))) then
    begin
      s := AnsiLowerCase(StrPas(buff));
      GetWindowText( WndCtrl, buff, SizeOf( buff ) );
      s1 := AnsiLowerCase(StrPas(buff));
      // вот тут ищем кусок класса  CVIRTLVDChild  или кусок по имени Панели
      If ((pos(AnsiLowerCase('TButton'), s) > 0)  and (pos(AnsiLowerCase('ok'), s1) > 0)) then
        begin
          Form1.found_window := WndCtrl;
          Result := False;
        end;
    end;
end;

procedure TForm1.ApplicationEvents1Minimize(Sender: TObject);
begin
// Hide the window and set its state variable to wsMinimized.
// hide();
//  WindowState := wsMinimized;

  // Show the animated tray icon and also a hint balloon.
  TrayIcon1.Visible := true;
  TrayIcon1.Animate := true;
end;


procedure TForm1.Button1Click(Sender: TObject);
begin
   if Timer1.Enabled=True then  begin
      Timer1.Enabled:=false ;
      Button1.Caption:='Контролировать Modus';
   end
   else begin
      Timer1.Enabled:=true ;
      Button1.Caption:='Не Контролировать Modus';
   end;

end;

procedure TForm1.FormCreate(Sender: TObject);
begin
    Timer1.Enabled:=false;
    Timer1.Interval:=1000;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
var
  Wnd: HWND;                   // Hahdle найденного окна
  Pos: TPoint;                 // Позиция курсора
  Rect: TRect;                 // Координаты окна
  buff: array[0..255] of char; // Буфер
  WndText: string;             // Текст окна
  NameClass: string;           // Класс окна

  B: BOOL;
  ProcList: THandle;
  PE: TProcessEntry32;
  ExeName:string;

  rect1 : TRect;

  Style: Integer ;

  Bf : BOOL ;
  WndBut: HWND;                   // Hahdle найденного окна
begin

 // SchemeViewer.exe - Topaz_Graphics_View
 // PnView.exe   - CVIRTLVDChild.........
 ExeName:='SchemeViewer.exe';
 ProcList := CreateToolHelp32Snapshot(TH32CS_SNAPPROCESS, 0);
 PE.dwSize := SizeOf(PE);
 B := Process32First(ProcList, PE);
 while B do begin
   Bf:=false;
   // if (UpperCase(PE.szExeFile) = UpperCase(ExtractFileName('SchemeViewer.exe'))) then Bf:=true;
   // if (UpperCase(PE.szExeFile) = UpperCase(ExtractFileName('SchemeAdjust.exe'))) then Bf:=true;
   Bf:=true;
   if (Bf=true) then
     begin

{ 1 // Если точно известен Класс }
      Wnd:= FindWindow(PChar('TF_DemoForm'), Nil);

{ 2 // Если имя и класс неизвестно }
      found_window := Invalid_Handle_Value;
      // перечисление всех окон первого урвня:
      EnumWindows(@EnumProc, 0);
      If found_window <> Invalid_Handle_Value then  Wnd:=found_window;

      if IsWindow(Wnd) then
      begin

        GetWindowText( Wnd, buff, SizeOf( buff ) );
        WndText := StrPas( buff );
        GetClassName( Wnd, buff, SizeOf( buff ) );
        NameClass := StrPas( buff );
        Label1.Caption:= WndText;

        // WndBut:= FindWindowEx(Wnd, 0, PChar('TButton'), Nil);
        found_window := Invalid_Handle_Value;
        EnumChildWindows(Wnd, @EnumChildProc, 0);
        If found_window <> Invalid_Handle_Value then  WndBut:=found_window;

        if  IsWindow(WndBut) then begin
           //ShowMessage('Find');
           SendMessage(WndBut, BM_CLICK, 0, 0);
        end;

        Refresh;

      end;
      break ; // изменяем одно окно

     end;
   B := Process32Next(ProcList, PE);
 end;
 CloseHandle(ProcList);
end;

procedure TForm1.TrayIcon1DblClick(Sender: TObject);
begin
  // Hide the tray icon and show the window,
  // setting its state property to wsNormal.
  TrayIcon1.Visible := false;
  Show();
  WindowState := wsNormal;
  Application.BringToFront();
end;

end.
