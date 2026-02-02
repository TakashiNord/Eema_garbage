object Form1: TForm1
  Left = 0
  Top = 0
  BorderStyle = bsSingle
  Caption = 'Modus'
  ClientHeight = 185
  ClientWidth = 298
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  FormStyle = fsStayOnTop
  OldCreateOrder = False
  Position = poScreenCenter
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 8
    Top = 31
    Width = 128
    Height = 13
    Caption = '--------------------------------'
  end
  object Button1: TButton
    Left = 0
    Top = 0
    Width = 233
    Height = 25
    Caption = #1050#1086#1085#1090#1088#1086#1083#1080#1088#1086#1074#1072#1090#1100
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 168
    Top = 56
    Width = 75
    Height = 25
    Caption = 'min'
    TabOrder = 1
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 168
    Top = 119
    Width = 75
    Height = 25
    Caption = 'move'
    TabOrder = 2
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 168
    Top = 88
    Width = 75
    Height = 25
    Caption = 'RESTORE'
    TabOrder = 3
    OnClick = Button4Click
  end
  object Button5: TButton
    Left = 168
    Top = 152
    Width = 75
    Height = 25
    Caption = 'size'
    TabOrder = 4
    OnClick = Button5Click
  end
  object TrayIcon1: TTrayIcon
    OnDblClick = TrayIcon1DblClick
    Left = 104
    Top = 112
  end
  object ApplicationEvents1: TApplicationEvents
    OnMinimize = ApplicationEvents1Minimize
    Left = 24
    Top = 120
  end
  object Timer1: TTimer
    OnTimer = Timer1Timer
    Left = 16
    Top = 56
  end
end
