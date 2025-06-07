using server.Data;
using server.Entities;
using server.Interface.Repository;

namespace server.Repository
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly DataContext _context;

        public ImageRepository(DataContext context) : base(context)
        {
            this._context = context;
        }
    }
}
