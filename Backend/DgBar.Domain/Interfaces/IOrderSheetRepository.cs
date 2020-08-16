using DgBar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DgBar.Domain.Interfaces
{
    public interface IOrderSheetRepository
    {


        SheetOrder Save(SheetOrder sheetOrder);

        SheetOrder Update(SheetOrder sheetOrder);

        bool Delete(int idSheetOrder);

    }
}
