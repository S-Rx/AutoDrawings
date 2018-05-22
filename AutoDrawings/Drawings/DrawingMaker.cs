using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvAddIn.Drawings
{
    class DrawingMaker
    {
        public List<SheetParameters> SheetsParameters { get; private set; } = new List<SheetParameters>();
        public DrawingDocument DrawDoc { get; set; }

        public DrawingMaker()
        {
            DrawDoc = CAddIn.App.Documents.Add(DocumentTypeEnum.kDrawingDocumentObject,
                CAddIn.App.FileManager.GetTemplateFile(DocumentTypeEnum.kDrawingDocumentObject)) as Inventor.DrawingDocument;
        }

        public SheetParameters AddSheetParameters(ComponentDefinition cdef)
        {
            var parameters = SheetParameters.CreateViewsAuto(cdef, DrawDoc);
            SheetsParameters.Add(parameters);
            return parameters;
        }

        public void CreateSheets()
        {
            foreach (var item in SheetsParameters)
            {
                var sheet = DrawDoc.Sheets.AddUsingSheetFormat(item.SheetFormat);

                var parameters = item.ViewsParameters[0];
                var baseview = sheet.DrawingViews.AddBaseView(item.Model, parameters.Point, 1.0, parameters.Orientation, parameters.Style);
                baseview.ScaleString = parameters.ScaleString;

                if (item.ViewsParameters.Count == 1) continue;

                for (int i = 1; i < item.ViewsParameters.Count; i++)
                {
                    var view_p = item.ViewsParameters[i];
                    //strange bug with Point2d Position parameter. Works fine if TransitionGeometry.CreatePoint2d inplace
                    //but E_INVALIDARG if make it in another place
                    var view = sheet.DrawingViews.AddProjectedView(baseview, view_p.Point.Copy(), view_p.Style);
                }

                sheet.TitleBlock.Delete();
                sheet.AddTitleBlock(item.TitleBlock);

                if (item.Model.DocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
                {
                    AddPartsList(sheet, baseview, item.SheetFormat.Name);
                }
            }
        }

        private void AddPartsList(Sheet sheet, DrawingView view, string format)
        {
            double x, y;
            if (format == "А4")
            {
                x = 2.0;
                y = 4.5;
            } else
            {
                x = 2.0;
                y = 0.5;
            }
            var oPlacementPoint = CAddIn.App.TransientGeometry.CreatePoint2d(x, y);
            var oPartsList = sheet.PartsLists.Add(view, oPlacementPoint, PartsListLevelEnum.kPartsOnly, null, 1, false);
            var vSize = oPartsList.RangeBox.MaxPoint.Y - oPartsList.RangeBox.MinPoint.Y;
            oPartsList.Position = CAddIn.App.TransientGeometry.CreatePoint2d(x, y + vSize);
        }
    }
}
