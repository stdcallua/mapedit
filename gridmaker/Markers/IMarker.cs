using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gridmaker.Markers
{
    public interface IMarker
    {
        string Next();
        void Refresh(string start, string step);
    }

    public enum MarkerTypes
    {
        Number,
        String
    }
}
