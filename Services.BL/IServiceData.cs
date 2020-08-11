using Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.BL
{
    public interface IServiceData
    {
        Task<List<Album>> GetAlbumsAsync();
        Task<List<Photo>> GetPhotos();
        Task<List<Comment>> GetComments();
    }
}
