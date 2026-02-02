// CviPanelParser.cpp : Defines the entry point for the console application.
//


#include "stdafx.h"

#include <fstream>
#include <iostream>
#include <iomanip>
#include <iterator>

#include <string>
#include <sstream>

#include "codecvt.h"

//
//  NLS Code Page Dependent APIs.
//

#define NOT_FIRST_COLUMN if(flagNotFirst){outfile << L"," << endl;} else {flagNotFirst = 1;};

using namespace std;
int flagNotFirst = 0;
int g_hPanel = -1;

void ClosePanel()
{
	if (g_hPanel >= 0)
	{
		DiscardPanel(g_hPanel);
		g_hPanel = -1;
	}
}

//
// ѕреобразование из 10 в 16 с добавлением ведущих нулей, до 6 символов
//

string Int2HexWeb(int iVal)
{
	char buf[16];
	string str = _itoa(iVal,buf,16);
	return string("#000000").substr(0, 7 - str.length()) + str;
}

int CVICALLBACK GraphPanelCallback(int panel, int evnt, void *callbackData, int eventData1, int eventData2)
{
	switch (evnt)
	{
	case EVENT_CLOSE:
		ClosePanel();
		QuitUserInterface(0);
		break;
	}
	return 0;
}

string str_replace(string str, string str_find,string str_repl)
{
	string::size_type ind;
    while((ind=str.find(str_find))!=string::npos) str.replace(ind, str_find.size(), str_repl);

	return str;    
}


void PanelTextMsg(std::basic_ofstream<wchar_t> &outfile, string textId, int x, int y,int h, int w, string textMsg,int attr_label_bold, int attr_label_color, int attr_bg_color, int attr_text_justify = 0, int attr_text_point_size = 16)
{
	USES_CONVERSION;
	NOT_FIRST_COLUMN;
		
	int flagColumn = 0;

	textMsg = str_replace(textMsg,"\n"," ");
	textMsg = str_replace(textMsg,"'"," ");
	
	outfile << L"{" << endl;
	outfile << L"xtype: 'label'," << endl;

	outfile << L"x: " << x << L"," << endl;
	outfile << L"y: " << y << L"," << endl;
	
	if(h != 0 && w != 0)
	{
		outfile << L"height:" << h << L"," << endl;
		outfile << L"width:" << w << L"," << endl;
	}

	outfile << L"style: 'padding:2px 0 2px 4px";
	flagColumn = 1;

	if(attr_label_bold == 1 || attr_label_color != 0 || attr_bg_color != 0 || attr_text_justify != 0 || attr_text_point_size != 16) 
	{
		
		if(attr_text_point_size != 16 ) 
		{ 
			if(flagColumn) 
				outfile << L";";
			else
				flagColumn = 1;

			outfile << L"font-size: " << (int)(attr_text_point_size*3/4) <<"pt";
		}

		if(attr_label_bold == 1 ) 
		{ 
			if(flagColumn) 
				outfile << L";";
			else
				flagColumn = 1;

			outfile << L"font-weight:bold";
		}

		if(attr_label_color != 0 ) 
		{
			if(flagColumn) 
				outfile << L";";
			else
				flagColumn = 1;
				
			outfile << L"color:" << A2W(Int2HexWeb(attr_label_color).c_str());
		}
		
		if(attr_bg_color != VAL_TRANSPARENT ) 
		{
			if(flagColumn) 
				outfile << L";";
			else
				flagColumn = 1;
				
			outfile << L"background-color:" << A2W(Int2HexWeb(attr_bg_color).c_str());
		}
		
		if(attr_text_justify != 0 ) 
		{
			if(flagColumn) 
				outfile << L";";
			else
				flagColumn = 1;
			
			outfile << L"text-align:" ;

			if(attr_text_justify == VAL_RIGHT_JUSTIFIED)
				outfile << L"right";
			else
				if(attr_text_justify == VAL_CENTER_JUSTIFIED)
				outfile << L"center";
		}
	}

	outfile << L"'," << endl;

	outfile << L"id: '" << A2W(textId.c_str()) << L"'," << endl;
	outfile << "text: '" << A2W(textMsg.c_str()) << "'" << endl;
	outfile << L"}";
}

void PanelNumeric(std::basic_ofstream<wchar_t> &outfile, string textId, int x,int y, int h, int w, int attr_precision, int zplane = 999 )
{
	USES_CONVERSION;
	NOT_FIRST_COLUMN;

	outfile << L"{" << endl;
	outfile << L"xtype: 'textfield'," << endl;
	outfile << L"x: " << x << L"," << endl;
	outfile << L"y: " << y << L"," << endl;
	outfile << L"width: "<< w << L"," << endl;
	outfile << L"readOnly: true," << endl;
	outfile << L"style:{'z-index': "<< zplane << L"}," << endl;
	outfile << L"decimalPrecision: " << attr_precision << L"," << endl;
	outfile << L"id: '" << A2W(textId.c_str()) << L"'," << endl;
	outfile << L"hideLabel: true," << endl;
	outfile << L"disabled: true" << endl;
	outfile << L"}";
}

void PanelPictureRing(std::basic_ofstream<wchar_t> &outfile, string textId, int x, int y, int h, int w)
{
	USES_CONVERSION;
	NOT_FIRST_COLUMN;

	outfile << L"{" << endl;
	outfile << L"xtype: 'fieldset'," << endl;
	outfile << L"x: " << x << L"," << endl;
	outfile << L"y: " << y << L"," << endl;
	outfile << L"height: "<< (h+4) << L"," << endl;
	outfile << L"width: "<< (w+4) << L"," << endl;
	outfile << L"id: '" << A2W(textId.c_str()) << L"'," << endl;
	outfile << L"vstyle: {0:[{background: \"url('/icons/TS0.png') center no-repeat\"}],1:[{background: \"url('/icons/TS1.png') center no-repeat\"}]}," << endl;
	outfile << L"cls: 'wasutp_ts_state_default'"  << endl;
	outfile << L"}";
}

void PanelRoundLed(std::basic_ofstream<wchar_t> &outfile, string textId, int x, int y, int h, int w, int attr_on_color, int attr_off_color, int zplane = 999 )
{
	USES_CONVERSION;
	NOT_FIRST_COLUMN;
		
	outfile << L"{" << endl;
	outfile << L"xtype: 'fieldset'," << endl;
	outfile << L"x: " << x << L"," << endl;
	outfile << L"y: " << y << L"," << endl;
	outfile << L"height: "<< h << L"," << endl;
	outfile << L"width: "<< w << L"," << endl;
	outfile << L"id: '" << A2W(textId.c_str()) << L"'," << endl;
	outfile << L"style:{'z-index': "<< zplane << L"}," << endl; 
	outfile << L"vstyle:{ 0:[{background: \"" << A2W( Int2HexWeb(attr_off_color).c_str())  << L"\"}],1:[{background: \"" <<  A2W(Int2HexWeb(attr_on_color).c_str()) << L"\"}]}," << endl;
	outfile << L"cls: 'wasutp_led_state_default'"  << endl;
	outfile << L"}";
}

void PanelSquareLed(std::basic_ofstream<wchar_t> &outfile, string textId, int x, int y, int h, int w, int attr_on_color, int attr_off_color, int zplane = 999 )
{
	USES_CONVERSION;
	NOT_FIRST_COLUMN;

	outfile << L"{" << endl;
	outfile << L"xtype: 'fieldset'," << endl;
	outfile << L"x: " << x << L"," << endl;
	outfile << L"y: " << y << L"," << endl;
	outfile << L"height: "<< h << L"," << endl;
	outfile << L"width: "<< w << L"," << endl;
	outfile << L"style:{'z-index': "<< zplane << L"}," << endl;
	outfile << L"id: '" << A2W(textId.c_str()) << L"'," << endl;
	outfile << L"vstyle:{ 0:[{background: \"" << A2W( Int2HexWeb(attr_off_color).c_str()) << L"\"}],1:[{background: \"" << A2W( Int2HexWeb(attr_on_color).c_str()) << L"\"}]}," << endl;
	outfile << L"cls: 'wasutp_led_sq_state_default'"  << endl;
	outfile << L"}";
}

void PanelSquareBorder(std::basic_ofstream<wchar_t> &outfile, string textId, int x, int y, int h, int w,int attr_bg_color=0)
{
	USES_CONVERSION;
	NOT_FIRST_COLUMN;

	
	outfile << L"{" << endl;
	outfile << L"xtype: 'fieldset'," << endl;
	outfile << L"x: " << x << L"," << endl;
	outfile << L"y: " << y << L"," << endl;
	outfile << L"height: "<< h << L"," << endl;
	outfile << L"width: "<< w << L"," << endl;

	if(attr_bg_color != 0) 
	{
		outfile << L"style: '";
			
		if(attr_bg_color != 0 ) 
		{
			outfile << L"background-color:" << A2W( Int2HexWeb(attr_bg_color).c_str());
		}
		

		outfile << L"'," << endl;
	}

	outfile << L"id: '" << A2W(textId.c_str()) << L"'" << endl;
	outfile << L"}";
}

void PanelCommandButton(std::basic_ofstream<wchar_t> &outfile, string textId, int x, int y, int h, int w, string textLabel, int zplane = 999 )
{
	USES_CONVERSION;
	NOT_FIRST_COLUMN;

	textLabel = str_replace(textLabel,"\n"," ");
	textLabel = str_replace(textLabel,"'"," ");

	outfile << L"{" << endl;
	outfile << L"xtype: 'button'," << endl;
	outfile << L"x: " << x << L"," << endl;
	outfile << L"y: " << y << L"," << endl;
	outfile << L"height: "<< h << L"," << endl;
	outfile << L"width: "<< w << L"," << endl;
	outfile << L"style:{'z-index': "<< zplane << L"}," << endl;
	outfile << "text: '" << A2W(textLabel.c_str()) << "'," << endl;
	outfile << L"id: '" << A2W(textId.c_str()) << L"'," << endl;
	outfile << L"onClick: function(){ panelsHelper.openWindow(this.isContained.id,this.id);}" << endl;
	outfile << L"}";
}

int _tmain(int argc, _TCHAR* argv[])
{
	USES_CONVERSION;

	if (argc < 2)
	{
		cout << "ѕередайте в качестве аргумента путь до панели";
		return -1;
	}

	if (InitCVIRTE(0, 0, 0) == 0)
	{
		cout << "Ќе удалось инициализировать CVI";
		return -1; 
	}

	SetSleepPolicy(VAL_SLEEP_MORE);

	g_hPanel = LoadPanel(0, argv[1], 1);

	if (g_hPanel < 0)
	{
		cout << "Ќе удалось открыть панель";
		return -1; 
	}

	InstallPanelCallback(g_hPanel, GraphPanelCallback, 0);
	DisplayPanel(g_hPanel);

	bool showPanel = false; // true, если нужно показать панель, ее закрытие осуществл€етс€ нажатием на крестик
	
	if (argc > 2 && (string(argv[2]).find("s") == 0))
	{
		showPanel = true; 
	}
	
	int nextControl = 0;
	int y = 0, x = 0;
	int h = 0, w = 0;

	char attr_constant_name[2048];
	char attr_dflt_value[2048];

	char driveName[255];
	char dirName[255];
	char fileName[255];

	char attr_label_text[2048];

	int attr_label_top;
	int attr_label_left;
	int attr_label_visible;
	int attr_ctrl_style;
	int attr_label_bold;
	int attr_label_color;
	int attr_bg_color;
	int attr_text_justify;
	int attr_size_to_text;
	int attr_precision;
	int attr_text_point_size;
	int attr_zplane_position;
	int attr_ctrl_tab_position;
	
	string so;

	SplitPath(argv[1],driveName,dirName,fileName);
	string panelName = str_replace(str_replace(string(fileName).substr(0,string(fileName).find(".uir"))," ","_"),"-","_");
	string outputFileName = (driveName) + string(dirName) + panelName + ".js";
			
	GetPanelAttribute(g_hPanel, ATTR_PANEL_FIRST_CTRL,	&nextControl);
	GetPanelAttribute(g_hPanel, ATTR_TITLE,				&attr_label_text);
	GetPanelAttribute(g_hPanel, ATTR_HEIGHT,			&h);
	GetPanelAttribute(g_hPanel, ATTR_WIDTH,				&w);
	GetPanelAttribute(g_hPanel, ATTR_BACKCOLOR,			&attr_bg_color);
	
	std::basic_ofstream<wchar_t> outfile(A2W(outputFileName.c_str()));
	outfile.imbue(std::locale(outfile.getloc(), new utf8_conversion));
		
	outfile << L"Ext.define('MyPanels." << A2W(panelName.c_str()) << L"', {" << endl;
	outfile << L"id: '" << A2W(panelName.c_str()) << L"'," << endl;
	outfile << L"title: '"<< A2W(attr_label_text) << L"'," << endl;
	outfile << L"width: "<< w+16 << L"," << endl;
	outfile << L"height: "<< h+32 << L"," << endl;
	outfile << L"layout: { type: 'absolute'}," << endl;
	outfile << L"bodyStyle: 'background-color:" << A2W( Int2HexWeb(attr_bg_color).c_str()) <<"',"<< endl;
	outfile << L"items: [" << endl;

	while (nextControl != 0)
	{
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_CONSTANT_NAME,		&attr_constant_name); 
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_CTRL_STYLE,		&attr_ctrl_style); 
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_DFLT_VALUE,		&attr_dflt_value);
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_TEXT,		&attr_label_text);
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_VISIBLE,		&attr_label_visible);
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_TOP,			&attr_label_top);
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_LEFT,		&attr_label_left);
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_LEFT,				&x);
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_TOP,				&y);
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_HEIGHT,			&h);
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_WIDTH,				&w);
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_ZPLANE_POSITION,	&attr_zplane_position);
		GetCtrlAttribute(g_hPanel, nextControl, ATTR_CTRL_TAB_POSITION,	&attr_ctrl_tab_position);
						
		switch(attr_ctrl_style)
		{
			case CTRL_TEXT_MSG:
			case CTRL_TEXT_BOX:
			case CTRL_TEXT_BOX_LS:
				GetCtrlAttribute(g_hPanel, nextControl, ATTR_TEXT_BOLD,			&attr_label_bold);
				GetCtrlAttribute(g_hPanel, nextControl, ATTR_TEXT_COLOR,		&attr_label_color);
				GetCtrlAttribute(g_hPanel, nextControl, ATTR_TEXT_BGCOLOR,		&attr_bg_color);
				GetCtrlAttribute(g_hPanel, nextControl, ATTR_TEXT_JUSTIFY,		&attr_text_justify);
				GetCtrlAttribute(g_hPanel, nextControl, ATTR_SIZE_TO_TEXT,		&attr_size_to_text);
				GetCtrlAttribute(g_hPanel, nextControl, ATTR_TEXT_POINT_SIZE,	&attr_text_point_size);
				
				
				if(attr_size_to_text)
				{
					h = 0;
					w = 0;
				}
												
				PanelTextMsg(outfile,attr_constant_name,x,y,h,w,attr_dflt_value,attr_label_bold,attr_label_color,attr_bg_color,attr_text_justify,attr_text_point_size);
			break;

			case CTRL_NUMERIC:
			case CTRL_NUMERIC_LS:
				GetCtrlAttribute(g_hPanel, nextControl, ATTR_PRECISION,				&attr_precision);
				
				PanelNumeric(outfile,attr_constant_name,x,y,h,w,attr_precision);

				if(attr_label_visible && string(attr_label_text).length() != 0)
				{
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_BOLD,			&attr_label_bold);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_COLOR,			&attr_label_color);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_BGCOLOR,			&attr_bg_color);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_HEIGHT,			&h);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_WIDTH,			&w);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_SIZE_TO_TEXT,	&attr_size_to_text);
										
				
					if(attr_size_to_text)
					{
						h = 0;
						w = 0;
					}
										
					PanelTextMsg(outfile,string(attr_constant_name).append("_LABEL"),attr_label_left,attr_label_top,h,w,attr_label_text,attr_label_bold,attr_label_color,attr_bg_color);
				}
			break;

			case CTRL_PICTURE_RING:
			case CTRL_PICTURE_RING_LS:
				PanelPictureRing(outfile,attr_constant_name,x,y,h,w);

				if(attr_label_visible && string(attr_label_text).length() != 0)
				{
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_BOLD,			&attr_label_bold);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_COLOR,			&attr_label_color);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_BGCOLOR,			&attr_bg_color);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_HEIGHT,			&h);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_WIDTH,			&w);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_SIZE_TO_TEXT,	&attr_size_to_text);
				
					if(attr_size_to_text)
					{
						h = 0;
						w = 0;
					}
					
					PanelTextMsg(outfile,string(attr_constant_name).append("_LABEL"),attr_label_left,attr_label_top,h,w,attr_label_text,attr_label_bold,attr_label_color,attr_bg_color);
				}
			break;

			case CTRL_ROUND_LIGHT:
			case CTRL_ROUND_LED:
			case CTRL_ROUND_LED_LS:
				{
				int attr_on_color,attr_off_color;

				GetCtrlAttribute(g_hPanel, nextControl, ATTR_ON_COLOR,	&attr_on_color);
				GetCtrlAttribute(g_hPanel, nextControl, ATTR_OFF_COLOR,	&attr_off_color);
				
				PanelRoundLed(outfile,attr_constant_name,x,y,h,w,attr_on_color,attr_off_color);

				if(attr_label_visible && string(attr_label_text).length() != 0)
				{
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_BOLD,			&attr_label_bold);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_COLOR,			&attr_label_color);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_BGCOLOR,			&attr_bg_color);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_HEIGHT,			&h);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_WIDTH,			&w);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_SIZE_TO_TEXT,	&attr_size_to_text);
				
					if(attr_size_to_text)
					{
						h = 0;
						w = 0;
					}
					
										
					PanelTextMsg(outfile,string(attr_constant_name).append("_LABEL"),attr_label_left,attr_label_top,h,w,attr_label_text,attr_label_bold,attr_label_color,attr_bg_color);
				}
				}
			break;

			case CTRL_SQUARE_LIGHT:
			case CTRL_SQUARE_LED:
			case CTRL_SQUARE_LED_LS:
				{
				int attr_on_color,attr_off_color;

				GetCtrlAttribute(g_hPanel, nextControl, ATTR_ON_COLOR,	&attr_on_color);
				GetCtrlAttribute(g_hPanel, nextControl, ATTR_OFF_COLOR,	&attr_off_color);

				PanelSquareLed(outfile,attr_constant_name,x,y,h,w,attr_on_color,attr_off_color);

				if(attr_label_visible && string(attr_label_text).length() != 0)
				{
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_BOLD,			&attr_label_bold);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_COLOR,			&attr_label_color);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_BGCOLOR,			&attr_bg_color);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_HEIGHT,			&h);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_WIDTH,			&w);
					GetCtrlAttribute(g_hPanel, nextControl, ATTR_LABEL_SIZE_TO_TEXT,	&attr_size_to_text);
				
					if(attr_size_to_text)
					{
						h = 0;
						w = 0;
					}
															
					PanelTextMsg(outfile,string(attr_constant_name).append("_LABEL"),attr_label_left,attr_label_top,h,w,attr_label_text,attr_label_bold,attr_label_color,attr_bg_color);
				}
				}

			break;

			case CTRL_SQUARE_COMMAND_BUTTON:
			case CTRL_OBLONG_COMMAND_BUTTON:
			case CTRL_ROUND_COMMAND_BUTTON:
			case CTRL_ROUNDED_COMMAND_BUTTON:
			case CTRL_PICTURE_COMMAND_BUTTON:
			case CTRL_SQUARE_COMMAND_BUTTON_LS:
			case CTRL_PICTURE_COMMAND_BUTTON_LS:
				
				PanelCommandButton(outfile,attr_constant_name,x,y,h,w,(attr_label_visible)?attr_label_text:"",attr_zplane_position);

			break;

			case CTRL_PICTURE:
			case CTRL_PICTURE_LS:
				GetCtrlAttribute(g_hPanel, nextControl, ATTR_PICT_BGCOLOR, &attr_bg_color);
				
				PanelSquareBorder(outfile,attr_constant_name,x,y,h,w,attr_bg_color);
			break;

			
			case CTRL_RAISED_BOX:
			case CTRL_RECESSED_BOX:
			case CTRL_FLAT_BOX:
			case CTRL_RAISED_CIRCLE:
			case CTRL_RECESSED_CIRCLE:
			case CTRL_FLAT_CIRCLE:
			case CTRL_RAISED_FRAME:
			case CTRL_RECESSED_FRAME:
			case CTRL_FLAT_FRAME:
			case CTRL_RAISED_ROUND_FRAME:
			case CTRL_RECESSED_ROUND_FRAME:
			case CTRL_FLAT_ROUND_FRAME:
			case CTRL_RAISED_ROUNDED_BOX:
			case CTRL_RECESSED_ROUNDED_BOX:
			case CTRL_FLAT_ROUNDED_BOX:
			case CTRL_RECESSED_BOX_LS:
			case CTRL_SMOOTH_HORIZONTAL_BOX_LS:
			case CTRL_RECESSED_NARROW_FRAME:
			case CTRL_RAISED_BOX_LS:
			case CTRL_SMOOTH_VERTICAL_BOX_LS:
			case CTRL_HORIZONTAL_SPLITTER_LS:
			case CTRL_VERTICAL_SPLITTER_LS:
				PanelSquareBorder(outfile,attr_constant_name,x,y,h,w);
			break;
						
			default:
				cout << "! element Id: " << attr_constant_name << " [" << attr_ctrl_style << "], x = " << x << ", y = " << y << ", h = " << h << ", w = " << w << endl;
			break;
		}

		GetCtrlAttribute(g_hPanel, nextControl, ATTR_NEXT_CTRL, &nextControl);
	}

	outfile << endl;
	outfile << L"]" << endl;
	outfile << L"});" << endl;

	outfile.close();
		
	if (showPanel)
		RunUserInterface();
	else
		ClosePanel();

	CloseCVIRTE();

	return 0;
}

