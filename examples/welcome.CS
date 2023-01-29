//#accoremgd.dll,acmgd.dll,acdbmgd.dll
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace AutoCADRunner
{
    public class MainClass
    {
        public void Main()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor Ed = doc.Editor;
            Ed.WriteMessage("Welcome From Autocad.");
        }
    }
}
