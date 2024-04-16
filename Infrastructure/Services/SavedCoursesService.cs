﻿using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Infrastructure.Services
{
    public class SavedCoursesService
    {
        private readonly SavedCoursesRepository _savedCoursesRepository;
        private readonly HttpClient _httpClient;
        public SavedCoursesService(HttpClient httpClient, SavedCoursesRepository savedCoursesRepository)
        {
            _savedCoursesRepository = savedCoursesRepository;
            _httpClient = httpClient;
        }

        public async Task<bool> SaveCourseAsync(SavedCoursesEntity entity)
        {
            try
            {
                if (entity != null)
                {
                    if (!await _savedCoursesRepository.Exists(x => x.UserId == entity.UserId && x.CourseId == entity.CourseId))
                    {
                        var result = await _savedCoursesRepository.AddToDB(entity);
                        return true;
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }

        public async Task<List<int>> GetSavedCoursesIdsAsync(string userId)
        {
            List<SavedCoursesEntity> dbList = new List<SavedCoursesEntity>();
         
            try
            {
                foreach (var course in await _savedCoursesRepository.GetAllFromDB())            
                    dbList.Add(course);
             
                if (dbList.Count > 0)
                {
                    List<int> idList = new List<int>();

                    for (int i = 0; i < dbList.Count; i++)
                    {
                        if (dbList[i].UserId == userId)
                        {
                            idList.Add(dbList[i].CourseId);
                        }
                    }
                    return idList;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<List<CourseDto>> PostIdsGetCoursesAsync(List<int> ids)
        {
            var url = $"https://localhost:7295/savedcourses?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1";
            var json = JsonConvert.SerializeObject(ids);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<CourseDto>>(responseData);
                return data!;
            }
            return null!;
        }

        public async Task<bool> RemoveCourse(int id, string userId)
        {
            try
            {
                if(await _savedCoursesRepository.Exists(x => x.UserId == userId && x.CourseId == id))
                {
                    var result = await _savedCoursesRepository.DeleteFromDb(x => x.UserId == userId && x.CourseId == id);
                    if (result == true)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error: {0}", e); }
            return false;
        }

        public async Task<bool> RemoveAllSavedCourses(string userId)
        {
            try
            {
                if (await _savedCoursesRepository.Exists(x => x.UserId == userId))
                {
                    var result = await _savedCoursesRepository.DeleteAllFromDb(x => x.UserId == userId);
                    if (result == true)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error: {0}", e); }
            return false;
        }
    }
}