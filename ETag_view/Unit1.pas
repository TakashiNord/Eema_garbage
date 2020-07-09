unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, ComCtrls, Grids, DBGrids, StdCtrls, DBCtrls, DB, ADODB;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    TreeView1: TTreeView;
    Splitter1: TSplitter;
    Panel2: TPanel;
    ADOConnection1: TADOConnection;
    DBNavigator1: TDBNavigator;
    DBGrid1: TDBGrid;
    Label1: TLabel;
    Edit1: TEdit;
    Label2: TLabel;
    Edit2: TEdit;
    Label3: TLabel;
    Edit3: TEdit;
    Button1: TButton;
    DataSource1: TDataSource;
    ADOQuery1: TADOQuery;
    ADOQuery2: TADOQuery;
    Panel3: TPanel;
    Panel4: TPanel;
    StatusBar1: TStatusBar;
    Button2: TButton;
    CheckBox1: TCheckBox;
    Button3: TButton;
    Button4: TButton;
    RichEdit1: TRichEdit;
    ADOQuery3: TADOQuery;
    Splitter2: TSplitter;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure TreeView1Click(Sender: TObject);
    procedure ADOQuery2BeforeDelete(DataSet: TDataSet);
    procedure Button2Click(Sender: TObject);
    procedure ADOQuery2AfterDelete(DataSet: TDataSet);
    procedure DBGrid1TitleClick(Column: TColumn);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);

  private
    { Private declarations }
  public
    { Public declarations }
    procedure  Tag_load(Sender: TObject);
    procedure  Tag_cnt(Sender: TObject);
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.ADOQuery2AfterDelete(DataSet: TDataSet);
begin
  Tag_cnt(nil);
end;

procedure TForm1.ADOQuery2BeforeDelete(DataSet: TDataSet);
var
  strQry : string;
  cnt: Integer;
begin

  strQry:='select * from tag_position where id_tag=' + DataSet.FieldByName('ID').AsString ;
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
//    ADOQuery1.Active:=True;
    ADOQuery1.Open;
    cnt:=ADOQuery1.RecordCount ;
    ADOQuery1.Close;
  except
   on e:Exception do
  end;

  if (cnt <= 0) then Exit ;

  strQry:='delete from tag_position where id_tag=' + DataSet.FieldByName('ID').AsString ;
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
//    ADOQuery1.Active:=True;
  except
   on e:Exception do
  end;

end;

procedure TForm1.Button1Click(Sender: TObject);
var
 pass,login,tns : String ;
begin
   tns:='rsdu1';
   login:='rsduadmin';
   pass:='passme';
   tns:=Edit1.Text ;
   login:=Edit2.Text ;
   pass:=Edit3.Text ;
   ADOConnection1.ConnectionString:='Provider=' +
   ADOConnection1.Provider + ';' +
   'Password=' + pass + ';' +
   'Persist Security Info=True;' +
   'User ID=' + login + ';' +
   'Data Source='+tns+';';
  try
    ADOConnection1.Close();
    ADOConnection1.Connected:=True ;
  except
   on e:Exception do begin
    Exit;
    Application.Terminate ;
   end;
  end;
   if (ADOConnection1.Connected=False) then  Exit ;
   Button1.Enabled:=False ;
   RichEdit1.Clear();
   Tag_load(Sender);
   Button2.Enabled:=true;
   Button3.Enabled:=true;
   Button4.Enabled:=true;

end;

procedure TForm1.Button2Click(Sender: TObject);
var
 i : Integer ;
 cnt_tag_position : Integer ;
 cnt_tag_list : Integer ;
 strQry : String ;
  rezult : TModalResult;
begin
  i:=TreeView1.Selected.AbsoluteIndex ;
  if i<=0 then Exit;

  strQry:=TreeView1.Selected.Text ;
  i:=TreeView1.Selected.SelectedIndex ;

  cnt_tag_position:=0;
  strQry:='select count(*) from tag_position where id_scheme=' + IntToStr(i) + '  ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
    //ADOQuery1.Active:=True;
    ADOQuery1.Open;
    if (ADOQuery1.Recordset.RecordCount>0) then begin
      cnt_tag_position:=ADOQuery1.Fields.Fields[0].AsInteger;
    end;
    StatusBar1.Panels[1].Text:='tag_position = ' + IntToStr(cnt_tag_position) ;
    ADOQuery1.Close;
  except
   on e:Exception do
  end;

  cnt_tag_list:=0;
  strQry:='select count(*) from tag_list where id in ( select id_tag from tag_position where id_scheme=' + IntToStr(i) + ' ) ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
    //ADOQuery1.Active:=True;
    ADOQuery1.Open;
    if (ADOQuery1.Recordset.RecordCount>0) then begin
      cnt_tag_list:=ADOQuery1.Fields.Fields[0].AsInteger;
    end;
    StatusBar1.Panels[2].Text:='tag_list = ' + IntToStr(cnt_tag_list) ;
    ADOQuery1.Close;
  except
   on e:Exception do
  end;

  if ((cnt_tag_list=0) and (cnt_tag_position=0)) then Exit ;

  cnt_tag_position:=0;
  strQry:='select * from tag_position where id_tag not in ( select id from tag_list ) and id_scheme=' + IntToStr(i) + ' ; ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
    //ADOQuery1.Active:=True;
    ADOQuery1.Open;
    cnt_tag_position:=ADOQuery1.Recordset.RecordCount ;
    ADOQuery1.Close;
    if (cnt_tag_position>0) then begin
        rezult:=MessageDlg('Delete = ' + IntToStr(cnt_tag_position) + ' record(s) ? from tag_position', mtConfirmation, [mbYes,mbNo],0 );
        if rezult = mrNo then Exit;
        strQry:='delete from tag_position where id_tag not in ( select id from tag_list ) and id_scheme=' + IntToStr(i) + ' ; ';
        ADOQuery1.Active:=False ;
        ADOQuery1.SQL.Clear;
        ADOQuery1.SQL.Add(strQry);
        ADOQuery1.ExecSQL;

        Tag_cnt(Sender);

    end;
  except
   on e:Exception do
  end;

end;

procedure TForm1.Button3Click(Sender: TObject);
var
 cnt_tag_list : Integer ;
 strQry : String ;
 rezult : TModalResult;
begin

  cnt_tag_list:=0;
  strQry:='select count(*) from tag_list where id not in ( select id_tag from tag_position ) ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
    //ADOQuery1.Active:=True;
    ADOQuery1.Open;
    if (ADOQuery1.Recordset.RecordCount>0) then begin
      cnt_tag_list:=ADOQuery1.Fields.Fields[0].AsInteger;
    end;
    ADOQuery1.Close;
  except
   on e:Exception do
  end;

  if (cnt_tag_list<=0) then Exit ;

  rezult:=MessageDlg('Delete = ' + IntToStr(cnt_tag_list) + ' record(s) ? from tag_list', mtConfirmation, [mbYes,mbNo],0 );
  if rezult = mrNo then Exit;

  TreeView1.Items.Item[0].Selected:=True;
  TreeView1.SetFocus();
  TreeView1Click(Sender);
  dbgrid1.refresh();

  strQry:='delete from tag_list where id not in ( select id_tag from tag_position )';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
  except
   on e:Exception do
  end;

end;

procedure TForm1.Button4Click(Sender: TObject);
var
 i : Integer ;
 cnt_tag_list : Integer ;
 strQry : String ;
 rezult : TModalResult;
begin
  i:=TreeView1.Selected.AbsoluteIndex ;
  if i<=0 then Exit;

  strQry:=TreeView1.Selected.Text ;
  i:=TreeView1.Selected.SelectedIndex ;

  cnt_tag_list:=0;
  strQry:='select count(*) from tag_list where visible=0 and id in ( select id_tag from tag_position where id_scheme=' + IntToStr(i) + ' ) ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
    //ADOQuery1.Active:=True;
    ADOQuery1.Open;
    if (ADOQuery1.Recordset.RecordCount>0) then begin
      cnt_tag_list:=ADOQuery1.Fields.Fields[0].AsInteger;
    end;
    ADOQuery1.Close;
  except
   on e:Exception do
  end;

  if (cnt_tag_list<=0) then Exit ;

  rezult:=MessageDlg('Delete = ' + IntToStr(cnt_tag_list) + ' record(s) ? from tag_list+tag_position', mtConfirmation, [mbYes,mbNo],0 );
  if rezult = mrNo then Exit;

  TreeView1.Items.Item[0].Selected:=True;
  TreeView1.SetFocus();
  TreeView1Click(Sender);
  dbgrid1.refresh();

  strQry:='delete from tag_list where visible=0 and id in ( select id_tag from tag_position where id_scheme=' + IntToStr(i) + ' ) ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
  except
   on e:Exception do
  end;

end;

procedure TForm1.DBGrid1TitleClick(Column: TColumn);
begin
  ADOQuery2.sort:=Column.FieldName;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  Button2.Enabled:=false;
  Button3.Enabled:=false;
  Button4.Enabled:=false;
  RichEdit1.Clear();
  try
    ADOConnection1.Connected:=False ;
  except
   on e:Exception do
    Application.Terminate ;
  end;
end;

procedure TForm1.Tag_load(Sender: TObject);
var
 strQry : String ;
 node : TTreeNode ;
 i : integer ;
 strField, strInd : String ;
 cnt_tag_position : Integer ;
begin
  // Запрещаем обновление TreeView
   TreeView1.Items.BeginUpdate;
   with TreeView1 do
   begin
      // Добавляем корневой узел
      //Items.Add( nil, 'Схема' ); // 515
      Items.Add( nil, 'Схема PCAD' );  // 516
      Items.Add( nil, 'Схема MODUS' ); // 518
      Items.Add( nil, 'Схема Anares' ); // 519
      Items.Add( nil, 'Схема TOPAZ' );  // 520
   end;

  RichEdit1.Lines.Add('  ');

  strQry:='select * from vs_form where id_type=516 ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
    ADOQuery1.Active:=True;
    ADOQuery1.Open;
    RichEdit1.Lines.Add('Схема PCAD ( id_type=516 ) = ' + IntToStr(ADOQuery1.RecordCount));
    if (ADOQuery1.Recordset.RecordCount>0) then begin
      ADOQuery1.Recordset.MoveFirst;
      for i:=0 to (ADOQuery1.RecordCount-1) do
      begin
        strInd:=VarToStr(ADOQuery1.Recordset.Fields['ID'].Value);
        strField:=VarToStr(ADOQuery1.Recordset.Fields['NAME'].Value);

        cnt_tag_position:=0;
        strQry:='select count(*) from tag_position where id_scheme=' + strInd ;
        try
         ADOQuery3.Active:=False ;
         ADOQuery3.SQL.Clear;
         ADOQuery3.SQL.Add(strQry);
         ADOQuery3.ExecSQL;
         ADOQuery3.Open;
         if (ADOQuery3.Recordset.RecordCount>0) then begin
           cnt_tag_position:=ADOQuery3.Fields.Fields[0].AsInteger;
         end;
         ADOQuery3.Close;
        except
          on e:Exception do
        end;

        node:=TreeView1.Items.AddChild( TreeView1.Items[0], strField + '(' + IntToStr(cnt_tag_position) + ')' );
        node.SelectedIndex:=StrToInt(strInd);

        RichEdit1.Lines.Add('  ' + strField + ' ( id= ' + strInd + ' )');
        ADOQuery1.Recordset.MoveNext;
      end;
    end;
    ADOQuery1.Close;
  except
   on e:Exception do
  end;

  strQry:='select * from vs_form where id_type=518 ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
    ADOQuery1.Active:=True;
    ADOQuery1.Open;
    RichEdit1.Lines.Add('Схема MODUS ( id_type=518 ) = ' + IntToStr(ADOQuery1.RecordCount));
    if (ADOQuery1.Recordset.RecordCount>0) then begin
      ADOQuery1.Recordset.MoveFirst;
      for i:=0 to (ADOQuery1.RecordCount-1) do
      begin
        strInd:=VarToStr(ADOQuery1.Recordset.Fields['ID'].Value);
        strField:=VarToStr(ADOQuery1.Recordset.Fields['NAME'].Value);

        cnt_tag_position:=0;
        strQry:='select count(*) from tag_position where id_scheme=' + strInd ;
        try
         ADOQuery3.Active:=False ;
         ADOQuery3.SQL.Clear;
         ADOQuery3.SQL.Add(strQry);
         ADOQuery3.ExecSQL;
         ADOQuery3.Open;
         if (ADOQuery3.Recordset.RecordCount>0) then begin
           cnt_tag_position:=ADOQuery3.Fields.Fields[0].AsInteger;
         end;
         ADOQuery3.Close;
        except
          on e:Exception do
        end;

        node:=TreeView1.Items.AddChild( TreeView1.Items[1], strField + '(' + IntToStr(cnt_tag_position) + ')' );
        node.SelectedIndex:=StrToInt(strInd);

        RichEdit1.Lines.Add('  ' + strField + ' ( id= ' + strInd + ' )');
        ADOQuery1.Recordset.MoveNext;
      end;
    end;
    ADOQuery1.Close;
  except
   on e:Exception do
  end;

  strQry:='select * from vs_form where id_type=519 ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
    ADOQuery1.Active:=True;
    ADOQuery1.Open;
    RichEdit1.Lines.Add('Схема Anares ( id_type=519 ) = ' + IntToStr(ADOQuery1.RecordCount));
    if (ADOQuery1.Recordset.RecordCount>0) then begin
      ADOQuery1.Recordset.MoveFirst;
      for i:=0 to (ADOQuery1.RecordCount-1) do
      begin
        strInd:=VarToStr(ADOQuery1.Recordset.Fields['ID'].Value);
        strField:=VarToStr(ADOQuery1.Recordset.Fields['NAME'].Value);

        cnt_tag_position:=0;
        strQry:='select count(*) from tag_position where id_scheme=' + strInd ;
        try
         ADOQuery3.Active:=False ;
         ADOQuery3.SQL.Clear;
         ADOQuery3.SQL.Add(strQry);
         ADOQuery3.ExecSQL;
         ADOQuery3.Open;
         if (ADOQuery3.Recordset.RecordCount>0) then begin
           cnt_tag_position:=ADOQuery3.Fields.Fields[0].AsInteger;
         end;
         ADOQuery3.Close;
        except
          on e:Exception do
        end;

        node:=TreeView1.Items.AddChild( TreeView1.Items[2], strField + '(' + IntToStr(cnt_tag_position) + ')' );
        node.SelectedIndex:=StrToInt(strInd);

        RichEdit1.Lines.Add('  ' + strField + ' ( id= ' + strInd + ' )');
        ADOQuery1.Recordset.MoveNext;
      end;
    end;
    ADOQuery1.Close;
  except
   on e:Exception do
  end;

  strQry:='select * from vs_form where id_type=520 ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
    ADOQuery1.Active:=True;
    ADOQuery1.Open;
    RichEdit1.Lines.Add('Схема TOPAZ ( id_type=520 ) = ' + IntToStr(ADOQuery1.RecordCount));
    if (ADOQuery1.Recordset.RecordCount>0) then begin
      ADOQuery1.Recordset.MoveFirst;
      for i:=0 to (ADOQuery1.RecordCount-1) do
      begin
        strInd:=VarToStr(ADOQuery1.Recordset.Fields['ID'].Value);
        strField:=VarToStr(ADOQuery1.Recordset.Fields['NAME'].Value);

        cnt_tag_position:=0;
        strQry:='select count(*) from tag_position where id_scheme=' + strInd ;
        try
         ADOQuery3.Active:=False ;
         ADOQuery3.SQL.Clear;
         ADOQuery3.SQL.Add(strQry);
         ADOQuery3.ExecSQL;
         ADOQuery3.Open;
         if (ADOQuery3.Recordset.RecordCount>0) then begin
           cnt_tag_position:=ADOQuery3.Fields.Fields[0].AsInteger;
         end;
         ADOQuery3.Close;
        except
          on e:Exception do
        end;

        node:=TreeView1.Items.AddChild( TreeView1.Items[3], strField + '(' + IntToStr(cnt_tag_position) + ')' );
        node.SelectedIndex:=StrToInt(strInd);

        RichEdit1.Lines.Add('  ' + strField + ' ( id= ' + strInd + ' )');
        ADOQuery1.Recordset.MoveNext;
      end;
    end;
    ADOQuery1.Close;
  except
   on e:Exception do
  end;

  cnt_tag_position:=0;
  strQry:='select count(*) from tag_list where id not in ( select id_tag from tag_position ) ';
  try
    ADOQuery3.Active:=False ;
    ADOQuery3.SQL.Clear;
    ADOQuery3.SQL.Add(strQry);
    ADOQuery3.ExecSQL;
    ADOQuery3.Open;
    if (ADOQuery3.Recordset.RecordCount>0) then begin
      cnt_tag_position:=ADOQuery3.Fields.Fields[0].AsInteger;
    end;
    ADOQuery3.Close;
  except
   on e:Exception do
  end;
  RichEdit1.Lines.Add('-----------------------------');
  RichEdit1.Lines.Add(' ДП не привязанные к схемам:');
  RichEdit1.Lines.Add(' Number elements = ' +  IntToStr(cnt_tag_position) );
  RichEdit1.Lines.Add(  strQry );
  RichEdit1.Lines.Add('-----------------------------');

  // Обновляем TreeView
  TreeView1.Items.EndUpdate;

  with TreeView1 do
   begin
      Items.BeginUpdate;
      FullExpand;
      Items.EndUpdate;
   end;

end;

procedure TForm1.Tag_cnt(Sender: TObject);
var
 i : Integer ;
 cnt_tag_position : Integer ;
 cnt_tag_list : Integer ;
 strQry : String ;
begin
  i:=TreeView1.Selected.AbsoluteIndex ;
  if i<=0 then Exit;

  strQry:=TreeView1.Selected.Text ;
  i:=TreeView1.Selected.SelectedIndex ;

  cnt_tag_position:=0;
  strQry:='select count(*) from tag_position where id_scheme=' + IntToStr(i) + '  ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
    //ADOQuery1.Active:=True;
    ADOQuery1.Open;
    if (ADOQuery1.Recordset.RecordCount>0) then begin
      cnt_tag_position:=ADOQuery1.Fields.Fields[0].AsInteger;
    end;
    StatusBar1.Panels[1].Text:='tag_position = ' + IntToStr(cnt_tag_position) ;
    ADOQuery1.Close;
  except
   on e:Exception do
  end;

  cnt_tag_list:=0;
  strQry:='select count(*) from tag_list where id in ( select id_tag from tag_position where id_scheme=' + IntToStr(i) + ' ) ';
  try
    ADOQuery1.Active:=False ;
    ADOQuery1.SQL.Clear;
    ADOQuery1.SQL.Add(strQry);
    ADOQuery1.ExecSQL;
    //ADOQuery1.Active:=True;
    ADOQuery1.Open;
    if (ADOQuery1.Recordset.RecordCount>0) then begin
      cnt_tag_list:=ADOQuery1.Fields.Fields[0].AsInteger;
    end;
    StatusBar1.Panels[2].Text:='tag_list = ' + IntToStr(cnt_tag_list) ;
    ADOQuery1.Close;
  except
   on e:Exception do
  end;

end;

procedure setgridcolumnwidths(grid: tdbgrid);
const
defborder = 10;
var
temp, n: integer;
lmax: array [0..30] of integer;
begin
with grid do
begin
canvas.font := font;
for n := 0 to columns.count - 1 do
//if columns[n].visible then
lmax[n] := canvas.textwidth(fields[n].fieldname) + defborder;
grid.datasource.dataset.first;
while not grid.datasource.dataset.eof do
begin
for n := 0 to columns.count - 1 do
begin
//if columns[n].visible then begin
temp := canvas.textwidth(trim(columns[n].field.displaytext)) + defborder;
if temp > lmax[n] then lmax[n] := temp;
//end; { if }
end; {for}
grid.datasource.dataset.next;
end; { while }
grid.datasource.dataset.first;
for n := 0 to columns.count - 1 do
if lmax[n] > 0 then
columns[n].width := lmax[n];
end; { with }
end; {setgridcolumnwidths }

procedure TForm1.TreeView1Click(Sender: TObject);
var
 i : Integer ;
 strQry : String ;
begin
  i:=TreeView1.Selected.AbsoluteIndex ;
  if i<=0 then Exit;

  strQry:=TreeView1.Selected.Text ;
  i:=TreeView1.Selected.SelectedIndex ;

  StatusBar1.Panels[0].Text:='Scheme id = ' + IntToStr(i) ;

  Tag_cnt(Sender);

  strQry:='select * from tag_list where id in ( select id_tag from tag_position where id_scheme=' + IntToStr(i) + ' ) ';

  if (CheckBox1.Checked=True) then
   strQry:='select ' +
   'ID, ID_NODE, ID_TYPE, ID_DIR , ' +
   ' VISIBLE, ' +
   ' ID_USER_CREATE , ' +
   ' from_dt1970(DT1970_CREATE) DT1970_CREATE , ' +
   ' ID_USER_MODIFY , ' +
   ' from_dt1970(DT1970_MODIFY) DT1970_MODIFY, ' +
   ' DESCRIPTION , ' +
   ' from_dt1970(DT1970_STARTEVENT) DT1970_STARTEVENT, '+
   ' from_dt1970(DT1970_ENDEVENT) DT1970_ENDEVENT' +
   ' from tag_list where id in ( select id_tag from tag_position where id_scheme=' + IntToStr(i) + ' ) ';

  try
    ADOQuery2.Active:=False ;
    ADOQuery2.SQL.Clear;
    ADOQuery2.SQL.Add(strQry);
    ADOQuery2.ExecSQL;
    ADOQuery2.Active:=True;
    setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;

end;

end.
