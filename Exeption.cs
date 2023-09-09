/* *********************************************************************************************
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
using System.Runtime.Serialization;

namespace CreateBuild
{
    [Serializable]
    internal class Exeption : Exception
    {
        public Exeption()
        {
        }

        public Exeption(string message) : base(message)
        {
        }

        public Exeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Exeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}