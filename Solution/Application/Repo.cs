
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence;
using X.PagedList;

namespace Application
{
  public class Repo : IRepo
  {
    private readonly DataContext dataContext;
    public Repo(DataContext data)
    {
      this.dataContext = data;
    }

    public Repo() :base()
    {
      
    }
    public async Task<IEnumerable<T>> FindPaged<T>(int page, int pageSize) where T : class
    {
      IEnumerable<T> list = await this.dataContext.Set<T>().ToPagedListAsync(page, pageSize);
      int test = 5;
      return list;
    }
  }
}
