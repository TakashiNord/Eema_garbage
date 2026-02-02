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
    procedure ApplicationEvents1Minimize(Sender: TObject);
    procedure TrayIcon1DblClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
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
      If pos(AnsiLowerCase('CVIRTLVDChild'), s) > 0 then
        begin
          // Showmessage(s);
          Form1.found_window := hWindow;
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

begin

 // SchemeViewer.exe - Topaz_Graphics_View
 // PnView.exe   - CVIRTLVDChild.........
 ExeName:='PnView.exe';
 ProcList := CreateToolHelp32Snapshot(TH32CS_SNAPPROCESS, 0);
 PE.dwSize := SizeOf(PE);
 B := Process32First(ProcList, PE);
 while B do begin
   if (UpperCase(PE.szExeFile) = UpperCase(ExtractFileName(ExeName))) then
     begin

{ 1 // Если точно известен Класс }
      Wnd:= FindWindow(PChar('Pnview'), Nil);

{ 2 // Если имя и класс неизвестно }
      found_window := Invalid_Handle_Value;
      // перечисление всех окон первого урвня:
      EnumWindows(@EnumProc, 0);
      If found_window <> Invalid_Handle_Value then  Wnd:=found_window;

      if Wnd <> 0 then
      begin
        GetWindowRect( Wnd, Rect );
        GetWindowText( Wnd, buff, SizeOf( buff ) );
        WndText := StrPas( buff );
        GetClassName( Wnd, buff, SizeOf( buff ) );
        NameClass := StrPas( buff );
        Label1.Caption:= WndText;

        if (Getwindowlong(Wnd, GWL_STYLE) and WS_CAPTION) <> 0 then begin
           if (GetWindowRect(GetDesktopWindow, rect1)) then
              SetWindowPos(Wnd, HWND_TOPMOST, rect.left, rect.top, rect.right, rect.bottom, SW_SHOWNORMAL);
           SetWindowLong(Wnd, GWL_STYLE, GetWindowLong (Wnd, GWL_STYLE) AND NOT WS_CAPTION );
           Showwindow(Wnd,SW_MAXIMIZE);
           Button1.Caption:='Показать заголовок';
        end else begin
           SetWindowPos(Wnd, HWND_NOTOPMOST, 40, 40, 300, 300, SW_SHOWNORMAL);
           SetWindowLong(Wnd, GWL_STYLE, GetWindowLong (Wnd, GWL_STYLE) or WS_CAPTION );
           Showwindow(Wnd,SW_NORMAL);
           Button1.Caption:='Убрать заголовок';
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
