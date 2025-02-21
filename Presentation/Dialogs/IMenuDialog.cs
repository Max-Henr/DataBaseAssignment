namespace Presentation.Dialogs
{
    public interface IMenuDialog
    {
        Task NewContactDialog();
        Task NewCustomerDialog();
        Task Show();
        Task ViewAllContactDialog();
        Task ViewAllCustomerDialog();
        Task ViewAllRoles();
        Task NewRoleDialog();
    }
}