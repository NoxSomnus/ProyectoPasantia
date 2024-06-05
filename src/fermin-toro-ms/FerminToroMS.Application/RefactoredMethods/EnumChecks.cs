using FerminToroMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.RefactoredMethods
{
    public class EnumChecks
    {
        public int RegularidadCheck(RegularidadEnum Regularidad) 
        {
            if (Regularidad == RegularidadEnum.Regular) return 8;
            if (Regularidad == RegularidadEnum.Intensivo) return 4;
            return 6;
        }
    }
}
