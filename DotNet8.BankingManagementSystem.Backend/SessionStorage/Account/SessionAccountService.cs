﻿namespace DotNet8.BankingManagementSystem.Backend.Services.Service.Localstorage.Account;

public class SessionAccountService
{
    private readonly SessionStorageService _service;

    public SessionAccountService(SessionStorageService service)
    {
        _service = service;
    }

    public async Task<AccountListResponseModel> GetAccountList()
    {
        AccountListResponseModel model = new AccountListResponseModel();
        var lst = await _service.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
        lst ??= new();

        model.Data = lst;
        model.Response = new MessageResponseModel(true, "Success.");
        return model;
    }

    public async Task<AccountResponseModel> GetAccount(AccountModel requestModel)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _service.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
        lst ??= new();
        var item = lst.FirstOrDefault(x => x.AccountNo == requestModel.AccountNo);
        if (item is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        model.Data = item;
        model.Response = new MessageResponseModel(true, "Success.");
        return model;
    }

    public async Task<AccountResponseModel> CreateAccount(AccountModel requestModel)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _service.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
        lst ??= new();
        lst.Add(requestModel);
        await _service.SetList(EnumService.Tbl_Account.GetKeyName(), lst);

        model.Response = new MessageResponseModel(true, "Account has been registered.");
        return model;
    }

    public async Task<AccountResponseModel> UpdateAccount(AccountModel requestModel)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _service.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
        var result = lst.FirstOrDefault(x => x.AccountNo == requestModel.AccountNo);
        var index = lst.FindIndex(x => result != null && x.AccountNo == result.AccountNo);
        if (result is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        result.AccountNo = requestModel.AccountNo;
        result.Balance = requestModel.Balance;
        result.CustomerCode = requestModel.CustomerCode;
        result.CustomerName = requestModel.CustomerName;
        lst[index] = result;

        await _service.SetList(EnumService.Tbl_Account.GetKeyName(), lst);
        model = new AccountResponseModel
        {
            Data = result,
            Response = new MessageResponseModel(true, "Account has been removed.")
        };
        return model;
    }

    public async Task<AccountResponseModel> DeleteAccount(AccountModel requestModel)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _service.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
        lst ??= new();
        var item = lst.FirstOrDefault(x => x.AccountNo == requestModel.AccountNo);
        if (item is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        lst.Remove(item);
        await _service.SetList(EnumService.Tbl_Account.GetKeyName(), lst);
        model.Response = new MessageResponseModel(true, "Account has been removed.");
        return model;
    }
}