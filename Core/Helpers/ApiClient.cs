using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Domain;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Core.Helpers
{
    public class ApiClient : IApiClient
    {
        public HttpClient HttpClient { get; set; } = new HttpClient()
        {
            BaseAddress = new Uri("http://192.168.1.120:8080")
        };

        public Dictionary<Type,string> UrlPostDictionary { get; set; } = new Dictionary<Type, string>()
        {
            {typeof(TodoItem),"/api/ToDo/Create" }
        };

        public Dictionary<Type,string> UrlPutDictionary { get; set; } = new Dictionary<Type, string>()
        {
            {typeof(TodoItem),"/api/ToDo" }
        };

        public Dictionary<Type,string> UrlGetDictionary { get; set; } = new Dictionary<Type, string>()
        {
            {typeof(TodoItem),"/api/ToDo" }
        };

        public Dictionary<Type,string> UrlDeleteDictionary { get; set; } = new Dictionary<Type, string>()
        {
            {typeof(TodoItem),"/api/ToDo" }
        };

        public async Task<bool?> PostAsync<T>(T entity) where T : EntityBase
        {
            var json = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage result = null;
            try
            {
                result = await HttpClient.PostAsync(new Uri(GetPostUri<T>()), content);
            }
            catch (Exception)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast($"An error has ocurred creating {typeof(T)}. Check internet connection.");
            }
            return result?.IsSuccessStatusCode;
        }

        public async Task<bool?> DeleteAsync<T>(T entity) where T : EntityBase
        {
            HttpResponseMessage result = null;
            try
            {
                result = await HttpClient.DeleteAsync(new Uri($"{GetDeleteUri<T>()}/{entity.Key}"));
            }
            catch (Exception)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast($"An error has ocurred deleting {typeof(T)}. Check internet connection.");
            }
            return result?.IsSuccessStatusCode;
        }

        

        public async Task<bool?> PutAsync<T>(T entity) where T : EntityBase
        {
            var json = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage result = null;

            try
            {
                result = await HttpClient.PutAsync(new Uri($"{GetPutUri<T>()}/{entity.Key}"), content);
            }
            catch (Exception)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast($"An error has ocurred updating {typeof(T)}. Check internet connection.");
            }
            return result?.IsSuccessStatusCode;
        }

        public async Task<List<T>> GetAsync<T>() where T : EntityBase
        {
            HttpResponseMessage result = null;
            try
            {
                    result = await HttpClient.GetAsync(new Uri(GetGetUri<T>()));
            }
            catch (Exception)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast($"An error has ocurred getting {typeof(T)} from server. Check internet connection.");
            }
            return result?.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<List<T>>(await result.Content.ReadAsStringAsync()) : null;
        }

        private string GetDeleteUri<T>()
        {
            return UrlDeleteDictionary[typeof(T)];
        }
        private string GetPutUri<T>()
        {
            return UrlPutDictionary[typeof(T)];
        }

        private string GetPostUri<T>()
        {
            return UrlPostDictionary[typeof(T)];
        }

        private string GetGetUri<T>()
        {
            return UrlGetDictionary[typeof(T)];
        }
    }
}