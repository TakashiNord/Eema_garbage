object Form1: TForm1
  Left = 0
  Top = 0
  Caption = #1044#1080#1089#1087#1077#1090#1095#1077#1088#1089#1082#1080#1077' '#1087#1086#1084#1077#1090#1082#1080
  ClientHeight = 538
  ClientWidth = 784
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Splitter1: TSplitter
    Left = 185
    Top = 59
    Height = 460
    ExplicitLeft = 200
    ExplicitTop = 296
    ExplicitHeight = 100
  end
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 784
    Height = 33
    Align = alTop
    TabOrder = 0
    object Label1: TLabel
      Left = 8
      Top = 3
      Width = 23
      Height = 13
      Caption = 'tns='
    end
    object Label2: TLabel
      Left = 104
      Top = 3
      Width = 30
      Height = 13
      Caption = 'login='
    end
    object Label3: TLabel
      Left = 223
      Top = 3
      Width = 54
      Height = 13
      Caption = 'password='
    end
    object Edit1: TEdit
      Left = 29
      Top = 2
      Width = 60
      Height = 21
      TabOrder = 0
      Text = 'rsdu2'
    end
    object Edit2: TEdit
      Left = 132
      Top = 2
      Width = 77
      Height = 21
      TabOrder = 1
      Text = 'rsduadmin'
    end
    object Edit3: TEdit
      Left = 283
      Top = 2
      Width = 70
      Height = 21
      TabOrder = 2
      Text = 'passme'
    end
    object Button1: TButton
      Left = 368
      Top = 2
      Width = 75
      Height = 25
      Caption = '<Connect>'
      TabOrder = 3
      OnClick = Button1Click
    end
  end
  object Panel2: TPanel
    Left = 188
    Top = 59
    Width = 596
    Height = 460
    Align = alClient
    Caption = 'Panel2'
    TabOrder = 1
    object Splitter2: TSplitter
      Left = 1
      Top = 367
      Width = 594
      Height = 3
      Cursor = crVSplit
      Align = alBottom
      ExplicitTop = 1
      ExplicitWidth = 369
    end
    object DBGrid1: TDBGrid
      Left = 1
      Top = 1
      Width = 594
      Height = 366
      Align = alClient
      DataSource = DataSource1
      Options = [dgEditing, dgTitles, dgIndicator, dgColumnResize, dgColLines, dgRowLines, dgTabs, dgCancelOnExit, dgTitleClick, dgTitleHotTrack]
      TabOrder = 0
      TitleFont.Charset = DEFAULT_CHARSET
      TitleFont.Color = clWindowText
      TitleFont.Height = -11
      TitleFont.Name = 'Tahoma'
      TitleFont.Style = []
      OnTitleClick = DBGrid1TitleClick
    end
    object RichEdit1: TRichEdit
      Left = 1
      Top = 370
      Width = 594
      Height = 89
      Align = alBottom
      Font.Charset = RUSSIAN_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = []
      Lines.Strings = (
        'RichEdit1')
      ParentFont = False
      ScrollBars = ssBoth
      TabOrder = 1
    end
  end
  object Panel3: TPanel
    Left = 0
    Top = 33
    Width = 784
    Height = 26
    Align = alTop
    TabOrder = 2
    object DBNavigator1: TDBNavigator
      Left = 543
      Top = 1
      Width = 240
      Height = 24
      DataSource = DataSource1
      VisibleButtons = [nbFirst, nbPrior, nbNext, nbLast, nbDelete, nbEdit, nbPost, nbCancel, nbRefresh]
      Align = alRight
      ConfirmDelete = False
      TabOrder = 0
    end
    object Button2: TButton
      Left = 1
      Top = 1
      Width = 133
      Height = 24
      Hint = #1059#1076#1072#1083#1077#1085#1080#1077' '#1082#1086#1086#1088#1076#1080#1085#1072#1090', '#1077#1089#1083#1080' '#1076#1083#1103' '#1085#1080#1093' '#1085#1077#1090' '#1044#1055' (1 '#1089#1093#1077#1084#1072')'
      Align = alLeft
      Caption = 'id tag_list=tag_position'
      ParentShowHint = False
      ShowHint = True
      TabOrder = 1
      OnClick = Button2Click
    end
    object CheckBox1: TCheckBox
      Left = 415
      Top = 4
      Width = 113
      Height = 17
      Caption = 'use from_dt1970()'
      TabOrder = 2
    end
    object Button3: TButton
      Left = 255
      Top = 2
      Width = 82
      Height = 24
      Hint = #1059#1076#1072#1083#1077#1085#1080#1077' '#1044#1055' '#1085#1077' '#1087#1088#1080#1074#1103#1079#1072#1085#1085#1099#1093' '#1085#1080' '#1082' '#1086#1076#1085#1086#1081' '#1080#1079' '#1089#1093#1077#1084
      Caption = 'all tag_list=?'
      ParentShowHint = False
      ShowHint = True
      TabOrder = 3
      OnClick = Button3Click
    end
    object Button4: TButton
      Left = 140
      Top = 2
      Width = 109
      Height = 24
      Hint = #1059#1076#1072#1083#1077#1085#1080#1077' '#1085#1077#1074#1080#1076#1080#1084#1099#1093' '#1044#1055' (1 '#1089#1093#1077#1084#1072') '
      Caption = 'id tag_list=visible(0)'
      ParentShowHint = False
      ShowHint = True
      TabOrder = 4
      OnClick = Button4Click
    end
  end
  object Panel4: TPanel
    Left = 0
    Top = 59
    Width = 185
    Height = 460
    Align = alLeft
    Caption = 'Panel4'
    TabOrder = 3
    object TreeView1: TTreeView
      Left = 1
      Top = 1
      Width = 183
      Height = 458
      Align = alClient
      Indent = 19
      TabOrder = 0
      OnClick = TreeView1Click
    end
  end
  object StatusBar1: TStatusBar
    Left = 0
    Top = 519
    Width = 784
    Height = 19
    Panels = <
      item
        Width = 150
      end
      item
        Width = 150
      end
      item
        Width = 150
      end
      item
        Width = 200
      end>
  end
  object ADOConnection1: TADOConnection
    LoginPrompt = False
    Mode = cmReadWrite
    Left = 112
    Top = 136
  end
  object DataSource1: TDataSource
    DataSet = ADOQuery2
    Left = 208
    Top = 128
  end
  object ADOQuery1: TADOQuery
    Connection = ADOConnection1
    Parameters = <>
    Left = 120
    Top = 200
  end
  object ADOQuery2: TADOQuery
    Connection = ADOConnection1
    BeforeDelete = ADOQuery2BeforeDelete
    AfterDelete = ADOQuery2AfterDelete
    Parameters = <>
    Left = 208
    Top = 192
  end
  object ADOQuery3: TADOQuery
    Connection = ADOConnection1
    Parameters = <>
    Left = 136
    Top = 440
  end
end
