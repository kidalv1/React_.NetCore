using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
  public interface IRepo
  {
    Task<IEnumerable<T>> FindPaged<T>(int page, int pageSize) where T : class;
  }
}
