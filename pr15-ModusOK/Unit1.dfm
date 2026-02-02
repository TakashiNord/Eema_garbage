object Form1: TForm1
  Left = 0
  Top = 0
  AutoSize = True
  BorderStyle = bsSingle
  Caption = 'Modus'
  ClientHeight = 44
  ClientWidth = 233
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
    Caption = #1050#1086#1085#1090#1088#1086#1083#1080#1088#1086#1074#1072#1090#1100' Modus'
    TabOrder = 0
    OnClick = Button1Click
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
    Left = 192
    Top = 8
  end
end
