/* *********************************************************************************************
 * CreateOWY.cs
 * 
 * Create Wall (API REVIT 2024) 
 * External Command 
 * -------------------------------------------------------------------------------------------
 * Visual Studio 2022 
 * C# | .NET 4.8
 * -------------------------------------------------------------------------------------------
 * Revit API : 
 * https://www.revitapidocs.com/2020/4a42066c-bc44-0f99-566c-4e8327bc3bfa.htm
 * 
 * Writing sgiman @ 2023 
 * **********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Windows.Forms;
using System.Diagnostics;


// ------------------------
//       CREATE WALL
// ------------------------
namespace CreateBuild
{
    /// <summary>
    /// Реализация revit add-in IExternalCommand interface
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Command_Create_Wall : IExternalCommand
    {
        // **************************
        //           MAIN
        //       (Enter Point)
        // **************************
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            MessageBox.Show("Create Wall");

            // -------------------------
            //   Code goes here ....
            // -------------------------

            /*
            UIApplication uiapp = commandData.Application;                          // application ui
            UIDocument uidoc = uiapp.ActiveUIDocument;                              // document ui     
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application; // application data 
            Document doc = uidoc.Document;                                          // document data
            */

            // Get application and document objects
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            // Grab Level
            FilteredElementCollector colLevels =
                new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.INVALID)
                .OfClass(typeof(Level));

            Element firstLevel = colLevels.FirstElement();

            // Access current selection
            using (Transaction tx = new Transaction(doc))
            {
                try
                {
                    tx.Start("Create_Wall");

                    // Create points
                    XYZ start = new XYZ(0, 0, 0);
                    XYZ end = new XYZ(20, 0, 0);

                    // Create line
                    Line geomLine = Line.CreateBound(start, end);

                    // Create wall
                    Wall.Create(doc, geomLine, firstLevel.Id, true);

                    // Выполнить!
                    tx.Commit();    

                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                    tx.RollBack();
                }

            } // --- using (tx) ---


            return Result.Succeeded;

        } // ---  Execute (main) ---


    } // --- CommandCreateWall : IExternalCommand ---


} // --- namespace CreateBuild ---
