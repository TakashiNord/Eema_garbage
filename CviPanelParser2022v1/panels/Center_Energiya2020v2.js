Ext.define('MyPanels.Center_Energiya', {
id: 'Center_Energiya',
title: 'Ярактинское нефтегазоконденсатное месторождение ООО "ИНК"',
width: 989,
height: 523,
layout: { type: 'absolute'},
bodyStyle: 'background-color:#e1e3ea',
items: [
{
xtype: 'label',
x: 722,
y: 24,
style: 'padding:2px 0 2px 4px;z-index: 9996;font-size: 14pt;background-color:#e1e3ea',
id: 'TEXTMSG_774',
text: 'DD.MM.YYYY HH24:MI'
},
{
xtype: 'fieldset',
x: 931,
y: 20,
height: 32,
width: 32,
style: 'margin: 0; padding:0;;background-color:#ffffff;z-index: 9994',
id: 'PICTURE_57'
},
{
xtype: 'fieldset',
x: 10,
y: 35,
height: 2,
width: 700,
style: 'margin: 0; padding:0;;background-color:#00664f;z-index: 9997',
id: 'SPLITTER_129'
},
{
xtype: 'label',
x: 10,
y: 4,
style: 'padding:2px 0 2px 4px;z-index: 9995;font-size: 16pt;color:#00664f',
id: 'TEXTMSG_4',
text: 'Центральная ГТЭС'
},
{
xtype: 'label',
x: 10,
y: 38,
style: 'padding:2px 0 2px 4px;z-index: 9998;font-size: 15pt;font-weight:bold;color:#00664f',
id: 'TEXTMSG_58',
text: 'Электроэнергия'
}
]
});
