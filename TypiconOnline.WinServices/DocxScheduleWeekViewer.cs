﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Schedule;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.AppServices.Common;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.WinServices
{
    public class DocxScheduleWeekViewer : IScheduleWeekViewer
    {
        private string _fileName;

        public DocxScheduleWeekViewer(string fileName)
        {
            _fileName = fileName;
        }

        public void Execute(ScheduleWeek week)
        {
            if (!File.Exists(_fileName))
                throw new FileNotFoundException(_fileName);

            if (week == null)
                throw new ArgumentNullException("week");

            using (WordprocessingDocument doc = WordprocessingDocument.Open(_fileName, true))
            {
                //просматриваем все элементы ChildElements и выбираем только таблицы
                //считаем, что всего таблиц 10. 
                //0 - шапка с названием седмицы, а остальные - шаблоны для обозначения знаков служб

                //эта коллекция будет вместилищем таблиц - docx-шаблонов
                var templateTables = new List<Table>();
                //ищем их все и добавляем в коллекцию
                foreach (OpenXmlElement element in doc.MainDocumentPart.Document.Body.ChildElements)
                {
                    if (element.GetType() == typeof(Table))
                        templateTables.Add((Table)element);
                }

                //создаем коллекцию таблиц, которые будут результирующим содержанием выходного документа
                List<OpenXmlElement> resultElements = new List<OpenXmlElement>();

                //шапка
                Table headerTable = templateTables[0];

                //Название седмицы
                //table[2]->tr[1]->td[1]->p[1]->r[1]->t[1]
                TableCell tdWeekName = (TableCell)headerTable.ChildElements[2].ChildElements[1];
                SetTextToCell(tdWeekName, week.Name, false, false);
                resultElements.Add(headerTable);

                foreach (ScheduleDay day in week.Days)
                {
                    Table dayTable = new Table();
                    int sign = InterpretSignNumber(day.SignNumber);
                    //в зависимости от того, какой знак дня - берем для заполнения шаблона соответствующую таблицу в templateTables
                    Table dayTemplateTable = templateTables[sign];

                    TableRow tr = (TableRow)dayTemplateTable.ChildElements[2].Clone();
                    TableCell tdDayofweek = (TableCell)tr.ChildElements[2];
                    TableCell tdName = (TableCell)tr.ChildElements[3];
                    string sDayofweek = day.Date.ToString("dddd").ToUpper();
                    string sName = day.Name;
                    //TODO: реализовать функционал
                    bool bIsNameBold = false;//(dayNode.SelectSingleNode("name").Attributes["isbold"] != null);

                    SetTextToCell(tdDayofweek, sDayofweek, false, false);
                    SetTextToCell(tdName, sName, bIsNameBold, false);
                    dayTable.AppendChild(tr);

                    tr = (TableRow)dayTemplateTable.ChildElements[3].Clone();
                    TableCell tdDate = (TableCell)tr.ChildElements[2];
                    string sDate = day.Date.ToString("dd MMMM yyyy г.");
                    SetTextToCell(tdDate, sDate, false, false);
                    dayTable.AppendChild(tr);

                    foreach (WorshipRuleViewModel service in day.Schedule.ChildElements)
                    {
                        tr = (TableRow)dayTemplateTable.ChildElements[4].Clone();
                        TableCell tdTime = (TableCell)tr.ChildElements[2];
                        TableCell tdSName = (TableCell)tr.ChildElements[3];

                        string sTime = service.Time.ToString();
                        string sSName = service.Text;

                        bool bIsTimeBold = false; //(serviceNode.Attributes["istimebold"] != null);
                        bool bIsTimeRed = false; //(serviceNode.Attributes["istimered"] != null);

                        bool bIsServiceNameBold = false; //(serviceNode.Attributes["isnamebold"] != null);
                        bool bIsServiceNameRed = false; //(serviceNode.Attributes["isnamered"] != null);


                        SetTextToCell(tdTime, sTime, bIsTimeBold, bIsTimeRed);
                        SetTextToCell(tdSName, sSName, bIsServiceNameBold, bIsServiceNameRed);

                        //additionalName
                        if (!string.IsNullOrEmpty(service.AdditionalName))
                        {
                            AppendTextToCell(tdSName, service.AdditionalName, true, false);
                        }

                        //tr.ChildElements[1].InnerXml = dayNode.SelectSingleNode("time").InnerText;
                        //tr.ChildElements[2].InnerXml = dayNode.SelectSingleNode("name").InnerText;
                        dayTable.AppendChild(tr);
                    }
                    //добавляем таблицу к выходной коллекции
                    resultElements.Add(dayTable);
                }

                //в конце удаляем все из документа, оставляя только колонтитулы (прописаны в SectionProperties) и результирующие таблицы

                foreach (OpenXmlElement el in doc.MainDocumentPart.Document.Body.ChildElements)
                {
                    if (el.GetType() == typeof(SectionProperties))
                    {
                        resultElements.Add(el);
                        break;
                    }
                }

                doc.MainDocumentPart.Document.Body.RemoveAllChildren();
                foreach (OpenXmlElement t in resultElements)
                {
                    doc.MainDocumentPart.Document.Body.AppendChild(t);
                }

                doc.MainDocumentPart.Document.Save();
            }
        }

        /// <summary>
        /// преобразует номер знака к индексу нужной таблицы в шаблоне
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        private int InterpretSignNumber(int sign)
        {
            return SignMigrator.GetOldId(k => k.Value.NewID == sign);
        }

        /// <summary>
        /// Функция задает строчное значение text для параграфа
        /// </summary>
        /// <param name="td"></param>
        /// <param name="text"></param>
        private void SetTextToCell(TableCell td, string text, bool isBold, bool isRed)
        {
            //выбираем первый элемент run
            //удаляем все остальные
            //задаем в элементе text нужное значение
            Paragraph p = (Paragraph)td.ChildElements[1];

            Run r = null;
            //ищем первый попавшийся элемент run в параграфе
            foreach (OpenXmlElement element in p.ChildElements)
            {
                if (element.GetType() == typeof(Run))
                {
                    r = (Run)element.Clone();
                    break;
                }
            }
            //RunStyle

            RunProperties properties = null;

            if (isBold || isRed)
            {
                properties = (RunProperties)r.ChildElements[0];
            }

            if (isBold)
            {
                properties.Bold = new Bold();
            }

            if (isRed)
            {
                properties.Color = new Color() { Val = "FF0000" };
            }

            Text t = (Text)r.ChildElements[1];
            t.Text = text;

            p.RemoveAllChildren<Run>();
            p.Append(r);
        }

        /// <summary>
        /// Функция добавляет строчное значение text для параграфа
        /// </summary>
        /// <param name="td"></param>
        /// <param name="text"></param>
        /// <param name="isBold"></param>
        /// <param name="isRed"></param>
        private void AppendTextToCell(TableCell td, string text, Boolean isBold, Boolean isRed)
        {
            //выбираем первый элемент run
            //удаляем все остальные
            //задаем в элементе text нужное значение
            Paragraph p = (Paragraph)td.ChildElements[1];

            Run r = null;
            //ищем первый попавшийся элемент run в параграфе
            foreach (OpenXmlElement element in p.ChildElements)
            {
                if (element.GetType() == typeof(Run))
                {
                    r = (Run)element.Clone();
                    break;
                }
            }

            //RunStyle

            RunProperties properties = null;

            if (isBold || isRed)
            {
                properties = (RunProperties)r.ChildElements[0];
            }

            if (isBold)
            {
                properties.Bold = new Bold();
            }
            else
                properties.Bold = null;

            if (isRed)
            {
                properties.Color = new Color() { Val = "FF0000" };
            }
            else
            {
                properties.Color = null;
            }

            Text t = (Text)r.ChildElements[1];
            t.Text = " " + text;
            //настройка, чтобы не резал пробелы
            t.Space = SpaceProcessingModeValues.Preserve;

            p.Append(r);
        }
    }
}
