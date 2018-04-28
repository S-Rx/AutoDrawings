using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InvAddIn.Drawings
{
    class Drawing
    {
        public List<Sheet> Sheets { get; private set; } = new List<Sheet>();
        public List<SheetFormat> SheetFormats { get; private set; } = new List<SheetFormat>();
        public Sheet ActiveSheet => oDrawDoc.ActiveSheet;

        private DrawingDocument oDrawDoc;

        public Drawing()
        {
            var _template = CAddIn.App.FileManager.GetTemplateFile(DocumentTypeEnum.kDrawingDocumentObject);
            oDrawDoc = CAddIn.App.Documents.Add(DocumentTypeEnum.kDrawingDocumentObject, _template) as Inventor.DrawingDocument;
            foreach (Sheet item in oDrawDoc.Sheets)
            {
                Sheets.Add(item);
            }

            foreach (SheetFormat item in oDrawDoc.SheetFormats)
            {
                SheetFormats.Add(item);
            }
        }

        public Sheet AddSheet(SheetFormat format)
        {
            var sheet = this.oDrawDoc.Sheets.AddUsingSheetFormat(format);
            this.Sheets.Add(sheet);
            return sheet;
        }




    }
}
