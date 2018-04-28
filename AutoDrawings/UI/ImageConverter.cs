using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.Compatibility.VB6;

namespace InvAddIn.UI {
	class ImageConverter: System.Windows.Forms.AxHost {
		public ImageConverter():base("") {}
		public static stdole.IPictureDisp ConvertImageToIPictureDisp(Image image) {
			if (image == null) return null;
			return GetIPictureDispFromPicture(image) as stdole.IPictureDisp;
		}
	}

	public static class ImageExtentions {
		public static stdole.IPictureDisp ToIPictureDisp(this Image image) {
			return Support.ImageToIPictureDisp(image) as stdole.IPictureDisp;
		}
	}
}
