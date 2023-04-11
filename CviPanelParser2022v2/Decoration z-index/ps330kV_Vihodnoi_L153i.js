Ext.define('MyPanels.ps330kV_Vihodnoi_L153', {
id: 'ps330kV_Vihodnoi_L153',
title: 'ПС 330 кВ Выходной Л-153',
width: 293,
height: 237,
layout: { type: 'absolute'},
bodyStyle: 'background-color:#c0c0c0',
items: [
{
xtype: 'label',
x: 219,
y: 118,
style: 'padding:2px 0 2px 4px;z-index: 9990;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_407',
text: 'A'
},
{
xtype: 'label',
x: 219,
y: 95,
style: 'padding:2px 0 2px 4px;z-index: 9989;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_406',
text: 'A'
},
{
xtype: 'textfield',
x: 145,
y: 111,
width: 69,
height: 21,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 1,
id: 'NUMERIC_U154_443',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '12px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 111,
y: 113,
height:18,
width:33,
style: 'padding:2px 0 2px 4px;z-index: 9988;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_443_LABEL',
text: 'I c'
},
{
xtype: 'textfield',
x: 145,
y: 89,
width: 69,
height: 21,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 1,
id: 'NUMERIC_U154_444',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '12px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 111,
y: 91,
height:18,
width:33,
style: 'padding:2px 0 2px 4px;z-index: 9987;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_444_LABEL',
text: 'I b'
},
{
xtype: 'textfield',
x: 145,
y: 68,
width: 69,
height: 21,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 1,
id: 'NUMERIC_U154_445',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '12px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 111,
y: 70,
height:18,
width:33,
style: 'padding:2px 0 2px 4px;z-index: 9985;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_445_LABEL',
text: 'I a'
},
{
xtype: 'textfield',
x: 145,
y: 136,
width: 69,
height: 21,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 2,
id: 'NUMERIC_U154_464',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '12px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 125,
y: 139,
height:17,
width:19,
style: 'padding:2px 0 2px 4px;z-index: 9992;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_464_LABEL',
text: 'P'
},
{
xtype: 'textfield',
x: 145,
y: 159,
width: 69,
height: 21,
readOnly: true,
style:'z-index: 9999',
decimalPrecision: 2,
id: 'NUMERIC_U154_465',
hideLabel: true,
disabled: true,
fieldStyle: {
'fontSize': '12px',
'fontWeight': 'bold'
}
},
{
xtype: 'label',
x: 124,
y: 162,
height:17,
width:20,
style: 'padding:2px 0 2px 4px;z-index: 9994;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_465_LABEL',
text: '  Q '
},
{
xtype: 'label',
x: 220,
y: 142,
style: 'padding:2px 0 2px 4px;z-index: 9991;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_422',
text: 'МВт'
},
{
xtype: 'label',
x: 220,
y: 164,
style: 'padding:2px 0 2px 4px;z-index: 9993;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_424',
text: 'МВАр'
},
{
xtype: 'label',
x: 219,
y: 72,
style: 'padding:2px 0 2px 4px;z-index: 9986;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_402',
text: 'A'
},
{
xtype: 'fieldset',
x: 0,
y: 0,
height: 30,
width: 280,
style: 'margin: 0; padding:0;;background-color:#d4d0c8;z-index: 9980',
id: 'DECORATION'
},
{
xtype: 'label',
x: 12,
y: 45,
style: 'padding:2px 0 2px 4px;z-index: 9998;font-size: 8pt;font-weight:bold;color:#000099;background-color:#d4d0c8',
id: 'TEXTMSG_63',
text: 'ТC'
},
{
xtype: 'label',
x: 113,
y: 45,
style: 'padding:2px 0 2px 4px;z-index: 9983;font-size: 8pt;font-weight:bold;color:#000099;background-color:#d4d0c8',
id: 'TEXTMSG_62',
text: 'ТИ'
},
{
xtype: 'fieldset',
x: 0,
y: 30,
height: 176,
width: 280,
style: 'margin: 0; padding:0;;background-color:#d4d0c8;z-index: 9982',
id: 'DECORATION_50'
},
{
xtype: 'fieldset',
x: 101,
y: 39,
height: 150,
width: 172,
style: 'margin: 0; padding:0;;border: 2px solid #d4d0c8;z-index: 9984',
id: 'DECORATION_51'
},
{
xtype: 'label',
x: 39,
y: 8,
style: 'padding:2px 0 2px 4px;z-index: 9981;font-size: 10pt;font-weight:bold;color:#000099;background-color:#d4d0c8',
id: 'TEXTMSG_58',
text: 'Л-153 150 кВ'
},
{
xtype: 'fieldset',
x: 11,
y: 135,
height: 20,
width: 20,
id: 'LED_4',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#009900"}],1:[{background: "#ff0000"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 36,
y: 136,
style: 'padding:2px 0 2px 4px;z-index: 9999;font-weight:bold;background-color:#d4d0c8',
id: 'LED_4_LABEL',
text: 'РЗЛ Л -153'
},
{
xtype: 'fieldset',
x: 10,
y: 65,
height: 20,
width: 20,
id: 'LED_2',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#009900"}],1:[{background: "#ff0000"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 37,
y: 66,
style: 'padding:2px 0 2px 4px;z-index: 9996;font-weight:bold;background-color:#d4d0c8',
id: 'LED_2_LABEL',
text: 'РЛ -153'
},
{
xtype: 'fieldset',
x: 10,
y: 87,
height: 20,
width: 20,
id: 'LED_3',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#009900"}],1:[{background: "#ff0000"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 37,
y: 88,
style: 'padding:2px 0 2px 4px;z-index: 9997;font-weight:bold;background-color:#d4d0c8',
id: 'LED_3_LABEL',
text: 'РО -153'
},
{
xtype: 'fieldset',
x: 10,
y: 110,
height: 20,
width: 20,
id: 'LED',
style:{'z-index': 9999},
vstyle:{ 0:[{background: "#009900"}],1:[{background: "#ff0000"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 37,
y: 111,
style: 'padding:2px 0 2px 4px;z-index: 9995;font-weight:bold;background-color:#d4d0c8',
id: 'LED_LABEL',
text: 'РШ -153'
}
]
});
