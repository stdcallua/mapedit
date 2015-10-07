using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gridmaker.Markers
{
    public class NumberMarker : IMarker
    {
        private float _number;
        private float _step;
        public string Next()
        {
            _number += _step;
            return _number.ToString();
        }

        public void Refresh(string start, string step)
        {
            if (!float.TryParse(start, out _number)) _number = 0;
            if (!float.TryParse(step, out _step)) _step = 1;
        }
    }
}
