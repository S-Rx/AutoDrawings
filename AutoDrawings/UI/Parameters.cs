using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Inventor;

namespace InvAddIn.UI {
	class RibbonParametersAttribute: System.Attribute {

		public string Ribbon { get; set; }

		public string TabDisplayName { get; set; }
		public string TabInternalName { get; set; }
		public string TargetTabInternalName { get; set; }
		public bool InsertBeforeTargetTab { get; set; }
		public bool Contextual { get; set; }

		public string PanelDisplayName { get; set; }
		public string PanelInternalName { get; set; }
		public string TargetPanelInternalName { get; set; }
		public bool InsertBeforeTargetPanel { get; set; }

		public string ClientId { get; set; }

		public RibbonParametersAttribute(string ribbon,
			string tabDisplayName, string tabInternalName, string panelDisplayName, string panelInternalName,
			string targetTabInternalName="", bool insertBeforeTargetTab=false, bool contextual=false,
			string targetPanelInternalName="", bool insertBeforeTargetPanel=false,
			string clientId="")
		{
			Ribbon = ribbon;
			TabDisplayName = tabDisplayName;
			TabInternalName = tabInternalName;
			TargetTabInternalName = targetTabInternalName;
			InsertBeforeTargetTab = insertBeforeTargetTab;
			Contextual = contextual;
			PanelDisplayName = panelDisplayName;
			PanelInternalName = panelInternalName;
			TargetPanelInternalName = targetPanelInternalName;
			InsertBeforeTargetPanel = insertBeforeTargetPanel;
			ClientId = clientId;
		}
	}

	class ButtonParametersAttribute: System.Attribute {

		public string DisplayName { get; set; }
		public string InternalName { get; set; }
		public CommandTypesEnum Classification { get; set; }
		public string ClientId { get; set; }
		public string DescriptionText { get; set; }
		public string ToolTipText { get; set; }

		public ButtonParametersAttribute(
			string displayName, string internalName,
			CommandTypesEnum classification=CommandTypesEnum.kQueryOnlyCmdType,
			string descriptionText="", string toolTipText="",
			string clientId="")
		{
			DisplayName = displayName;
			InternalName = internalName;
			Classification = classification;
			ClientId = clientId;
			DescriptionText = descriptionText;
			ToolTipText = toolTipText;
		}
	}
}
