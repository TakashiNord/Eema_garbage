object Form1: TForm1
  Left = 0
  Top = 0
  Caption = #1059#1089#1090#1072#1074#1082#1080' ........'
  ClientHeight = 508
  ClientWidth = 591
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'Segoe UI'
  Font.Style = []
  TextHeight = 15
  object PageControl1: TPageControl
    Left = 0
    Top = 0
    Width = 591
    Height = 508
    ActivePage = TabSheet1
    Align = alClient
    TabOrder = 0
    object TabSheet1: TTabSheet
      Caption = #1050#1086#1085#1090#1088#1086#1083#1100' '#1091#1089#1090#1072#1074#1086#1082
      object GroupBox1: TGroupBox
        Left = 3
        Top = 0
        Width = 566
        Height = 470
        Caption = #1059#1089#1090#1072#1074#1082#1080
        TabOrder = 0
        object Label1: TLabel
          Left = 16
          Top = 19
          Width = 127
          Height = 15
          Caption = #1059#1089#1090#1072#1074#1082#1080' '#1076#1083#1103' '#1087#1072#1088#1072#1084#1077#1090#1088#1072
        end
        object ComboBox1: TComboBox
          Left = 224
          Top = 16
          Width = 321
          Height = 23
          Style = csDropDownList
          ItemIndex = 0
          TabOrder = 0
          Text = #1050#1086#1085#1090#1088#1086#1083#1100' '#1088#1077#1078#1080#1084#1072
          Items.Strings = (
            #1050#1086#1085#1090#1088#1086#1083#1100' '#1088#1077#1078#1080#1084#1072)
        end
        object Button1: TButton
          Left = 224
          Top = 45
          Width = 75
          Height = 25
          Caption = #1044#1086#1073#1072#1074#1080#1090#1100
          Enabled = False
          TabOrder = 1
        end
        object Button2: TButton
          Left = 470
          Top = 45
          Width = 75
          Height = 25
          Caption = #1059#1076#1072#1083#1080#1090#1100
          Enabled = False
          TabOrder = 2
        end
        object GroupBox2: TGroupBox
          Left = 16
          Top = 76
          Width = 537
          Height = 304
          Caption = #1058#1080#1087' '#1082#1086#1085#1090#1088#1086#1083#1103
          TabOrder = 3
          object RadioGroup1: TRadioGroup
            Left = 7
            Top = 16
            Width = 302
            Height = 138
            ItemIndex = 0
            Items.Strings = (
              #1050#1086#1085#1090#1088#1086#1083#1100' '#1074' '#1076#1080#1072#1087#1072#1079#1086#1085#1077
              #1050#1086#1085#1090#1088#1086#1083#1100' '#1085#1072' '#1087#1088#1077#1074#1099#1096#1077#1085#1080#1080' '#1091#1088#1086#1074#1085#1103
              #1050#1086#1085#1090#1088#1086#1083#1100' '#1085#1072' '#1087#1086#1085#1080#1078#1077#1085#1080#1080' '#1091#1088#1086#1074#1085#1103
              #1047#1072#1074#1080#1089#1080#1084#1099#1081' '#1082#1086#1085#1090#1088#1086#1083#1100' '#1074' '#1076#1080#1072#1087#1072#1079#1086#1085#1077
              #1047#1072#1074#1080#1089#1080#1084#1099#1081' '#1082#1086#1085#1090#1088#1086#1083#1100' '#1085#1072' '#1087#1088#1077#1074#1099#1096#1077#1085#1080#1080' '#1091#1088#1086#1074#1085#1103
              #1047#1072#1074#1080#1089#1080#1084#1099#1081' '#1082#1086#1085#1090#1088#1086#1083#1100' '#1085#1072' '#1087#1086#1085#1080#1078#1077#1085#1080#1080' '#1091#1088#1086#1074#1085#1103)
            TabOrder = 0
            OnClick = RadioGroup1Click
          end
          object Memo1: TMemo
            Left = 315
            Top = 16
            Width = 214
            Height = 82
            Lines.Strings = (
              'Memo1')
            ReadOnly = True
            ScrollBars = ssVertical
            TabOrder = 1
          end
          object ChartControl: TChart
            Left = 7
            Top = 160
            Width = 302
            Height = 138
            Title.Text.Strings = (
              #1059#1089#1090#1072#1074#1082#1080)
            BottomAxis.LabelStyle = talPointValue
            BottomAxis.Title.Caption = 'sec'
            LeftAxis.Axis.Style = psDash
            LeftAxis.Title.Caption = 'Value'
            Shadow.Transparency = 49
            Shadow.Visible = False
            View3D = False
            View3DWalls = False
            Zoom.KeepAspectRatio = True
            TabOrder = 2
            DefaultCanvas = 'TGDIPlusCanvas'
            PrintMargins = (
              15
              27
              15
              27)
            ColorPaletteIndex = 13
            object Series1: TLineSeries
              HoverElement = [heCurrent]
              Legend.Text = 'max'
              LegendTitle = 'max'
              Brush.BackColor = clDefault
              Pointer.InflateMargins = True
              Pointer.Style = psRectangle
              Pointer.Visible = True
              XValues.Name = 'X'
              XValues.Order = loAscending
              YValues.Name = 'Y'
              YValues.Order = loNone
            end
            object Series2: TLineSeries
              HoverElement = [heCurrent]
              Legend.Text = 'min'
              LegendTitle = 'min'
              SeriesColor = 4227072
              Brush.BackColor = clDefault
              Pointer.InflateMargins = True
              Pointer.Style = psRectangle
              Pointer.Visible = True
              XValues.Name = 'X'
              XValues.Order = loAscending
              YValues.Name = 'Y'
              YValues.Order = loNone
            end
            object Series3: TLineSeries
              HoverElement = [heCurrent]
              Legend.Text = 'param'
              LegendTitle = 'param'
              SeriesColor = clRed
              Brush.BackColor = clDefault
              Pointer.InflateMargins = True
              Pointer.Style = psRectangle
              XValues.Name = 'X'
              XValues.Order = loAscending
              YValues.Name = 'Y'
              YValues.Order = loNone
            end
          end
          object StringGridControl: TStringGrid
            Left = 315
            Top = 104
            Width = 214
            Height = 169
            ColCount = 3
            FixedCols = 0
            Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRangeSelect, goEditing, goFixedRowDefAlign]
            TabOrder = 3
          end
          object ButtonAdd: TButton
            Left = 315
            Top = 276
            Width = 26
            Height = 25
            Caption = '+'
            TabOrder = 4
            OnClick = ButtonAddClick
          end
          object ButtonDel: TButton
            Left = 503
            Top = 276
            Width = 26
            Height = 25
            Caption = '-'
            TabOrder = 5
            OnClick = ButtonDelClick
          end
          object ButtonBuild: TButton
            Left = 391
            Top = 276
            Width = 75
            Height = 25
            Caption = 'Build Graph'
            TabOrder = 6
            OnClick = ButtonBuildClick
          end
          object ButtonList: TButton
            Left = 464
            Top = 276
            Width = 33
            Height = 25
            Caption = 'List'
            TabOrder = 7
            OnClick = ButtonListClick
          end
        end
        object CheckBox1: TCheckBox
          Left = 168
          Top = 386
          Width = 265
          Height = 17
          Caption = #1055#1086#1074#1090#1086#1088#1103#1090#1100' '#1089#1080#1075#1085#1072#1083' '#1089' '#1087#1077#1088#1080#1086#1076#1080#1095#1085#1086#1089#1090#1100#1102', '#1089#1077#1082
          TabOrder = 4
        end
        object CheckBox2: TCheckBox
          Left = 168
          Top = 434
          Width = 265
          Height = 17
          Caption = #1042#1088#1077#1084#1077#1085#1085#1099#1081' '#1075#1077#1089#1090#1077#1088#1077#1079#1080#1089' '#1089#1088#1072#1073#1072#1090#1099#1074#1072#1085#1080#1103', '#1089#1077#1082
          Enabled = False
          TabOrder = 5
        end
        object StaticText1: TStaticText
          Left = 104
          Top = 409
          Width = 372
          Height = 19
          Caption = #1042#1085#1080#1084#1072#1085#1080#1077'! '#1050#1086#1085#1090#1088#1086#1083#1100' '#1091#1089#1090#1072#1074#1086#1082' '#1086#1090#1082#1083#1102#1095#1072#1077#1090#1089#1103' '#1087#1088#1080' '#1085#1091#1083#1077#1074#1086#1084' '#1079#1085#1072#1095#1077#1085#1080#1080
          Font.Charset = RUSSIAN_CHARSET
          Font.Color = clWindowText
          Font.Height = -12
          Font.Name = 'Segoe UI Semibold'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 6
        end
        object Edit1: TEdit
          Left = 478
          Top = 386
          Width = 50
          Height = 23
          TabOrder = 7
          Text = '0'
        end
        object Edit2: TEdit
          Left = 478
          Top = 434
          Width = 51
          Height = 23
          Enabled = False
          TabOrder = 8
          Text = '0'
        end
        object UpDown1: TUpDown
          Left = 528
          Top = 386
          Width = 16
          Height = 23
          Associate = Edit1
          Max = 3600
          TabOrder = 9
        end
        object UpDown2: TUpDown
          Left = 529
          Top = 434
          Width = 16
          Height = 23
          Associate = Edit2
          Max = 3600
          TabOrder = 10
        end
        object ButtonCreate: TButton
          Left = 363
          Top = 352
          Width = 46
          Height = 25
          Caption = 'Create'
          TabOrder = 11
          OnClick = ButtonCreateClick
        end
      end
    end
    object TabSheet2: TTabSheet
      Caption = #1048#1089#1090#1086#1095#1085#1080#1082#1080' '#1091#1089#1090#1072#1074#1086#1082
      ImageIndex = 1
      object RadioGroupSrcU: TRadioGroup
        Left = 3
        Top = 0
        Width = 577
        Height = 475
        Caption = #1048#1089#1090#1086#1095#1085#1080#1082#1080' '#1091#1089#1090#1072#1074#1086#1082
        ItemIndex = 0
        Items.Strings = (
          #1054#1087#1077#1088#1072#1090#1086#1088
          #1040#1085#1072#1083#1086#1075#1086#1074#1099#1081' = '#1048#1089#1090#1086#1095#1085#1080#1082' '#1079#1085#1072#1095#1077#1085#1080#1081
          #1040#1085#1072#1083#1086#1075#1086#1074#1099#1081)
        TabOrder = 0
      end
      object OEditmax: TEdit
        Left = 160
        Top = 56
        Width = 121
        Height = 23
        TabOrder = 1
        Text = 'OEditmax'
      end
      object OEditmin: TEdit
        Left = 160
        Top = 93
        Width = 121
        Height = 23
        TabOrder = 2
        Text = 'OEditmin'
      end
    end
    object TabSheet3: TTabSheet
      Caption = #1048#1089#1090#1086#1095#1085#1080#1082#1080' '#1079#1085#1072#1095#1077#1085#1080#1081
      ImageIndex = 2
      object Label2: TLabel
        Left = 256
        Top = 115
        Width = 59
        Height = 15
        Caption = '- '#1079#1085#1072#1095#1077#1085#1080#1077
      end
      object Label3: TLabel
        Left = 256
        Top = 155
        Width = 66
        Height = 15
        Caption = '- '#1076#1080#1089#1087#1077#1088#1089#1080#1103
      end
      object RadioGroupSrcOwn: TRadioGroup
        Left = 3
        Top = 0
        Width = 577
        Height = 475
        Caption = #1048#1089#1090#1086#1095#1085#1080#1082#1080' '#1079#1085#1072#1095#1077#1085#1080#1081
        ItemIndex = 0
        Items.Strings = (
          #1054#1087#1077#1088#1072#1090#1086#1088
          #1040#1085#1072#1083#1086#1075#1086#1074#1099#1081)
        TabOrder = 0
      end
      object OwnEdit: TEdit
        Left = 120
        Top = 112
        Width = 121
        Height = 23
        TabOrder = 1
        Text = '0'
      end
      object OwnEditDist: TEdit
        Left = 120
        Top = 152
        Width = 121
        Height = 23
        TabOrder = 2
        Text = '0'
      end
    end
    object TabSheet4: TTabSheet
      Caption = #1050#1086#1085#1090#1088#1086#1083#1100
      ImageIndex = 3
      object RichEditControl: TRichEdit
        Left = 3
        Top = 346
        Width = 577
        Height = 129
        Font.Charset = RUSSIAN_CHARSET
        Font.Color = clWindowText
        Font.Height = -12
        Font.Name = 'Segoe UI'
        Font.Style = []
        Lines.Strings = (
          'RichEdit1')
        ParentFont = False
        TabOrder = 0
      end
      object Chart1: TChart
        Left = 16
        Top = 32
        Width = 377
        Height = 242
        Title.Text.Strings = (
          'TChart')
        TabOrder = 1
        DefaultCanvas = 'TGDIPlusCanvas'
        ColorPaletteIndex = 13
      end
    end
  end
  object Timer1: TTimer
    Left = 180
    Top = 58
  end
end
