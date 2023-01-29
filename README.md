# CSharpRunner
A .NET project to run C# script inside Autocad Runtime.

## Code references
Add all your code references to the line starting with "//#".
wrong reference declaration may get your program compilation errors.

## namespace rule
All your classes must be under AutoCADRunner namespace. otherwise assembly may not find
executable codes.

## Class rule
you must write a class with name "MainClass" and write your code main codes there.

## Main method rule
MainClass must contain a "Main" method where you can write actual scripts.

## Script structure
```
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
            //write your codes here.
        }
    }
}
```
## Example
```
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
```
