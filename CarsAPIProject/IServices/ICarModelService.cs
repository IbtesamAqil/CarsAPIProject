using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPIProject.Interfaces
{
    public interface ICarModelService
    {
        Task<string[]> GetModelsForMakeAndYear(string sMake, int nModelYear);
    }
}
