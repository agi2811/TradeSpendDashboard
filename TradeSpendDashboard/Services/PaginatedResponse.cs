using TradeSpendDashboard.Models.DTO;
using TradeSpendDashboard.Services.ExceptionHandler;
using IdentityModel.Client;
using System.Collections.Generic;

namespace TradeSpendDashboard.Services
{
    public class PaginatedResponse<TModel> : ProtocolResponse
    {
        public PaginatedResultDTO<TModel> Data
        {
            get
            {
                return Json?.ToObject<PaginatedResultDTO<TModel>>();
            }
        }

        public List<ValidationModelError> ModelErrors
        {
            get
            {
                if (IsError)
                {
                    List<ValidationModelError> errList = new List<ValidationModelError>();

                    if (Exception.Data != null && Exception.Data.Count > 0)
                    {
                        foreach (var key in Exception.Data.Keys)
                        {
                            var err = Exception.Data[key.ToString()].ToString();
                            errList.Add(new ValidationModelError { Name = key.ToString(), Reason = err });
                        }

                        return errList;
                    }
                }

                return null;
            }
        }
    }
}