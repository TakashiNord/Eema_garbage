//---------------------------------------------------------------------------

#ifndef Unit1H
#define Unit1H
//---------------------------------------------------------------------------
#include <System.Classes.hpp>
#include <Vcl.Controls.hpp>
#include <Vcl.StdCtrls.hpp>
#include <Vcl.Forms.hpp>
#include <Vcl.ComCtrls.hpp>
#include <Vcl.ExtCtrls.hpp>
#include <Vcl.Mask.hpp>
#include <Vcl.Grids.hpp>
#include <Vcl.ValEdit.hpp>
#include <VCLTee.Chart.hpp>
#include <VclTee.TeeGDIPlus.hpp>
#include <VCLTee.TeEngine.hpp>
#include <VCLTee.TeeProcs.hpp>
#include <VCLTee.Series.hpp>
//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
	TPageControl *PageControl1;
	TTabSheet *TabSheet1;
	TTabSheet *TabSheet2;
	TTabSheet *TabSheet3;
	TTabSheet *TabSheet4;
	TGroupBox *GroupBox1;
	TLabel *Label1;
	TComboBox *ComboBox1;
	TButton *Button1;
	TButton *Button2;
	TGroupBox *GroupBox2;
	TCheckBox *CheckBox1;
	TCheckBox *CheckBox2;
	TStaticText *StaticText1;
	TTimer *Timer1;
	TEdit *Edit1;
	TEdit *Edit2;
	TUpDown *UpDown1;
	TUpDown *UpDown2;
	TRadioGroup *RadioGroup1;
	TRadioGroup *RadioGroupSrcOwn;
	TEdit *OwnEdit;
	TRadioGroup *RadioGroupSrcU;
	TEdit *OEditmax;
	TEdit *OEditmin;
	TMemo *Memo1;
	TRichEdit *RichEditControl;
	TChart *Chart1;
	TChart *ChartControl;
	TStringGrid *StringGridControl;
	TLineSeries *Series1;
	TLineSeries *Series2;
	TLineSeries *Series3;
	TButton *ButtonAdd;
	TButton *ButtonDel;
	TButton *ButtonBuild;
	TEdit *OwnEditDist;
	TLabel *Label2;
	TLabel *Label3;
	TButton *ButtonList;
	TButton *ButtonCreate;
	void __fastcall RadioGroup1Click(TObject *Sender);
	void __fastcall ButtonDelClick(TObject *Sender);
	void __fastcall ButtonAddClick(TObject *Sender);
	void __fastcall ButtonBuildClick(TObject *Sender);
	void __fastcall ButtonListClick(TObject *Sender);
	void __fastcall ButtonCreateClick(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
