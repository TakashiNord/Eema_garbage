Ext.define('MyPanels.meteo_ses', {
id: 'meteo_ses',
title: 'Метео и СГО',
width: 1343,
height: 716,
layout: { type: 'absolute'},
bodyStyle: 'background-color:#d4d0c8',
items: [
{
xtype: 'label',
x: 1153,
y: 445,
style: 'padding:1px 0 2px 4px;z-index: 9838 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ;text-decoration: underline; text-decoration-color: #000066 ;',
id: 'TEXTMSG_87',
text: 'ПС 330 кВ'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_18',
x: 1226,
y: 525,
height: 24,
width: 50,
style:'z-index: 9818',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1188,
y: 530,
height:15,
width:38,
style: 'padding:1px 0 2px 4px;z-index: 9818 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_18_LABEL',
text: 'Титан'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_19',
x: 1226,
y: 502,
height: 24,
width: 50,
style:'z-index: 9817',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1152,
y: 507,
height:15,
width:74,
style: 'padding:1px 0 2px 4px;z-index: 9817 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_19_LABEL',
text: 'Оленегорск'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_36',
x: 1226,
y: 458,
height: 24,
width: 50,
style:'z-index: 9825',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1165,
y: 463,
style: 'padding:1px 0 2px 4px;z-index: 9825 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_36_LABEL',
text: 'Выходной'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_20',
x: 1226,
y: 480,
height: 24,
width: 50,
style:'z-index: 9816',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1152,
y: 485,
style: 'padding:1px 0 2px 4px;z-index: 9816 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_20_LABEL',
text: 'Мончегорск'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_176',
x: 1224,
y: 353,
height: 24,
width: 50,
style:'z-index: 9999',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1177,
y: 357,
height:15,
width:45,
style: 'padding:1px 0 2px 4px;z-index: 9999 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_176_LABEL',
text: 'ПС 409'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_174',
x: 1224,
y: 329,
height: 24,
width: 50,
style:'z-index: 9997',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1177,
y: 333,
height:15,
width:45,
style: 'padding:1px 0 2px 4px;z-index: 9997 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_174_LABEL',
text: 'ПС 401'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_16',
x: 1224,
y: 309,
height: 24,
width: 50,
style:'z-index: 9814',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1177,
y: 313,
height:15,
width:45,
style: 'padding:1px 0 2px 4px;z-index: 9814 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_16_LABEL',
text: 'ПС 388'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_32',
x: 1224,
y: 287,
height: 24,
width: 50,
style:'z-index: 9821',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1177,
y: 291,
height:15,
width:49,
style: 'padding:1px 0 2px 4px;z-index: 9821 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_32_LABEL',
text: 'ПС 385'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_172',
x: 1224,
y: 216,
height: 24,
width: 50,
style:'z-index: 9995',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1186,
y: 219,
height:15,
width:38,
style: 'padding:1px 0 2px 4px;z-index: 9995 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_172_LABEL',
text: 'ПС 98'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_175',
x: 1224,
y: 262,
height: 24,
width: 50,
style:'z-index: 9998',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1179,
y: 265,
height:15,
width:45,
style: 'padding:1px 0 2px 4px;z-index: 9998 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_175_LABEL',
text: 'ПС 101'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_35',
x: 1224,
y: 239,
height: 24,
width: 50,
style:'z-index: 9824',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1186,
y: 242,
height:15,
width:38,
style: 'padding:1px 0 2px 4px;z-index: 9824 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_35_LABEL',
text: 'ПС 99'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_169',
x: 1224,
y: 188,
height: 24,
width: 50,
style:'z-index: 9992',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1185,
y: 193,
style: 'padding:1px 0 2px 4px;z-index: 9992 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_169_LABEL',
text: 'ПС 50'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_21',
x: 1224,
y: 166,
height: 24,
width: 50,
style:'z-index: 9820',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1186,
y: 171,
style: 'padding:1px 0 2px 4px;z-index: 9820 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_21_LABEL',
text: 'ПС 36'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_22',
x: 1224,
y: 143,
height: 24,
width: 50,
style:'z-index: 9819',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1185,
y: 147,
style: 'padding:1px 0 2px 4px;z-index: 9819 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_22_LABEL',
text: 'ПС 29'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_173',
x: 1224,
y: 120,
height: 24,
width: 50,
style:'z-index: 9996',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1185,
y: 125,
style: 'padding:1px 0 2px 4px;z-index: 9996 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_173_LABEL',
text: 'ПС 28'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_8',
x: 1224,
y: 97,
height: 24,
width: 50,
style:'z-index: 9813',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1185,
y: 102,
style: 'padding:1px 0 2px 4px;z-index: 9813 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_8_LABEL',
text: 'ПС 21'
},
{
xtype: 'panel-dial',
x: 960,
y: 222,
height: 132,
width: 132,
style:'z-index: 9804',
minValue: 0,
maxValue: 360,
circleBgColor: '#d4d0c8',
arrowColor: '#ff0000',
dashColor: 'black',
circleColor: 'black',
id: 'NUMERICGAUGE_9',
},
{
xtype: 'label',
x: 973,
y: 190,
style: 'padding:1px 0 2px 4px;z-index: 9804 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERICGAUGE_9_LABEL',
text: 'Направление ветра'
},
{
xtype: 'label',
x: 1071,
y: 336,
style: 'padding:1px 0 2px 4px;z-index: 9812 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_195',
text: 'юв'
},
{
xtype: 'label',
x: 960,
y: 337,
style: 'padding:1px 0 2px 4px;z-index: 9811 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_196',
text: 'юз'
},
{
xtype: 'label',
x: 1076,
y: 228,
style: 'padding:1px 0 2px 4px;z-index: 9810 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_197',
text: 'св'
},
{
xtype: 'label',
x: 957,
y: 225,
style: 'padding:1px 0 2px 4px;z-index: 9809 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_198',
text: 'сз'
},
{
xtype: 'label',
x: 1022,
y: 204,
style: 'padding:1px 0 2px 4px;z-index: 9805 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_199',
text: 'C'
},
{
xtype: 'label',
x: 1021,
y: 354,
style: 'padding:1px 0 2px 4px;z-index: 9806 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_200',
text: 'Ю'
},
{
xtype: 'button',
x: 824,
y: 530,
height: 20,
width: 50,
text: 'График',
id: 'COMMANDBUTTON_306',
ui: 'normal',
style: ' color: #000000 ; z-index: 9887 ;border: 1px outset #000000; border-radius: 2px;;background : #c0c0c0 ;background-color: #c0c0c0 ;background-image: linear-gradient( to top left, rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2) 30%, rgba(0, 0, 0, 0) ); ;box-shadow: inset 1px 1px 2px rgba(255, 255, 255, 0.6), inset -1px -1px 2px rgba(0, 0, 0, 0.6); ;',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 600,
y: 531,
height: 20,
width: 50,
text: 'График',
id: 'COMMANDBUTTON_307',
ui: 'normal',
style: ' color: #000000 ; z-index: 9886 ;border: 1px outset #000000; border-radius: 2px;;background : #c0c0c0 ;background-color: #c0c0c0 ;background-image: linear-gradient( to top left, rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2) 30%, rgba(0, 0, 0, 0) ); ;box-shadow: inset 1px 1px 2px rgba(255, 255, 255, 0.6), inset -1px -1px 2px rgba(0, 0, 0, 0.6); ;',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 145,
y: 532,
height: 20,
width: 50,
text: 'График',
id: 'COMMANDBUTTON_309',
ui: 'normal',
style: ' color: #000000 ; z-index: 9888 ;border: 1px outset #000000; border-radius: 2px;;background : #c0c0c0 ;background-color: #c0c0c0 ;background-image: linear-gradient( to top left, rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2) 30%, rgba(0, 0, 0, 0) ); ;box-shadow: inset 1px 1px 2px rgba(255, 255, 255, 0.6), inset -1px -1px 2px rgba(0, 0, 0, 0.6); ;',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 370,
y: 532,
height: 20,
width: 50,
text: 'График',
id: 'COMMANDBUTTON_310',
ui: 'normal',
style: ' color: #000000 ; z-index: 9885 ;border: 1px outset #000000; border-radius: 2px;;background : #c0c0c0 ;background-color: #c0c0c0 ;background-image: linear-gradient( to top left, rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2) 30%, rgba(0, 0, 0, 0) ); ;box-shadow: inset 1px 1px 2px rgba(255, 255, 255, 0.6), inset -1px -1px 2px rgba(0, 0, 0, 0.6); ;',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 824,
y: 8,
height: 20,
width: 50,
text: 'График',
id: 'COMMANDBUTTON_303',
ui: 'normal',
style: ' color: #000000 ; z-index: 9664 ;border: 1px outset #000000; border-radius: 2px;;background : #c0c0c0 ;background-color: #c0c0c0 ;background-image: linear-gradient( to top left, rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2) 30%, rgba(0, 0, 0, 0) ); ;box-shadow: inset 1px 1px 2px rgba(255, 255, 255, 0.6), inset -1px -1px 2px rgba(0, 0, 0, 0.6); ;',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 599,
y: 6,
height: 20,
width: 50,
text: 'График',
id: 'COMMANDBUTTON_302',
ui: 'normal',
style: ' color: #000000 ; z-index: 9882 ;border: 1px outset #000000; border-radius: 2px;;background : #c0c0c0 ;background-color: #c0c0c0 ;background-image: linear-gradient( to top left, rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2) 30%, rgba(0, 0, 0, 0) ); ;box-shadow: inset 1px 1px 2px rgba(255, 255, 255, 0.6), inset -1px -1px 2px rgba(0, 0, 0, 0.6); ;',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1056,
y: 2,
height: 20,
width: 50,
text: 'График',
id: 'COMMANDBUTTON_305',
ui: 'normal',
style: ' color: #000000 ; z-index: 9884 ;border: 1px outset #000000; border-radius: 2px;;background : #c0c0c0 ;background-color: #c0c0c0 ;background-image: linear-gradient( to top left, rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2) 30%, rgba(0, 0, 0, 0) ); ;box-shadow: inset 1px 1px 2px rgba(255, 255, 255, 0.6), inset -1px -1px 2px rgba(0, 0, 0, 0.6); ;',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 144,
y: 6,
height: 20,
width: 50,
text: 'График',
id: 'COMMANDBUTTON_304',
ui: 'normal',
style: ' color: #000000 ; z-index: 9883 ;border: 1px outset #000000; border-radius: 2px;;background : #c0c0c0 ;background-color: #c0c0c0 ;background-image: linear-gradient( to top left, rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2) 30%, rgba(0, 0, 0, 0) ); ;box-shadow: inset 1px 1px 2px rgba(255, 255, 255, 0.6), inset -1px -1px 2px rgba(0, 0, 0, 0.6); ;',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 369,
y: 6,
height: 20,
width: 50,
text: 'График',
id: 'COMMANDBUTTON_301',
ui: 'normal',
style: ' color: #000000 ; z-index: 9881 ;border: 1px outset #000000; border-radius: 2px;;background : #c0c0c0 ;background-color: #c0c0c0 ;background-image: linear-gradient( to top left, rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2) 30%, rgba(0, 0, 0, 0) ); ;box-shadow: inset 1px 1px 2px rgba(255, 255, 255, 0.6), inset -1px -1px 2px rgba(0, 0, 0, 0.6); ;',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'button',
x: 1218,
y: 423,
height: 23,
width: 63,
text: 'на карте',
id: 'COMMANDBUTTON_300',
ui: 'normal',
style: ' color: #000000 ; z-index: 9822 ;border: 1px outset #000000; border-radius: 2px;;background : #c0c0c0 ;background-color: #c0c0c0 ;background-image: linear-gradient( to top left, rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2) 30%, rgba(0, 0, 0, 0) ); ;box-shadow: inset 1px 1px 2px rgba(255, 255, 255, 0.6), inset -1px -1px 2px rgba(0, 0, 0, 0.6); ;',
onClick: function(){ panelsHelper.openWindow(this.up('panel').id,this.id);}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_170',
x: 1224,
y: 73,
height: 24,
width: 50,
style:'z-index: 9993',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1192,
y: 78,
style: 'padding:1px 0 2px 4px;z-index: 9993 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_170_LABEL',
text: 'ПС 8'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_171',
x: 1224,
y: 29,
height: 24,
width: 50,
style:'z-index: 9994',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1192,
y: 34,
style: 'padding:1px 0 2px 4px;z-index: 9994 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_171_LABEL',
text: 'ПС 3'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_33',
x: 1224,
y: 51,
height: 24,
width: 50,
style:'z-index: 9823',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1192,
y: 56,
style: 'padding:1px 0 2px 4px;z-index: 9823 ;color:#000066;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_33_LABEL',
text: 'ПС 4'
},
{
xtype: 'label',
x: 940,
y: 280,
style: 'padding:1px 0 2px 4px;z-index: 9807 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_201',
text: 'З'
},
{
xtype: 'fieldset',
x: 1138,
y: 0,
height: 588,
width: 181,
style: 'margin: 0; padding:0;;z-index: 0;background-color:#505050',
id: 'DECORATION_7'
},
{
xtype: 'label',
x: 1094,
y: 282,
style: 'padding:1px 0 2px 4px;z-index: 9808 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_202',
text: 'В'
},
{
xtype: 'label',
x: 1141,
y: 0,
style: 'padding:1px 0 2px 4px;z-index: 9665 ;color:#000066;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ;text-decoration: underline; text-decoration-color: #000066 ;',
id: 'TEXTMSG_66',
text: ' Температура воздуха    окружающей среды, `C'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_99',
x: 1040,
y: 417,
height: 24,
width: 60,
style:'z-index: 9802',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 954,
y: 414,
style: 'padding:1px 0 2px 4px;z-index: 9802 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_99_LABEL',
text: 'Влажность'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_100',
x: 1006,
y: 156,
height: 24,
width: 60,
style:'z-index: 9800',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 922,
y: 154,
style: 'padding:1px 0 2px 4px;z-index: 9800 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_100_LABEL',
text: 'Атмосферное'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_119',
x: 146,
y: 651,
height: 24,
width: 54,
style:'z-index: 9879',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_101',
x: 1041,
y: 365,
height: 24,
width: 60,
style:'z-index: 9798',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 939,
y: 371,
style: 'padding:1px 0 2px 4px;z-index: 9798 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_101_LABEL',
text: 'Скорость ветра'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_120',
x: 146,
y: 628,
height: 24,
width: 54,
style:'z-index: 9877',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_121',
x: 146,
y: 605,
height: 24,
width: 54,
style:'z-index: 9875',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_102',
x: 1041,
y: 392,
height: 24,
width: 60,
style:'z-index: 9796',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 952,
y: 389,
style: 'padding:1px 0 2px 4px;z-index: 9796 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_102_LABEL',
text: 'Температура'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_122',
x: 42,
y: 651,
height: 24,
width: 60,
style:'z-index: 9873',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 9,
y: 656,
style: 'padding:1px 0 2px 4px;z-index: 9873 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_122_LABEL',
text: 'ф.С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_123',
x: 42,
y: 628,
height: 24,
width: 60,
style:'z-index: 9871',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 9,
y: 633,
style: 'padding:1px 0 2px 4px;z-index: 9871 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_123_LABEL',
text: 'ф.В'
},
{
xtype: 'label',
x: 201,
y: 656,
style: 'padding:1px 0 2px 4px;z-index: 9880 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_228',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_124',
x: 42,
y: 605,
height: 24,
width: 60,
style:'z-index: 9869',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 9,
y: 610,
style: 'padding:1px 0 2px 4px;z-index: 9869 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_124_LABEL',
text: 'ф.А'
},
{
xtype: 'label',
x: 201,
y: 634,
style: 'padding:1px 0 2px 4px;z-index: 9878 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_229',
text: '°С'
},
{
xtype: 'label',
x: 201,
y: 611,
style: 'padding:1px 0 2px 4px;z-index: 9876 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_230',
text: '°С'
},
{
xtype: 'label',
x: 103,
y: 657,
style: 'padding:1px 0 2px 4px;z-index: 9874 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_231',
text: 'кг'
},
{
xtype: 'label',
x: 102,
y: 634,
style: 'padding:1px 0 2px 4px;z-index: 9872 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_232',
text: 'кг'
},
{
xtype: 'label',
x: 101,
y: 611,
style: 'padding:1px 0 2px 4px;z-index: 9870 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_233',
text: 'кг'
},
{
xtype: 'panel-dial',
x: 42,
y: 224,
height: 132,
width: 132,
style:'z-index: 9860',
minValue: 0,
maxValue: 360,
circleBgColor: '#d4d0c8',
arrowColor: '#ff0000',
dashColor: 'black',
circleColor: 'black',
id: 'NUMERICGAUGE_10',
},
{
xtype: 'label',
x: 57,
y: 193,
style: 'padding:1px 0 2px 4px;z-index: 9860 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERICGAUGE_10_LABEL',
text: 'Направление ветра'
},
{
xtype: 'label',
x: 153,
y: 338,
style: 'padding:1px 0 2px 4px;z-index: 9868 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_234',
text: 'юв'
},
{
xtype: 'label',
x: 42,
y: 339,
style: 'padding:1px 0 2px 4px;z-index: 9867 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_235',
text: 'юз'
},
{
xtype: 'label',
x: 158,
y: 230,
style: 'padding:1px 0 2px 4px;z-index: 9866 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_236',
text: 'св'
},
{
xtype: 'label',
x: 39,
y: 227,
style: 'padding:1px 0 2px 4px;z-index: 9865 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_237',
text: 'сз'
},
{
xtype: 'label',
x: 102,
y: 206,
style: 'padding:1px 0 2px 4px;z-index: 9861 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_238',
text: 'C'
},
{
xtype: 'label',
x: 103,
y: 356,
style: 'padding:1px 0 2px 4px;z-index: 9862 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_239',
text: 'Ю'
},
{
xtype: 'label',
x: 22,
y: 282,
style: 'padding:1px 0 2px 4px;z-index: 9863 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_240',
text: 'З'
},
{
xtype: 'label',
x: 176,
y: 284,
style: 'padding:1px 0 2px 4px;z-index: 9864 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_241',
text: 'В'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_103',
x: 368,
y: 651,
height: 24,
width: 54,
style:'z-index: 9794',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 1103,
y: 424,
style: 'padding:1px 0 2px 4px;z-index: 9803 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_205',
text: '%'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_127',
x: 117,
y: 421,
height: 24,
width: 60,
style:'z-index: 9858',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 30,
y: 418,
style: 'padding:1px 0 2px 4px;z-index: 9858 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_127_LABEL',
text: 'Влажность '
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_104',
x: 368,
y: 628,
height: 24,
width: 54,
style:'z-index: 9792',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_128',
x: 100,
y: 160,
height: 24,
width: 60,
style:'z-index: 9856',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 13,
y: 158,
style: 'padding:1px 0 2px 4px;z-index: 9856 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_128_LABEL',
text: 'Атмосферное'
},
{
xtype: 'label',
x: 1068,
y: 161,
style: 'padding:1px 0 2px 4px;z-index: 9801 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_206',
text: 'мм.рт.ст'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_105',
x: 368,
y: 605,
height: 24,
width: 54,
style:'z-index: 9790',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_129',
x: 118,
y: 371,
height: 24,
width: 60,
style:'z-index: 9854',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 16,
y: 376,
style: 'padding:1px 0 2px 4px;z-index: 9854 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_129_LABEL',
text: 'Скорость ветра'
},
{
xtype: 'label',
x: 1103,
y: 371,
style: 'padding:1px 0 2px 4px;z-index: 9799 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_207',
text: 'м/с'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_106',
x: 264,
y: 654,
height: 24,
width: 60,
style:'z-index: 9788',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 231,
y: 659,
style: 'padding:1px 0 2px 4px;z-index: 9788 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_106_LABEL',
text: 'ф.С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_130',
x: 118,
y: 396,
height: 24,
width: 60,
style:'z-index: 9852',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 29,
y: 393,
style: 'padding:1px 0 2px 4px;z-index: 9852 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_130_LABEL',
text: 'Температура'
},
{
xtype: 'label',
x: 1103,
y: 396,
style: 'padding:1px 0 2px 4px;z-index: 9797 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_208',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_107',
x: 264,
y: 629,
height: 24,
width: 60,
style:'z-index: 9786',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 231,
y: 634,
style: 'padding:1px 0 2px 4px;z-index: 9786 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_107_LABEL',
text: 'ф.В'
},
{
xtype: 'label',
x: 425,
y: 657,
style: 'padding:1px 0 2px 4px;z-index: 9795 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_209',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_108',
x: 264,
y: 606,
height: 24,
width: 60,
style:'z-index: 9783',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 231,
y: 611,
style: 'padding:1px 0 2px 4px;z-index: 9783 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_108_LABEL',
text: 'ф.А'
},
{
xtype: 'label',
x: 425,
y: 634,
style: 'padding:1px 0 2px 4px;z-index: 9793 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_210',
text: '°С'
},
{
xtype: 'label',
x: 425,
y: 609,
style: 'padding:1px 0 2px 4px;z-index: 9791 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_211',
text: '°С'
},
{
xtype: 'label',
x: 325,
y: 659,
style: 'padding:1px 0 2px 4px;z-index: 9789 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_212',
text: 'кг'
},
{
xtype: 'label',
x: 325,
y: 634,
style: 'padding:1px 0 2px 4px;z-index: 9787 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_213',
text: 'кг'
},
{
xtype: 'label',
x: 325,
y: 611,
style: 'padding:1px 0 2px 4px;z-index: 9785 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_214',
text: 'кг'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_131',
x: 138,
y: 125,
height: 24,
width: 54,
style:'z-index: 9850',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 179,
y: 429,
style: 'padding:1px 0 2px 4px;z-index: 9859 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_244',
text: '%'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_132',
x: 138,
y: 102,
height: 24,
width: 54,
style:'z-index: 9848',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 162,
y: 165,
style: 'padding:1px 0 2px 4px;z-index: 9857 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_245',
text: 'мм.рт.ст'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_133',
x: 138,
y: 79,
height: 24,
width: 54,
style:'z-index: 9846',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 178,
y: 378,
style: 'padding:1px 0 2px 4px;z-index: 9855 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_246',
text: 'м/с'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_134',
x: 42,
y: 125,
height: 24,
width: 60,
style:'z-index: 9844',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 8,
y: 130,
style: 'padding:1px 0 2px 4px;z-index: 9844 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_134_LABEL',
text: 'ф.С'
},
{
xtype: 'label',
x: 180,
y: 405,
style: 'padding:1px 0 2px 4px;z-index: 9853 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_247',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_135',
x: 42,
y: 102,
height: 24,
width: 60,
style:'z-index: 9842',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 8,
y: 107,
style: 'padding:1px 0 2px 4px;z-index: 9842 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_135_LABEL',
text: 'ф.В'
},
{
xtype: 'label',
x: 193,
y: 130,
style: 'padding:1px 0 2px 4px;z-index: 9851 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_248',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_136',
x: 42,
y: 79,
height: 24,
width: 60,
style:'z-index: 9840',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 8,
y: 84,
style: 'padding:1px 0 2px 4px;z-index: 9840 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_136_LABEL',
text: 'ф.А'
},
{
xtype: 'label',
x: 193,
y: 109,
style: 'padding:1px 0 2px 4px;z-index: 9849 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_249',
text: '°С'
},
{
xtype: 'label',
x: 193,
y: 86,
style: 'padding:1px 0 2px 4px;z-index: 9847 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_250',
text: '°С'
},
{
xtype: 'label',
x: 105,
y: 130,
style: 'padding:1px 0 2px 4px;z-index: 9845 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_251',
text: 'кг'
},
{
xtype: 'label',
x: 104,
y: 107,
style: 'padding:1px 0 2px 4px;z-index: 9843 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_252',
text: 'кг'
},
{
xtype: 'label',
x: 103,
y: 86,
style: 'padding:1px 0 2px 4px;z-index: 9841 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_253',
text: 'кг'
},
{
xtype: 'panel-dial',
x: 273,
y: 224,
height: 132,
width: 132,
style:'z-index: 9774',
minValue: 0,
maxValue: 360,
circleBgColor: '#d4d0c8',
arrowColor: '#ff0000',
dashColor: 'black',
circleColor: 'black',
id: 'NUMERICGAUGE_8',
},
{
xtype: 'label',
x: 284,
y: 192,
style: 'padding:1px 0 2px 4px;z-index: 9774 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERICGAUGE_8_LABEL',
text: 'Направление ветра'
},
{
xtype: 'label',
x: 384,
y: 338,
style: 'padding:1px 0 2px 4px;z-index: 9782 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_174',
text: 'юв'
},
{
xtype: 'label',
x: 273,
y: 339,
style: 'padding:1px 0 2px 4px;z-index: 9781 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_175',
text: 'юз'
},
{
xtype: 'label',
x: 389,
y: 230,
style: 'padding:1px 0 2px 4px;z-index: 9780 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_176',
text: 'св'
},
{
xtype: 'label',
x: 270,
y: 227,
style: 'padding:1px 0 2px 4px;z-index: 9779 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_177',
text: 'сз'
},
{
xtype: 'label',
x: 333,
y: 206,
style: 'padding:1px 0 2px 4px;z-index: 9775 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_178',
text: 'C'
},
{
xtype: 'label',
x: 334,
y: 356,
style: 'padding:1px 0 2px 4px;z-index: 9776 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_179',
text: 'Ю'
},
{
xtype: 'label',
x: 253,
y: 282,
style: 'padding:1px 0 2px 4px;z-index: 9777 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_180',
text: 'З'
},
{
xtype: 'label',
x: 407,
y: 284,
style: 'padding:1px 0 2px 4px;z-index: 9778 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_181',
text: 'В'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_87',
x: 347,
y: 421,
height: 24,
width: 60,
style:'z-index: 9772',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 261,
y: 418,
style: 'padding:1px 0 2px 4px;z-index: 9772 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_87_LABEL',
text: 'Влажность'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_88',
x: 324,
y: 160,
height: 24,
width: 60,
style:'z-index: 9770',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 237,
y: 158,
style: 'padding:1px 0 2px 4px;z-index: 9770 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_88_LABEL',
text: 'Атмосферное'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_111',
x: 1046,
y: 121,
height: 24,
width: 54,
style:'z-index: 9836',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_89',
x: 347,
y: 371,
height: 24,
width: 60,
style:'z-index: 9768',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 245,
y: 376,
style: 'padding:1px 0 2px 4px;z-index: 9768 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_89_LABEL',
text: 'Скорость ветра'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_112',
x: 1046,
y: 98,
height: 24,
width: 54,
style:'z-index: 9834',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_113',
x: 1046,
y: 75,
height: 24,
width: 54,
style:'z-index: 9832',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_90',
x: 347,
y: 396,
height: 24,
width: 60,
style:'z-index: 9766',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 260,
y: 393,
style: 'padding:1px 0 2px 4px;z-index: 9766 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_90_LABEL',
text: 'Температура'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_114',
x: 946,
y: 121,
height: 24,
width: 60,
style:'z-index: 9830',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 914,
y: 126,
style: 'padding:1px 0 2px 4px;z-index: 9830 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_114_LABEL',
text: 'ф.С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_115',
x: 946,
y: 98,
height: 24,
width: 60,
style:'z-index: 9828',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 914,
y: 103,
style: 'padding:1px 0 2px 4px;z-index: 9828 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_115_LABEL',
text: 'ф.В'
},
{
xtype: 'label',
x: 1101,
y: 126,
style: 'padding:1px 0 2px 4px;z-index: 9837 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_218',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_116',
x: 946,
y: 75,
height: 24,
width: 60,
style:'z-index: 9826',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 914,
y: 80,
style: 'padding:1px 0 2px 4px;z-index: 9826 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_116_LABEL',
text: 'ф.А'
},
{
xtype: 'label',
x: 1101,
y: 103,
style: 'padding:1px 0 2px 4px;z-index: 9835 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_219',
text: '°С'
},
{
xtype: 'label',
x: 1101,
y: 80,
style: 'padding:1px 0 2px 4px;z-index: 9833 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_220',
text: '°С'
},
{
xtype: 'label',
x: 1008,
y: 126,
style: 'padding:1px 0 2px 4px;z-index: 9831 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_221',
text: 'кг'
},
{
xtype: 'label',
x: 1009,
y: 104,
style: 'padding:1px 0 2px 4px;z-index: 9829 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_222',
text: 'кг'
},
{
xtype: 'label',
x: 1008,
y: 81,
style: 'padding:1px 0 2px 4px;z-index: 9827 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_223',
text: 'кг'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_91',
x: 367,
y: 125,
height: 24,
width: 54,
style:'z-index: 9764',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 409,
y: 427,
style: 'padding:1px 0 2px 4px;z-index: 9773 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_184',
text: '%'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_92',
x: 367,
y: 102,
height: 24,
width: 54,
style:'z-index: 9762',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 388,
y: 165,
style: 'padding:1px 0 2px 4px;z-index: 9771 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_185',
text: 'мм.рт.ст'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_93',
x: 367,
y: 79,
height: 24,
width: 54,
style:'z-index: 9760',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 409,
y: 377,
style: 'padding:1px 0 2px 4px;z-index: 9769 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_186',
text: 'м/с'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_94',
x: 264,
y: 125,
height: 24,
width: 60,
style:'z-index: 9758',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 232,
y: 130,
style: 'padding:1px 0 2px 4px;z-index: 9758 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_94_LABEL',
text: 'ф.С'
},
{
xtype: 'label',
x: 409,
y: 403,
style: 'padding:1px 0 2px 4px;z-index: 9767 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_187',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_95',
x: 264,
y: 102,
height: 24,
width: 60,
style:'z-index: 9756',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 232,
y: 107,
style: 'padding:1px 0 2px 4px;z-index: 9756 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_95_LABEL',
text: 'ф.В'
},
{
xtype: 'label',
x: 422,
y: 130,
style: 'padding:1px 0 2px 4px;z-index: 9765 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_188',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_96',
x: 264,
y: 79,
height: 24,
width: 60,
style:'z-index: 9754',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 232,
y: 84,
style: 'padding:1px 0 2px 4px;z-index: 9754 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_96_LABEL',
text: 'ф.А'
},
{
xtype: 'label',
x: 422,
y: 107,
style: 'padding:1px 0 2px 4px;z-index: 9763 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_189',
text: '°С'
},
{
xtype: 'label',
x: 422,
y: 84,
style: 'padding:1px 0 2px 4px;z-index: 9761 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_190',
text: '°С'
},
{
xtype: 'label',
x: 322,
y: 131,
style: 'padding:1px 0 2px 4px;z-index: 9759 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_191',
text: 'кг'
},
{
xtype: 'label',
x: 322,
y: 108,
style: 'padding:1px 0 2px 4px;z-index: 9757 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_192',
text: 'кг'
},
{
xtype: 'label',
x: 322,
y: 84,
style: 'padding:1px 0 2px 4px;z-index: 9755 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_193',
text: 'кг'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_79',
x: 819,
y: 651,
height: 24,
width: 54,
style:'z-index: 9752',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_80',
x: 819,
y: 628,
height: 24,
width: 54,
style:'z-index: 9750',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_81',
x: 819,
y: 606,
height: 24,
width: 54,
style:'z-index: 9748',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_82',
x: 724,
y: 651,
height: 24,
width: 60,
style:'z-index: 9746',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 689,
y: 656,
style: 'padding:1px 0 2px 4px;z-index: 9746 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_82_LABEL',
text: 'ф.С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_83',
x: 724,
y: 628,
height: 24,
width: 60,
style:'z-index: 9744',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 689,
y: 633,
style: 'padding:1px 0 2px 4px;z-index: 9744 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_83_LABEL',
text: 'ф.В'
},
{
xtype: 'label',
x: 875,
y: 657,
style: 'padding:1px 0 2px 4px;z-index: 9753 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_167',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_84',
x: 724,
y: 605,
height: 24,
width: 60,
style:'z-index: 9741',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 689,
y: 610,
style: 'padding:1px 0 2px 4px;z-index: 9741 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_84_LABEL',
text: 'ф.А'
},
{
xtype: 'label',
x: 874,
y: 634,
style: 'padding:1px 0 2px 4px;z-index: 9751 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_168',
text: '°С'
},
{
xtype: 'label',
x: 874,
y: 611,
style: 'padding:1px 0 2px 4px;z-index: 9749 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_169',
text: '°С'
},
{
xtype: 'label',
x: 786,
y: 655,
style: 'padding:1px 0 2px 4px;z-index: 9747 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_170',
text: 'кг'
},
{
xtype: 'label',
x: 786,
y: 633,
style: 'padding:1px 0 2px 4px;z-index: 9745 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_171',
text: 'кг'
},
{
xtype: 'label',
x: 786,
y: 610,
style: 'padding:1px 0 2px 4px;z-index: 9743 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_172',
text: 'кг'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_67',
x: 588,
y: 651,
height: 24,
width: 54,
style:'z-index: 9739',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_68',
x: 588,
y: 628,
height: 24,
width: 54,
style:'z-index: 9737',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_69',
x: 588,
y: 605,
height: 24,
width: 54,
style:'z-index: 9735',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_70',
x: 494,
y: 651,
height: 24,
width: 60,
style:'z-index: 9733',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 459,
y: 656,
style: 'padding:1px 0 2px 4px;z-index: 9733 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_70_LABEL',
text: 'ф.С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_71',
x: 494,
y: 628,
height: 24,
width: 60,
style:'z-index: 9731',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 459,
y: 633,
style: 'padding:1px 0 2px 4px;z-index: 9731 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_71_LABEL',
text: 'ф.В'
},
{
xtype: 'label',
x: 643,
y: 657,
style: 'padding:1px 0 2px 4px;z-index: 9740 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_146',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_72',
x: 494,
y: 605,
height: 24,
width: 60,
style:'z-index: 9728',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 459,
y: 610,
style: 'padding:1px 0 2px 4px;z-index: 9728 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_72_LABEL',
text: 'ф.А'
},
{
xtype: 'label',
x: 643,
y: 634,
style: 'padding:1px 0 2px 4px;z-index: 9738 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_147',
text: '°С'
},
{
xtype: 'label',
x: 643,
y: 610,
style: 'padding:1px 0 2px 4px;z-index: 9736 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_148',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_167',
x: 1041,
y: 482,
height: 24,
width: 60,
style:'z-index: 9990',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 954,
y: 477,
style: 'padding:1px 0 2px 4px;z-index: 9990 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_167_LABEL',
text: 'Напряжение солнечной батареи'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_168',
x: 1041,
y: 451,
height: 24,
width: 60,
style:'z-index: 9988',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 952,
y: 447,
style: 'padding:1px 0 2px 4px;z-index: 9988 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_168_LABEL',
text: 'Напряжение аккумулятора'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_163',
x: 791,
y: 485,
height: 24,
width: 60,
style:'z-index: 9986',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 704,
y: 480,
style: 'padding:1px 0 2px 4px;z-index: 9986 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_163_LABEL',
text: 'Напряжение солнечной батареи'
},
{
xtype: 'label',
x: 1104,
y: 487,
style: 'padding:1px 0 2px 4px;z-index: 9991 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_381',
text: 'V'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_164',
x: 791,
y: 454,
height: 24,
width: 60,
style:'z-index: 9984',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 702,
y: 450,
style: 'padding:1px 0 2px 4px;z-index: 9984 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_164_LABEL',
text: 'Напряжение аккумулятора'
},
{
xtype: 'label',
x: 1103,
y: 456,
style: 'padding:1px 0 2px 4px;z-index: 9989 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_382',
text: 'V'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_161',
x: 566,
y: 483,
height: 24,
width: 60,
style:'z-index: 9982',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 479,
y: 478,
style: 'padding:1px 0 2px 4px;z-index: 9982 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_161_LABEL',
text: 'Напряжение солнечной батареи'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_162',
x: 566,
y: 452,
height: 24,
width: 60,
style:'z-index: 9980',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 477,
y: 448,
style: 'padding:1px 0 2px 4px;z-index: 9980 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_162_LABEL',
text: 'Напряжение аккумулятора'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_159',
x: 346,
y: 483,
height: 24,
width: 60,
style:'z-index: 9978',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 259,
y: 478,
style: 'padding:1px 0 2px 4px;z-index: 9978 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_159_LABEL',
text: 'Напряжение солнечной батареи'
},
{
xtype: 'label',
x: 854,
y: 490,
style: 'padding:1px 0 2px 4px;z-index: 9987 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_377',
text: 'V'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_160',
x: 346,
y: 452,
height: 24,
width: 60,
style:'z-index: 9976',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 257,
y: 448,
style: 'padding:1px 0 2px 4px;z-index: 9976 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_160_LABEL',
text: 'Напряжение аккумулятора'
},
{
xtype: 'label',
x: 853,
y: 459,
style: 'padding:1px 0 2px 4px;z-index: 9985 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_378',
text: 'V'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_49',
x: 117,
y: 482,
height: 24,
width: 60,
style:'z-index: 9974',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 30,
y: 477,
style: 'padding:1px 0 2px 4px;z-index: 9974 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_49_LABEL',
text: 'Напряжение солнечной батареи'
},
{
xtype: 'label',
x: 629,
y: 488,
style: 'padding:1px 0 2px 4px;z-index: 9983 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_375',
text: 'V'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_50',
x: 117,
y: 451,
height: 24,
width: 60,
style:'z-index: 9972',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 28,
y: 447,
style: 'padding:1px 0 2px 4px;z-index: 9972 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_50_LABEL',
text: 'Напряжение аккумулятора'
},
{
xtype: 'label',
x: 628,
y: 457,
style: 'padding:1px 0 2px 4px;z-index: 9981 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_376',
text: 'V'
},
{
xtype: 'label',
x: 555,
y: 657,
style: 'padding:1px 0 2px 4px;z-index: 9734 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_149',
text: 'кг'
},
{
xtype: 'label',
x: 409,
y: 488,
style: 'padding:1px 0 2px 4px;z-index: 9979 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_373',
text: 'V'
},
{
xtype: 'label',
x: 555,
y: 633,
style: 'padding:1px 0 2px 4px;z-index: 9732 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_150',
text: 'кг'
},
{
xtype: 'label',
x: 408,
y: 457,
style: 'padding:1px 0 2px 4px;z-index: 9977 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_374',
text: 'V'
},
{
xtype: 'label',
x: 180,
y: 487,
style: 'padding:1px 0 2px 4px;z-index: 9975 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_119',
text: 'V'
},
{
xtype: 'label',
x: 179,
y: 456,
style: 'padding:1px 0 2px 4px;z-index: 9973 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_120',
text: 'V'
},
{
xtype: 'label',
x: 555,
y: 610,
style: 'padding:1px 0 2px 4px;z-index: 9730 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_151',
text: 'кг'
},
{
xtype: 'panel-dial',
x: 716,
y: 223,
height: 132,
width: 132,
style:'z-index: 9719',
minValue: 0,
maxValue: 360,
circleBgColor: '#d4d0c8',
arrowColor: '#ff0000',
dashColor: 'black',
circleColor: 'black',
id: 'NUMERICGAUGE_5',
},
{
xtype: 'label',
x: 730,
y: 193,
style: 'padding:1px 0 2px 4px;z-index: 9719 ;color:#000000;background-color:transparent;font-size: 8pt ;font-weight:bold ',
id: 'NUMERICGAUGE_5_LABEL',
text: 'Направление ветра'
},
{
xtype: 'label',
x: 830,
y: 339,
style: 'padding:1px 0 2px 4px;z-index: 9727 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_111',
text: 'юв'
},
{
xtype: 'label',
x: 716,
y: 340,
style: 'padding:1px 0 2px 4px;z-index: 9726 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_112',
text: 'юз'
},
{
xtype: 'label',
x: 832,
y: 220,
style: 'padding:1px 0 2px 4px;z-index: 9725 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_113',
text: 'св'
},
{
xtype: 'label',
x: 716,
y: 221,
style: 'padding:1px 0 2px 4px;z-index: 9724 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_114',
text: 'сз'
},
{
xtype: 'label',
x: 776,
y: 205,
style: 'padding:1px 0 2px 4px;z-index: 9720 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_115',
text: 'C'
},
{
xtype: 'label',
x: 777,
y: 356,
style: 'padding:1px 0 2px 4px;z-index: 9721 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_116',
text: 'Ю'
},
{
xtype: 'label',
x: 701,
y: 282,
style: 'padding:1px 0 2px 4px;z-index: 9722 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_117',
text: 'З'
},
{
xtype: 'label',
x: 850,
y: 284,
style: 'padding:1px 0 2px 4px;z-index: 9723 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_118',
text: 'В'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_51',
x: 792,
y: 421,
height: 24,
width: 60,
style:'z-index: 9717',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'center'}
},
{
xtype: 'label',
x: 709,
y: 418,
style: 'padding:1px 0 2px 4px;z-index: 9717 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_51_LABEL',
text: 'Влажность'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_52',
x: 778,
y: 160,
height: 24,
width: 60,
style:'z-index: 9716',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'center'}
},
{
xtype: 'label',
x: 693,
y: 158,
height:15,
width:84,
style: 'padding:1px 0 2px 4px;z-index: 9716 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_52_LABEL',
text: 'Атмосферное'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_53',
x: 792,
y: 369,
height: 24,
width: 60,
style:'z-index: 9714',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'center'}
},
{
xtype: 'label',
x: 692,
y: 375,
style: 'padding:1px 0 2px 4px;z-index: 9714 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_53_LABEL',
text: 'Скорость ветра'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_54',
x: 792,
y: 396,
height: 24,
width: 60,
style:'z-index: 9712',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'center'}
},
{
xtype: 'label',
x: 707,
y: 393,
style: 'padding:1px 0 2px 4px;z-index: 9712 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_54_LABEL',
text: 'Температура'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_55',
x: 819,
y: 125,
height: 24,
width: 54,
style:'z-index: 9710',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 854,
y: 424,
style: 'padding:1px 0 2px 4px;z-index: 9718 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_121',
text: '%'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_56',
x: 819,
y: 102,
height: 24,
width: 54,
style:'z-index: 9708',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 839,
y: 169,
style: 'padding:1px 0 2px 4px;z-index: 9907 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_122',
text: 'мм.рт.ст'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_57',
x: 819,
y: 79,
height: 24,
width: 54,
style:'z-index: 9706',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 856,
y: 374,
style: 'padding:1px 0 2px 4px;z-index: 9715 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_123',
text: 'м/с'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_58',
x: 724,
y: 125,
height: 24,
width: 60,
style:'z-index: 9704',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 689,
y: 130,
style: 'padding:1px 0 2px 4px;z-index: 9704 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_58_LABEL',
text: 'ф.С'
},
{
xtype: 'label',
x: 852,
y: 402,
style: 'padding:1px 0 2px 4px;z-index: 9713 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_124',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_59',
x: 724,
y: 102,
height: 24,
width: 60,
style:'z-index: 9702',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 689,
y: 107,
style: 'padding:1px 0 2px 4px;z-index: 9702 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_59_LABEL',
text: 'ф.B'
},
{
xtype: 'label',
x: 874,
y: 130,
style: 'padding:1px 0 2px 4px;z-index: 9711 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_125',
text: '°С'
},
{
xtype: 'label',
x: 28,
y: 553,
style: 'padding:1px 0 2px 4px;z-index: 9971 ;color:#000000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_372',
text: 'опора № 34'
},
{
xtype: 'label',
x: 253,
y: 553,
style: 'padding:1px 0 2px 4px;z-index: 9970 ;color:#000000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_371',
text: 'опора № 38'
},
{
xtype: 'label',
x: 475,
y: 553,
style: 'padding:1px 0 2px 4px;z-index: 9969 ;color:#000000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_370',
text: 'опора № 356'
},
{
xtype: 'label',
x: 712,
y: 553,
style: 'padding:1px 0 2px 4px;z-index: 9968 ;color:#000000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_369',
text: 'опора № 83'
},
{
xtype: 'label',
x: 928,
y: 24,
style: 'padding:1px 0 2px 4px;z-index: 9967 ;color:#000000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_368',
text: 'опора № 270'
},
{
xtype: 'label',
x: 704,
y: 28,
style: 'padding:1px 0 2px 4px;z-index: 9966 ;color:#000000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_366',
text: 'опора № 83'
},
{
xtype: 'label',
x: 475,
y: 28,
style: 'padding:1px 0 2px 4px;z-index: 9965 ;color:#000000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_365',
text: 'опора № 356'
},
{
xtype: 'label',
x: 250,
y: 28,
style: 'padding:1px 0 2px 4px;z-index: 9964 ;color:#000000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_364',
text: 'опора № 81'
},
{
xtype: 'label',
x: 32,
y: 27,
style: 'padding:1px 0 2px 4px;z-index: 9963 ;color:#000000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_363',
text: 'опора № 36'
},
{
xtype: 'label',
x: 46,
y: 6,
style: 'padding:1px 0 2px 4px;z-index: 9839 ;color:#000000;background-color:#d4d0c8;font-size: 16pt ;font-weight:bold ;text-decoration: underline; text-decoration-color: #000000 ;',
id: 'TEXTMSG_254',
text: 'Л-179'
},
{
xtype: 'label',
x: 46,
y: 532,
style: 'padding:1px 0 2px 4px;z-index: 9666 ;color:#000000;background-color:#d4d0c8;font-size: 16pt ;font-weight:bold ;text-decoration: underline; text-decoration-color: #000000 ;',
id: 'TEXTMSG_255',
text: 'Л-226'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_60',
x: 724,
y: 79,
height: 24,
width: 60,
style:'z-index: 9699',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 689,
y: 84,
style: 'padding:1px 0 2px 4px;z-index: 9699 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_60_LABEL',
text: 'ф.А'
},
{
xtype: 'label',
x: 874,
y: 105,
style: 'padding:1px 0 2px 4px;z-index: 9709 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_126',
text: '°С'
},
{
xtype: 'label',
x: 874,
y: 80,
style: 'padding:1px 0 2px 4px;z-index: 9707 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_127',
text: '°С'
},
{
xtype: 'label',
x: 785,
y: 126,
style: 'padding:1px 0 2px 4px;z-index: 9705 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_128',
text: 'кг'
},
{
xtype: 'label',
x: 785,
y: 104,
style: 'padding:1px 0 2px 4px;z-index: 9703 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_129',
text: 'кг'
},
{
xtype: 'label',
x: 785,
y: 81,
style: 'padding:1px 0 2px 4px;z-index: 9701 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_130',
text: 'кг'
},
{
xtype: 'panel-dial',
x: 495,
y: 226,
height: 132,
width: 132,
style:'z-index: 9690',
minValue: 0,
maxValue: 360,
circleBgColor: '#d4d0c8',
arrowColor: '#ff0000',
dashColor: 'black',
circleColor: 'black',
id: 'NUMERICGAUGE_4',
},
{
xtype: 'label',
x: 496,
y: 193,
style: 'padding:1px 0 2px 4px;z-index: 9690 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERICGAUGE_4_LABEL',
text: 'Направление ветра'
},
{
xtype: 'label',
x: 606,
y: 340,
style: 'padding:1px 0 2px 4px;z-index: 9698 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_102',
text: 'юв'
},
{
xtype: 'label',
x: 495,
y: 341,
style: 'padding:1px 0 2px 4px;z-index: 9697 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_103',
text: 'юз'
},
{
xtype: 'label',
x: 611,
y: 232,
style: 'padding:1px 0 2px 4px;z-index: 9696 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_104',
text: 'св'
},
{
xtype: 'label',
x: 492,
y: 229,
style: 'padding:1px 0 2px 4px;z-index: 9695 ;color:#0000cc;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_106',
text: 'сз'
},
{
xtype: 'label',
x: 555,
y: 208,
style: 'padding:1px 0 2px 4px;z-index: 9691 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_107',
text: 'C'
},
{
xtype: 'label',
x: 556,
y: 358,
style: 'padding:1px 0 2px 4px;z-index: 9692 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_108',
text: 'Ю'
},
{
xtype: 'label',
x: 479,
y: 284,
style: 'padding:1px 0 2px 4px;z-index: 9693 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_109',
text: 'З'
},
{
xtype: 'label',
x: 629,
y: 286,
style: 'padding:1px 0 2px 4px;z-index: 9694 ;color:#ff0000;background-color:#d4d0c8;font-size: 10pt ;font-weight:bold ',
id: 'TEXTMSG_110',
text: 'В'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_46',
x: 567,
y: 421,
height: 24,
width: 60,
style:'z-index: 9688',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'center'}
},
{
xtype: 'label',
x: 480,
y: 418,
style: 'padding:1px 0 2px 4px;z-index: 9688 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_46_LABEL',
text: 'Влажность '
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_45',
x: 551,
y: 160,
height: 24,
width: 60,
style:'z-index: 9686',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'center'}
},
{
xtype: 'label',
x: 462,
y: 158,
height:15,
width:84,
style: 'padding:1px 0 2px 4px;z-index: 9686 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_45_LABEL',
text: 'Атмосферное'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_44',
x: 567,
y: 371,
height: 24,
width: 60,
style:'z-index: 9684',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'center'}
},
{
xtype: 'label',
x: 463,
y: 376,
style: 'padding:1px 0 2px 4px;z-index: 9684 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_44_LABEL',
text: 'Скорость ветра'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_43',
x: 567,
y: 396,
height: 24,
width: 60,
style:'z-index: 9682',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'center'}
},
{
xtype: 'label',
x: 478,
y: 393,
style: 'padding:1px 0 2px 4px;z-index: 9682 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_43_LABEL',
text: 'Температура'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_40',
x: 588,
y: 125,
height: 24,
width: 54,
style:'z-index: 9680',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 629,
y: 428,
style: 'padding:1px 0 2px 4px;z-index: 9689 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_99',
text: '%'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_41',
x: 588,
y: 102,
height: 24,
width: 54,
style:'z-index: 9678',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 613,
y: 167,
style: 'padding:1px 0 2px 4px;z-index: 9687 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_98',
text: 'мм.рт.ст'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_42',
x: 588,
y: 79,
height: 24,
width: 54,
style:'z-index: 9676',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 629,
y: 376,
style: 'padding:1px 0 2px 4px;z-index: 9685 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_97',
text: 'м/с'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_39',
x: 493,
y: 125,
height: 24,
width: 60,
style:'z-index: 9674',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 459,
y: 130,
style: 'padding:1px 0 2px 4px;z-index: 9674 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_39_LABEL',
text: 'ф.С'
},
{
xtype: 'label',
x: 629,
y: 402,
style: 'padding:1px 0 2px 4px;z-index: 9683 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_96',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_38',
x: 493,
y: 102,
height: 24,
width: 60,
style:'z-index: 9672',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 459,
y: 107,
style: 'padding:1px 0 2px 4px;z-index: 9672 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_38_LABEL',
text: 'ф.В'
},
{
xtype: 'label',
x: 643,
y: 127,
style: 'padding:1px 0 2px 4px;z-index: 9681 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_93',
text: '°С'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_145',
x: 143,
y: 551,
height: 24,
width: 54,
style:'z-index: 9905',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_144',
x: 368,
y: 550,
height: 24,
width: 54,
style:'z-index: 9903',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_143',
x: 822,
y: 549,
height: 24,
width: 54,
style:'z-index: 9901',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_142',
x: 597,
y: 549,
height: 24,
width: 54,
style:'z-index: 9899',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_141',
x: 822,
y: 25,
height: 24,
width: 54,
style:'z-index: 9897',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_140',
x: 367,
y: 25,
height: 24,
width: 54,
style:'z-index: 9895',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_139',
x: 141,
y: 25,
height: 24,
width: 54,
style:'z-index: 9893',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 197,
y: 557,
style: 'padding:1px 0 2px 4px;z-index: 9906 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_264',
text: 'МВт'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_138',
x: 1053,
y: 21,
height: 24,
width: 54,
style:'z-index: 9891',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 422,
y: 557,
style: 'padding:1px 0 2px 4px;z-index: 9904 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_263',
text: 'МВт'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_137',
x: 596,
y: 25,
height: 24,
width: 54,
style:'z-index: 9889',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 877,
y: 556,
style: 'padding:1px 0 2px 4px;z-index: 9902 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_262',
text: 'МВт'
},
{
xtype: 'textfield',
id: 'NUMERIC_U154_37',
x: 494,
y: 79,
height: 24,
width: 60,
style:'z-index: 9669',
decimalPrecision: 2,
hideLabel: true,
disabled: true,
emptyText: '0',
fieldStyle: {'fontSize': '13px','fontWeight': 'bold','color': '#000000' ,'background':'#ffffff' ,'text-align':'right'}
},
{
xtype: 'label',
x: 459,
y: 84,
style: 'padding:1px 0 2px 4px;z-index: 9669 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'NUMERIC_U154_37_LABEL',
text: 'ф.А'
},
{
xtype: 'label',
x: 651,
y: 556,
style: 'padding:1px 0 2px 4px;z-index: 9900 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_261',
text: 'МВт'
},
{
xtype: 'label',
x: 947,
y: 2,
style: 'padding:1px 0 2px 4px;z-index: 9667 ;color:#000000;background-color:#d4d0c8;font-size: 16pt ;font-weight:bold ;text-decoration: underline; text-decoration-color: #000000 ;',
id: 'TEXTMSG_224',
text: 'Л-141'
},
{
xtype: 'label',
x: 876,
y: 31,
style: 'padding:1px 0 2px 4px;z-index: 9898 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_260',
text: 'МВт'
},
{
xtype: 'label',
x: 643,
y: 105,
style: 'padding:1px 0 2px 4px;z-index: 9679 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_94',
text: '°С'
},
{
xtype: 'label',
x: 421,
y: 33,
style: 'padding:1px 0 2px 4px;z-index: 9896 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_259',
text: 'МВт'
},
{
xtype: 'label',
x: 643,
y: 84,
style: 'padding:1px 0 2px 4px;z-index: 9677 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_95',
text: '°С'
},
{
xtype: 'label',
x: 196,
y: 33,
style: 'padding:1px 0 2px 4px;z-index: 9894 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_258',
text: 'МВт'
},
{
xtype: 'label',
x: 554,
y: 131,
style: 'padding:1px 0 2px 4px;z-index: 9675 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_92',
text: 'кг'
},
{
xtype: 'label',
x: 1107,
y: 26,
style: 'padding:1px 0 2px 4px;z-index: 9892 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_257',
text: 'МВт'
},
{
xtype: 'label',
x: 554,
y: 107,
style: 'padding:1px 0 2px 4px;z-index: 9673 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_91',
text: 'кг'
},
{
xtype: 'label',
x: 650,
y: 32,
style: 'padding:1px 0 2px 4px;z-index: 9890 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_256',
text: 'МВт'
},
{
xtype: 'label',
x: 554,
y: 84,
style: 'padding:1px 0 2px 4px;z-index: 9671 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_90',
text: 'кг'
},
{
xtype: 'label',
x: 1035,
y: 47,
style: 'padding:1px 0 2px 4px;z-index: 9931 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_330',
text: 'Температура '
},
{
xtype: 'label',
x: 815,
y: 51,
style: 'padding:1px 0 2px 4px;z-index: 9913 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_303',
text: 'Температура '
},
{
xtype: 'label',
x: 352,
y: 51,
style: 'padding:1px 0 2px 4px;z-index: 9952 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_351',
text: 'Температура '
},
{
xtype: 'label',
x: 133,
y: 51,
style: 'padding:1px 0 2px 4px;z-index: 9940 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_339',
text: 'Температура '
},
{
xtype: 'label',
x: 586,
y: 51,
style: 'padding:1px 0 2px 4px;z-index: 9909 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_296',
text: 'Температура '
},
{
xtype: 'label',
x: 1035,
y: 58,
style: 'padding:1px 0 2px 4px;z-index: 9934 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_331',
text: 'провода'
},
{
xtype: 'label',
x: 133,
y: 579,
style: 'padding:1px 0 2px 4px;z-index: 9947 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_347',
text: 'Температура '
},
{
xtype: 'label',
x: 354,
y: 579,
style: 'padding:1px 0 2px 4px;z-index: 9959 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_359',
text: 'Температура '
},
{
xtype: 'label',
x: 584,
y: 579,
style: 'padding:1px 0 2px 4px;z-index: 9921 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_311',
text: 'Температура '
},
{
xtype: 'label',
x: 815,
y: 579,
style: 'padding:1px 0 2px 4px;z-index: 9917 ;color:#000000;background-color:#d4d0c8;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_307',
text: 'Температура '
},
{
xtype: 'label',
x: 946,
y: 58,
style: 'padding:1px 0 2px 4px;z-index: 9933 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_332',
text: 'отложений'
},
{
xtype: 'label',
x: 133,
y: 590,
style: 'padding:1px 0 2px 4px;z-index: 9950 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_348',
text: 'провода'
},
{
xtype: 'label',
x: 356,
y: 590,
style: 'padding:1px 0 2px 4px;z-index: 9962 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_360',
text: 'провода'
},
{
xtype: 'label',
x: 815,
y: 62,
style: 'padding:1px 0 2px 4px;z-index: 9916 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_304',
text: 'провода'
},
{
xtype: 'label',
x: 586,
y: 590,
style: 'padding:1px 0 2px 4px;z-index: 9924 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_312',
text: 'провода'
},
{
xtype: 'label',
x: 44,
y: 590,
style: 'padding:1px 0 2px 4px;z-index: 9949 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_349',
text: 'отложений'
},
{
xtype: 'label',
x: 44,
y: 578,
style: 'padding:1px 0 2px 4px;z-index: 9948 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_350',
text: 'Масса'
},
{
xtype: 'label',
x: 263,
y: 590,
style: 'padding:1px 0 2px 4px;z-index: 9961 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_361',
text: 'отложений'
},
{
xtype: 'label',
x: 263,
y: 578,
style: 'padding:1px 0 2px 4px;z-index: 9960 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_362',
text: 'Масса'
},
{
xtype: 'label',
x: 946,
y: 46,
style: 'padding:1px 0 2px 4px;z-index: 9932 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_333',
text: 'Масса'
},
{
xtype: 'label',
x: 495,
y: 590,
style: 'padding:1px 0 2px 4px;z-index: 9923 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_313',
text: 'отложений'
},
{
xtype: 'label',
x: 495,
y: 578,
style: 'padding:1px 0 2px 4px;z-index: 9922 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_314',
text: 'Масса'
},
{
xtype: 'label',
x: 726,
y: 62,
style: 'padding:1px 0 2px 4px;z-index: 9915 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_305',
text: 'отложений'
},
{
xtype: 'label',
x: 815,
y: 590,
style: 'padding:1px 0 2px 4px;z-index: 9920 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_308',
text: 'провода'
},
{
xtype: 'label',
x: 726,
y: 590,
style: 'padding:1px 0 2px 4px;z-index: 9919 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_309',
text: 'отложений'
},
{
xtype: 'label',
x: 352,
y: 62,
style: 'padding:1px 0 2px 4px;z-index: 9955 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_352',
text: 'провода'
},
{
xtype: 'label',
x: 726,
y: 578,
style: 'padding:1px 0 2px 4px;z-index: 9918 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_310',
text: 'Масса'
},
{
xtype: 'label',
x: 133,
y: 62,
style: 'padding:1px 0 2px 4px;z-index: 9943 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_340',
text: 'провода'
},
{
xtype: 'label',
x: 726,
y: 50,
style: 'padding:1px 0 2px 4px;z-index: 9914 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_306',
text: 'Масса'
},
{
xtype: 'label',
x: 586,
y: 62,
style: 'padding:1px 0 2px 4px;z-index: 9912 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_302',
text: 'провода'
},
{
xtype: 'label',
x: 922,
y: 167,
style: 'padding:1px 0 2px 4px;z-index: 9936 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_334',
text: 'давление'
},
{
xtype: 'label',
x: 693,
y: 171,
style: 'padding:1px 0 2px 4px;z-index: 9926 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_316',
text: 'давление'
},
{
xtype: 'label',
x: 955,
y: 425,
style: 'padding:1px 0 2px 4px;z-index: 9938 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_335',
text: 'воздуха'
},
{
xtype: 'label',
x: 955,
y: 400,
style: 'padding:1px 0 2px 4px;z-index: 9937 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_336',
text: 'воздуха'
},
{
xtype: 'label',
x: 710,
y: 431,
style: 'padding:1px 0 2px 4px;z-index: 9930 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_319',
text: 'воздуха'
},
{
xtype: 'label',
x: 710,
y: 404,
style: 'padding:1px 0 2px 4px;z-index: 9929 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_320',
text: 'воздуха'
},
{
xtype: 'label',
x: 263,
y: 62,
style: 'padding:1px 0 2px 4px;z-index: 9954 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_353',
text: 'отложений'
},
{
xtype: 'label',
x: 263,
y: 431,
style: 'padding:1px 0 2px 4px;z-index: 9958 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_356',
text: 'воздуха'
},
{
xtype: 'label',
x: 32,
y: 431,
style: 'padding:1px 0 2px 4px;z-index: 9946 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_344',
text: 'воздуха'
},
{
xtype: 'label',
x: 263,
y: 50,
style: 'padding:1px 0 2px 4px;z-index: 9953 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_354',
text: 'Масса'
},
{
xtype: 'label',
x: 482,
y: 431,
style: 'padding:1px 0 2px 4px;z-index: 9928 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_318',
text: 'воздуха'
},
{
xtype: 'label',
x: 263,
y: 404,
style: 'padding:1px 0 2px 4px;z-index: 9957 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_357',
text: 'воздуха'
},
{
xtype: 'label',
x: 44,
y: 62,
style: 'padding:1px 0 2px 4px;z-index: 9942 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_341',
text: 'отложений'
},
{
xtype: 'label',
x: 32,
y: 404,
style: 'padding:1px 0 2px 4px;z-index: 9945 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_345',
text: 'воздуха'
},
{
xtype: 'label',
x: 44,
y: 50,
style: 'padding:1px 0 2px 4px;z-index: 9941 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_342',
text: 'Масса'
},
{
xtype: 'label',
x: 480,
y: 404,
style: 'padding:1px 0 2px 4px;z-index: 9927 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_317',
text: 'воздуха'
},
{
xtype: 'label',
x: 237,
y: 171,
style: 'padding:1px 0 2px 4px;z-index: 9956 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_355',
text: 'давление'
},
{
xtype: 'label',
x: 13,
y: 171,
style: 'padding:1px 0 2px 4px;z-index: 9944 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_343',
text: 'давление'
},
{
xtype: 'label',
x: 462,
y: 171,
style: 'padding:1px 0 2px 4px;z-index: 9925 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_315',
text: 'давление'
},
{
xtype: 'label',
x: 497,
y: 62,
style: 'padding:1px 0 2px 4px;z-index: 9911 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_301',
text: 'отложений'
},
{
xtype: 'label',
x: 497,
y: 50,
style: 'padding:1px 0 2px 4px;z-index: 9910 ;color:#000000;background-color:transparent;font-size: 9pt ;font-weight:bold ',
id: 'TEXTMSG_295',
text: 'Масса'
},
{
xtype: 'label',
x: 493,
y: 6,
style: 'padding:1px 0 2px 4px;z-index: 9670 ;color:#000000;background-color:#d4d0c8;font-size: 16pt ;font-weight:bold ;text-decoration: underline; text-decoration-color: #000000 ;',
id: 'TEXTMSG_89',
text: 'Л-163 '
},
{
xtype: 'label',
x: 726,
y: 6,
style: 'padding:1px 0 2px 4px;z-index: 9700 ;color:#000000;background-color:transparent;font-size: 16pt ;font-weight:bold ;text-decoration: underline; text-decoration-color: #000000 ;',
id: 'TEXTMSG_131',
text: 'Л-163'
},
{
xtype: 'label',
x: 498,
y: 532,
style: 'padding:1px 0 2px 4px;z-index: 9729 ;color:#000000;background-color:transparent;font-size: 16pt ;font-weight:bold ;text-decoration: underline; text-decoration-color: #000000 ;',
id: 'TEXTMSG_152',
text: 'Л-164'
},
{
xtype: 'label',
x: 726,
y: 532,
style: 'padding:1px 0 2px 4px;z-index: 9742 ;color:#000000;background-color:#d4d0c8;font-size: 16pt ;font-weight:bold ;text-decoration: underline; text-decoration-color: #000000 ;',
id: 'TEXTMSG_173',
text: 'Л-164'
},
{
xtype: 'label',
x: 264,
y: 6,
style: 'padding:1px 0 2px 4px;z-index: 9668 ;color:#000000;background-color:#d4d0c8;font-size: 16pt ;font-weight:bold ;text-decoration: underline; text-decoration-color: #000000 ;',
id: 'TEXTMSG_194',
text: 'Л-171'
},
{
xtype: 'label',
x: 266,
y: 532,
style: 'padding:1px 0 2px 4px;z-index: 9784 ;color:#000000;background-color:#d4d0c8;font-size: 16pt ;font-weight:bold ;text-decoration: underline; text-decoration-color: #000000 ;',
id: 'TEXTMSG_215',
text: 'Л-172'
},
{
xtype: 'fieldset',
x: 227,
y: 0,
height: 683,
width: 224,
style: 'margin: 0; padding:0;;z-index: 0;background-color:#505050',
id: 'DECORATION_43'
},
{
xtype: 'fieldset',
x: 0,
y: 0,
height: 683,
width: 224,
style: 'margin: 0; padding:0;;z-index: 0;background-color:#505050',
id: 'DECORATION_42'
},
{
xtype: 'fieldset',
x: 454,
y: 0,
height: 683,
width: 224,
style: 'margin: 0; padding:0;;z-index: 0;background-color:#505050',
id: 'DECORATION_39'
},
{
xtype: 'fieldset',
x: 911,
y: 0,
height: 588,
width: 227,
style: 'margin: 0; padding:0;;z-index: 0;background-color:#505050',
id: 'DECORATION_41'
},
{
xtype: 'fieldset',
x: 684,
y: 0,
height: 683,
width: 224,
style: 'margin: 0; padding:0;;z-index: 0;background-color:#505050',
id: 'DECORATION_6'
}
]
});
