/* *********************************************************************************************
 * CreateEmpty.cs
 * 
 * Create Ceiling (API REVIT 2024) 
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


namespace CreateBuild
{
    // -------------------------
    //          EMPTY
    // -------------------------
    /// <summary>
    /// Реализация revit add-in IExternalCommand interface
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Command_Create_Empty : IExternalCommand
    {

        // **************************
        //           MAIN
        //       (Enter Point)
        // **************************
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            MessageBox.Show("EMPTY");

            // -------------------------
            //   Code goes here ....
            // -------------------------

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

            return Result.Succeeded;

        } // ---  Execute (main) ---


    } // --- CommandCreatFloor : IExternalCommand ---


} // --- namespace CreateBuild ---
