﻿using DotNet8.BankingManagementSystem.App.Api;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.TownShip;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.Township
{
    public partial class P_Township
    {
        private PageSettingModel _setting = new PageSettingModel()
        {
            PageNo = 1,
            PageSize = 10
        };
        private TownshipListResponceModel? _model;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await List(_setting.PageNo, _setting.PageSize);
                StateHasChanged();
            }
        }
        private async Task List(int pageNo, int pageSize)
        {
            _model = await TownshipApi.GetTownships(pageNo, pageSize);
            if (_model.Response.IsError)
            {
                //
                return;
            }
            StateHasChanged();
        }
        private async Task PageChanged(int i)
        {
            _setting.PageNo = i;
            await List(_setting.PageNo, _setting.PageSize);
        }
    }
}