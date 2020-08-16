using DgBar.Domain.Entities;
using DgBar.Domain.Interfaces;
using DgBar.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace DgBar.Domain.Services
{
    public class OrderManage : IOrderSheetService
    {
        private readonly IOrderSheetRepository _repositoryOrder;
        private readonly IMenuRepository _repositoryMenu;

        public OrderManage(IOrderSheetRepository repositoryOrder, IMenuRepository repositoryMenu)
        {
            _repositoryOrder = repositoryOrder;
            _repositoryMenu = repositoryMenu;
        }

        public SheetOrder RegistryOrder(int idSheetOrder, int idItem)
        {
            try
            {
                SheetOrder sheetOrder =  _repositoryOrder.GetOrderById(idSheetOrder);
                SheetOrder sheetOrderItem =  _repositoryOrder.GetItemByIdAndOrder(idItem, idSheetOrder);
                Menu menu = _repositoryMenu.GetById(idItem);

                SheetOrder newOrderSheet = new SheetOrder();
                if (sheetOrder == null && menu != null)
                {
                    if(sheetOrderItem == null)
                    {
                        newOrderSheet.Id = idItem;
                        newOrderSheet.IdMenu = idItem;
                        newOrderSheet.Amount = 1;
                        _repositoryOrder.Save(newOrderSheet);
                    }
                    else
                    {
                         sheetOrderItem.Amount += 1;
                        _repositoryOrder.Update(sheetOrderItem);
                        return sheetOrderItem;
                    }
                }
                return newOrderSheet;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public OrderViewModel GenerateNote(int id)
        {
            List<SheetOrder> orders = _repositoryOrder.GetAllOrdersById(id);
            OrderViewModel note = new OrderViewModel();

                foreach (SheetOrder details in orders)
                {
                    var item = _repositoryMenu.GetById(details.IdMenu);
                    note.Items.Add(item);
                    
                    int freeWatter = 0;
                    int amoutBear = 0;
                    
                    switch (details.IdMenu)
                    {
                        case 1:
                            amoutBear = details.Amount;
                            int amoutDesc = amoutBear % 5;
                            var descountBeer = amoutDesc * item.Price;
                            note.Discount = descountBeer;
                            note.TotalDiscount += note.Discount;
                            break;
                        case 2:
                            if (amoutBear >= 2 && details.Amount >= 3)
                            {
                                freeWatter = 1;
                            }
                            break;
                        case 4:
                            if (freeWatter == 1)
                            {
                                note.Discount = -50;
                                note.TotalDiscount += note.Discount;
                                details.Amount += 1;
                            }
                            break;
                    }
                    note.TotalPrice += details.Amount * item.Price;
                    note.TotalDiscount += details.Discount;
            }

            return note;
        }


        public bool ResetOrder(int id)
        {
            try
            {
                var items = _repositoryOrder.GetAllOrdersById(id);
                foreach(SheetOrder item in items)
                {
                   var result = _repositoryOrder.Delete(2);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
        }

    }
}
