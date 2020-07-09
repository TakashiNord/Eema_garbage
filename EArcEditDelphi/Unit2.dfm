object Form2: TForm2
  Left = 0
  Top = 0
  BorderStyle = bsSizeToolWin
  Caption = '........'
  ClientHeight = 393
  ClientWidth = 710
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  FormStyle = fsStayOnTop
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object DBGrid1: TDBGrid
    Left = 0
    Top = 0
    Width = 710
    Height = 393
    Align = alClient
    DataSource = DataSource1
    Options = [dgTitles, dgIndicator, dgColumnResize, dgColLines, dgRowLines, dgTabs, dgConfirmDelete, dgCancelOnExit, dgTitleClick, dgTitleHotTrack]
    ReadOnly = True
    TabOrder = 0
    TitleFont.Charset = DEFAULT_CHARSET
    TitleFont.Color = clWindowText
    TitleFont.Height = -11
    TitleFont.Name = 'Tahoma'
    TitleFont.Style = []
    OnDblClick = DBGrid1DblClick
    OnTitleClick = DBGrid1TitleClick
  end
  object DataSource1: TDataSource
    DataSet = ADOTable1
    Left = 312
    Top = 80
  end
  object ADOConnection1: TADOConnection
    KeepConnection = False
    LoginPrompt = False
    Left = 216
    Top = 24
  end
  object ADOTable1: TADOTable
    Connection = ADOConnection1
    Left = 312
    Top = 24
  end
  object ADOQuery1: TADOQuery
    Connection = ADOConnection1
    Parameters = <>
    Left = 384
    Top = 24
  end
end
