﻿namespace DotNet8.BankingManagementSystem.Frontend.Pages.Transaction.Withdraw;

public partial class P_Withdraw : ComponentBase
{
    private AccountRequestModel _model = new();
    private AccountListResponseModel? _accountListResponseModel;
    private UserListResponseModel? _userListResponseModel;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _accountListResponseModel = await ApiService.GetAccountList(0);
            _userListResponseModel = await ApiService.GetUserList(0);
            StateHasChanged();
        }
    }

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            var response = await ApiService.Withdraw(_model);
            await InjectService.Go("/user-and-account/account");
            await InjectService.SuccessMessage("Withdraw Successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}