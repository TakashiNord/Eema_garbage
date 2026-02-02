using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;

// 134 прибор цифровой 2
// 17 метка
// 3 прямоугольник



namespace Xsde2Xml
{

    public struct DispName
    {
        public string text;         // Текст
        public string position;     // Позиция относительно центра фигуры
        public string align;        // Позиция относительно координаты X
        public string textColor;    // Цвет текста
        public string bgColor;      // Цвет фона
        public float x;             // Координаты подписи
        public float y;
        public string points;       // Точки для длинной фигуры
        public int type;            // Тип фигуры (надо что бы рассчитать отступы)
        public float scaleText;     // Масштаб текста
        public float scaleFigure;   // Масштаб фигуры
        public bool hasParamText;
    };
   
    public partial class Form1 : Form
    {
        string fileNameOutput;
        Hashtable substitutesType;
        Hashtable borderTI;
        
        int[] TC = { 41, 42, 43, 45, 46, 49, 50, 54, 106, 162, 163, 164, 398 };
        int[] TT = { 3, 17, 134 };

        public Form1()
        {
            InitializeComponent();
            buttonStart.Enabled = false;
            processToUniConvert.EnableRaisingEvents = true;
            processToUniConvert.Exited += new EventHandler(OnUniProcessExiting);

            // Таблица подстановок типов
            substitutesType = new Hashtable();
            substitutesType.Add(399, 164);  // Автомат силовой -> отделитель
            substitutesType.Add(146, 7);    // Опора - точка
            substitutesType.Add(102, 134);    // Табло - прибор цифровой2

            borderTI = new Hashtable();
                    
        }


        private string CorrectColor(string color)
        {
            switch (color)
            {
                case "золотистый":  return "$DAA520";
                case "темно_голубой":  return "$A6CAF0";
                    
                case "черный":      return "$000000";
                case "black":       return "$000000";
                case "белый":       return "$FFFFFF";
                case "white":       return "$FFFFFF";

                default:
                    short number;
                    if (Int16.TryParse(color.TrimStart('$'), out number))
                        return color;
                    else
                        return "$AAAAAA";
            }
        }

        private void CorrectMargin(ref DispName dm)
        {
            float shift = 0;

            switch (dm.type)
            {
                case 47:  // Автотрансформатор
                    shift = 8;
                break;
                case 106:   // Лампа
                    shift = 3.5F;
                break;
                case 24:   // Шина
                    shift = 2;
                break;
                case 54:    // Заземляющий нож
                case 168:   //Разрядник
                    shift = 4;
                break;
                case 134:   //Прибор цифровой
                shift = 4;
                break;
                case 7:     // Точка
                    shift = 2;
                break;
                case 55:     // Трансформатор напряжения
                shift = 4;
                break;
            }

            if (dm.position == "LEFT" || dm.position == "RIGHT")
            {
                if (dm.type == 2) //С точками лажа у Модуса
                {
                    switch (dm.align)
                    {
                        case "NONE":
                            dm.y -= shift;
                            break;
                        case "RIGHT":
                            dm.y -= (shift / 2);
                            break;
                        case "CENTER":
                            break;
                        case "LEFT":
                            dm.y += (shift / 2);
                            break;
                        case "ABSRIGHT":
                            dm.y += shift;
                            break;
                    }
                }
                else
                {
                    switch (dm.align)
                    {
                        case "NONE":
                            dm.y -= shift;
                            break;
                        case "LEFT":
                            dm.y -= (shift / 2);
                            break;
                       case "CENTER":
                            break;
                        case "RIGHT":
                            dm.y += (shift / 2);
                            break;
                        case "ABSRIGHT":
                            dm.y += shift;
                            break;
                    }
                }
            }

            switch(dm.position)
            {
                case "TOP":
                    dm.y -= shift;
                break;
                case "BOTTOM":
                    dm.y += shift;
                break;
                case "LEFT":
                     shift += dm.text.Length * (16F + dm.scaleText)*(16F + dm.scaleFigure)*0.003F;
                     dm.x -= shift;
                break;
                case "RIGHT":
                if (dm.type == 24)
                {
                    string[] str = dm.points.Split(new Char[] { ',' });
                    if (str.Count() > 1)
                    {
                        string[] str1 = str[str.Count()-1].Split(new Char[] { ' ' });

                        dm.x = Single.Parse(str1[0].Replace('.', ','));
                        dm.y = Single.Parse(str1[1].Replace('.', ','));
                    }

                }
                    
                    dm.x += shift;
                break;
            }
            
        }

        /**
         * Возвращает замененный тип, для типов которые не поддреживаюся в клиенте отображения через WEB
         */

        
        private int GetSubstituteType(int id)
        {
            return (substitutesType.ContainsKey(id)) ? (int)substitutesType[id] : id;
        }

        private string GetTypeNameFromTypeId(int id)
        {
            foreach (int i in TC)
                if (i == id)
                    return "TC1:3:";

            foreach (int i in TT)
                if (i == id)
                    return "TT1:1:";

            return "";

        }

        private void OnClickOpenFile(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;

            openFileDialog1.Filter = "MODUS xml files (*.xsde)|*.xsde";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxInput.Text   = openFileDialog1.FileName;
                buttonStart.Enabled = true;

               fileNameOutput = System.IO.Path.GetDirectoryName(openFileDialog1.FileName) +@"\"+ System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName).Replace(" ","_") + @".out" + @".xsde";
            }
        }

        private void OnUniProcessExiting(object sender, EventArgs e)
        {
            textBoxMessages.AppendText("Конвертирование в формат uni заверешено [" + processToUniConvert.ExitCode + "]\n");

            if (System.IO.File.Exists(fileNameOutput + @".uni"))
            {
               // System.IO.File.Delete(fileNameOutput);

                string strUniFile = (textBoxInput.Text + @".uni").Replace(" ", "_");

                if (System.IO.File.Exists(strUniFile))
                {
                    System.IO.File.Delete(strUniFile);
                }

                System.IO.File.Move(fileNameOutput + @".uni", strUniFile);

                if (processToUniConvert.ExitCode == 0)
                {
                    AddBorderOnTi(strUniFile);
                    textBoxMessages.AppendText("Результат в файле: " + strUniFile + "\n");
                }
                
            }
        }

        private void AddBorderOnTi(string strUniFile)
        {
            XmlDocument xmlDocIn = new XmlDocument();
            xmlDocIn.Load(strUniFile);
            string strValue;
            string strColor;

            foreach (XmlNode table in xmlDocIn.DocumentElement.ChildNodes)
            {
                if (table.Name == "pages")
                {
                    foreach (XmlNode pages in table.ChildNodes)
                    {
                        if (pages.Name == "page")
                        {
                            foreach (XmlNode page in pages.ChildNodes)
                            {
                                if (page.Name == "type" && page.Attributes["id"].Value == "134")
                                {
                                    foreach (XmlElement obj in page.ChildNodes)
                                    {
                                        strValue = obj.Attributes["keyLink"].Value.Split(new Char[]{':'})[2];
                                        if(borderTI.ContainsKey(strValue))
                                        {
                                            
                                            strColor = CorrectColor(borderTI[strValue].ToString()).TrimStart('$');
                                            obj.SetAttribute("borderColor", strColor);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
             
            }

            xmlDocIn.Save(strUniFile);
        }

        private void OnStart(object sender, EventArgs e)
        {
            DispName    dispName;
            
            dispName.text       = "";
            dispName.textColor  = "0A0A0A";
            dispName.bgColor    = "000000";
            dispName.position   = "LEFT";
            dispName.align      = "NONE";
            dispName.x          = 0;
            dispName.y          = 0;
            dispName.scaleText  = 0;
            dispName.scaleFigure = 0;
            dispName.type       = 0;
            dispName.points     = "";
            dispName.hasParamText = false;

            string bgColor = "000000";

            XmlNode     textNode = null;

            int num = 0;
            string strTypeName;

            textBoxMessages.Clear();
            
            XmlDocument xmlDocIn = new XmlDocument();

            xmlDocIn.Load(openFileDialog1.FileName);
 
            string strVersion = xmlDocIn.DocumentElement.Attributes["FullVersion"].InnerText;

            if (strVersion.StartsWith("5.20"))
            {

                foreach (XmlNode table in xmlDocIn.DocumentElement.ChildNodes)
                {
                    if (table.Name == "Pages")
                    {
                        foreach (XmlNode pages in table.ChildNodes)
                        {
                            if (pages.Name == "Page")
                            {
                                if (pages.Attributes["color"] != null)
                                {
                                    bgColor = CorrectColor(pages.Attributes["color"].Value);
                                }

                                foreach (XmlNode page in pages.ChildNodes)
                                {
                                    if (page.Name == "SDEObjects")
                                    {
                                        foreach (XmlNode sdeobj in page.ChildNodes)
                                        {
                                            if ((int)(int.Parse(sdeobj.Attributes["typ"].InnerText)) == 5)
                                            {
                                                // Текст уже есть
                                                textNode = sdeobj;

                                                if (sdeobj.Attributes["color"] != null)
                                                {
                                                    if(correctTextColor.Checked && (CorrectColor(sdeobj.Attributes["color"].Value) == CorrectColor(bgColor)))
                                                        sdeobj.Attributes["color"].Value = "$A0A0A0";
                                                }

                                            }


                                            if (sdeobj.Name == "Type")
                                            {
                                                
                                                foreach (XmlNode sde in sdeobj.ChildNodes)
                                                {
                                                    if (sde.Name == "SDE")
                                                    {
                                                        dispName.type = (int)int.Parse(sdeobj.Attributes["typ"].Value);

                                                        if (sde.Attributes["scale"] != null)
                                                            dispName.scaleFigure = Single.Parse(sde.Attributes["scale"].Value.Replace('.', ','));

                                                        if (sde.Attributes["origin"] != null)
                                                        {
                                                            string [] str = sde.Attributes["origin"].Value.Split(new Char[] { ' ' });
                                                            if (str.Count() == 2)
                                                            {
                                                                dispName.x = Single.Parse(str[0].Replace('.',','));
                                                                dispName.y = Single.Parse(str[1].Replace('.',','));
                                                            }
                                                        }
                                                        else
                                                            if (sde.Attributes["coords"] != null)
                                                            {
                                                                string[] str = sde.Attributes["coords"].Value.Split(new Char[] { ',' });
                                                                if (str.Count() > 1)
                                                                {
                                                                    string[] str1 = str[0].Split(new Char[] { ' ' });

                                                                    dispName.x = Single.Parse(str1[0].Replace('.', ','));
                                                                    dispName.y = Single.Parse(str1[1].Replace('.', ','));
                                                                }
                                                            }
                                                            else
                                                                if (sde.Attributes["points"] != null)
                                                                {
                                                                    dispName.points = sde.Attributes["points"].Value;

                                                                    string[] str = sde.Attributes["points"].Value.Split(new Char[] { ',' });
                                                                    if (str.Count() > 1)
                                                                    {
                                                                        string[] str1 = str[0].Split(new Char[] { ' ' });

                                                                        dispName.x = Single.Parse(str1[0].Replace('.', ','));
                                                                        dispName.y = Single.Parse(str1[1].Replace('.', ','));
                                                                    }
                                                                }

                                                        foreach (XmlElement tech in sde.ChildNodes)
                                                        {
                                                            if (tech.Name == "LineStyle" && sdeobj.Attributes["typ"].Value == "134")
                                                            {
                                                                if (tech.Attributes["color"] != null)
                                                                    borderTI.Add(sde.Attributes["sTag"].Value,tech.Attributes["color"].Value);
                                                                                                                                
                                                            }

                                                            if (tech.Name == "ParamText")
                                                            {
                                                                dispName.hasParamText = true;

                                                                if (tech.Attributes["fPosition"] != null)
                                                                    dispName.position = tech.Attributes["fPosition"].Value;

                                                                if (tech.Attributes["fAlign"] != null)
                                                                    dispName.align = tech.Attributes["fAlign"].Value;

                                                                if (tech.Attributes["color"] != null)
                                                                    dispName.textColor = tech.Attributes["color"].Value;

                                                                if (tech.Attributes["bgColor"] != null)
                                                                    dispName.bgColor = tech.Attributes["bgColor"].Value;

                                                                if (tech.Attributes["scale"] != null)
                                                                    dispName.scaleText = Single.Parse(tech.Attributes["scale"].Value.Replace('.', ','));

                                                                if (tech.Attributes["subscriptName"] != null && tech.Attributes["subscriptName"].Value != "%дисп_имя%")
                                                                    dispName.text = tech.Attributes["subscriptName"].Value;
                                                            }

                                                            if (tech.Name == "Tech")
                                                            {
                                                                sdeobj.Attributes["typ"].Value = GetSubstituteType((int)int.Parse(sdeobj.Attributes["typ"].InnerText)).ToString();

                                                                if ((strTypeName = GetTypeNameFromTypeId(int.Parse(sdeobj.Attributes["typ"].InnerText))) != "")
                                                                {
                                                                    tech.SetAttribute("keyLink", "#" + strTypeName + sde.Attributes["sTag"].InnerText);
                                                                    textBoxMessages.AppendText(++num + ": " + sdeobj.Attributes["ObjectType"].InnerText + "[" + sdeobj.Attributes["typ"].InnerText + "] sTag:" + sde.Attributes["sTag"].InnerText + "\n");
                                                                }
                                                                else
                                                                {
                                                                    textBoxMessages.AppendText("Нет привязки для: " + sdeobj.Attributes["ObjectType"].InnerText + "[" + sdeobj.Attributes["typ"].InnerText + "] sTag:" + ((sde.Attributes["sTag"]!=null)?sde.Attributes["sTag"].InnerText:"X") + "\n");
                                                                }

                                                                
                                                                // Получаем диспетчерское наименование элемента

                                                                if (tech.Attributes["DispName"] != null )
                                                                {                                                                    
                                                                    dispName.text = tech.Attributes["DispName"].Value;
                                                                }
                                                           }

                                                        }
                                                    }

                                                    if (dispName.text != "")
                                                    {
                                                        textBoxMessages.AppendText(dispName.text + ":" + dispName.position + ":" + dispName.x + ":" + dispName.y + ":" + dispName.textColor + ":" + dispName.bgColor + ":" + dispName.type + "\n");

                                                        CorrectMargin(ref dispName);

                                                        if (textNode != null)
                                                        {
                                                            if (dispName.textColor != "прозрачный" && dispName.hasParamText)
                                                            {
                                                                XmlNode newNode = xmlDocIn.CreateNode(XmlNodeType.Element, "SDE", "");

                                                                XmlNode attr = xmlDocIn.CreateNode(XmlNodeType.Attribute, "origin", "");
                                                                attr.Value = dispName.x.ToString().Replace(',', '.') + " " + dispName.y.ToString().Replace(',', '.');
                                                                newNode.Attributes.SetNamedItem(attr);

                                                                attr = xmlDocIn.CreateNode(XmlNodeType.Attribute, "inText", "");
                                                                attr.Value = dispName.text;
                                                                newNode.Attributes.SetNamedItem(attr);

                                                                attr = xmlDocIn.CreateNode(XmlNodeType.Attribute, "color", "");
                                                                attr.Value = dispName.textColor;
                                                                newNode.Attributes.SetNamedItem(attr);

                                                                attr = xmlDocIn.CreateNode(XmlNodeType.Attribute, "bgColor", "");
                                                                attr.Value = dispName.bgColor;
                                                                newNode.Attributes.SetNamedItem(attr);

                                                                attr = xmlDocIn.CreateNode(XmlNodeType.Attribute, "scale", "");
                                                                attr.Value = dispName.scaleText.ToString().Replace(',', '.');
                                                                newNode.Attributes.SetNamedItem(attr);

                                                                newNode.InnerXml = "<Tech /><ParamText />";
                                                                textNode.AppendChild(newNode);
                                                            }
                                                        }
                                                    }

                                                    dispName.text = "";
                                                    dispName.position = "LEFT";
                                                    dispName.x = 0;
                                                    dispName.y = 0;
                                                    dispName.scaleText = 0;
                                                    dispName.scaleFigure = 0;
                                                    dispName.type = 0;
                                                    dispName.points = "";
                                                    dispName.align = "NONE";
                                                    dispName.hasParamText = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                textBoxMessages.AppendText("Найдено тегов: " + num + "\n");
                xmlDocIn.Save(fileNameOutput);

                if (System.IO.File.Exists("XsdeToUni.exe"))
                {
                    textBoxMessages.AppendText("Запуск конвертера в формат uni\n");

                    try
                    {
                        processToUniConvert.StartInfo.FileName = @"XsdeToUni.exe";
                        processToUniConvert.StartInfo.Arguments = "\"" + fileNameOutput + "\"";
                        processToUniConvert.Start();
                    }
                    catch (Exception ex)
                    {
                        textBoxMessages.AppendText("Ошибка при конвертации в uni:" + ex.Message + "\n");
                    }
                }
                else
                    textBoxMessages.AppendText("Не найден конвертер в формат uni\n");
            }
            else
                textBoxMessages.AppendText("Версия xsde файла должна быть 5.20. Выбранный файл имеет версию: " + strVersion + "\n");

        }

    }
}
