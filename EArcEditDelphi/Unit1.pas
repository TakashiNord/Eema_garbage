unit Unit1;

interface

uses
  Windows, Messages, SysUtils,DateUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ComCtrls, ToolWin, DB, ADODB, Grids, DBGrids, ExtCtrls, DBCtrls,
  ImgList, StdCtrls, Buttons, Math , ShellAPI, Unit2 ;

type
  TForm1 = class(TForm)
    StatusBar1: TStatusBar;
    ADOConnection1: TADOConnection;
    ADOQuery1: TADOQuery;
    DataSource1: TDataSource;
    DataSource2: TDataSource;
    ADOQuery2: TADOQuery;
    DataSource3: TDataSource;
    ADOQuery3: TADOQuery;
    DataSource4: TDataSource;
    ADOQuery4: TADOQuery;
    DataSource5: TDataSource;
    DataSource6: TDataSource;
    ADOTable1: TADOTable;
    ADOTable5: TADOTable;
    Timer1: TTimer;
    ADOQuery71: TADOQuery;
    SaveDialog1: TSaveDialog;
    ADOTable81: TADOTable;
    DataSource81: TDataSource;
    Panel8: TPanel;
    Edit1: TEdit;
    Edit2: TEdit;
    Edit3: TEdit;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Button5: TButton;
    Timer2: TTimer;
    Edit4: TEdit;
    Button6: TButton;
    Button7: TButton;
    DateTimePicker1: TDateTimePicker;
    DateTimePicker2: TDateTimePicker;
    ADOQuery81: TADOQuery;
    DataSource82: TDataSource;
    ADOTable82: TADOTable;
    Button42: TButton;
    Button43: TButton;
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    Splitter6: TSplitter;
    Panel9: TPanel;
    Button3: TButton;
    Button8: TButton;
    Button9: TButton;
    Button10: TButton;
    Button11: TButton;
    Button12: TButton;
    Button13: TButton;
    Button14: TButton;
    Button15: TButton;
    Button16: TButton;
    Button17: TButton;
    Button18: TButton;
    Button19: TButton;
    Button20: TButton;
    Button21: TButton;
    Button22: TButton;
    Button23: TButton;
    Button24: TButton;
    Button2: TButton;
    Button25: TButton;
    Button26: TButton;
    Button27: TButton;
    Button28: TButton;
    Button29: TButton;
    Button30: TButton;
    Button31: TButton;
    Button32: TButton;
    Button33: TButton;
    Button34: TButton;
    Button35: TButton;
    dba_synonyms: TButton;
    Button36: TButton;
    Button37: TButton;
    Button38: TButton;
    Button39: TButton;
    Button40: TButton;
    Button41: TButton;
    Panel10: TPanel;
    RichEdit1: TRichEdit;
    DBGrid1: TDBGrid;
    TabSheet2: TTabSheet;
    Splitter4: TSplitter;
    DBGrid2: TDBGrid;
    Panel6: TPanel;
    Label1: TLabel;
    DBNavigator4: TDBNavigator;
    Memo2: TMemo;
    TabSheet3: TTabSheet;
    Splitter5: TSplitter;
    DBGrid3: TDBGrid;
    Panel5: TPanel;
    Label2: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    DBNavigator5: TDBNavigator;
    DBNavigator6: TDBNavigator;
    DBGrid8: TDBGrid;
    TabSheet4: TTabSheet;
    RadioGroupArcJ: TRadioGroup;
    DBGrid4: TDBGrid;
    Panel2: TPanel;
    StaticText1: TStaticText;
    StaticText2: TStaticText;
    TabSheet5: TTabSheet;
    Splitter1: TSplitter;
    DBGrid5: TDBGrid;
    Panel4: TPanel;
    RadioGroupStat: TRadioGroup;
    DBNavigator2: TDBNavigator;
    Button4: TButton;
    Memo1: TMemo;
    TabSheet6: TTabSheet;
    Splitter3: TSplitter;
    TreeView1: TTreeView;
    Panel1: TPanel;
    DBNavigator1: TDBNavigator;
    DBGrid6: TDBGrid;
    TabSheet7: TTabSheet;
    Panel3: TPanel;
    Label3: TLabel;
    Button1: TButton;
    StaticText6: TStaticText;
    ComboBox1: TComboBox;
    StringGrid1: TStringGrid;
    Button45: TButton;
    procedure FormResize(Sender: TObject);
    procedure RadioGroupArcJClick(Sender: TObject);
    procedure RadioGroupStatClick(Sender: TObject);
    procedure TreeView1Click(Sender: TObject);
    procedure PageControl1Change(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure ComboBox1Change(Sender: TObject);
    procedure StringGrid1DrawCell(Sender: TObject; ACol, ARow: Integer;
      Rect: TRect; State: TGridDrawState);
    procedure Button5Click(Sender: TObject);
    procedure Timer2Timer(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure Button7Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button8Click(Sender: TObject);
    procedure Button10Click(Sender: TObject);
    procedure Button9Click(Sender: TObject);
    procedure Button11Click(Sender: TObject);
    procedure Button12Click(Sender: TObject);
    procedure Button13Click(Sender: TObject);
    procedure Button14Click(Sender: TObject);
    procedure Button15Click(Sender: TObject);
    procedure Button16Click(Sender: TObject);
    procedure Button17Click(Sender: TObject);
    procedure Button18Click(Sender: TObject);
    procedure Button19Click(Sender: TObject);
    procedure Button20Click(Sender: TObject);
    procedure Button21Click(Sender: TObject);
    procedure Button24Click(Sender: TObject);
    procedure Button22Click(Sender: TObject);
    procedure DBGrid1TitleClick(Column: TColumn);
    procedure DBGrid2TitleClick(Column: TColumn);
    procedure DBGrid3TitleClick(Column: TColumn);
    procedure DBGrid4TitleClick(Column: TColumn);
    procedure DBGrid5TitleClick(Column: TColumn);
    procedure DBGrid6TitleClick(Column: TColumn);
    procedure DBGrid7TitleClick(Column: TColumn);
    procedure Button23Click(Sender: TObject);
    procedure DBGrid8TitleClick(Column: TColumn);
    procedure DBGrid2EditButtonClick(Sender: TObject);
    procedure DBGrid3EditButtonClick(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button27Click(Sender: TObject);
    procedure Button25Click(Sender: TObject);
    procedure Button26Click(Sender: TObject);
    procedure Button31Click(Sender: TObject);
    procedure Button30Click(Sender: TObject);
    procedure Button33Click(Sender: TObject);
    procedure Button32Click(Sender: TObject);
    procedure Button29Click(Sender: TObject);
    procedure Button34Click(Sender: TObject);
    procedure Button35Click(Sender: TObject);
    procedure Button28Click(Sender: TObject);
    procedure dba_synonymsClick(Sender: TObject);
    procedure Button36Click(Sender: TObject);
    procedure Button39Click(Sender: TObject);
    procedure Button40Click(Sender: TObject);
    procedure Button41Click(Sender: TObject);
    procedure Button37Click(Sender: TObject);
    procedure Button38Click(Sender: TObject);
    procedure Button42Click(Sender: TObject);
    procedure Button43Click(Sender: TObject);
    procedure Button45Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    Reg : Integer ;
    procedure ArcCreate(Sender: TObject);
    procedure ArcAll(Sender: TObject);
    procedure ArcSystem(Sender: TObject);
    procedure ArcProfile(Sender: TObject);
    procedure ArcStat(Sender: TObject);
    procedure ArcJournal(Sender: TObject);
    procedure ArcHelp(Sender: TObject);
    procedure ArcUpdate(Sender: TObject);
    procedure ArcCalc(Sender: TObject);
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}


procedure SetGridClient(grid: tdbgrid);
var i,s,s1,n:integer;
begin
  s:=0;s1:=0;
  for i:=0 to Grid.Columns.Count-1 do
    s:=s+Grid.Columns[i].Width;
  n:=Grid.ClientWidth-Grid.Columns.Count-1;
  if dgIndicator in Grid.Options then n:=n-12;
  for i:=0 to Grid.Columns.Count-1 do
    begin
      Grid.Columns[i].Width:=Round(Grid.Columns[i].Width/s*n);
      if Grid.Columns[i].Width<2 then Grid.Columns[i].Width:=2;
      s1:=s1+Grid.Columns[i].Width;
    end;
  i:=Grid.Columns.Count - 1 - n mod Grid.Columns.Count ;
  Grid.Columns[i].Width:=Grid.Columns[i].Width-s1+n;
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



procedure TForm1.FormCreate(Sender: TObject);
begin
  try
    ADOConnection1.Connected:=False ;
  except
   on e:Exception do
    Application.Terminate ;
  end;
  PageControl1.Enabled:=False ;
  PageControl1.ActivePageIndex:=0;

  DateTimePicker1.Date:=StrToDate(DateToStr(now));
  DateTimePicker2.Time:=StrToTime(TimeToStr(now));

  Button6Click(Sender);
end;

procedure TForm1.FormResize(Sender: TObject);
begin
//
end;

procedure TForm1.PageControl1Change(Sender: TObject);
var
  i : integer ;
begin
  //
  i:=PageControl1.ActivePageIndex ;
  if i=1 then begin
    StatusBar1.Panels[0].Text:='Row = ' + IntToStr(ADOQuery2.RecordCount) ;
  end;
  if i=2 then begin
    StatusBar1.Panels[0].Text:='Row = ' + IntToStr(ADOQuery3.RecordCount) ;
  end;
  if i=3 then begin
    StatusBar1.Panels[0].Text:='Row = ' + IntToStr(ADOQuery4.RecordCount) ;
  end;
  if i=4 then begin
    StatusBar1.Panels[0].Text:='Row = ' + IntToStr(ADOTable5.RecordCount) ;
  end;
  if i=5 then begin
    StatusBar1.Panels[0].Text:='Row = ' ;
  end;
  if i=6 then begin
    StatusBar1.Panels[0].Text:='Row = ' ;
  end;


end;

procedure TForm1.RadioGroupArcJClick(Sender: TObject);
begin
  ArcJournal(Sender);
end;


procedure TForm1.ArcJournal(Sender: TObject);
var
 strQry4 : String ;
 ind : integer ;
begin
 ind :=RadioGroupArcJ.ItemIndex ;

 if ind=0 then
strQry4:='select   ' +
'  jms.ID          , ' +
'  jms.ID_USER      ,  ' +
//'  datetime(jms.DT1970, "unixepoch") ,   ' +
'  jms.DTDATE       ,  ' +
'  jms.ULOGIN       ,  ' +
'  jms.ID_TBLLST    , TBL.NAME ,  ' +
'  jms.ID_GINFO     , AR.NAME , ' +
'  jms.STATUS       ,  ' +
'  jms.IDSID        ,  ' +
'  jms.LOGON_TIME   ,  ' +
'  jms.DESCRIPTION     ' +
'from J_MOVE_STACK jms , ARC_GINFO ar, sys_tbllst tbl   ' +
'where jms.ID_GINFO=ar.ID and jms.ID_TBLLST=tbl.ID   ' +
'order by jms.DT1970 DESC ;';

 if ind=1 then
 strQry4:='select ' +
'  jre.ID        ,' +
'  jre.ID_USER   , ' +
//'  datetime(jms.DT1970, "unixepoch")    , ' +
'  jre.DTDATE    , ' +
'  jre.ULOGIN    , ' +
'  jre.ERR_LEVEL  , ' +
'  jre.ERR_NUMBER  ,' +
'  jre.ERR_DESC   , ' +
'  jre.ERR_STACK    ' +
'from J_RSDU_ERROR jre  ' +
'order by jre.DT1970 DESC ;';

 if ind=2 then
strQry4:='select   ' +
'  *          ' +
'from J_ARC_HIST_CLEAR  ;';

 if ind=3 then
strQry4:='select   ' +
'  *          ' +
'from J_ARC_RESTORE  ;';

 if ind=4 then
strQry4:='select   ' +
'  *          ' +
'from J_ARC_VAL_CHANGE  ;';



 try
ADOQuery4.Active:=False ;
ADOQuery4.SQL.Clear;
ADOQuery4.SQL.Add(strQry4);
ADOQuery4.ExecSQL;
ADOQuery4.Active:=True;
StatusBar1.Panels[0].Text:='Row = ' + IntToStr(ADOQuery4.RecordCount) ;
//SetGridClient(DBgrid4);
setgridcolumnwidths(DBgrid4);
  except
   on e:Exception do
  end;

end;

procedure TForm1.RadioGroupStatClick(Sender: TObject);
begin
  ArcStat(Sender);
end;

procedure TForm1.StringGrid1DrawCell(Sender: TObject; ACol, ARow: Integer;
  Rect: TRect; State: TGridDrawState);
var s:string;
 Flag: Cardinal;
 H: integer;
 clColor: Integer ;
begin

 s := StringGrid1.Cells[ACol,ARow];

 clColor:=StringGrid1.Canvas.Brush.Color;
 StringGrid1.Canvas.Brush.Color:=clColor;//clWindow;

 StringGrid1.Canvas.FillRect(Rect);

 Case Acol mod 1 of
   0: Flag := DT_LEFT;
   1: Flag := DT_CENTER;
 else
   Flag := DT_RIGHT;
 end;
 Flag := Flag or DT_WORDBREAK ; //or DT_CALCRECT;
 Inc(Rect.Left,3);
 Dec(Rect.Right,3);

 H := DrawText(StringGrid1.Canvas.Handle,PChar(s),length(s),Rect,Flag);
 if H > StringGrid1.RowHeights[ARow] then
    StringGrid1.RowHeights[ARow] := H;  //увеличиваем
end;

procedure TForm1.Timer1Timer(Sender: TObject);
var
  Res: LongInt;
const
   // Sets UnixStartDate to TDateTime of 01/01/1970
  UnixStartDate: TDateTime = 25569.0;
begin
  Res:=DateTimeToUnix(Now); //GetTime
  //Res := Round((Time - UnixStartDate) * 86400);
  //StatusBar1.Panels[1].Text:='Time = ' + TimeToStr(Now) + '  |   Unix=' + IntToStr(Res);
  StatusBar1.Panels[1].Text:='Time = ' + FormatDateTime('dd/mm/yyyy hh:nn:ss', Now) + '  |   Unix=' + IntToStr(Res);
end;

procedure TForm1.Timer2Timer(Sender: TObject);
begin
  if 4<>PageControl1.ActivePageIndex then begin
     Timer2.Enabled:=False ;
     Exit ;
  end;
  ArcStat(Sender);
end;

procedure TForm1.TreeView1Click(Sender: TObject);
begin
 ArcHelp(Sender);
end;

procedure TForm1.ArcStat(Sender: TObject);
var
 strQry : String ;
 ind, i : integer ;
begin

 ind := RadioGroupStat.ItemIndex ;
 if ind=0 then strQry:='arc_stat';
 if ind=1 then strQry:='ARC_STAT_CURRENT_V';
 if ind=1 then Timer2.Enabled:=True else  Timer2.Enabled:=False ;
 if ind=2 then strQry:='ARC_STAT_AVG_V';

 try
ADOTable5.Active:=False ;
ADOTable5.TableName:= strQry ;
ADOTable5.Active:=True;
StatusBar1.Panels[0].Text:='Row = ' + IntToStr(ADOTable5.RecordCount) ;
//SetGridClient(DBgrid5);
setgridcolumnwidths(DBgrid5);
  except
   on e:Exception do
  end;

end;


procedure TForm1.ArcAll(Sender: TObject);
begin
  RichEdit1.Lines.Clear();
end;


procedure TForm1.ArcSystem(Sender: TObject);
var
 i : Integer  ;
 strQry : String ;
begin

  strQry:='select   ' +
'ar.id,    ' +
'GT.NAME,  ' +
'AR.ID_TYPE,AT.NAME,  ' +
'ar.NAME,  ' +
'ar.DEPTH,  ' +
'ar.DEPTH_LOCAL,  ' +
'ar.CACHE_SIZE,  ' +
'ar.CACHE_TIMEOUT,  ' +
'ar.FLUSH_INTERVAL,  ' +
'ar.RESTORE_INTERVAL,  ' +
'ar.STACK_INTERVAL,  ' +
'ar.WRITE_MINMAX,  ' +
'ar.RESTORE_TIME,  ' +
'ar.STATE,  ' +
'ar.RESTORE_TIME_LOCAL,  ' +
'ar.DEPTH_PARTITION  ' +
'from ARC_GINFO ar, sys_gtopt gt, arc_type at   ' +
'where ar.ID_GTOPT=GT.ID and AT.ID=AR.ID_TYPE   ' +
'order by ar.id ASC';

  strQry:='select  * ' +
'from ARC_GINFO ar ' +
'order by ar.id ASC';

 try
ADOQuery2.Active:=False ;
ADOQuery2.SQL.Clear;
ADOQuery2.SQL.Add(strQry);
ADOQuery2.ExecSQL;
ADOQuery2.Active:=True;
StatusBar1.Panels[0].Text:='Row = ' + IntToStr(ADOQuery2.RecordCount) ;
//SetGridClient(DBgrid5);
setgridcolumnwidths(DBgrid2);
  except
   on e:Exception do
  end;

  for i:=0 to DBGrid2.Columns.Count-1 do begin
   if DBGrid2.Columns.Items[i].FieldName = 'ID_TYPE' then
     begin
        DBGrid2.Columns.Items[i].PickList.Add('1');
        DBGrid2.Columns.Items[i].PickList.Add('2');
        DBGrid2.Columns.Items[i].ButtonStyle:= cbsAuto; // cbsAuto  cbsEllipsis ;
     end;
   if DBGrid2.Columns.Items[i].FieldName = 'ID_GTOPT' then
     begin
        DBGrid2.Columns.Items[i].ButtonStyle:= cbsEllipsis; // cbsAuto  cbsEllipsis ;
     end;
   if DBGrid2.Columns.Items[i].FieldName = 'STATE' then
     begin
        DBGrid2.Columns.Items[i].ButtonStyle:= cbsEllipsis; // cbsAuto  cbsEllipsis ;
     end;
  end ;
  DBGrid2.Refresh;

end;

procedure TForm1.ArcProfile(Sender: TObject);
var
 i : Integer ;
 strQry : String ;
begin

  strQry:='select   ' +
'pr.ID,    ' +
'pr.ID_TBLLST,  ' +
'TBL.NAME,   ' +
'pr.ID_GINFO,  ' +
'AR.NAME,   ' +
'pr.IS_WRITEON,  ' +
'pr.STACK_NAME,  ' +
'pr.LAST_UPDATE,  ' +
'pr.IS_VIEWABLE   ' +
'from ARC_SUBSYST_PROFILE pr, ARC_GINFO ar, sys_tbllst tbl   ' +
'where pr.ID_GINFO=ar.ID and pr.ID_TBLLST=tbl.ID   ' +
'order by pr.id ASC';

  strQry:='select * ' +
'from ARC_SUBSYST_PROFILE ' +
'order by ARC_SUBSYST_PROFILE.id ASC';

 try
ADOQuery3.Active:=False ;
ADOQuery3.SQL.Clear;
ADOQuery3.SQL.Add(strQry);
ADOQuery3.ExecSQL;
ADOQuery3.Active:=True;
StatusBar1.Panels[0].Text:='Row = ' + IntToStr(ADOQuery3.RecordCount) ;
//SetGridClient(DBgrid5);
setgridcolumnwidths(DBgrid3);
  except
   on e:Exception do
  end;

 strQry:='ARC_SERVICES_TUNE';
 try
ADOTable82.Active:=False ;
ADOTable82.TableName:= strQry ;
ADOTable82.Active:=True;
setgridcolumnwidths(DBgrid8);
  except
   on e:Exception do
  end;

  for i:=0 to DBGrid3.Columns.Count-1 do begin
   if DBGrid3.Columns.Items[i].FieldName = 'IS_VIEWABLE' then
     begin
        DBGrid3.Columns.Items[i].PickList.Add('0');
        DBGrid3.Columns.Items[i].PickList.Add('1');
        DBGrid3.Columns.Items[i].ButtonStyle:= cbsAuto; // cbsAuto  cbsEllipsis ;
     end;
   if DBGrid3.Columns.Items[i].FieldName = 'IS_WRITEON' then
     begin
        DBGrid3.Columns.Items[i].PickList.Add('0');
        DBGrid3.Columns.Items[i].PickList.Add('1');
        DBGrid3.Columns.Items[i].ButtonStyle:= cbsAuto; // cbsAuto  cbsEllipsis ;
     end;
   if DBGrid3.Columns.Items[i].FieldName = 'ID_TBLLST' then
     begin
        DBGrid3.Columns.Items[i].ButtonStyle:= cbsEllipsis; // cbsAuto  cbsEllipsis ;
     end;
   if DBGrid3.Columns.Items[i].FieldName = 'ID_GINFO' then
     begin
        DBGrid3.Columns.Items[i].ButtonStyle:= cbsEllipsis; // cbsAuto  cbsEllipsis ;
     end;
  end ;
  DBGrid3.Refresh;


end;


procedure TForm1.ArcHelp(Sender: TObject);
var
 i : Integer ;
 strQry : String ;
label
  GotoLabel;
begin
 i:=TreeView1.Selected.AbsoluteIndex ;
 if i<0 then Goto GotoLabel;
 if i=0 then Goto GotoLabel;
 if i=1 then strQry:='ARC_FTR';
 if i=2 then strQry:='ARC_TYPE';
 if i=3 then strQry:='ARC_SERVICES_TYPE';
 if i=4 then strQry:='ARC_DB_SCHEMA';
 if i=5 then strQry:='ARC_READ_DEFAULTS';
 if i=6 then strQry:='ARC_INTEGRITY_DESC';
 if i=7 then Goto GotoLabel;
 if i=8 then strQry:='SYS_GTOPT';
 if i=9 then strQry:='SYS_GTYP';
 if i=10 then strQry:='SYS_TBLLST';
 if i=11 then Goto GotoLabel;
 if i=12 then strQry:='AD_SERVICE';
 if i=13 then Goto GotoLabel;
 if i=14 then strQry:='ARC_SERVICES_INFO';
 if i=15 then strQry:='ARC_SERVICES_ACCESS';
 if i=16 then strQry:='ARC_VIEW_PARTITIONS';
 if i=17 then strQry:='ARC_READ_VIEW';
 if i=18 then strQry:='ARC_INTEGRITY';
 if i=19 then strQry:='ARC_HIST_PARTITIONS';

 try
ADOTable1.Active:=False ;
ADOTable1.TableName:= strQry ;
ADOTable1.Active:=True;
StatusBar1.Panels[0].Text:='Row = ' + IntToStr(ADOTable1.RecordCount) ;
//SetGridClient(DBgrid5);
setgridcolumnwidths(DBgrid6);
  except
   on e:Exception do
  end;

  GotoLabel:

end;

procedure TForm1.ArcUpdate(Sender: TObject);
var
  i : integer ;
begin
  i:=PageControl1.ActivePageIndex ;
  if i=0 then ArcAll(Sender);
  if i=1 then ArcSystem(Sender);
  if i=2 then ArcProfile(Sender);
  if i=3 then RadioGroupArcJClick(Sender);
  if i=4 then RadioGroupStatClick(Sender);
  //if i=5 then begin ; end ;
  //if i=6 then begin ; end ;
end;


procedure TForm1.ArcCreate(Sender: TObject);
begin
  PageControl1.ActivePageIndex:=0;
  ArcAll(Sender);
  ArcSystem(Sender);
  ArcProfile(Sender);
  RadioGroupArcJClick(Sender);
  RadioGroupStatClick(Sender);
  ArcCalc(Sender);
  with TreeView1 do
   begin
      Items.BeginUpdate;
      FullExpand;
      Items.EndUpdate;
   end;

  PageControl1Change(Sender);
end;


procedure TForm1.ArcCalc(Sender: TObject);
var
 strQry : String ;
begin

 with StringGRid1 do
 begin
    Cells[1, 0]:='Параметры Сбора';
    ColWidths[1]:=5*Length(Cells[1, 0]);
    Cells[2, 0]:='Электрические параметры';
    ColWidths[2]:=5*Length(Cells[2, 0]);
    Cells[3, 0]:='Прочие параметры';
    ColWidths[3]:=5*Length(Cells[3, 0]);
    Cells[4, 0]:='Коммутационные аппараты';
    ColWidths[4]:=5*Length(Cells[4, 0]);
    Cells[5, 0]:='Устройства  защиты';
    ColWidths[5]:=5*Length(Cells[5, 0]);
	
    Cells[6, 0]:='Внешний Сбор';
    ColWidths[6]:=5*Length(Cells[6, 0]);
    Cells[7, 0]:='Диспетчерские графики';
    ColWidths[7]:=5*Length(Cells[7, 0]);	
	
    Cells[8, 0]:='Формула';
    ColWidths[8]:=40*Length(Cells[8, 0]);

    Cells[0, 1]:='Число параметров:';
    Cells[1, 1]:='10000';
    Cells[2, 1]:='7000';
    Cells[3, 1]:='2000';
    Cells[4, 1]:='1000';
    Cells[5, 1]:='1000';
	Cells[6, 1]:='100';
    Cells[7, 1]:='50';
	
	Cells[8, 1]:='п.1';


  strQry:='select count (*) from DA_COLUMN_DATA_V;';
 try
ADOQuery71.Active:=False ;
ADOQuery71.SQL.Clear;
ADOQuery71.SQL.Add(strQry);
ADOQuery71.ExecSQL;
ADOQuery71.Active:=True;
Cells[1, 1]:=IntToStr(ADOQuery71.Fields.Fields[0].AsInteger);
  except
   on e:Exception do
  end;


  strQry:='select count (*) from elreg_list_v;';
 try
ADOQuery71.Active:=False ;
ADOQuery71.SQL.Clear;
ADOQuery71.SQL.Add(strQry);
ADOQuery71.ExecSQL;
ADOQuery71.Active:=True;
Cells[2, 1]:=IntToStr(ADOQuery71.Fields.Fields[0].AsInteger);
  except
   on e:Exception do
  end;

  strQry:='select count (*) from phreg_list_v';
 try
ADOQuery71.Active:=False ;
ADOQuery71.SQL.Clear;
ADOQuery71.SQL.Add(strQry);
ADOQuery71.ExecSQL;
ADOQuery71.Active:=True;
Cells[3, 1]:=IntToStr(ADOQuery71.Fields.Fields[0].AsInteger);
  except
   on e:Exception do
  end;

  strQry:='select count (*) from pswt_list_v';
 try
ADOQuery71.Active:=False ;
ADOQuery71.SQL.Clear;
ADOQuery71.SQL.Add(strQry);
ADOQuery71.ExecSQL;
ADOQuery71.Active:=True;
Cells[4, 1]:=IntToStr(ADOQuery71.Fields.Fields[0].AsInteger);
  except
   on e:Exception do
  end;

  strQry:='select count (*) from auto_list_v';
 try
ADOQuery71.Active:=False ;
ADOQuery71.SQL.Clear;
ADOQuery71.SQL.Add(strQry);
ADOQuery71.ExecSQL;
ADOQuery71.Active:=True;
Cells[5, 1]:=IntToStr(ADOQuery71.Fields.Fields[0].AsInteger);
  except
   on e:Exception do
  end;

  strQry:='select count (*) from EXDATA_LIST_V';
 try
ADOQuery71.Active:=False ;
ADOQuery71.SQL.Clear;
ADOQuery71.SQL.Add(strQry);
ADOQuery71.ExecSQL;
ADOQuery71.Active:=True;
Cells[6, 1]:=IntToStr(ADOQuery71.Fields.Fields[0].AsInteger);
  except
   on e:Exception do
  end;

  strQry:='select count (*) from DG_LIST';
 try
ADOQuery71.Active:=False ;
ADOQuery71.SQL.Clear;
ADOQuery71.SQL.Add(strQry);
ADOQuery71.ExecSQL;
ADOQuery71.Active:=True;
Cells[7, 1]:=IntToStr(ADOQuery71.Fields.Fields[0].AsInteger);
  except
   on e:Exception do
  end;


    Cells[0, 2]:='Ширина одной записи:';
    Cells[1, 2]:='90';
    Cells[2, 2]:='90';
    Cells[3, 2]:='90';
    Cells[4, 2]:='90';
    Cells[5, 2]:='90';
	Cells[6, 2]:='90';
	Cells[7, 2]:='90';
    Cells[8, 2]:='п.2    = 11 + 11 + 11 + 19 + 19 + 19 = 90';

    Cells[0, 3]:='Количество записей в час:';
    Cells[1, 3]:='3600';
    Cells[2, 3]:='1';
    Cells[3, 3]:='1';
    Cells[4, 3]:='1';
    Cells[5, 3]:='1';
    Cells[6, 3]:='1';
    Cells[7, 3]:='1';	
    Cells[8, 3]:='п.3';

    Cells[0, 4]:='Длительность хранения архивов, в часах:';
    Cells[1, 4]:='720';
    Cells[2, 4]:='720';
    Cells[3, 4]:='720';
    Cells[4, 4]:='720';
    Cells[5, 4]:='720';
    Cells[6, 4]:='720';
    Cells[7, 4]:='720';	
    Cells[8, 4]:='п.4';

    Cells[0, 5]:='Объем данных, гб:';
    Cells[1, 5]:='0';
    Cells[2, 5]:='0';
    Cells[3, 5]:='0';
    Cells[4, 5]:='0';
    Cells[5, 5]:='0';
    Cells[6, 5]:='0';
    Cells[7, 5]:='0';	
    Cells[8, 5]:='п.5 = ( n.1 * n.2 * n.3 * n.4 ) / (1024 * 1024 * 1024)';

    Cells[0, 6]:='С учетом индексов (обслуживание поиска, построения графиков, анализа и т.п.), гб:';
    Cells[1, 6]:='0';
    Cells[2, 6]:='0';
    Cells[3, 6]:='0';
    Cells[4, 6]:='0';
    Cells[5, 6]:='0';
    Cells[6, 6]:='0';
    Cells[7, 6]:='0';	
    Cells[8, 6]:='п.6 =  п.5 *50% ';

    Cells[0, 7]:='Дополнительный объем для системных и оперативных объектов БД, гб:';
    Cells[1, 7]:='0';
    Cells[2, 7]:='0';
    Cells[3, 7]:='0';
    Cells[4, 7]:='0';
    Cells[5, 7]:='0';
    Cells[6, 7]:='0';
    Cells[7, 7]:='0';	
    Cells[8, 7]:='п.7 = (п.5 + п.6)*10%';

    Cells[0, 8]:='Дополнительный объем для выполнения резервного копирования БД, гб:';
    Cells[1, 8]:='0';
    Cells[2, 8]:='0';
    Cells[3, 8]:='0';
    Cells[4, 8]:='0';
    Cells[5, 8]:='0';
    Cells[6, 8]:='0';
    Cells[7, 8]:='0';	
    Cells[8, 8]:='п.8 = (п.5 + п.6 + п.7)*50%';

    ColWidths[0]:=4*Length(Cells[0, 8]);

 end;

  ComboBox1.ItemIndex:=12;  // 3 года
  ComboBox1Change(Sender);

end;


procedure TForm1.Button10Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>Процент использования FRA:');
  strQry:='SELECT name, ' +
    'TO_CHAR(SPACE_USED, ''999,999,999,999'') AS "Used", '  +
    'TO_CHAR(SPACE_RECLAIMABLE, ''999,999,999,999'') as "SPACE_RECLAIMABLE" , ' +
    'TO_CHAR(SPACE_LIMIT, ''999,999,999,999'') as "SPACE_LIMIT" , ' +
    'TO_CHAR(SPACE_LIMIT - SPACE_USED + SPACE_RECLAIMABLE, ''999,999,999,999'')' +
    '   AS "Free",'  +
    'ROUND((SPACE_USED - SPACE_RECLAIMABLE)/SPACE_LIMIT * 100, 1)' +
    '   AS "Persent Used" , ' +
    'number_of_files ' +
    'FROM V$RECOVERY_FILE_DEST;';
RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button11Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  strQry:='select num , name , value , description from v$parameter ' ;
RichEdit1.Lines.Add('>>v$parameter');
RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button12Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  strQry:='SELECT id, dt1970, define_alias, state FROM RSDU_UPDATE ORDER BY define_alias Desc ' ;
RichEdit1.Lines.Add('>>RSDU_UPDATE');
RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button13Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  strQry:='SELECT t.tablespace_name, file_name,file_id, autoextensible "AutoExtend", ' +
       'bytes /1024/1024 "Current Size, Mb", t.status, ' +
       't.increment_by* d.block_size /1024/1024 "Increment, Mb", ' +
       ' maxbytes /1024/1024 "Max Size, Mb"  ' +
  ' FROM Dba_Data_Files t, dba_tablespaces d ' +
 ' WHERE d.tablespace_name =  t.tablespace_name ' ;
RichEdit1.Lines.Add('>>tablespace size');
RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button14Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  strQry:='SELECT  ' +
//   ' dir.id dir, ' +
   'users.id id,' +
   'dir2.NAME parname,' +
   'dir.NAME servname, ' +
   'appl.NAME appl, ' +
   'appl.ALIAS applalias,' +
   'users.login login , ' +
   'users.NAME users  ' +
'FROM  AD_DIR dir, AD_DIR dir1, AD_DIR dir2, AD_DTYP dtyp, AD_SINFO sinfo, ' +
 'S_USERS users,  SYS_APPL appl ' +
'WHERE   ' +
 'dtyp.ID = dir1.ID_TYPE AND  ' +
 'dir1.ID = dir.ID_PARENT AND  ' +
 'dir1.ID_PARENT = dir2.ID AND ' +
 'dtyp.DEFINE_ALIAS = ''ADV_APPLICATION'' AND  ' +
 'sinfo.ID_SERVER_NODe = dir.ID AND ' +
 'sinfo.ID_USER = users.ID AND  ' +
 'sinfo.ID_APPL = appl.ID' ;
RichEdit1.Lines.Add('>>Users Serv');
RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button15Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  strQry:='select * from v$version;';
RichEdit1.Lines.Add('>>v$version');
RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button16Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  strQry:='select * from nls_session_parameters';
RichEdit1.Lines.Add('>>nls_session_parameters');
RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;

end;

procedure TForm1.Button17Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  strQry:='select name||'' = ''||value name, null MB ' +
'from v$parameter '+
'where name like ''%undo_tablespace'' '+
'or name like ''instance_name'' '+
'or name like ''instance_number'' '+
'or name like ''cluster_database_instances'' '+
'union all  '  +
'select ''ASM disc ''||rpad (name, 10) || TOTAL_MB ' +
'|| '' MB (''|| FREE_MB||'' free)'', null MB ' +
'from v$asm_diskgroup_stat union all ' +
'select ''DB Files Total SIZE:'' filename, sum (mb) '  +
'MB from ' +
'(select round (sum (f.bytes)/1024/1024) mb ' +
'from v$datafile f union all ' +
'select round (sum (t.bytes)/1024/1024) mb ' +
'from v$tempfile t) union all ' +
'select name, round (bytes/1024/1024) mb from '  +
'v$tempfile union all ' +
'select name, round (bytes/1024/1024) mb from '  +
'v$datafile '  +
'order by mb desc;' ;

RichEdit1.Lines.Add('>>Размер и свободное место');
RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;

end;

procedure TForm1.Button18Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  strQry:='select * from v$sysstat';
RichEdit1.Lines.Add('>>v$sysstat');
RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;

end;

procedure TForm1.Button19Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  strQry:='select a.value  total_cur, ' +
'       s.program, s.username, s.sid, s.serial#, s.client_identifier ' +
'from v$sesstat  a, v$statname b, v$session  s  ' +
'where a.statistic# = b.statistic# ' +
'      and s.sid = a.sid ' +
'      and b.name = ''opened cursors current''  ' +
'union select sum(a.value), NULL, NULL, NULL, NULL, NULL ' +
'       from v$sesstat a, v$statname b ' +
' where a.statistic# = b.statistic# and b.name = ''opened cursors current''  ' ;
RichEdit1.Lines.Add('>>opened cursors current');
RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button1Click(Sender: TObject);
var
  fVal : Extended;
  lVal : Longint ;
  lInt : Longint ;
  eCode : Integer ;
  i,j: Integer ;
begin
 //
 with StringGRid1 do
 begin
  for I := 1 to 5 do begin
   Val(Cells[1, i],lVal,eCode) ; Cells[1, i]:=IntToStr(lVal);
   Val(Cells[2, i],lVal,eCode) ; Cells[2, i]:=IntToStr(lVal);
   Val(Cells[3, i],lVal,eCode) ; Cells[3, i]:=IntToStr(lVal);
   Val(Cells[4, i],lVal,eCode) ; Cells[4, i]:=IntToStr(lVal);
   Val(Cells[5, i],lVal,eCode) ; Cells[5, i]:=IntToStr(lVal);
   Val(Cells[6, i],lVal,eCode) ; Cells[6, i]:=IntToStr(lVal);
   Val(Cells[7, i],lVal,eCode) ; Cells[7, i]:=IntToStr(lVal);
  end ;
  for I := 1 to 7 do begin
    fVal:= (StrToFloat(Cells[i, 1])*StrToFloat(Cells[i, 2])*StrToFloat(Cells[i, 3])*StrToFloat(Cells[i, 4]))/(1024*1024*1024);
    if (fVal<0.01) then fVal:=0.01;
    lInt:=StrToInt(Cells[i, 2]);
    if (lInt<=0) then fVal:=0.0;
    Cells[i, 5]:=FloatToStrF(fVal, ffFixed, 5, 2);
    Cells[i, 6]:=FloatToStr(1.5* StrToFloat(Cells[i, 5]));
    Cells[i, 7]:=FloatToStr(1.1* (StrToFloat(Cells[i, 5])+StrToFloat(Cells[i, 6])));
    Cells[i, 8]:=FloatToStr(1.5* (StrToFloat(Cells[i, 5])+StrToFloat(Cells[i, 6])+StrToFloat(Cells[i, 7])));
  end ;
 end ;
 //
end;

procedure TForm1.Button20Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>v$sga_dynamic_components');
  strQry:='SELECT component, current_size, min_size, max_size FROM v$sga_dynamic_components;';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button21Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>v$pgastat');
  strQry:='SELECT name, value FROM v$pgastat';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button22Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>UNDO');
  strQry:='SELECT d.undo_size/(1024*1024) "ACTUAL UNDO SIZE [MByte]" ' +
     ', SUBSTR(e.value,1,25) "UNDO RETENTION [Sec]" ' +
     ', (TO_NUMBER(e.value) * TO_NUMBER(f.value) * g.undo_block_per_sec) / (1024*1024) "NEEDED UNDO SIZE [MByte]" ' +
     ', ROUND((d.undo_size / (to_number(f.value) * g.undo_block_per_sec))) "OPTIMAL UNDO RETENTION [Sec]" ' +
'FROM (SELECT SUM(a.bytes) undo_size ' +
      'FROM v$datafile a ' +
      '   , v$tablespace b ' +
      '   , dba_tablespaces c ' +
      'WHERE c.contents = ''UNDO'' ' +
      '  AND c.status = ''ONLINE''  ' +
      '  AND b.name = c.tablespace_name ' +
      '  AND a.ts# = b.ts# ' +
     ') d ' +
   ', v$parameter e ' +
   ', v$parameter f ' +
  ' , (SELECT MAX(undoblks/((end_time-begin_time)*3600*24)) undo_block_per_sec ' +
   '   FROM v$undostat ' +
  '   ) g  ' +
'WHERE e.name = ''undo_retention'' ' +
 ' AND f.name = ''db_block_size'' ' ;
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;

end;

procedure TForm1.Button23Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>v$process');
  strQry:='select * from v$process';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button24Click(Sender: TObject);
begin
 RichEdit1.Clear();
end;

procedure TForm1.Button25Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>Сессии - CPU used by this session');
  strQry:='SELECT s.value, n.SID, n.SERIAL#, n.osuser , n.username ,  n.MACHINE , n.PROGRAM ' +
'FROM v$session n , v$sesstat s, v$statname t ' +
'WHERE s.statistic# = t.statistic# ' +
'AND n.sid = s.sid ' +
'AND t.name=''CPU used by this session'' ' +
'ORDER BY s.value desc ';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button26Click(Sender: TObject);
begin
 //
end;

procedure TForm1.Button27Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>Количество открытых курсоров');
  strQry:='SELECT v$session.sid,' +
          'user_name,' +
          'program,' +
          'v$session.action,'+
   ' loaded_versions, ' +
   ' open_versions, ' +
   ' users_opening, ' +
   ' v$sqlarea.sql_text ' +
 'FROM v$open_cursor, v$sqlarea, v$session ' +
 'WHERE v$open_cursor.sid = v$session.sid AND  ' +
 '   v$open_cursor.address = v$sqlarea.address AND ' +
 '   v$open_cursor.hash_value = v$sqlarea.hash_value  ' +
 'ORDER BY 1; ';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button28Click(Sender: TObject);
var
  Res : Integer ;
  //fBm : TBookmarkStr;
  fDt : TDataSet;
  fStr, fStr2 : string;
  fName : String ;
  i : Integer;
  fTf : TextFile;
begin

  DateTimeToString(fStr2, '-yyyymmdd_hhnnss', Now);
  with SaveDialog1 do begin
    InitialDir:=ExtractFilePath(Application.ExeName);
    FileName:='dbstat'+fStr2+'.cvs';
    Filter:='Xls files (*.cvs)|*.cvs|All files (*.*)|*.*';
  end;
  //Открываем диалог сохранения файла.
  if not SaveDialog1.Execute then Exit;

  fName:=SaveDialog1.FileName;

//Если файл с указанным именем уже существует.
  if FileExists(SaveDialog1.FileName) then begin
    Res := MessageDlg(
      'Файл с именем:' + #10
      + '"' + SaveDialog1.FileName + '"' + #10
      + 'Уже существует. Перезаписать?'
      ,mtConfirmation
      ,[mbYes, mbNo]
      ,0
    );
    //Если пользователь отказался перезаписывать файл - выходим.
    if Res = mrNo then Exit;
  end;

  fDt := DBGrid1.DataSource.DataSet;
  fDt.DisableControls;
  //fName:='Arc_stat.cvs';
  AssignFile(fTf, fName);
  Rewrite(fTf);
  try
    fStr := '';
    for i := 0 to Pred(DBGrid1.Columns.Count) do begin
      fStr := fStr + dbgrid1.Columns[i].Title.Caption + ';';
    end;
    Writeln(fTf, fStr);
    fDt.First;
    while not fDt.Eof do begin
      fStr := '';
      for i := 0 to Pred(DBGrid1.Columns.Count) do begin
        if not DBGrid1.Columns[i].Visible then // Пропускаем невидимые столбцы
          Continue;
        fStr := fStr + fDt.FieldByName(DBGrid1.Columns[i].FieldName).AsString + ';';
      end;
      Writeln(fTf, fStr);
      fDt.Next
    end;
  finally
    fDt.EnableControls;
    CloseFile(fTf);
  end;
 //
end;

procedure TForm1.Button29Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>dba_tab_privs-RSDUADMIN');
  strQry:='select owner ,  privilege,  table_name ,  grantee '+
'from dba_tab_privs '+
'where lower(owner) = lower(''RSDUADMIN'')'+
'order by grantee; ';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button2Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>v$open_cursor');
  strQry:='select count(*) from v$open_cursor;';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button30Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>dba_db_links; - о database links: владелец, имя линка, имя схемы....');
  strQry:='select * from dba_db_links';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button31Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>dba_directories - информацию о Oracle-directories: владелец, имя, путь в файловой системе ОС');
  strQry:='select * from  dba_directories';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button32Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>v$locked_object');
  strQry:='select vo.session_id sid , vo.os_user_name , vo.oracle_username ' +
     ', do.owner||''.''||do.object_name object , do.subobject_name ' +
     ', decode(vo.locked_mode, ' +
		 '	   1, ''No Lock'',  ' +
		 '	   2, ''Row Share'',  ' +
		 '	   3, ''Row Exclusive'', ' +
		 '	   4, ''Shared Table'', ' +
		 '	   5, ''Shared Row Exclusive'', ' +
		 '	   6, ''Exclusive'') locked_mode  ' +
'from v$locked_object vo , dba_objects do where vo.object_id=do.object_id ' +
'order by do.owner , do.object_name; ';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button33Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>v$session');
  strQry:='select substr(status,0,1) status  , sid  ' +
     ', to_char(logon_time,''DD.MM HH24:MI:SS'') logon_time  ' +
    ' , username , osuser , machine , program  ' +
    ' , decode(client_info, client_info || '','', '''') || module || decode(action, ''-''|| action, '''') || action action ' +
'from v$session order by status , machine , username ';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button34Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>!!!! Приложение РСДУ - bin\ApplRightsManager.exe - покажет шаблонные права доступа и установленные');
  RichEdit1.Lines.Add('>>');
  RichEdit1.Lines.Add('>>Права доступа Приложений - SYS_APP_SSYST.');
  strQry:='SELECT ' +
'   sapp.ID, sapp.ID_APPL, ' +
'   sapl.NAME, sapl.ALIAS, ' +
'   sapp.ID_SUBSYST, ' +
'   RSUB.NAME , RSUB.ALIAS , ' +
'   sapp.STAND_MASK, sapp.EXT_MASK, sapp.USER_MASK , ' +
'   SAPL.URL  ' +
'FROM SYS_APP_SSYST sapp, SYS_APPL sapl , RSDU_SUBSYST rsub  ' +
'where sapp.ID_APPL=sapl.ID and RSUB.ID=sapp.ID_SUBSYST ' +
'order by 2 ;';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button35Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>!!!! Приложение РСДУ - bin\ApplRightsManager.exe - покажет шаблонные права доступа и установленные');
  RichEdit1.Lines.Add('>>');
  RichEdit1.Lines.Add('>>Права доступа пользователей');
  strQry:='SELECT ' +
'   sur.ID, sur.ID_USER, ' +
'   su.LOGIN, su.NAME, ' +
'   sur.ID_SUBSYST, ' +
'   RSUB.NAME , RSUB.ALIAS , ' +
'   sur.STAND_RIGHTS, sur.EXT_RIGHTS, sur.USER_RIGHTS ' +
'FROM S_U_RGHT sur,S_USERS su, RSDU_SUBSYST rsub ' +
'where sur.ID_USER=su.ID and RSUB.ID=sur.ID_SUBSYST ' +
'order by 2 ';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button36Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>dba_recyclebin');
  strQry:='SELECT * FROM SYS.dba_recyclebin WHERE owner like ''RSDU%'' ';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button37Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>gv$database');
  strQry:='SELECT * FROM gv$database;';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button38Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>gv$asm_diskgroup');
  strQry:='SELECT * FROM gv$asm_diskgroup;';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button39Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>v$resource_limit');
  strQry:='select * from v$resource_limit order by resource_name;';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.dba_synonymsClick(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>dba_synonyms');
  strQry:='SELECT * FROM dba_synonyms where table_owner like ''RSDUA%'' order by 2,3';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button3Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>dbtimezone');
  strQry:='select dbtimezone from dual';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button40Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>v$session');
  strQry:='select null "state", null "count" from dual ' +
' union select ''INACTIVE'', count(*) from v$session where status = ''INACTIVE'' ' +
' union select ''ACTIVE'', count(*) from v$session where status = ''ACTIVE'' ';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button41Click(Sender: TObject);
var
 strQry : String ;
begin
  RichEdit1.Lines.Add(' ');
  RichEdit1.Lines.Add('>>gv$session');
  strQry:='SELECT inst_id, username, module, machine ' +
' FROM gv$session ' +
' WHERE username NOT IN (''SYS'', ''SYSMAN'', ''DBSNMP''); ';
  RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button42Click(Sender: TObject);
begin
 ShellExecute(Form1.Handle, nil, 'calc.exe', nil, nil, SW_SHOW);
end;

procedure TForm1.Button43Click(Sender: TObject);
begin
  ShowMessage('AppBar - Ready!');
end;

function tir(n: integer): string;
var
  i: integer;
  s: string;
begin
  s := '';
  for i := 1 to n do
    s := s + '-';
  tir := s;
end;
 
function adder(s: string; n: integer): string;
var
  i: integer;
  tmp: string;
begin
  tmp := '';
  for i := 1 to n - length(s) do
    tmp := tmp + ' ';
  adder := tmp + s;
end;

procedure TForm1.Button45Click(Sender: TObject);
var
  a: array of integer;
  i, j, dl: integer;
  s1, s2: string;
  str: tstringlist;
  
  Res : Integer ;
  fStr, fStr2 : string;
  fName : String ;
  
begin

  DateTimeToString(fStr2, '-yyyymmdd_hhnnss', Now);
  with SaveDialog1 do begin
    InitialDir:=ExtractFilePath(Application.ExeName);
    FileName:='Arc_Num'+fStr2+'.txt';
    Filter:='txt files (*.txt)|*.txt|All files (*.*)|*.*';
  end;
  //Открываем диалог сохранения файла.
  if not SaveDialog1.Execute then Exit;

  fName:=SaveDialog1.FileName;

//Если файл с указанным именем уже существует.
  if FileExists(SaveDialog1.FileName) then begin
    Res := MessageDlg(
      'Файл с именем:' + #10
      + '"' + SaveDialog1.FileName + '"' + #10
      + 'Уже существует. Перезаписать?'
      ,mtConfirmation
      ,[mbYes, mbNo]
      ,0
    );
    //Если пользователь отказался перезаписывать файл - выходим.
    if Res = mrNo then Exit;
  end;


  str := tstringlist.create;
  setlength(a, StringGRid1.ColCount);
  for i := 0 to StringGRid1.ColCount - 1 do
  begin
    a[i] := 0;
    for j := 0 to StringGRid1.rowcount - 1 do
      if length(StringGRid1.Cells[i, j]) > a[i] then
        a[i] := length(StringGRid1.Cells[i, j]);
  end;
  dl := 0;
  for i := 0 to StringGRid1.ColCount - 1 do
    dl := dl + a[i];
  s1 := tir(dl + StringGRid1.ColCount + 1);
  str.Append(s1);
  for i := 0 to StringGRid1.rowcount - 1 do
  begin
    s2 := '|';
    for j := 0 to StringGRid1.colcount - 1 do
      s2 := s2 + adder(StringGRid1.Cells[j, i], a[j]) + '|';
    str.Append(s2);
    str.Append(s1);
  end;
  str.SaveToFile(fName);
  setlength(a, 0);
  str.free;
 //
end;

procedure TForm1.Button4Click(Sender: TObject);
var
  Res : Integer ;
  //fBm : TBookmarkStr;
  fDt : TDataSet;
  fStr, fStr2 : string;
  fName : String ;
  i : Integer;
  fTf : TextFile;
begin

  DateTimeToString(fStr2, '-yyyymmdd_hhnnss', Now);
  with SaveDialog1 do begin
    InitialDir:=ExtractFilePath(Application.ExeName);
    FileName:='Arc_stat'+fStr2+'.cvs';
    Filter:='Xls files (*.cvs)|*.cvs|All files (*.*)|*.*';
  end;
  //Открываем диалог сохранения файла.
  if not SaveDialog1.Execute then Exit;

  fName:=SaveDialog1.FileName;

//Если файл с указанным именем уже существует.
  if FileExists(SaveDialog1.FileName) then begin
    Res := MessageDlg(
      'Файл с именем:' + #10
      + '"' + SaveDialog1.FileName + '"' + #10
      + 'Уже существует. Перезаписать?'
      ,mtConfirmation
      ,[mbYes, mbNo]
      ,0
    );
    //Если пользователь отказался перезаписывать файл - выходим.
    if Res = mrNo then Exit;
  end;

  fDt := DBGrid5.DataSource.DataSet;
  fDt.DisableControls;
  //fName:='Arc_stat.cvs';
  AssignFile(fTf, fName);
  Rewrite(fTf);
  try
    fStr := '';
    for i := 0 to Pred(DBGrid5.Columns.Count) do begin
      fStr := fStr + dbgrid5.Columns[i].Title.Caption + ';';
    end;
    Writeln(fTf, fStr);
    fDt.First;
    while not fDt.Eof do begin
      fStr := '';
      for i := 0 to Pred(DBGrid5.Columns.Count) do begin
        if not DBGrid5.Columns[i].Visible then // Пропускаем невидимые столбцы
          Continue;
        fStr := fStr + fDt.FieldByName(DBGrid5.Columns[i].FieldName).AsString + ';';
      end;
      Writeln(fTf, fStr);
      fDt.Next
    end;
  finally
    fDt.EnableControls;
    CloseFile(fTf);
  end;
 //
end;

procedure TForm1.Button5Click(Sender: TObject);
var
 pass,login,tns : String ;
begin
   tns:='rsdu1';
   login:='rsduadmin';
   pass:='passme';
   tns:=Edit1.Text ;
   login:=Edit2.Text ;
   pass:=Edit3.Text ;

   // MSDASQL
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
   Form1.Caption:= Form1.Caption + ' - connected (' + ADOConnection1.Version + ') ';
   PageControl1.Enabled:=True ;
   ArcCreate(Sender);
   Button5.Enabled:=False ;
   Reg:=1;
end;

procedure TForm1.Button6Click(Sender: TObject);
var
  Res: LongInt;
  myDateTime : TDateTime;
begin
  myDateTime := StrToDateTime(DateToStr (DateTimePicker1.Date) + ' ' +  TimeToStr (DateTimePicker2.Time));
  Res:=DateTimeToUnix(myDateTime); //GetTime
  Edit4.Text :=  IntToStr(Res) ;
end;

procedure TForm1.Button7Click(Sender: TObject);
var
  Res: LongInt;
  Res2: TDateTime;
begin
   Res:=0;
   try
      Res:=abs(StrToInt(Edit4.Text));
      Edit4.Text:=IntToStr(Res);
   except
       Edit4.Text:='0';
   end;
  Res2:= UnixToDateTime(Res); // преобразовываем число в дату
  DateTimePicker1.Date:=StrToDate(DateToStr(Res2));
  DateTimePicker2.Time:=StrToTime(TimeToStr(Res2));
end;

procedure TForm1.Button8Click(Sender: TObject);
var
 strQry : String ;
begin
RichEdit1.Lines.Add(' ');
RichEdit1.Lines.Add('>>Status');
  strQry:='select host_name, instance_name, startup_time, status, version, archiver, logins from gv$instance;';
RichEdit1.Lines.Add(strQry);
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);
  except
   on e:Exception do
  end;
end;

procedure TForm1.Button9Click(Sender: TObject);
var
 i : Integer;
 s1, done : String ;
 max_id , seq_currval, delta , delta10 , deltat : Integer ;
 strQry : String ;
 r1_tblname,  r1_seqname : String ;
 r1_INCREMENT_BY, r1_last_number, r1_CACHE_SIZE : integer ;
begin
RichEdit1.Lines.Add(' ');
RichEdit1.Lines.Add('>>Выравнивание последовательности');
  strQry:='SELECT t.table_name, s.sequence_name, s.INCREMENT_BY, s.last_number, ' +
        ' decode(s.ORDER_FLAG, ''Y'', 0, s.CACHE_SIZE) CACHE_SIZE ' +
       'FROM user_sequences s,  user_tables t ' +
       'WHERE s.sequence_name = t.table_name||''_''||''S'' ' +
       'ORDER BY 1';
RichEdit1.Lines.Add(strQry);
RichEdit1.Lines.Add('---------');
 try
ADOQuery1.Active:=False ;
ADOQuery1.SQL.Clear;
ADOQuery1.SQL.Add(strQry);
ADOQuery1.ExecSQL;
ADOQuery1.Active:=True;
setgridcolumnwidths(DBgrid1);

RichEdit1.Lines.Add('TABLE         max(id)          currval');
RichEdit1.Lines.Add('-----------------------------');
ADOQuery1.Open ;

for i := 0 to ADOQuery1.RecordCount - 1 do
 begin

  r1_tblname:=ADOQuery1.Fields.Fields[0].AsString;
  r1_seqname:=ADOQuery1.Fields.Fields[1].AsString;
  r1_INCREMENT_BY:=ADOQuery1.Fields.Fields[2].AsInteger;
  r1_last_number:=ADOQuery1.Fields.Fields[3].AsInteger;
  r1_CACHE_SIZE:=ADOQuery1.Fields.Fields[4].AsInteger;

  done:='' ;

  strQry:='SELECT nvl(max(id), 0) from ' + r1_tblname ;
  try
   ADOQuery71.Active:=False ; ADOQuery71.SQL.Clear;
   ADOQuery71.SQL.Add(strQry); ADOQuery71.ExecSQL; ADOQuery71.Active:=True;
   max_id:=ADOQuery71.Fields.Fields[0].AsInteger;
  except
   on e:Exception do
  end;

  deltat:=r1_last_number - r1_CACHE_SIZE ;
  if (deltat<max_id) then begin
    seq_currval:=0;
    while True do begin
      strQry:='SELECT ' + r1_seqname+'.nextval from dual ;';
      try
        ADOQuery71.Active:=False ; ADOQuery71.SQL.Clear;
        ADOQuery71.SQL.Add(strQry); ADOQuery71.ExecSQL; ADOQuery71.Active:=True;
        s1:=ADOQuery71.Fields.Fields[0].AsString;
        if (s1<>'') then seq_currval:=StrToInt(s1);
      except
         on e:Exception do
      end;

     delta:=ceil((max_id-seq_currval)/r1_INCREMENT_BY)*r1_INCREMENT_BY ;
     if (max_id>=0) then break ;
     if (delta<=0) then break ;

     delta10:=r1_INCREMENT_BY*10 ;
     if (delta > delta10) then begin
       strQry:='alter sequence ' + r1_seqname + ' increment by ' + IntToStr(delta) ;
       try
         ADOQuery71.Active:=False ; ADOQuery71.SQL.Clear;
         ADOQuery71.SQL.Add(strQry); ADOQuery71.ExecSQL; ADOQuery71.Active:=True;
        except
         on e:Exception do
       end;

       seq_currval:=0;
       strQry:='SELECT ' + r1_seqname+'.nextval from dual ;';
       try
         ADOQuery71.Active:=False ; ADOQuery71.SQL.Clear;
         ADOQuery71.SQL.Add(strQry); ADOQuery71.ExecSQL; ADOQuery71.Active:=True;
         s1:=ADOQuery71.Fields.Fields[0].AsString;
         if (s1<>'') then seq_currval:=StrToInt(s1);
        except
          on e:Exception do
       end;

       strQry:='alter sequence ' + r1_seqname + ' increment by ' + IntToStr(r1_INCREMENT_BY) ;
       try
         ADOQuery71.Active:=False ; ADOQuery71.SQL.Clear;
         ADOQuery71.SQL.Add(strQry); ADOQuery71.ExecSQL; ADOQuery71.Active:=True;
        except
         on e:Exception do
       end;

       done:= done + ' ' + IntToStr(delta) ;
     end;

    end;

    RichEdit1.Lines.Add(r1_tblname+'  '+IntToStr(max_id)+' '+IntToStr(seq_currval)+' '+done);
  end;
  ADOQuery1.Next ;
  //ADOQuery1.Recordset.MoveNext;
 end ;
ADOQuery1.Close ;
RichEdit1.Lines.Add('---------');
ADOQuery1.Active:=True;

  except
   on e:Exception do
  end;
end;

procedure TForm1.ComboBox1Change(Sender: TObject);
var
  coef : LongInt ;
begin
 //
  coef:=0;
  case (ComboBox1.ItemIndex) of
  0 : coef:=24;
  1 : coef:=2*24;
  2 : coef:=3*24;
  3 : coef:=4*24;
  4 : coef:=5*24;
  5 : coef:=6*24;
  6 : coef:=7*24;
  7 : coef:=24*31;
  8 : coef:=24*40;
  9 : coef:=6*24*31;
  10 : coef:=12*24*31;
  11 : coef:=2*12*24*31;
  12 : coef:=3*12*24*31;
  13 : coef:=4*12*24*31;
  14 : coef:=5*12*24*31;
  end;

   with StringGRid1 do
   begin
    //Cells[1, 4]:=IntToStr(coef);
    Cells[2, 4]:=IntToStr(coef);
    Cells[3, 4]:=IntToStr(coef);
    Cells[4, 4]:=IntToStr(coef);
    Cells[5, 4]:=IntToStr(coef);
	Cells[6, 4]:=IntToStr(coef);
	Cells[7, 4]:=IntToStr(coef);
   end ;

end;

procedure TForm1.DBGrid1TitleClick(Column: TColumn);
begin
   ADOQuery1.sort:=Column.FieldName;
end;

procedure TForm1.DBGrid2EditButtonClick(Sender: TObject);
var
  strQry : String ;
begin
  //
  strQry:= 'sys_gtopt' ;
  if (DBGrid2.SelectedField.FieldName='STATE') then strQry:= 'arc_ftr' ;
  //
  Form2.ADOConnection1.ConnectionString:= ADOConnection1.ConnectionString;
 try
Form2.ADOTable1.Active:=False ;
Form2.ADOTable1.TableName:= strQry ;
//Form2.ADOTable1.Sort:='id asc';
Form2.ADOTable1.Active:=True;
Form2.DBGrid1.ReadOnly:=True;
setgridcolumnwidths(Form2.DBGrid1);
  except
   on e:Exception do
  end;
  Form2.ShowModal();
end;

procedure TForm1.DBGrid2TitleClick(Column: TColumn);
begin
 ADOQuery2.sort:=Column.FieldName;
end;

procedure TForm1.DBGrid3EditButtonClick(Sender: TObject);
var
  strQry : String ;
begin
  //
  strQry:= 'arc_ginfo' ;
  if (DBGrid3.SelectedField.FieldName='ID_TBLLST') then strQry:= 'sys_tbllst' ;

  Form2.ADOConnection1.ConnectionString:= ADOConnection1.ConnectionString;
 try
Form2.ADOTable1.Active:=False ;
Form2.ADOTable1.TableName:= strQry ;
//Form2.ADOTable1.Sort:='id asc';
Form2.ADOTable1.Active:=True;
Form2.DBGrid1.ReadOnly:=True;
setgridcolumnwidths(Form2.DBGrid1);
  except
   on e:Exception do
  end;
  Form2.ShowModal();
end;

procedure TForm1.DBGrid3TitleClick(Column: TColumn);
begin
  ADOQuery3.sort:=Column.FieldName;
end;

procedure TForm1.DBGrid4TitleClick(Column: TColumn);
begin
 ADOQuery4.sort:=Column.FieldName;
end;

procedure TForm1.DBGrid5TitleClick(Column: TColumn);
begin
  ADOTable5.sort:=Column.FieldName;
end;

procedure TForm1.DBGrid6TitleClick(Column: TColumn);
begin
  ADOTable1.sort:=Column.FieldName;
end;

procedure TForm1.DBGrid7TitleClick(Column: TColumn);
begin
    ADOQuery81.sort:=Column.FieldName;
end;

procedure TForm1.DBGrid8TitleClick(Column: TColumn);
begin
   ADOTable82.sort:=Column.FieldName;
end;

end.
