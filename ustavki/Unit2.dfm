object Form2: TForm2
  Left = 0
  Top = 0
  Caption = 'create'
  ClientHeight = 354
  ClientWidth = 592
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'Segoe UI'
  Font.Style = []
  TextHeight = 15
  object Chart1: TChart
    Left = 8
    Top = 8
    Width = 561
    Height = 153
    Title.Text.Strings = (
      'TChart')
    BottomAxis.Automatic = False
    BottomAxis.AutomaticMaximum = False
    BottomAxis.AutomaticMinimum = False
    BottomAxis.Maximum = 99999999999.000000000000000000
    BottomAxis.Minimum = -99999999999.000000000000000000
    LeftAxis.Automatic = False
    LeftAxis.AutomaticMaximum = False
    LeftAxis.AutomaticMinimum = False
    LeftAxis.Maximum = 1.000000000000000000
    LeftAxis.Minimum = -1.000000000000000000
    View3D = False
    TabOrder = 0
    DefaultCanvas = 'TGDIPlusCanvas'
    PrintMargins = (
      15
      13
      15
      13)
    ColorPaletteIndex = 13
    object Series1: TBarSeries
      HoverElement = []
      XValues.Name = 'X'
      XValues.Order = loAscending
      YValues.Name = 'Bar'
      YValues.Order = loNone
    end
  end
  object Edit1: TEdit
    Left = 16
    Top = 271
    Width = 121
    Height = 23
    TabOrder = 1
    Text = 'Edit1'
  end
  object Edit2: TEdit
    Left = 16
    Top = 303
    Width = 121
    Height = 23
    TabOrder = 2
    Text = 'Edit2'
  end
  object Edit3: TEdit
    Left = 176
    Top = 271
    Width = 121
    Height = 23
    TabOrder = 3
    Text = 'Edit3'
  end
  object Edit4: TEdit
    Left = 176
    Top = 300
    Width = 121
    Height = 23
    TabOrder = 4
    Text = 'Edit4'
  end
  object Edit5: TEdit
    Left = 344
    Top = 271
    Width = 121
    Height = 23
    TabOrder = 5
    Text = 'Edit5'
  end
  object Edit6: TEdit
    Left = 344
    Top = 300
    Width = 121
    Height = 23
    TabOrder = 6
    Text = 'Edit6'
  end
end
