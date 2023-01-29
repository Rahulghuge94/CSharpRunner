#region References
using System;
using System.IO;
using System.Linq;
using System.Text;
using wf = System.Windows.Forms;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.Generic;

#endregion

namespace CSharpRunner
{
    public class AssemblyPaths
    {
        static Dictionary<string, string> PATHS = new Dictionary<string, string>();
    }
    public class CSRunner
    {
        static CompilerParameters CompilerParams = new CompilerParameters
        {
            GenerateInMemory = true,
            TreatWarningsAsErrors = false,
            GenerateExecutable = false
        };

        static string[] references;
        static string[] code;
        static CSharpCodeProvider provider = new CSharpCodeProvider();
        static string pathAcad = @"C:\Program Files\Autodesk\AutoCAD 2018";
        static string pathC3d = @"C:\Program Files\Autodesk\AutoCAD 2018";
        static void CompileAndRun(string[] code)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            DateTime start = DateTime.Now;
            CompilerResults compile = provider.CompileAssemblyFromSource(CompilerParams, code);
            DateTime compilationFinished = DateTime.Now;

            //check if any error in compilation then run it.
            if (compile.Errors.HasErrors)
            {
                string ers = "";
                foreach (CompilerError ce in compile.Errors)
                {
                    ers = ers + ce.ToString() + "\n";
                }
                wf.MessageBox.Show("Compilation Error:\n" + ers.ToString());
                return;
            }
            else
            {
                System.Reflection.Assembly a = compile.CompiledAssembly;
                object o = a.CreateInstance("AutoCADRunner.MainClass");
                Type t = o.GetType();
                MethodInfo mi = t.GetMethod("Main");
                mi.Invoke(o, null);
            }

            DateTime executionFinished = DateTime.Now;
            ed.WriteMessage("\n" + $"Compile time: {compilationFinished - start}" + "\n");
            ed.WriteMessage($"Execution time: {executionFinished - compilationFinished}" + "\n");
        }
        [CommandMethod("RUNSCRIPT")]
        public void ReadFileAndRun()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            short fd = (short)Application.GetSystemVariable("FILEDIA");
            // As the user to select a .cs/vb file
            PromptOpenFileOptions pfo = new PromptOpenFileOptions("Select C#/VB.Net Script to run.");
            pfo.Filter = "CSharp Files (*.CS)|*.CS";
            PromptFileNameResult pr = ed.GetFileNameForOpen(pfo);

            if (pr.Status == PromptStatus.OK)
            {
                var fl = pr.StringResult;
                //ed.WriteMessage("RESULT1:\n" + fl);
                StringReader reader = new StringReader(System.IO.File.ReadAllText(fl));
                //code = new string[] { System.IO.File.ReadAllText(fl) };
                var tempcode = System.IO.File.ReadAllText(fl);

                if (tempcode.Contains("namespace AutoCADRunner") == false)
                {
                    wf.MessageBox.Show("All Codes must be under 'AutoCADRunner' namespace only.");
                    return;
                }

                if (tempcode.Contains("public class MainClass") == false)
                {
                    wf.MessageBox.Show("Executable class name must be 'public class MainClass'.");
                    return;
                }

                if (tempcode.Contains(" Main(") == false)
                {
                    wf.MessageBox.Show("There is No 'Main' method in MainClass. Run your procedure from Main method only.");
                    return;
                }

                string[] code_ = {tempcode};
                //ed.WriteMessage("RESULT2:\n" + reader.ReadToEnd());
                string[] lines = System.IO.File.ReadAllLines(fl);
                foreach (string line in lines)
                {
                    if (line.StartsWith("//#"))
                    {
                        var tempref = line.Replace("//#", "").Split(',');
                        references = GetReferences(tempref);
                        references.Append("System.dll");
                        references.Append("mscorlib.dll");
                        //ed.WriteMessage(string.Concat(references));
                        break;
                    }
                }
                //Add References and run the code.
                CompilerParams.ReferencedAssemblies.AddRange(references);
                CompileAndRun(code_);

            }
        }
        public string[] GetReferences(string[] refs)
        {
            var trefs = new List<string>();
            string[] filePaths = Directory.GetFiles(pathAcad, "*.dll", SearchOption.AllDirectories);
            foreach (string val in refs)
            {
                bool valfound = false;
                foreach (string filePath in filePaths)
                {
                    if (filePath.Contains(val))
                    {
                        valfound = true;
                        trefs.Add(filePath);
                    }
                }
                if (valfound = false)
                {
                    trefs.Append(val);
                }
            }
            return trefs.ToArray();
        }
    }
}
