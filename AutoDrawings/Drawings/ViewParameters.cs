using Inventor;

namespace InvAddIn.Drawings
{
    public class ViewParameters
    {
        public DrawingView ParentView { get; private set; }
        public Point2d Point { get; private set; }
        public string ScaleString { get; private set; }
        public ViewOrientationTypeEnum Orientation { get; private set; }
        public DrawingViewStyleEnum Style { get; private set; }
        public ViewJustificationEnum ViewJustification { get; private set; }

        public ViewParameters(DrawingView parentView = null,
            Point2d point = null,
            string scaleString = "1:10",
            ViewOrientationTypeEnum orientation = ViewOrientationTypeEnum.kDefaultViewOrientation,
            DrawingViewStyleEnum style = DrawingViewStyleEnum.kHiddenLineRemovedDrawingViewStyle,
            ViewJustificationEnum viewJustification = ViewJustificationEnum.kCenteredViewJustification)
        {
            ParentView = parentView;
            Point = point ?? CAddIn.App.TransientGeometry.CreatePoint2d(11.5, 22.0);
            ScaleString = scaleString;
            Orientation = orientation;
            Style = style;
            ViewJustification = viewJustification;
        }
    }
}