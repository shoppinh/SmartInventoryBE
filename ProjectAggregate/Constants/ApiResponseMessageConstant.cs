namespace WeSpace.Core.ProjectAggregate.Constants;

public static class ApiResponseMessageConstant
{
    #region Category
    public const string Category_GetSuccess = "Get category data successfully.";
    public const string Category_GetFailed = "Get category data failed.";
    public const string Category_GetNotFound = "Category not found.";
    public const string Category_AddSuccess = "Add new category successfully.";
    public const string Category_AddFailed = "Add new category failed.";
    public const string Category_UpdateSuccess = "Update category successfully.";
    public const string Category_UpdateFailed = "Update category failed.";
    public const string Category_DeleteSuccess = "Delete category successfully.";
    #endregion
    #region Product
    public const string Product_GetSuccess = "Get product data successfully.";
    public const string Product_GetFailed = "Get product data failed.";
    public const string Product_AddSuccess = "Add new product successfully.";
    public const string Product_AddFailed = "Add new product failed.";
    public const string Product_UpdateSuccess = "Update product successfully.";
    public const string Product_UpdateFailed = "Update product failed.";
    public const string Product_DeleteSuccess = "Delete product successfully.";
    public const string Product_GetNotFound = "Product not found.";
    #endregion
    #region Inventory
    public const string Inventory_GetSuccess = "Get inventory data successfully.";
    public const string Inventory_GetFailed = "Get inventory data failed.";
    public const string Inventory_AddSuccess = "Add new inventory successfully.";
    public const string Inventory_AddFailed = "Add new inventory failed.";
    public const string Inventory_UpdateSuccess = "Update inventory successfully.";
    public const string Inventory_UpdateFailed = "Update inventory failed.";
    public const string Inventory_DeleteSuccess = "Delete inventory successfully.";
    public const string Inventory_GetNotFound = "Inventory not found.";
    #endregion
    #region User
    public const string User_InvalidUser = "Invalid user.";
    public const string User_InvalidValidation = "Invalid validation.";
    public const string User_RegisterSuccess = "Register user successfully.";
    public const string User_RegisterFailed = "An error occurred while registering user.";
    public const string User_UpdateUserSuccess = "Update user successfully.";
    public const string User_UpdateUserFailed = "An error occurred while updating user infos.";
    public const string User_RemoveSuccess = "Remove user successfully.";
    public const string User_RemoveFailed = "An error occurred while removing user.";
    public const string User_SearchSuccess = "Search user successfully.";
    public const string User_SearchFailed = "An error occurred while searching user.";
    public const string User_GetYourTeamSuccess = "Get your team of user successfully.";
    public const string User_GetYourTeamFailed = "An error occurred while getting your team of user.";
    public const string User_AccountDisabled = "This user is not available.";
    public const string User_GetPhotoError = "An error occurred while getting photo.";
    public const string User_GetPhotoSuccess = "Get photo successfully.";
    public const string User_ImportUserSuccess = "Import user successfully.";
    public const string User_ImportUserFailed = "An error occurred while importing user.";
    public const string User_GetCurrentUserSuccess = "Get current user successfully.";
    public const string User_GetCurrentUserFailed = "An error occurred while getting current user.";
    public const string User_CountUserSuccess = "Count user successfully.";
    public const string User_CountUserFailed = "An error occurred while counting user.";
    public const string User_InvalidUserId = "Invalid userId.";
    public const string User_CannotRemove = "Cannot remove this user.";
    public const string User_SyncAzureUserError = "An error occurred while syncing Azure user";
    public const string User_InvalidDefaultOffice = "Invalid default office";
    public const string User_UserNotFound = "Cannot found this user";
    public const string User_UserNotFoundV2 = "Cannot find user";
    public const string User_ChangeLocationSuccess = "Change location successfully.";
    public const string User_ChangeLocationFailed = "An error occurred while changing location.";
    public const string User_UploadProfilePictureSuccesss = "Upload profile picture successfully.";
    public const string User_GetUserGroupFailed = "An error occurred while getting user group.";
    public const string User_GetUserGroupSuccess = "Get user group successfully.";
    #endregion

    #region Base crud api controller base

    public const string ApiControllerBase_GetSuccess = "Get data successfully.";
    public const string ApiControllerBase_GetFailed = "Get data failed.";
    public const string ApiControllerBase_AddSuccess = "Add new data successfully.";
    public const string ApiControllerBase_AddFailed = "Add new data failed.";
    public const string ApiControllerBase_UpdateSuccess = "Update data successfully.";
    public const string ApiControllerBase_UpdateFailed = "Update data failed.";
    public const string ApiControllerBase_DeleteSuccess = "Delete data successfully.";
    public const string ApiControllerBase_DeleteFailed = "Delete data failed.";
    public const string AuthControllerBase_AccessIsDenied = "Access is denied";

    #endregion

    #region Common

    public const string SomethingWentWrong = "Oops, Something went wrong!";
    public const string AzureTokenExpired = "Token expired. Please reauthenticate";
    public const string RequestTimeOut = "Request time out";
    public const string Forbidden = "You don't have permission to access this resource.";
    public const string Common_FromDateMustLowerThanToDate = "The FromDate must be earlier than the ToDate.";
    #endregion
}
