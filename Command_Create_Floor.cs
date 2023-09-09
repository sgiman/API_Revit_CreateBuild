/* *********************************************************************************************
 * CreateOWX.cs
 * 
 * Create Floor (API REVIT 2024) 
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
    // -----------------------
    //      CREATE FLOOR
    // -----------------------

    /// <summary>
    /// Реализация revit add-in IExternalCommand interface
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Command_Create_Floor : IExternalCommand
    {

        // **************************
        //           MAIN
        //       (Enter Point)
        // **************************
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            MessageBox.Show("Create Floor");

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
           
            
            // Access current selection
            using (Transaction tx = new Transaction(doc))
            {
                try
                {
                    tx.Start("Create_Floor");

                    /// В приведенном ниже примере показано, как использовать Floor.
                    /// Метод Create для создания нового этажа с заданной отметкой,
                    /// на одном уровне, используя профиль геометрии и тип пола.

                    /// Он показывает, как адаптировать ваш код, 
                    /// использующий методы NewFloor и NewSlab, которые устарели с 2022 года.
                    /// В этом примере профиль геометрии представляет собой CurveLoop из линий, 
                    /// вы также можете использовать дуги, эллипсы и сплайны.


                    // Получаем тип пола для создания этажа
                    // Вы должны указать допустимый тип пола
                    // (в отличие от устаревших методов NewFloor и NewSlab).                     
                    ElementId floorTypeId = Floor.GetDefaultFloorType(doc, false);

                    // Получить уровень
                    // Вы должны указать допустимый уровень (в отличие от устаревших методов NewFloor и NewSlab).
                    double offset;
                    double elevation = 0.0d;
                    ElementId levelId = Level.GetNearestLevelId(doc, elevation, out offset);

                    // Build a floor profile for the floor creation
                    XYZ p1 = new XYZ(0, 0, 0);
                    XYZ p2 = new XYZ(20, 0, 0);
                    XYZ p3 = new XYZ(20, 15, 0);
                    XYZ p4 = new XYZ(0, 15, 0);
                    CurveLoop profile = new CurveLoop();
                    profile.Append(Line.CreateBound(p1, p2));
                    profile.Append(Line.CreateBound(p2, p3));
                    profile.Append(Line.CreateBound(p3, p4));
                    profile.Append(Line.CreateBound(p4, p1));

                    // The elevation of the curve loops is not taken into account (unlike in now obsolete NewFloor and NewSlab methods).
                    // If the default elevation is not what you want, you need to set it explicitly.
                    var floor = Floor.Create(doc, new List<CurveLoop> { profile }, floorTypeId, levelId);
                    Parameter param = floor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
                    param.Set(offset);


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


    } // --- Command_Creat_Floor : IExternalCommand ---


} // --- namespace CreateBuild ---


/***************************************************
// FLOOR
public static Floor Create(
            Document doc,
            IList<CurveLoop> profile,
            ElementId floorTypeId,
            ElementId levelId)

// WALL 
public static Wall Create(
            Document document,
            Curve curve,
            ElementId levelId,
            bool structural
)

// LINE
public static Line CreateBound(
            XYZ endpoint1,
            XYZ endpoint2
)
***************************************************/

