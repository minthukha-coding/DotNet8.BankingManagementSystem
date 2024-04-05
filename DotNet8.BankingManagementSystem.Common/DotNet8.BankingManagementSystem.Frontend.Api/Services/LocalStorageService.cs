﻿namespace DotNet8.BankingManagementSystem.Frontend.Api.Services;

public class LocalStorageService
{
    private readonly ILocalStorageService _localStorageService;

    public LocalStorageService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<List<T>> GetList<T>(string keyName)
    {
        var result = await _localStorageService.GetItemAsync<List<T>>(keyName);
        return result ?? new List<T>();
    }

    public async Task SetList<T>(string keyName, List<T> lst)
    {
        await _localStorageService.SetItemAsync(keyName, lst);
    }
}