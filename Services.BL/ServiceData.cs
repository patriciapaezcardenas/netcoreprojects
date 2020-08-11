using Modelos;
using Servicios.AD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.BL
{
    public class ServiceData : IServiceData
    {

        private readonly ClienteWS _context;

        public ServiceData(ClienteWS context)
        {
            _context = context;
        }

        public async Task<List<Album>> GetAlbumsAsync()
        {
            return await _context.GetAlbums();
        }

        public async Task<List<Comment>> GetComments()
        {
            return await _context.GetComments();
        }

        public async Task<List<Photo>> GetPhotos()
        {
            return await _context.GetPhotos();
        }
    }
}
