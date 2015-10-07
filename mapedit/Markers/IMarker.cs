using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mapedit.Markers
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
