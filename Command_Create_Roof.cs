/* *********************************************************************************************
 * Command_Create_Roof.cs
 * Create Build (API REVIT 2024) 
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
using Autodesk.Revit.DB.Electrical;


// -------------------------
//        CREATE ROOF
// -------------------------

namespace CreateBuild
{
    /// <summary>
    /// Реализация revit add-in IExternalCommand interface
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Command_Create_Roof : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            MessageBox.Show("Create Roof");

            // Get application and document objects
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            // Access current selection
            using (Transaction tx = new Transaction(doc))
            {
                try
                {
                    tx.Start("Create_Roof");

                    // Получаем тип пола для создания этажа
                    // Вы должны указать допустимый тип пола
                    // (в отличие от устаревших методов NewFloor и NewSlab).                     
                    ElementId floorTypeId = Floor.GetDefaultFloorType(doc, false);

                    // Получить уровень
                    // Вы должны указать допустимый уровень (в отличие от устаревших методов NewFloor и NewSlab).
                    double offset = 0;
                    double elevation = 20;
                    //ElementId levelId = Level.GetNearestLevelId(doc, elevation, out offset);
                    Level lvl = Level.Create(doc, elevation);
                    //lvl.Id;

                    // Создаем профиль пола для создания пола
                    XYZ p1 = new XYZ(0, 0, 0);
                    XYZ p2 = new XYZ(20, 0, 0);
                    XYZ p3 = new XYZ(20, 15, 0);
                    XYZ p4 = new XYZ(0, 15, 0);
                    CurveLoop profile = new CurveLoop();
                    profile.Append(Line.CreateBound(p1, p2));
                    profile.Append(Line.CreateBound(p2, p3));
                    profile.Append(Line.CreateBound(p3, p4));
                    profile.Append(Line.CreateBound(p4, p1));

                    // Высота петель кривой не учитывается (в отличие от ныне устаревших методов NewFloor и NewSlab).
                    // Если высота по умолчанию не соответствует вашим требованиям, вам нужно установить ее явно.
                    var floor = Floor.Create(doc, new List<CurveLoop> { profile }, floorTypeId, lvl.Id);
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


            // -------------------------
            //   Code goes here ....
            // -------------------------


            return Result.Succeeded;
        }       

    }


}
