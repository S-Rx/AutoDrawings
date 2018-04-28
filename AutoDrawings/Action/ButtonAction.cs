using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Inventor;
using InvAddIn.UI;
using InvAddIn.Extensions;


namespace InvAddIn.Action
{
    internal struct DrawingSheet
    {
        public Document oModel;
        public SheetFormat oFormat;
    }

    internal class ButtonAction
    {
        private AssemblyDocument oAsmDoc;
        private DrawingDocument oDrawDoc;

        public ButtonAction()
        {
            oAsmDoc = CAddIn.App.ActiveDocument as AssemblyDocument;
            oDrawDoc = CAddIn.App.Documents.Add(DocumentTypeEnum.kDrawingDocumentObject,
                CAddIn.App.FileManager.GetTemplateFile(DocumentTypeEnum.kDrawingDocumentObject)) as Inventor.DrawingDocument;
        }

        public void Run()
        {
            var oBOM = oAsmDoc.ComponentDefinition.BOM;
            oBOM.StructuredViewEnabled = true;
            oBOM.StructuredViewFirstLevelOnly = false;
            var oBOMView = oBOM.BOMViews[1];

            //Create drawing sheet for top level assembly document
            this.Action((ComponentDefinition)oAsmDoc.ComponentDefinition, 0);

            oBOMView.BOMViewTraverse(this.Action);
        }

        private void Action(ComponentDefinition cdef, int level)
        {
            if (!cdef.IsProjectPart()) return;

            var format = oDrawDoc.SheetFormats["А4"];
            var sheet = oDrawDoc.Sheets.AddUsingSheetFormat(format);
            var oTitleBlockDefs = oDrawDoc.TitleBlockDefinitions;

            var oPointBase = CAddIn.App.TransientGeometry.CreatePoint2d(115 / 10, 220 / 10);
            var oPointRel = CAddIn.App.TransientGeometry.CreatePoint2d(115 / 10, 150 / 10);

            var oBView = sheet.DrawingViews.AddBaseView(
                Model: (_Document)cdef.Document,
                Position: oPointBase,
                Scale: 1.0,
                ViewOrientation: ViewOrientationTypeEnum.kDefaultViewOrientation,
                ViewStyle:  DrawingViewStyleEnum.kHiddenLineDrawingViewStyle);
            oBView.ScaleString = "1:10";
            oBView.ViewJustification = ViewJustificationEnum.kCenteredViewJustification;

            var oDView = sheet.DrawingViews.AddProjectedView(
                ParentView: oBView,
                Position: oPointRel,
                ViewStyle: DrawingViewStyleEnum.kHiddenLineDrawingViewStyle);


            //TODO: разделить создание листа для сборки и для детали
            if (cdef.Type == ObjectTypeEnum.kAssemblyComponentDefinitionObject)
            {
                this.AddPartsList(sheet, oBView);
                sheet.TitleBlock.Delete();
                sheet.AddTitleBlock(oTitleBlockDefs[2]);
            }
        }

        private void AddPartsList(Sheet sheet, DrawingView view)
        {
            var oPlacementPoint = CAddIn.App.TransientGeometry.CreatePoint2d(2.0, 4.5);
            var oPartsList = sheet.PartsLists.Add(view, oPlacementPoint, PartsListLevelEnum.kPartsOnly, null, 1, false);
            var vSize = oPartsList.RangeBox.MaxPoint.Y - oPartsList.RangeBox.MinPoint.Y;
            oPartsList.Position = CAddIn.App.TransientGeometry.CreatePoint2d(2.0, 4.5 + vSize);
        }
    }
}
