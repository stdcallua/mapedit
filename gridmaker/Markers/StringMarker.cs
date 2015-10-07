using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gridmaker.Markers
{
    public class StringMarker : IMarker
    {
        private int _number;
        private int _step;
        /*
        Function ConvertToLetter(iCol As Integer) As String
   Dim iAlpha As Integer
   Dim iRemainder As Integer
   iAlpha = Int(iCol / 27)
   iRemainder = iCol - (iAlpha* 26)
   If iAlpha > 0 Then
      ConvertToLetter = Chr(iAlpha + 64)
   End If
   If iRemainder > 0 Then
      ConvertToLetter = ConvertToLetter & Chr(iRemainder + 64)
   End If
End Function*/
        private string ConvertToLetter(int iCol)
        {
            int iAlpha = iCol / 27;
            int iRemainder = iCol - (iAlpha * 26);
            string result = String.Empty;
            if (iAlpha > 0)
                result = Convert.ToChar(iAlpha + 64).ToString();
            if (iRemainder > 0)
                result = result + Convert.ToChar(iRemainder + 64);
            return result;
        }

        public string Next()
        {
            _number += _step;
            return ConvertToLetter(_number);
        }

        public void Refresh(string start, string step)
        {
            if (!int.TryParse(start, out _number)) _number = 0;
            if (!int.TryParse(step, out _step)) _step = 1;
        }
    }
}
