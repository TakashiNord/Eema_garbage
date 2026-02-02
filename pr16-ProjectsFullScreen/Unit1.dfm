object Form1: TForm1
  Left = 0
  Top = 0
  AutoSize = True
  BorderStyle = bsSingle
  Caption = 'MinMax'
  ClientHeight = 85
  ClientWidth = 137
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  FormStyle = fsStayOnTop
  OldCreateOrder = False
  Position = poScreenCenter
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 0
    Top = 64
    Width = 128
    Height = 21
    Caption = '--------------------------------'
  end
  object Button1: TButton
    Left = 0
    Top = 0
    Width = 137
    Height = 25
    Caption = #1059#1073#1088#1072#1090#1100' '#1079#1072#1075#1086#1083#1086#1074#1086#1082
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
end
