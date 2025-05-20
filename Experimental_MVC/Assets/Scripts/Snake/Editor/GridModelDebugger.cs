using BatuhanYigit.Grid2D.Editor;
using SnakeExample.Grid;
using UnityEditor;
using UnityEngine;

namespace SnakeExample.Editor
{
    public class GridModelDebugger : AbstractGridDebuggerWindow<IGridObject>
    {
        protected override int GetGUILayoutWidth() => 100;

        protected override int GetGUILayoutHeight() => 75;

        [MenuItem("Tools/Debug/Grid Model Debugger")]
        public static void ShowWindow()
        {
            GetWindow<GridModelDebugger>("Grid Model Debugger");
        }
        protected override string GetCellDisplayValue(IGridObject cellData)
        {
            return cellData.ObjectType.ToString();
        }
    }
}
