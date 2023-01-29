# CSharpRunner
A .NET project to run C# script inside Autocad/Civil 3d Runtime.

reference taken from (https://gist.github.com/fearofcode/3abb41d0b1a60194765f1fdd81da5269).
## Code references
Add all your code references to the line starting with "//#".
wrong reference declaration may get your program compilation errors.
Assembly which is not part of Autocad and .Net framework must be referenced with full path.
```
//#accoremgd.dll,acmgd.dll,acdbmgd.dll
```
with path
```
//#accoremgd.dll,acmgd.dll,acdbmgd.dll,C:\Users\RG\OneDrive\Desktop\example.dll
```
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
## getting Started with CSharpRunner

1) Download the CSharpRunner.dll for your autocad/Civil 3d version.
2) enter Netload command and select file you downloaded.
3) now enter "RUNSCRIPT" command on autocad/civil 3d and select c# file that you want to run.

## download
download the required version file from release folder.

