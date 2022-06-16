using Microsoft.EntityFrameworkCore;

namespace Api.ObjectModels.Base;

public abstract class ApplicationDbContextBase : DbContext
{
    protected ApplicationDbContextBase(DbContextOptions contextOptions)
        : base(contextOptions)
    {
    }
}