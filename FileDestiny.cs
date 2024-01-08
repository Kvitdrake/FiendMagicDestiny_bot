using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Color = DocumentFormat.OpenXml.Wordprocessing.Color;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using NPOI.XWPF.UserModel;
using Telegram.Bot;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.VariantTypes;
using BottomBorder = DocumentFormat.OpenXml.Drawing.BottomBorder;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using ParagraphProperties = DocumentFormat.OpenXml.Drawing.ParagraphProperties;
using Path = System.IO.Path;
using RightBorder = DocumentFormat.OpenXml.Wordprocessing.RightBorder;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using RunProperties = DocumentFormat.OpenXml.Wordprocessing.RunProperties;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using TableCell = DocumentFormat.OpenXml.Wordprocessing.TableCell;
using TableCellProperties = DocumentFormat.OpenXml.Wordprocessing.TableCellProperties;
using TableProperties = DocumentFormat.OpenXml.Wordprocessing.TableProperties;
using TableRow = DocumentFormat.OpenXml.Wordprocessing.TableRow;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using System.Runtime.CompilerServices;
using Document = DocumentFormat.OpenXml.Wordprocessing.Document;

namespace FiendMagicDestiny_bot
{
    internal class FileDestiny
    {
        private string fileName;
        public FileDestiny(string fileName)
        {
            

        }
        public void CreateDocument(string fileName)
        {
            fileName = this.fileName;
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                Document doc = new Document();
                Body body = new Body();
                doc.Append(body);
                mainPart.Document = doc;

                wordDoc.Save();
            }
        }
        public void WriteToFile(string data)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileName, true))
            {
                Body body = wordDoc.MainDocumentPart.Document.Body;

                Paragraph paragraph = new Paragraph(new Run(new Text(data)));
                body.Append(paragraph);
                wordDoc.Save();
            }
        }
        public void FormattingTextToBold(string[] words)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileName, true))
            {
                Body body = wordDoc.MainDocumentPart.Document.Body;

                foreach(Paragraph paragraph in body.Elements<Paragraph>())
                {
                    foreach(Run run in paragraph.Elements<Run>())
                    {
                        foreach(Text text in run.Elements<Text>())
                        {
                            foreach(string word in words)
                            {
                                if (text.Text.Contains(word))
                                {
                                    RunProperties properties = new RunProperties();
                                    Bold bold = new Bold();
                                    properties.Append(bold);
                                    run.RunProperties = properties;
                                }
                            }
                        }
                    }
                }
                wordDoc.Save();
            }
        }
        public void FormattingTextToColor(string[] words, string color)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileName, true))
            {
                Body body = wordDoc.MainDocumentPart.Document.Body;

                foreach (Paragraph paragraph in body.Elements<Paragraph>())
                {
                    foreach (Run run in paragraph.Elements<Run>())
                    {
                        foreach (Text text in run.Elements<Text>())
                        {
                            foreach (string word in words)
                            {
                                if (text.Text.Contains(word))
                                {
                                    RunProperties properties = new RunProperties();
                                    Color colorEl = new Color() { Val = color };
                                    properties.Append(colorEl);
                                    run.RunProperties = properties;
                                }
                            }
                        }
                    }
                }
                wordDoc.Save();
            }
        }
        public async Task SendingFile(ITelegramBotClient botClient, long chatId, string fileName)
        {
            var FilePath = Path.Combine(Environment.CurrentDirectory, fileName);

            using (var stream = System.IO.File.OpenRead(FilePath))
            {
                var inputFile = InputFile.FromStream(stream);
                //InputFileStream inputFile = new InputFileStream(fileStream, Path.GetFileName(FilePath));
                await botClient.SendDocumentAsync(chatId, inputFile);
            }
        }
        
        public void DeleteDocument()
        {
            System.IO.File.Delete(fileName);
        }
    }
}
