using DgBar.Domain.Entities;
using DgBar.Domain.Interfaces;
using DgBar.InfraData.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DgBar.InfraData.Repository
{
    public class OrderSheetRepository : IOrderSheetRepository
    {
          private readonly BarDGContext _context;

          public OrderSheetRepository(BarDGContext context)
          {
              this._context = context;
          }

          public SheetOrder GetOrderById(int Id)
          {
              return _context.SheetOrder.Where(x => x.IdOrder == Id).FirstOrDefault();
          }

          public SheetOrder GetItemByIdAndOrder(int? IdItem, int? IdOrder)
          {
            return _context.SheetOrder.FirstOrDefault(x => x.IdMenu == IdItem && x.IdOrder == IdOrder);
          }

          public List<SheetOrder> GetAllOrdersById(int Id)
          {

            var a = _context.SheetOrder.Where(i => i.IdOrder == Id).ToList();
            return a;
          }

          public SheetOrder Save(SheetOrder sheetOrder)
          {
              _context.Add(sheetOrder);
              _context.SaveChanges();
              return sheetOrder;
          }

          public SheetOrder Update(SheetOrder sheetOrder)
          {
              
            _context.SheetOrder.Update(sheetOrder);
            _context.SaveChanges();
              return sheetOrder;
          }


          public bool Delete(int? idSheetOrder)
          {
              var orderItem = _context.SheetOrder.Where(i => i.IdOrder == idSheetOrder).FirstOrDefault();
              if (idSheetOrder != null)
              {
                  _context.Entry(orderItem).State = EntityState.Deleted;
                  _context.SaveChanges();
                  return true;
              }
              return false;
          }

    }
}
