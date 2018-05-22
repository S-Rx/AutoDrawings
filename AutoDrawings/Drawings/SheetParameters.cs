using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InvAddIn.Drawings
{
    public class SheetParameters
    {
        public SheetFormat SheetFormat { get; set; }
        public List<ViewParameters> ViewsParameters { get; private set; } = new List<ViewParameters>();
        public _Document Model { get; private set; }
        public DrawingDocument DrawDoc { get; private set; }
        public TitleBlockDefinition TitleBlock { get; set; }
        private TitleBlockDefinitions TitleBlocks { get; set; }


        public SheetParameters(ComponentDefinition cdef, DrawingDocument drawDoc)
        {
            Model = cdef.Document as _Document;
            DrawDoc = drawDoc;
            TitleBlocks = DrawDoc.TitleBlockDefinitions;
            TitleBlock = null;
        }

        public ViewParameters AddViewParameters()
        {
            var parameters = new ViewParameters();
            ViewsParameters.Add(parameters);
            return parameters;
        }

        private bool IsAssemblyModel()
        {
            return Model.DocumentType == DocumentTypeEnum.kAssemblyDocumentObject;
        }

        public void AddBaseView(Point2d point)
        {
            var parameters = new ViewParameters(point: point);
            ViewsParameters.Add(parameters);
        }

        public void AddProjectedView(Point2d point)
        {
            var parameters = new ViewParameters(point: point);
            ViewsParameters.Add(parameters);
        }

        public void AddTitleBlock(string sheetFormat)
        {
            if (sheetFormat.Contains("А4")) TitleBlock = TitleBlocks[1];
            else if (sheetFormat.Contains("А3")) TitleBlock = TitleBlocks[2];
        }

        public static SheetParameters CreateA42Views(ComponentDefinition cdef, DrawingDocument drawDoc)
        {
            var parameters = new SheetParameters(cdef, drawDoc);
            parameters.SheetFormat = drawDoc.SheetFormats["А4"];
            parameters.AddBaseView(point: CAddIn.App.TransientGeometry.CreatePoint2d(11.5, 22.5));
            parameters.AddProjectedView(point: CAddIn.App.TransientGeometry.CreatePoint2d(11.5, 15.5));
            return parameters;
        }

        public static SheetParameters CreateA43Views(ComponentDefinition cdef, DrawingDocument drawDoc)
        {
            var parameters = new SheetParameters(cdef, drawDoc);
            parameters.SheetFormat = drawDoc.SheetFormats["А4"];
            parameters.AddBaseView(point: CAddIn.App.TransientGeometry.CreatePoint2d(8.5, 22.5));
            parameters.AddProjectedView(point: CAddIn.App.TransientGeometry.CreatePoint2d(13.5, 22.5));
            parameters.AddProjectedView(point: CAddIn.App.TransientGeometry.CreatePoint2d(8.5, 15.5));
            return parameters;
        }

        public static SheetParameters CreateA32Views(ComponentDefinition cdef, DrawingDocument drawDoc)
        {
            var parameters = new SheetParameters(cdef, drawDoc);
            parameters.SheetFormat = drawDoc.SheetFormats["А3"];
            parameters.AddBaseView(point: CAddIn.App.TransientGeometry.CreatePoint2d(11.5, 22.5));
            parameters.AddProjectedView(point: CAddIn.App.TransientGeometry.CreatePoint2d(11.5, 15.5));
            return parameters;
        }

        public static SheetParameters CreateA33Views(ComponentDefinition cdef, DrawingDocument drawDoc)
        {
            var parameters = new SheetParameters(cdef, drawDoc);
            parameters.SheetFormat = drawDoc.SheetFormats["А3"];
            parameters.AddBaseView(point: CAddIn.App.TransientGeometry.CreatePoint2d(8.5, 22.5));
            parameters.AddProjectedView(point: CAddIn.App.TransientGeometry.CreatePoint2d(18.5, 22.5));
            parameters.AddProjectedView(point: CAddIn.App.TransientGeometry.CreatePoint2d(8.5, 15.5));
            return parameters;
        }

        public static SheetParameters CreateViewsAuto(ComponentDefinition cdef, DrawingDocument drawDoc)
        {
            var doc = cdef.Document as Inventor.Document;
            if (doc.ReferencedDocuments.Count > 2) {
                var parameters = SheetParameters.CreateA33Views(cdef, drawDoc);
                parameters.AddTitleBlock("А3");
                return parameters;
            }
            else if (doc.ReferencedDocuments.Count == 2)
            {
                var parameters = SheetParameters.CreateA42Views(cdef, drawDoc);
                parameters.AddTitleBlock("А3");
                return parameters;
            }
            else
            {
                var parameters = SheetParameters.CreateA42Views(cdef, drawDoc);
                parameters.AddTitleBlock("А4");
                return parameters;
            }
        }
    }
}
