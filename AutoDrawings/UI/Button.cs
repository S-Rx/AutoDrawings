using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;
using InvAddIn.Action;
using AutoDrawings;

namespace InvAddIn.UI {

	public abstract class RxButton {
		ButtonDefinition m_ButtonDef = null;
		protected stdole.IPictureDisp StandardIcon { get; set; }
		protected stdole.IPictureDisp LargeIcon { get; set; }

		public Inventor.ButtonDefinition ButtonDefinition {
			get {
				if (m_ButtonDef == null) {
					ButtonParametersAttribute p = System.Attribute.GetCustomAttribute(this.GetType(), typeof(ButtonParametersAttribute)) as ButtonParametersAttribute;
					if (p == null) throw new ArgumentNullException("ButtonParametersAttribute", "Attribute was not found on class");

					m_ButtonDef = CAddIn.App.CommandManager.ControlDefinitions.AddButtonDefinition(
						DisplayName: p.DisplayName,
						InternalName: p.InternalName,
						Classification: p.Classification,
						ClientId: p.ClientId,
						DescriptionText: p.DescriptionText,
						ToolTipText: p.ToolTipText,
						StandardIcon: StandardIcon,
						LargeIcon: LargeIcon);

					m_ButtonDef.OnExecute += ButtonDefinition_OnExecute;
					return m_ButtonDef;
				} else {
					return m_ButtonDef;
				}
			}
			set {
				StandardAddInServer_OnDeactivate(this, new EventArgs());
				m_ButtonDef = value;
			}
		}

		public RxButton() {
			StandardAddInServer.OnDeactivate += StandardAddInServer_OnDeactivate;
		}

		private void StandardAddInServer_OnDeactivate(object sender, EventArgs e) {
			StandardAddInServer.OnDeactivate -= StandardAddInServer_OnDeactivate;
			ButtonDefinition.OnExecute -= ButtonDefinition_OnExecute;
			ButtonDefinition.Delete();
		}

		public abstract void ButtonDefinition_OnExecute(NameValueMap Context);
	}


	[ButtonParameters(displayName: "Создать чертежи", internalName: "bd_AutoDrawingsCmd")]
	public class Button: RxButton {

		public Button():base() {
			StandardIcon = Properties.Resources.icon16.ToIPictureDisp();
			LargeIcon = Properties.Resources.icon32.ToIPictureDisp();
		}

		public override void ButtonDefinition_OnExecute(NameValueMap Context) {
			//System.Windows.Forms.MessageBox.Show("Моя первая кнопка");
            var action = new ButtonAction();
            action.Run();
		}

	}
}
