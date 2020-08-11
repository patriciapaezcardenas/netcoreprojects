using Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Servicios.AD
{
    public class ClienteWS
    {
        private static readonly HttpClient client = new HttpClient();


        public ClienteWS()
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Album>> GetAlbums()
        {
            var url = "albums";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            List<Album> albums = JsonConvert.DeserializeObject<List<Album>>(resp);
            return albums;

        }


        public async Task<List<Photo>> GetPhotos()
        {
            var url = "photos";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            List<Photo> photos = JsonConvert.DeserializeObject<List<Photo>>(resp);
            return photos;

        }


        public async Task<List<Comment>> GetComments()
        {
            var url = "comments";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(resp);
            return comments;

        }

    }
}
