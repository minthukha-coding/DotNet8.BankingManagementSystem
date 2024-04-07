﻿namespace DotNet8.BankingManagementSystem.Frontend.Pages.UserAndAccount.User;

public partial class P_User : ComponentBase
{
    private PageSettingModel _setting = new()
    {
        PageNo = 1,
        PageSize = 10
    };

    private UserListResponseModel? _model;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await List(_setting.PageNo, _setting.PageSize);
        }
    }

    private async Task List(int pageNo = 1, int pageSize = 10)
    {
        await InjectService.EnableLoading();
        _model = await ApiService.GetUserList(pageNo, pageSize);
        if (_model.Response.IsError)
        {
            //
            return;
        }

        StateHasChanged();
        await InjectService.DisableLoading();
    }

    private async Task PageChanged(int i)
    {
        _setting.PageNo = i;
        await List(_setting.PageNo, _setting.PageSize);
        //Nav.NavigateTo("/general-setup/user");
    }

    private async Task Delete(string userCode)
    {
        var parameters = new DialogParameters<Dialog>();
        parameters.Add(x => x.ContentText,
            "Are you sure want to delete?");

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<Dialog>("Confirm", parameters, options);
        var result = await dialog.Result;
        if (result.Canceled) return;

        var response = await ApiService.DeleteUser(userCode);
        if (result is not null)
        {
            await InjectService.EnableLoading();
            await List(_setting.PageNo, _setting.PageSize);
            await InjectService.DisableLoading();
            await InjectService.SuccessMessage("Deleting Successful.");
        }

        StateHasChanged();
    }

    private async Task Generate()
    {
        Random random = new Random();
        await InjectService.EnableLoading();
        var lst = await HttpClientService.GetAsync<UserRequestModel>("https://raw.githubusercontent.com/sannlynnhtun-coding/Banking-Management-System/main/User.json");
        lst.ForEach(x =>
        {
            int randomNumber = random.Next(1000000);
            x.Nrc = $"12/BHN(N){randomNumber}";
            x.StateCode = "MMR013";
            x.TownshipCode = "MMR013040";
        });
        await ApiService.CreateUsers(lst);
        await List(_setting.PageNo, _setting.PageSize);
    }
}