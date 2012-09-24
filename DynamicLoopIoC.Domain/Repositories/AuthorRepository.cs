using DynamicLoopIoC.Domain.Entities;

namespace DynamicLoopIoC.Domain.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public override Author CreateNew()
        {
            return new Author();
        }
    }
}