using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _I = Inventor;

namespace InvAddIn.Extensions
{
    public static class InventorExtensions
    {
        public static void BOMViewTraverse(this _I.BOMView view, Action<_I.ComponentDefinition, int> action)
        {
            int level = 0;
            QueryRows(view.BOMRows, level);

            void QueryRows(_I.BOMRowsEnumerator oBOMRows, int lvl)
            {
                foreach (_I.BOMRow oRow in oBOMRows)
                {
                    var oCompDef = oRow.ComponentDefinitions[1];
                    action(oCompDef, lvl);

                    if (oRow.ChildRows != null)
                    {
                        QueryRows(oRow.ChildRows, ++level);
                        level--;
                    }
                }
            }
        }

        public static bool IsProjectPart(this _I.ComponentDefinition oCompDef)
        {
            var filename = ((_I.Document)oCompDef.Document).FullFileName;
            var result = CAddIn.App.DesignProjectManager.IsFileInActiveProject(filename, out _I.LocationTypeEnum locationType, out string projectpath);
            return locationType == _I.LocationTypeEnum.kWorkspaceLocation;
        }

    }
}
