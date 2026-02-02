Ext.define('MyPanels.ps57_f_17', {
id: 'ps57_f_17',
title: 'ПС 57  ВФ-17',
width: 424,
height: 555,
layout: { type: 'absolute'},
bodyStyle: 'background-color:#c0c0c0',
items: [
{
xtype: 'fieldset',
x: 0,
y: 0,
height: 29,
width: 410,
id: 'DECORATION'
},
{
xtype: 'fieldset',
x: 10,
y: 497,
height: 20,
width: 20,
id: 'LED_162',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 498,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_162_LABEL',
text: 'ЧАПВ'
},
{
xtype: 'fieldset',
x: 10,
y: 474,
height: 20,
width: 20,
id: 'LED_157',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 475,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_157_LABEL',
text: 'АЧР-2'
},
{
xtype: 'fieldset',
x: 10,
y: 448,
height: 20,
width: 20,
id: 'LED_156',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 449,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_156_LABEL',
text: 'АЧР-1'
},
{
xtype: 'fieldset',
x: 10,
y: 423,
height: 20,
width: 20,
id: 'LED_155',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 424,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_155_LABEL',
text: 'УРОВ'
},
{
xtype: 'fieldset',
x: 10,
y: 301,
height: 20,
width: 20,
id: 'LED_154',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 302,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_154_LABEL',
text: 'ТО'
},
{
xtype: 'label',
x: 362,
y: 328,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_429',
text: 'кВар*ч'
},
{
xtype: 'label',
x: 362,
y: 288,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_430',
text: 'кВар*ч'
},
{
xtype: 'label',
x: 362,
y: 308,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_431',
text: 'кВт*ч'
},
{
xtype: 'textfield',
x: 261,
y: 304,
width: 100,
readOnly: true,
decimalPrecision: 2,
id: 'NUMERIC_U154_466',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 220,
y: 306,
height:18,
width:40,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_466_LABEL',
text: 'Wact-'
},
{
xtype: 'textfield',
x: 261,
y: 324,
width: 100,
readOnly: true,
decimalPrecision: 2,
id: 'NUMERIC_U154_467',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 215,
y: 326,
height:18,
width:45,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_467_LABEL',
text: 'Wract-'
},
{
xtype: 'label',
x: 362,
y: 268,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_432',
text: 'кВт*ч'
},
{
xtype: 'textfield',
x: 261,
y: 264,
width: 100,
readOnly: true,
decimalPrecision: 2,
id: 'NUMERIC_U154_468',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 220,
y: 266,
height:18,
width:40,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_468_LABEL',
text: 'Wact+'
},
{
xtype: 'textfield',
x: 261,
y: 284,
width: 100,
readOnly: true,
decimalPrecision: 2,
id: 'NUMERIC_U154_469',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 216,
y: 286,
height:18,
width:45,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_469_LABEL',
text: 'Wract+'
},
{
xtype: 'fieldset',
x: 10,
y: 400,
height: 20,
width: 20,
id: 'LED_153',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 401,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_153_LABEL',
text: 'Отказ управления'
},
{
xtype: 'fieldset',
x: 10,
y: 374,
height: 20,
width: 20,
id: 'LED_152',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 375,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_152_LABEL',
text: 'Неисправность ЦУ'
},
{
xtype: 'fieldset',
x: 10,
y: 324,
height: 20,
width: 20,
id: 'LED_163',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 325,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_163_LABEL',
text: 'Неисправность ТТ'
},
{
xtype: 'fieldset',
x: 10,
y: 349,
height: 20,
width: 20,
id: 'LED_151',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 350,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_151_LABEL',
text: 'Неисправность ЦП'
},
{
xtype: 'fieldset',
x: 10,
y: 278,
height: 20,
width: 20,
id: 'LED_150',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 279,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_150_LABEL',
text: 'МТЗ-1'
},
{
xtype: 'fieldset',
x: 10,
y: 253,
height: 20,
width: 20,
id: 'LED_149',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 254,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_149_LABEL',
text: 'МТЗ на землю направленная'
},
{
xtype: 'fieldset',
x: 10,
y: 228,
height: 20,
width: 20,
id: 'LED_148',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 229,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_148_LABEL',
text: 'Запрет ТУ'
},
{
xtype: 'fieldset',
x: 10,
y: 204,
height: 20,
width: 20,
id: 'LED_144',
vstyle:{ 0:[{background: "#ff0000"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 33,
y: 205,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_144_LABEL',
text: 'SEPAM серьезно поврежден'
},
{
xtype: 'fieldset',
x: 11,
y: 178,
height: 20,
width: 20,
id: 'LED_141',
vstyle:{ 0:[{background: "#339900"}],1:[{background: "#ff3300"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 34,
y: 179,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_141_LABEL',
text: 'РЗ'
},
{
xtype: 'fieldset',
x: 11,
y: 155,
height: 20,
width: 20,
id: 'LED_127',
vstyle:{ 0:[{background: "#339900"}],1:[{background: "#ff3300"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 34,
y: 156,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_127_LABEL',
text: 'Тележка'
},
{
xtype: 'fieldset',
x: 11,
y: 75,
height: 20,
width: 20,
id: 'LED_161',
vstyle:{ 0:[{background: "#a0a0a0"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 34,
y: 77,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_161_LABEL',
text: 'ВФ-17 ОТКЛ'
},
{
xtype: 'fieldset',
x: 11,
y: 51,
height: 20,
width: 20,
id: 'LED_131',
vstyle:{ 0:[{background: "#a0a0a0"}],1:[{background: "#a0a0a0"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 34,
y: 53,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_131_LABEL',
text: 'ВФ-17 ВКЛ '
},
{
xtype: 'fieldset',
x: 0,
y: 27,
height: 498,
width: 213,
id: 'DECORATION_49'
},
{
xtype: 'label',
x: 14,
y: 110,
style: 'padding:2px 0 2px 4px;font-size: 8pt;font-weight:bold;color:#0000cc;background-color:#d4d0c8',
id: 'TEXTMSG_60',
text: 'ТС'
},
{
xtype: 'label',
x: 7,
y: 34,
style: 'padding:2px 0 2px 4px;font-size: 8pt;font-weight:bold;color:#0000cc;background-color:#d4d0c8',
id: 'TEXTMSG_59',
text: 'Телеуправление'
},
{
xtype: 'label',
x: 165,
y: 6,
style: 'padding:2px 0 2px 4px;font-size: 8pt;font-weight:bold;color:#0000cc;background-color:#d4d0c8',
id: 'TEXTMSG_58',
text: 'ВФ-17'
},
{
xtype: 'label',
x: 334,
y: 169,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_409',
text: 'кВ'
},
{
xtype: 'label',
x: 334,
y: 150,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_408',
text: 'кВ'
},
{
xtype: 'label',
x: 334,
y: 129,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_403',
text: 'кВ'
},
{
xtype: 'label',
x: 334,
y: 106,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_411',
text: 'A'
},
{
xtype: 'label',
x: 334,
y: 85,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_410',
text: 'A'
},
{
xtype: 'label',
x: 334,
y: 61,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_402',
text: 'A'
},
{
xtype: 'fieldset',
x: 11,
y: 133,
height: 20,
width: 20,
id: 'LED_128',
vstyle:{ 0:[{background: "#339900"}],1:[{background: "#ff3300"}]},
cls: 'wasutp_led_state_default'
},
{
xtype: 'label',
x: 34,
y: 135,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'LED_128_LABEL',
text: 'ВФ-17'
},
{
xtype: 'label',
x: 334,
y: 211,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_405',
text: 'кВАр'
},
{
xtype: 'label',
x: 334,
y: 187,
style: 'padding:2px 0 2px 4px;font-size: 6pt;font-weight:bold;background-color:#d4d0c8',
id: 'TEXT_404',
text: 'кВт'
},
{
xtype: 'textfield',
x: 262,
y: 99,
width: 69,
readOnly: true,
decimalPrecision: 1,
id: 'NUMERIC_U154_443',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 228,
y: 101,
height:18,
width:33,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_443_LABEL',
text: 'I c'
},
{
xtype: 'textfield',
x: 262,
y: 77,
width: 69,
readOnly: true,
decimalPrecision: 1,
id: 'NUMERIC_U154_444',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 229,
y: 79,
height:18,
width:33,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_444_LABEL',
text: 'I b'
},
{
xtype: 'textfield',
x: 262,
y: 56,
width: 69,
readOnly: true,
decimalPrecision: 1,
id: 'NUMERIC_U154_445',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 228,
y: 58,
height:18,
width:33,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_445_LABEL',
text: 'I a'
},
{
xtype: 'textfield',
x: 262,
y: 162,
width: 69,
readOnly: true,
decimalPrecision: 2,
id: 'NUMERIC_U154_446',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 228,
y: 164,
height:18,
width:33,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_446_LABEL',
text: 'U ca'
},
{
xtype: 'textfield',
x: 262,
y: 141,
width: 69,
readOnly: true,
decimalPrecision: 2,
id: 'NUMERIC_U154_447',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 228,
y: 143,
height:18,
width:33,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_447_LABEL',
text: 'U bc'
},
{
xtype: 'textfield',
x: 262,
y: 122,
width: 69,
readOnly: true,
decimalPrecision: 2,
id: 'NUMERIC_U154_448',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 228,
y: 124,
height:18,
width:33,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_448_LABEL',
text: 'U ab'
},
{
xtype: 'textfield',
x: 262,
y: 205,
width: 69,
readOnly: true,
decimalPrecision: 2,
id: 'NUMERIC_U154_450',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 229,
y: 207,
height:18,
width:33,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_450_LABEL',
text: 'Q'
},
{
xtype: 'textfield',
x: 262,
y: 184,
width: 69,
readOnly: true,
decimalPrecision: 2,
id: 'NUMERIC_U154_451',
hideLabel: true,
disabled: true
},
{
xtype: 'label',
x: 228,
y: 186,
height:18,
width:33,
style: 'padding:2px 0 2px 4px;font-weight:bold;background-color:#d4d0c8',
id: 'NUMERIC_U154_451_LABEL',
text: 'P'
},
{
xtype: 'label',
x: 234,
y: 37,
style: 'padding:2px 0 2px 4px;font-size: 8pt;font-weight:bold;color:#0000cc;background-color:#d4d0c8',
id: 'TEXTMSG_15',
text: 'ТИ'
},
{
xtype: 'fieldset',
x: 212,
y: 28,
height: 497,
width: 200,
id: 'DECORATION_38'
}
]
});
