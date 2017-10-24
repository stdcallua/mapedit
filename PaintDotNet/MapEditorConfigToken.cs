/////////////////////////////////////////////////////////////////////////////////
// Paint.NET Effect Plugin Name: Barcode                                       //
// Author: Michael J. Sepcot                                                   //
// Version: 1.1.1                                                              //
// Release Date: 19 March 2007                                                 //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace MapEdit
{
    public class MapEditorConfigToken : PaintDotNet.Effects.EffectConfigToken
    {
        public MapEditorConfigToken()
            : base()
        {
        }

      

        public override object Clone()
        {
            return new MapEditorConfigToken();
        }
    }
}