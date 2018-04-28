using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;
using InvAddIn;

namespace InvAddIn.UI {

	public abstract class RxRibbon {
        public List<RxButton> Buttons { get; private set; }
		public RibbonPanel Panel { get; set; }
        private RibbonParametersAttribute _Parameters { get; set; }

		public RxRibbon() {
            Buttons = new List<RxButton>();
			_Parameters = System.Attribute.GetCustomAttribute(this.GetType(), typeof(RibbonParametersAttribute)) as RibbonParametersAttribute;
			if (_Parameters == null) throw new ArgumentNullException("RibbonParametersAttribute", "Attribute was not found on class");
			Panel = CAddIn.App.UserInterfaceManager.Ribbons[_Parameters.Ribbon]
				.RibbonTabs.GetOrCreate(_Parameters.TabDisplayName, _Parameters.TabInternalName, _Parameters.ClientId)
				.RibbonPanels.GetOrCreate(_Parameters.PanelDisplayName, _Parameters.PanelInternalName, _Parameters.ClientId);
		}

		public void AddButton(RxButton btn) {
			Panel.CommandControls.AddButton(btn.ButtonDefinition, true);
            Buttons.Add(btn);
		}
    }

    public static class RibbonExtensions
    {
        public static RibbonTab GetOrCreate(this RibbonTabs tabs, string TabDisplayName, string TabInternalName, string ClientId)
        {
            if (tabs[TabInternalName] != null)
            {
                return tabs[TabInternalName];
            } else
            {
                return tabs.Add(TabDisplayName, TabInternalName, ClientId);
            }
        }

        public static RibbonPanel GetOrCreate(this RibbonPanels panels, string PanelDisplayName, string PanelInternalName, string ClientId)
        {
            if (panels[PanelInternalName] != null)
            {
                return panels[PanelInternalName];
            }
            else
            {
                return panels.Add(PanelDisplayName, PanelInternalName, ClientId);
            }
        }
    }

    //"id_RxTab"
    //"id_RxPanel"
    [RibbonParameters(ribbon: "Assembly", tabDisplayName: "СМК Инструменты", tabInternalName: "id_RxTab",
		panelDisplayName: "Спецификация", panelInternalName: "id_RxPanel")]
	public class ParametrizedRibbon: RxRibbon {}
}
