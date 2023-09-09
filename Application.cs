/* *********************************************************************************************
 * BuildingShape.cs
 * 
 * Create Build (API REVIT 2024) 
 * Application (add-ins)
 * -------------------------------------------------------------------------------------------
 * Visual Studio 2022 
 * C# | .NET 4.8
 * -------------------------------------------------------------------------------------------
 * Revit API (Create Wall): 
 * https://www.revitapidocs.com/2020/4a42066c-bc44-0f99-566c-4e8327bc3bfa.htm
 * 
 * Writing sgiman @ 2023 
 * **********************************************************************************************/

using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;

// ***************************
//         APPLICATION
// ***************************

namespace CreateBuild
{
    // ----------------------
    //    add-in interface
    // ----------------------
    /// <summary>
    /// Реализация Revit add-in interface IExternalApplication
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Application : IExternalApplication
    {
        // --- On Shutdown ---
        /// <summary>
        /// Реализация Shutdown event
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }


        // --- On Startup ---
        /// <summary>
        /// Реализация OnStartup event
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel panel = RibbonPanel(application);
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            // ----- BS -----
            if(panel.AddItem(new PushButtonData("Create Floor", "Floor", thisAssemblyPath, "CreateBuild.Command_Create_Floor")) 
                is PushButton button1)
            {
                button1.ToolTip = "Create Floor IFC (TEST)";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "Hide.png"));
                BitmapImage bitmapImage = new BitmapImage(uri);
                button1.LargeImage = bitmapImage;
            }

            // ----- OWX -----
            if (panel.AddItem(new PushButtonData("Create Wall", "Wall", thisAssemblyPath, "CreateBuild.Command_Create_Wall"))
                is PushButton button2)
            {
                button2.ToolTip = "Create Wall IFC (TEST)";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "Register.png"));
                BitmapImage bitmapImage = new BitmapImage(uri);
                button2.LargeImage = bitmapImage;
            }

            // ----- OWY -----
            if (panel.AddItem(new PushButtonData("Create Roof", "Roof", thisAssemblyPath, "CreateBuild.Command_Create_Roof"))
                is PushButton button3)
            {
                button3.ToolTip = "Create Roof IFC (TEST)";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "Show.png"));
                BitmapImage bitmapImage = new BitmapImage(uri);
                button3.LargeImage = bitmapImage;
            }

            // ----- EMPTY -----
            if (panel.AddItem(new PushButtonData("Empty", "Empty", thisAssemblyPath, "CreateBuild.Command_Create_Empty"))
                is PushButton button4)
            {
                button4.ToolTip = "Empty Test IFC (R&D)";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "StrcturalWall.png"));
                BitmapImage bitmapImage = new BitmapImage(uri);
                button4.LargeImage = bitmapImage;
            }
            

            return Result.Succeeded;    

        }

        // ----------------------------------
        //           Ribbon Panel
        // ----------------------------------
        /// <summary>
        /// Function that creates RibbonPanel
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public RibbonPanel RibbonPanel(UIControlledApplication a)
        {
            string tab = "IFC Building";

            RibbonPanel ribbonPanel = null;

            try
            {
                a.CreateRibbonTab(tab);
            }   
            catch (Exception ex) 
            {
               Debug.WriteLine(ex.Message);
            }

            try
            {
                RibbonPanel panel = a.CreateRibbonPanel(tab, "IFC Building");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            List<RibbonPanel> panels = a.GetRibbonPanels(tab);
            foreach (RibbonPanel p in panels.Where(p => p.Name == "IFC Building"))
            {
                ribbonPanel = p;
            }

            return ribbonPanel;


        } // ---  RibbonPanel ---


    } //  --- Application : IExternalApplication ---

} // --- Class myFirstPlugin ---

